using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
  public class RecipeOptionItemButton:Button
    {
      public int RecipeOptionItemId { set; get;}
      public int ParentOptionId { set; get; }
      public int RecipeOptionId{set;get;}
      public int RestaurantId { set; get; }
      public string Title { set; get; }
      public string MinusTitle { set; get; }
      public double InPrice { set; get; }
      public double Price { set; get; }
      public RecipeOptionButton RecipeOptionButton { set; get; }
      public bool IsNooption { get; set; }

      //  public string MinusOption  { set; get; }

    }
}
