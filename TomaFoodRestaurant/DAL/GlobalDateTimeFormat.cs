using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.DAL
{
   public class GlobalDateTimeFormat
    {
     public  static string TimeSpanFormat = @"dd-MMM-yy";

       public DateTime ConvertTimeCustome { get; set; }
       public GlobalDateTimeFormat(string cleaned){

            if (cleaned.Contains("0000-00-00") || cleaned.Contains("00-000-00 12:00:00 AM"))
           {
               cleaned = cleaned.Replace("0000-00-00", "0001-01-01");
               ConvertTimeCustome = Convert.ToDateTime(cleaned);

           }
           else
           {
               ConvertTimeCustome = DateTime.Parse(cleaned);
           }
            
        }
    }

   public class TimeFormatCustom
    {
     TimeSpan date=DateTime.Today.TimeOfDay;
     public static string Format = "dd-MMM-yy HH:mm:ss tt";
     //public static string Format = "dd-MMM-yy hh:mm:ss tt";
    }
}
