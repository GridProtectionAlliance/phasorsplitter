//******************************************************************************************************
//  ProxyConnectionStatus.cs - Gbtc
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
//  09/06/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;

namespace StreamSplitter
{
    #region [ Enumerations ]

    /// <summary>
    /// Defines connection state enumeration.
    /// </summary>
    [Serializable]
    public enum ConnectionState
    {
        /// <summary>
        /// Disabled state - gray.
        /// </summary>
        Disabled,
        /// <summary>
        /// Disconnected state - red.
        /// </summary>
        Disconnected,
        /// <summary>
        /// Connected with no data state - yellow.
        /// </summary>
        ConnectedNoData,
        /// <summary>
        /// Connected normally state - green.
        /// </summary>
        Connected
    }

    #endregion

    /// <summary>
    /// Represents the current status of a <see cref="StreamProxy"/>.
    /// </summary>
    [Serializable]
    public class StreamProxyStatus
    {
        #region [ Members ]

        // Constants

        /// <summary>
        /// Maximum size of status string to be maintained.
        /// </summary>
        /// <remarks>
        /// Value should not exceed TextBox.MaxLength (i.e., 32767).
        /// </remarks>
        public const int MaximumStatusSize = 1024;

        // Fields
        private ConnectionState m_connectionState;
        private string m_recentStatusMessages;
        private readonly Guid m_id;

        [NonSerialized]
        private readonly object m_updateLock;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="StreamProxyStatus"/>.
        /// </summary>
        /// <param name="id">ID of the associated stream proxy.</param>
        public StreamProxyStatus(Guid id)
        {
            m_id = id;
            m_updateLock = new object();
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the ID of the associated stream proxy.
        /// </summary>
        public Guid ID
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// Gets or sets current connection state for a stream proxy.
        /// </summary>
        public ConnectionState ConnectionState
        {
            get
            {
                return m_connectionState;
            }
            set
            {
                m_connectionState = value;
            }
        }

        /// <summary>
        /// Gets recent status messages for a stream proxy.
        /// </summary>
        /// <remarks>
        /// This property is usually accessed post-deserialization in the stream splitter manager.
        /// </remarks>
        public string RecentStatusMessages
        {
            get
            {
                return m_recentStatusMessages;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Append a new status message for the stream proxy.
        /// </summary>
        /// <param name="statusMessage">New status message to append to current messages.</param>
        /// <remarks>
        /// This method is usually accessed pre-serialization in the service (via <see cref="StreamProxy"/>).
        /// </remarks>
        public void AppendStatusMessage(string statusMessage)
        {
            if (string.IsNullOrWhiteSpace(statusMessage))
                return;

            lock (m_updateLock)
            {
                if (string.IsNullOrEmpty(m_recentStatusMessages))
                {
                    m_recentStatusMessages = ToolTipEx.WordWrapStatus(statusMessage);
                    return;
                }

                string updatedStatusMessages = m_recentStatusMessages + Environment.NewLine + ToolTipEx.WordWrapStatus(statusMessage);

                // Truncate from the left to maintain maximum status size
                if (updatedStatusMessages.Length > MaximumStatusSize)
                    updatedStatusMessages = updatedStatusMessages.Substring(updatedStatusMessages.Length - MaximumStatusSize);

                m_recentStatusMessages = updatedStatusMessages;
            }
        }

        #endregion
    }
}
