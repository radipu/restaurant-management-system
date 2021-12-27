using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TomaFoodRestaurant.Model
{
   //public  class PrinterConfig
   // {

   //     private  DataSet tempDataSet;

   //     private string kitchenPrintName = "default kitchen printer";
   //     private string bevaragePrintName = "default bevarage printer";
   //     private string clientPrintName = "";
    


   //     public PrinterConfig()
   //     {
   //         try
   //         {
   //             tempDataSet = new DataSet();
   //             tempDataSet.ReadXml("Config/Print_Config.xml");
   //             kitchenPrintName = tempDataSet.Tables[0].Rows[0]["KitchenPrinterName"].ToString();
   //             bevaragePrintName = tempDataSet.Tables[0].Rows[0]["BeveragePrinterName"].ToString();
   //             clientPrintName = tempDataSet.Tables[0].Rows[0]["ClientPrinterName"].ToString();
          

   //             //OtherNonFoodPrinter
   //             XDocument doc = XDocument.Load("Config/Print_Config.xml");

   //             var authors = doc.Descendants("CPrintConfig");
   //             string sr = "";

   //             foreach (var author in authors)
   //             {
   //                  sr += author.Value;
   //             }
       


   //         }
   //         catch (Exception ex)
   //         {
   //             MessageBox.Show(ex.ToString());
   //         }  
   //     }

   //     public bool SaveChanges()
   //     {
   //         bool success = false;
   //         try
   //         {
   //             tempDataSet.Tables[0].Rows[0]["KitchenPrinterName"]=kitchenPrintName;
   //             tempDataSet.Tables[0].Rows[0]["BeveragePrinterName"]=bevaragePrintName;
   //             tempDataSet.Tables[0].Rows[0]["ClientPrinterName"]=clientPrintName;
   //             tempDataSet.WriteXml("Config/Print_Config.xml");

   //             success= true;
   //         }
   //         catch (Exception ex)
   //         {
   //             success = false;
   //             MessageBox.Show(ex.ToString());
   //         }
   //         return success;
   //     }

   //     public string KitchenPrinterName 
   //     {
   //         get { return kitchenPrintName; }
   //         set { kitchenPrintName = value; }
   //     }
   //     public string BeveragePrinterName
   //     {
   //         get { return bevaragePrintName; }
   //         set { bevaragePrintName = value; }
   //     }
   //     public string ClientPrinterName
   //     {
   //         get { return clientPrintName; }
   //         set { clientPrintName = value; }
   //     }
   // }
}
