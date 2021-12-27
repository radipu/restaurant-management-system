using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
   public class ReceipeItemChangePrice
    {
       public int restaurant_id { get; set; }
       public Int32 recipe_id { get; set; }
       public string recipe_name { get; set; }
       public double out_price { get; set; }
       public double in_price { get; set; }
    

    }
}
