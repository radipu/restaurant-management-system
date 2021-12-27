using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TomaFoodRestaurant.OtherForm
{
      public partial class  TomafoodSite : Form
    {
        //public ChromiumWebBrowser chromeBrowser;

        public TomafoodSite()
        {
            InitializeComponent();
          //  InitializeChromium();
        }

        private void TomafoodSite_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate(Properties.Settings.Default.backend);
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.Text = "Navigating";
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Text = e.Url.ToString() + " loaded";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Do you want to update local database", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                AllSettingsForm.Status = "";
                processBar aProcessBar = new processBar("Please wait,Local database updating...", 1);
                aProcessBar.ShowDialog();

                if (AllSettingsForm.Status == "logout")
                {
                    this.Close();
                    Application.Restart();
                }

            }
            else {
                this.Close();
            }



            //
        }

        //public void InitializeChromium()
        //{
        //    CefSettings settings = new CefSettings();
        //    Cef.Initialize(settings);
        //    chromeBrowser = new ChromiumWebBrowser("http://ourcodeworld.com");
        //    this.Controls.Add(chromeBrowser);
        //    chromeBrowser.Dock = DockStyle.Fill;
        //}

        //private void TomafoodSite_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Cef.Shutdown();
        //}
    }
}
