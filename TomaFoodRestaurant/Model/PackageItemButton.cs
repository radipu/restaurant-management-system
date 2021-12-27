using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.Model
{
   public  class PackageItemButton:Button
    {
       public String ItemName { set; get; }
       public int PackageId { set; get; }
       public int RecipeId { set; get; }
       public string OptionName { set; get; }
       public double AddPrice { set; get;}
       public int SubCategoryId { set; get; }
       public int CategoryId { set; get; }
       public string ReciptName { set; get; }
       public List<FreeOptionMD> FreeOptionMds { set; get; }
       public RecipePackageButton RecipePackageButton { set; get; }
       public PackageCategoryButton PackageCategoryButton { set; get; }
       public string Colorname { set; get; }
      
       public int CountPackageItem { get; set; }
    }
}
