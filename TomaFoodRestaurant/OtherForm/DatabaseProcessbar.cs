using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.NewLoginForm;
using Timer = System.Windows.Forms.Timer;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class DatabaseProcessbar : Form
    {
        private int type = 0;
        private string schema = "";
        private string data = "";
        private CheckSoftwareActivation formSoftwareActivation;
        public DatabaseProcessbar(string schema,string data, CheckSoftwareActivation t)
        {
            InitializeComponent();
            formSoftwareActivation = t;
            this.schema = schema;
            this.data = data.Replace("\r\n",String.Empty).Replace(");","));");
            //Download(0);
        }

        public DatabaseProcessbar(string schema, string data)
        {
            InitializeComponent();
          //  formSoftwareActivation = t;
            this.schema = schema;
            this.data = data.Replace("\r\n", String.Empty).Replace(");", "));");
            //Download(0);
        }
        
        
        int count = 1; 
        private List<string> schemaStrings;
        public string status;

        public List<string> SqlLiteSeparation(List<string> queryData)
        {List<string> newlist = new List<string>();


            int j = 0; string query="";
            for (int i = 0; i < queryData.Count; i++)
            {
                string Query = queryData[i] + ";";
                if (Query == "\r\n")
                {
                    count++;
                    continue;

                }
                if (j != 150)
                {
                    query += Query;
                    j++;
                    continue;
                }
                newlist.Add(query);
                j = 0;
                query = "";



            }
            return newlist;
        } 
        private void DatabaseProcessbar_Load(object sender, EventArgs e)
        {
            //progressBar1.Properties.Step = 1;

            string query="";
          
            string[] remove = { ");"};
            List<string> strings = new List<string>(data.Split(remove, StringSplitOptions.RemoveEmptyEntries));
            List<string> schemasList = new List<string>(schema.Split(';'));

            schemaStrings = SqlLiteSeparation(strings);
            //if (GlobalSetting.DbType=="MYSQL")
            //{
             
            // string optionAdd = File.ReadAllText("Config/mysql_db.txt");
             schemaStrings = schemasList.Concat(strings).ToList();
            // schemaStrings.Add(optionAdd);

            //}
            

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
            progressBar1.Properties.Minimum = 0;
            progressBar1.Properties.Maximum = schemaStrings.Count;

           
        }

        
        public int SchemaUpload(List<string> schema, ConnectionModelSave modelSave)
        {

            try
            {
                MySqlCommand comm;
                string conn = "SERVER=" + modelSave.ipadderss + ";UID=" + modelSave.username + ";PASSWORD=" + modelSave.password;
                MySqlConnection connection = new MySqlConnection(conn);
                connection.Open();
                string creteDb = String.Format("drop database {0};create database {0};", modelSave.database);
                comm = new MySqlCommand(creteDb, connection);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                connection.Close();

                string con = "SERVER=" + modelSave.ipadderss + ";UID=" + modelSave.username + ";PASSWORD=" + modelSave.password + ";database=" + modelSave.database;
                connection = new MySqlConnection(con);
                connection.Open();
               
                for (int i = 0; i < schemaStrings.Count; i++)
                {
                    string Query = schemaStrings[i];
                    if (Query.Replace("\r\n", "").Trim() == "" || Query.Replace("\n", "").Trim() == "")
                    {
                        count++;
                        continue;

                    }
                   
                    comm = new MySqlCommand(Query, connection);
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQuery();
                    count++;   
                    backgroundWorker1.ReportProgress(count);
                 

                }
                connection.Close();
                return count;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Query No: "+count +" " + schemaStrings[count-1] + exception.Message);
                return count;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {


            var connection = new ConnectionModelSave();
            if (GlobalSetting.DbType == "MYSQL")
            {
                SchemaUpload(schemaStrings, connection);}
            else
            {
                SchemaUploadForSqlLite(schemaStrings, connection);

            }
                            

        }

        private void SchemaUploadForSqlLite(List<string> list, ConnectionModelSave modelSave)
        {
            string conn = GlobalSetting.ConnectionString;
            SQLiteConnection connection = new SQLiteConnection(conn);
            SQLiteCommand command = null;
           DeleteTableAll(command,connection);
           

            connection.Open();
            for (int i = 0; i < list.Count; i++)
            {
                string Query = list[i];
                if (Query == "\r\n")
                {
                    count++;
                    continue;

                }
                command = new SQLiteCommand(Query.Replace("r\n",String.Empty), connection);
                command.ExecuteNonQuery();
                count++;
                backgroundWorker1.ReportProgress(count);
            }


            connection.Close();
        }

        private void DeleteTableAll(SQLiteCommand command,SQLiteConnection connection)
        {
            
            connection.Open();
            string dELETEQuery = "";
            for (int i = 0; i < Table().Count; i++)
            {
                dELETEQuery += "DELETE FROM " + Table()[i] + ";";
            }

            command = new SQLiteCommand(dELETEQuery.Replace("r\n", String.Empty), connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Position = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (count >0)
            {
                status = "Database has been Updated Successfully";
                formSoftwareActivation.statusLabel.Text = status;
                DialogResult result = MessageBox.Show("Please Restart the Application !", "Warning",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                formSoftwareActivation.statusUpdateTimer.Enabled = true;
                
                if (result == DialogResult.OK)
                {

                    var parentForm = this.ParentForm;
                    if (parentForm != null) parentForm.Close();
                    NewLoginForm.LoginForm form = new LoginForm();
                    form.Show();

                    
                }
                else 
                {
                    //GlobalSetting.RestaurantInformation.Id = Convert.ToInt32(formSoftwareActivation.restaurantIdTextBox.Text.Trim());
                    this.formSoftwareActivation.Activate();
                }
            }
            else
            {
                status = "Download Failed";
                formSoftwareActivation.statusLabel.Text = status;
            }
           

        }


        private List<string> Table()
        {
            List<string> tableList=new List<string>();
            tableList.Add("rcs_restaurant");
            tableList.Add("rcs_restaurant_license");
            tableList.Add("rcs_recipe_subcategories");
            tableList.Add("rcs_recipe_category");
            tableList.Add("rcs_recipe_type_categories");
            tableList.Add("rcs_recipe_types");
            tableList.Add("rcs_attribute");
            tableList.Add("rcs_online_recipe");
            tableList.Add("rcs_online_recipe_category");
            tableList.Add("rcs_option_item");
            tableList.Add("rcs_option_items");
            tableList.Add("rcs_package_recipe");
            tableList.Add("rcs_package_category");
            tableList.Add("rcs_package");
            tableList.Add("rcs_restaurant_recipe_option");
            tableList.Add("rcs_recipe_option");
            tableList.Add("rcs_recipe");
            tableList.Add("rcs_recipes");
            tableList.Add("rcs_recipe");
            tableList.Add("rcs_restaurant_table");


            
            return tableList;
        }


    }
}
