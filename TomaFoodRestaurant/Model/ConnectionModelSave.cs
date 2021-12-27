using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaFoodRestaurant.Model
{
   public class ConnectionModelSave
   {
       public string ipadderss = Properties.Settings.Default.ipaddress;
       public string database = Properties.Settings.Default.database;
       public string username = Properties.Settings.Default.username;
       public string password = Properties.Settings.Default.password;
       public string connString = Properties.Settings.Default.connString;
       public string pcVersion = Properties.Settings.Default.pcVersion;




   }
}
