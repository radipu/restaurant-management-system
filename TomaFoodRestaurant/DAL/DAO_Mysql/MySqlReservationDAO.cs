using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
   public class MySqlReservationDAO:MySqlGatewayConnection
   {
       private string ReservationQuery =
           "id,restaurant_id,Cast(reserved_date as DATETIME) AS reserved_date,firstname,lastname,email,phone,no_of_people,special_notes,Cast(reserved_date as DATETIME) AS reserved_date,reservation_type,`status`,online_reservation_id,email_text";

        internal string UpdateReservation(int ReservationId, string reservationType, string status)
        {
            int lastId = 0;
            try
            {

                Query = String.Format("UPDATE rcs_booking SET reservation_type=@reservation_type,status=@status WHERE id=@id");
                try
                {

                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@reservation_type", reservationType);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@id", ReservationId);


                    lastId = command.ExecuteNonQuery();

                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
                        " VALUES (@restaurant_id,@reserved_date,@firstname,@lastname,@email,@phone,@no_of_people,@special_notes,@reservation_date,@reservation_type," +
                        "@status,@online_reservation_id,@email_text)");

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
                command.Parameters.AddWithValue("@email_text", reservation.EmailText ?? "");
                
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
                    command.Parameters.AddWithValue("@email_text", reservation.EmailText ?? "");


                    lastId = command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return (int)lastId;
        }

        internal string UpdateLocalReservation(Reservation aReservation)
        {
            int lastId = 0;
            try
            {


                Query =
                    String.Format(
                        "UPDATE rcs_booking SET reserved_date=@reserved_date, firstname=@firstname,lastname=@lastname,email=@email, phone=@phone,no_of_people=@no_of_people," +
                        "special_notes=@special_notes, online_reservation_id=@online_reservation_id  WHERE id=@id");


                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@reserved_date", aReservation.reservedDate);
                    command.Parameters.AddWithValue("@firstname", aReservation.FirstName);
                    command.Parameters.AddWithValue("@lastname", aReservation.LastName);
                    command.Parameters.AddWithValue("@email", aReservation.Email);
                    command.Parameters.AddWithValue("@phone", aReservation.Phone);
                    command.Parameters.AddWithValue("@no_of_people", aReservation.noOfPeople);
                    command.Parameters.AddWithValue("@special_notes", aReservation.specialNotes);
                    command.Parameters.AddWithValue("@online_reservation_id", aReservation.online_reservation_id);
                    command.Parameters.AddWithValue("@id", aReservation.ReserveId);

                    lastId = command.ExecuteNonQuery();
                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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


        internal DataTable GetRestaurantReservationByDate(DateTime startDate, DateTime endDate, string type)
        {
            DataTable DT = new DataTable();
            string sDate = startDate.ToString("yyyy-MM-dd");
            string eDate = endDate.AddDays(1).ToString("yyyy-MM-dd");

          
            try
            {
                DataSet DS = new DataSet();
                if (type == "ALL")
                {
                    Query = String.Format("SELECT * FROM rcs_booking WHERE reserved_date >='{0}' AND reserved_date <='{1}' ORDER BY reservation_type, reserved_date DESC", sDate, eDate);
                }
                else if (type == "pending")
                {
                    Query = String.Format("SELECT " + ReservationQuery + " FROM rcs_booking where `status` = 0 AND reservation_type='{0}' ORDER BY reserved_date DESC", type);

                }
                else if (type == "confirm")
                {

                    //Query = String.Format("SELECT " + ReservationQuery + " FROM rcs_booking where `status`> 0 AND reservation_type='{0}' AND reserved_date >='{1}' AND reserved_date <='{2}'", type, sDate, eDate);
                    Query = String.Format("SELECT * FROM rcs_booking WHERE `status`> 0 AND reservation_type='{0}' AND reserved_date >='{1}' AND reserved_date <='{2}' ORDER BY reserved_date DESC", type, sDate, eDate);


                }

                else
                {
                    Query = String.Format("SELECT * FROM rcs_booking WHERE reservation_type='{0}' AND reserved_date >='{1}' AND reserved_date <='{2}' ORDER BY reserved_date DESC", type, sDate, eDate);
                }
               
                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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

            string datetimeNew = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            DataTable DT = new DataTable();
            try
            {
                DataSet DS = new DataSet();
                Query = String.Format("SELECT " + ReservationQuery + " FROM rcs_booking where status =0 AND reservation_type ='pending'");

                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
                Query = String.Format("SELECT " + ReservationQuery + " FROM rcs_booking where status = 0 AND reservation_type='pending'");

                Adapter = GetAdapter(Adapter);


                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                int cnt = 0;
                foreach (DataRow row in DT.Rows)
                {
                    cnt++;
                    Reservation aReservation = new Reservation();

                    aReservation.SL = cnt;
                    aReservation.ReserveId = (int)row.Field<long>("id");
                    aReservation.FirstName = row.Field<string>("firstname") + " " + row.Field<string>("lastname");
                    aReservation.reservedDate = row.Field<DateTime>("reserved_date");
                    aReservation.noOfPeople = (int)row.Field<long>("no_of_people");
                    aReservation.Phone = row.Field<string>("phone");
                    aReservation.specialNotes = row.Field<string>("special_notes");
                    aReservation.online_reservation_id = (int)row.Field<Int32>("online_reservation_id");
                    aReservations.Add(aReservation);


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
                Query = String.Format("SELECT " + ReservationQuery + " FROM rcs_booking where id={0}", bookingId);

                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                int cnt = 0;
                foreach (DataRow row in DT.Rows)
                {
                    cnt++;


                    aReservation.SL = cnt;
                    aReservation.ReserveId = Convert.ToInt32(row["id"]);
                    aReservation.FirstName = row.Field<string>("firstname");
                    aReservation.LastName = row.Field<string>("lastname");
                    aReservation.Email = row.Field<string>("email");
                    aReservation.reservedDate = row.Field<DateTime>("reserved_date");
                    aReservation.noOfPeople = row.Field<Int32>("no_of_people");
                    aReservation.Phone = row.Field<string>("phone");
                    aReservation.specialNotes = row.Field<string>("special_notes");
                    aReservation.online_reservation_id = row.Field<Int32>("online_reservation_id");

                    }
            }catch (Exception exception)
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
                Query = String.Format("SELECT " + ReservationQuery + " FROM rcs_booking where online_reservation_id={0}", onlineReservationId);
                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                int cnt = 0;
                foreach (DataRow row in DT.Rows)
                {
                    cnt++;


                    aReservation.SL = cnt;
                    aReservation.ReserveId = Convert.ToInt32(row["id"]);
                    aReservation.FirstName = row.Field<string>("firstname");
                    aReservation.LastName = row.Field<string>("lastname");
                    aReservation.Email = row.Field<string>("email");
                    aReservation.reservedDate = row.Field<DateTime>("reserved_date");aReservation.noOfPeople = Convert.ToInt32(row["no_of_people"]);
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
