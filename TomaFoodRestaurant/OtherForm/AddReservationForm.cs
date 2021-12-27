using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class AddReservationForm : Form
    {
        private string message = "";

        public AddReservationForm()
        {
            InitializeComponent();
            numberPadUs1.ControlToInputText = firstNameTextBox;
        }

        private void AddReservationForm_Load(object sender, EventArgs e)
        {
            LoadRerservationTimeCombobox();
            GetEmailText();
        }

        private void LoadRerservationTimeCombobox()
        {
            List<string> reservationTime = new List<string>();

            DateTime lowerTime=new DateTime(2016,12,12,17,0,0);
            DateTime upperTime=new DateTime(2016,12,13,0,0,0);

            while(lowerTime<upperTime)
            {              
                string singleTime = lowerTime.ToString("HH:mm");
                reservationTime.Add(singleTime);
                lowerTime = lowerTime.AddMinutes(15);
            }

            reservationTimeComboBox.DataSource = reservationTime;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
          this.DialogResult=DialogResult.No;
            this.Close();
        }

        public bool IsValidEmail(string strIn)
        {
            //try
            //{
            //    var m = new MailAddress(emailaddress);
            //    return m.Address == emailaddress;
            //    return true;
            //}
            //catch (FormatException)
            //{
            //    return false;
            //}
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(strIn, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (VallidForm())
                {
                    Reservation aReservation = new Reservation();
                    aReservation.Email = emailTextBox.Text.Trim();
                    aReservation.FirstName = firstNameTextBox.Text.Trim();
                    aReservation.LastName = lastNameTextBox.Text.Trim();
                    aReservation.Phone = contactNumberTextBox.Text.Trim();
                    aReservation.RestaurantId = GlobalSetting.RestaurantInformation.Id;
                    aReservation.Status = 1;
                    aReservation.Type = "confirm";
                    TimeSpan time = TimeSpan.Parse(reservationTimeComboBox.Text);
                    aReservation.noOfPeople = Convert.ToInt32(noOfPersontextBox.Text.Trim());
                    aReservation.reservedDate = new DateTime(reservationDateTimePicker1.Value.Date.Year, reservationDateTimePicker1.Value.Date.Month,
                        reservationDateTimePicker1.Value.Date.Day, time.Hours, time.Minutes, 00);
                    aReservation.restaurantId = GlobalSetting.RestaurantInformation.Id;
                    aReservation.specialNotes = specialNotesTextBox.Text.Trim();
                    aReservation.EmailText = sendEmailTextBox.Text.Trim();
                    aReservation.ReservationDate = DateTime.Now;
                    aReservation.online_reservation_id = 0;
                    ReservationBLL aReservationBll = new ReservationBLL();
                    int id = aReservationBll.InsertOnlineReservation(aReservation, false);
                    if (id > 0)
                    {
                        MessageBox.Show("Reservation has been save successfully.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something wrong! please try again.");
                    }
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private bool VallidForm()
        {
            int person;
            if (firstNameTextBox.Text.Trim().Length <= 0 && lastNameTextBox.Text.Trim().Length <= 0)
            {
                message = "Please write down customer name.";
                return false;
            }
            //if (!IsValidEmail(emailTextBox.Text.Trim()))
            //{
            //    message = "Please write down valid email address.";
            //    return false;
            //}
            if (contactNumberTextBox.Text.Trim().Length <= 0)
            {
                message = "Please write down contact number.";
                return false;
            }
            if (!int.TryParse(noOfPersontextBox.Text.Trim(), out person))
            {
                message = "Please write down number of person.";
                return false;
            }

            if (reservationTimeComboBox.Text.Trim().Length <= 0)
            {
                message = "Please select reservation time.";
                return false;
            }

            return true;
        }

        private void saveAndMailButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (VallidForm())
                {
                    Reservation aReservation = new Reservation();
                    aReservation.Email = emailTextBox.Text.Trim();
                    aReservation.FirstName = firstNameTextBox.Text.Trim();
                    aReservation.LastName = lastNameTextBox.Text.Trim();
                    aReservation.Phone = contactNumberTextBox.Text.Trim();
                    aReservation.RestaurantId = GlobalSetting.RestaurantInformation.Id;
                    aReservation.Status = 0;
                    aReservation.Type = "confirm";
                    TimeSpan time = TimeSpan.Parse(reservationTimeComboBox.Text);
                    aReservation.noOfPeople = Convert.ToInt32(noOfPersontextBox.Text.Trim());
                    aReservation.reservedDate = new DateTime(reservationDateTimePicker1.Value.Date.Year, reservationDateTimePicker1.Value.Date.Month,
                        reservationDateTimePicker1.Value.Date.Day, time.Hours, time.Minutes, 00);
                    aReservation.restaurantId = GlobalSetting.RestaurantInformation.Id;
                    aReservation.specialNotes = specialNotesTextBox.Text.Trim();
                    aReservation.EmailText = sendEmailTextBox.Text.Trim();
                    aReservation.ReservationDate = DateTime.Now;

                    ReservationBLL aReservationBll = new ReservationBLL();


                    if (OthersMethod.CheckForInternetConnection())
                    {
                        int onlineReservationId = aReservationBll.SendReservationDataToWeb(aReservation);
                        aReservation.online_reservation_id = onlineReservationId;
                    }
                    else aReservation.online_reservation_id = 0;

                    int id = aReservationBll.InsertOnlineReservation(aReservation, false);
                    if (id > 0)
                    {
                       // MessageBox.Show("Reservation has been save successfully.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Something wrong! please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Please check all input filed");
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }       
        }      

        private void reservationTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmailText();
        }

        private void GetEmailText()
        {
            string time = reservationTimeComboBox.Text;
            string day = reservationDateTimePicker1.Value.ToString("dddd");
            string date = reservationDateTimePicker1.Value.ToShortDateString();
            string dataText = "We are pleased to confirm your booking for 1 people at " + time + " on " + day + ", " + date + "." + ""
                              + "\nWe look forward to welcoming you and if we can be of further assistance please do not hesitate to contact us.";
            sendEmailTextBox.Text = dataText.Replace("\n", Environment.NewLine);
        }

        private void reservationDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GetEmailText();
        }

        private void firstNameTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = firstNameTextBox;
        }

        private void lastNameTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = lastNameTextBox;
        }

        private void emailTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = emailTextBox;
        }

        private void contactNumberTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = contactNumberTextBox;
        }

        private void noOfPersontextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = noOfPersontextBox;
        }

        private void specialNotesTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = specialNotesTextBox;
        }

        private void sendEmailTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = sendEmailTextBox;
        }
    }    
}