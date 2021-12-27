using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

using TomaFoodRestaurant.DAL.DAO;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class HouseByPostcode : Form
    {
        public string houseNo = "";
        public string postcode = "";

        public HouseByPostcode()
        {
            InitializeComponent();            
        }

        private void HouseByPostcode_Load(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Clear();
            //MySqlPostcode MySqlPostcode = new MySqlPostcode();
            List<Postcode> aps = getHouseByPostcode(postcode);

            int wt = 0;
            int ht = 0;
            int inc = 1;

            if (aps.Count > 0)
            {
                foreach (Postcode cover in aps)
                {
                    Button coverButton = new Button();
                    coverButton.Click += new EventHandler(coverButton_click);
                    coverButton.Text = cover.HouseNumber + " " + cover.HouseName;
                    coverButton.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold);
                    coverButton.FlatAppearance.BorderSize = 0;
                    coverButton.Height = 65;
                    coverButton.Width = 213;
                    coverButton.FlatStyle = FlatStyle.Standard;
                    coverButton.AutoSize = false;
                    coverButton.UseVisualStyleBackColor = true;
                    coverButton.Location = new Point(wt, ht);
                    flowLayoutPanel.Controls.Add(coverButton);
                }
            }
            else {
                houseNo = "";
                this.Close();
                MessageBox.Show("Not found ! ");
            }
        }

        private void coverButton_click(object sender, EventArgs e)
        {
             Button coverButton = sender as Button;
             houseNo = coverButton.Text;
             this.Close();
        }

        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private List<Postcode> getHouseByPostcode(string postcode)
        {
            
            List<Postcode> aPostcodes = new List<Postcode>();
            try
            {
                CoverageAreaBLL aCoverageAreaBll = new CoverageAreaBLL();
                
                string postString = @"{""postcode"":""" + postcode + "\"}".ToString().Replace(@"\", "");
                
                Uri target = new Uri(GlobalVars.apiUrl + "postcode/get_house");

                var request = (HttpWebRequest)WebRequest.Create(target);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = postString.Length;
                request.Headers.Add("x-api-key", "muzahid");
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();
                //bar.progressBar(20);
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var text = reader.ReadToEnd();
                        
                        dynamic objects = JArray.Parse(text);

                        foreach (JObject ject in objects)
                        {
                            Postcode pcode = new Postcode();
                            pcode.Id = (int) ject["id"];
                            pcode.HouseName = ject["HouseName"].ToString();
                            pcode.HouseNumber = ject["HouseNumber"].ToString();
                            pcode.AddressLine1 = ject["AddressLine1"].ToString();
                            pcode.AddressLine2 = ject["AddressLine2"].ToString();
                            pcode.Town = ject["Town"].ToString();
                            pcode.PostCode = ject["Postcode"].ToString();

                            aPostcodes.Add(pcode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }

            return aPostcodes;
        }
    }
}