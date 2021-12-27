using System;
using System.Drawing;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.Sequrity;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class AllSettingsForm : Form
    {
        public static string Status = "";
        public static mainForm mainFomr;
        public static MainFormView resPage;

        public AllSettingsForm(string openapge,mainForm singlePage,MainFormView ResponsivePage)
        {
            InitializeComponent();
            //mainFomr = backMainFormView;
            
            if (singlePage!=null)
            {
                mainFomr = singlePage;
            }
            if (ResponsivePage != null)
            {
                resPage = ResponsivePage;
            }

            settingsPanel.Controls.Clear();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            buttonLoadReservation.Visible = false;
            if (GlobalSetting.SettingInformation.IsReservationCheck && GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "restaurant")
            {
                buttonLoadReservation.Visible = true;
            }
            if (Properties.Settings.Default.showkitichineSection)
            {
                //seperateKitichine.Visible = true;
                kitchenSectiontoolStripeBtn.Visible = true;
            }
            if (Properties.Settings.Default.showcouponsection)
            {
                couopnMenu.Visible = true;
              //  seperatCoupon.Visible = true;
            }
            if (openapge == "reservation")
            {
                settingsPanel.Controls.Clear();
                foreach (ToolStripButton aa in toolStrip1.Items)
                {
                    aa.BackColor = Color.Transparent;
                }
                buttonLoadReservation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                ReservationTable objParent = new ReservationTable();
                objParent.Parent = this;
                settingsPanel.Controls.Add(objParent);
                objParent.Dock = DockStyle.Fill;
            }

            //if (openapge == "settings")
            //{
            //   // settingsPanel.Controls.Clear();

            //    cltOrderBtn.BackColor = Color.Transparent;
            //    ordersButton.BackColor = Color.Transparent;
            //    buttonLoadReservation.BackColor = Color.Transparent;
            //    delOrderBtn.BackColor = Color.Transparent;
            //    PrinterSettingsForm objParent = new PrinterSettingsForm();
            //    objParent.Parent = this;
            //    settingsPanel.Controls.Add(objParent);
            //    objParent.Dock = DockStyle.Fill;
            //}        
        }

        public AllSettingsForm()
        {
            InitializeComponent();
        }

        //private void ClearAllSlection()
        //{
        //    foreach (ToolStripButton cc in toolStrip1.contr)
        //    {

        //        foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
        //        {

        //            c.BackColor = PackItemsControl.DefaultBackColor;

        //        }


        //        cc.BackColor = PackageDetails.DefaultBackColor;

        //    }
        //}

        private void printerSetupButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            }
          //  printerSetupButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            PrinterSettingsForm objParent = new PrinterSettingsForm();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void printCopyButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            PrintCopySetupForm objParent = new PrintCopySetupForm();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void globalSettingtoolStripButton1_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            GlobalUrlSetupForm objParent = new GlobalUrlSetupForm();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void ordersButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear(); 
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            } 
            ordersButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            RestaurantOrdersReport objParent = new RestaurantOrdersReport();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;           
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            RestaurantOrdersReport.OrderId = 0;
            settingsPanel.Controls.Clear();
            this.Close();   
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            RestaurantOrdersReport.OrderId = 0;
            Status = "logout";
            
            this.Close();
        }

        private void AllSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G && e.Alt)
            {
                PathChangingForm aPathChangingForm = new PathChangingForm();
                aPathChangingForm.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.D && e.Alt)
            {
                DBsetup aPathChangingForm = new DBsetup(this);
                aPathChangingForm.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.S && e.Alt)
            {
                setupStripe SetupStripe = new setupStripe();
                SetupStripe.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.R && e.Alt)
            {
                ResetForm resetForm = new ResetForm();
                resetForm.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.T && e.Alt)
            {
                OthersControl resetForm = new OthersControl();
                resetForm.ShowDialog();            
            }
            if (e.Control && e.KeyCode == Keys.U && e.Alt)
            {
                UrlChageForm resetForm = new UrlChageForm();
                resetForm.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.Q && e.Alt)
            {
                QueryExecuteForm queryExecuteForm = new QueryExecuteForm();
                queryExecuteForm.ShowDialog();
            }
        }

        private void buttonLoadReservation_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            } 
            buttonLoadReservation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            ReservationTable objParent = new ReservationTable();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void cltOrderBtn_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            } 
            cltOrderBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption; 
            CollectionReport objParent = new CollectionReport();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void delOrderBtn_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            }
            delOrderBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

            DeliveryReport objParent = new DeliveryReport();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //settingsPanel.Controls.Clear();
            //cltOrderBtn.BackColor = Color.Transparent;
            //delOrderBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            //ordersButton.BackColor = Color.Transparent;
            //buttonLoadReservation.BackColor = Color.Transparent;
            //LocalOrderForm objParent = new LocalOrderForm(mainFomr,resPage);
            //objParent.Parent = this;
            //settingsPanel.Controls.Add(objParent);
            //objParent.Dock = DockStyle.Fill;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            }
            couopnMenu.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
             
            CouponCodeGenerate objParent = new CouponCodeGenerate();
            objParent.Parent = this;settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            }
            toolStripButton3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            PrinterSettingsForm objParent = new PrinterSettingsForm();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;        
        }

        private void kitchenSectiontoolStripeBtn_Click(object sender, EventArgs e)
        {
             settingsPanel.Controls.Clear();

            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            }
            kitchenSectiontoolStripeBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

            KitchenControl objParent = new KitchenControl();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void callLogBtn_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            }

            callLogBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

            CallList objParent = new CallList();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void ResCtrlSetup_Click(object sender, EventArgs e)
        {
            settingsPanel.Controls.Clear();
            
            foreach (ToolStripButton aa in toolStrip1.Items)
            {
                aa.BackColor = Color.Transparent;
            } 
            ResCtrlSetup.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

            ResControlSetup objParent = new ResControlSetup();
            objParent.Parent = this;
            settingsPanel.Controls.Add(objParent);
            objParent.Dock = DockStyle.Fill;
        }

        private void btnWebviewClick(object sender, EventArgs e)
        {
            frmWebview frmWebview = new frmWebview();
            frmWebview.TopLevel = false;
            frmWebview.Visible = true;
            settingsPanel.Controls.Add(frmWebview);
            frmWebview.Dock = DockStyle.Fill;
            frmWebview.BringToFront();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;

                return Params;
            }
        }
    }
}