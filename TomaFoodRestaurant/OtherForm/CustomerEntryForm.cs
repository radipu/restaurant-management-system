using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Text.RegularExpressions; 
namespace TomaFoodRestaurant.OtherForm
{
    public partial class CustomerEntryForm : Form
    {
        List<PostCodeModel> aPostCodeModelList = new List<PostCodeModel>();
        public static RestaurantUsers aRestaurantUser = new RestaurantUsers();
        public static string OrderType = "";
        public static string Status = "";
        public bool editbutton = false;

        public CustomerEntryForm(string customerPhone, RestaurantUsers User)
        {
            InitializeComponent(); 

            CustomerBLL aCustomerBll = new CustomerBLL();
            btnSave.Visible = true;
            if (User.Id > 0)
            {
                aRestaurantUser = User;
                editbutton = true;
                btnSave.Visible = false;
            }

            else
            {
                aRestaurantUser = new RestaurantUsers();
                Int64 n;
                if (!Int64.TryParse(customerPhone, out n))
                {
                    if (customerPhone != "Search Customer")
                        postCodeTextBox.Text = customerPhone;
                }
                else if (customerPhone.Count() >= 2 && customerPhone[1] == '7')
                {
                    mobilePhoneTextBox.Text = customerPhone;
                }
                else
                {
                    homePhoneTextBox.Text = customerPhone;
                }
            }
        }
        private void CustomerEntryForm_Load(object sender, EventArgs e)
        {
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            CustomerBLL customerBLL = new CustomerBLL();
            List<MemberShipType> memberShipTypes = new List<MemberShipType>();
            memberShipTypes = customerBLL.GetMembershipsByResId(restaurantInformation.Id);

            MemberShipType DemoMem = new MemberShipType();
            DemoMem.Id = 0;
            DemoMem.TypeName = "None";
            memberShipTypes.Add(DemoMem);
            memberShipTypes = memberShipTypes.OrderBy(a => a.Id).ToList();
            comboBoxMembership.DataSource = memberShipTypes;
            comboBoxMembership.DisplayMember = "TypeName";
            comboBoxMembership.ValueMember = "Id";
            CoverageAreaBLL aCoverageAreaBll = new CoverageAreaBLL();
            List<AreaCoverage> aAreaCoverages = aCoverageAreaBll.GetCoverageArea();

            int wt = 0;
            int ht = 0;
            int inc = 1;
            foreach (AreaCoverage cover in aAreaCoverages)
            {
                Button coverButton = new Button();
                coverButton.Click += new EventHandler(coverButton_click);
                coverButton.Text = cover.Postcode;
                coverButton.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
                coverButton.FlatAppearance.BorderSize = 0;
                coverButton.Height = 45;
                coverButton.Width = 75;
                coverButton.FlatStyle = FlatStyle.Standard;
                coverButton.AutoSize = false;
                coverButton.UseVisualStyleBackColor = true;
                coverButton.Location = new Point(wt, ht);
                panel2.Controls.Add(coverButton);
                wt += (coverButton.Width + 2);
                if (inc % 3 == 0 && inc != 0)
                {
                    wt = 0;
                    ht += 50;
                }
                inc++;
            }
     
            if (aRestaurantUser.Id > 0)
            {
                GetCustomerInformation(aRestaurantUser);
            }
            else if (postCodeTextBox.Text.Trim().Length > 0)
            {
                LoadAddressDetails();
            }
            //if (!OthersMethod.CheckForInternetConnection())
            //{
            //    SearchFromGoogleBtn.Visible = false;
            //}
        }

        private void coverButton_click(object sender, EventArgs e)
        {
            Button coverButton = sender as Button;
            postCodeTextBox.Text = "";
            numberPadUs1.ControlToInputText = postCodeTextBox;
            if (coverButton.Text.Length > 6)
            {
                postCodeTextBox.Text = coverButton.Text.ToUpper();
            }
            else
            {
                postCodeTextBox.Text = coverButton.Text.ToUpper();
                Refresh();
                SendKeys.Send(" ");
                postCodeTextBox.Select(coverButton.Text.Length + 1, 0);
                postCodeTextBox.Select();
                Refresh();
            }
        }

