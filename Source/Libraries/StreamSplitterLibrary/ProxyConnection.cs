//******************************************************************************************************
//  ProxyConnection.cs - Gbtc
//
//  Copyright © 2015, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse public License -v 1.0 (the "License"); you may
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
//  09/01/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using GSF;
using GSF.PhasorProtocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace StreamSplitter
{
    /// <summary>
    /// Represents the needed attributes for a <see cref="StreamProxy"/> connection.
    /// </summary>
    [Serializable]
    public class ProxyConnection : INotifyPropertyChanged, ISerializable
    {
        #region [ Members ]

        // Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Fields
        private Guid m_id;
        private IConnectionParameters m_connectionParameters;
        private string m_connectionString;
        private string m_name;
        private readonly Dictionary<ConnectionState, string> m_connectionStateDescriptions;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ProxyConnection"/>.
        /// </summary>
        public ProxyConnection()
        {
            m_id = Guid.NewGuid();
            m_connectionStateDescriptions = InitializeConnectionStateDescriptions();
        }

        /// <summary>
        /// Creates a new <see cref="ProxyConnection"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ProxyConnection(SerializationInfo info, StreamingContext context)
        {
            // Deserialize proxy connection fields
            try
            {
                m_id = (Guid)info.GetValue("ID", typeof(Guid));
                ConnectionParameters = (IConnectionParameters)info.GetValue("connectionParameters", typeof(IConnectionParameters));
                ConnectionString = info.GetString("connectionString");
            }
            catch (SerializationException)
            {
                m_id = Guid.NewGuid();
                ConnectionParameters = null;
                ConnectionString = "";
            }

            m_connectionStateDescriptions = InitializeConnectionStateDescriptions();
        }

        private Dictionary<ConnectionState, string> InitializeConnectionStateDescriptions() => 
            Enum.GetValues(typeof(ConnectionState))
                .Cast<ConnectionState>()
                .ToDictionary(state => state, state => state.GetDescription());

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets ID for the proxy connection.
        /// </summary>
        public Guid ID
        {
            get => m_id;
            set
            {
                m_id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Gets or set connection string.
        /// </summary>
        public string ConnectionString
        {
            get => m_connectionString;
            set
            {
                m_connectionString = ParseConnectionString(value);
                OnPropertyChanged("ConnectionString");
            }
        }

        /// <summary>
        /// Gets or sets protocol specific connection parameters.
        /// </summary>
        public IConnectionParameters ConnectionParameters
        {
            get => m_connectionParameters;
            set
            {
                m_connectionParameters = value;
                OnPropertyChanged("ConnectionParameters");
            }
        }

        /// <summary>
        /// Gets name for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public string Name => !string.IsNullOrWhiteSpace(m_name) ? m_name : m_id.ToString();

        /// <summary>
        /// Gets enabled state for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>.
        /// </remarks>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Gets source settings (or null if there are none) for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>.
        /// </remarks>
        public string SourceSettings { get; private set; }

        /// <summary>
        /// Gets proxy settings (or null if there are none) for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>.
        /// </remarks>
        public string ProxySettings { get; private set; }

        /// <summary>
        /// Gets or sets <see cref="ConnectionState"/> for the <see cref="ProxyConnection"/> controlling the connection state.
        /// </summary>
        public ConnectionState ConnectionState { get; set; }

        /// <summary>
        /// Gets the description of the <see cref="ConnectionState"/> for the <see cref="ProxyConnection"/>.
        /// </summary>
        public string ConnectionStateDescription => m_connectionStateDescriptions[ConnectionState];

        /// <summary>
        /// Gets or sets the <see cref="ProxyConnection"/> visibility flag.
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Parse primary values for proxy connection from updated connection string. 
        /// </summary>
        /// <param name="connectionString">New connection string to parse.</param>
        public string ParseConnectionString(string connectionString)
        {
            // Reset fields to their default value
            m_name = m_id.ToString();
            Enabled = false;
            SourceSettings = null;
            ProxySettings = null;

            if (string.IsNullOrWhiteSpace(connectionString))
                return string.Empty;

            Dictionary<string, string> settings = connectionString.ParseKeyValuePairs();

            if (settings.TryGetValue("name", out string setting) && !string.IsNullOrWhiteSpace(setting))
                m_name = setting;

            if (settings.TryGetValue("enabled", out setting))
                Enabled = setting.ParseBoolean();

            if (settings.TryGetValue("sourceSettings", out setting))
            {
                Dictionary<string, string> sourceSettings = setting.ParseKeyValuePairs();
                ProxyConnectionEditor.InjectMaxSendQueueSize(sourceSettings);
                SourceSettings = sourceSettings.JoinKeyValuePairs();
                settings["sourceSettings"] = SourceSettings;
            }

            if (settings.TryGetValue("proxySettings", out setting))
            {
                Dictionary<string, string> proxySettings = setting.ParseKeyValuePairs();
                ProxyConnectionEditor.InjectMaxSendQueueSize(proxySettings);
                ProxySettings = proxySettings.JoinKeyValuePairs();
                settings["proxySettings"] = ProxySettings;
            }

            return settings.JoinKeyValuePairs();
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination <see cref="StreamingContext"/> for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize proxy connection fields
            info.AddValue("ID", m_id, typeof(Guid));
            info.AddValue("connectionParameters", ConnectionParameters, typeof(IConnectionParameters));
            info.AddValue("connectionString", ConnectionString);
        }

        #endregion
    }
}
