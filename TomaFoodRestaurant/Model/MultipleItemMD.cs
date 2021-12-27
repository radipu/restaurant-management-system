using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public class MultipleItemMD
    {
        public int ItemId { set; get; }
        public string ItemName { set; get; }
        public int Qty { set; get; }
        public double Price { set; get; }
        public string OptionName { set; get; }
        public int CategoryId { set; get; }
        public int SubcategoryId { set; get; }
        public int OptionsIndex { set; get; }
        public int RecipeTypeId { get; set; }
        public List<OptionJson> OptionList { get; set; }
    }
}
