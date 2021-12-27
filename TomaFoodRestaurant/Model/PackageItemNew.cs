using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
   public class PackageItemNew
    {
        public string MinusOption { get; set; }
        public string ItemFullName { set; get; }
        public int Id { set; get; }
        public int ItemId { set; get; }
        public string ItemName { set; get; }
        public int Qty { set; get; }
        public double Price { set; get; }
        public string OptionName { set; get; }
        public int PackageId { set; get; }
        public int CategoryId { set; get; }
        public int CategorySortOrder { set; get; }
        public int SubcategoryId { set; get; }
        public int OptionsIndex { set; get; }
        public string Type { set; get; }
        public string OptionId { set; get; }
        public string EditName { get; set; }

        public string ListChildOptionName { get; set; }
        public string ListChildOptionId { get; set; }
        public List<RecipeOptionMD> PackageItemOptionList { get; set; }
       
        public bool DeleteItem { get; set; }
        public double ExtraPrice { get; set; }
        public List<MultipleItemMD> MultipleItem { get; set; }



    }
}
