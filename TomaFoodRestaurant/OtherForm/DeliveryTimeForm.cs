using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class DeliveryTimeForm : Form
    {
        public static TimeDetails TimeDetails = new TimeDetails();
        bool isCollect;
        GlobalUrl urls = new GlobalUrl();
        public DeliveryTimeForm(bool collect)
        {
            InitializeComponent();
            isCollect = collect;
        }

        private void DeliveryTimeForm_Load(object sender, EventArgs e)
        {

            int cnt = 0;
            int acnt = 0;
            int bcnt = 0;
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            Button aButton = new Button();
            if (isCollect) {
                aButton = NewButton("WAIT");
                timeFlowLayoutPanel.Controls.Add(aButton);
                aButton = NewButton("COLLECT");
                timeFlowLayoutPanel.Controls.Add(aButton);
            }
            DateTime aDateTime = new DateTime();
            var now = new TimeSpan(0, DateTime.Now.Minute, DateTime.Now.Second);
            int corrTo5MinutesUpper = (now.Minutes / 5) * 5;
            if (now.Minutes % 5 > 0)
            {
                corrTo5MinutesUpper = corrTo5MinutesUpper + 5;
                
            }
            if (corrTo5MinutesUpper == 60)
            {
                corrTo5MinutesUpper = 0;
                 aDateTime = Convert.ToDateTime(DateTime.Now.AddHours(1).ToString("HH") + ":" + corrTo5MinutesUpper.ToString());
            }
            else
            {
                 aDateTime = Convert.ToDateTime(DateTime.Now.ToString("HH") + ":" + corrTo5MinutesUpper.ToString());

            }
            //MessageBox.Show(corrTo5MinutesUpper.ToString());
          
            string time = aDateTime.ToString("HH:mm");
            aButton = NewButton(time);
            timeFlowLayoutPanel.Controls.Add(aButton);
            while(cnt<20)
            {
                cnt++;
                aDateTime = aDateTime.AddMinutes(5);
                time= aDateTime.ToString("HH:mm");
                aButton = NewButton(time);
                timeFlowLayoutPanel.Controls.Add(aButton);
            
            }
            aButton = NewButton("OTHER");
            timeFlowLayoutPanel.Controls.Add(aButton);
            //timeFlowLayoutPanel.Controls.Add(timepanel);


            timeFlowLayoutPanel.Size = new Size(timeFlowLayoutPanel.Size.Width, timeFlowLayoutPanel.Size.Height * 4);
            label3.Location = new Point(label3.Location.X, timeFlowLayoutPanel.Location.Y + timeFlowLayoutPanel.Size.Height + 10);
           


            while (acnt < 11)
            {
                acnt++;
                int atime = (acnt * 5);
                aButton = FixedTimeButton(atime.ToString());
                fixedTimeflowLayoutPanel.Controls.Add(aButton);


            }
            acnt = 0;
            while (acnt < 12)
            {
               
                int atime = (acnt * 5);
                aButton = FixedHourButton(atime.ToString());
                fixedTimeflowLayoutPanel.Controls.Add(aButton);
                acnt++;
               
            }

            Button aaButton = new Button();
            aaButton.BackColor = System.Drawing.Color.LightSeaGreen;
            aaButton.FlatAppearance.BorderSize = 0;
            aaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            aaButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            aaButton.ForeColor = System.Drawing.Color.White;
            aaButton.Margin = new System.Windows.Forms.Padding(2);
            aaButton.Size = new System.Drawing.Size(148, 60);
            aaButton.Text = "02:00";
            aaButton.MouseClick += FixedHourButton_Clicked;
            aaButton.UseVisualStyleBackColor = false;
            fixedTimeflowLayoutPanel.Controls.Add(aaButton);

            fixedTimeflowLayoutPanel.Location = new Point(fixedTimeflowLayoutPanel.Location.X, label3.Location.Y + label3.Size.Height + 10);
            fixedTimeflowLayoutPanel.Size = new Size(fixedTimeflowLayoutPanel.Size.Width, fixedTimeflowLayoutPanel.Size.Height * 5);
           
            okayButton.Location = new Point(fixedTimeflowLayoutPanel.Location.X, fixedTimeflowLayoutPanel.Location.Y + fixedTimeflowLayoutPanel.Size.Height + 10);
            closeButton.Location = new Point(okayButton.Location.X + okayButton.Width + 10, fixedTimeflowLayoutPanel.Location.Y + fixedTimeflowLayoutPanel.Size.Height + 10);
            timepanel.Location = new Point(450, fixedTimeflowLayoutPanel.Location.Y + fixedTimeflowLayoutPanel.Size.Height + 10);
           // timepanel.Visible = true;
           // MessageBox.Show(time);
        }


        public Button NewButton(string time) {

            Button aButton = new Button();
            aButton.BackColor = System.Drawing.Color.LightSeaGreen;
            aButton.FlatAppearance.BorderSize = 0;
            aButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            aButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            aButton.ForeColor = System.Drawing.Color.White;
            aButton.Margin = new System.Windows.Forms.Padding(2);
            aButton.Size = new System.Drawing.Size(148, 60);
            aButton.Text = time;
            aButton.MouseClick += TimeBUtton_Clicked;
            aButton.UseVisualStyleBackColor = false;

            return aButton;
        }

        public Button FixedHourButton(string time)
        {

            Button aButton = new Button();
            aButton.BackColor = System.Drawing.Color.LightSeaGreen;
            aButton.FlatAppearance.BorderSize = 0;
            aButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            aButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            aButton.ForeColor = System.Drawing.Color.White;
            aButton.Margin = new System.Windows.Forms.Padding(2);
            aButton.Size = new System.Drawing.Size(148, 60);
            aButton.Text = "01:"+time;
            aButton.MouseClick += FixedHourButton_Clicked;
            aButton.UseVisualStyleBackColor = false;

            return aButton;
        }

        public Button FixedTimeButton(string time)
        {

            Button aButton = new Button();
            aButton.BackColor = System.Drawing.Color.LightSeaGreen;
            aButton.FlatAppearance.BorderSize = 0;
            aButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            aButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            aButton.ForeColor = System.Drawing.Color.White;
            aButton.Margin = new System.Windows.Forms.Padding(2);
            aButton.Size = new System.Drawing.Size(148, 60);
            aButton.Text = time;
            aButton.MouseClick += FixedTimeButton_Clicked;
            aButton.UseVisualStyleBackColor = false;
           
            return aButton;
        }


        private void FixedHourButton_Clicked(object sen23, MouseEventArgs e)
        {
            Button aButton = sen23 as Button;
            TimeDetails.IsCollect = "";
            TimeDetails.Status = "Ok";

            var now = new TimeSpan(0, DateTime.Now.Minute, DateTime.Now.Second);
            int corrTo5MinutesUpper = (now.Minutes / 5) * 5;
            if (now.Minutes % 5 > 0)
            {
                corrTo5MinutesUpper = corrTo5MinutesUpper + 5;
            }
            //    MessageBox.Show(corrTo5MinutesUpper.ToString());
            DateTime aDateTime = DateTime.Now; //Convert.ToDateTime(DateTime.Now.ToString("hh") + ":" + corrTo5MinutesUpper.ToString());
            //string textBtn = Regex.Replace(aButton.Text, "01:", "");
            //aDateTime = aDateTime.AddMinutes(Convert.ToDouble(textBtn));
            //aDateTime = aDateTime.AddHours(1); 
            
            aDateTime = aDateTime.Add(TimeSpan.Parse(aButton.Text));
            string time = aDateTime.ToString("HH:mm");
            TimeDetails.DeliveryTime = time;
            this.Close();


        }




        private void FixedTimeButton_Clicked(object sen23, MouseEventArgs e)
        {
            Button aButton = sen23 as Button;
            TimeDetails.IsCollect = "";
            TimeDetails.Status = "Ok";

            var now = new TimeSpan(0, DateTime.Now.Minute, DateTime.Now.Second);
            int corrTo5MinutesUpper = (now.Minutes / 5) * 5;
            if (now.Minutes % 5 > 0)
            {
                corrTo5MinutesUpper = corrTo5MinutesUpper + 5;
            }
            //    MessageBox.Show(corrTo5MinutesUpper.ToString());
            DateTime aDateTime = DateTime.Now; //Convert.ToDateTime(DateTime.Now.ToString("hh") + ":" + corrTo5MinutesUpper.ToString());
            aDateTime = aDateTime.AddMinutes(Convert.ToDouble(aButton.Text));

            string time = aDateTime.ToString("HH:mm");
            TimeDetails.DeliveryTime = time;
            this.Close();


        }
         
        private void TimeBUtton_Clicked(object sen23, MouseEventArgs e)
        {
            Button aButton = sen23 as Button;
            if (aButton.Text.ToLower() == "other") {
                timepanel.Visible = true;
            }
            else if (aButton.Text.ToLower() == "wait" || aButton.Text.ToLower() == "collect")
            {
                TimeDetails.IsCollect = aButton.Text.ToLower() == "collect" ? "Time" : aButton.Text;
                if (TimeDetails.IsCollect == "WAIT") TimeDetails.IsCollect = "Wait";
                else if (TimeDetails.IsCollect == "COLLECT") TimeDetails.IsCollect = "Collect";
                TimeDetails.Status = "Ok";
                TimeDetails.DeliveryTime = "";
                this.Close();
            }
            else {
                TimeDetails.IsCollect ="";
                TimeDetails.Status = "Ok";
                TimeDetails.DeliveryTime =aButton.Text;
                this.Close();
            }

        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            TimeDetails.IsCollect = "";
            TimeDetails.Status = "Ok";
            TimeDetails.DeliveryTime =textBox1.Text=="" &&textBox2.Text=="" ? "Time" : textBox1.Text + ":" + textBox2.Text;
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            OthersMethod aOthersMethod = new OthersMethod();

            try
            {
                textBox1.Text = "";
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 270);
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

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            OthersMethod aOthersMethod = new OthersMethod();

            try
            {
                textBox2.Text = "";
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 270);
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

    public class TimeDetails {

        public string DeliveryTime{set;get;}
        public string IsCollect { set; get; }
        public string Status { set; get; }
    }

}
