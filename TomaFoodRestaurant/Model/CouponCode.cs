using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
    public class CouponCode
    {
        public int Id { get; set; }
        public string Code { get; set; }  
        public string Title { get; set; }  
        public string Type { get; set; }
        public string ServiceType { get; set; }
        public double Discount { get; set; }
        public DateTime Expiring { get; set; }
        public string Usage { get; set; }
        public string Message { get; set; }
        public string Remove { get; set; }
        public string Print { get; set; }
        public string Edit { get; set; }
        public double MinAmount { get; set; }
    }
}
