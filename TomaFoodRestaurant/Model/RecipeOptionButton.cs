using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
   public  class RecipeOptionButton:Button
    {
       public int RecipeOptionId { set; get; }
       public string Title { set; get; }
       public string Type { set; get; }
       public int ItemLImit { set; get; }
       public int IsOnline { set; get; }
       public int IsOffline { set; get; }
       public double Price { set; get; }
       public double InPrice{set;get;}
       public int PlusMinus { set; get; }
       public string optionId { set; get; }


    }
}
