using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class PrintStyleBLL
    {

       internal PrintStyle GetprintStyleForHeaderAndFooter()
       {
           RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
           PrintStyle aPrintStyle = new PrintStyle();
           RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
           if (aRestaurantInformation.RecieptOption != "none")
           {
               aPrintStyle.Header = aRestaurantInformation.RestaurantName + "\r\n" + aRestaurantInformation.House + "," + aRestaurantInformation.Address + " " +
               aRestaurantInformation.Postcode + "\r\n" + aRestaurantInformation.Phone + "\r\n" + aRestaurantInformation.VatRegNo;
           }
           else
           {
               aPrintStyle.Header = " ";
           }
           PrintFormat aPrintFormat = new PrintFormat(25);
          // aPrintStyle.Footer = aPrintFormat.SetComments(aRestaurantInformation.ThankYouMsg, 25);
           return aPrintStyle;

       }

    }
}
