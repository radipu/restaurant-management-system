using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services.Description;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class ReservationBLL
    {
       internal string UpdateReservation(int ReservationId, string reservationType, string status)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.UpdateReservation(ReservationId, reservationType, status);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.UpdateReservation(ReservationId, reservationType, status);   
           }
       }

       internal int InsertOnlineReservation(Reservation reservation, bool isReservationId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.InsertOnlineReservation(reservation, isReservationId);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.InsertOnlineReservation(reservation, isReservationId);
           }
       }

       internal string UpdateLocalReservation(Reservation aReservation)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.UpdateLocalReservation(aReservation);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.UpdateLocalReservation(aReservation);
           }
       }

       internal DataTable GetRestaurantReservationByDate(DateTime startDate, DateTime endDate, string status)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {

               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.GetRestaurantReservationByDate(startDate, endDate, status);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.GetRestaurantReservationByDate(startDate, endDate, status);
           }
       }

       internal DataTable GetNewReservation()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.GetNewReservation();
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.GetNewReservation();
           }
        
       }

       internal List<Reservation> GetPendingReservation()
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.GetPendingReservation();
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.GetPendingReservation();
           }
       }

       internal Reservation GetBookingByBookingId(int bookingId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.GetBookingByBookingId(bookingId);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.GetBookingByBookingId(bookingId);
           }

       }

       internal Reservation GetBookingByOnlineReservationId(int onlineReservationId)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.GetBookingByOnlineReservationId(onlineReservationId);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.GetBookingByOnlineReservationId(onlineReservationId);
           }
       }

       public string AddLocalReservationToServer(string data, string url)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               ReservationDAO aReservationDao = new ReservationDAO();
               return aReservationDao.AddLocalReservationToServer(data, url);
           }
           else
           {
               MySqlReservationDAO aReservationDao = new MySqlReservationDAO();
               return aReservationDao.AddLocalReservationToServer(data, url);
           }
       }

       public int SendReservationDataToWeb(Reservation aReservation)
       {
           ReservationBLL aReservationBll = new ReservationBLL();
           GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
           GlobalUrl urls = aGlobalUrlBll.GetUrls();
           string url = urls.AcceptUrl + "restaurantcontrol/request/crud/add_reservation/" + GlobalSetting.RestaurantInformation.Id;
           string data = "data=" + JsonConvert.SerializeObject(aReservation);
           string id = aReservationBll.AddLocalReservationToServer(data, url);

           int online_reservation_id = 0;

           if (int.TryParse(id, out online_reservation_id))
           {
               return online_reservation_id;
           }

           return online_reservation_id;
       }

       public Reservation GetOnlineReservation(JObject oReader)
       {
           Reservation arcs_order = new Reservation();
           if (oReader["id"] != null)
           {
               arcs_order.online_reservation_id = Convert.ToInt32(oReader["id"]);
           }

           if (oReader["restaurant_id"] != null)
           {
               arcs_order.RestaurantId = Convert.ToInt32(oReader["restaurant_id"]);
           }

           if (oReader["reserved_date"] != null)
           {

               try
               {
                   string time = Convert.ToString(oReader["reserved_date"]);
                   string mynumber = Regex.Replace(time, @"\D", "");
                   arcs_order.reservedDate = Convert.ToDateTime(mynumber);

               }
               catch (Exception ex)
               {

                   string time = Convert.ToString(oReader["reserved_date"]);
                   arcs_order.reservedDate = Convert.ToDateTime(time);
                 //  MessageBox.Show(ex.GetBaseException().ToString());
               }
               //arcs_order.reservedDate = Convert.ToDateTime(oReader["reserved_date"]);
           }

           if (oReader["firstname"] != null)
           {
               arcs_order.FirstName = Convert.ToString(oReader["firstname"]);
           }

           if (oReader["lastname"] != null)
           {
               arcs_order.LastName = Convert.ToString(oReader["lastname"]);
           }
           if (oReader["email"] != null)
           {
               arcs_order.Email = Convert.ToString(oReader["email"]);
           }


           if (oReader["phone"] != null)
           {
               arcs_order.Phone = Convert.ToString(oReader["phone"]);
           }


           if (oReader["no_of_people"] != null)
           {
               arcs_order.noOfPeople = Convert.ToInt32(oReader["no_of_people"]);
           }


           if (oReader["special_notes"] != null)
           {
               arcs_order.specialNotes = Convert.ToString(oReader["special_notes"]);
           }


           if (oReader["reservation_type"] != null)
           {
               arcs_order.Type = Convert.ToString(oReader["reservation_type"]);
           }


            if (oReader["status"] != null)
            {
                arcs_order.Status = Convert.ToInt32(oReader["status"]);
            }
            if (oReader["encrypted_link"] != null)
            {
                arcs_order.encryptedLink = Convert.ToString(oReader["encrypted_link"]);
            }
            return arcs_order;
       }
    }
}
