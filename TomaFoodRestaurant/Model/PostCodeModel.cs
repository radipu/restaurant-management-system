using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public   class PostCodeModel
    {

        public String Postcode { set; get; }
        public String Latitude { set; get; }
        public String Longitude { set; get; }
        public String Easting { set; get; }
        public String Northing { set; get; }
        public String Gridref { set; get; }
        public String County { set; get; }
        public String District { set; get; }
        public String Ward { set; get; }
        public Int64 Updated { set; get; }
        public String Formatted_address { set; get; }
    }
}
