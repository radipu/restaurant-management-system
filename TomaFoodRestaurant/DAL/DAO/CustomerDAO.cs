using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{

    public class CustomerDAO : GatewayConnection
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
                // var dateTime = aRestaurantUser.LastActivity;
                command.Parameters.AddWithValue("@last_activity", DateTime.Now.ToString(TimeFormatCustom.Format));
                command.Parameters.AddWithValue("@can_manage", aRestaurantUser.CanManage);
                command.Parameters.AddWithValue("@use_java", aRestaurantUser.UseJava);
                command.Parameters.AddWithValue("@check_online_order", aRestaurantUser.CheckOnlineOrder);
                command.Parameters.AddWithValue("@gcm_reg_id", aRestaurantUser.GcmRegId ?? "");
                command.Parameters.AddWithValue("@full_address", aRestaurantUser.FullAddress ?? "");
                command.Parameters.AddWithValue("@is_update", aRestaurantUser.IsUpdate);




                int id = command.ExecuteNonQuery();

                Query = String.Format("select max(id) from rcs_users");
                command = CommandMethod(command);


                lastId = (long)command.ExecuteScalar();





            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return (int)lastId;
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



            return (int)lastId > 0 ? true : false;
        }
        public RestaurantUsers GetRestaurantCustomerByHomePhone(string phoneNumber, string phnTrack)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();
            //  SQLiteDataAdapter DB;
            try
            {

                DataSet DS = new DataSet();
                DataTable DT = new DataTable();

                Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,password,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,status,activation_key,logged_in,mac_address ," +
                "strftime(last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update FROM rcs_users  where " +
               " (rcs_users.firstname LIKE @phonenumber OR rcs_users.lastname LIKE  @phonenumber OR rcs_users.homephone LIKE  @phonenumber OR rcs_users.workphone LIKE  @phonenumber OR rcs_users.mobilephone LIKE  @phonenumber)");

                Adapter = GetAdapter(Adapter);
                Adapter.SelectCommand.Parameters.AddWithValue("@phonenumber", "%" + phoneNumber + "%");
                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];
                if (DT.Rows.Count > 0)
                {
                    aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, 0);

                }



                return aRestaurantUser;
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());

            }
            return aRestaurantUser;
        }

        public List<RestaurantUsers> GetRestaurantAllCusttomer()
        {

            List<RestaurantUsers> users = new List<RestaurantUsers>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_users");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);

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
            Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,password,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,status,activation_key,logged_in,mac_address," +
                "strftime(last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update  FROM  rcs_users where id=@customerId");

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
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return aRestaurantUser;
        }

        internal List<RestaurantUsers> GetResturantCustomerByIsUpdate(int isUpdate)
        {
            List<RestaurantUsers> aRestaurantUsers = new List<RestaurantUsers>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,password,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,status,activation_key,logged_in,mac_address," +
            "strftime(last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update  FROM  rcs_users where is_update=@is_update AND (homephone !='' OR mobilephone !='')");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@is_update", isUpdate);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);

            int rowCount = 0;
            // dataRow = command.ExecuteReader();
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

                lastId = command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return (int)lastId;
        }
        public List<RestaurantUsers> GetRestaurantAllCustomerForShow(string espression)
        {
            List<RestaurantUsers> users = new List<RestaurantUsers>();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,password,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,status,activation_key,logged_in,mac_address," +
                                  "strftime(last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update from rcs_users where usertype='user'  AND (rcs_users.firstname LIKE  @espression   OR rcs_users.lastname LIKE  @espression " +
                                  "OR replace( rcs_users.postcode,'',' ') LIKE    @espression    OR printf('%s %s', rcs_users.house, rcs_users.address)  LIKE  @espression " +
                                  " OR rcs_users.homephone LIKE  @espression   OR rcs_users.workphone LIKE  @espression   OR rcs_users.mobilephone LIKE  @espression   ) limit 8");
            Adapter = GetAdapter(Adapter);
            Adapter.SelectCommand.Parameters.AddWithValue("@espression", "%" + espression + "%");
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];

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
                command.Parameters.AddWithValue("@expire_date", Convert.ToDateTime("31-12-2021").ToString(TimeFormatCustom.Format));
                int id = command.ExecuteNonQuery();

                Query = String.Format("select max(id) from rcs_membership");
                command = CommandMethod(command);

                lastId = (long)command.ExecuteScalar();





            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }




            return (int)lastId;
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
            Query = String.Format("SELECT rcs_membership.* FROM rcs_membership left join rcs_membership_type" +
            " where rcs_membership.membership_id =@membership_id and rcs_membership_type.restaurant_id=@restaurant_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@membership_id", userId);
            command.Parameters.AddWithValue("@restaurant_id", resId);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);

            if (DT.Rows.Count > 0)
            {
                mem = _customerReader.ReaderToReadMemberShips(DT, 0);


            }

            return mem;


        }

        public RestaurantUsers GetCustomerByPhone(string phoneNumber)
        {
            RestaurantUsers aRestaurantUser = new RestaurantUsers();

            //  SQLiteDataAdapter DB;



            DataSet DS = new DataSet();



            DataTable DT = new DataTable();



            Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,password,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,status,activation_key,logged_in,mac_address," +
                                  "strftime(last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update FROM rcs_users rcs_users where " +



                                         "(rcs_users.homephone LIKE  @phonenumber OR rcs_users.workphone LIKE  @phonenumber OR rcs_users.mobilephone LIKE  @phonenumber)");





            //  string query = String.Format("SELECT * FROM rcs_users where "+phnTrack+"='{0}';", phoneNumber); //   REPLACE(rcs_users.postcode, ' ', '') LIKE '%" + phoneNumber + "%' OR CONCAT(rcs_users.house, ' ',rcs_users.address) LIKE '%" + phoneNumber + "%' OR







            // dataRow = command.ExecuteReader();





            command = CommandMethod(command);



            command.Parameters.AddWithValue("@phonenumber", phoneNumber);





            Reader = ReaderMethod(Reader, command);


            DT.Load(Reader);



            if (DT.Rows.Count > 0)
            {



                aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(DT, 0);





            } return aRestaurantUser;
        }

        public DataTable GetResturantCustomerByCustomerIdDataTableSync(int customerId)
        {
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,usertype,firstname,lastname,email,username,password,manage_password,house,address,homephone,workphone,mobilephone,city,county,postcode,full_address,status,activation_key,logged_in,mac_address," +
                "strftime(last_activity) as last_activity,can_manage,use_java,check_online_order,gcm_reg_id,is_opt_out,is_update  FROM  rcs_users where id=@customerId");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@customerId", customerId);



            try
            {
                Reader = ReaderMethod(Reader, command);

                DT.Load(Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return DT;
        }

        public DataTable GetMemberShipByUserIdDataTableByMemberShips(int userId, int restaurantId)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT rcs_membership.*  FROM rcs_membership left join rcs_membership_type ON  rcs_membership.membership_type_id =rcs_membership_type.id  where rcs_membership.membership_id =@membership_id and rcs_membership_type.restaurant_id=@restaurant_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@membership_id", userId);
            command.Parameters.AddWithValue("@restaurant_id", restaurantId); 

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);

            return DT;
        }

        public RestaurantUsers IsExistCustomer(RestaurantUsers aRestaurantUser)
        {
            Query = String.Format("SELECT firstname,mobilephone FROM rcs_users where mobilephone =@mobilephone  or homephone=@homephone" );
            //, userId, MemberShipTypeID);

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@mobilephone", aRestaurantUser.Mobilephone);
            command.Parameters.AddWithValue("@homephone", aRestaurantUser.Mobilephone);
            DataTable dt = new DataTable();
           
           
            Reader = ReaderMethod(Reader, command);
            dt.Load(Reader);
            if (dt.Rows.Count>0)
            {
                aRestaurantUser = _customerReader.ReaderToReadRestaurantCustomer(dt, 0);
                return aRestaurantUser;
            }
            return null;
        }
    }
}
