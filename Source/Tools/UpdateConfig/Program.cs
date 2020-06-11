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
using GSF;
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

            const string ProxySettings = "protocol=Udp; interface=0.0.0.0; port=-1; clients=192.168.1.100:{0}, 192.168.1.101:{0}, 192.168.1.102:{0}, 127.0.0.1:{0}";

            foreach (ProxyConnection proxy in configuration)
            {
                Dictionary<string, string> settings = proxy.ConnectionString.ParseKeyValuePairs();

                Console.WriteLine($"   >> Updating \"{proxy.Name}\"...");

                if (settings.TryGetValue("proxySettings", out string proxySettingsConnectionString))
                {
                    Dictionary<string, string> proxySettings = proxySettingsConnectionString.ParseKeyValuePairs();

                    if (proxySettings.TryGetValue("clients", out string clientValues))
                    {
                        string[] clients = clientValues.Split(',');

                        if (clients.Length > 0)
                        {
                            string[] parts = clients[0].Split(':');

                            if (parts.Length == 2 && ushort.TryParse(parts[1], out ushort port))
                            {
                                settings["proxySettings"] = string.Format(ProxySettings, port);
                                proxy.ConnectionString = settings.JoinKeyValuePairs();
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Saving updated config file...");
            ProxyConnectionCollection.SaveConfiguration(configuration, FilePath.GetAbsolutePath("UpdatedConfig.s3config"));

            return 0;
        }
    }
}
