using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class ReservationMessageForm : Form
    {
        public static string message = "";
        public static Int32 reservationId = 0;
        public static string frmType = "confirm";

        public ReservationMessageForm(string sr, Int32 reservation_id, string _frmType = "confirm")
        {
            InitializeComponent();
            sendEmailTextBox.Text = sr;
            reservationId = reservation_id;
            frmType = _frmType;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool returnValue = true;
            //string refNo = "";
            //MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            //OrderPayments payment = aRestaurantOrderDao.getOrderPaymentDetailsByreservationId(reservationId);
            //if (payment.Id > 0)
            //{
            //    refNo = payment.payment_reference;
            //    if (frmType == "confirm")
            //    {
            //        returnValue = paymentSendToServer(refNo);
            //    }
            //    else
            //    {
            //        returnValue = paymentSendToServer(refNo, "reject");
            //    }
            //}
            if (sendEmailTextBox.Text.Trim().Length > 0)
            {
                message = sendEmailTextBox.Text.Trim();
               
                this.DialogResult = DialogResult.Cancel;
                if (returnValue)
                    {
                        this.DialogResult = DialogResult.OK;
                    } 
                this.Close();
            }
        }


        public bool paymentSendToServer(string refNo,string type="confirm")
        {
            string text = "";
            try
            {
                var stripeData = new
                {
                    stripeToken = Properties.Settings.Default.StripeAPI,
                    paymentReference = refNo,
                    stripeAccount = Properties.Settings.Default.accountId 
                };

                string postString = JsonConvert.SerializeObject(stripeData);
                Uri target = new Uri("https://europe-west2-ginilab.cloudfunctions.net/app/api/payment/stripe/capture");
                if (type != "confirm")
                {
                    target = new Uri("https://europe-west2-ginilab.cloudfunctions.net/app/api/payment/stripe/capture?action=cancel");
                }
                var request = (HttpWebRequest)WebRequest.Create(target);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = postString.Length; 

                request.Headers.Add("gini-ApiKey", GlobalVars.giniApi);
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        text = reader.ReadToEnd();
                    }
                }

                dynamic data = JObject.Parse(text);               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }



        private void ReservationMessageForm_Load(object sender, EventArgs e)
        {
            //MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            //OrderPayments  payment = aRestaurantOrderDao.getOrderPaymentDetailsByreservationId(reservationId);
            //if (payment.Id > 0)
            //{
            //    if (frmType == "confirm")
            //    {
            //        saveButton.Text = "Capture £" + payment.booking_amount + " and send email.";
            //    }
            //    else
            //    {

            //        saveButton.Text = "Reject £" + payment.booking_amount + " and send email.";
            //    }
            //}

        }

        private void sendEmailTextBox_TextChanged(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = sendEmailTextBox;
        }
    }
}
