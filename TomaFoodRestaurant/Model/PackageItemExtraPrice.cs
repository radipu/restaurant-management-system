using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public  class PackageItemExtraPrice
    {
      public int RecipeId { set; get; }
      public int PackageId { set; get; }
      public double CategoryAddPrice { set; get; }
      public double AddPrice { set; get; }
      public string OptionName{ set; get; }
    }
}
