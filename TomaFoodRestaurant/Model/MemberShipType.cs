using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class MemberShipType
    {

       public Int32  Id{ set; get; }
       public String TypeName{ set; get; }
       public Int32 RestaurantId { set; get; } 
	 
    }
}
