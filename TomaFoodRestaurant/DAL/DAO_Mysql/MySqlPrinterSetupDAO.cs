using System;
using System.Collections.Generic;
using System.Data;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
   public class MySqlPrinterSetupDAO:MySqlGatewayConnection{
        internal int SavePrinter(PrinterSetup aPrinterSettings)
        {
            long lastId = 0;
            Query = String.Format("INSERT INTO PrinterSetup (RestaurantId,PrinterName,PrinterAddress,PrintStyle,RecipeTypeList,RecipeNames,printCopy,Status,printerMargin)" +
                " VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", aPrinterSettings.RestaurantId, aPrinterSettings.PrinterName, aPrinterSettings.PrinterAddress,
                aPrinterSettings.PrintStyle, aPrinterSettings.RecipeTypeList, aPrinterSettings.RecipeNames, aPrinterSettings.PrintCopy, aPrinterSettings.Status, aPrinterSettings.printerMargin);
             try
             {
                    command = CommandMethod(command);
                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
             }
             catch (Exception exception)
             {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
             }
             return (int)lastId;
        }

        internal List<PrinterSetup> GetTotalPrinterList()
        {
            List<PrinterSetup> printerList = new List<PrinterSetup>();
            //SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM PrinterSetup");
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                try
                {
                    PrinterSetup aPrinter = ReaderToPrinter(DT.Rows[i]);
                    printerList.Add(aPrinter);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

       
          return printerList;
        }

        private PrinterSetup ReaderToPrinter(DataRow oReader)
        {
            PrinterSetup aPrinter = new PrinterSetup();
            if (oReader["Id"] != DBNull.Value)
            {
                aPrinter.Id = Convert.ToInt32(oReader["Id"]);}
            if (oReader["RestaurantId"] != DBNull.Value)
            {aPrinter.RestaurantId = Convert.ToInt32(oReader["RestaurantId"]);
            }
            if (oReader["PrinterName"] != DBNull.Value)
            {
                aPrinter.PrinterName = Convert.ToString(oReader["PrinterName"]);
            }
            if (oReader["PrinterAddress"] != DBNull.Value)
            {
                aPrinter.PrinterAddress = Convert.ToString(oReader["PrinterAddress"]);
            }
            if (oReader["PrintStyle"] != DBNull.Value)
            {
                aPrinter.PrintStyle = Convert.ToString(oReader["PrintStyle"]);
            }

            if (oReader["RecipeTypeList"] != DBNull.Value)
            {
                aPrinter.RecipeTypeList = Convert.ToString(oReader["RecipeTypeList"]);
            }

            if (oReader["RecipeNames"] != DBNull.Value)
            {
                aPrinter.RecipeNames = Convert.ToString(oReader["RecipeNames"]);
            }
            if (oReader["printCopy"] != DBNull.Value)
            {
                aPrinter.PrintCopy = Convert.ToInt16(oReader["printCopy"]);
            }
            if (oReader["Status"] != DBNull.Value)  
            {
                aPrinter.Status = Convert.ToString(oReader["Status"]);
            }
            if (oReader["printerMargin"] != DBNull.Value)
            {
                aPrinter.printerMargin = Convert.ToInt16(oReader["printerMargin"]);
            }
          
            return aPrinter;
        }


        internal int DeletePrinterByPrinterId(int printerId)
        {

            long lastId = 0;

            Query = String.Format("delete from  PrinterSetup where Id={0};", printerId);

            try
            {

                command = CommandMethod(command);

                lastId = command.ExecuteNonQuery();
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            


            return (int)lastId;
        }

        internal PrinterSetup GetPrinterByPrinterId(int printerId)
        {
            PrinterSetup aPrinter = new PrinterSetup();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM PrinterSetup where Id={0};", printerId);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            // dataRow = command.ExecuteReader();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                try
                {
                    aPrinter = ReaderToPrinter(DT.Rows[i]);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }
       
            return aPrinter;
        }

        internal int UpdatePrinter(PrinterSetup aPrinterSettings)
        {
            long lastId = 0;

            Query = String.Format("update PrinterSetup set PrinterName='{0}',PrinterAddress='{1}',PrintStyle='{2}' where Id={3};",
                aPrinterSettings.PrinterName, aPrinterSettings.PrinterAddress, aPrinterSettings.PrintStyle, aPrinterSettings.Id);
            
            try
            {
                command = CommandMethod(command);

                lastId = command.ExecuteNonQuery();
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }




            return (int)lastId;
        }

       public DataTable GetAllReceipeTypeForPrint()
       {
           DataSet DS = new DataSet();
           DataTable DT = new DataTable();
           Query = String.Format("select c.recipe_type,c.name,c.id from rcs_recipe_type t inner join rcs_recipe_category c on  t.id=c.recipe_type");

           command = CommandMethod(command);

           Reader = ReaderMethod(Reader, command);
           DT.Load(Reader);

           return DT;
       }
       public int UpdatePrinterStatus(PrinterSetup aPrinterSettings) //Basher
       {
           long lastId = 0;

           Query = String.Format("update PrinterSetup set Status='{0}' where Id={1};",
               aPrinterSettings.Status, aPrinterSettings.Id);

           try
           {
               command = CommandMethod(command);

               lastId = command.ExecuteNonQuery();
               bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

           }
           catch (Exception exception)
           {
               ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
               aErrorReportBll.SendErrorReport(exception.ToString());
           }
           return (int)lastId;
       }
   }
}
