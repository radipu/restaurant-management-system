using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
   public  class AttributeButton:Button
    {
       public int AttributeId { set; get; }
       public int RestaurantId { set; get; }
       public string AttributeName { set; get; }
       public double Price { set; get; }
       public string AttributeUnit { set; get; }
       public int SortOrder { set; get; }
       public double Discount { set; get; }
       public string AttributeColor { set; get;}

    }
}
