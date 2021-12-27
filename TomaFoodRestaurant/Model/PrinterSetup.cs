using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
    public class PrinterSetup
    {

        public int Id { set; get; }
        public int RestaurantId { set; get; }
        public string PrinterName { set; get; }
        public string PrinterAddress { set; get; }
        public string PrintStyle { set; get; }
        public string RecipeTypeList { set; get; }
        public string RecipeNames { set; get; }
        public int PrintCopy { get; set; }
        public string Status { set; get; }
        public int printerMargin { set; get; }
       // public int kitchenEmptyLine { set; get; }
    }
}
