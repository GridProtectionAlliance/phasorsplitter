//******************************************************************************************************
//  Program.cs - Gbtc
//
//  Copyright © 2015, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  09/04/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

#if RELEASE
using Microsoft.Extensions.Logging.EventLog;
#endif

using Gemstone.Configuration;
using Gemstone.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging;

namespace StreamSplitter;

internal class Program
{
    private static void Main(string[] args)
    {
        // Define settings for the service. Note that the Gemstone defaults
        // for handling INI and SQLite configuration are defined in a hierarchy
        // where the configuration settings are loaded are in the following
        // priority order, from lowest to highest:
        // - INI file (defaults.ini) - Machine Level, %programdata% folder
        // - INI file (settings.ini) - Machine Level, %programdata% folder
        // - SQLite database (settings.db) - User Level, %appdata% folder (not used by service)
        // - Environment variables - Machine Level
        // - Environment variables - User Level
        // - Command line arguments
        Settings settings = new()
        {
            INIFile = ConfigurationOperation.ReadWrite,
            SQLite = ConfigurationOperation.Disabled
        };

        // Define settings for service components
        ServiceHost.DefineSettings(settings);

        // Bind settings to configuration sources
        settings.Bind(new ConfigurationBuilder()
            .ConfigureGemstoneDefaults(settings)
            .AddCommandLine(args, settings.SwitchMappings));

        HostApplicationBuilderSettings appSettings = new()
        {
            Args = args,
            ApplicationName = nameof(StreamSplitter),
            DisableDefaults = true,
        };

        HostApplicationBuilder application = new(appSettings);

        application.Services.AddWindowsService(options =>
        {
            options.ServiceName = appSettings.ApplicationName;
        });

        application.Services.AddHostedService<ServiceHost>();

        ConfigureLogging(application.Logging);

        IHost host = application.Build();
        host.Run();

#if DEBUG
        Settings.Save(forceSave: true);
#else
        Settings.Save();
#endif
    }

    internal static void ConfigureLogging(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Information);

        builder.AddFilter("Microsoft", LogLevel.Warning);
        builder.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Error);
        builder.AddFilter<DebugLoggerProvider>("", LogLevel.Debug);
        builder.AddFilter<DiagnosticsLoggerProvider>("", LogLevel.Trace);

        builder.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Error);
        builder.AddDebug();

        // Add Gemstone diagnostics logging
        builder.AddGemstoneDiagnostics();

#if RELEASE
        if (OperatingSystem.IsWindows())
        {
            builder.AddFilter<EventLogLoggerProvider>("Application", LogLevel.Warning);
            builder.AddEventLog();
        }
#endif
    }
}