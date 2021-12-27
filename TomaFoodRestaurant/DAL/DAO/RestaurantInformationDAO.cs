using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class RestaurantInformationDAO : GatewayConnection
    {
        public RestaurantInformation GetRestaurantInformation()
        {

            Query = String.Format("SELECT * FROM rcs_restaurant;");
            RestaurantInformation aRestaurantInformation = new RestaurantInformation();

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            DataTable Dt=new DataTable();
            Dt.Load(Reader);
            int rowCount = 0; 
            while (Dt.Rows.Count>rowCount)
            {
                aRestaurantInformation = new RestaurantInformationReader().ReadRestaurantInformation(Dt,rowCount);
                rowCount++;
            }
            return aRestaurantInformation;

        }

     
        internal bool UpdateRestaurantLicense(RestaurantSync aRestaurantSync)
        {

            int lastId = 0;
            try
            {

                Query = String.Format("UPDATE [rcs_restaurant] SET expire='{0}', update_required='{1}'", aRestaurantSync.expire, aRestaurantSync.update_required);

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

            }
            catch (Exception ex)
            {
                 ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                
            }

            return lastId > 0;

        }


        internal DataTable getKitchenData()
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_restaurant_kitchen;");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            return DT;
        }
    }
}
