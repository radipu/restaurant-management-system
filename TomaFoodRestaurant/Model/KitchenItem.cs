using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    public class KitchenItem:Button
    {

        public Int32 orderItemId { set; get; }
        public String itemName { set; get; }
        public Int32 itemQuantity { set; get; }
        public Int32 sendToKitchen { set; get; }
        public Int32 kitchenProcessing { set; get; }
        public Int32 kichenDone { set; get; }
        public String tableName { set; get; }
        public String Options { set; get; }
        public String MinusOptions { set; get; }
        public Int32 personCount { set; get; }
        public Int32 kichenId { set; get; } 
    }
}
