using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class ExtraPriceBLL
    {
       public ExtraPriceModel GetExtraPrice()
       {

           if (GlobalSetting.DbType == "SQLITE")
           {
               ExtraPriceDAO aExtraPriceDao = new ExtraPriceDAO();
               return aExtraPriceDao.GetExtraPrice();
           }
           else
           {
               MySqlExtraPriceDAO aExtraPriceDao = new MySqlExtraPriceDAO();
               return aExtraPriceDao.GetExtraPrice();
           }

       }
    }
}
