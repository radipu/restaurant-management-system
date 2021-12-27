using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlUserLoginDAO : MySqlGatewayConnection
    {
        internal RestaurantUsers GetResturantUserByUserId(int userId)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users where id={0};", userId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);



            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                aRestaurantUser = ReaderToReadRestaurantUsers(Reader);

            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return aRestaurantUser;
        }

        public List<RestaurantUsers> GetRestaurantAllUser()
        {

            List<RestaurantUsers> users = new List<RestaurantUsers>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users where usertype LIKE '%restaurant_%';");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                RestaurantUsers aRestaurantUser = ReaderToReadRestaurantUsers(Reader);
                users.Add(aRestaurantUser);

            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);



            return users;
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

        internal RestaurantUsers GetRestaurantUserByPassword(string password)
        {
            // AND usertype='{2}'
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users where usertype IN ('admin', 'restaurant_admin') AND password='{0}';", password);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                aRestaurantUser = ReaderToReadRestaurantUsers(Reader);

            }


            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return aRestaurantUser;
        }

        internal RestaurantUsers GetRestaurantUserByUsernameAndPassword(string userName, string password)
        {
            // AND usertype='{2}'
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users where username='{0}' AND password='{1}';", userName, password);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                aRestaurantUser = ReaderToReadRestaurantUsers(Reader);

            }


            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return aRestaurantUser;
        }        

        internal List<RestaurantUsers> GetRestaurantUserByRestaurantId(int restaurantId)
        {
            List<RestaurantUsers> users = new List<RestaurantUsers>();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users LEFT JOIN rcs_restaurant_user ON rcs_restaurant_user.user_id = rcs_users.id " +
                                         "where rcs_restaurant_user.restaurant_id ={0} AND rcs_users.usertype LIKE '%restaurant_%' group by  rcs_users.id;", restaurantId);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                RestaurantUsers aRestaurantUser = ReaderToReadRestaurantUsers(Reader);
                users.Add(aRestaurantUser);
            }


            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            users = users.OrderBy(a => a.Username).ToList();
            return users;
        }


        internal string UpdateUserInfo(string userID, string password)
        {
            int lastId = 0;


            try
            {

                Query = String.Format("UPDATE rcs_users SET password='{1}' WHERE id={0}", userID, password);

                command = CommandMethod(command);

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
        internal RestaurantUsers GetUserByUserId(int userId)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users where id={0};", userId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                aRestaurantUser = ReaderToReadRestaurantUsers(Reader);

            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return aRestaurantUser;
        }


    }
}
