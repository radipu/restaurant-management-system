using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomaFoodRestaurant.Model
{
  public  class RestaurantUsers
    {

        public Int32 Id { set; get; }
        public String Usertype { set; get; }
        public String Firstname { set; get; }
        public String Lastname { set; get; }
        public String Email { set; get; }
        public String Username { set; get; }
        public String Password { set; get; }
        public String ManagePassword { set; get; }
        public String House { set; get; }
        public String Address { set; get; }
        public String Homephone { set; get; }
        public String Workphone { set; get; }
        public String Mobilephone { set; get; }
        public String City { set; get; }
        public String County { set; get; }
        public String Postcode { set; get; }
        public string FullAddress { set; get; }
        public String Status { set; get; }
        public String ActivationKey { set; get; }
        public Int64 LoggedIn { set; get; }
        public String MacAddress { set; get; }
        public DateTime LastActivity { set; get; }
        public Int64 CanManage { set; get; }
        public Int64 UseJava { set; get; }
        public Int64 CheckOnlineOrder { set; get; }
        public string GcmRegId { set; get; }
        public int IsUpdate { set; get; }

        public string Name { set; get; }
      public bool Autorize { get; set; }
    }
}