        private void LoadCustumSorceForTexBox()
        {
            AutoCompleteStringCollection postcodeCollection = new AutoCompleteStringCollection();
            AutoCompleteStringCollection streetCollection = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cityCollection = new AutoCompleteStringCollection();
            foreach (PostCodeModel aPostCodeModel in aPostCodeModelList)
            {
                postcodeCollection.Add(aPostCodeModel.Postcode);
                //streetCollection.Add(aPostCodeModel.District);
                //cityCollection.Add(aPostCodeModel.County);
            }
            postCodeTextBox.AutoCompleteCustomSource = postcodeCollection;
            postCodeTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        private void LoadPostCode()
        {
            PostCodeBLL aPostCodeBll = new PostCodeBLL();
            CoverageAreaBLL aCoverageAreaBll = new CoverageAreaBLL();
            List<PostCodeModel> tempPostCodeModelList = aPostCodeBll.GetPostCodeInformation();
            List<AreaCoverage> aAreaCoverages = aCoverageAreaBll.GetCoverageArea();

            List<string> Coverages = aAreaCoverages.Select(b => b.Postcode).Distinct().ToList();
            foreach (string cover in Coverages)
            {
                List<PostCodeModel> postcode = tempPostCodeModelList.Where(a => a.Postcode.StartsWith(cover.Trim())).ToList();
                if (postcode.Count > 0)
                {
                    aPostCodeModelList.AddRange(postcode);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearField();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Activate();
            }
        }

        private void saveDiscountButton_Click(object sender, EventArgs e)
        {
            updateCustomer("clt");
        }

        private void ClearField()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            postCodeTextBox.Clear();
            houseTextBox.Clear();
            streetTextBox.Clear();
            cityTextBox.Clear();
            emailAddressTextBox.Clear();
            mobilePhoneTextBox.Clear();
            homePhoneTextBox.Clear();
            btnSave.Visible = false;
        }

        private bool ValidPhoneNumber(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;

            }
            if (str.Length != 11)
            {
                return false;
            }
            return true;
        }

        private bool ValidForm()
        {
            string mobileNo = mobilePhoneTextBox.Text;
            string phoneNo = homePhoneTextBox.Text;

            //if (mobilePhoneTextBox.Text.Trim() == "" && homePhoneTextBox.Text.Trim() == "" && firstNameTextBox.Text.Trim() == "" && lastNameTextBox.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please fillup atleast one field.");
            //    return false;
            //}
            //return true;

            if (mobilePhoneTextBox.Text.Trim() != "")
            {
                if (!ValidPhoneNumber(mobilePhoneTextBox.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid number only.");
                    return false;
                }
                string sub = mobilePhoneTextBox.Text.Trim().Replace(" ", "").Substring(0, 2);
                if (sub == "07" && mobilePhoneTextBox.Text.Trim().Replace(" ", "").Length == 11)
                {
                    return true;
                }
                else
                {
                    string phoneNoChecked = mobileNo.Trim().Replace(" ", "").Substring(0, 3);

                    if (phoneNoChecked.Contains("19"))
                    {
                        homePhoneTextBox.Text = mobileNo;

                        mobilePhoneTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid  mobile number.");
                    }

                    return false;
                }

                return true;

            }
            else if (homePhoneTextBox.Text.Trim() != "")
            {
                if (!ValidPhoneNumber(homePhoneTextBox.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid number only.");
                    return false;
                }
                string sub = homePhoneTextBox.Text.Trim().Replace(" ", "").Substring(0, 2);
                if ((sub == "01" && homePhoneTextBox.Text.Trim().Replace(" ", "").Length == 11) || (sub == "02" && homePhoneTextBox.Text.Trim().Replace(" ", "").Length == 11))
                {
                    return true;
                }
                else
                {
                    string mobilePhoneChecked = phoneNo.Trim().Replace(" ", "").Substring(0, 3);

                    if (mobilePhoneChecked.Contains("79"))
                    {
                        mobilePhoneTextBox.Text = phoneNo;
                        homePhoneTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid  Phone number.");
                    }

                    return false;
                }
            }
            return true;
        }

        private void postCodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {
                LoadAddressDetails();
            }
        }

        private void LoadAddressDetails()
        {
            string postCode = postCodeTextBox.Text.ToUpper().Trim();
            var aPostcodeModel = aPostCodeModelList.FirstOrDefault(a => a.Postcode == postCode);
            if (aPostcodeModel != null)
            {
                MessageBox.Show(postCode);//streetTextBox.Text = aPostcodeModel.Ward;
                //cityTextBox.Text = aPostcodeModel.District;
                if (aPostcodeModel.Formatted_address != null && aPostcodeModel.Formatted_address != "")
                {
                    fullAddressTextBox.Text = aPostcodeModel.Formatted_address.ToString().Replace(", ", "\r\n");
                    SearchFromGoogleBtn.Visible = true;
                }
                else
                {
                    fullAddressTextBox.Text = "";
                    SearchFromGoogleBtn.Visible = true;
                }
            }
        }

        private void streetTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {
                var aPostcodeModel = aPostCodeModelList.FirstOrDefault(a => a.District == streetTextBox.Text.Trim());
                if (aPostcodeModel != null)
                {
                    postCodeTextBox.Text = aPostcodeModel.Postcode;
                    cityTextBox.Text = aPostcodeModel.County;
                }
            }
        }

