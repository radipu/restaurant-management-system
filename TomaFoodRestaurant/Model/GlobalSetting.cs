using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.Model
{
  public   class GlobalSetting
    {
      public static int UserId{set;get;}
      public static string UserName { set; get; }
      public static Boolean IsOffline = true;
      public static RestaurantInformation RestaurantInformation { set; get; }
      public static RestaurantUsers RestaurantUsers { set; get; }
      public static Boolean IsCustomerAdd = false;
      public static Boolean IsLicenseUpdate = true;
      public static string ReportMessage { set; get; }
      // public static string ConnectionString = "Data Source=habijabi;Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;PRAGMA journal_mode = WAL;PRAGMA synchronous = OFF;datetimeformat=CurrentCulture;password=ginilab";
      public static string ConnectionString = "Data Source=habijabi;Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture;";
      public static string serverConnectionString = "Data Source=habijabi;Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture;";
      public static string DbType = Properties.Settings.Default.pcVersion;
      public static LicenceKey SettingInformation { get; set; }
     // public static string DbType = Properties.Settings.Default.pcVersion;
     //  public static string DbType = "MYSQL";
     // public static string ConnectionString = "Data Source=" + path + ";Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture";
      public static bool IsAutoLogin=false;
   
     
    }}
