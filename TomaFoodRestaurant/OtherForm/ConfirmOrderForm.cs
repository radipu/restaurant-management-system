using FastFoodManagementSystem.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class ConfirmOrderForm : Form
    {
        double TotalOrderAmount = 0;
        public static PaymentDetails PaymentDetails=new PaymentDetails();
       
        private double OrderTotal = 0;
        GlobalUrl urls = new GlobalUrl();
        OthersMethod  aOthersMethod=new OthersMethod();
        public RestaurantOrder restaurantOrder;
        int screenHeight = 0;
        public ConfirmOrderForm(RestaurantOrder order)
        {
            
            InitializeComponent();
            restaurantOrder = order;

            string amount = "£" + GlobalVars.numberRound(order.TotalCost, 2).ToString("F2");
            if (amount.Contains("£"))
            {
                orderTotalLabel.Text = amount;
                string[] amounts = amount.Split('£');
                
                TotalOrderAmount = GlobalVars.numberRound(Convert.ToDouble(amounts[1]), 2); 
                OrderTotal = GlobalVars.numberRound(Convert.ToDouble(amounts[1]), 2);
            }
            else {
                TotalOrderAmount = GlobalVars.numberRound(Convert.ToDouble(amount), 2);
                OrderTotal = GlobalVars.numberRound(Convert.ToDouble(amount), 2);
                orderTotalLabel.Text = "£"+ GlobalVars.numberRound(Convert.ToDouble(amount), 2); 
            }
         
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            PaymentDetails.Status = "CANCEL";
            screenHeight = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height - 450);
                                                                                                             
        }

        private void LoadTotalAmount(bool staus)
        {
            if (!staus)
            {
                double cardFee = OrderTotal >= GlobalSetting.RestaurantInformation.CardMinOrder ? 0 : GlobalSetting.RestaurantInformation.CardFee;
                TotalOrderAmount = OrderTotal;

            }
            else
            {
                double cardFee = OrderTotal >= GlobalSetting.RestaurantInformation.CardMinOrder ? 0 : GlobalSetting.RestaurantInformation.CardFee;
                TotalOrderAmount = OrderTotal + cardFee;
            }
            orderTotalLabel.Text = "£" + TotalOrderAmount.ToString("F2");

            TotalOrderAmount = GlobalVars.numberRound(Convert.ToDouble(TotalOrderAmount), 2);

        }


        private void cashTextBox_Enter(object sender, EventArgs e)
        {
            if (cashTextBox.Text == "CASH")
            {
                cashTextBox.Text = "";
                paidAmountTextBox.Text = "0";
                cardPaymentButton.BackColor = Color.LightSkyBlue;
            }
        }

        private void cashTextBox_Leave(object sender, EventArgs e)
        {
            if (cashTextBox.Text == "" && cardTextBox.Text == "CARD")
            {
                cashTextBox.Text = "CASH";
            }
        }

        private void cardTextBox_Enter(object sender, EventArgs e)
        {
            if (cardTextBox.Text == "CARD")
            {
                cardTextBox.Text = "";
                paidAmountTextBox.Text = "0";
                cardPaymentButton.BackColor = Color.LightSkyBlue;
                
            }
        }

        private void cardTextBox_Leave(object sender, EventArgs e)
        {
            if (cardTextBox.Text == "" && cashTextBox.Text == "CASH")
            {
                cardTextBox.Text = "CARD";
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {

            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();
            paidAmountTextBox.Text = "0";
            cardTextBox.Text = "CARD";
            cashTextBox.Text = "CASH";
            cardPaymentButton.BackColor = Color.LightSkyBlue;
        }

        private void exactButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();
            paidAmountTextBox.Text = GlobalVars.numberRound(TotalOrderAmount, 2).ToString("F2");
            cardTextBox.Text = "CARD";
            cashTextBox.Text = "CASH";
            cardPaymentButton.BackColor = Color.LightSkyBlue;
        }

        private void paidAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            double paidAmount = TotalOrderAmount - GlobalVars.numberRound(Convert.ToDouble(paidAmountTextBox.Text), 2);
            changeAmountLabel.Text = "£" + (paidAmount < 0 ? paidAmount.ToString("F2") : "0");
        }

        private void CoinButton_Click(object sen1, EventArgs e)
        {
            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();
            Button aButton = sen1 as Button;
            string totalAmount = aButton.Text;
            string[] amounts = totalAmount.Split('£');
            paidAmountTextBox.Text = (Convert.ToDouble(amounts[1]) + Convert.ToDouble("0" + paidAmountTextBox.Text)).ToString("F2");
            cardPaymentButton.BackColor = Color.LightSkyBlue;

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();


            PaymentDetails.PaymentMethod = "Cash";
            PaymentDetails.CardAmount = 0;
            PaymentDetails.CashAmount = 0;
            PaymentDetails.ChangeAmount = 0;
            PaymentDetails.Status = "CANCEL";
            this.Close();
        }

        private void cashTextBox_TextChanged(object sender, EventArgs e)
        {
            double amount;
            if (double.TryParse("0" + cashTextBox.Text.Trim(), out amount)) {

                double cardAmount = TotalOrderAmount - amount;
                if (cardAmount == 0)
                {
                    cardTextBox.Text = "0";
                }
                else
                {
                    cardTextBox.Text = cardAmount.ToString("F2");

                    changeAmountLabel.Text = "£0";
                }
                  
            
            }
        }

        private void cardTextBox_TextChanged(object sender, EventArgs e)
        {
            double amount;
            if (double.TryParse("0" + cardTextBox.Text.Trim(), out amount))
            {

                double cashAmount = TotalOrderAmount - amount;
                if (cashAmount == 0)
                {
                    cashTextBox.Text = "0";
                }
                else
                {
                    cashTextBox.Text = GlobalVars.numberRound(cashAmount, 2).ToString("F2");
                    // cashTextBox.Text = cashAmount.ToString();
                    changeAmountLabel.Text = "£0";
                }
              

            }
        }

        private void cardPaymentButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();
            cardPaymentButton.BackColor = Color.Black;
            paidAmountTextBox.Text = "0";
            cardTextBox.Text = "CARD";
            cashTextBox.Text = "CASH";
            if (Properties.Settings.Default.StripeAPI != "NONE" && Properties.Settings.Default.stripeEnable && Properties.Settings.Default.StripeAPI.Length > 25)
            { 

                restaurantOrder.TotalCost = GlobalVars.numberRound(TotalOrderAmount,2);
                CardDetails cardDetails = new CardDetails(restaurantOrder);
                cardDetails.ShowDialog();
                if (cardDetails.PaymentTransferd)
                {
                    restaurantOrder.PaymentMethod = cardDetails.restaurantOrder.PaymentMethod;
                    PaymentDetails.PaymentMethod = "Card";
                    PaymentDetails.PaymentTransferId = cardDetails.PaymentTransferId;
                    PaymentDetails.CardAmount = GlobalVars.numberRound(TotalOrderAmount,2);
                    PaymentDetails.CashAmount = 0;
                    PaymentDetails.ChangeAmount = 0;
                    PaymentDetails.IsPrint = true;
                    PaymentDetails.Status = "Ok";
                    this.Close();
                }
                
            } 
        }private void printButton_Click(object sender, EventArgs e)
        {
            try
            {
                aOthersMethod.NumberPadClose();
                aOthersMethod.KeyBoardClose();
                if (!Valid()) return;
                double cashAmount;
                double cardAmount;
                PaymentDetails.ServedBy = "" + servedByTextBox.Text;
                if (cardFeeButton.BackColor == Color.Black)
                {
                    PaymentDetails.CardFee = OrderTotal > GlobalSetting.RestaurantInformation.CardMinOrder ? 0 : GlobalSetting.RestaurantInformation.CardFee;
                }

                if (cardPaymentButton.BackColor == Color.Black)
                {
                    PaymentDetails.PaymentMethod = "Card";
                    PaymentDetails.CardAmount = GlobalVars.numberRound(TotalOrderAmount,2);
                    PaymentDetails.CashAmount = 0;
                    PaymentDetails.ChangeAmount = 0;
                    PaymentDetails.IsPrint = true;
                    PaymentDetails.Status = "Ok";
                   // this.Close();
                }
                else if (double.TryParse(paidAmountTextBox.Text.Trim(), out cashAmount) && cashAmount > 0)
                {
                    PaymentDetails.PaymentMethod = "Cash";
                    PaymentDetails.CardAmount = 0;
                    PaymentDetails.CashAmount = GlobalVars.numberRound(TotalOrderAmount,2);
                    PaymentDetails.ChangeAmount = cashAmount - TotalOrderAmount;
                    PaymentDetails.IsPrint = true;
                    PaymentDetails.Status = "Ok";
                  //  this.Close();
                }

                else if (double.TryParse("0" + cashTextBox.Text.Trim(), out cashAmount) && double.TryParse("0" + cardTextBox.Text.Trim(), out cardAmount) && (cashAmount + cardAmount) >= TotalOrderAmount)
                {
                    PaymentDetails.PaymentMethod = "Split";
                    PaymentDetails.CardAmount = GlobalVars.numberRound(cardAmount,2);
                    PaymentDetails.CashAmount = GlobalVars.numberRound(cashAmount,2);
                    PaymentDetails.ChangeAmount = 0;
                    PaymentDetails.IsPrint = true;
                    PaymentDetails.Status = "Ok";
                    //this.Close();

                }
                if (PaymentDetails.Status == "Ok") {
                    try
                    {
                        if (GlobalSetting.SettingInformation.till == "Enable")
                        {
                            RawPrinterHelper aRawPrinterHelper = new RawPrinterHelper();
                            aRawPrinterHelper.openCashDrawer();
                        }
                    }
                    catch (Exception exception)
                    { 
                        MessageBox.Show("Please check till configuration.", "Till Open Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning); 
                    }
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
  

        }

   private bool Valid()
      {
            double cashAmount;
            double cardAmount;
       if (GlobalSetting.RestaurantInformation.RequireServed > 0 && servedByTextBox.Text.Trim().Length <= 0)
       {
           MessageBox.Show("Please Enter Served By.");
           return false;
       }
       if(cardPaymentButton.BackColor==Color.Black) return true;
          if(double.TryParse( paidAmountTextBox.Text.Trim(),out cashAmount) && cashAmount>= TotalOrderAmount) return true;
          if (double.TryParse("0" + cashTextBox.Text.Trim(), out cashAmount) && double.TryParse("0" + cardTextBox.Text.Trim(), out cardAmount) && (cashAmount+cardAmount) >= TotalOrderAmount) return true;
        
          return false;
      }

   private void doNotPrintButton_Click(object sender, EventArgs e)
   {
       try
       {
           aOthersMethod.NumberPadClose();
           aOthersMethod.KeyBoardClose();
           if (!Valid()) return;
           double cashAmount;
           double cardAmount;
           PaymentDetails.ServedBy = "" + servedByTextBox.Text;

           if (cardFeeButton.BackColor == Color.Black)
           {
               PaymentDetails.CardFee = OrderTotal > GlobalSetting.RestaurantInformation.CardMinOrder ? 0 : GlobalSetting.RestaurantInformation.CardFee;
           }

           if (cardPaymentButton.BackColor == Color.Black)
           {
               PaymentDetails.PaymentMethod = "Card";
               PaymentDetails.CardAmount = GlobalVars.numberRound(TotalOrderAmount,2);
               PaymentDetails.CashAmount = 0;
               PaymentDetails.ChangeAmount = 0;
               PaymentDetails.IsPrint = false;
               PaymentDetails.Status = "Ok";
             //  this.Close();
           }

           else if (double.TryParse(paidAmountTextBox.Text.Trim(), out cashAmount) && cashAmount > 0)
           {
               PaymentDetails.PaymentMethod = "Cash";
               PaymentDetails.CardAmount = 0;
               PaymentDetails.CashAmount = GlobalVars.numberRound(TotalOrderAmount,2);
               PaymentDetails.ChangeAmount = cashAmount - TotalOrderAmount;
               PaymentDetails.IsPrint = false;
               PaymentDetails.Status = "Ok";
            //   this.Close();

           }

           else if (double.TryParse(cashTextBox.Text.Trim(), out cashAmount) &&
               double.TryParse(cardTextBox.Text.Trim(), out cardAmount))
           {
               PaymentDetails.PaymentMethod = "Split";
               PaymentDetails.CardAmount = GlobalVars.numberRound(cardAmount,2);
               PaymentDetails.CashAmount = GlobalVars.numberRound(cashAmount,2);
               PaymentDetails.ChangeAmount = 0;
               PaymentDetails.IsPrint = false;
               PaymentDetails.Status = "Ok";
              // this.Close();

           }


           if (PaymentDetails.Status == "Ok")
           {
               try
               {
                   if (GlobalSetting.SettingInformation.till == "Enable")
                   {
                       RawPrinterHelper aRawPrinterHelper = new RawPrinterHelper();
                       aRawPrinterHelper.openCashDrawer();
                   }


               }
               catch (Exception exception)
               {
                   MessageBox.Show("Please check till configuration.", "Till Open Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
               }
               this.Close();
           }
       }
       catch (Exception exception)
       {
           ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
           aErrorReportBll.SendErrorReport(exception.ToString());
       }


   }

   private void cardFeeButton_Click(object sender, EventArgs e)
   {
       aOthersMethod.NumberPadClose();
       aOthersMethod.KeyBoardClose();

       if (cardFeeButton.BackColor == Color.Black)
       {
           cardFeeButton.BackColor = Color.Green;
           LoadTotalAmount(false);
       }
       else
       {
           cardFeeButton.BackColor = Color.Black;
           LoadTotalAmount(true);
       }

   }

   private void numberForm_Open(object sender, EventArgs e)
       {
      

       }

   private void paidAmountTextBox_MouseClick(object sender, MouseEventArgs e)
   {
      

       try
       {
          
           aOthersMethod.KeyBoardClose();
           if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
           {
               Point aPoint = new Point(0, screenHeight);
               NumberForm aNumberPad = new NumberForm(aPoint);
               aNumberPad.Show();

           }

       }
       catch
       {
       }
   }

   private void servedByTextBox_MouseClick(object sender, MouseEventArgs e)
   {
       try
       {
           aOthersMethod.KeyBoardClose();
           if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
           {
               Point aPoint = new Point(0, screenHeight+150);
               NumberPad aNumberPad = new NumberPad(aPoint);
               aNumberPad.Show();
           }

       }
       catch
       {
       }
   }

   private void cashTextBox_MouseClick(object sender, MouseEventArgs e)
   {
       try
       {
           aOthersMethod.KeyBoardClose();
           if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
           {
               Point aPoint = new Point(0, screenHeight);
               NumberForm aNumberPad = new NumberForm(aPoint);
               aNumberPad.Show();
           }

       }
       catch
       {
       }
   }

   private void cardTextBox_MouseClick(object sender, MouseEventArgs e)
   {
       try
       {
           aOthersMethod.KeyBoardClose();
           if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
           {
               Point aPoint = new Point(0, screenHeight);
               NumberForm aNumberPad = new NumberForm(aPoint);
               aNumberPad.Show();
           }

       }
       catch
       {
       }
   }



    }

    public class PaymentDetails
    {
        public string PaymentMethod{set;get;}
        public double CashAmount{set;get;}
        public double CardAmount{set;get;}
        public double ChangeAmount{set;get;}
        public bool IsPrint{set;get;}
        public string Status{set;get; }
        public string ServedBy { set; get; }
        public string PaymentTransferId { set; get; }
        public double CardFee { set; get; }

    }
    
}