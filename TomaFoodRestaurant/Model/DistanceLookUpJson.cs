using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
   public class DistanceLookUpJson
    {
       public string[] destination_addresses { set; get; }
       public string[] origin_addresses { set; get; }
       public List<rows> rows { set; get; }
       public string status { set; get; }
    }
   public class rows
   {
       public List<element> elements { set; get; }
   }

    public class element
    {
        public distance distance { set; get; }
        public duration duration { set; get; }
        public string status { set; get; }
    }

    public class distance
    {
        public string text { set; get; }
        public string value { set; get; }
    }
    public class duration
    {
        public string text { set; get; }
        public string value { set; get; }
    }

}
