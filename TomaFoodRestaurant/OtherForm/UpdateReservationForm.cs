using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class UpdateReservationForm : Form
    {
        private string message = "";
        Reservation aReservation = new Reservation();
        public UpdateReservationForm(int reservationId)
        {
            InitializeComponent();
            ReservationBLL aReservationBll = new ReservationBLL();
            aReservation = aReservationBll.GetBookingByBookingId(reservationId);
            numberPadUs1.ControlToInputText = firstNameTextBox;


        }

        private void LoadRerservationTimeCombobox()
        {
            List<string> reservationTime = new List<string>();


            DateTime lowerTime = new DateTime(2016, 12, 12, 17, 0, 0);
            DateTime upperTime = new DateTime(2016, 12, 13, 0, 0, 0);


            while (lowerTime < upperTime)
            {

                string singleTime = lowerTime.ToString("HH:mm");
                reservationTime.Add(singleTime);
                lowerTime = lowerTime.AddMinutes(15);
            }

            reservationTimeComboBox.DataSource = reservationTime;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        public bool IsValidEmail(string emailaddress)
        {
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(emailaddress, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (VallidForm())
                {

                    aReservation.Email = emailTextBox.Text.Trim();
                    aReservation.FirstName = firstNameTextBox.Text.Trim();
                    aReservation.LastName = lastNameTextBox.Text.Trim();
                    aReservation.Phone = contactNumberTextBox.Text.Trim();
                    TimeSpan time = TimeSpan.Parse(reservationTimeComboBox.Text);
                    aReservation.noOfPeople = Convert.ToInt32(noOfPersontextBox.Text.Trim());
                    aReservation.reservedDate = new DateTime(reservationDateTimePicker1.Value.Date.Year, reservationDateTimePicker1.Value.Date.Month,
                        reservationDateTimePicker1.Value.Date.Day, time.Hours, time.Minutes, 00);
                    aReservation.specialNotes = specialNotesTextBox.Text.Trim();
                    ReservationBLL aReservationBll = new ReservationBLL();
                    string result = aReservationBll.UpdateLocalReservation(aReservation);
                    if (result == "Reservation has been updated sucessfully.")
                    {
                        //MessageBox.Show(result);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show(result);
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

        private void UpdateReservationForm_Load(object sender, EventArgs e)
        {

            LoadRerservationTimeCombobox();
            LoadExistingInformation();

        }

        private void LoadExistingInformation()
        {
            try
            {
                firstNameTextBox.Text = aReservation.FirstName;
                lastNameTextBox.Text = aReservation.LastName;
                emailTextBox.Text = aReservation.Email;
                contactNumberTextBox.Text = aReservation.Phone;
                reservationDateTimePicker1.Value = aReservation.reservedDate;
                reservationTimeComboBox.SelectedIndex = reservationTimeComboBox.FindStringExact(aReservation.reservedDate.ToString("HH:mm"));
                noOfPersontextBox.Text = aReservation.noOfPeople.ToString();
                specialNotesTextBox.Text = aReservation.specialNotes;
            }
            catch (Exception)
            {

            }
 
        }

        private void reservationDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void updateAndMailButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (VallidForm())
                {

                    aReservation.Email = emailTextBox.Text.Trim();
                    aReservation.FirstName = firstNameTextBox.Text.Trim();
                    aReservation.LastName = lastNameTextBox.Text.Trim();
                    aReservation.Phone = contactNumberTextBox.Text.Trim();
                    TimeSpan time = TimeSpan.Parse(reservationTimeComboBox.Text);
                    aReservation.noOfPeople = Convert.ToInt32(noOfPersontextBox.Text.Trim());
                    aReservation.reservedDate = new DateTime(reservationDateTimePicker1.Value.Date.Year, reservationDateTimePicker1.Value.Date.Month,
                        reservationDateTimePicker1.Value.Date.Day, time.Hours, time.Minutes, 00);
                    aReservation.specialNotes = specialNotesTextBox.Text.Trim();


                    ReservationBLL aReservationBll = new ReservationBLL();
                    if (OthersMethod.CheckForInternetConnection() && aReservation.online_reservation_id > 0)
                    {
                        int onlineReservationId = aReservationBll.SendReservationDataToWeb(aReservation);
                    }

                    string result = aReservationBll.UpdateLocalReservation(aReservation);
                    if (result == "Reservation has been updated sucessfully.")
                    {
                        MessageBox.Show(result);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show(result);
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


     
    }
}
