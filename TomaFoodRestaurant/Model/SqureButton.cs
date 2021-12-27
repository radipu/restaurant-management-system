using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    class SqureButton:Button
    {

      protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
      {
          GraphicsPath path = new GraphicsPath();
          Rectangle pathRect = new Rectangle(0, 0, 200, 200);
          path.AddRectangle(pathRect);
          this.Region = new Region(path);
          base.OnPaint(pe);
      }
         
        

    }
}
