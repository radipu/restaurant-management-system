using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Preview;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlCustomerDAO : MySqlGatewayServerConnection
    {
        CustomerReader _customerReader = new CustomerReader();
        internal int InsertRestaurantCustomer(RestaurantUsers aRestaurantUser)
        {
            long lastId = 0;


            Query =
                String.Format(
                    "INSERT INTO rcs_users (usertype,firstname ,lastname ,email ,username,password," +
                    "manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,status,activation_key," +
                    " logged_in ,mac_address,last_activity ,can_manage,use_java,check_online_order,gcm_reg_id,full_address, is_update) " +
                    "VALUES (@usertype,@firstname,@lastname,@email,@username,@password" +
                    ",@manage_password,@house,@address,@homephone,@workphone,@mobilephone,@city,@country,@postcode,@status,@activation_key" +
                    ",@logged_in,@mac_address,@last_activity,@can_manage,@use_java,@check_online_order,@gcm_reg_id,@full_address,@is_update)");


            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@usertype", aRestaurantUser.Usertype ?? "");
                command.Parameters.AddWithValue("@firstname", aRestaurantUser.Firstname ?? "");
                command.Parameters.AddWithValue("@lastname", aRestaurantUser.Lastname ?? "");
                command.Parameters.AddWithValue("@email", aRestaurantUser.Email ?? "");
                command.Parameters.AddWithValue("@username", aRestaurantUser.Username ?? "");
                command.Parameters.AddWithValue("@password", aRestaurantUser.Password ?? "");
                command.Parameters.AddWithValue("@manage_password", aRestaurantUser.ManagePassword ?? "");
                command.Parameters.AddWithValue("@house", aRestaurantUser.House ?? "");
                command.Parameters.AddWithValue("@address", aRestaurantUser.Address ?? "");
                command.Parameters.AddWithValue("@homephone", aRestaurantUser.Homephone ?? "");
                command.Parameters.AddWithValue("@workphone", aRestaurantUser.Workphone ?? "");
                command.Parameters.AddWithValue("@mobilephone", aRestaurantUser.Mobilephone ?? "");
                command.Parameters.AddWithValue("@city", aRestaurantUser.City ?? "");
                command.Parameters.AddWithValue("@country", aRestaurantUser.County ?? "");
                command.Parameters.AddWithValue("@postcode", aRestaurantUser.Postcode ?? "");
                command.Parameters.AddWithValue("@status", aRestaurantUser.Status ?? "");
                command.Parameters.AddWithValue("@activation_key", aRestaurantUser.ActivationKey ?? "");
                command.Parameters.AddWithValue("@logged_in", aRestaurantUser.LoggedIn);
                command.Parameters.AddWithValue("@mac_address", aRestaurantUser.MacAddress ?? "");
                command.Parameters.AddWithValue("@last_activity", DateTime.Now);
                command.Parameters.AddWithValue("@can_manage", aRestaurantUser.CanManage);
                command.Parameters.AddWithValue("@use_java", aRestaurantUser.UseJava);
                command.Parameters.AddWithValue("@check_online_order", aRestaurantUser.CheckOnlineOrder);
                command.Parameters.AddWithValue("@gcm_reg_id", aRestaurantUser.GcmRegId ?? "");
                command.Parameters.AddWithValue("@full_address", aRestaurantUser.FullAddress ?? "");
                command.Parameters.AddWithValue("@is_update", aRestaurantUser.IsUpdate);




                int id = command.ExecuteNonQuery();

                Query = String.Format("select max(id) from rcs_users");
                command = CommandMethod(command);


                lastId = (long)Convert.ToUInt64(command.ExecuteScalar());



                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);



            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return (int)lastId;
        }
         
        internal int InsertNewCustomer(RestaurantUsers aRestaurantUser)
        {
            long lastId = 0;
            Query =
                String.Format(
                    "INSERT INTO rcs_users (id,usertype,firstname ,lastname ,email ,username,password," +
                    "manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,status,activation_key," +
                    " logged_in ,mac_address,last_activity ,can_manage,use_java,check_online_order,gcm_reg_id,full_address, is_update) " +
                    "VALUES (@id,@usertype,@firstname,@lastname,@email,@username,@password" +
                    ",@manage_password,@house,@address,@homephone,@workphone,@mobilephone,@city,@country,@postcode,@status,@activation_key" +
                    ",@logged_in,@mac_address,@last_activity,@can_manage,@use_java,@check_online_order,@gcm_reg_id,@full_address,@is_update)");


            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", aRestaurantUser.Id);
                command.Parameters.AddWithValue("@usertype", aRestaurantUser.Usertype ?? "");
                command.Parameters.AddWithValue("@firstname", aRestaurantUser.Firstname ?? "");
                command.Parameters.AddWithValue("@lastname", aRestaurantUser.Lastname ?? "");
                command.Parameters.AddWithValue("@email", aRestaurantUser.Email ?? "");
                command.Parameters.AddWithValue("@username", aRestaurantUser.Username ?? "");
                command.Parameters.AddWithValue("@password", aRestaurantUser.Password ?? "");
                command.Parameters.AddWithValue("@manage_password", aRestaurantUser.ManagePassword ?? "");
                command.Parameters.AddWithValue("@house", aRestaurantUser.House ?? "");
                command.Parameters.AddWithValue("@address", aRestaurantUser.Address ?? "");
                command.Parameters.AddWithValue("@homephone", aRestaurantUser.Homephone ?? "");
                command.Parameters.AddWithValue("@workphone", aRestaurantUser.Workphone ?? "");
                command.Parameters.AddWithValue("@mobilephone", aRestaurantUser.Mobilephone ?? "");
                command.Parameters.AddWithValue("@city", aRestaurantUser.City ?? "");
                command.Parameters.AddWithValue("@country", aRestaurantUser.County ?? "");
                command.Parameters.AddWithValue("@postcode", aRestaurantUser.Postcode ?? "");
                command.Parameters.AddWithValue("@status", aRestaurantUser.Status ?? "");
                command.Parameters.AddWithValue("@activation_key", aRestaurantUser.ActivationKey ?? "");
                command.Parameters.AddWithValue("@logged_in", aRestaurantUser.LoggedIn);
                command.Parameters.AddWithValue("@mac_address", aRestaurantUser.MacAddress ?? "");
                command.Parameters.AddWithValue("@last_activity", DateTime.Now);
                command.Parameters.AddWithValue("@can_manage", aRestaurantUser.CanManage);
                command.Parameters.AddWithValue("@use_java", aRestaurantUser.UseJava);
                command.Parameters.AddWithValue("@check_online_order", aRestaurantUser.CheckOnlineOrder);
                command.Parameters.AddWithValue("@gcm_reg_id", aRestaurantUser.GcmRegId ?? "");
                command.Parameters.AddWithValue("@full_address", aRestaurantUser.FullAddress ?? "");
                command.Parameters.AddWithValue("@is_update", aRestaurantUser.IsUpdate);
                 
                int id = command.ExecuteNonQuery();

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);



            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return aRestaurantUser.Id; 
        }

        internal bool DeleteItemsAndPackage(int orderId)
        {
            int lastId = 0;

            Query = String.Format("delete from rcs_order_item where id=@id");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", orderId);
                lastId = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }

            Query = String.Format("delete from rcs_order_package where id=@id");

            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", orderId);
                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }


            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return (int)lastId > 0 ? true : false;
        }
        public RestaurantUsers GetRestaurantCustomerByHomePhone(string phoneNumber, string phnTrack)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT`rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity`" +
                                  " FROM rcs_users  as  rcs_users where " +
                                         "(rcs_users.firstname LIKE @phonenumber OR rcs_users.lastname LIKE  @phonenumber OR rcs_users.homephone LIKE  @phonenumber OR rcs_users.workphone LIKE  @phonenumber OR rcs_users.mobilephone LIKE  @phonenumber) AND `rcs_users`.`usertype`='user'");
            // string query = String.Format("SELECT * FROM rcs_users where "+phnTrack+"='{0}';", phoneNumber); //   REPLACE(rcs_users.postcode, ' ', '') LIKE '%" + phoneNumber + "%' OR CONCAT(rcs_users.house, ' ',rcs_users.address) LIKE '%" + phoneNumber + "%' OR
            // dataRow = command.ExecuteReader();

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@phonenumber",phoneNumber);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            if (DT.Rows.Count > 0)
            {
                aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, 0);

            }



            return aRestaurantUser;
        }

        public List<RestaurantUsers> GetRestaurantAllCusttomer()
        {

            List<RestaurantUsers> users = new List<RestaurantUsers>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity` FROM rcs_users where `rcs_users`.`usertype`='user'");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            int rowCount = 0;

            while (DT.Rows.Count > rowCount)
            {


                RestaurantUsers aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, rowCount);
                users.Add(aRestaurantUser);
            }
            return users;
        }

        internal RestaurantUsers GetResturantCustomerByCustomerId(int customerId)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity` FROM rcs_users where id=@customerId AND `rcs_users`.`usertype`='user'");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@customerId", customerId);



            try
            {
                Reader = ReaderMethod(Reader, command);

                DT.Load(Reader);
                if (DT.Rows.Count > 0)
                {
                    aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, 0);

                }
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return aRestaurantUser;
        }

        internal DataTable GetResturantCustomerByCustomerIdDataTableSync(int customerId)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity` FROM rcs_users where id=@customerId");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@customerId", customerId);



            try
            {
                Reader = ReaderMethod(Reader, command);

                DT.Load(Reader);
                CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return DT;
        }

        public DataTable GetResturantCustomerDataTableByUpdateStatus(int isUpdate, int limit = 10)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity` FROM rcs_users where is_update=@isUpdate LIMIT @limit");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@isUpdate", isUpdate);
            command.Parameters.AddWithValue("@limit", limit);



            try
            {
                Reader = ReaderMethod(Reader, command);

                DT.Load(Reader);
                CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return DT;
        }

        internal List<RestaurantUsers> GetResturantCustomerByIsUpdate(int isUpdate)
        {
            List<RestaurantUsers> aRestaurantUsers = new List<RestaurantUsers>();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();



            Query = String.Format("select `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity` from `rcs_users` where is_update=@is_update AND (homephone !='' OR mobilephone !='')"); // AND homephone !='' AND workphone !='' AND mobilephone !=''
            command = CommandMethod(command);
            command.Parameters.AddWithValue("@is_update", isUpdate);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {
                RestaurantUsers aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, rowCount);
                aRestaurantUsers.Add(aRestaurantUser);
                rowCount++;
            }




            return aRestaurantUsers;
        }


        internal int UpdateRestaurantCustomer(RestaurantUsers aRestaurantUser)
        {
            long lastId = 0;


            Query =
                String.Format(
                    "UPDATE rcs_users SET firstname = @firstname ,lastname = @lastname,email = @email," +
                    "house = @house,homephone = @homephone," +
                    " mobilephone = @mobilephone ,city = @city,county =@country,postcode =@postcode," +
                    "full_address = @full_address," +
                    "address=@address, is_update=@is_update WHERE Id=@id");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@firstname", aRestaurantUser.Firstname ?? "");
                command.Parameters.AddWithValue("@lastname", aRestaurantUser.Lastname ?? "");
                command.Parameters.AddWithValue("@email", aRestaurantUser.Email ?? "");
                command.Parameters.AddWithValue("@house", aRestaurantUser.House ?? "");
                command.Parameters.AddWithValue("@homephone", aRestaurantUser.Homephone ?? "");
                command.Parameters.AddWithValue("@mobilephone", aRestaurantUser.Mobilephone ?? "");
                command.Parameters.AddWithValue("@city", aRestaurantUser.City ?? "");
                command.Parameters.AddWithValue("@country", aRestaurantUser.County ?? "");
                command.Parameters.AddWithValue("@postcode", aRestaurantUser.Postcode ?? "");
                command.Parameters.AddWithValue("@full_address", aRestaurantUser.FullAddress ?? "");
                command.Parameters.AddWithValue("@address", aRestaurantUser.Address ?? "");
                command.Parameters.AddWithValue("@is_update", aRestaurantUser.IsUpdate);
                command.Parameters.AddWithValue("@id", aRestaurantUser.Id);

                command.ExecuteNonQuery();
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return aRestaurantUser.Id;
        }
        public List<RestaurantUsers> GetRestaurantAllCustomerForShow(string espression)
        {
            List<RestaurantUsers> users = new List<RestaurantUsers>();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("select `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity` from rcs_users as rcs_users where usertype='user'  AND (rcs_users.firstname LIKE '%{0}%' OR rcs_users.lastname LIKE '%{0}%' " +
                                         "OR replace( rcs_users.postcode,'',' ') LIKE  '%{0}%'  OR CONCAT(rcs_users.house,CONCAT(' ', rcs_users.address))  LIKE  '%{0}%' " +
                                         " OR rcs_users.homephone LIKE '%{0}%' OR rcs_users.full_address LIKE '%{0}%' OR rcs_users.workphone LIKE '%{0}%' OR rcs_users.mobilephone LIKE '%{0}%') AND `rcs_users`.`usertype`='user' limit 9;", espression);
            Adapter = GetAdapter(Adapter);
            // Adapter.SelectCommand.Parameters.AddWithValue("@espression", "%" + espression + "%");
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {
                RestaurantUsers aRestaurantUser = _customerReader.ReaderToReadRestaurantUsersForShow(DT, rowCount);
                users.Add(aRestaurantUser);
                rowCount++;
            }

            return users;

        }



        public List<CustomerOrderItems> GetCustomerItems(int customerId)
        {
            List<CustomerOrderItems> items = new List<CustomerOrderItems>();
            return items;
        }
        public string DeleteCustomerOrderedItems(int customerId)
        {
            int lastId = 0;

            Query = String.Format("delete from rcs_customer_order_items where customer_id=@customerId");

            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@customerId", customerId);
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




        public List<MemberShipType> GetMembershipsByResId(int resId)
        {

            List<MemberShipType> memberShipTypes = new List<MemberShipType>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,type_name,restaurant_id FROM rcs_membership_type where restaurant_id=@resId");

            command = CommandMethod(command);

            command.Parameters.AddWithValue(@"resId", resId);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            int rowCount = 0;
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            // dataRow = command.ExecuteReader();
            while (DT.Rows.Count > rowCount)
            {
                MemberShipType memberShipType = _customerReader.ReaderToReadMemberships(DT, rowCount);
                memberShipTypes.Add(memberShipType);
                rowCount++;
            }

            return memberShipTypes;
        }








        internal int InsertMembership(MemberShips mem)
        {

            long lastId = 0;

            Query =
                String.Format(
                    "INSERT INTO rcs_membership (membership_type_id,membership_id,membership_number,expire_date)" +
                    "  VALUES(@membership_type_id,@membership_id,@membership_number,@expire_date)");



            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@membership_type_id", mem.MembershipTypeId);
                command.Parameters.AddWithValue("@membership_id", mem.MembershipTypeId);
                command.Parameters.AddWithValue("@membership_number", "0");
                command.Parameters.AddWithValue("@expire_date", "31-12-2021");
                int id = command.ExecuteNonQuery();

                Query = String.Format("select max(id) from rcs_membership");
                command = CommandMethod(command);

                lastId = (long)Convert.ToUInt64(command.ExecuteScalar());

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);




            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return (int)lastId;
        }


        public int UpdateMembership(int memberShipId, MemberShips mem)
        {
            
            Query =
                String.Format(
                    "UPDATE rcs_membership " +
                    "SET membership_type_id = @membership_type_id, " +
                    "membership_id = @membership_id, " +
                    "membership_number = @membership_number," +
                    "expire_date = @expire_date WHERE id = @id");


            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@membership_type_id", mem.MembershipTypeId);
                command.Parameters.AddWithValue("@membership_id", mem.MembershipTypeId);
                command.Parameters.AddWithValue("@membership_number", "0");
                command.Parameters.AddWithValue("@expire_date", "31-12-2021");
                command.Parameters.AddWithValue("@id", memberShipId);
                int id = command.ExecuteNonQuery();

               
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return memberShipId;
        }





        internal MemberShips GetMemberShips(int userId, int MemberShipTypeID)
        {

            MemberShips mem = new MemberShips();
            mem.Id = 0;
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_membership where membership_id =@membership_id and membership_type_id =@membership_type_id");
            //, userId, MemberShipTypeID);

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@membership_id", mem.MembershipId);
            command.Parameters.AddWithValue("@membership_type_id", mem.MembershipTypeId);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            if (DT.Rows.Count > 0)
            {
                mem = _customerReader.ReaderToReadMemberShips(DT, 0);

            }

            return mem;

        }



        internal MemberShips GetMemberShipByUserID(int userId, int resId)
        {
            MemberShips mem = new MemberShips();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT rcs_membership.*  FROM rcs_membership left join rcs_membership_type ON  rcs_membership.membership_type_id =rcs_membership_type.id  where rcs_membership.membership_id =@membership_id and rcs_membership_type.restaurant_id=@restaurant_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@membership_id", userId);
            command.Parameters.AddWithValue("@restaurant_id", resId);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            if (DT.Rows.Count > 0)
            {
                mem = _customerReader.ReaderToReadMemberShips(DT, 0);


            }

            return mem;


        }
        internal DataTable GetMemberShipByUserIdDataTableByMemberShips(int userId, int resId)
        {

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT rcs_membership.*  FROM rcs_membership left join rcs_membership_type ON  rcs_membership.membership_type_id =rcs_membership_type.id  where rcs_membership.membership_id =@membership_id and rcs_membership_type.restaurant_id=@restaurant_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@membership_id", userId);
            command.Parameters.AddWithValue("@restaurant_id", resId);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return DT;


        }
        public RestaurantUsers GetCustomerByPhone(string phoneNumber)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //File.AppendAllText("Config/log.txt", " \n\n number :: " + phoneNumber + "\n\n");

            try
            {
                //  SQLiteDataAdapter DB;



                DataSet DS = new DataSet();



                DataTable DT = new DataTable();



                Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,`password`,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,`status`,activation_key,logged_in,mac_address," +
                                      "date_format(rcs_users.last_activity,rcs_users.last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update FROM rcs_users where (homephone LIKE  '"+ phoneNumber + "'  OR mobilephone LIKE  '"+ phoneNumber + "') AND usertype='user'");

                File.AppendAllText("Config/log.txt", " \n\n number :: " + Query.ToString() + "\n\n");

                command = CommandMethod(command);
            //    command.Parameters.AddWithValue("@phonenumber", phoneNumber);
                Reader = ReaderMethod(Reader, command);
                DT.Load(Reader);

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


                if (DT.Rows.Count > 0)
                {
                    aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, 0);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            return aRestaurantUser;
        }

        public RestaurantUsers IsExistCustomer(RestaurantUsers aRestaurantUser)
        {
            if (aRestaurantUser.Mobilephone != null && aRestaurantUser.Mobilephone != "")
            {
                Query = String.Format("SELECT `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity`" +
         "FROM  rcs_users where usertype='user' AND mobilephone=@mobilephone");
                //, userId, MemberShipTypeID);
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@mobilephone", aRestaurantUser.Mobilephone); 
            }
            if (aRestaurantUser.Homephone != null && aRestaurantUser.Homephone != "")
            {
                Query = String.Format("SELECT `rcs_users`.`id` AS `id`,`rcs_users`.`usertype` AS `usertype`,`rcs_users`.`firstname` AS `firstname`,`rcs_users`.`lastname` AS `lastname`,`rcs_users`.`email` AS `email`,`rcs_users`.`username` AS `username`,`rcs_users`.`password` AS `password`,`rcs_users`.`manage_password` AS `manage_password`,`rcs_users`.`house` AS `house`,`rcs_users`.`address` AS `address`,`rcs_users`.`homephone` AS `homephone`,`rcs_users`.`workphone` AS `workphone`,`rcs_users`.`mobilephone` AS `mobilephone`,`rcs_users`.`city` AS `city`,`rcs_users`.`county` AS `county`,`rcs_users`.`postcode` AS `postcode`,`rcs_users`.`full_address` AS `full_address`,`rcs_users`.`status` AS `status`,`rcs_users`.`activation_key` AS `activation_key`,`rcs_users`.`logged_in` AS `logged_in`,`rcs_users`.`mac_address` AS `mac_address`,`rcs_users`.`can_manage` AS `can_manage`,`rcs_users`.`use_java` AS `use_java`,`rcs_users`.`check_online_order` AS `check_online_order`,`rcs_users`.`gcm_reg_id` AS `gcm_reg_id`,`rcs_users`.`is_opt_out` AS `is_opt_out`,`rcs_users`.`is_update` AS `is_update`,date_format(`rcs_users`.`last_activity`,`rcs_users`.`last_activity`) AS `last_activity`" +
         "FROM  rcs_users where usertype='user' AND homephone=@homephone");
                //, userId, MemberShipTypeID);
                command = CommandMethod(command); 
                command.Parameters.AddWithValue("@homephone", aRestaurantUser.Homephone);

            }

         

            DataTable dt = new DataTable();
            Reader = ReaderMethod(Reader, command);
            dt.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            if (dt.Rows.Count  > 0)
            {
                aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(dt, 0);
                return aRestaurantUser;
            }
            return null;
        }

        public List<CouponCode> GetCustomerCoupon()
        {
            List<CouponCode> tempCouponCodeList = new List<CouponCode>();


            Query = "Select * from rcs_coupon where restaurant_id='" + GlobalSetting.RestaurantInformation.Id + "'";
            command = CommandMethod(command);
            int id = 0;
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                id++;
                tempCouponCodeList.Add(new CouponCode()
                {

                    Id = id,
                    Code = Reader["code"].ToString(),
                    Discount = Convert.ToDouble(Reader["discount"]),
                    Expiring = Convert.ToDateTime(Reader["expire_date"]),
                    Usage = Reader["use_type"].ToString(),
                    Message = Reader["message"].ToString(),
                    ServiceType = Reader["code"].ToString(),
                    Type = Reader["service_type"].ToString(),
                    Title = Reader["coupon_title"].ToString(),
                    Edit = Reader["id"].ToString(),
                    Remove = Reader["id"].ToString(),
                    Print = Reader["id"].ToString()



                });

            }
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return tempCouponCodeList;
        }

        public CouponCode GetCustomerCouponById(CouponCode CouponCode)
        {
            Query = "Select * from rcs_coupon where id='" + CouponCode.Id + "' and restaurant_id='" + GlobalSetting.RestaurantInformation.Id + "'";
            command = CommandMethod(command);

            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                CouponCode.Code = Reader["code"].ToString();
                CouponCode.Discount = Convert.ToDouble(Reader["discount"]);
                CouponCode.Expiring = Convert.ToDateTime(Reader["expire_date"]);
                CouponCode.Usage = Reader["use_type"].ToString();
                CouponCode.Message = Reader["message"].ToString();
                CouponCode.ServiceType = Reader["service_type"].ToString();
                CouponCode.Title = Reader["coupon_title"].ToString();
                CouponCode.Type = Reader["type"].ToString();
                CouponCode.MinAmount = Convert.ToDouble(Reader["min_amount"]);
            }
            return CouponCode;
        }

        public int CouponEdit(CouponCode couponCode)
        {
            try
            {
                Query =
              String.Format(
                  "UPDATE rcs_coupon SET coupon_title=@coupon_title,code=@code,service_type=@service_type,discount=@discount,`type`=@type,min_amount=@min_amount,expire_date=@expire_date,message=@message,use_type=@use_type where id=@id and restaurant_id=@restaurant_id");
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@coupon_title", couponCode.Title);
                command.Parameters.AddWithValue("@id", couponCode.Id);
                command.Parameters.AddWithValue("@restaurant_id", GlobalSetting.RestaurantInformation.Id);
                command.Parameters.AddWithValue("@code", couponCode.Code);
                command.Parameters.AddWithValue("@service_type", couponCode.ServiceType);
                command.Parameters.AddWithValue("@discount", couponCode.Discount);
                command.Parameters.AddWithValue("@type", couponCode.Type);
                command.Parameters.AddWithValue("@min_amount", couponCode.MinAmount);
                command.Parameters.AddWithValue("@expire_date", couponCode.Expiring);
                command.Parameters.AddWithValue("@message", couponCode.Message);
                command.Parameters.AddWithValue("@use_type", couponCode.Usage);
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return 0;}


        }

        public int CouponRemove(CouponCode couponCode)
        {
            try
            {
                Query =
                    String.Format("delete from rcs_coupon where restaurant_id=@restaurant_id and id=@id");
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", couponCode.Id);
                command.Parameters.AddWithValue("@restaurant_id", GlobalSetting.RestaurantInformation.Id);



                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public int CouponCreate(CouponCode couponCode)
        {
            try
            {



                command = CommandMethod(command);

                Query = "INSERT INTO rcs_coupon (special_instruction,by_recipe,get_recipe,free_item,`limit`,restaurant_id,coupon_title,code,service_type,discount,`type`,min_amount,expire_date,message,use_type) " +
                     "VALUES (@special_instruction,@by_recipe,@get_recipe,@free_item,@limit,@restaurant_id,@coupon_title,@code,@service_type,@discount,@type,@min_amount,@expire_date,@message,@use_type)";
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@restaurant_id", GlobalSetting.RestaurantInformation.Id);
                command.Parameters.AddWithValue("@coupon_title", couponCode.Title);
                command.Parameters.AddWithValue("@code", couponCode.Code);command.Parameters.AddWithValue("@service_type", couponCode.ServiceType);
                command.Parameters.AddWithValue("@discount", couponCode.Discount);
                command.Parameters.AddWithValue("@type", couponCode.Type);
                command.Parameters.AddWithValue("@min_amount", couponCode.MinAmount);
                command.Parameters.AddWithValue("@expire_date", DateTime.Now.Date);
                command.Parameters.AddWithValue("@message", couponCode.Message);
                command.Parameters.AddWithValue("@use_type", couponCode.Usage);
                command.Parameters.AddWithValue("@special_instruction", " ");
                command.Parameters.AddWithValue("@by_recipe", 0);command.Parameters.AddWithValue("@get_recipe", " ");
                command.Parameters.AddWithValue("@free_item", " ");
                command.Parameters.AddWithValue("@limit", 0);


                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show(message);
                return 0;
            }
        }


        private RestaurantUsers ReaderToReadRestaurantUsers(IDataReader oReader)
        {
            RestaurantUsers arcs_users = new RestaurantUsers();
            if (oReader["id"] != DBNull.Value)
            {
                arcs_users.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["usertype"] != DBNull.Value)
            {
                arcs_users.Usertype = Convert.ToString(oReader["usertype"]);
            }
            if (oReader["firstname"] != DBNull.Value)
            {
                arcs_users.Firstname = Convert.ToString(oReader["firstname"]);
            }
            if (oReader["lastname"] != DBNull.Value)
            {
                arcs_users.Lastname = Convert.ToString(oReader["lastname"]);
            }
            if (oReader["email"] != DBNull.Value)
            {
                arcs_users.Email = Convert.ToString(oReader["email"]);
            }
            if (oReader["username"] != DBNull.Value)
            {
                arcs_users.Username = Convert.ToString(oReader["username"]);
            }
            if (oReader["password"] != DBNull.Value)
            {
                arcs_users.Password = Convert.ToString(oReader["password"]);
            }
            if (oReader["manage_password"] != DBNull.Value)
            {
                arcs_users.ManagePassword = Convert.ToString(oReader["manage_password"]);
            }
            if (oReader["house"] != DBNull.Value)
            {
                arcs_users.House = Convert.ToString(oReader["house"]);
            }
            if (oReader["address"] != DBNull.Value)
            {
                arcs_users.Address = Convert.ToString(oReader["address"]);
            }
            if (oReader["homephone"] != DBNull.Value)
            {
                arcs_users.Homephone = Convert.ToString(oReader["homephone"]);
            }
            if (oReader["workphone"] != DBNull.Value)
            {
                arcs_users.Workphone = Convert.ToString(oReader["workphone"]);
            }
            if (oReader["mobilephone"] != DBNull.Value)
            {
                arcs_users.Mobilephone = Convert.ToString(oReader["mobilephone"]);
            }
            if (oReader["city"] != DBNull.Value)
            {
                arcs_users.City = Convert.ToString(oReader["city"]);
            }
            if (oReader["county"] != DBNull.Value)
            {
                arcs_users.County = Convert.ToString(oReader["county"]);
            }
            if (oReader["postcode"] != DBNull.Value)
            {
                arcs_users.Postcode = Convert.ToString(oReader["postcode"]);
            }
            if (oReader["status"] != DBNull.Value)
            {
                arcs_users.Status = Convert.ToString(oReader["status"]);
            }
            if (oReader["activation_key"] != DBNull.Value)
            {
                arcs_users.ActivationKey = Convert.ToString(oReader["activation_key"]);
            }
            if (oReader["logged_in"] != DBNull.Value)
            {
                arcs_users.LoggedIn = Convert.ToInt64(oReader["logged_in"]);
            }
            if (oReader["mac_address"] != DBNull.Value)
            {
                arcs_users.MacAddress = Convert.ToString(oReader["mac_address"]);
            }
            //try
            //{

            //    if (oReader["last_activity"] != DBNull.Value)
            //    {
            //        arcs_users.LastActivity = Convert.ToDateTime(oReader["last_activity"]);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}

            if (oReader["can_manage"] != DBNull.Value)
            {
                arcs_users.CanManage = Convert.ToInt64(oReader["can_manage"]);
            }
            if (oReader["use_java"] != DBNull.Value)
            {
                arcs_users.UseJava = Convert.ToInt64(oReader["use_java"]);
            }
            if (oReader["check_online_order"] != DBNull.Value)
            {
                arcs_users.CheckOnlineOrder = Convert.ToInt64(oReader["check_online_order"]);
            }
            if (oReader["gcm_reg_id"] != DBNull.Value)
            {
                arcs_users.GcmRegId = Convert.ToString(oReader["gcm_reg_id"]);
            }
            if (oReader["full_address"] != DBNull.Value)
            {
                arcs_users.FullAddress = Convert.ToString(oReader["full_address"]);
            }

            if (arcs_users.FullAddress == "0")
            {
                arcs_users.FullAddress = "";
            }

            arcs_users.Name = arcs_users.Firstname + " " + arcs_users.Lastname;
            return arcs_users;
        }

   

        internal RestaurantUsers GetUserByUserId(int userId)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users where id={0};", userId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read())
            {

                aRestaurantUser = ReaderToReadRestaurantUsers(Reader);

            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return aRestaurantUser;
        }
    }

}
