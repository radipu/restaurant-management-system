using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class DBsetup : Form
    {
        private Form aForm;
        public DBsetup(Form aForm)
        {
            InitializeComponent();
            labelText.Text = "Running " + Properties.Settings.Default.pcVersion;
            //labelText.Text = Properties.Settings.Default.connString;
            textBoxIP.Text = Properties.Settings.Default.ipaddress;
            textBoxdatabase.Text = Properties.Settings.Default.database;
            textBoxusername.Text = Properties.Settings.Default.username;
            textBoxpassword.Text = Properties.Settings.Default.password;
            txtServerAddress.Text = Properties.Settings.Default.serverIp;
            cmbDeviceType.Text = Properties.Settings.Default.deviceType;

            this.aForm = aForm;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.pcVersion = button1.Text;
            GlobalSetting.DbType = button1.Text;
            Properties.Settings.Default.Save();
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.pcVersion = button1.Text;
            Properties.Settings.Default.Save();
             
            panelhide.Visible = false;
            button1.Visible = false;
            Form parentForm = this.ParentForm;
            if (parentForm != null) parentForm.Close();
        }
        public void SaveDbPath()
        {

            con = "SERVER=" + textBoxIP.Text + ";DATABASE=" + textBoxdatabase.Text + ";UID=" +
                                  textBoxusername.Text + ";PASSWORD=" + textBoxpassword.Text + ";";

            Properties.Settings.Default.ipaddress = textBoxIP.Text;
            Properties.Settings.Default.database = textBoxdatabase.Text;
            Properties.Settings.Default.username = textBoxusername.Text;
            Properties.Settings.Default.password = textBoxpassword.Text;
            Properties.Settings.Default.serverIp = txtServerAddress.Text;
            Properties.Settings.Default.connString = con;
            Properties.Settings.Default.pcVersion = "MYSQL";
            GlobalSetting.DbType = "MYSQL";
            Properties.Settings.Default.deviceType = cmbDeviceType.Text;
            Properties.Settings.Default.Save();
            labelText.Text = "Connected to server.";
        }

        private string con = "";
        private void buttonCon_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeviceType.Text==string.Empty||textBoxIP.Text == "" || textBoxdatabase.Text == "" || textBoxusername.Text == "" ||textBoxpassword.Text == "" || txtServerAddress.Text==string.Empty)
                {
                    MessageBox.Show("Please enter proper settings.");
                }
                else
                {
                    Properties.Settings.Default.pcVersion = "MYSQL";
                    
                    Properties.Settings.Default.Save();
                    ConnectionModelSave modelSave=new ConnectionModelSave();

                    modelSave.database = textBoxdatabase.Text;
                    modelSave.ipadderss = textBoxIP.Text;
                    modelSave.username = textBoxusername.Text;
                    modelSave.password = textBoxpassword.Text;

                    // ConnectionString = "Server=" + MySqlHost + ";Database='" + MySqlDatabase + "';Username='" + MySqlUsername + "';Password='" + MySqlPassword + "';Pooling=true; Max Pool Size = 100; Min Pool Size = 5";
                    con = "SERVER=" + textBoxIP.Text + ";DATABASE=" + textBoxdatabase.Text + ";UID=" + textBoxusername.Text + ";PASSWORD=" + textBoxpassword.Text + ";";

                 
                    modelSave.connString = con;

                    if (MySqlGatewayConnection.IsExistDatatabase(modelSave))
                    {
                        MessageBox.Show(@"Connection Successfull.");
                       
                    }
                    else
                    {
                        if (MySqlGatewayConnection.CreateDataBase(modelSave) > 0)
                        {
                            MessageBox.Show(@"Datatabase Create Successfull.");
                           
                        }
                        else
                        {
                            MessageBox.Show(@"Please Check Database Connection or Others","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            this.Activate();
                        }
                    }
                    SaveDbPath();
                    this.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(@"Please Check Database Connection or Others", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Activate();
                //if (ex.InnerException.Message == "Unknown database '" + textBoxdatabase.Text + "'")
                //{
                //    string con = "SERVER=" + textBoxIP.Text + ";UID=" + textBoxusername.Text + ";PASSWORD=" +textBoxpassword.Text + ";";
                //    MySqlConnection connection = new MySqlConnection(con);
                //    connection.Open();
                //    string Query = String.Format("create database {0};", textBoxdatabase.Text);
                //    MySqlCommand command = new MySqlCommand(Query, connection);
                //    command.ExecuteNonQuery();
                //    connection.Close();
                //    SaveDbPath();

                //    this.Close();

                //}
                //    else
                //{
                //    MessageBox.Show(@"Please check connection string");
                //    this.Activate();
                //}

            }

        }

        private void DBsetup_Load(object sender, EventArgs e)
        {

        }

    
    }
}
