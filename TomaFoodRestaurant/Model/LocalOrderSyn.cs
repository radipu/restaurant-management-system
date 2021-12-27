using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
    public class LocalOrderSyn
    {
        public DataTable RestaurantOrder { get; set; }
        public DataTable RestaurantUsers { get; set; }
        public DataTable MemberShips { get; set; }
        public DataTable OrderItems { get; set; }
        public DataTable OrderPackages { get; set; }
        public DataTable CustomerOrderItems { get; set; }
        public int restaurantId { get; set; }



    }
}
