using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    public class CustomFlowLayoutPanel : FlowLayoutPanel
    {
        public CustomFlowLayoutPanel(): base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = false;
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Invalidate();

            base.OnScroll(se);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_CLIPCHILDREN
                return cp;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                         Color.Black,1, ButtonBorderStyle.None,
                                         Color.Black, 1, ButtonBorderStyle.None,
                                         Color.Black, 1, ButtonBorderStyle.None,
                                         Color.CornflowerBlue, 1, ButtonBorderStyle.Inset);
        }
    }
}
