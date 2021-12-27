using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
   public  class ReceipeSubCategoryButton:Button
    {
       public int SubCategoryId { set; get; }
       public int ParentSubcategoryId { set; get; }
       public int RestaurantId { set; get; }
       public int RecipeTypeId { set; get; }
       public string Title { set; get; }
       public string SubCategoryName { set; get;}
       public string Description { set; get; }
       public int SortOrder { set; get; }
       public int OnlineSortOrder { set; get; }
       public int ExcludeTable { set; get; }
       public int Hot { set; get; }
       public int Nut { set; get; }
       public string ButtonColor { set; get; }
       public int ButtonHeight { set; get; }
       public int ButtonWidth { set; get; }

    }
}
