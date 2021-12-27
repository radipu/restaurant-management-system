using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{

           class RoundButton : Button {
               Pen _pen;
               Color _color=new Color();
               SolidBrush _brush;
               SolidBrush _brushInside;
             public   RoundButton()
             {
                  _pen = new Pen(_color);
                  _brush = new SolidBrush(Color.FromKnownColor (KnownColor.Control));
                  _brushInside = new SolidBrush(_color);
            }
           
          
                protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new Region(path);
            base.OnPaint(pe);
        }

               


            
}

    
}
