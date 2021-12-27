using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
   public  class ReceipeTypeButton:Button
    {
       public int TypeId { set; get; }
       public int ParentTypeId { set; get;}
       public string TypeName { set; get; }
       public int RestaurantId { set; get; }
       public int SortOrder { set; get; }
       public int MergeItems { set; get; }
       public int CategoryWidth { set; get; }
       public int PackageWidth { set; get; }
       public int SubcategoryWidth { set; get; }



    }
}
