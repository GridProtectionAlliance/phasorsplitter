//******************************************************************************************************
//  TransparentPanel.cs - Gbtc
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
//  09/11/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StreamSplitter
{
    /// <summary>
    /// Represents a transparent panel.
    /// </summary>
    public class TransparentPanel : Panel
    {
        #region [ Members ]

        // Fields
        private SolidBrush m_brush;
        private bool m_disposed;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="TransparentPanel"/>.
        /// </summary>
        public TransparentPanel()
        {
            m_brush = new SolidBrush(Color.FromArgb(120, Color.LightGray));
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="TransparentPanel"/> object and optionally releases the managed resources.
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
                        if ((object)m_brush != null)
                        {
                            m_brush.Dispose();
                            m_brush = null;
                        }
                    }
                }
                finally
                {
                    m_disposed = true;          // Prevent duplicate dispose.
                    base.Dispose(disposing);    // Call base class Dispose().
                }
            }
        }
        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        /// <returns>
        /// A <see cref="CreateParams"/> that contains the required creation parameters when the handle to the control is created.
        /// </returns>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT 
                return cp;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //using (HatchBrush brush = new HatchBrush(HatchStyle.DashedDownwardDiagonal, SystemColors.ControlDark, Color.Transparent))
            //    e.Graphics.FillRectangle(brush, e.ClipRectangle);

            //e.Graphics.FillRectangle(m_brush, 0, 0, Width, Height);

            if ((object)m_brush != null)
            {
                e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                e.Graphics.FillRectangle(m_brush, e.ClipRectangle);
            }
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Suppress normal background painting
        }

        #endregion
    }
}
