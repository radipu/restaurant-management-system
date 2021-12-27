using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class PrintToPrinterBLL //Basher
    {
        public List<PrinterSetup> printerSetupList = new List<PrinterSetup>();

        public bool CheckPrinterStatus()
        {
            bool isError = false;
            try
            {
                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                printerSetupList = aPrinterSetupBll.GetTotalPrinterList();

                List<string> sysemPrinterList = GetPrinterList();

                foreach (PrinterSetup psetup in printerSetupList)
                {
                    string[] printerName = psetup.PrinterName.Split(new char[] { '\\' });
                    if (printerName.Length > 0)
                    {
                        if (sysemPrinterList.Where(name => name.ToLower() == printerName[printerName.Length - 1].ToLower()).Count() == 0)
                        {
                            psetup.Status = "Error";
                            aPrinterSetupBll.UpdatePrinterStatus(psetup);
                            isError = true;
                        }
                        else
                        {
                            psetup.Status = "Active";
                            aPrinterSetupBll.UpdatePrinterStatus(psetup);
                        }
                    }
                }
                
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
                isError = true;
            }

            return isError;
        }
        public List<string> GetPrinterList()
        {
            List<string> printerList = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printerList.Add(printer);
            }
            return printerList;
        }
    }
}
