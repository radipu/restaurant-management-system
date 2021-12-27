using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant
{
    public static class GlobalVars
    {
        public static bool isOnlineOrderTimer = false;
        
        //interval time of checking online order (n means 10n secs interval)
        public static int intervalCheckOnlineOrder = 6;
        public static int checkOnlineOrderCounter = intervalCheckOnlineOrder;


        //software version
        public static string sVersion = "1.5.67";



        //Base url and firebase for live server
        //public static string hostUrl = "https://tomafood.net/";
        //public static string apiUrl = "https://api.tomafood.net/v1/";
        //public static string firebaseUrl = "https://tomafood.firebaseio.com";
        //public static string firebaseAuth = "vJoIfWKh0hBPR8lJ0XrjqU9kftK5ZwITaQhX8xd3";



        //  Base url and firebase for staging server
        public static string hostUrl = "http://tomafood-net.test/";
        public static string apiUrl = "http://tomafood-net.test/api/v1/";
        public static string firebaseUrl = "https://tomafood-test.firebaseio.com";
        public static string firebaseAuth = "JDkqE3HuAvCao34dl65MhScuycyFOax3wv3Y2AMg";

        //module name for stripe
        public static string stripePaymentModule = "Stripe_payment";
        public static string giniApi = "q2RKbS62lWqRUHwycQcCcE7Z1KpMDNGi";

        /// <summary>
        /// Return rounded number with provided decimal point
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="pointDecimal"></param>
        /// <returns></returns>
        public static double numberRound(double amount,int pointDecimal = 2) {
            double newAmount = amount;
            newAmount = (double) Math.Round((decimal) amount, pointDecimal, MidpointRounding.AwayFromZero);
            return newAmount;
        }
    }

    //version //updates
    //1.5.55  //after updating local db, table order reset issue fixed, merged item (pacakge) print with option checking 
    //1.5.59  //table refresh on click table button from homepage removed
    //1.5.60  //fix kitchen print package item name not coming
    //1.5.61  //fix orderpad kitchen print for merge style, bar print, table update time
    //1.5.62  //fix startar dash, floating point amount, reservartion color, descending order, sub total amount. NOTE: mangrove update kitchen item, who using kitchen should have problem
    //1.5.63  //fix max order number counter
    //1.5.64  //add new version of cideasy caller id
    //1.5.65  //reservation sorting 
    //1.5.66  //quick menu removed and attribute item add issue
    //1.5.67  //reservation time
}
