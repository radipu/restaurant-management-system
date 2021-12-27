using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public class EmailModule
    {
        public Int32 Id { set; get; }
        public Int32 RestaurantId { set; get; }
        public String Slug { set; get; }
        public String Name { set; get; }
        public String ApiUrl { set; get; }
        public String ApiKey { set; get; }
        public String ApiSecret { set; get; }
        public String Status { set; get; }
    }
}
