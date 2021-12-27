using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.Model
{
    public class EmailSettings
    {
        public string sender_name { get; set; }
        public string sender_email { get; set; }
        public string to_email { get; set; }
        public string to_name { get; set; }
        public string subject { get; set; }
        public string delivary_time { get; set; }
        public string order_type { get; set; }
        public string status { get; set; }
        public string msg { get; set; }

    }
}
