using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class RecipeOptionMD
    {

        public int RecipeId { set; get; }
        public int TableNumber { set; get; }
        public int RecipeOptionId { set; get; }
        public int PackageItemRowId { set; get; }
        public int RecipeOPtionItemId { set; get; }
        public double Qty { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
        public double Price { set; get; }
        public double InPrice { set; get; }
        public int OptionsIndex { set; get; }
        public int PackageItemOptionsIndex { set; get; }
        public string MinusOption { set; get; }
       public bool Isoption { get; set; }
    }
}
