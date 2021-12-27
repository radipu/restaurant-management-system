using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
  public   class LicenceKey
    {
        public string till { get; set; }
        public string viewpage { get; set; }
        public string onlineConnect { get; set; }
        public Int32 restaurant_id { set; get; }
        public String license_code { set; get; }
        public Int64 is_installed { set; get; }
        public String date_installed { set; get; }
        public String hardware_info { set; get; }

        public Int32 checkOnlineOrder { set; get; }
        public Int32 checkReservation { set; get; }
        public bool IsReservationCheck { get; set; }
        public bool IsCardVisible { get; set; }
        public bool IsCallerId { get; set; }
    }
}
