using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class DistanceBLL
    {
        public Distance GetDistanceByPostcode(string restaurantPostCode, string Destination)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                DistanceDAO aDistanceDao = new DistanceDAO();
                return aDistanceDao.GetDistanceByPostcode(restaurantPostCode, Destination);
            }
            else
            {
                MySqlDistanceDAO aDistanceDao = new MySqlDistanceDAO();
                return aDistanceDao.GetDistanceByPostcode(restaurantPostCode, Destination);
            }
            
        }

        internal int InsertDistance(Distance distance)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                DistanceDAO aDistanceDao = new DistanceDAO();
                return aDistanceDao.InsertDistance(distance);
            }
            else
            {
                MySqlDistanceDAO aDistanceDao = new MySqlDistanceDAO();
                return aDistanceDao.InsertDistance(distance);
            }
            
        }

    }
}
