using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.Sequrity;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class ResControlSetup : UserControl
    {
        public ResControlSetup()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            RestaurantUsers users = new RestaurantUsers();
            AutorizeForm form = new AutorizeForm(users, "resSetup");
            form.ShowDialog();

            if (form.user.Autorize)
            {
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                restaurantInformation.DeliveryTime = delTimeComboBox.Text;
                restaurantInformation.ServiceOption = serviceTypeComboBox.Text.ToLower();
                restaurantInformation.IsBusy = busyComboBox.Text == "YES" ? 1 : 0;
                restaurantInformation.PreOrder = preOrderComboBox.Text == "YES" ? 1 : 0;
                restaurantInformation.ShowOrderNumber = orderNoComboBox.Text == "YES" ? 1 : 0;
                restaurantInformation.CollectionTime = cltTimeComboBox.Text;
                restaurantInformation.DeliveryCharge = Convert.ToDouble(deliveryChargeTextBox.Text);
                restaurantInformation.DelPrintCopy = Convert.ToInt32(delPrintCopyComboBox.Text);
                restaurantInformation.PrintCopy = Convert.ToInt32(cltPrintCopyComboBox.Text);
                restaurantInformation.DineInPrintCopy = Convert.ToInt32(inPrintCopyComboBox.Text);

                //changed object
                var changedData = new {
                    delivery_time = delTimeComboBox.Text,
                    collection_time = cltTimeComboBox.Text,
                    service_option = serviceTypeComboBox.Text.ToLower(),
                    is_busy = busyComboBox.Text == "YES" ? 1 : 0,
                    pre_order = preOrderComboBox.Text == "YES" ? 1 : 0,
                    show_order_number = orderNoComboBox.Text == "YES" ? 1 : 0,
                    delivery_charge = Convert.ToDouble(deliveryChargeTextBox.Text),
                    del_print_copy = Convert.ToInt32(delPrintCopyComboBox.Text),
                    print_copy = Convert.ToInt32(cltPrintCopyComboBox.Text),
                    in_print_copy = Convert.ToInt32(inPrintCopyComboBox.Text)
                };

                var jsonSerialiser = new JavaScriptSerializer();

                string postData = jsonSerialiser.Serialize(changedData);

                aRestaurantInformationBll.updateRestaurantInformation(restaurantInformation, postData);
            }
        }

        private void ResControlSetup_Load(object sender, EventArgs e)
        {
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            delTimeComboBox.Text = restaurantInformation.DeliveryTime.ToString().ToUpper();
            serviceTypeComboBox.Text = restaurantInformation.ServiceOption;
            busyComboBox.Text = restaurantInformation.IsBusy > 0 ? "YES" : "NO";
            preOrderComboBox.Text = restaurantInformation.PreOrder > 0 ? "YES" : "NO";
            cltTimeComboBox.Text = restaurantInformation.CollectionTime.ToString();
            deliveryChargeTextBox.Text = restaurantInformation.DeliveryCharge.ToString();
            orderNoComboBox.Text = restaurantInformation.ShowOrderNumber > 0 ? "YES" : "NO";
            delPrintCopyComboBox.Text = restaurantInformation.DelPrintCopy.ToString();
            cltPrintCopyComboBox.Text = restaurantInformation.PrintCopy.ToString();
            inPrintCopyComboBox.Text = restaurantInformation.DineInPrintCopy.ToString();
         //   receiptFontcomboBox.Text = restaurantInformation.RecieptFont.ToString();
            //   delPrintCopyComboBox.Text = restaurantInformation.DelPrintCopy.ToString();
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