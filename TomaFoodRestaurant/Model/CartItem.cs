using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraEditors;

namespace TomaFoodRestaurant.Model
{
    public class CartItem : TileItem
    {
        public int Index { get; set; }
        public int OrderId { get; set; }
        public int Recipeid { get; set; }
        public int PackageId { get; set; }
        public new string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double ExtraPrice { get; set; }
        public int Senttokitchen { get; set; }
        public int  KitchenProcessing { get; set; }
        public int KitchenDone { get; set; }
        public string LastModifyTime { get; set; }
        public string Options { get; set; }
        public string OptionsMinus { get; set; }
        public string MultipleMenu { get; set; }
        public int OrderPackageid { get; set; }
        public int Desription { get; set; }
        public string ShortDescription { get; set; }
        public Int32 RecipeMenuItemId { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }

        public int RecipeOptionItemId { set; get; }
        public int ParentOptionId { set; get; }
        public int RecipeOptionId { set; get; }
        public int RestaurantId { set; get; }
        public string Title { set; get; }
        public string MinusTitle { set; get; }
        public double InPrice { set; get; }
        public string MenuType { set; get; }

        public bool IsNooption { get; set; }
      
        public RecipeOptionButton RecipeOptionButton { set; get; }
        public ReceipeMenuItemButton ReceipeMenuItemButton { set; get; }
        public RecipeOptionItemButton RecipeOptionItemButton { set; get; }
        public RecipePackageButton RecipePackageButton { set; get; }
        public string ItemType { get; set; }
    }
}
