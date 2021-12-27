using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class PrinterSetupDAO : GatewayConnection
    {
        internal int SavePrinter(PrinterSetup aPrinterSettings)
        {
            long lastId = 0;


            Query = String.Format("INSERT INTO PrinterSetup (RestaurantId,PrinterName,PrinterAddress,PrintStyle,RecipeTypeList,RecipeNames)" +
                " VALUES ({0},'{1}','{2}','{3}','{4}','{5}');", aPrinterSettings.RestaurantId, aPrinterSettings.PrinterName, aPrinterSettings.PrinterAddress,
                aPrinterSettings.PrintStyle, aPrinterSettings.RecipeTypeList, aPrinterSettings.RecipeNames);



            try
            {
                command = CommandMethod(command);


                lastId = command.ExecuteNonQuery();

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
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM PrinterSetup;");

            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);


            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    PrinterSetup aPrinter = ReaderToPrinter(Reader);
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

        private PrinterSetup ReaderToPrinter(SQLiteDataReader oReader)
        {
            PrinterSetup aPrinter = new PrinterSetup();
            if (oReader["Id"] != DBNull.Value)
            {
                aPrinter.Id = Convert.ToInt32(oReader["Id"]);
            }
            if (oReader["RestaurantId"] != DBNull.Value)
            {
                aPrinter.RestaurantId = Convert.ToInt32(oReader["RestaurantId"]);
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

            return aPrinter;
        }


        internal int DeletePrinterByPrinterId(int printerId)
        {

            long lastId = 0;

            Query = String.Format("delete from  PrinterSetup where Id={0};", printerId);

            try
            {

                command = CommandMethod(command);

                lastId = command.ExecuteNonQuery(CommandBehavior.SingleResult);

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

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    aPrinter = ReaderToPrinter(Reader);

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
    }
}
