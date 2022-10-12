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
        private ProxyConnectionEditor m_proxyConnectionEditor;
        private IConnectionParameters m_temporaryConnectionParameters;
        private string m_temporaryConnectionString;
        private string m_name;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ProxyConnection"/>.
        /// </summary>
        public ProxyConnection()
        {
            m_id = Guid.NewGuid();
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
                m_temporaryConnectionString = "";
            }
        }

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
                OnProperyChanged("ID");
            }
        }

        /// <summary>
        /// Gets or set connection string.
        /// </summary>
        public string ConnectionString
        {
            get => m_proxyConnectionEditor is null ? m_temporaryConnectionString : m_proxyConnectionEditor.ConnectionString;
            set
            {
                // If no associated proxy connection editor control is available yet, we temporarily
                // cache any connection string until a control is referenced
                if (m_proxyConnectionEditor is not null)
                    m_proxyConnectionEditor.ConnectionString = value;
                else
                    m_temporaryConnectionString = value;

                // Parse primary values from connection string
                ParseConnectionString(value);

                OnProperyChanged("ConnectionString");
            }
        }

        /// <summary>
        /// Gets or sets protocol specific connection parameters.
        /// </summary>
        public IConnectionParameters ConnectionParameters
        {
            get
            {
                if (m_proxyConnectionEditor is null)
                    return m_temporaryConnectionParameters;

                return m_proxyConnectionEditor.ConnectionParameters;
            }
            set
            {
                // If no associated proxy connection editor control is available yet, we temporarily
                // cache any connection parameters until a control is referenced
                if (m_proxyConnectionEditor is not null)
                    m_proxyConnectionEditor.ConnectionParameters = value;
                else
                    m_temporaryConnectionParameters = value;

                OnProperyChanged("ConnectionParameters");
            }
        }

        /// <summary>
        /// Gets or sets reference to associated <see cref="ProxyConnectionEditor"/>.
        /// </summary>
        public ProxyConnectionEditor ProxyConnectionEditor
        {
            get => m_proxyConnectionEditor;
            set
            {
                if (value is not null && m_proxyConnectionEditor is not null)
                    throw new InvalidOperationException("Proxy connection is already associated with an existing editing control, only one association is expected per instance.");

                m_proxyConnectionEditor = value;

                if (m_proxyConnectionEditor is not null)
                {
                    if (!string.IsNullOrWhiteSpace(m_temporaryConnectionString))
                        m_proxyConnectionEditor.ConnectionString = m_temporaryConnectionString;

                    if (m_temporaryConnectionParameters is not null)
                        m_proxyConnectionEditor.ConnectionParameters = m_temporaryConnectionParameters;
                }

                OnProperyChanged("ProxyConnectionEditor");
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
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Gets source settings (or null if there are none) for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public string SourceSettings { get; private set; }

        /// <summary>
        /// Gets proxy settings (or null if there are none) for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public string ProxySettings { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ProxyConnection"/> visibility flag.
        /// </summary>
        public bool Visible
        {
            get => m_proxyConnectionEditor?.Visible ?? false;
            set
            {
                if (m_proxyConnectionEditor is not null)
                    m_proxyConnectionEditor.Visible = value;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Parse primary values for proxy connection from updated connection string. 
        /// </summary>
        /// <param name="connectionString">New connection string to parse.</param>
        public void ParseConnectionString(string connectionString)
        {
            // Reset fields to their default value
            m_name = m_id.ToString();
            Enabled = false;
            SourceSettings = null;
            ProxySettings = null;

            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            Dictionary<string, string> settings = connectionString.ParseKeyValuePairs();

            if (settings.TryGetValue("name", out string setting) && !string.IsNullOrWhiteSpace(setting))
                m_name = setting;

            if (settings.TryGetValue("enabled", out setting))
                Enabled = setting.ParseBoolean();

            if (settings.TryGetValue("sourceSettings", out setting))
                SourceSettings = setting;

            if (settings.TryGetValue("proxySettings", out setting))
                ProxySettings = setting;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of property that changed.</param>
        protected virtual void OnProperyChanged(string propertyName) => 
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
