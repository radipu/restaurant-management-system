using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
  public  class GlobalUrlBLL
    {
      internal GlobalUrl GetUrls()
      {
          if (GlobalSetting.DbType == "SQLITE")
          {
              GlobalUrlDAO aGlobalUrlDao = new GlobalUrlDAO();
              return aGlobalUrlDao.GetUrls();
          }
          else
          {
              MySqlGlobalUrlDAO aGlobalUrlDao = new MySqlGlobalUrlDAO();
              return aGlobalUrlDao.GetUrls();
          }
         
      }

      internal int InsertUrls(GlobalUrl url)
      {

          if (GlobalSetting.DbType == "SQLITE")
          {

              GlobalUrlDAO aGlobalUrlDao = new GlobalUrlDAO();
              return aGlobalUrlDao.InsertUrls(url);

          }
          else
          {

              MySqlGlobalUrlDAO aGlobalUrlDao = new MySqlGlobalUrlDAO();
              return aGlobalUrlDao.InsertUrls(url);

          }
      }
      internal int UpdateUrlsForSetting(LicenceKey SettingURL)
      {

          if (GlobalSetting.DbType == "SQLITE")
          {
              GlobalUrlDAO aGlobalUrlDao = new GlobalUrlDAO();
              return aGlobalUrlDao.UpdateUrlsForSetting(SettingURL);

          }
          else
          {
              MySqlGlobalUrlDAO aGlobalUrlDao = new MySqlGlobalUrlDAO();
              return aGlobalUrlDao.UpdateUrlsForSetting(SettingURL);
          }

      }
      internal string UpdateUrls(GlobalUrl url)
      {

          if (GlobalSetting.DbType == "SQLITE")
          {
              GlobalUrlDAO aGlobalUrlDao = new GlobalUrlDAO();
              return aGlobalUrlDao.UpdateUrls(url);

          }
          else
          {
              MySqlGlobalUrlDAO aGlobalUrlDao = new MySqlGlobalUrlDAO();
              return aGlobalUrlDao.UpdateUrls(url);
          }
         
      }
    }
}
