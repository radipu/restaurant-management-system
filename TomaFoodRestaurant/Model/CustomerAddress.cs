using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
    public class CustomerAddress
    {
        public Int32 Id { set; get; }
        public Int32 CustomerId { set; get; }
        public String HouseNumber { set; get; }
        public String Address { set; get; }
        public String Town { set; get; } 
        public String PostCode { set; get; }
        public String Latitude { set; get; }
        public String Longitude { set; get; }
    }
}
