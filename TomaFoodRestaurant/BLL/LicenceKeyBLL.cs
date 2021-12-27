using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class LicenceKeyBLL
    {
       public int InsertLicenceKey(LicenceKey restaurantLicence)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               LicenceKeyDAO aLicenceKeyDao = new LicenceKeyDAO();
               return aLicenceKeyDao.InsertLicenceKey(restaurantLicence);
           }
           else
           {
               MySqlLicenceKeyDAO aLicenceKeyDao = new MySqlLicenceKeyDAO();
               return aLicenceKeyDao.InsertLicenceKey(restaurantLicence);
           }
          
       }

       internal string UpdateLicenceKey(LicenceKey aLicenceKey,string key)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               LicenceKeyDAO aLicenceKeyDao = new LicenceKeyDAO();
               return aLicenceKeyDao.UpdateLicenceByKey(aLicenceKey, key);
           }
           else
           {
               MySqlLicenceKeyDAO aLicenceKeyDao = new MySqlLicenceKeyDAO();
               return aLicenceKeyDao.UpdateLicenceByKey(aLicenceKey, key);
           }
        
       }
       internal LicenceKey GetRestaurantLicence(int restaurantId, string systemKey)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               LicenceKeyDAO aLicenceKeyDao = new LicenceKeyDAO();
               return aLicenceKeyDao.GetRestaurantLicence(restaurantId, systemKey);
           }
           else
           {
               MySqlLicenceKeyDAO aLicenceKeyDao = new MySqlLicenceKeyDAO();
               return aLicenceKeyDao.GetRestaurantLicence(restaurantId, systemKey);
           }
         
       }

       internal LicenceKey GetRestaurantLicenceByLicence(int restaurantId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               LicenceKeyDAO aLicenceKeyDao = new LicenceKeyDAO();
               return aLicenceKeyDao.GetRestaurantLicenceByLicence(restaurantId);
           }
           else
           {
               MySqlLicenceKeyDAO aLicenceKeyDao = new MySqlLicenceKeyDAO();
               return aLicenceKeyDao.GetRestaurantLicenceByLicence(restaurantId);
           }

         
       }


       internal LicenceKey GetRestaurantLicenceBykey(int restaurantId, string systemKey)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               LicenceKeyDAO aLicenceKeyDao = new LicenceKeyDAO();
               return aLicenceKeyDao.GetRestaurantLicenceBykey(restaurantId, systemKey);
           }
           else
           {
               MySqlLicenceKeyDAO aLicenceKeyDao = new MySqlLicenceKeyDAO();
               return aLicenceKeyDao.GetRestaurantLicenceBykey(restaurantId, systemKey);
           }
       }
    }
}
