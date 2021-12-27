using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public class RestaurantTable
    {
        public Int32 Id { set; get; }
        public Int32 RestaurantId { set; get; }
        public String Name { set; get; }
        public Int32 Person { set; get; }
        public String TableShape { set; get; }
        public Int32 SortOrder { set; get; }
        public String CurrentStatus { set; get; }
        public DateTime UpdateTime { set; get; }
        public bool IsBill { set; get; }
        public int MergeStatus { set; get; }
    }
}
