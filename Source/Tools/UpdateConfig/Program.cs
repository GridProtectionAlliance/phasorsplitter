//******************************************************************************************************
//  Program.cs - Gbtc
//
//  Copyright © 2020, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  06/11/2020 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GSF;
using GSF.Collections;
using GSF.IO;
using StreamSplitter;

namespace UpdateConfig
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Expected one argument for source configuration file name.");
                return 1;
            }

            string configurationFile = FilePath.GetAbsolutePath(args[0]);

            if (!File.Exists(configurationFile))
            {
                Console.Error.WriteLine($"Specified configuration file \"{configurationFile}\" does not exist.");
                return 2;
            }

            // Attempt to load current configuration
            ProxyConnectionCollection configuration = ProxyConnectionCollection.LoadConfiguration(configurationFile);

            Console.WriteLine($"Loaded {configuration.Count:N0} connections from \"{configurationFile}\".");

            foreach (ProxyConnection proxy in configuration.ToArray())
            {
                Dictionary<string, string> settings = proxy.ConnectionString.ParseKeyValuePairs();

                Console.WriteLine($"   >> Updating \"{proxy.Name}\"...");

                ushort port = 0;

                // Get target port from first client in UDP proxy settings
                if (settings.TryGetValue("proxySettings", out string proxySettingsConnectionString))
                {
                    Dictionary<string, string> proxySettings = proxySettingsConnectionString.ParseKeyValuePairs();

                    if ((proxySettings.TryGetValue("protocol", out string protocol) || proxySettings.TryGetValue("transportProtocol", out protocol)) && protocol.Equals("Udp", StringComparison.OrdinalIgnoreCase))
                    {
                        if (proxySettings.TryGetValue("clients", out string clientValues))
                        {
                            string[] clients = clientValues.Split(',');

                            if (clients.Length > 0)
                            {
                                string[] parts = clients[0].Split(':');

                                if (parts.Length == 2)
                                {
                                    ushort.TryParse(parts[1], out port);

                                    // Update existing proxy UDP connections publish to two destinations, primary and backup
                                    proxySettings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                    {
                                        ["protocol"] = "Udp",
                                        ["interface"] = "0.0.0.0",
                                        ["port"] = "-1",
                                        ["clients"] = $"192.168.1.142:{port}, 192.168.1.143:{port}"
                                    };

                                    settings["proxySettings"] = proxySettings.JoinKeyValuePairs();
                                    proxy.ConnectionString = settings.JoinKeyValuePairs();
                                }
                            }
                        }
                    }
                }

                if (port == 0)
                {
                    Console.WriteLine($"      >> Unable to determine target port for \"{proxy.Name}\".");
                    continue;
                }

                if (settings.TryGetValue("sourceSettings", out string sourceSettingsConnectionString))
                {
                    Dictionary<string, string> sourceSettings = sourceSettingsConnectionString.ParseKeyValuePairs();

                    // Add a new TCP proxy record for field device connection
                    if ((sourceSettings.TryGetValue("transportProtocol", out string protocol) || sourceSettings.TryGetValue("protocol", out protocol)) && protocol.Equals("Tcp", StringComparison.OrdinalIgnoreCase))
                    {
                        // Add new TCP proxy record for field device connection
                        ProxyConnection newProxy = new()
                        {
                            ConnectionString = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                            {
                                ["sourceSettings"] = sourceSettingsConnectionString,
                                ["proxySettings"] = $"protocol=Tcp; interface=0.0.0.0; port={port}",
                                ["enabled"] = "True",
                                ["name"] = $"{proxy.Name} TCP Proxy"
                            }
                            .JoinKeyValuePairs()
                        };

                        configuration.Add(newProxy);
                        Console.WriteLine($"      >> Added new local TCP proxy \"{newProxy.Name}\"...");

                        // Update existing connection to connect to local TCP proxy
                        sourceSettings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                        {
                            ["transportProtocol"] = "Tcp",
                            ["interface"] = "0.0.0.0",
                            ["server"] = $"127.0.0.1:{port}",
                            ["phasorProtocol"] = "IEEEC37_118V1",
                            ["accessID"] = "1"
                        };

                        settings["sourceSettings"] = sourceSettings.JoinKeyValuePairs();
                        proxy.ConnectionString = settings.JoinKeyValuePairs();

                        Console.WriteLine($"      >> Updated \"{proxy.Name}\" to connect to local TCP proxy.");
                    }
                }
            }

            ProxyConnectionCollection sortedConfig = new();
            sortedConfig.AddRange(configuration.OrderBy(connection => connection.Name));

            Console.WriteLine("Saving updated config file...");
            ProxyConnectionCollection.SaveConfiguration(sortedConfig, FilePath.GetAbsolutePath("UpdatedConfig.s3config"));

            Console.ReadKey();
            return 0;
        }
    }
}