        private void SearchFromGoogleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PostCodeBLL aPostCodeBll = new PostCodeBLL();
                fullAddressTextBox.Text = "Loading Address...";
               // Refresh();

                if (postCodeTextBox.Text.Length > 0)
                {
                    string postcode = postCodeTextBox.Text;
                    string item = aPostCodeBll.GetFormattedAddressByPostcode(postcode);

                    if (item.Length > 0)
                    {
                        fullAddressTextBox.Text = item.Replace(", ", "\r\n");
                        //fullAddressTextBox.Text = fullAddressTextBox.Text.Contains(postCodeTextBox.Text) ? fullAddressTextBox.Text.Replace(postCodeTextBox.Text, "") : fullAddressTextBox.Text;
                        //Refresh();
                        PostCodeModel aPostCodeModel = new PostCodeModel();
                        aPostCodeModel.Postcode = postCodeTextBox.Text.Replace(" ", "");
                        aPostCodeModel.Formatted_address = item.Replace(", ", "\r\n");
                        aPostCodeBll.UpdateFormattedAddressInPostcode(aPostCodeModel);
                    }
                    else
                    {
                        fullAddressTextBox.Text = "";
                        MessageBox.Show("Please enter valid postcode.");
                    }
                    File.AppendAllText("Config/log.txt", "POST CODE LOG : " + postCodeTextBox.Text + " : " + fullAddressTextBox.Text + DateTime.Now + " \n\n");

                }
                else
                {
                    MessageBox.Show("Please enter postcode to find address.");
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
                fullAddressTextBox.Text = "";
                SearchFromGoogleBtn.Text = "Find Address";
                MessageBox.Show("Sorry ! unable to find this address.Please enter manually");
            }
        }

