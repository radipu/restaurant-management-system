using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class PrinterSetupBLL
    {
       internal int SavePrinter(PrinterSetup aPrinterSettings)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.SavePrinter(aPrinterSettings);
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.SavePrinter(aPrinterSettings);
           }
          
       }

       public DataTable GetAllReceipeTypeForPrint()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.GetAllReceipeTypeForPrint();
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.GetAllReceipeTypeForPrint();
           }
       }
       internal List<PrinterSetup> GetTotalPrinterList()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.GetTotalPrinterList();
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.GetTotalPrinterList();
           }
       }

       internal int DeletePrinterByPrinterId(int printerId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.DeletePrinterByPrinterId(printerId);
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.DeletePrinterByPrinterId(printerId);
           }
       }

       internal PrinterSetup GetPrinterByPrinterId(int printerId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.GetPrinterByPrinterId(printerId);
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.GetPrinterByPrinterId(printerId);
               
           }
       }

       internal int UpdatePrinter(PrinterSetup aPrinterSettings)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.UpdatePrinter(aPrinterSettings);
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.UpdatePrinter(aPrinterSettings);
           }
       }

       
       internal int UpdatePrinterStatus(PrinterSetup aPrinterSettings) //Basher
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               PrinterSetupDAO aPrinterSetupDao = new PrinterSetupDAO();
               return aPrinterSetupDao.UpdatePrinter(aPrinterSettings);
           }
           else
           {
               MySqlPrinterSetupDAO aPrinterSetupDao = new MySqlPrinterSetupDAO();
               return aPrinterSetupDao.UpdatePrinterStatus(aPrinterSettings);
           }
       }
    }
}
