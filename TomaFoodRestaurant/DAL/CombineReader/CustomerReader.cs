using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
    public class CustomerReader
    {
        public RestaurantUsers ReaderToReadRestaurantCustomer(DataTable oReader, int i)
        {
            RestaurantUsers arcs_users = new RestaurantUsers();

            arcs_users.Id = Convert.ToInt32(oReader.Rows[i]["id"]);

            arcs_users.Usertype = Convert.ToString(oReader.Rows[i]["usertype"]);

            arcs_users.Firstname = Convert.ToString(oReader.Rows[i]["firstname"]);

            arcs_users.Lastname = Convert.ToString(oReader.Rows[i]["lastname"]);

            arcs_users.Email = Convert.ToString(oReader.Rows[i]["email"]);

            arcs_users.Username = Convert.ToString(oReader.Rows[i]["username"]);

            arcs_users.Password = Convert.ToString(oReader.Rows[i]["password"]);

            arcs_users.ManagePassword = Convert.ToString(oReader.Rows[i]["manage_password"]);

            arcs_users.House = Convert.ToString(oReader.Rows[i]["house"]);

            arcs_users.Address = Convert.ToString(oReader.Rows[i]["address"]);

            arcs_users.Homephone = Convert.ToString(oReader.Rows[i]["homephone"]);

            arcs_users.Workphone = Convert.ToString(oReader.Rows[i]["workphone"]);

            arcs_users.Mobilephone = Convert.ToString(oReader.Rows[i]["mobilephone"]);

            arcs_users.City = Convert.ToString(oReader.Rows[i]["city"]);
            arcs_users.County = Convert.ToString(oReader.Rows[i]["county"]);

            arcs_users.Postcode = Convert.ToString(oReader.Rows[i]["postcode"]);

            arcs_users.Status = Convert.ToString(oReader.Rows[i]["status"]);

            arcs_users.ActivationKey = Convert.ToString(oReader.Rows[i]["activation_key"]);

            arcs_users.LoggedIn = Convert.ToInt64(oReader.Rows[i]["logged_in"]);

            arcs_users.MacAddress = Convert.ToString(oReader.Rows[i]["mac_address"]);


            //if (oReader.Rows[i]["last_activity"] != DBNull.Value)
            //{
            //    var convertDate = new GlobalDateTimeFormat(oReader.Rows[i]["last_activity"].ToString());
            //     arcs_users.LastActivity = Convert.ToDateTime(convertDate.ConvertTimeCustome);
            //}

            //try
            //{
            //    if (oReader.Rows[i]["last_activity"] != DBNull.Value && oReader.Rows[i]["last_activity"] != "")
            //    {
            //        arcs_users.LastActivity = Convert.ToDateTime(oReader.Rows[i]["last_activity"]);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    arcs_users.LastActivity = DateTime.Now;
            //}

            arcs_users.CanManage = Convert.ToInt64(oReader.Rows[i]["can_manage"]);

            arcs_users.UseJava = Convert.ToInt64(oReader.Rows[i]["use_java"]);

            arcs_users.CheckOnlineOrder = Convert.ToInt64(oReader.Rows[i]["check_online_order"]);

            arcs_users.GcmRegId = Convert.ToString(oReader.Rows[i]["gcm_reg_id"]);
            
            arcs_users.FullAddress = Convert.ToString(oReader.Rows[i]["full_address"]);

            if (arcs_users.FullAddress == "0")
            {
                arcs_users.FullAddress = "";
            }

            try
            {
                arcs_users.IsUpdate = Convert.ToInt32(oReader.Rows[i]["is_update"]);

            }
            catch (Exception)
            {
            }

            arcs_users.Name = arcs_users.Firstname + " " + arcs_users.Lastname;
            return arcs_users;
        }


        public RestaurantUsers ReaderToReadRestaurantUsersForShow(DataTable oReader, int i)
        {
            RestaurantUsers arcs_users = new RestaurantUsers();
            if (oReader.Rows[i]["id"] != DBNull.Value)
            {
                arcs_users.Id = Convert.ToInt32(oReader.Rows[i]["id"]);
            }
            if (oReader.Rows[i]["usertype"] != DBNull.Value)
            {
                arcs_users.Usertype = Convert.ToString(oReader.Rows[i]["usertype"]);
            }
            if (oReader.Rows[i]["firstname"] != DBNull.Value)
            {
                arcs_users.Firstname = Convert.ToString(oReader.Rows[i]["firstname"]);
            }
            if (oReader.Rows[i]["lastname"] != DBNull.Value)
            {
                arcs_users.Lastname = Convert.ToString(oReader.Rows[i]["lastname"]);
            }

            if (oReader.Rows[i]["house"] != DBNull.Value)
            {
                arcs_users.House = Convert.ToString(oReader.Rows[i]["house"]);
            }
            if (oReader.Rows[i]["address"] != DBNull.Value)
            {
                arcs_users.Address = Convert.ToString(oReader.Rows[i]["address"]);
            }
            if (oReader.Rows[i]["homephone"] != DBNull.Value)
            {
                arcs_users.Homephone = Convert.ToString(oReader.Rows[i]["homephone"]);
            }
            if (oReader.Rows[i]["workphone"] != DBNull.Value)
            {
                arcs_users.Workphone = Convert.ToString(oReader.Rows[i]["workphone"]);
            }
            if (oReader.Rows[i]["mobilephone"] != DBNull.Value)
            {
                arcs_users.Mobilephone = Convert.ToString(oReader.Rows[i]["mobilephone"]);
            }
            if (oReader.Rows[i]["city"] != DBNull.Value)
            {
                arcs_users.City = Convert.ToString(oReader.Rows[i]["city"]);
            }
            if (oReader.Rows[i]["county"] != DBNull.Value)
            {
                arcs_users.County = Convert.ToString(oReader.Rows[i]["county"]);
            }
            if (oReader.Rows[i]["postcode"] != DBNull.Value)
            {
                arcs_users.Postcode = Convert.ToString(oReader.Rows[i]["postcode"]);
            }

            if (oReader.Rows[i]["full_address"] != DBNull.Value)
            {
                arcs_users.FullAddress = Convert.ToString(oReader.Rows[i]["full_address"]);
            }

            arcs_users.Name = arcs_users.Firstname + " " + arcs_users.Lastname;
            return arcs_users;
        }

        public MemberShipType ReaderToReadMemberships(DataTable oReader, int i)
        {
            MemberShipType memberShipType = new MemberShipType();
            if (oReader.Rows[i]["id"] != DBNull.Value)
            {
                memberShipType.Id = Convert.ToInt32(oReader.Rows[i]["id"]);
            }
            if (oReader.Rows[i]["type_name"] != DBNull.Value)
            {
                memberShipType.TypeName = Convert.ToString(oReader.Rows[i]["type_name"]);
            }
            if (oReader.Rows[i]["restaurant_id"] != DBNull.Value)
            {
                memberShipType.RestaurantId = Convert.ToInt32(oReader.Rows[i]["restaurant_id"]);
            }
            return memberShipType;
        }

        public MemberShips ReaderToReadMemberShips(DataTable oReader, int i)
        {
            MemberShips memberShips = new MemberShips();


            if (oReader.Rows[i]["id"] != DBNull.Value)
            {
                memberShips.Id = Convert.ToInt32(oReader.Rows[i]["id"]);
            }
            if (oReader.Rows[i]["membership_type_id"] != DBNull.Value)
            {
                memberShips.MembershipTypeId = Convert.ToInt32(oReader.Rows[i]["membership_type_id"]);
            }
            if (oReader.Rows[i]["membership_number"] != DBNull.Value)
            {
                memberShips.MembershipNumber = Convert.ToInt32("0");
            }
            if (oReader.Rows[i]["membership_id"] != DBNull.Value)
            {
                memberShips.MembershipId = Convert.ToInt32(oReader.Rows[i]["membership_id"]);
            }
            return memberShips;
        }

    }
}
