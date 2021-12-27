using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class GeneralInformation
    {
       public int OrderId { set; get; }
       public double CardFee { set; get; }
       public double OrderDiscount { set; get;}
       public string DiscountType { set; get; }
       public double DiscountPercent { set; get; }
       public double DiscountFlat { set; get; }
       public double ItemDiscount { set; get; }
       public double DeliveryCharge { set; get; }
       public string OrderType { set; get; }
       public string PrintOrderType { set; get; }
       public string DeliveryTime { set; get; }
       public string PaymentMethod { set; get; }
       public string TableNumber { set; get; }
       public int Person { set; get; }
       public int CustomerId { set; get; }
       public int TableId { get; set; }
       public double ServiceCharge { set; get; }
       public double ServiceChargePercent { set; get; }
       public int OrderNo { get; set; }

       public string GetSha1(string value)
       {
           var data = Encoding.ASCII.GetBytes(value);
           var hashData = new SHA1Managed().ComputeHash(data);

           var hash = string.Empty;

           foreach (var b in hashData)
               hash += b.ToString("X2");

           return hash;
       }

    }
}
