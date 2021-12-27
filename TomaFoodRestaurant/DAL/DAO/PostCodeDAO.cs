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
    public class PostCodeDAO : GatewayConnection
    {
        public PostCodeModel GetPostCodeInformationByPostCode(string postCode)
        {


            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_postcode  where Replace(postcode,' ','')='{0}';", postCode);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            PostCodeModel aPostCodeModel = new PostCodeModel();
            while (Reader.Read())
            {
                aPostCodeModel = ReaderToReadPostcode(Reader);
            }



            return aPostCodeModel;
        }
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



            return aPostCodeModelList;
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
                        "UPDATE [rcs_postcode] SET [formatted_address] = '{0}', [district]='{2}',[ward]='{3}' WHERE postcode='{1}';",
                        aRestaurantUsers.FullAddress, aRestaurantUsers.Postcode, aRestaurantUsers.City,
                        aRestaurantUsers.Address);


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

                Query = String.Format("UPDATE [rcs_postcode] SET [formatted_address] = '{0}' WHERE postcode='{1}';", aPostCodeModel.Formatted_address, aPostCodeModel.Postcode);


                try
                {
                    command = CommandMethod(command);


                    lastId = command.ExecuteNonQuery();

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
    }
}
