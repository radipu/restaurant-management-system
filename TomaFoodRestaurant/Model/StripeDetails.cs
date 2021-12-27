using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
   public class StripeDetails
    {
        public string apiKey { set; get; }
        public string publishKey { set; get; }
        public string accNumber { set; get; }
        public string feeType { set; get; }
        public double accFee { set; get; }
    }
}
