using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class OrderItem
    {       
        public Int32 Id{ set; get; }
		public Int32 OrderId { set; get; }
		public Int32 PreOrderId { set; get; }
		public Int32 RecipeId{ set; get; }
	 	public Int32 PackageId{ set; get; }
	 	public String Name{ set; get; }
	 	public Int32 Quantity{ set; get; }
	 	public Double Price{ set; get; }
	 	public Double ExtraPrice{ set; get; }
	 	public Int32 SentToKitchen{ set; get; }
	 	public Int32 KitchenProcessing{ set; get; }
	 	public Int32 KitchenDone{ set; get; }
	 	public DateTime LastModifyTime { set; get; }
	 	public String Options{ set; get; }
        public String MinusOptions { set; get; }
        public String MultipleMenu { set; get; }
        public int orderPackageId { get; set; }
        public string Options_ids { get; set; }
        public List<PackageItemNew> PackageItems { get; set; }
        public string Group { get; set; }
    }
}
