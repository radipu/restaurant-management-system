using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class DeliveryChargeBLL
    {
       public bool CheckDeliveryCharge(int restaurantId)
       {
           //if (GlobalSetting.DbType == "SQLITE")
           //{
           //    DeliveryChargeDAO aDeliveryChargeDAO = new DeliveryChargeDAO();
           //    return aDeliveryChargeDAO.CheckDeliveryCharge(restaurantId);
           //}
           //else
           //{
               MySqlDeliveryChargeDAO aDeliveryChargeDAO = new MySqlDeliveryChargeDAO();
               return aDeliveryChargeDAO.CheckDeliveryCharge(restaurantId);
               
          //}
           
           
          
       }

       public DelvaryCharge GetDeliveryChargeByDistance(double distance, int restaurantId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               DeliveryChargeDAO aDeliveryChargeDAO = new DeliveryChargeDAO();
               return aDeliveryChargeDAO.GetDeliveryChargeByDistance(distance, restaurantId);
           }
           else
           {

               MySqlDeliveryChargeDAO aDeliveryChargeDAO = new MySqlDeliveryChargeDAO();
               return aDeliveryChargeDAO.GetDeliveryChargeByDistance(distance, restaurantId);
           }
           
       }
    }
}
