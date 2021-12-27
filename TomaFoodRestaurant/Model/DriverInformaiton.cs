using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;

namespace TomaFoodRestaurant.Model
{
   public class DriverInformaiton:TileItem
    {
       public string DriverId { get; set; }
       public string DriverName { get; set; }
       public string PhoneNo { get; set; }
       public bool IsAvailable { get; set; }
     public  List<DriverInformaiton> DriverList { get; set; } 

    }
}
