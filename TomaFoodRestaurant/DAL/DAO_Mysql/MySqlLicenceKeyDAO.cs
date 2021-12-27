using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
  public  class MySqlLicenceKeyDAO:MySqlGatewayConnection
    {

        public int InsertLicenceKey(LicenceKey restaurantLicence)
        {
            int lastId = 0;


            Query =
                String.Format(
                    "INSERT INTO rcs_restaurant_license (restaurant_id,license_code,is_installed,date_installed,hardware_info,app_info,online_info)" +
                    " VALUES (@restaurant_id,@license_code,@is_installed,@date_installed,@hardware_info)");


            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@restaurant_id", restaurantLicence.restaurant_id);
                command.Parameters.AddWithValue("@license_code", restaurantLicence.license_code ?? "");
                command.Parameters.AddWithValue("@is_installed", restaurantLicence.is_installed);
                command.Parameters.AddWithValue("@date_installed", restaurantLicence.date_installed);
                command.Parameters.AddWithValue("@hardware_info", restaurantLicence.hardware_info ?? "");

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

        internal string UpdateLicenceKey(LicenceKey aLicenceKey)
        {
            int lastId = 0;
            try
            {




                //string query = String.Format("UPDATE [rcs_restaurant_license] SET restaurant_id={0}, license_code='{1}',is_installed={2}, date_installed='{3}'," +
                //                             "hardware_info='{4}',app_info='{5}',online_info='{6}'", aLicenceKey.restaurant_id, aLicenceKey.license_code, aLicenceKey.is_installed, aLicenceKey.date_installed,
                //                             aLicenceKey.hardware_info,
                //                             aLicenceKey.app_info,
                //                             aLicenceKey.online_info);


                Query =
                    String.Format(
                        "UPDATE rcs_restaurant_license SET restaurant_id=@restaurant_id, license_code=@license_code,is_installed=@is_installed," +
                        " date_installed=@date_installed," +
                        "hardware_info=@hardware_info");

                try
                {


                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@restaurant_id", aLicenceKey.restaurant_id);
                    command.Parameters.AddWithValue("@license_code", aLicenceKey.license_code);
                    command.Parameters.AddWithValue("@is_installed", aLicenceKey.is_installed);
                    command.Parameters.AddWithValue("@date_installed", aLicenceKey.date_installed);
                    command.Parameters.AddWithValue("@hardware_info", aLicenceKey.hardware_info);

                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }

            catch (Exception ex)
            {

            }

            return (int)lastId > 0 ? "Yes" : "No";
        }

        internal LicenceKey GetRestaurantLicence(int restaurantId, string systemKey)
        {
            // AND usertype='{2}'
            LicenceKey restaurantLicence = new LicenceKey();
            string[] splitstring = systemKey.Split('#');
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_restaurant_license where restaurant_id=@restaurant_id and  Left(rcs_restaurant_license.hardware_info,LENGTH(rcs_restaurant_license.hardware_info) - LENGTH(SUBSTRING_INDEX(rcs_restaurant_license.hardware_info,'_NAME',-1))-5)=@hardware_info" +
                                  " OR rcs_restaurant_license.hardware_info LIKE @processId");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@restaurant_id", restaurantId);
            command.Parameters.AddWithValue("@hardware_info", systemKey);
            command.Parameters.AddWithValue("@processId", "%" + splitstring[1] + "%");
           // command.Parameters.AddWithValue("@HDD",  "%"+splitstring[2]+"%");
            
            Reader = ReaderMethod(Reader, command);
            // dataRow = command.ExecuteReader();
            DT.Load(Reader);
            if (DT.Rows.Count > 0)
            {
                restaurantLicence = new LicenceKeyReader().ReaderToReadrcs_restaurant_license(DT, 0);
            }
         
            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);



            return restaurantLicence;
        }

        internal LicenceKey GetRestaurantLicenceByLicence(int restaurantId)
        {
            // AND usertype='{2}'
            LicenceKey restaurantLicence = new LicenceKey();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_restaurant_license where restaurant_id={0} ;", restaurantId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            if (DT.Rows.Count > 0)
            {
                restaurantLicence = new LicenceKeyReader().ReaderToReadrcs_restaurant_license(DT, 0);
            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return restaurantLicence;
        }


        internal LicenceKey GetRestaurantLicenceBykey(int restaurantId, string systemKey)
        {
            // AND usertype='{2}'
            LicenceKey restaurantLicence = new LicenceKey();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_restaurant_license where restaurant_id=@restaurant_id and license_code=@hardware_info");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@restaurant_id", restaurantId);
            command.Parameters.AddWithValue("@hardware_info", systemKey);

            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            DT.Load(Reader);
            if (DT.Rows.Count > 0)
            {
                restaurantLicence = new LicenceKeyReader().ReaderToReadrcs_restaurant_license(DT, 0);
            }


            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return restaurantLicence;
        }

        internal string UpdateLicenceByKey(LicenceKey aLicenceKey, string key)
        {
            int lastId = 0;
            
                Query =
                    String.Format(
                        "UPDATE rcs_restaurant_license SET restaurant_id=@restaurant_id,is_installed=@is_installed," +
                        " date_installed=@date_installed," +
                        "hardware_info=@hardware_info where  license_code=@license_code");

                try
                {


                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@restaurant_id", aLicenceKey.restaurant_id);
                    command.Parameters.AddWithValue("@license_code", key);
                    command.Parameters.AddWithValue("@is_installed", aLicenceKey.is_installed);
                    command.Parameters.AddWithValue("@date_installed", aLicenceKey.date_installed);
                    command.Parameters.AddWithValue("@hardware_info", aLicenceKey.hardware_info);

                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

 

            return (int)lastId > 0 ? "Yes" : "No";
        }
    }
}
