//******************************************************************************************************
//  ProxyConnectionEqualityComparer.cs - Gbtc
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

using System.Collections.Generic;

namespace StreamSplitter
{
    /// <summary>
    /// Represents an <see cref="IEqualityComparer{T}"/> for comparing proxy connections using its ID.
    /// </summary>
    public class ProxyConnectionIDComparer : IEqualityComparer<ProxyConnection>
    {
        #region [ Methods ]

        /// <summary>
        /// Determines whether the specified <see cref="ProxyConnection"/>objects are equal based on their ID.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">The first <see cref="ProxyConnection"/> to compare.</param>
        /// <param name="y">The second <see cref="ProxyConnection"/> to compare.</param>
        public bool Equals(ProxyConnection x, ProxyConnection y)
        {
            return x.ID.Equals(y.ID);
        }

        /// <summary>
        /// Returns a hash code for the specified <see cref="ProxyConnection"/>.
        /// </summary>
        /// <returns>
        /// A hash code for the <see cref="ProxyConnection"/>.
        /// </returns>
        /// <param name="obj">The <see cref="ProxyConnection"/> for which a hash code is to be returned.</param>
        public int GetHashCode(ProxyConnection obj)
        {
            return obj.ID.GetHashCode();
        }

        #endregion

        #region [ Static ]

        // Static Fields
        private static ProxyConnectionIDComparer s_defaultComparer;

        // Static Methods

        /// <summary>
        /// Gets the default instance of the <see cref="ProxyConnectionIDComparer"/>.
        /// </summary>
        public static ProxyConnectionIDComparer Default
        {
            get
            {
                if ((object)s_defaultComparer == null)
                    s_defaultComparer = new ProxyConnectionIDComparer();

                return s_defaultComparer;
            }
        }

        #endregion
    }
}
