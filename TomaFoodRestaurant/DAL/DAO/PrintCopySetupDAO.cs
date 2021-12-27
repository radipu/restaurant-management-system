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
    public class PrintCopySetupDAO : GatewayConnection
    {
        internal int UpdatePrintCopySetup(PrintCopySetup aPrintCopySetup)
        {
            int lastId = 0;

            Query = String.Format("UPDATE PrintCopySetup set  TakeawayCopy={1}, CollectionCopy= {2},TableCopy={3} where Id={0};"
                , aPrintCopySetup.Id, aPrintCopySetup.TakeawayCopy, aPrintCopySetup.CollectionCopy, aPrintCopySetup.TableCopy);


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

        internal int InsertPrintCopySetup(PrintCopySetup aPrintCopySetup)
        {
            int lastId = 0;

            Query = String.Format("INSERT INTO PrintCopySetup (TakeawayCopy,CollectionCopy,TableCopy)" +
                " VALUES ({0},{1},{2});", aPrintCopySetup.TakeawayCopy, aPrintCopySetup.CollectionCopy, aPrintCopySetup.TableCopy);


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




        internal PrintCopySetup GetPrintCopy()
        {
            PrintCopySetup printerList = new PrintCopySetup();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            string query = String.Format("SELECT * FROM PrintCopySetup;");

            using (SQLiteConnection c = new SQLiteConnection(GlobalSetting.ConnectionString, true))
            {

                using (SQLiteCommand command = new SQLiteCommand(query, c))
                {

                    c.Open();
                    using (SQLiteDataReader dataRow = command.ExecuteReader())
                    {
                        // dataRow = command.ExecuteReader();
                        while (dataRow.Read())
                        {

                            try
                            {
                                printerList = ReaderToPrinterCopy(dataRow);
                            }
                            catch (Exception exception)
                            {
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(exception.ToString());
                            }


                        }

                    }
                    c.Close();
                }
            }

            return printerList;
        }

        private PrintCopySetup ReaderToPrinterCopy(SQLiteDataReader oReader)
        {
            PrintCopySetup aPrintCopy = new PrintCopySetup();
            if (oReader["Id"] != DBNull.Value)
            {
                aPrintCopy.Id = Convert.ToInt32(oReader["Id"]);
            }
            if (oReader["TakeawayCopy"] != DBNull.Value)
            {
                aPrintCopy.TakeawayCopy = Convert.ToInt32(oReader["TakeawayCopy"]);
            }
            if (oReader["TableCopy"] != DBNull.Value)
            {
                aPrintCopy.TableCopy = Convert.ToInt32(oReader["TableCopy"]);
            }
            if (oReader["CollectionCopy"] != DBNull.Value)
            {
                aPrintCopy.CollectionCopy = Convert.ToInt32(oReader["CollectionCopy"]);
            }

            return aPrintCopy;
        }
    }
}
