using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class RestaurantTableBLL
    {
       public List<RestaurantTable> GetRestaurantTable()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               RestaurantTableDAO aRestaurantTableDao = new RestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTable();
           }
           else
           {

               MySqlRestaurantTableDAO aRestaurantTableDao = new MySqlRestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTable();
           }
       }

       internal RestaurantTable GetRestaurantTableByTableId(int tableId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               RestaurantTableDAO aRestaurantTableDao = new RestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTableByTableId(tableId);
           }
           else
           {
               MySqlRestaurantTableDAO aRestaurantTableDao = new MySqlRestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTableByTableId(tableId);
           }
       }

       internal int UpdateRestaurantTable(RestaurantTable aRestaurantTable)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               RestaurantTableDAO aRestaurantTableDao = new RestaurantTableDAO();
               return aRestaurantTableDao.UpdateRestaurantTable(aRestaurantTable);
           }
           else
           {
               MySqlRestaurantTableDAO aRestaurantTableDao = new MySqlRestaurantTableDAO();
               return aRestaurantTableDao.UpdateRestaurantTable(aRestaurantTable);
           }
       }

       internal RestaurantTable GetRestaurantTableByTableNumber(string tableNumber)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               RestaurantTableDAO aRestaurantTableDao = new RestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTableByTableNumber(tableNumber);
           }
           else
           {

               MySqlRestaurantTableDAO aRestaurantTableDao = new MySqlRestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTableByTableNumber(tableNumber);
           }
       }

       internal string ToAvailableMergeTable(RestaurantTable aTable)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               RestaurantTableDAO aRestaurantTableDao = new RestaurantTableDAO();
               return aRestaurantTableDao.ToAvailableMergeTable(aTable);
           }
           else
           {
               MySqlRestaurantTableDAO aRestaurantTableDao = new MySqlRestaurantTableDAO();
               return aRestaurantTableDao.ToAvailableMergeTable(aTable);
           }
           
       }

       internal List<RestaurantTable> GetRestaurantTableByMergeId(int mergeId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               RestaurantTableDAO aRestaurantTableDao = new RestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTableByMergeId(mergeId);
           }
           else
           {
               MySqlRestaurantTableDAO aRestaurantTableDao = new MySqlRestaurantTableDAO();
               return aRestaurantTableDao.GetRestaurantTableByMergeId(mergeId);
           }

       }


    }
}
