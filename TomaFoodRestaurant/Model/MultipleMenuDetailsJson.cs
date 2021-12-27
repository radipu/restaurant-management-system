using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public  class MultipleMenuDetailsJson
    {

        public int id { set; get; }
        public string name { set; get; }
        public double price { set; get; }
        public double quantity { set; get; }
        public int category_id { set; get; }
        public int recipe_type_id { set; get; }
        public int sort_order { set; get; }
        public int kitchen_section { set; get; }
        public List<string> options { set; get; }

        public List<string> minus_options { set; get; }
    }

  public class MultipleMenuDetailsJsonResponsivePage
  {

      public int id { set; get; }
      public string name { set; get; }
      public double price { set; get; }
      public double quantity { set; get; }
      public int category_id { set; get; }
      public int recipe_type_id { set; get; }
      public int sort_order { set; get; }
      public int kitchen_section { set; get; }
      public string options { set; get; }

      public string minus_options { set; get; }
  }

  public class MultipleMenuDetailsJsonResponsivePageTest
  {

      public int id { set; get; }
      public string name { set; get; }
      public double price { set; get; }
      public double quantity { set; get; }
      public int category_id { set; get; }
      public int recipe_type_id { set; get; }
      public int sort_order { set; get; }
      public int kitchen_section { set; get; }
      public string options { set; get; }
      public string minus_options { set; get; }
  }
}
