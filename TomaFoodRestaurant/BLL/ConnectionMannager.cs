using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.DAL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class ConnectionMannager
    {
       public static bool IsExistDatatabase(ConnectionModelSave connectionModelSave)
       {
           if (GlobalSetting.DbType == "MYSQL")
           {
               return MySqlGatewayConnection.IsExistDatatabase(connectionModelSave);
           }
           else
           {
               return GatewayConnection.IsExistDatatabase(connectionModelSave);
           }
       }
    }
}
