using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
   public class OptionJson
   { 
       public string optionId { get; set; }
       public string optionName { get; set; } 
       public double optionPrice { get; set; } 
       public int optionQty { get; set; } 
       public bool NoOption { get; set; }

    }
}
