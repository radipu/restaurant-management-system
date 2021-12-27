using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class OrderDetails
    {

       public int OrderDetailsId { set; get; }
       public int ItemId { set; get; }
       public int ItemQty { set; get; }
       public string ItemName{set;get;}
       public double UnitPrice { set; get; }
       public double TotalPrice { set; get; }

    }
}
