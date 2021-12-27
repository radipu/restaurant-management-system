using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
       public class GlobalUrl
    { 
           public int Id { set; get; }
           public string AcceptUrl { set; get; }
           public string RejectUrl { set; get; }
           public string OrderSyn { set; get; }
           public string fontSize { set; get; }
           public string fontStyle { get; set; }
           public string fontFamily { get; set; }
           public string PrinterName { get; set; }
           public int Cursur { get; set; }
           public int Keyboard { get; set; }
    }
}
