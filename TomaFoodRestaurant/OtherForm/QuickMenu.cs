using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.Properties;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class QuickMenu : Form
    {
        private bool isCollapsed;
        private RestaurantOrdersReport restaurantOrders;
        private CollectionReport collection;
        private DeliveryReport delivery;
        private ResControlSetup resControl;

        public QuickMenu()
        {
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            restaurantOrders = new RestaurantOrdersReport();
            collection = new CollectionReport();
            delivery = new DeliveryReport();
            resControl = new ResControlSetup();
            this.ActiveControl = null;
            ExpandCollapse.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                ExpandCollapse.Image = Resources.Collapse_Arrow_20px;
                panelDropDown.Height += 10;
                if (panelDropDown.Size == panelDropDown.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                ExpandCollapse.Image = Resources.Expand_Arrow_20px;
                panelDropDown.Height -= 10;
                if (panelDropDown.Size == panelDropDown.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void ExpandCollapse_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void home_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.ShowDialog();
            this.Close();
            mf.ShowInTaskbar = false;
        }

        private void Table_Click(object sender, EventArgs e)
        {
            TableLoadForm tableLoadForm = new TableLoadForm();
            tableLoadForm.Show();
        }

        private void OnlineOrder_Click(object sender, EventArgs e)
        {
            OnlineOrderForm onlineOrderForm = new OnlineOrderForm();
            onlineOrderForm.Show();
        }

        private void changePrice_Click(object sender, EventArgs e)
        {
            PriceChangeForm priceChange = new PriceChangeForm();
            priceChange.Show();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if (isCollapsed)
                timer1.Stop();
            else
                timer1.Start();
        }

        private void resOrder_Click(object sender, EventArgs e)
        {
            //restaurantOrders.Dock = DockStyle.Fill;
            //AllSettingsForm allSettings = new AllSettingsForm();
            //allSettings.Controls.Add(restaurantOrders);
            //this.restaurantOrders.Visible = true;
            //this.restaurantOrders.BringToFront();
            //allSettings.Show();
        }

        private void resCollection_Click(object sender, EventArgs e)
        {
            //collection.Dock = DockStyle.Fill;
            //AllSettingsForm allSettings = new AllSettingsForm();
            //allSettings.Controls.Add(collection);
            //this.collection.Visible = true;
            //this.collection.BringToFront();
            //allSettings.Show();
        }

        private void resDelivery_Click(object sender, EventArgs e)
        {
            //delivery.Dock = DockStyle.Fill;
            //AllSettingsForm allSettings = new AllSettingsForm();
            //allSettings.Controls.Add(delivery);
            //this.delivery.Visible = true;
            //this.delivery.BringToFront();
            //allSettings.Show();
        }

        private void resSetup_Click(object sender, EventArgs e)
        {
            //resControl.Dock = DockStyle.Fill;
            //AllSettingsForm allSettings = new AllSettingsForm();
            //allSettings.Controls.Add(resControl);
            //this.resControl.Visible = true;
            //this.resControl.BringToFront();
            //allSettings.Show();
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