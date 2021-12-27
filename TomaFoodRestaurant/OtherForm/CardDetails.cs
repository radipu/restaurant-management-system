using Stripe;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model; 

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CardDetails : Form
    {
        double TotalOrderAmount = 0;
        public RestaurantOrder restaurantOrder;
        public bool PaymentTransferd = false;
        public string PaymentTransferId = "";
        public StripeDetails stripeDetails = new StripeDetails();
        GlobalUrl urls = new GlobalUrl();
        public CardDetails(RestaurantOrder order)
        {
            
            InitializeComponent();
            restaurantOrder = order;
            if (restaurantOrder.TotalCost.ToString("F02").Contains("£"))
            {
                string[] amounts = restaurantOrder.TotalCost.ToString("F02").Split('£');
                TotalOrderAmount = Convert.ToDouble(amounts[1]);
            }
            else {
                TotalOrderAmount = Convert.ToDouble(restaurantOrder.TotalCost);
            }
            submitButton.Text = "£" + TotalOrderAmount.ToString("F02") + " Pay";
            cardHoldertextBox.Text = restaurantOrder.CustomerName;
            textBoxEmail.Text = restaurantOrder.CustomerEmail;
            zipCodetextBox.Text = restaurantOrder.PostCode;
            
            comboBoxMonth.SelectedIndex = 0;
            comboBoxYear.SelectedIndex = 0;
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();

            //StripeConfiguration.SetApiKey(Properties.Settings.Default.StripeAPI);
             
        }
        
        private void EXITButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (validFrm())
            {
                try
                {

                    labelError.ForeColor = Color.Green;
                    labelError.Text = "Connecting Stripe.Please wait...";
                    StripeConfiguration.SetApiKey(Properties.Settings.Default.StripeAPI);
                    string key = Properties.Settings.Default.StripeAPI;
                    var chargeservice = new StripeChargeService(key);
                    int chargetotal = Convert.ToInt32((TotalOrderAmount * 100)); 

                    var tokenOptions = new StripeTokenCreateOptions()
                    {
                        Card = new StripeCreditCardOptions()
                        {
                            Number = cardNumbertextBox.Text,
                            ExpirationYear = Convert.ToInt16(comboBoxYear.Text),
                            ExpirationMonth = Convert.ToInt16(comboBoxMonth.Text),
                            Cvc = CVCtextBox.Text,
                            AddressLine1 = cardHoldertextBox.Text != null ? cardHoldertextBox.Text : "",
                            AddressLine2 = textBoxEmail.Text != null ? textBoxEmail.Text : "",
                            AddressZip = zipCodetextBox.Text != null ? zipCodetextBox.Text : "",
                            AddressCountry = "UK"
                        },
                        // CustomerId = customer.Id
                    };
                    var tokenService = new StripeTokenService();
                    StripeToken stripeToken = tokenService.Create(tokenOptions);
                    //stripeToken
                    var mycharge = new StripeChargeCreateOptions();
                    mycharge.Amount = chargetotal;
                    mycharge.Currency = "GBP";

                    if (Properties.Settings.Default.accountId != "")
                    {
                        mycharge.ApplicationFee = (int)(Properties.Settings.Default.applicationFees * 100);
                        if (Properties.Settings.Default.feeType == "percentage")
                        {
                            double newCharge = GlobalVars.numberRound((Properties.Settings.Default.applicationFees / 100) * (chargetotal / 100), 2);
                            mycharge.ApplicationFee = (int)(newCharge * 100);
                        }
                    }


                    // mycharge.Amoun = Properties.Settings.Default.accountId;
                    mycharge.Description = "TPOS payment at " + GlobalSetting.RestaurantInformation.RestaurantName + (cardHoldertextBox.Text != null ? cardHoldertextBox.Text : "") + " " + (textBoxEmail.Text != null ? textBoxEmail.Text : "") + " " + (zipCodetextBox.Text != null ? zipCodetextBox.Text : "");


                    mycharge.StatementDescriptor = GlobalSetting.RestaurantInformation.RestaurantName;
                    //mycharge.CustomerId = customer.Id;
                    if (textBoxEmail.Text.Length > 7)
                    {
                        mycharge.ReceiptEmail = textBoxEmail.Text;
                    }
                    mycharge.SourceTokenOrExistingSourceId = stripeToken.Id;

                    if (Properties.Settings.Default.accountId != "")
                    {

                        var requestOption = new StripeRequestOptions();
                        requestOption.StripeConnectAccountId = Properties.Settings.Default.accountId;
                        StripeCharge currentcharge = chargeservice.Create(mycharge, requestOption);
                        if (currentcharge.Status == "succeeded")
                        {
                            //var stripeData = new { charge = currentcharge.Id, payment_intent = "", refund_id = "" };
                            //OrderPayments insertArray = new OrderPayments();
                            //insertArray.order_id = restaurantOrder.Id;
                            //insertArray.payment_reference = JsonConvert.SerializeObject(stripeData);
                            //MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                            //aRestaurantOrderDao.InsertOrderPayment(insertArray);
                            PaymentTransferId = currentcharge.Id;
                            MessageBox.Show("Payment Successful.");
                            PaymentTransferd = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        StripeCharge currentcharge = chargeservice.Create(mycharge);
                        if (currentcharge.Status == "succeeded")
                        {
                            PaymentTransferId = currentcharge.Id;

                            MessageBox.Show("Payment Successful.");
                            PaymentTransferd = true;
                            this.Close();
                        }
                    }
                    // labelError.Text = currentcharge.Status;
                }
                catch (StripeException ex)
                {
                    labelError.ForeColor = Color.Red;
                    labelError.Text = ex.Message;
                }
            }



        }

        //private void submitButton_Click(object sender, EventArgs e)
        //{
        //    if(validFrm())
        //    {
        //        try
        //        {

        //            //labelError.ForeColor = Color.Green;
        //            //labelError.Text = "Connecting Stripe.Please wait...";
        //            //StripeConfiguration.SetApiKey(Properties.Settings.Default.StripeAPI);
        //            //string key = Properties.Settings.Default.StripeAPI;
        //            //var chargeservice = new StripeChargeService(key);
        //            ////var mycust = new StripeCustomerCreateOptions();
        //            ////mycust.Email = textBoxEmail.Text == null ? textBoxEmail.Text : "";
        //            ////mycust.Description = cardHoldertextBox.Text + " (" + textBoxEmail.Text + ")";
        //            ////var customerservice = new StripeCustomerService();
        //            ////StripeCustomer customer =  customerservice.Create(mycust);

        //            //var tokenOptions = new StripeTokenCreateOptions()
        //            //{
        //            //    Card = new StripeCreditCardOptions()
        //            //    {
        //            //        Number = cardNumbertextBox.Text,
        //            //        ExpirationYear = Convert.ToInt16(comboBoxYear.Text),
        //            //        ExpirationMonth = Convert.ToInt16(comboBoxMonth.Text),
        //            //        Cvc = CVCtextBox.Text,
        //            //        AddressLine1 = cardHoldertextBox.Text != null ? cardHoldertextBox.Text : "",
        //            //        AddressLine2 = textBoxEmail.Text != null ? textBoxEmail.Text : "",
        //            //        AddressZip = zipCodetextBox.Text != null ? zipCodetextBox.Text : "",
        //            //        AddressCountry = "UK"
        //            //    },
        //            //    // CustomerId = customer.Id
        //            //};
        //            //var tokenService = new StripeTokenService();
        //            //StripeToken stripeToken = tokenService.Create(tokenOptions);
        //            ////stripeToken
        //            //var mycharge = new StripeChargeCreateOptions();
        //            //mycharge.Amount = chargetotal;
        //            //mycharge.Currency = "GBP";

        //            //if(Properties.Settings.Default.accountId != "")
        //            //{
        //            //    mycharge.ApplicationFee = (int)(Properties.Settings.Default.applicationFees * 100);
        //            //    if (Properties.Settings.Default.feeType == "percent")
        //            //    {
        //            //        double newCharge = GlobalVars.numberRound((Properties.Settings.Default.applicationFees / 100) * (chargetotal / 100), 2);
        //            //        mycharge.ApplicationFee = (int)(newCharge * 100);
        //            //    }
        //            //}                    


        //            //// mycharge.Amoun = Properties.Settings.Default.accountId;
        //            //mycharge.Description = "TPOS ORDER PAYMENT " + (cardHoldertextBox.Text != null ? cardHoldertextBox.Text : "") + " " + (textBoxEmail.Text != null ? textBoxEmail.Text : "") + " " + (zipCodetextBox.Text != null ? zipCodetextBox.Text : "");
        //            ////mycharge.CustomerId = customer.Id;
        //            //if (textBoxEmail.Text.Length > 7)
        //            //{
        //            //    mycharge.ReceiptEmail = textBoxEmail.Text;
        //            //}
        //            //mycharge.SourceTokenOrExistingSourceId = stripeToken.Id;

        //            //if (Properties.Settings.Default.accountId != "")
        //            //{

        //            //    var requestOption = new StripeRequestOptions();
        //            //    requestOption.StripeConnectAccountId = Properties.Settings.Default.accountId;
        //            //    StripeCharge currentcharge = chargeservice.Create(mycharge, requestOption);
        //            //    if (currentcharge.Status == "succeeded")
        //            //    {
        //            //        //var stripeData = new { charge = currentcharge.Id, payment_intent = "", refund_id = "" };
        //            //        //OrderPayments insertArray = new OrderPayments();
        //            //        //insertArray.order_id = restaurantOrder.Id;
        //            //        //insertArray.payment_reference = JsonConvert.SerializeObject(stripeData);
        //            //        //MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
        //            //        //aRestaurantOrderDao.InsertOrderPayment(insertArray);
        //            //        PaymentTransferId = currentcharge.Id;
        //            //        MessageBox.Show("Payment Successful.");
        //            //        PaymentTransferd = true;
        //            //        this.Close();
        //            //    }
        //            //}
        //            //else {
        //            //    StripeCharge currentcharge = chargeservice.Create(mycharge);
        //            //    if (currentcharge.Status == "succeeded")
        //            //    {
        //            //        PaymentTransferId = currentcharge.Id;
        //            //        MessageBox.Show("Payment Successful.");
        //            //        PaymentTransferd = true;
        //            //        this.Close();
        //            //    }
        //            //}
        //            // labelError.Text = currentcharge.Status;

        //            //var stripeRequest = new RequestOptions();
        //            //stripeRequest.ApiKey = Properties.Settings.Default.StripeAPI;

        //            //stripeRequest.
        //            //var client = new StripeClient();

        //            int chargetotal = Convert.ToInt32((TotalOrderAmount * 100));

        //            StripeConfiguration.ApiKey = Properties.Settings.Default.StripeAPI;

        //            var options = new PaymentMethodCreateOptions
        //            {
        //                Type = "card",

        //                Card = new PaymentMethodCardOptions
        //                {
        //                    Number = cardNumbertextBox.Text,
        //                    ExpYear = Convert.ToInt16(comboBoxYear.Text),
        //                    ExpMonth = Convert.ToInt16(comboBoxMonth.Text),
        //                    Cvc = CVCtextBox.Text,

        //                },


        //            };

        //            var service = new PaymentMethodService();
        //            PaymentMethod paymentMethod = service.Create(options);
        //            //if acc available then add connected acc info 


        //            //else 
        //            var intentOption = new PaymentIntentCreateOptions();

        //            intentOption.Amount = chargetotal;


        //            if (Properties.Settings.Default.accountId != "")
        //            {
        //                double newCharge = (Properties.Settings.Default.applicationFees * 100);
        //                if (Properties.Settings.Default.feeType == "percent")
        //                {
        //                    newCharge = GlobalVars.numberRound((Properties.Settings.Default.applicationFees / 100) * (chargetotal / 100), 2);
        //                    // intentOption.ApplicationFeeAmount = (int)(newCharge * 100);

        //                }

        //                chargetotal = (int)(chargetotal - (newCharge));
        //                //  intentOption.OnBehalfOf = Properties.Settings.Default.accountId;
        //                intentOption.TransferData = new PaymentIntentTransferDataOptions
        //                {
        //                    Amount = chargetotal,
        //                    Destination = Properties.Settings.Default.accountId,

        //                };

        //            }
        //            intentOption.StatementDescriptor = GlobalSetting.RestaurantInformation.RestaurantName;
        //            intentOption.Currency = "gbp";
        //            intentOption.PaymentMethodTypes = new List<string>
        //                {
        //                    "card",
        //                };
        //            intentOption.PaymentMethod = paymentMethod.Id;
        //            intentOption.Description = "TPOS order at " + GlobalSetting.RestaurantInformation.RestaurantName;

        //            var piServeice = new PaymentIntentService();
        //            PaymentIntent paymentIntent = piServeice.Create(intentOption);

        //            paymentIntent = piServeice.Confirm(paymentIntent.Id);

        //            PaymentTransferId = paymentIntent.Id;
        //            MessageBox.Show("Payment Successful.");
        //            PaymentTransferd = true;
        //            this.Close();



        //        }
        //        catch (StripeException ex)
        //        {
        //            labelError.ForeColor = Color.Red;
        //            labelError.Text = ex.Message;
        //        }
        //    }
        //}

        private bool validFrm()
        {
            bool error = true;

            //if (cardHoldertextBox.Text.Length < 5)
            //{
            //    error = false;
            //    labelError.Text = "Please enter card holder name.";
            //}
            if (cardNumbertextBox.Text.Length < 15)
            {
                error = false;
                labelError.Text = "Please enter valid card No.";
            }
            else if (CVCtextBox.Text == "")
            {
                error = false;
                labelError.Text = "Please enter CVC Number.";
            }
            else if (comboBoxMonth.Text == "" || comboBoxYear.Text == "")
            {
                error = false;
                labelError.Text = "Please select expire date.";
            }
            //else if (textBoxEmail.Text == "")
            //{
            //    error = false;
            //    labelError.Text = "Please enter valid email address.";
            //}

            return error;
        }

        private void CardDetails_Load(object sender, EventArgs e)
        {
            StripeConfiguration.SetApiKey(Properties.Settings.Default.StripeAPI);

        }

        private void cardNumbertextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!System.Windows.Forms.Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 350);
                    NumberForm aNumberPad = new NumberForm(aPoint);
                    aNumberPad.ShowDialog();

                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void zipCodetextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 450);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.ShowDialog();

                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void cardHoldertextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 450);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.ShowDialog();
                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void textBoxEmail_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 450);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.ShowDialog();
                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void CVCtextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 450);
                    NumberForm aNumberPad = new NumberForm(aPoint);
                    aNumberPad.ShowDialog();
                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

    }
}
