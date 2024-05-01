//******************************************************************************************************
//  ProxyConnections.cs - Gbtc
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
//  09/02/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading;
using GSF;
using GSF.Threading;

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
    public class ProxyConnectionCollection : BindingList<ProxyConnection>, ISerializable
    {
        #region [ Members ]

        private class LocalPropertyDescriptor : PropertyDescriptor
        {
            public LocalPropertyDescriptor(string name) : base(name, null)
            {
            }

            public override bool CanResetValue(object component) => throw new NotImplementedException();

            public override object GetValue(object component) => throw new NotImplementedException();

            public override void ResetValue(object component) => throw new NotImplementedException();

            public override void SetValue(object component, object value) => throw new NotImplementedException();

            public override bool ShouldSerializeValue(object component) => throw new NotImplementedException();

            public override Type ComponentType { get; }
            public override bool IsReadOnly { get; }
            public override Type PropertyType { get; }
        }

        // Events

        /// <summary>
        /// Sends notification that the <see cref="ProxyConnection"/> has changed and should be saved.
        /// </summary>
        /// <remarks>
        /// This event will only be raised if the proxy connection is associated with an editor control.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection>> ConfigurationChanged;

        /// <summary>
        /// Sends notification that the <see cref="ProxyConnection"/> has requested that changes should be applied.
        /// </summary>
        /// <remarks>
        /// This event will only be raised if the proxy connection is associated with an editor control.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection>> ApplyChanges;

        /// <summary>
        /// Sends notification that the <see cref="ProxyConnection"/> enabled state has changed.
        /// </summary>
        /// <remarks>
        /// This event will only be raised if the proxy connection is associated with an editor control.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection, bool>> EnabledStateChanged;

        /// <summary>
        /// Sends notification that a search operation has completed.
        /// </summary>
        public event EventHandler SearchOperationCompleted;

        /// <summary>
        /// Sends notification that the specified <see cref="ProxyConnection"/> is about to be removed.
        /// </summary>
        /// <remarks>
        /// This event will be raised any time a proxy connection is about to be removed from the list. The
        /// second argument (i.e., the <see cref="EventArgs{T1,T2}.Argument2"/> boolean) can be set to false
        /// to cancel the item removal, e.g., using a message box to verify item removal.
        /// </remarks>
        public event EventHandler<EventArgs<ProxyConnection, bool>> RemovingItem;

        private List<ProxyConnection> m_visibleConnections;
        private ShortSynchronizedOperation m_searchOperation;
        private string m_searchText;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ProxyConnectionCollection"/>.
        /// </summary>
        public ProxyConnectionCollection()
        {
            AllowEdit = true;
            AllowNew = true;
            AllowRemove = true;
        }

        /// <summary>
        /// Creates a new <see cref="ProxyConnectionCollection"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ProxyConnectionCollection(SerializationInfo info, StreamingContext context)
        {
            // Deserialize proxy connection collection
            for (int x = 0; x < info.GetInt32("count"); x++)
                Add((ProxyConnection)info.GetValue("item" + x, typeof(ProxyConnection)));
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets <see cref="ProxyConnection"/> based on <paramref name="id"/> index.
        /// </summary>
        /// <param name="id">ID of <see cref="ProxyConnection"/> to get or set.</param>
        /// <returns>The <see cref="ProxyConnection"/> based on <paramref name="id"/> index.</returns>
        public ProxyConnection this[Guid id]
        {
            get
            {
                return this.FirstOrDefault(connection => connection.ID == id);
            }
            set
            {
                ProxyConnection connection = this[id];

                if (connection is null)
                {
                    Add(value);
                }
                else
                {
                    int index = IndexOf(connection);

                    if (index > -1)
                        this[index] = value;
                    else
                        Add(value);
                }
            }
        }

        /// <summary>
        /// Sets search text.
        /// </summary>
        public string SearchText
        {
            set
            {
                m_searchText = value.Trim();
                Search();
            }
        }

        private void Search() => (m_searchOperation ??= new ShortSynchronizedOperation(SearchConnections)).RunOnceAsync();

        private void SearchConnections()
        {
            string value = m_searchText;

            if (string.IsNullOrEmpty(value))
            {
                Interlocked.Exchange(ref m_visibleConnections, null);
            }
            else
            {
                HashSet<ProxyConnection> visibleConnections = new();
                string[] searchValues = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (ProxyConnection connection in this)
                {
                    if (searchValues.Any(searchValue => connection.ConnectionString.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0))
                        visibleConnections.Add(connection);
                }

                Interlocked.Exchange(ref m_visibleConnections, visibleConnections.ToList());
            }

            foreach (ProxyConnection connection in this)
                connection.Visible = m_visibleConnections?.Contains(connection) ?? true;

            SearchOperationCompleted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets the reduced search list.
        /// </summary>
        public BindingList<ProxyConnection> SearchList
        {
            get
            {
                List<ProxyConnection> visibleConnections = m_visibleConnections;

                return visibleConnections is null || visibleConnections.Count == 0 ? null : 
                    new BindingList<ProxyConnection>(visibleConnections);
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Sorts the items in the list by name in the specified <paramref name="direction"/>.
        /// </summary>
        /// <param name="fieldName">Field name to sort on.</param>
        /// <param name="direction">Sort direction.</param>
        public void Sort(string fieldName, ListSortDirection direction)
        {
            ApplySortCore(new LocalPropertyDescriptor(fieldName), direction);
        }

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
                info.AddValue("item" + x, this[x], typeof(ProxyConnection));
        }

        /// <inheritdoc />
        protected override void RemoveItem(int index)
        {
            ProxyConnection connection = this[index];

            // Raise event notifying consumers about the item about to be removed - this affords
            // an opportunity to cancel this activity if the deletion was accidental
            if (OnRemovingItem(connection))
                base.RemoveItem(index);
        }

        /// <inheritdoc />
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (Items is not List<ProxyConnection> items)
                return;

            if (m_visibleConnections is not null)
                items = m_visibleConnections;

            switch (prop.Name)
            {
                case "Name":
                    if (direction == ListSortDirection.Ascending)
                        items.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
                    else
                        items.Sort((a, b) => string.Compare(b.Name, a.Name, StringComparison.OrdinalIgnoreCase));
                    break;
                case "ConnectionState":
                    if (direction == ListSortDirection.Ascending)
                        items.Sort((a, b) => a.ConnectionState.CompareTo(b.ConnectionState));
                    else
                        items.Sort((a, b) => b.ConnectionState.CompareTo(a.ConnectionState));
                    break;
                case "Enabled":
                    if (direction == ListSortDirection.Ascending)
                        items.Sort((a, b) => a.Enabled.CompareTo(b.Enabled));
                    else
                        items.Sort((a, b) => b.Enabled.CompareTo(a.Enabled));
                    break;
                default:
                    return;
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <inheritdoc />
        protected override void RemoveSortCore()
        {
            if (Items is not List<ProxyConnection> items)
                return;

            if (m_visibleConnections is not null)
                items = m_visibleConnections;

            items.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <inheritdoc />
        protected override bool SupportsSortingCore => true;

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object that has changed.</param>
        protected virtual void OnConfigurationChanged(ProxyConnection connection)
        {
            ConfigurationChanged?.Invoke(this, new EventArgs<ProxyConnection>(connection));
        }

        /// <summary>
        /// Raises the <see cref="ApplyChanges"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object that has requested that changes be applied.</param>
        protected virtual void OnApplyChanges(ProxyConnection connection)
        {
            ApplyChanges?.Invoke(this, new EventArgs<ProxyConnection>(connection));
        }

        /// <summary>
        /// Raises the <see cref="EnabledStateChanged"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object that has changed its enabled state.</param>
        /// <param name="newState">New enabled state.</param>
        protected virtual void OnEnabledStateChanged(ProxyConnection connection, bool newState)
        {
            EnabledStateChanged?.Invoke(this, new EventArgs<ProxyConnection, bool>(connection, newState));
        }

        /// <summary>
        /// Raises the <see cref="RemovingItem"/> event.
        /// </summary>
        /// <param name="connection"><see cref="ProxyConnection"/> object about to be removed.</param>
        protected virtual bool OnRemovingItem(ProxyConnection connection)
        {
            EventArgs<ProxyConnection, bool> e = new(connection, m_visibleConnections is null);

            RemovingItem?.Invoke(this, e);

            return e.Argument2;
        }

        // Bubble up proxy editor control events
        internal void EditorControl_ConfigurationChanged(object sender, EventArgs e)
        {
            if (sender is ProxyConnectionEditor editorControl)
                OnConfigurationChanged(editorControl.ProxyConnection);
        }

        internal void EditorControl_ApplyChanges(object sender, EventArgs e)
        {
            if (sender is ProxyConnectionEditor editorControl)
                OnApplyChanges(editorControl.ProxyConnection);
        }

        internal void EditorControl_EnabledStateChanged(object sender, EventArgs<bool> e)
        {
            if (sender is ProxyConnectionEditor editorControl)
                OnEnabledStateChanged(editorControl.ProxyConnection, e.Argument);
        }

        #endregion

        #region [ Static ]

        // Static Methods

        /// <summary>
        /// Serializes proxy connections to a stream.
        /// </summary>
        /// <param name="proxyConnections">Existing <see cref="ProxyConnectionCollection"/> instance to save.</param>
        /// <param name="destinationStream">Destination stream.</param>
        public static void SerializeConfiguration(ProxyConnectionCollection proxyConnections, Stream destinationStream)
        {
            SoapFormatter formatter = new()
            {
                AssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeFormat = FormatterTypeStyle.TypesWhenNeeded
            };

            formatter.Serialize(destinationStream, proxyConnections);
        }

        /// <summary>
        /// Deserializes proxy connections from a stream.
        /// </summary>
        /// <param name="sourceStream">Source stream.</param>
        /// <param name="invoke">Defines any invoke function that may be needed to update UI controls, if applicable.</param>
        /// <param name="suspend">Defines suspend UI function.</param>
        /// <param name="resume">Defines resume UI function.</param>
        /// <returns>New <see cref="ProxyConnectionCollection"/> instance from specified <paramref name="sourceStream"/>.</returns>
        public static ProxyConnectionCollection DeserializeConfiguration(Stream sourceStream, Func<Delegate, object> invoke = null, Action suspend = null, Action resume = null)
        {
            SoapFormatter formatter = new()
            {
                AssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeFormat = FormatterTypeStyle.TypesWhenNeeded
            };

            ProxyConnectionCollection proxyConnections = formatter.Deserialize(sourceStream) as ProxyConnectionCollection;

            if (proxyConnections is not null)
            {
                // Parse updated connection strings in proxy connections
                foreach (ProxyConnection proxyConnection in proxyConnections)
                    proxyConnection.ConnectionString = proxyConnection.ParseConnectionString(proxyConnection.ConnectionString);
            }

            return proxyConnections;
        }

        /// <summary>
        /// Serializes proxy connections to a byte-array.
        /// </summary>
        /// <param name="proxyConnections">Existing <see cref="ProxyConnectionCollection"/> instance to save.</param>
        /// <returns>Buffer of serializes proxy connections.</returns>
        public static byte[] SerializeConfiguration(ProxyConnectionCollection proxyConnections)
        {
            using MemoryStream destinationStream = new();

            SerializeConfiguration(proxyConnections, destinationStream);
            return destinationStream.ToArray();
        }

        /// <summary>
        /// Deserializes proxy connections from a byte-array.
        /// </summary>
        /// <param name="buffer">Byte-array of serialized proxy connections.</param>
        /// <param name="invoke">Defines any invoke function that may be needed to update UI controls, if applicable.</param>
        /// <param name="suspend">Defines suspend UI function.</param>
        /// <param name="resume">Defines resume UI function.</param>
        /// <returns>New <see cref="ProxyConnectionCollection"/> instance from specified <paramref name="buffer"/>.</returns>
        public static ProxyConnectionCollection DeserializeConfiguration(byte[] buffer, Func<Delegate, object> invoke = null, Action suspend = null, Action resume = null)
        {
            if (buffer is null)
                return null;

            using MemoryStream sourceStream = new(buffer);

            return DeserializeConfiguration(sourceStream, invoke, suspend, resume);
        }

        /// <summary>
        /// Saves proxy connections to a configuration file.
        /// </summary>
        /// <param name="proxyConnections">Existing <see cref="ProxyConnectionCollection"/> instance to save.</param>
        /// <param name="fileName">Configuration file name to use when saving.</param>
        public static void SaveConfiguration(ProxyConnectionCollection proxyConnections, string fileName)
        {
            using FileStream settingsFile = File.Create(fileName);

            SerializeConfiguration(proxyConnections, settingsFile);
        }

        /// <summary>
        /// Loads proxy connections from a previously saved configuration file.
        /// </summary>
        /// <param name="fileName">Configuration file to load.</param>
        /// <returns>New <see cref="ProxyConnectionCollection"/> instance from specified <paramref name="fileName"/>.</returns>
        public static ProxyConnectionCollection LoadConfiguration(string fileName)
        {
            ProxyConnectionCollection proxyConnections = null;

            fileName ??= string.Empty;

            if (File.Exists(fileName))
            {
                using FileStream settingsFile = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                proxyConnections = DeserializeConfiguration(settingsFile);
            }

            if (proxyConnections is not null)
                return proxyConnections;
            
            // Create an empty proxy connection list if none exists
            return new ProxyConnectionCollection();
        }

        #endregion
    }
}
