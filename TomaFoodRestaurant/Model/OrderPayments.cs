using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
    public class OrderPayments
    {

        public Int32 Id { set; get; }
        public Int32 order_id { set; get; }
        public Int32 booking_id { set; get; }
        public string payment_reference { set; get; }
        public string refund_reference { set; get; }
        public double refund_amount { set; get; }
        public string order_info { set; get; }
        public double booking_amount { set; get; }
        public DateTime created { set; get; }
        public DateTime updated { set; get; } 
    }
}
