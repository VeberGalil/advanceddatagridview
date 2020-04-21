#region License
// Advanced DataGridView
//
// Copyright (c), 2020 Vladimir Bershadsky <vladimir@galileng.com>
// Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal class SortFilterMenuStripResizeEventArgs : EventArgs
    {
        public int WidthChange { get; private set; }
        public int HeightChange { get; private set; }

        public SortFilterMenuStripResizeEventArgs(int dX, int dY)
        {
            this.WidthChange = dX;
            this.HeightChange = dY;
        }
    }

    internal class SortFilterMenuStripResizer : ToolStripControlHost
    {
        #region // Constructor
        public SortFilterMenuStripResizer() : base(new Control())
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Name = "resizeBoxControlHost";
            Control.Cursor = Cursors.SizeNWSE;
            Padding = new Padding(0);
            Size = new Size(10, 10);
        }

        #endregion

        #region // Public event
        
        public event EventHandler<SortFilterMenuStripResizeEventArgs> ResizeMenu;

        #endregion

        #region // Public methods

        public void CancelResize()
        {
            ResizeClean();
            //       _resizeEndPoint = new Point(-1, -1);
        }

        #endregion 

        #region // Internal events
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.RightToLeft == RightToLeft.Yes)
            {
                e.Graphics.DrawImage(Properties.Resources.MenuStrip_ResizeGrip_RTL, 0, 0);
            }
            else
            {
                e.Graphics.DrawImage(Properties.Resources.MenuStrip_ResizeGrip, 0, 0);
            }
        }

        protected override void OnOwnerChanged(EventArgs e)
        {
            base.OnOwnerChanged(e);
            // Change icon when owning SortFilterMenuStrip RTL changes
            ((ContextMenuStrip)Owner).RightToLeftChanged += (s, ea) =>
            {
                this.RightToLeft = Owner.RightToLeft;
                if (Owner.RightToLeft == RightToLeft.Yes)
                {
                    Control.Cursor = Cursors.SizeNESW;
                    Margin = new Padding(Owner.Width - 46, 1, 0, 0);

                }
                else
                {
                    Control.Cursor = Cursors.SizeNWSE;
                    Margin = new Padding(Owner.Width - 45, 0, 0, 0);
                }
            };

           Margin = new Padding(Owner.Width - 45, 0, 0, 0);
        }

        #endregion

        #region // Resize mechanism

        private Point _resizeStartPoint = Point.Empty;
        private Point _resizeEndPoint = new Point(-1, -1);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            // 
            if (e.Button == MouseButtons.Left)
            {
                // Set start point
                Point startPoint = new Point((RightToLeft == RightToLeft.Yes) ? 1 + Owner.Width : 1, 1);
                _resizeStartPoint = Owner.PointToScreen(startPoint);
                // Draw initial frame (should we?)
                ResizeClean();
            }

        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);
            //
            if (Owner.Visible)
            {
                if (mea.Button == MouseButtons.Left)
                {
                    // Remove previous frame
                    ResizeClean();
                    // Current cursor position
                    int x = mea.X;
                    int y = mea.Y;
                    // Don't let frame to become smaller (narrower/shorter) than minimal size
                    // (only horisontal position is affected by RTL)
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        x = Math.Min(x, Owner.Width - Owner.MinimumSize.Width - 1);
                    }
                    else
                    {
                        x += Owner.Width - Width;
                        x = Math.Max(x, Owner.MinimumSize.Width - 1);
                    }
                    y += Owner.Height - Height;
                    y = Math.Max(y, Owner.MinimumSize.Height - 1);
                    // Draw resize frame
                    _resizeEndPoint = Owner.PointToScreen(new Point(x, y));
                    DrawResizeFrame();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            if (_resizeEndPoint.X != -1)
            {
                // Clear resize frame
                ResizeClean();
                // If menu is visible, do resize
                if (Visible)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        // New bounds
                        int newWidth;
                        if (RightToLeft == RightToLeft.Yes)
                        {
                            newWidth = Math.Max(Owner.Width - e.X, Owner.MinimumSize.Width);
                        }
                        else
                        {
                            newWidth = Math.Max(e.X + Owner.Width - Width, Owner.MinimumSize.Width);
                        }
                        int newHeight = Math.Max(e.Y + Owner.Height - Height, Owner.MinimumSize.Height);
                        // Raise event
                        ResizeMenu(this, new SortFilterMenuStripResizeEventArgs(newWidth - Owner.Width, newHeight - Owner.Height));
                        // Reposition resizer icon
                        if (Owner.RightToLeft == RightToLeft.Yes)
                        {
                            Margin = new Padding(Owner.Width - 46, 1, 0, 0);
                        }
                        else
                        {
                            Margin = new Padding(Owner.Width - 45, 0, 0, 0);
                        }
                    }
                }
            }
        }

        private void DrawResizeFrame()
        {
            Rectangle rc = new Rectangle()
            {
                X = Math.Min(_resizeStartPoint.X, _resizeEndPoint.X),
                Y = Math.Min(_resizeStartPoint.Y, _resizeEndPoint.Y),
                Width = Math.Abs(_resizeStartPoint.X - _resizeEndPoint.X),
                Height = Math.Abs(_resizeStartPoint.Y - _resizeEndPoint.Y)
            };
            ControlPaint.DrawReversibleFrame(rc, Color.Black, FrameStyle.Dashed);
        }

        private void ResizeClean()
        {
            if (_resizeEndPoint.X != -1)
            {   // i.e. if _resizeEndPoint is already set by mouse movement
                DrawResizeFrame();
                // Reset end point so the same frame will not be drawn again
                _resizeEndPoint.X = -1;
            }
        }

        #endregion
    }
}
