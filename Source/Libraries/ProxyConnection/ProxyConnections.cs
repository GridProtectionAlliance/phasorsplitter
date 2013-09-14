//******************************************************************************************************
//  ProxyConnections.cs - Gbtc
//
//  Copyright © 2013, Grid Protection Alliance.  All Rights Reserved.
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
//  09/02/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using GSF;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;

namespace StreamSplitter
{
    /// <summary>
    /// Defines a collection of <see cref="ProxyConnection"/> instances.
    /// </summary>
    /// <remarks>
    /// This class will optionally monitor and collectively raise events from <see cref="ProxyConnectionEditor"/>
    /// controls if any of the <see cref="ProxyConnection"/> instances in the collection are associated with an
    /// editor - this association can happen at any time during the life-cycle of the proxy connection. Even so,
    /// this collection of proxy connection instances can exist happily without editor associations which also
    /// makes the class suitable for use in non-UI based applications, e.g., a service host.
    /// </remarks>
    [Serializable]
    public class ProxyConnections : BindingList<ProxyConnection>, ISerializable
    {
        #region [ Members ]

        // Events

        /// <summary>
        /// Sends notification that the <see cref="ProxyConnection"/> associated editor control has focus.
        /// </summary>
        /// <remarks>
        /// This event will only be raised if the proxy connection is associated with an editor control.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection>> GotFocus;

        /// <summary>
        /// Sends notification that the <see cref="ProxyConnection"/> has changed and should be saved.
        /// </summary>
        /// <remarks>
        /// This event will only be raised if the proxy connection is associated with an editor control.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection>> ConfigurationChanged;

        /// <summary>
        /// Sends notification that the <see cref="ProxyConnection"/> enabled state has changed.
        /// </summary>
        /// <remarks>
        /// This event will only be raised if the proxy connection is associated with an editor control.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection, bool>> EnabledStateChanged;

        /// <summary>
        /// Sends notification that the specified <see cref="ProxyConnection"/> is about to be removed.
        /// </summary>
        /// <remarks>
        /// This event will be raised any time a proxy connection is about to be removed from the list. The
        /// second argument (i.e., the <see cref="EventArgs{T1,T2}.Argument2"/> boolean) can be set to false
        /// to cancel the item removal, e.g., using a message box to verify item removal.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection, bool>> RemovingItem;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ProxyConnections"/>.
        /// </summary>
        public ProxyConnections()
        {
        }

