using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public class RecipeMultipleMD
    {
        public int CategoryId { set; get; }
        public int OrderItemId { set; get; }
        public int RestaurantId { set; get; }
        public int RecipeTypeId { set; get; }
        public string MultiplePartName { set; get; }
        public string Description { set; get; }
        public double UnitPrice { set; get; }
        public double Qty { set; get; }
        public double ItemLimit { set; get; }
        public int OptionsIndex { set; get; }
        public int ItemId { set; get; }
        public List<MultipleItemMD> MultipleItem { get; set; } 
    }
}
