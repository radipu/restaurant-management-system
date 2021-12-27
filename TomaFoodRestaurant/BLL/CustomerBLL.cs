using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class CustomerBLL
    {
        public int InsertRestaurantCustomer(RestaurantUsers aRestaurantUser)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.InsertRestaurantCustomer(aRestaurantUser);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.InsertRestaurantCustomer(aRestaurantUser);
            }

        }


        public RestaurantUsers IsExistCustomer(RestaurantUsers aRestaurantUser)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.IsExistCustomer(aRestaurantUser);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.IsExistCustomer(aRestaurantUser);
            }
        }


        public RestaurantUsers GetRestaurantCustomerByHomePhone(string phoneNumber, string phnTrack)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetRestaurantCustomerByHomePhone(phoneNumber, phnTrack);

            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetRestaurantCustomerByHomePhone(phoneNumber, phnTrack);

            }

        }
 

        public List<RestaurantUsers> GetRestaurantAllCusttomer()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetRestaurantAllCusttomer();
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetRestaurantAllCusttomer();
            }


        }

        internal int InsertNewCustomer(RestaurantUsers customer)
        {
            MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
            return aCustomerDAO.InsertNewCustomer(customer);
        }

        internal RestaurantUsers GetResturantCustomerByCustomerId(int customerId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetResturantCustomerByCustomerId(customerId);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetResturantCustomerByCustomerId(customerId);
            }


        }
    
        internal int UpdateRestaurantCustomer(RestaurantUsers aRestaurantUser)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.UpdateRestaurantCustomer(aRestaurantUser);
            }
            else
            {

                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.UpdateRestaurantCustomer(aRestaurantUser);
            }

        }

        public List<RestaurantUsers> GetRestaurantAllCustomerForShow(string espression)
        {

            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetRestaurantAllCustomerForShow(espression);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetRestaurantAllCustomerForShow(espression);
            }

        }

        public List<CustomerOrderItems> GetCustomerItems(int customerId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetCustomerItems(customerId);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetCustomerItems(customerId);
            }
        }

        public string DeleteCustomerOrderedItems(int customerId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.DeleteCustomerOrderedItems(customerId);

            }
            else
            {

                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.DeleteCustomerOrderedItems(customerId);
            }

        }

        internal List<RestaurantUsers> GetResturantCustomerByIsUpdate(int isUpdate)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetResturantCustomerByIsUpdate(isUpdate);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetResturantCustomerByIsUpdate(isUpdate);
            }

        }
         
        internal string CustomerSyncronise(RestaurantUsers user)
        {

            
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                GlobalUrl urls = aGlobalUrlBll.GetUrls();
                bool flag = false;
                CustomerBLL aCustomerBll = new CustomerBLL();


                //////////////////////////////////

                LocalOrderSyn localCustomerSyn = new LocalOrderSyn();
                if (GlobalSetting.DbType == "MYSQL")
                {
                    localCustomerSyn.restaurantId = GlobalSetting.RestaurantInformation.Id;

                    localCustomerSyn.RestaurantUsers =
                        new MySqlCustomerDAO().GetResturantCustomerByCustomerIdDataTableSync(user.Id);
                    localCustomerSyn.MemberShips =
                        new MySqlCustomerDAO().GetMemberShipByUserIdDataTableByMemberShips(user.Id,
                            GlobalSetting.RestaurantInformation.Id);
                }
                else
                {
                    localCustomerSyn.RestaurantUsers =
                        new CustomerDAO().GetResturantCustomerByCustomerIdDataTableSync(user.Id);
                    localCustomerSyn.MemberShips =
                        new CustomerDAO().GetMemberShipByUserIdDataTableByMemberShips(user.Id,
                            GlobalSetting.RestaurantInformation.Id);

                }

                try
                {
                    string JSONresult = JsonConvert.SerializeObject(localCustomerSyn);
                    string result = "0";
                    if (OthersMethod.CheckForInternetConnection())
                    {
                        string url = GlobalVars.hostUrl + "restaurantcontrol/request/json_add_customer";
                        result = CustomerSyncToServer(JSONresult, url); 
                    }

                    if (result != "" && Convert.ToInt32(result) > 0)
                    {
                        flag = true;
                        user.IsUpdate = 1;
                        aCustomerBll.UpdateRestaurantCustomer(user);
                    }

                }
                catch (Exception ex)
                {
                    flag = false;
                } 

                return flag == true ? "Operation Successful!" : "User syncronize unsuccessful! Try later";

            
        }

        public string CustomerSyncToServer(string JSONresult, string url)
        {
             
            try
            {
                Console.WriteLine("{0} seconds with one transaction.", DateTime.Now);

                string result = "";
                using (var wb = new WebClient())
                {
                    var post_data = new NameValueCollection();
                    post_data["CUSTOMER"] = JSONresult;

                    var response = wb.UploadValues(url, "POST", post_data);
                    result = Encoding.UTF8.GetString(response);
                }
                return result;
            }
            catch (Exception ex)
            {
                return '0'.ToString();
            }
        }

        public MemberShips GetMemberShipByUserID(int userId, int resId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetMemberShipByUserID(userId, resId);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetMemberShipByUserID(userId, resId);
            }
        }



        internal List<MemberShipType> GetMembershipsByResId(int resId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetMembershipsByResId(resId);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetMembershipsByResId(resId);
            }

        }

        public int InsertMembership(MemberShips mem)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.InsertMembership(mem);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.InsertMembership(mem);

            }


        }



        public MemberShips GetMemberShips(int userId, int MemberShipTypeID)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                CustomerDAO aCustomerDAO = new CustomerDAO();
                return aCustomerDAO.GetMemberShips(userId, MemberShipTypeID);
            }
            else
            {
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetMemberShips(userId, MemberShipTypeID);
            }
            
        }


        internal RestaurantUsers GetCustomerByPhone(string phoneNumber)
        {
            //if (GlobalSetting.DbType == "SQLITE")
            //{
            //    CustomerDAO aCustomerDAO = new CustomerDAO();
            //    return aCustomerDAO.GetCustomerByPhone(phoneNumber);
            //}
            //else
            //{
                MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                return aCustomerDAO.GetCustomerByPhone(phoneNumber);
          //  }
              
             
        }
        internal List<CouponCode> GetCustomerCoupon()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                //CustomerDAO aCustomerDAO = new CustomerDAO();
                //return aCustomerDAO.GetCustomerByPhone(phoneNumber);
                return null;
            }
            else
            {
                return new MySqlCustomerDAO().GetCustomerCoupon();
            }


        }
        internal CouponCode GetCustomerCouponById(CouponCode id)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                //CustomerDAO aCustomerDAO = new CustomerDAO();
                //return aCustomerDAO.GetCustomerByPhone(phoneNumber);
                return null;
            }else
            {
                return new MySqlCustomerDAO().GetCustomerCouponById(id);
            }


        }

        public string CouponCRUID(CouponCode couponCode,string actionType)
        {
            try
            {
                if (GlobalSetting.DbType == "SQLITE")
                {
                    //CustomerDAO aCustomerDAO = new CustomerDAO();
                    //return aCustomerDAO.GetCustomerByPhone(phoneNumber);
                    return "";
                }
                else
                {
                    if (actionType == "Edit")
                    {
                        if (new MySqlCustomerDAO().CouponEdit(couponCode) > 0)
                        {
                            return "Success Edit";
                        }

                    }
                    else if (actionType == "Remove")
                    {
                        if (new MySqlCustomerDAO().CouponRemove(couponCode) > 0)
                        {
                            return "Success Remove";
                        }

                    }
                    else if (actionType == "Create")
                    {
                        if (new MySqlCustomerDAO().CouponCreate(couponCode) > 0)
                        {
                            return "Success Create";
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
               
            }
           
            return "Faild";
        }
    }
}
