using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class CustomerRecentItemBLL
    {
        public List<CustomerRecentItemMD> GetCustomerRecentItemMd(int customerId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerRecentItemDAO aCustomerRecentItemDao = new CustomerRecentItemDAO();
                return aCustomerRecentItemDao.GetCustomerRecentItemMd(customerId);
            }
            else
            {
                MySqlCustomerRecentItemDAO aCustomerRecentItemDao = new MySqlCustomerRecentItemDAO();
                return aCustomerRecentItemDao.GetCustomerRecentItemMd(customerId);
            }

        }

        public string InsertCustomerRecentItems(List<CustomerRecentItemMD> aCustomerRecentItemMds)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerRecentItemDAO aCustomerRecentItemDao = new CustomerRecentItemDAO();
                return aCustomerRecentItemDao.InsertCustomerRecentItems(aCustomerRecentItemMds);
            }
            else
            {
                MySqlCustomerRecentItemDAO aCustomerRecentItemDao = new MySqlCustomerRecentItemDAO();
                return aCustomerRecentItemDao.InsertCustomerRecentItems(aCustomerRecentItemMds);
            }

        }
    }
}
