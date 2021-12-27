using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public   class CustomerOrderItems
    {
      public int CustomerId { set; get; }
      public int RecipeId { set; get; }
      public int PackageId { set; get; }
      public string TimeAdded { set; get;}
    }
}
