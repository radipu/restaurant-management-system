using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
    public class Postcode
    {
        public Int64 Id { set; get; }
        public String HouseNumber { set; get; }
        public String HouseName { set; get; }
        public String AddressLine1 { set; get; }
        public String AddressLine2 { set; get; }
        public String Town { set; get; }
        public String PostCode { set; get; }
        public String Latitude { set; get; }
        public String Longitude { set; get; }
    }
}
