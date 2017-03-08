//******************************************************************************************************
//  ToolTipEx.cs - Gbtc
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
//  09/13/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using GSF;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StreamSplitter
{
    /// <summary>
    /// Represents a tool-tip provider with the ability to change the drawing font.
    /// </summary>
    public class ToolTipEx : ToolTip
    {
        #region [ Members ]

        // Fields
        private Font m_font;
        private bool m_disposed;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ToolTipEx"/>.
        /// </summary>
        public ToolTipEx()
        {
            Draw += OnDraw;
            Popup += OnPopup;
        }

        /// <summary>
        /// Creates a new <see cref="ToolTipEx"/>.
        /// </summary>
        public ToolTipEx(IContainer container)
            : this()
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets font to use with this tool tip.
        /// </summary>
        [Description("Defines font to use with this tool tip.")]
        public Font Font
        {
            get
            {
                return m_font;
            }
            set
            {
                m_font = value;
                OwnerDraw = ((object)m_font != null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ToolTipEx"/> has been disposed of.
        /// </summary>
        public bool IsDisposed => m_disposed;

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ToolTipEx"/> object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                try
                {
                    if (disposing)
                    {
                        m_font?.Dispose();
                    }
                }
                finally
                {
                    m_disposed = true;          // Prevent duplicate dispose.
                    base.Dispose(disposing);    // Call base class Dispose().
                }
            }
        }

        // Handle custom drawing of tool-tip when a new font is provided
        private void OnPopup(object sender, PopupEventArgs e)
        {
            if (OwnerDraw)
            {
                e.ToolTipSize = TextRenderer.MeasureText(GetToolTip(e.AssociatedControl), m_font);
                e.ToolTipSize = new Size(e.ToolTipSize.Width + 10, e.ToolTipSize.Height + 5);
            }
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (OwnerDraw)
            {
                DrawToolTipEventArgs newArgs = new DrawToolTipEventArgs(e.Graphics,
                    e.AssociatedWindow, e.AssociatedControl, e.Bounds, e.ToolTipText,
                    BackColor, ForeColor, m_font);

                newArgs.DrawBackground();
                newArgs.DrawBorder();
                newArgs.DrawText(TextFormatFlags.TextBoxControl);
            }
        }

        #endregion

        #region [ Static ]

        // Static Methods

        /// <summary>
        /// Word wrap status lines at 80 characters.
        /// </summary>
        /// <param name="status">Status to be wrapped.</param>
        /// <returns>Wrapped status.</returns>
        public static string WordWrapStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                return string.Empty;

            string[] lines = status.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] segments = lines[i].GetSegments(80);
                lines[i] = string.Join("\r\n", segments);
            }

            return string.Join("\r\n", lines);
        }

        #endregion
    }
}
