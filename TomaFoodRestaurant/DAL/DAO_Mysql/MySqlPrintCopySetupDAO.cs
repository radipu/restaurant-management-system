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
   public class MySqlPrintCopySetupDAO:MySqlGatewayConnection
    {
        internal int UpdatePrintCopySetup(PrintCopySetup aPrintCopySetup)
        {
            int lastId = 0;

            Query = String.Format("UPDATE PrintCopySetup set  TakeawayCopy=@TakeawayCopy, CollectionCopy=@CollectionCopy,TableCopy=@TableCopy where Id=@Id;");


            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@TakeawayCopy", aPrintCopySetup.TakeawayCopy);
                command.Parameters.AddWithValue("@CollectionCopy", aPrintCopySetup.CollectionCopy);
                command.Parameters.AddWithValue("@TableCopy", aPrintCopySetup.TableCopy);
                command.Parameters.AddWithValue("@Id", aPrintCopySetup.Id);

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

        internal int InsertPrintCopySetup(PrintCopySetup aPrintCopySetup)
        {
            int lastId = 0;

            Query = String.Format("INSERT INTO PrintCopySetup (TakeawayCopy,CollectionCopy,TableCopy)" +
                " VALUES ({0},{1},{2});", aPrintCopySetup.TakeawayCopy, aPrintCopySetup.CollectionCopy, aPrintCopySetup.TableCopy);


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




        internal PrintCopySetup GetPrintCopy()
        {
            PrintCopySetup printerList = new PrintCopySetup();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM PrintCopySetup");

            try
            {
                command = CommandMethod(command);
                Reader = ReaderMethod(Reader, command);
                DT.Load(Reader);
                foreach (DataRow dataRow in DT.Rows)
                {
                    printerList = ReaderToPrinterCopy(dataRow);
                }
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return printerList;
        }

        private PrintCopySetup ReaderToPrinterCopy(DataRow oReader)
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
