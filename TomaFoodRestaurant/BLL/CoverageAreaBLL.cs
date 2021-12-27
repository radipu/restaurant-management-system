using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class CoverageAreaBLL
    {
        public List<AreaCoverage> GetCoverageArea()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CoverageAreaDAO aCoverageAreaDao = new CoverageAreaDAO();
                return aCoverageAreaDao.GetCoverageArea();
            }
            else
            {
                MySqlCoverageAreaDAO aCoverageAreaDao = new MySqlCoverageAreaDAO();
                return aCoverageAreaDao.GetCoverageArea();
            }

        }

        public AreaCoverage GetCoverageAreaByPostcode(string postCode, int restaurantId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CoverageAreaDAO aCoverageAreaDao = new CoverageAreaDAO();
                return aCoverageAreaDao.GetCoverageAreaByPostcode(postCode, restaurantId);

            }
            else
            {
                MySqlCoverageAreaDAO aCoverageAreaDao = new MySqlCoverageAreaDAO();
                return aCoverageAreaDao.GetCoverageAreaByPostcode(postCode, restaurantId);

            }

        }
    }
}