        /// <summary>
        /// Creates a new <see cref="ProxyConnections"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ProxyConnections(SerializationInfo info, StreamingContext context)
        {
            // Deserialize proxy connection collection
            for (int x = 0; x < info.GetInt32("count"); x++)
            {
                Add((ProxyConnection)info.GetValue("item" + x, typeof(ProxyConnection)));
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination <see cref="StreamingContext"/> for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize proxy connection collection
            info.AddValue("count", Count);

            for (int x = 0; x < Count; x++)
            {
                info.AddValue("item" + x, this[x], typeof(ProxyConnection));
            }
        }

        /// <summary>
        /// Inserts the specified item in the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index where the item is to be inserted.</param>
        /// <param name="connection">The item to insert in the list.</param>
        protected override void InsertItem(int index, ProxyConnection connection)
        {
            ProxyConnectionEditor editorControl = connection.ProxyConnectionEditor;

            // Attach to proxy connection events
            connection.PropertyChanged += item_PropertyChanged;

            // Attach to editor control events
            if ((object)editorControl != null)
            {
                editorControl.GotFocus += editorControl_GotFocus;
                editorControl.ConfigurationChanged += editorControl_ConfigurationChanged;
                editorControl.EnabledStateChanged += editorControl_EnabledStateChanged;
            }

            base.InsertItem(index, connection);
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="System.NotSupportedException">You are removing a newly added item and <see cref="System.ComponentModel.IBindingList.AllowRemove"/> is set to false.</exception>
        protected override void RemoveItem(int index)
        {
            ProxyConnection connection = this[index];

            // Raise event notifying consumers about the item about to be removed - this affords
            // an opportunity to cancel this activity if the deletion was accidental
            if (OnRemovingItem(connection))
            {
                // Detach from editor control events
                ProxyConnectionEditor editorControl = connection.ProxyConnectionEditor;

                if ((object)editorControl != null)
                {
                    editorControl.GotFocus -= editorControl_GotFocus;
                    editorControl.ConfigurationChanged -= editorControl_ConfigurationChanged;
                    editorControl.EnabledStateChanged -= editorControl_EnabledStateChanged;
                }

                // Detach from proxy connection events
                connection.PropertyChanged -= item_PropertyChanged;

                // Remove item from list
                base.RemoveItem(index);
            }
        }

        /// <summary>
        /// Raises the <see cref="GotFocus"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object that has been selected.</param>
        protected virtual void OnGotFocus(ProxyConnection connection)
        {
            if ((object)GotFocus != null)
                GotFocus(this, new EventArgs<ProxyConnection>(connection));
        }

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object that has changed.</param>
        protected virtual void OnConfigurationChanged(ProxyConnection connection)
        {
            if ((object)ConfigurationChanged != null)
                ConfigurationChanged(this, new EventArgs<ProxyConnection>(connection));
        }

        /// <summary>
        /// Raises the <see cref="EnabledStateChanged"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object that has changed its enabled state.</param>
        /// <param name="newState">New enabled state.</param>
        protected virtual void OnEnabledStateChanged(ProxyConnection connection, bool newState)
        {
            if ((object)EnabledStateChanged != null)
                EnabledStateChanged(this, new EventArgs<ProxyConnection, bool>(connection, newState));
        }

        /// <summary>
        /// Raises the <see cref="RemovingItem"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object about to be removed.</param>
        protected virtual bool OnRemovingItem(ProxyConnection connection)
        {
            EventArgs<ProxyConnection, bool> e = new EventArgs<ProxyConnection, bool>(connection, true);

            if ((object)RemovingItem != null)
                RemovingItem(this, e);

            return e.Argument2;
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // When proxy connection instances are added at during start-up, items are deserialized first then assigned
            // an editing control afterwards so we must handle attaching to events when the property has changed. During
            // post-load normal run-time a new connection will be associated with its editing control during creation
            // before the collection has even attached to this event (since item won't be in list yet) and the InsertItem
            // event attachment will be executed.
            if (e.PropertyName == "ProxyConnectionEditor")
            {
                ProxyConnection item = sender as ProxyConnection;

                if ((object)item != null)
                {
                    ProxyConnectionEditor editorControl = item.ProxyConnectionEditor;

                    if ((object)editorControl != null)
                    {
                        editorControl.GotFocus += editorControl_GotFocus;
                        editorControl.ConfigurationChanged += editorControl_ConfigurationChanged;
                        editorControl.EnabledStateChanged += editorControl_EnabledStateChanged;
                    }
                }
            }
        }

        // Bubble up proxy editor control events
        private void editorControl_GotFocus(object sender, EventArgs e)
        {
            ProxyConnectionEditor editorControl = sender as ProxyConnectionEditor;

            if ((object)editorControl != null)
                OnGotFocus(editorControl.ProxyConnection);
        }

        private void editorControl_ConfigurationChanged(object sender, EventArgs e)
        {
            ProxyConnectionEditor editorControl = sender as ProxyConnectionEditor;

            if ((object)editorControl != null)
                OnConfigurationChanged(editorControl.ProxyConnection);
        }

        private void editorControl_EnabledStateChanged(object sender, EventArgs<bool> e)
        {
            ProxyConnectionEditor editorControl = sender as ProxyConnectionEditor;

            if ((object)editorControl != null)
                OnEnabledStateChanged(editorControl.ProxyConnection, e.Argument);
        }

        #endregion

        #region [ Static ]

        // Static Methods

        /// <summary>
        /// Loads proxy connections from a previously saved configuration file.
        /// </summary>
        /// <param name="fileName">Configuration file to load.</param>
        /// <returns>New <see cref="ProxyConnections"/> instance from specified <paramref name="fileName"/>.</returns>
        public static ProxyConnections LoadConfiguration(string fileName)
        {
            ProxyConnections proxyConnections = null;
            FileStream settingsFile = null;

            if ((object)fileName == null)
                fileName = string.Empty;

            if (File.Exists(fileName))
            {
                try
                {
                    settingsFile = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                    SoapFormatter formatter = new SoapFormatter
                    {
                        AssemblyFormat = FormatterAssemblyStyle.Simple,
                        TypeFormat = FormatterTypeStyle.TypesWhenNeeded
                    };

                    proxyConnections = formatter.Deserialize(settingsFile) as ProxyConnections;
                }
                finally
                {
                    if ((object)settingsFile != null)
                        settingsFile.Close();
                }
            }

            // Create an empty proxy connection list if none exists
            if ((object)proxyConnections == null)
                proxyConnections = new ProxyConnections();

            return proxyConnections;
        }

        /// <summary>
        /// Saves proxy connections to a configuration file.
        /// </summary>
        /// <param name="proxyConnections">Existing <see cref="ProxyConnections"/> instance to save.</param>
        /// <param name="fileName">Configuration file name to use when saving.</param>
        public static void SaveConfiguration(ProxyConnections proxyConnections, string fileName)
        {
            FileStream settingsFile = null;

            try
            {
                settingsFile = File.Create(fileName);

                SoapFormatter formatter = new SoapFormatter
                {
                    AssemblyFormat = FormatterAssemblyStyle.Simple,
                    TypeFormat = FormatterTypeStyle.TypesWhenNeeded
                };

                formatter.Serialize(settingsFile, proxyConnections);

                // Parse updated connection strings in proxy connections
                foreach (ProxyConnection proxyConnection in proxyConnections)
                {
                    proxyConnection.ParseConnectionString(proxyConnection.ConnectionString);
                }
            }
            finally
            {
                if ((object)settingsFile != null)
                    settingsFile.Close();
            }
        }

        #endregion
    }
}
