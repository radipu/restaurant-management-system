using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
   public  class RecipePackageButton:Button
    {
       public int PackageId { set; get; }
       public int RestaurantId { set; get; }
       public int RecipeTypeId { set; get; }
       public string PackageName { set; get; }
       public string Description { set; get; }
       public double InPrice { set; get; }
       public double OutPrice { set; get; }
       public int CustomPackage { set; get; }
       public int ItemLimit { set; get; }
       public int SortOrder { set; get; }
       public string OnlineName { set; get; }
       public int DisplayTop { set; get; }
       public string PackageUpdateOrNot { get; set; }
       public int ItemQty { get; set; }
       public double ExtraItemPrice { get; set; }

       public List<PackageItem> UpdatePackageList { get; set; }
    }
}
