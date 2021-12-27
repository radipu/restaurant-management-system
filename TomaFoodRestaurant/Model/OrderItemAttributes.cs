using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class OrderItemAttributes
    {
       public Int32 Id{ set; get; }
	 	public Int32 OrderItemId{ set; get; }
	 	public Int32 AttributeId{ set; get; }
	 	public String Value{ set; get; }
	 	public Double Price{ set; get; }

    }
}
