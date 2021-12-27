using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public  class OrderItemMerged
    {
    
     public string Option_ids;
     public int ItemId { set; get; }
     public int OrderItemId { set; get; }
     public int OrderId { set; get; }
     public int TableNumber { set; get; }
     public string ItemName { set; get; }
     public string ItemFullName { set; get; }
     public double Price {set;get;}
     public int Qty {set;get;} 
     public int CategoryId {set;get;}
     public int RecipeTypeId {set;get;}
     public int SortOrder { set; get; }
     public int CatSortOrder { set; get; }
     public int KitchenSection {set;get;}
     public Int32 sendToKitchen { set; get; }
     public Int32 KitchenProcessing { set; get; }
     public Int32 KitchenDone { set; get; }
     public int OptionsIndex {set;get;}
     public string OptionName { get; set; }
     public string OptionNoOption { get; set; }
     public List<OptionJson> ItemOption { get; set; }
    }
}
