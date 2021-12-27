using System;
using System.Collections.Generic; 
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using System.Net;
using System.IO;
using System.Drawing;
using System.Web.UI.WebControls;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class ReservationTable : UserControl
    {
        public static string status { set; get; }
        public static int Id { set; get; }
        GlobalUrl urls = new GlobalUrl();
        RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        public ReservationTable()
        {
            InitializeComponent();
            LoadStatusCombobx();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            RestaurantInformationBLL aRestaurantInformationBll=new RestaurantInformationBLL();
            aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            SetAlternateRowColor();
        }

        private void buttonLoadReservation_Click(object sender, EventArgs e)
        { 

            LoadReservation();
            SetAlternateRowColor();
        }

        
        private void LoadReservation()
        {
            List<Reservation> aReservations = new List<Reservation>();
            try
            {
                string reservationType = statusComboBox.Text;


                if (reservationType == "Pending")
                {
                    reservationType = "pending";
                }
                else if (reservationType == "Accepted")
                {
                    reservationType = "confirm";

                    var dataGridViewColumn = reservationDataGridView.Columns["Arrived"];
                    if (dataGridViewColumn != null)
                        dataGridViewColumn.Visible = true;


                }
                else if (reservationType == "Rejected")
                {
                    reservationType = "reject";
                }
                if (reservationType != "pending")
                {
                    var dataGridViewColumn = reservationDataGridView.Columns["Accept"];
                    if (dataGridViewColumn != null)
                        dataGridViewColumn.Visible = false;

                    var dataGridViewColumn1 = reservationDataGridView.Columns["Reject"];
                    if (dataGridViewColumn1 != null)
                        dataGridViewColumn1.Visible = false;
                }
                else
                {
                    var dataGridViewColumn = reservationDataGridView.Columns["Accept"];
                    if (dataGridViewColumn != null)
                        dataGridViewColumn.Visible = true;

                    var dataGridViewColumn1 = reservationDataGridView.Columns["Reject"];
                    if (dataGridViewColumn1 != null)
                        dataGridViewColumn1.Visible = true;
                }

                DateTime fromDateTime = fromDateTimePicker.Value.Date;
                DateTime toDateTime = toDateTimePicker.Value.Date.AddDays(1);
                toDateTime = toDateTime.AddMinutes(-1);
                ReservationBLL aReservationBll = new ReservationBLL();


                DataTable aDataTable = aReservationBll.GetRestaurantReservationByDate(fromDateTime, toDateTime, reservationType);
                IEnumerable<DataRow> query;
                //if (reservationType != "pending")
                //{
                //    query = (from myRow in aDataTable.AsEnumerable() 
                //             where  (Convert.ToDateTime(myRow.Field<DateTime>("reserved_date")) >= fromDateTime &&
                //             Convert.ToDateTime(myRow.Field<DateTime>("reserved_date")) <= toDateTime)
                //        select myRow);
                //}
                //else
                //{
                //    query = (from myRow in aDataTable.AsEnumerable()
                //             where (Convert.ToDateTime(myRow.Field<DateTime>("reserved_date")) >= fromDateTime)                       
                //              select myRow);               
                //}




                //DataTable boundTable = new DataTable();
                //if (query.Count() > 0)
                //{
                //    boundTable = query.CopyToDataTable<DataRow>();
                //}
               
                int cnt = 0;
                foreach (DataRow row in aDataTable.Rows)
                {
                    cnt++;
                    Reservation aReservation = new Reservation();

                    aReservation.SL = cnt;
                    aReservation.ReserveId =Convert.ToInt32(row["id"]);
                    aReservation.FirstName = row.Field<string>("firstname") + " " + row.Field<string>("lastname");
                    aReservation.reservedDate = Convert.ToDateTime(row["reserved_date"]);
                   // aReservation.reservedDate = Convert.ToDateTime(DateTime.ParseExact(Convert.ToDateTime(row["reserved_date"]).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                   // aReservation.reservedDate = DateTime.ParseExact(value, "MM/yy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                    aReservation.noOfPeople = Convert.ToInt32(row["no_of_people"]);
                    aReservation.Phone = row.Field<string>("phone");
                    aReservation.specialNotes = row.Field<string>("special_notes");
                    aReservation.online_reservation_id = Convert.ToInt32(row["online_reservation_id"]);
                    aReservation.Type = row.Field<string>("reservation_type");
                    aReservation.Arrived = Convert.ToInt32(row["status"]) == 2 ? "Arrived" : "N/A";
                    aReservations.Add(aReservation);

                }

                totalAcceptOrderLabel.Text = "Total Accepted: " + aReservations.Count(a => a.Type == "confirm");
                totalAcceptedOrderPersonlabel.Text = "Accepted Person: " + aReservations.Where(a => a.Type == "confirm").Sum(b => b.noOfPeople);

                totalPendingOrderLabel.Text = "Total Pending: " + aReservations.Count(a => a.Type == "pending");
                totalPendingOrderPersonlabel.Text = "Pending Person: " + aReservations.Where(a => a.Type == "pending").Sum(b => b.noOfPeople);


                reservationDataGridView.AutoGenerateColumns = false;
               
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            reservationDataGridView.DataSource = aReservations;
        }



        private void LoadStatusCombobx()
        {
            List<string> status = new List<string>() { "ALL", "Pending", "Accepted", "Rejected" };
            statusComboBox.DataSource = status;
        }

        private IEnumerable<DataRow> CheckStatus(IEnumerable<DataRow> query)
        {
            if (statusComboBox.Text != "ALL")
            {
                List<DataRow> aRows = new List<DataRow>();
                string status = "1";
                if (statusComboBox.Text == "Pending") status = "0"; 
                foreach (DataRow data in query)
                {

                    if (data["status"].ToString().ToUpper() == status.ToUpper())
                    {
                        aRows.Add(data);
                    }

                }
                query = aRows;
            }
            return query;
        }
      

        private void reservationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                ReservationBLL aReservationBll = new ReservationBLL();

                if (e.RowIndex > -1 && reservationDataGridView.Columns["ReservationId"] != null)
                {
                   
                    int ReservationId = Convert.ToInt32("0" + reservationDataGridView.Rows[e.RowIndex].Cells["ReservationId"].Value);
                    int online_reservation_id = Convert.ToInt32("0" + reservationDataGridView.Rows[e.RowIndex].Cells["online_reservation_id"].Value);

                    if (reservationDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == reservationDataGridView.Rows[e.RowIndex].Cells["Accept"])
                    {
                        ReservationMessageForm.message = "";
                        Reservation aReservation = new Reservation();
                        if (reservationDataGridView.CurrentRow != null)
                        {
                            aReservation = (Reservation)reservationDataGridView.CurrentRow.DataBoundItem;
                        }

                        string message = GetReservationAcceptMessage(aReservation);

                        ReservationMessageForm aReservationMessageForm = new ReservationMessageForm(message, aReservation.ReserveId);
                        DialogResult dr = aReservationMessageForm.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            string postData = "message=" + ReservationMessageForm.message;

                            string url = urls.AcceptUrl + "restaurantcontrol/request/crud/accept_reservation/" + aRestaurantInformation.Id + "/" + online_reservation_id;
                            string result = "";
                            if (online_reservation_id > 0)
                            {
                                result = SendOnlineReservationStatus(ReservationId, postData, url);
                            }
                            else result = online_reservation_id.ToString();


                            if (result == online_reservation_id.ToString())
                            {
                                string res = aReservationBll.UpdateReservation(ReservationId, "confirm", "1");
                                if (res == "Yes")
                                {
                                   // MessageBox.Show("Successfully accepted.");
                                    LoadReservation();
                                    SetAlternateRowColor();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please try again.");
                                LoadReservation();
                                SetAlternateRowColor();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please try again.");
                            LoadReservation();
                            SetAlternateRowColor();
                        }



                    }
                    else if (reservationDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == reservationDataGridView.Rows[e.RowIndex].Cells["Reject"])
                    {

                        Reservation aReservation = new Reservation();
                        if (reservationDataGridView.CurrentRow != null)
                        {
                            aReservation = (Reservation)reservationDataGridView.CurrentRow.DataBoundItem;
                        }

                        string message = GetReservationRejectMessage(aReservation);
                        ReservationMessageForm.message = "";
                        ReservationMessageForm aReservationMessageForm = new ReservationMessageForm(message, aReservation.ReserveId,"reject");
                        DialogResult dr = aReservationMessageForm.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            string postData = "message=" + ReservationMessageForm.message;
                            string url = urls.AcceptUrl + "restaurantcontrol/request/crud/reject_reservation/" +
                                         aRestaurantInformation.Id + "/" + online_reservation_id;
                            string result = "";
                            if (online_reservation_id > 0)
                            {
                                result = SendOnlineReservationStatus(ReservationId, postData, url);
                            }
                            else result = online_reservation_id.ToString();
                            if (result == online_reservation_id.ToString())
                            {
                                string res = aReservationBll.UpdateReservation(ReservationId, "reject", "1");
                                if (res == "Yes")
                                {
                                  //  MessageBox.Show("Successfully rejected.");
                                    LoadReservation();
                                    SetAlternateRowColor();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please try again.");
                                LoadReservation();
                                SetAlternateRowColor();
                            }

                        }
                    }
                    else if (reservationDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == reservationDataGridView.Rows[e.RowIndex].Cells["edit"])
                    {

                        UpdateReservationForm aUpdateReservationForm = new UpdateReservationForm(ReservationId);
                        DialogResult result = aUpdateReservationForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            LoadReservation();
                            SetAlternateRowColor();
                        }



                    }
                    else if (reservationDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == reservationDataGridView.Rows[e.RowIndex].Cells["Arrived"])
                    {
                        string res = aReservationBll.UpdateReservation(ReservationId, "confirm", "2");
                        if (res == "Yes")
                        {
                            LoadReservation();
                            SetAlternateRowColor();
                        }
                    }

                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

    
        }

        private string GetReservationAcceptMessage(Reservation aReservation)
        {
            SetAlternateRowColor();
            string sr = "We are pleased to confirm your booking for " + aReservation.noOfPeople + " people"+ (aReservation.noOfPeople > 1 ? "s":"") + " at " + aReservation.reservedDate.ToShortTimeString() + " on " + aReservation.reservedDate.DayOfWeek.ToString("G") + ", " +  aReservation.reservedDate.ToShortDateString()+".";
            sr += "\r\n\r\nWe look forward to welcoming you and if we can be of further assistance please do not hesitate to contact us.";
            return sr;
        }

        private string GetReservationRejectMessage(Reservation aReservation)
        {
            string sr = "Unfortunately we are fully booked at " + aReservation.reservedDate.ToShortTimeString() + " on " + aReservation.reservedDate.DayOfWeek.ToString("G") + ", " + aReservation.reservedDate.ToShortDateString() + ". However, we would be able to take booking from or later." +
                        " Sorry for the inconvenience caused. Hope to see you next time. If we can be of further assistance please do not hesitate to contact us.";
            return sr;
        }


        private string SendOnlineReservationStatus(int ReservationId, string data, string url)
        {
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            Uri target = new Uri(url);
            WebRequest request = WebRequest.Create(target);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            string result = string.Empty;
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

            return result;
           // return Convert.ToString(ReservationId);
        }

        private void reservationDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            var dataGridViewColumn = reservationDataGridView.Columns["ReservationId"];
            if (dataGridViewColumn != null)
            {
               dataGridViewColumn.Visible = false;
            } 

            var dataGridViewColumn1 = reservationDataGridView.Columns["Accept"];
            if (dataGridViewColumn1 != null)
                dataGridViewColumn1.DisplayIndex = 8;
            var dataGridViewColumn2 = reservationDataGridView.Columns["Reject"];
            if (dataGridViewColumn2 != null)
                dataGridViewColumn2.DisplayIndex = 9; 

            var dataGridViewColumn4 = reservationDataGridView.Columns["Status"];
            if (dataGridViewColumn4 != null) dataGridViewColumn4.Visible = false;
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReservationTable_Load(object sender, EventArgs e)
        {
            LoadReservation();
            SetAlternateRowColor();
        }

        private void newReservationButton_Click(object sender, EventArgs e)
        {
            AddReservationForm addReservationForm=new AddReservationForm();
            DialogResult result=  addReservationForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadReservation();
                SetAlternateRowColor();
            }
        }

        public void SetAlternateRowColor()
        {
            for (int iCount = 0; iCount < reservationDataGridView.Rows.Count; iCount++)
            {
                if (Convert.ToString(reservationDataGridView.Rows[iCount].Cells[7].Value) == "pending")
                    reservationDataGridView.Rows[iCount].DefaultCellStyle.BackColor = Color.Orange;
                else if (Convert.ToString(reservationDataGridView.Rows[iCount].Cells[7].Value) == "confirm")
                    reservationDataGridView.Rows[iCount].DefaultCellStyle.BackColor = Color.GreenYellow;
                else
                    reservationDataGridView.Rows[iCount].DefaultCellStyle.BackColor = Color.Red;
            }
        }


    }
}