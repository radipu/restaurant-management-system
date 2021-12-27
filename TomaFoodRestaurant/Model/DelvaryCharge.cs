using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class DelvaryCharge
    {
        public int Id { set; get; }
        public Int32 RestaurantId{ set; get; }
       public Int32 from { set; get; }
       public Int32 to { set; get; }
        public Double amount { set; get; }
	 
    }
}
