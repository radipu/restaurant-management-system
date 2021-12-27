using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;

namespace TomaFoodRestaurant.Model
{
 public   class RountTileButton:TileItem

    {
                Pen _pen;
               Color _color;
               SolidBrush _brush;
               SolidBrush _brushInside;
               public RountTileButton(Color color)
             {
                   _color = color;
                   _pen = new Pen(_color);
                  _brush = new SolidBrush(Color.FromKnownColor (KnownColor.Control));
                  _brushInside = new SolidBrush(_color);
            }
           
           
          
               
    }
}
