using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    public class PrinterInformation
    {
        public static bool SetDefault(string defaultPrinter)
        {
            
            //string mainPrinter = defaultPrinter.Substring(defaultPrinter.LastIndexOf('\\') + 1);
            string machineName = System.Environment.MachineName;
            string printer = "";


           // printer = "\\\\" + machineName + "\\";
              
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            {using (ManagementObjectCollection objectCollection = objectSearcher.Get())
                {
                    foreach (ManagementObject mo in objectCollection)
                    {

                        string devicePrinterName =  mo["Name"].ToString().Trim();

                        if (defaultPrinter.Trim().Equals(devicePrinterName))
                        {
                            string status = mo["PrinterStatus"].ToString();

                            string state=PrinterState(Convert.ToInt32(status));
                            if (state == "Not Available" || state == "Offline")
                            {
                                 MessageBox.Show("Printer is :" + state, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                return false;

                            }
                            else
                            {
                                mo.InvokeMethod("SetDefaultPrinter", null, null);
                                return true;
                            }

                         
                           
                        }
                    }
                }
            } 
            return false;
        }


        public static  string PrinterState(int state)
        {
            string[] arrExtendedPrinterStatus = { 
                "","Other", "Unknown", "Idle", "Printing", "Warming Up",
                "Stopped Printing", "Offline", "Paused", "Error", "Busy",
                "Not Available", "Waiting", "Processing", "Initialization",
                "Power Save", "Pending Deletion", "I/O Active", "Manual Feed"
            };
            return arrExtendedPrinterStatus[state];
        }
    }

    public class PrinterState
    {
        public string IsOffline { get; set; }

    }
}
