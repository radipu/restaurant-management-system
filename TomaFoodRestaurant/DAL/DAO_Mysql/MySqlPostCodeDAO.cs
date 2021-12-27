using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlPostCodeDAO : MySqlGatewayConnection
    {
        public List<PostCodeModel> GetPostCodeInformation()
        {

            List<PostCodeModel> aPostCodeModelList = new List<PostCodeModel>();
            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_postcode;");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);


            while (Reader.Read())
            {
                PostCodeModel aPostCodeModel = new PostCodeModel();
                aPostCodeModel = ReaderToReadPostcode(Reader);
                aPostCodeModelList.Add(aPostCodeModel);
            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return aPostCodeModelList;
        }
        public PostCodeModel GetPostCodeInformationByPostCode(string postCode)
        {

          
            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_postcode  where Replace(postcode,' ','')='{0}';",postCode);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            PostCodeModel aPostCodeModel = new PostCodeModel();
            while (Reader.Read())
            {
                aPostCodeModel = ReaderToReadPostcode(Reader);
            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return aPostCodeModel;
        }
        private PostCodeModel ReaderToReadPostcode(IDataReader oReader)
        {
            PostCodeModel arcs_postcode = new PostCodeModel();
            if (oReader["postcode"] != DBNull.Value)
            {
                arcs_postcode.Postcode = Convert.ToString(oReader["postcode"]);
            }
            if (oReader["latitude"] != DBNull.Value)
            {
                arcs_postcode.Latitude = Convert.ToString(oReader["latitude"]);
            }
            if (oReader["longitude"] != DBNull.Value)
            {
                arcs_postcode.Longitude = Convert.ToString(oReader["longitude"]);
            }
            if (oReader["easting"] != DBNull.Value)
            {
                arcs_postcode.Easting = Convert.ToString(oReader["easting"]);
            }
            if (oReader["northing"] != DBNull.Value)
            {
                arcs_postcode.Northing = Convert.ToString(oReader["northing"]);
            }
            if (oReader["gridref"] != DBNull.Value)
            {
                arcs_postcode.Gridref = Convert.ToString(oReader["gridref"]);
            }
            if (oReader["county"] != DBNull.Value)
            {
                arcs_postcode.County = Convert.ToString(oReader["county"]);
            }
            if (oReader["district"] != DBNull.Value)
            {
                arcs_postcode.District = Convert.ToString(oReader["district"]);
            }
            if (oReader["ward"] != DBNull.Value)
            {
                arcs_postcode.Ward = Convert.ToString(oReader["ward"]);
            }
            if (oReader["updated"] != DBNull.Value)
            {
                arcs_postcode.Updated = Convert.ToInt64(oReader["updated"]);
            }
            if (oReader["formatted_address"] != DBNull.Value)
            {
                arcs_postcode.Formatted_address = Convert.ToString(oReader["formatted_address"]);
            }
            return arcs_postcode;
        }
        public void UpdatePostcode(RestaurantUsers aRestaurantUsers)
        {

            try
            {
                int lastId = 0;


                Query =
                    String.Format(
                        "UPDATE rcs_postcode SET formatted_address = @formatted_address, district=@district,ward=@ward WHERE postcode=@postcode;");


                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@formatted_address", aRestaurantUsers.FullAddress);
                    command.Parameters.AddWithValue("@district", aRestaurantUsers.Postcode);
                    command.Parameters.AddWithValue("@ward", aRestaurantUsers.City);
                    command.Parameters.AddWithValue("@postcode", aRestaurantUsers.Address);

                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


                // MessageBox.Show(item.ToString());

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateFormattedAddressInPostcode(PostCodeModel aPostCodeModel)
        {

            try
            {
                int lastId = 0;

                Query =
                    String.Format(
                        "UPDATE rcs_postcode SET formatted_address =@formatted_address WHERE REPLACE(postcode,' ','')=@postcode;");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@formatted_address", aPostCodeModel.Formatted_address);
                    command.Parameters.AddWithValue("@postcode", aPostCodeModel.Postcode);
                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                //  mytransaction.Commit();

                // MessageBox.Show(item.ToString());

            }
            catch (Exception)
            {

                throw;
            }

        }
        internal bool insertCustomerAddress(CustomerAddress address)
        { 
                int lastId = 0;

           
            Query =  String.Format("INSERT INTO `rcs_customer_address` (`customer_id`, `house_no`, `address`, `postcode`, `latitude`, `longitude`) VALUES (@customer_id,@house_no,@address,@postcode,@latitude, @longitude);");


                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@customer_id", address.CustomerId);
                    command.Parameters.AddWithValue("@house_no", address.HouseNumber);
                    command.Parameters.AddWithValue("@address", address.Address);
                    command.Parameters.AddWithValue("@postcode", address.PostCode);
                    command.Parameters.AddWithValue("@latitude", address.Latitude);
                    command.Parameters.AddWithValue("@longitude", address.Longitude);

                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {

                if(exception.Message.Contains("doesn't exist"))
                {
                    createAddressTable();
                }
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

            




            return true;
           
        }



        internal bool createAddressTable()
        {
            int lastId = 0;

            Query = String.Format("CREATE TABLE IF NOT EXISTS `rcs_customer_address` (`id` int(11) NOT NULL AUTO_INCREMENT,`customer_id` int(11) DEFAULT NULL,`house_no` varchar(50) DEFAULT NULL,`address` text,`town` varchar(50) DEFAULT NULL,`postcode` varchar(50) DEFAULT NULL,`latitude` varchar(50) DEFAULT NULL,`longitude` varchar(50) DEFAULT NULL,PRIMARY KEY(`id`)) ENGINE = InnoDB DEFAULT CHARSET = latin1; TRUNCATE `rcs_customer_address`;");



            try
            {
                command = CommandMethod(command);
                command.ExecuteNonQuery();
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }


            return true;
        }
        internal Postcode getAddressFromLocalDb(string address, string postcode)
        {
            Postcode text = new Postcode();

            try
            {

                Query = String.Format("SELECT * FROM `rcs_customer_address` where `address`='{0}' AND `postcode`='{1}';", address, postcode);

                command = CommandMethod(command);
                Reader = ReaderMethod(Reader, command);
                while (Reader.Read())
                {

                    text.Latitude = Convert.ToString(Reader["latitude"]);
                    text.Longitude = Convert.ToString(Reader["longitude"]);
                }

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            }
            catch (Exception exception)
            { 
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return text;
        }
    }
}
