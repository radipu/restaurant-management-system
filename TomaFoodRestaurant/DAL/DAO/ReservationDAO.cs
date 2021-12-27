using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class ReservationDAO : GatewayConnection
    {


        internal string UpdateReservation(int ReservationId, string reservationType, string status)
        {
            int lastId = 0;
            try
            {

                Query = String.Format("UPDATE rcs_booking SET reservation_type='{1}',status='{2}' WHERE id={0}", ReservationId, reservationType, status);

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

            }
            catch (Exception ex)
            {

            }

            return (int)lastId > 0 ? "Yes" : "No";
        }

        internal int InsertOnlineReservation(Reservation reservation, bool isReservationId)
        {

            int lastId = 0, lastId2 = 0;

            if (isReservationId)
            {

                Query =
                    String.Format(
                        "INSERT INTO rcs_booking (restaurant_id,reserved_date,firstname,lastname,email,phone,no_of_people,special_notes," +
                        "reservation_date,reservation_type,status,online_reservation_id,email_text)" +
                        " VALUES (@restaurant_id,@reserved_date,@firstname,@lastname,@email,@phone,@no_of_people,@special_notes,@reservation_date," +
                        "@reservation_type,@status,@online_reservation_id,@email_text)");

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@restaurant_id", reservation.restaurantId);
                command.Parameters.AddWithValue("@reserved_date", reservation.reservedDate);
                command.Parameters.AddWithValue("@firstname", reservation.FirstName ?? "");
                command.Parameters.AddWithValue("@lastname", reservation.LastName ?? "");
                command.Parameters.AddWithValue("@email", reservation.Email ?? "");
                command.Parameters.AddWithValue("@phone", reservation.Phone ?? "");
                command.Parameters.AddWithValue("@no_of_people", reservation.noOfPeople);
                command.Parameters.AddWithValue("@special_notes", reservation.specialNotes ?? "");
                command.Parameters.AddWithValue("@reservation_date", reservation.ReservationDate);
                command.Parameters.AddWithValue("@reservation_type", reservation.Type ?? "");
                command.Parameters.AddWithValue("@status", reservation.Status);
                command.Parameters.AddWithValue("@online_reservation_id", reservation.online_reservation_id);
                command.Parameters.AddWithValue("@email_text", reservation.EmailText ?? "N/A");


                lastId = command.ExecuteNonQuery();
            }
            else
            {
                Query =
                    String.Format(
                        "INSERT INTO rcs_booking (restaurant_id,reserved_date,firstname,lastname,email,phone,no_of_people,special_notes," +
                        "reservation_date,reservation_type,status,online_reservation_id,email_text)" +
                        " VALUES (@restaurant_id,@reserved_date,@firstname,@lastname,@email,@phone,@no_of_people,@special_notes,@reservation_date," +
                        "@reservation_type,@status,@online_reservation_id,@email_text)");



                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@restaurant_id", reservation.restaurantId);
                    command.Parameters.AddWithValue("@reserved_date", reservation.reservedDate);
                    command.Parameters.AddWithValue("@firstname", reservation.FirstName ?? "");
                    command.Parameters.AddWithValue("@lastname", reservation.LastName ?? "");
                    command.Parameters.AddWithValue("@email", reservation.Email ?? "");
                    command.Parameters.AddWithValue("@phone", reservation.Phone ?? "");
                    
                    command.Parameters.AddWithValue("@no_of_people", reservation.noOfPeople);
                    command.Parameters.AddWithValue("@special_notes", reservation.specialNotes ?? "");
                    command.Parameters.AddWithValue("@reservation_date", reservation.ReservationDate);
                    command.Parameters.AddWithValue("@reservation_type", reservation.Type ?? "");
                    command.Parameters.AddWithValue("@status", reservation.Status);
                    command.Parameters.AddWithValue("@online_reservation_id", reservation.online_reservation_id);
                    command.Parameters.AddWithValue("@email_text", reservation.EmailText ?? "N/A");


                    lastId = command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }

            return (int)lastId;
        }

        internal string UpdateLocalReservation(Reservation aReservation)
        {
            int lastId = 0;
            try
            {


                Query =String.Format("UPDATE rcs_booking SET reserved_date='{1}', firstname='{2}',lastname='{3}',email='{4}', phone='{5}',no_of_people='{6}'," +
                                 "special_notes='{7}', online_reservation_id={8}  WHERE id={0}", aReservation.ReserveId, aReservation.reservedDate, aReservation.FirstName, aReservation.LastName, aReservation.Email,
                                 aReservation.Phone, aReservation.noOfPeople, aReservation.specialNotes, aReservation.online_reservation_id);


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


            }
            catch (Exception ex)
            {

            }

            return (int)lastId > 0 ? "Reservation has been updated sucessfully." : "Something wrong! Please try again.";
        }


        internal DataTable GetRestaurantReservationByDate(DateTime startDate, DateTime endDate, string status)
        {
            DataTable DT = new DataTable();
            try
            {
                DataSet DS = new DataSet();
                Query = status == "ALL"
                    ? String.Format("SELECT * FROM rcs_booking")
                    : String.Format("SELECT * FROM rcs_booking where reservation_type='{0}'", status);

                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return DT;

        }


        internal DataTable GetNewReservation()
        {
            DataTable DT = new DataTable();
            try
            {
                DataSet DS = new DataSet();
                Query = String.Format("SELECT * FROM rcs_booking where status = 0 AND reservation_type='pending'");

                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return DT;

        }



        internal List<Reservation> GetPendingReservation()
        {
            DataTable DT = new DataTable();

            List<Reservation> aReservations = new List<Reservation>();
            try
            {
                DataSet DS = new DataSet();
                Query = String.Format("SELECT * FROM rcs_booking where status = 0 AND reservation_type='pending'");

                Adapter = GetAdapter(Adapter);


                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];


                int cnt = 0;
                foreach (DataRow row in DT.Rows)
                {
                    cnt++;
                    Reservation aReservation = new Reservation();

                    aReservation.SL = cnt;
                    aReservation.ReserveId = Convert.ToInt32(row["id"]);
                    aReservation.FirstName = row.Field<string>("firstname");
                    aReservation.LastName = row.Field<string>("lastname");
                    aReservation.Email = row.Field<string>("email");
                    aReservation.reservedDate = Convert.ToDateTime(row["reserved_date"]);
                    aReservation.noOfPeople = Convert.ToInt32(row["no_of_people"]);
                    aReservation.Phone = row.Field<string>("phone");
                    aReservation.specialNotes = row.Field<string>("special_notes");
                    aReservation.online_reservation_id = Convert.ToInt32(row["online_reservation_id"]);



                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return aReservations;

        }

        internal Reservation GetBookingByBookingId(int bookingId)
        {
            DataTable DT = new DataTable();

            Reservation aReservation = new Reservation();
            try
            {
                DataSet DS = new DataSet();
                Query = String.Format("SELECT * FROM rcs_booking where id={0}", bookingId);

                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];


                int cnt = 0;
                foreach (DataRow row in DT.Rows)
                {
                    cnt++;


                    aReservation.SL = cnt;
                    aReservation.ReserveId = (int)row.Field<long>("id");
                    aReservation.FirstName = row.Field<string>("firstname");
                    aReservation.LastName = row.Field<string>("lastname");
                    aReservation.Email = row.Field<string>("email");
                    aReservation.reservedDate = Convert.ToDateTime(row.Field<string>("reserved_date"));
                    aReservation.noOfPeople = (int)row.Field<long>("no_of_people");
                    aReservation.Phone = row.Field<string>("phone");
                    aReservation.specialNotes = row.Field<string>("special_notes");
                    aReservation.online_reservation_id = -(int)row.Field<long>("online_reservation_id");


                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return aReservation;

        }


        internal Reservation GetBookingByOnlineReservationId(int onlineReservationId)
        {
            DataTable DT = new DataTable();

            Reservation aReservation = new Reservation();
            try
            {
                DataSet DS = new DataSet();
                Query = String.Format("SELECT * FROM rcs_booking where online_reservation_id={0}", onlineReservationId);
                Adapter = GetAdapter(Adapter);

                        DS.Reset();
                        Adapter.Fill(DS);
                        DT = DS.Tables[0];
                  

                int cnt = 0;
                foreach (DataRow row in DT.Rows)
                {
                    cnt++;


                    aReservation.SL = cnt;
                    aReservation.ReserveId = (int)row.Field<long>("id");
                    aReservation.FirstName = row.Field<string>("firstname");
                    aReservation.LastName = row.Field<string>("lastname");
                    aReservation.Email = row.Field<string>("email");
                    aReservation.reservedDate = Convert.ToDateTime(row.Field<string>("reserved_date"));
                    aReservation.noOfPeople = (int)row.Field<long>("no_of_people");
                    aReservation.Phone = row.Field<string>("phone");
                    aReservation.specialNotes = row.Field<string>("special_notes");
                    aReservation.online_reservation_id = -(int)row.Field<long>("online_reservation_id");


                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return aReservation;

        }

        public string AddLocalReservationToServer(string data, string url)
        {
            string result = "";
            try
            {

                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Post the data to the right place.
                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd();
                        }
                    }
                }


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return result;


        }
    }
}
