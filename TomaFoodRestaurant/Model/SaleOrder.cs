using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class SaleOrder
    {
        public string statusa { get; set; }
       public string reorder { get; set; }
        public string details { get; set; }




       public int OrderId { set; get; }
       public int SL { set; get; }
       public string Date { set; get; }
       public string Customer { set; get; }
       public string Type { set; get; }
       public string Status { set; get; }
       public string Cash { set; get; }
       public string Card { set; get; }
     
    
       public string Total { set; get; }
       public string Receipt { set; get; }
       public int OnlineStatus { set; get; }
       
    }
}
