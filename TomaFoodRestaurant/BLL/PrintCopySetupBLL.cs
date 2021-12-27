using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class PrintCopySetupBLL
    {
       internal int UpdatePrintCopySetup(PrintCopySetup aPrintCopySetup)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrintCopySetupDAO aPrintCopySetupDao = new PrintCopySetupDAO();
               return aPrintCopySetupDao.UpdatePrintCopySetup(aPrintCopySetup);
           }
           else
           {
               MySqlPrintCopySetupDAO aPrintCopySetupDao = new MySqlPrintCopySetupDAO();
               return aPrintCopySetupDao.UpdatePrintCopySetup(aPrintCopySetup);
           }

       }

       internal int InsertPrintCopySetup(PrintCopySetup aPrintCopySetup)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {

               PrintCopySetupDAO aPrintCopySetupDao = new PrintCopySetupDAO();
               return aPrintCopySetupDao.InsertPrintCopySetup(aPrintCopySetup);
           }
           else
           {
               MySqlPrintCopySetupDAO aPrintCopySetupDao = new MySqlPrintCopySetupDAO();
               return aPrintCopySetupDao.InsertPrintCopySetup(aPrintCopySetup);
           }
       }

       internal PrintCopySetup GetPrintCopy()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrintCopySetupDAO aPrintCopySetupDao = new PrintCopySetupDAO();
               return aPrintCopySetupDao.GetPrintCopy();
           }
           else
           {
               MySqlPrintCopySetupDAO aPrintCopySetupDao = new MySqlPrintCopySetupDAO();
               return aPrintCopySetupDao.GetPrintCopy();
           }
       }
    }
}
