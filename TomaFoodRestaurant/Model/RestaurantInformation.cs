using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public class RestaurantInformation
    {

      public int Id { set; get; }
      public int RestaurantCategoryId { set; get;}
      public string ThemeName { set; get;}
      public int Owner { set; get; }
      public string RestaurantType { set; get; }
      public string RestaurantName{set;get;}
      public string RestaurantSummary { set; get; }
      public string House { set; get;}
      public string Address { set; get; }
      public string Postcode { set; get; }
      public string Phone { set; get; }
      public string Email { set; get; }
      public string Website { set; get; }
      public string Status { set; get; }
      public double Vat { set; get; }
      public int MenuMaxRow { set; get; }
      public Int64 Expire { set; get; }
      public int PackageMaxRow { set; get; }
      public string VatRegNo { set; get; }
      public string DeliveryFrom { set; get; }
      public string DeliveryTo { set; get; }
      public string CollectionFrom { set; get; }
      public string CollectionTo { set; get; }
      public String DeliveryTime { set; get; }
      public string ThankYouMsg { set; get; }
      public string DiscountType { set; get; }
      public double DeliveryCharge { set; get; }
      public string DescriptionText { set; get; }
      public double CardFee { set; get; }
      public double CardMinOrder { set; get; }
      public int IsBusy { set; get; }
      public String PaymentOption{ set; get; }
	  public Double MinOrderDelivery{ set; get; }
      public String ServiceOption{ set; get; }
	  public String DefaultOrderType{ set; get; }
	  public String InLogo{ set; get; }
      public String RecieptOption{ set; get; }
	  public Int32 MenuSeparation{ set; get; }
	  public String CollectionTime{ set; get; }
	  public Int64 ServerCallButton{ set; get; }
	  public Int64 PreOrder{ set; get; }
	  public Int64 ShowOptionInline{ set; get; }
	  public String ExcludeDiscount{ set; get; }
	  public String RecieptFont{ set; get; }
	  public Int64 ConfirmPayment{ set; get; }
	  public Int32 UpdateRequired{ set; get; }
	  public String CurrentVersion{ set; get; }
	  public Int64 ShowOrderNumber{ set; get; }
	  public String DefaultOrderStatus{ set; get; }
	  public Int32 PrintCopy{ set; get; }
      public Int32 DelPrintCopy { set; get; }
      public Int32 DineInPrintCopy { set; get; }
	  public Int64 UseJava{ set; get; }
	  public String LocalIp{ set; get; }
	  public Int32 RecieptMinHeight{ set; get; }
      public String SyncType { set; get; }
      public Int32 RequireServed { set; get; }
      public Int32 IsServiceCharge { set; get; }

      public string City { get; set; }

      public string Country { get; set; }

      public string Fax { get; set; }

      public string Logo { get; set; }

      public long MenuDrag { get; set; }

      public double MinOrder { get; set; }

      public long IsHalal { get; set; }

      public int ReportClosingHour { get; set; }

      public int ReportClosingMin { get; set; }

      public string Url { get; set; }
      public int MultiplePart { set; get; }

      public Int32 IsSyncOrder { get; set; }

      public Int32 IsSyncCustomer { get; set; }
    
  }
}
