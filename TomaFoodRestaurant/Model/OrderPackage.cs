using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public class OrderPackage
    {
        public Int32 Id{ set; get; }
        public Int32 OrderId { set; get; }
        public Int32 PreOrderId { set; get; }
        public Int32 PackageId{ set; get; }
	 	public String Name{ set; get; }
	 	public Int32 Quantity{ set; get; }
	 	public Double Price{ set; get; }
	 	public Double Extra_price{ set; get; }
        public Int32 optionIndex { set; get; }
        public List<PackageItem> PackageItem { get; set; }
    }
}