        private void firstNameTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = firstNameTextBox;
        }

        private void lastNameTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = lastNameTextBox;
        }

        private void homePhoneTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = homePhoneTextBox;
        }

        private void emailAddressTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = emailAddressTextBox;
        }

        private void mobilePhoneTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = mobilePhoneTextBox;
        }

        private void postCodeTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = postCodeTextBox;
        }

        private void houseTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = houseTextBox;
        }

        private void streetTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = streetTextBox;
        }

        private void cityTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = cityTextBox;
        }

        private void deliveryButton_Click(object sender, EventArgs e)
        {
            updateCustomer("del");
        }

        public void updateCustomer(string type)
        {
            try
            {
                if (ValidForm())
                {
                    int MemberShipTypeID;
                    RestaurantUsers aaRestaurantUser = new RestaurantUsers();
                    aaRestaurantUser.Firstname = firstNameTextBox.Text.Trim();
                    aaRestaurantUser.Lastname = lastNameTextBox.Text.Trim();
                    aaRestaurantUser.Postcode = postCodeTextBox.Text.Trim();
                    aaRestaurantUser.House = houseTextBox.Text.Trim();
                    aaRestaurantUser.City = cityTextBox.Text.Trim();
                    aaRestaurantUser.Address = streetTextBox.Text.Trim();
                    aaRestaurantUser.Email = emailAddressTextBox.Text.Trim();
                    aaRestaurantUser.Mobilephone = mobilePhoneTextBox.Text.Trim();
                    aaRestaurantUser.Homephone = homePhoneTextBox.Text.Trim();
                    aaRestaurantUser.FullAddress = fullAddressTextBox.Text.Trim().Replace("\r\n", ", ").Replace("\'", "");
                    MemberShipTypeID = Convert.ToInt32(comboBoxMembership.SelectedValue);

                    // MessageBox.Show(MemberShipTypeID.ToString());
                    aaRestaurantUser.GcmRegId = "";
                    aaRestaurantUser.IsUpdate = 0;
                    OrderType = type;
                    CustomerBLL aCustomerBll = new CustomerBLL();
                    if (aRestaurantUser.Id > 0)
                    {
                        aaRestaurantUser.Id = aRestaurantUser.Id;
                        int result = aCustomerBll.UpdateRestaurantCustomer(aaRestaurantUser);
                        if (result > 0)
                        {
                            aaRestaurantUser.Id = result;
                            aRestaurantUser = aaRestaurantUser;
                            this.Close();
                            ClearField();
                        }
                    }
                    else
                    {
                        aaRestaurantUser.Usertype = "user";
                        if (aaRestaurantUser.Mobilephone != String.Empty)
                        {
                            aRestaurantUser = aCustomerBll.IsExistCustomer(aaRestaurantUser);
                            if (aRestaurantUser != null)
                            {                                
                                aaRestaurantUser.Id = aRestaurantUser.Id;
                                int update = aCustomerBll.UpdateRestaurantCustomer(aaRestaurantUser);
                                aRestaurantUser = aaRestaurantUser;
                            }
                            else
                            {

                                int result = aCustomerBll.InsertRestaurantCustomer(aaRestaurantUser);
                                if (result > 0)
                                {
                                    aaRestaurantUser.Id = result;
                                    aRestaurantUser = aaRestaurantUser;
                                }                            
                            }
                        }
                        else if (aaRestaurantUser.Homephone != String.Empty)
                        {
                            aRestaurantUser = aCustomerBll.IsExistCustomer(aaRestaurantUser);
                            if (aRestaurantUser != null)
                            {                                
                                aaRestaurantUser.Id = aRestaurantUser.Id;                                
                                int update = aCustomerBll.UpdateRestaurantCustomer(aaRestaurantUser);
                                aRestaurantUser = aaRestaurantUser;

                            }
                            else
                            {
                                int result = aCustomerBll.InsertRestaurantCustomer(aaRestaurantUser);
                                if (result > 0)
                                {
                                    aaRestaurantUser.Id = result;
                                    aRestaurantUser = aaRestaurantUser;
                                }
                            }
                        }
                        else
                        {
                            int result = aCustomerBll.InsertRestaurantCustomer(aaRestaurantUser);
                            if (result > 0)
                            {
                                aaRestaurantUser.Id = result;
                                aRestaurantUser = aaRestaurantUser;
                            }                        
                        }

                        //update membership
                        if(aRestaurantUser.Id > 0)
                        {
                            MemberShips memm = new MemberShips();
                            if (MemberShipTypeID > 0)
                            {
                                memm = aCustomerBll.GetMemberShips(aRestaurantUser.Id, MemberShipTypeID);
                                if (memm.Id == 0)
                                {
                                    memm.MembershipId = aaRestaurantUser.Id;
                                    memm.MembershipTypeId = MemberShipTypeID;//Convert.ToInt32(comboBoxMembership.SelectedItem);
                                    aCustomerBll.InsertMembership(memm);

                                }
                                else
                                {
                                    //update membership
                                    //todo: fix the membership system
                                }
                            }
                        }
                       
                        try
                        {
                            string customer = aCustomerBll.CustomerSyncronise(aRestaurantUser);
                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }

                        this.Close();
                        ClearField();
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }
        }

        public void SaveCustomer()
        {
            if (ValidFormForSave())
            {
                int MemberShipTypeID;RestaurantUsers aaRestaurantUser = new RestaurantUsers();
                aaRestaurantUser.Firstname = firstNameTextBox.Text.Trim();
                aaRestaurantUser.Lastname = lastNameTextBox.Text.Trim();
                aaRestaurantUser.Postcode = postCodeTextBox.Text.Trim();
                aaRestaurantUser.House = houseTextBox.Text.Trim();
                aaRestaurantUser.City = cityTextBox.Text.Trim();
                aaRestaurantUser.Address = streetTextBox.Text.Trim();
                aaRestaurantUser.Email = emailAddressTextBox.Text.Trim();
                aaRestaurantUser.Mobilephone = mobilePhoneTextBox.Text.Trim();
                aaRestaurantUser.Homephone = homePhoneTextBox.Text.Trim();
                aaRestaurantUser.FullAddress = fullAddressTextBox.Text.Trim().Replace("\r\n", ", ").Replace("\'", "");
                MemberShipTypeID = Convert.ToInt32(comboBoxMembership.SelectedValue);

                // MessageBox.Show(MemberShipTypeID.ToString());
                aaRestaurantUser.GcmRegId = "";
                aaRestaurantUser.IsUpdate = 0;
               // OrderType = "del";
                CustomerBLL aCustomerBll = new CustomerBLL();
                if (aRestaurantUser.Id > 0)
                {
                    aaRestaurantUser.Id = aRestaurantUser.Id;
                    int result = aCustomerBll.UpdateRestaurantCustomer(aaRestaurantUser);
                    if (result > 0)
                    {
                        MemberShips memm = new MemberShips();
                        if (MemberShipTypeID > 0)
                        {
                            memm = aCustomerBll.GetMemberShips(aaRestaurantUser.Id, MemberShipTypeID);
                            if (memm.Id == 0)
                            {
                                memm.MembershipId = aaRestaurantUser.Id;
                                memm.MembershipTypeId = MemberShipTypeID;//Convert.ToInt32(comboBoxMembership.SelectedItem);
                                aCustomerBll.InsertMembership(memm);
                            }
                        }
                        aRestaurantUser = aaRestaurantUser;
                        try
                        { 
                           aCustomerBll.CustomerSyncronise(aRestaurantUser);
                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        } 
                    }
                }
                else
                {
                    aaRestaurantUser.Usertype = "user";
                   
                        aRestaurantUser = aCustomerBll.IsExistCustomer(aaRestaurantUser);
                    if (aRestaurantUser != null)
                    {
                        firstNameTextBox.Text = aRestaurantUser.Firstname;
                        lastNameTextBox.Text = aRestaurantUser.Lastname;
                        postCodeTextBox.Text = aRestaurantUser.Postcode;
                        houseTextBox.Text = aRestaurantUser.House;
                        streetTextBox.Text = aRestaurantUser.Address;
                        cityTextBox.Text = aRestaurantUser.City;
                        emailAddressTextBox.Text = aRestaurantUser.Email;
                        mobilePhoneTextBox.Text = aRestaurantUser.Mobilephone;
                        homePhoneTextBox.Text = aRestaurantUser.Homephone;
                        fullAddressTextBox.Text = aRestaurantUser.FullAddress.Replace(", ", "\r\n"); ;

                        MemberShips memm = new MemberShips();
                        memm = new CustomerBLL().GetMemberShipByUserID(aRestaurantUser.Id, GlobalSetting.RestaurantInformation.Id);
                        if (memm.Id > 0)
                        {
                            comboBoxMembership.SelectedValue = memm.MembershipTypeId;
                            comboBoxMembership.SelectedItem = memm.MembershipTypeId;
                            //  comboBoxMembership.SelectedIndex = memm.MembershipTypeId;
                        }

                        return;
                    }
                    else
                    {
                        int result = aCustomerBll.InsertRestaurantCustomer(aaRestaurantUser);
                        if (result > 0)
                        {
                            aaRestaurantUser.Id = result;
                            if (MemberShipTypeID > 0)
                            {
                                MemberShips mem = new MemberShips();
                                mem.MembershipId = result;
                                mem.MembershipTypeId = MemberShipTypeID;
                                aCustomerBll.InsertMembership(mem);
                            }
                            aRestaurantUser = aaRestaurantUser;
                            try
                            {
                                aCustomerBll.CustomerSyncronise(aRestaurantUser);
                            }
                            catch (Exception exception)
                            {
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(exception.ToString());
                            }
                        }
                    }
                       
                    //}
                    //else {
                    //    MessageBox.Show("Please enter valid number."); 
                    //} 
                }
                aRestaurantUser = new RestaurantUsers();
                ClearField();
                this.Close();
            }
        }

        private bool ValidFormForSave()
        {
            string mobileNo = mobilePhoneTextBox.Text;
            string phoneNo = homePhoneTextBox.Text; 

            if (firstNameTextBox.Text.Trim() == "" && lastNameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Please enter customer name.");
                return false;
            }
            if (mobilePhoneTextBox.Text.Trim() == "" && homePhoneTextBox.Text.Trim() == "" && firstNameTextBox.Text.Trim() == "" && lastNameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Please enter name or a telephone number.");
                return false;
            }

            //return true;

            if (mobilePhoneTextBox.Text.Trim() != "")
            {
                if (!ValidPhoneNumber(mobilePhoneTextBox.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid number only.");
                    return false;
                }
                string sub = mobilePhoneTextBox.Text.Trim().Replace(" ", "").Substring(0, 2);
                if (sub == "07" && mobilePhoneTextBox.Text.Trim().Replace(" ", "").Length == 11)
                {
                    return true;
                }
                else
                {
                    string phoneNoChecked = mobileNo.Trim().Replace(" ", "").Substring(0, 3);

                    if (phoneNoChecked.Contains("19"))
                    {
                        homePhoneTextBox.Text = mobileNo;

                        mobilePhoneTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid  mobile number.");
                    }

                    return false;
                }

                return true;
            }
            else if (homePhoneTextBox.Text.Trim() != "")
            {
                if (!ValidPhoneNumber(homePhoneTextBox.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid number only.");
                    return false;
                }
                string sub = homePhoneTextBox.Text.Trim().Replace(" ", "").Substring(0, 2);
                if ((sub == "01" && homePhoneTextBox.Text.Trim().Replace(" ", "").Length == 11) || (sub == "02" && homePhoneTextBox.Text.Trim().Replace(" ", "").Length == 11))
                {
                    return true;
                }
                else
                {
                    string mobilePhoneChecked = phoneNo.Trim().Replace(" ", "").Substring(0, 3);

                    if (mobilePhoneChecked.Contains("79"))
                    {
                        mobilePhoneTextBox.Text = phoneNo;
                        homePhoneTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid  Phone number.");
                    }

                    return false;
                }
            }

            return true;
        }

        private void fullAddressTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = fullAddressTextBox;
        }

        private void postCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fullAddressTextBox.Text = "";
                string postcodeAddress = postCodeTextBox.Text.Replace(" ", "");
                if (postcodeAddress.Length >= 6)
                {
                    bool ValidPostCode = PostCodeVaild(postcodeAddress);
                    if (ValidPostCode)
                    {
                        PostCodeModel postCode = new PostCodeBLL().GetPostCodeInformation(postcodeAddress);
                        if (postCode != null)
                        {
                            if (postCode.Formatted_address != "")
                            {
                                fullAddressTextBox.Text = postCode.Formatted_address;

                            }
                            else
                            {
                                fullAddressTextBox.Text = postCode.Ward;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }
        }

        public bool PostCodeVaild(string postCode)
        {
            var reg = @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z]))))\s?[0-9][A-Za-z]{2})";

            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            return regex.IsMatch(postCode);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCustomer();        
        }

        private void mobilePhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Visible = false;
                if (mobilePhoneTextBox.Text.Length >= 11 && editbutton == false)
                {
                    btnSave.Visible = true;
                    RestaurantUsers users = new RestaurantUsers();
                    users.Mobilephone = mobilePhoneTextBox.Text;
                    var existUser = new CustomerBLL().IsExistCustomer(users);
                    if (existUser != null)
                    {
                        GetCustomerInformation(existUser);
                    }
                }
            }
            catch (Exception)
            {
                this.Activate();
            }
        }

        public void GetCustomerInformation(RestaurantUsers aRestaurantUser)
        {
            if (aRestaurantUser != null)
            {
                btnSave.Visible = false;
                firstNameTextBox.Text = aRestaurantUser.Firstname;
                lastNameTextBox.Text = aRestaurantUser.Lastname;
                postCodeTextBox.Text = aRestaurantUser.Postcode;
                houseTextBox.Text = aRestaurantUser.House;
                streetTextBox.Text = aRestaurantUser.Address;
                cityTextBox.Text = aRestaurantUser.City;
                emailAddressTextBox.Text = aRestaurantUser.Email;
                mobilePhoneTextBox.Text = aRestaurantUser.Mobilephone;
                homePhoneTextBox.Text = aRestaurantUser.Homephone;
                if (aRestaurantUser.FullAddress != "")
                {
                    fullAddressTextBox.Text = aRestaurantUser.FullAddress;
                }
                else
                {
                    PostCodeModel postCode = new PostCodeBLL().GetPostCodeInformation(aRestaurantUser.Postcode);
                    if (postCode != null)
                    {
                        if (postCode.Formatted_address != "")
                        {
                            fullAddressTextBox.Text = postCode.Formatted_address;

                        }
                        else
                        {
                            fullAddressTextBox.Text = postCode.Ward;
                        }
                    }
                }

                MemberShips memm = new MemberShips();
                memm = new CustomerBLL().GetMemberShipByUserID(aRestaurantUser.Id, GlobalSetting.RestaurantInformation.Id);
                if (memm.Id > 0)
                {
                    comboBoxMembership.SelectedValue = memm.MembershipTypeId;
                    comboBoxMembership.SelectedItem = memm.MembershipTypeId;
                    //  comboBoxMembership.SelectedIndex = memm.MembershipTypeId;
                }

                return;
            }
        }

        private void homePhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Visible = false;
                if (homePhoneTextBox.Text.Length >= 11 && editbutton == false)
                {
                    btnSave.Visible = true;
                    RestaurantUsers users = new RestaurantUsers();
                    users.Mobilephone = homePhoneTextBox.Text;
                    var existUser = new CustomerBLL().IsExistCustomer(users);
                    if (existUser != null)
                    {
                        GetCustomerInformation(existUser);
                    }
                }
            }
            catch (Exception)
            {
                this.Activate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (postCodeTextBox.Text.Length >= 5)
            {
                HouseByPostcode aCustomerEntryForm = new HouseByPostcode();
                aCustomerEntryForm.postcode = postCodeTextBox.Text;
                aCustomerEntryForm.ShowDialog();
                if (aCustomerEntryForm.houseNo != "") {
                    houseTextBox.Text = aCustomerEntryForm.houseNo.Trim();
                }
            }
            else
            {
                MessageBox.Show("Please enter valid  Postcode.");
            }
        }
    }
}