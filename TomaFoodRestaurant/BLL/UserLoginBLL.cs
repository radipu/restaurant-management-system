using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class UserLoginBLL{
       internal RestaurantUsers GetResturantUserByUserId(int userId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               UserLoginDAO aUserLoginDao = new UserLoginDAO();
               return aUserLoginDao.GetResturantUserByUserId(userId);
           }
           else
           {
               MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
               return aUserLoginDao.GetResturantUserByUserId(userId);
           }
       }

       public List<RestaurantUsers> GetRestaurantAllUser()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               UserLoginDAO aUserLoginDao = new UserLoginDAO();
               return aUserLoginDao.GetRestaurantAllUser();
           }
           else
           {
               MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
               return aUserLoginDao.GetRestaurantAllUser();
           }
       }

        internal RestaurantUsers GetRestaurantUserByPassword(string password)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                UserLoginDAO aUserLoginDao = new UserLoginDAO();
                return aUserLoginDao.GetRestaurantUserByPassword(password);
            }
            else
            {
                MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
                return aUserLoginDao.GetRestaurantUserByPassword(password);
            }
        }

       internal RestaurantUsers GetRestaurantUserByUsernameAndPassword(string userName, string password)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               UserLoginDAO aUserLoginDao = new UserLoginDAO();
               return aUserLoginDao.GetRestaurantUserByUsernameAndPassword(userName, password);
           }
           else
           {
               MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
               return aUserLoginDao.GetRestaurantUserByUsernameAndPassword(userName, password);
           }
       }        

        internal List<RestaurantUsers> GetRestaurantUserByRestaurantId(int restaurantId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               UserLoginDAO aUserLoginDao = new UserLoginDAO();
               return aUserLoginDao.GetRestaurantUserByRestaurantId(restaurantId);
           }
           else
           {
               MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
               return aUserLoginDao.GetRestaurantUserByRestaurantId(restaurantId);
               
           }
           
       }

       internal RestaurantUsers GetUserByUserId(int userId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               UserLoginDAO aUserLoginDao = new UserLoginDAO();
               return aUserLoginDao.GetResturantUserByUserId(userId);
           }
           else
           {
             //  MySqlUserLoginDAO aUserLoginDao = new MySqlUserLoginDAO();
            // return aUserLoginDao.GetUserByUserId(userId);
               MySqlCustomerDAO aUserLoginDao = new MySqlCustomerDAO();
               return aUserLoginDao.GetUserByUserId(userId);                    
           }
       }
    }
}
