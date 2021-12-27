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
    public class GlobalUrlDAO : GatewayConnection
    {
        string currentUrl = Properties.Settings.Default.backend;
        public int UpdateUrlsForSetting(LicenceKey settingUrl)
        {

            int lastId = 0;
            try
            {

                Query =
                    String.Format(
                        "UPDATE rcs_restaurant_license SET IsonlineOrderCheck=@IsonlineOrderCheck,ViewPage=@ViewPage,IstillEnable=@IstillEnable,IsReservationCheck=@IsReservationCheck,IsCardVisible=@IsCardVisible," +
                        "IsCallerId=@IsCallerId  WHERE license_code=@license_code");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@IsonlineOrderCheck", settingUrl.onlineConnect);
                    command.Parameters.AddWithValue("@ViewPage", settingUrl.viewpage);
                    command.Parameters.AddWithValue("@IstillEnable", settingUrl.till);
                    command.Parameters.AddWithValue("@IsReservationCheck", settingUrl.IsReservationCheck);
                    command.Parameters.AddWithValue("@IsCardVisible", settingUrl.IsCardVisible);
                    command.Parameters.AddWithValue("@IsCallerId", settingUrl.IsCallerId);
                    command.Parameters.AddWithValue("@license_code", settingUrl.license_code);
                    lastId = command.ExecuteNonQuery();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return lastId;
        }
      
        internal GlobalUrl GetUrls()
        {

            currentUrl = Properties.Settings.Default.backend;
            GlobalUrl url = new GlobalUrl();
            url.AcceptUrl = currentUrl;
            Query = String.Format("SELECT * FROM UrlSetup;");
            try
            {

                command = CommandMethod(command);

                Reader = ReaderMethod(Reader, command);

                // dataRow = command.ExecuteReader();
                while (Reader.Read())
                {

                    url = ReaderToReadGlobalUrl(Reader);

                }



            }
            catch (Exception ex)
            {
                url.fontFamily = "Century Gothic";
                url.fontStyle = "Normal";
                url.fontSize = "11";
                url.Cursur = 1;
                url.AcceptUrl = currentUrl;
            }



            if (url.fontFamily == null || url.fontFamily.Length <= 0)
            {
                url.fontFamily = "Century Gothic";
                url.fontStyle = "Normal";
                url.fontSize = "11";

                url.Cursur = 1;
            }
            if (url.AcceptUrl.Length <= 0)
            {
                url.AcceptUrl = currentUrl;
            }



            if (url.fontFamily.Length <= 0)
            {
                url.fontFamily = "Century Gothic";
                url.fontStyle = "Normal";
                url.fontSize = "11";
                url.AcceptUrl = currentUrl;
                url.Cursur = 1;

            }
            return url;
        }


        private GlobalUrl ReaderToReadGlobalUrl(IDataReader oReader)
        {
            GlobalUrl url = new GlobalUrl();
            if (oReader["Id"] != DBNull.Value)
            {
                url.Id = Convert.ToInt32(oReader["Id"]);
            }
            if (oReader["Accept"] != DBNull.Value)
            {
                url.AcceptUrl = Properties.Settings.Default.backend;
            }
            if (oReader["Reject"] != DBNull.Value)
            {
                url.RejectUrl = Convert.ToString(oReader["Reject"]);
            }
            if (oReader["OrderSyn"] != DBNull.Value)
            {
                url.OrderSyn = Convert.ToString(oReader["OrderSyn"]);
            }
            if (oReader["fontSize"] != DBNull.Value)
            {
                url.fontSize = Convert.ToString(oReader["fontSize"]);
            }
            if (oReader["fontStyle"] != DBNull.Value)
            {
                url.fontStyle = Convert.ToString(oReader["fontStyle"]);
            }
            if (oReader["fontFamily"] != DBNull.Value)
            {
                url.fontFamily = Convert.ToString(oReader["fontFamily"]);
            }
            if (oReader["PrinterName"] != DBNull.Value)
            {
                url.PrinterName = Convert.ToString(oReader["PrinterName"]);
            }
            if (oReader["Cursur"] != DBNull.Value)
            {
                url.Cursur = Convert.ToInt32(oReader["Cursur"]);
            }
            if (oReader["Keyboard"] != DBNull.Value)
            {
                url.Keyboard = Convert.ToInt32(oReader["Keyboard"]);
            }



            return url;
        }

        internal int InsertUrls(GlobalUrl url)
        {
            int lastId = 0;


            Query = String.Format("INSERT INTO UrlSetup (Accept,Reject,OrderSyn,fontSize,fontStyle,fontFamily,PrinterName,Cursur,Keyboard)" +
                " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8});", currentUrl, url.RejectUrl, url.OrderSyn, url.fontSize, url.fontStyle,
                url.fontFamily, url.PrinterName, url.Cursur, url.Keyboard);


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

        internal string UpdateUrls(GlobalUrl url)
        {
            int lastId = 0;
            try
            {

                   Query = String.Format("UPDATE [UrlSetup] SET Accept='{1}',Reject='{2}',OrderSyn='{3}',fontSize='{4}',fontStyle='{5}'," +
                                                 "fontFamily='{6}',PrinterName='{7}',Cursur={8},Keyboard={9} WHERE Id={0}",
                        url.Id, currentUrl, url.RejectUrl, url.OrderSyn, url.fontSize, url.fontStyle, url.fontFamily, url.PrinterName, url.Cursur, url.Keyboard);

                 
                        try
                        {
                            command = CommandMethod(command);

                            lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                     




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
