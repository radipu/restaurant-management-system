using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using TomaFoodRestaurant.DAL.DAO;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class Refund : Form
    {
        public int orderID = 0;
        public double orderAmount = 0;
        public string requestReason = "requested_by_customer";
        public bool paymentStatus = false;
        public Refund()
        {
            InitializeComponent();
        }
        public Refund(double amount=0,int order_id=0)
        {
            InitializeComponent();
            orderID = order_id;
            textBoxAmount.Text = amount.ToString();
            orderAmount = amount;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            orderAmount = Convert.ToDouble(textBoxAmount.Text);
            requestReason = comboBoxReason.Text.ToLower();
            if (requestReason.Contains(" ")) {
                requestReason = requestReason.Replace(" ", "_");
            }
            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            string payment = aRestaurantOrderDao.getOrderPayment(orderID);
          //  dynamic data = JObject.Parse(payment);
            if (paymentSendToServer(payment, orderID))
            {
                paymentStatus = true;
                MessageBox.Show("Successfully refunded.");
                this.Close();
            }
            else {
                MessageBox.Show("Please try again after sometime."); 
            }
        }

        public bool paymentSendToServer(string refNo, int orderId)
        {
            string text = "";
            try
            {
                var stripeData = new {
                    stripeToken = Properties.Settings.Default.StripeAPI,
                    paymentReference = refNo,
                    stripeAccount = Properties.Settings.Default.accountId,
                    orderId = orderId.ToString(),
                    refundAmount = (orderAmount * 100).ToString(),
                    refundApplicationFee = "false",
                    refundReason = requestReason,
                    webhookUrl= GlobalVars.hostUrl + "restaurantcontrol/order/refund_order"
                };

                string postString = JsonConvert.SerializeObject(stripeData);

                Uri target = new Uri("https://europe-west2-ginilab.cloudfunctions.net/app/api/payment/stripe/refund");
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
                if (data.status == "succeeded")
                {
                    MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                    aRestaurantOrderDao.updateOrderPayment(orderID, Convert.ToString(data.id),orderAmount);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

          

            //  string json = text;

            return true;
        }

    }
}
