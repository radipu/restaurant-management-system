using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class KitchenSection
    {

        public Int32 Id { set; get; }
        public Int32 RestaurantId { set; get; }
	    public String Name{ set; get; } 
    }
}
