using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CallList : UserControl
    {
        public CallList()
        {
            InitializeComponent();
        }

        private void CallList_Load(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("Config/call.txt"))
            {
                string str = String.Empty;
                string predate = DateTime.Today.AddDays(-1).ToShortDateString();
                while ((str = sr.ReadLine()) != null)
                {
                    if (str.Contains(predate))
                    {
                      
                        File.WriteAllText("Config/call.txt", "");
                        break;
                    }
                    else
                    {
                        Label lableText = new Label();
                        lableText.AutoSize = true;
                        lableText.Location = new System.Drawing.Point(23, 20);
                        lableText.Size = new System.Drawing.Size(59, 23);
                        lableText.TabIndex = 0;
                        lableText.Text = str;
                        flowLayoutPanelForCallLog.Controls.Add(lableText);
                    }
                }
            }
        }
    }
}
