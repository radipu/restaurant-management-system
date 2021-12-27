using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class CustomerRecentItemMD
    {
        public int customer_id { set; get; }
        public int recipe_id { set; get; }
        public int package_id { set; get; }
        public string time_added { set; get; }
    }
}
