using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    public class ReceipeMenuItemButton:Button
    {
        public int RecipeMenuItemId{set;get;}
        public int ParentId{set;get;}
        public int RestaurantId{set;get;}
        public int CategoryId{set;get;}
        public int RecipeTypeId { set; get; }
        public int SubCategoryId { set; get; }
        public string ItemName{set;get;}
        public string ReceiptName{set;get;}
        public string ShortDescrip{set;get;}
        public string LongDescrip{set;get;}
        public double InPrice{set;get;}
        public double OutPrice{set;get;}
        public double DiscountPrice{set;get;}
        public int SortOrder{set;get;}
        public int KitchenSection{set;get;}
        public int IsExclusixe{set;get;}
        public int Hot{set;get;}
        public int Nut{set;get;}
        public int ShowCategory{set;get;}
        public int ButtonHeight{set;get;}
        public int ButtonWidth{set;get;}
        public string ItemType { get; set; }
        public string OptionList { get; set; }
        public RecipePackageButton RecipePackageButton { get; set; }
        public string Status { get; set; }
        public List<OptionJson> OptionJson { get; set; }
        public ReceipeMenuItemButton receipeMenuItemButton { get; set; }

        public List<RecipeOptionItemButton> aRecipeOptionItemButtons=new List<RecipeOptionItemButton>();
        public List<PackageItem> GetMultiItemList { get; set; } 




    }
}
