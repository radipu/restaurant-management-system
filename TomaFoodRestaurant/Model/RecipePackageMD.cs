using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public class RecipePackageMD
    {
      public List<PackageItem> packageItemList { get; set; }
      public int id { get; set; }


        public int PackageId { set; get; }
        public int RestaurantId { set; get; }
        public int RecipeTypeId { set; get; }
        public string PackageName { set; get; }
        public string Description { set; get; }
        public double UnitPrice { set; get; }
        public double Qty { set; get; }
        public double ItemLimit { set; get; }
        public int OptionsIndex { set; get; }
        public double Extraprice { set; get; }
        public int KitichineDone { get; set; }
      public RecipePackageButton RecipePackageButton { get; set; }
    }
}
