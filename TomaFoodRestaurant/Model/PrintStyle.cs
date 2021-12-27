using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class PrintStyle
    {
        public int PrintStyleId { set; get; }
        public string Header { set; get; }
        public string Body { set; get; }
        public string Footer { set; get; }
        public Byte[] Logo = new Byte[8000];
        public Image LogoImage = new Bitmap(150, 150);

    }
}
