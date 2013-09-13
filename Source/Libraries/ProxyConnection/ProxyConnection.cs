//******************************************************************************************************
//  ProxyConnection.cs - Gbtc
//
//  Copyright © 2013, Grid Protection Alliance.  All Rights Reserved.
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
    /// Represents attributes for a proxy connection.
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
        private bool m_enabled;
        private string m_proxySettings;
        private string m_sourceSettings;

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
            get
            {
                return m_id;
            }
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
            get
            {
                if ((object)m_proxyConnectionEditor == null)
                    return m_temporaryConnectionString;

                return m_proxyConnectionEditor.ConnectionString;
            }
            set
            {
                // If no associated proxy connection editor control is available yet, we temporarily
                // cache any connection string until a control is referenced
                if ((object)m_proxyConnectionEditor != null)
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
                if ((object)m_proxyConnectionEditor == null)
                    return m_temporaryConnectionParameters;

                return m_proxyConnectionEditor.ConnectionParameters;
            }
            set
            {
                // If no associated proxy connection editor control is available yet, we temporarily
                // cache any connection parameters until a control is referenced
                if ((object)m_proxyConnectionEditor != null)
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
            get
            {
                return m_proxyConnectionEditor;
            }
            set
            {
                if ((object)value != null && (object)m_proxyConnectionEditor != null)
                    throw new InvalidOperationException("Proxy connection is already associated with an existing editing control, only one association is expected per instance.");

                m_proxyConnectionEditor = value;

                if ((object)m_proxyConnectionEditor != null)
                {
                    if (!string.IsNullOrWhiteSpace(m_temporaryConnectionString))
                        m_proxyConnectionEditor.ConnectionString = m_temporaryConnectionString;

                    if ((object)m_temporaryConnectionParameters != null)
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
        public string Name
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(m_name))
                    return m_name;

                return m_id.ToString();
            }
        }

        /// <summary>
        /// Gets enabled state for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
        }

        /// <summary>
        /// Gets source settings (or null if there are none) for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public string SourceSettings
        {
            get
            {
                return m_sourceSettings;
            }
        }

        /// <summary>
        /// Gets proxy settings (or null if there are none) for the proxy connection.
        /// </summary>
        /// <remarks>
        /// This value will only be as recent as the last update to the <see cref="ConnectionString"/>,
        /// if there is an associated editor control it may have more recent edited values.
        /// </remarks>
        public string ProxySettings
        {
            get
            {
                return m_proxySettings;
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
            m_enabled = false;
            m_sourceSettings = null;
            m_proxySettings = null;

            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            Dictionary<string, string> settings = connectionString.ParseKeyValuePairs();
            string setting;

            if (settings.TryGetValue("name", out setting) && !string.IsNullOrWhiteSpace(setting))
                m_name = setting;

            if (settings.TryGetValue("enabled", out setting))
                m_enabled = setting.ParseBoolean();

            if (settings.TryGetValue("sourceSettings", out setting))
                m_sourceSettings = setting;

            if (settings.TryGetValue("proxySettings", out setting))
                m_proxySettings = setting;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of property that changed.</param>
        protected virtual void OnProperyChanged(string propertyName)
        {
            if ((object)PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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
