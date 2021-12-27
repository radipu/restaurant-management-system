using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class RestaurantOrder
    {
        public bool isFinalize { get; set; }
        public bool isKitchenPrint { get; set; }
       
        public Int32 Id{ set; get; }
	 	public Int32 UserId{ set; get; }
	 	public Int32 CustomerId{ set; get; }
	 	public Int32 RestaurantId{ set; get; }
	 	public DateTime OrderTime { set; get; }
	 	public DateTime  DeliveryTime{ set; get; }
	 	public DateTime  PaymentDate {  set;get; }
	 	public String Status{ set; get; }
	 	public String PaymentMethod{ set; get; }
	 	public String PaymentModule{ set; get; }
	 	public String DeliveryAddress{ set; get; }
	 	public String Comments{ set; get; }
	 	public Double DeliveryCost{ set; get; }
	 	public Double Vat{ set; get; }
	 	public Double TotalCost{ set; get; }
	 	public Int32 Person{ set; get; }
	 	public Int32 OrderTable{ set; get; }
	 	public Double Discount{ set; get; }
	 	public String OrderType{ set; get; }
	 	public String OrderStatus{ set; get; }
	 	public Int64 Receipt{ set; get; }
	 	public String Comment{ set; get; }
	 	public String CartInformation{ set; get; }
	 	public String CustomerName{ set; get; }
	 	public String Coupon{ set; get; }
        public string SpecialOfferItem { set; get; }
	 	public Int64 OnlineOrder{ set; get; }
	 	public String OnlineOrderStatus{ set; get; }
	 	public String CustomerEmail{ set; get; }
	 	public Double CardFee{ set; get; }
	 	public Int64 OnlineOrderId{ set; get; }
	 	public Double CashAmount{ set; get; }
	 	public Double CardAmount{ set; get; }
	 	public Int32 DriverId{ set; get; }
	 	public Int32 OrderNo{ set; get; }
        public DateTime UpdateTime { set; get; }
        public string ServedBy { set; get; }
        public Int32 IsSync { set; get; }
        public Double ServiceCharge { set; get; }
        public string PostCode { get; set; }
        public List<OrderItem> OrderItem { get; set; }
        public double DiscountAmount { get; set; }
        public string Position { get; set; }
        public string FormatedAddtress { get; set; }
    }
}
