using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.Model
{
  public  class Customer
    {
      private void LoadCustomerForReorder(int customerId, mainForm form)
      {
          RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

          CustomerBLL aCustomerBll = new CustomerBLL();
          RestaurantUsers aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(customerId);
          if (aRestaurantUser != null && aRestaurantUser.Id > 0)
          {
              string cell = aRestaurantUser.Mobilephone != ""
                  ? aRestaurantUser.Mobilephone
                  : aRestaurantUser.Homephone;

              string address = aRestaurantUser.Firstname;
              address += "," + cell;
              bool flag = false;
              if (form.aGeneralInformation != null && form.aGeneralInformation.OrderId > 0)
              { 
                  address += Environment.NewLine;
                  RestaurantOrder aORder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(form.aGeneralInformation.OrderId);
                  if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                  {
                      string[] ss = aORder.DeliveryAddress.Split(',');
                      flag = true;
                      // address +="\r\n"+ aORder.DeliveryAddress.Replace(",",",\r\n");

                      if (ss.Count() > 0)
                      {
                          address += "," + ss[0];
                      }

                      if (ss.Count() > 1)
                      {
                          address += "," + ss[1];
                      }
                      if (ss.Count() > 2)
                      {
                          address += "," + ss[2];
                      }
                      if (ss.Count() > 3)
                      {
                          address += ", " + ss[3];
                      }
                  }

              }

             
          }
      }
    }
}
