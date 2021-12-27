using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
   public class MySqlDatabaseSyncDAO:MySqlGatewayConnection
   {
        internal string DeleteAllTable(string restaurantdata, string tableSchema)
        {
            string res = "";

            List<string> tableName = new List<string>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();


          ////  Query = String.Format("SELECT * FROM sqlite_master WHERE type=@table");
   

            Query = String.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'  AND TABLE_SCHEMA='{0}';", Properties.Settings.Default.database);
            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                try
                {
                    string sr = Reader[0].ToString();
                    if (!IsLocalDBTable(sr))
                    {
                        tableName.Add(sr);
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            Reader.Close();
            bool flag = false;
            Query = String.Format("drop database {0};create database {0};", Properties.Settings.Default.database);
            command = CommandMethod(command);
            Transaction = TransactionMethod(Transaction);
            int id = command.ExecuteNonQuery();
            if (id > 0) flag = true;
            Transaction.Commit();
            Console.WriteLine("{0} seconds with one transaction.",stopwatch.Elapsed.TotalSeconds);
            if (tableName.Count() == 0)
            {
                flag = true;
            }
            if (flag)
            {
                flag = false;
                Transaction = TransactionMethod(Transaction);
                Query = "USE " + Properties.Settings.Default.database +";"+ tableSchema;
                //Query = tableSchema;
                command = CommandMethod(command);


                try
                {
                    flag = true;
                    int lastId = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            Transaction.Commit();


            Console.WriteLine("{0} seconds with one transaction.",
             stopwatch.Elapsed.TotalSeconds);
            flag = false;
            Query = restaurantdata;
            
            Transaction = TransactionMethod(Transaction);
            command = CommandMethod(command);
            

            try
            {
                flag = true;
                int lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }


            Transaction.Commit();

            //string tblSchemaUpdate = File.ReadAllText("Config/mysql_db.txt");


            //Query = tblSchemaUpdate;
            //Transaction = TransactionMethod(Transaction);

            //command = CommandMethod(command);
            //try
            //{
            //    flag = true;
            //    int lastId = command.ExecuteNonQuery();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}



            if (flag)
            {
                res = "Database has been Updated Successfully";
            }


            Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);



            if (res == "")
            {
                res = "Something wrong please try again.";
            }
            return res;
        }


       public List<string> SeperateCustomerScriptData(string data)
       {
           string[] remove = { ");" };
           List<string> strings = new List<string>(data.Split(remove, StringSplitOptions.RemoveEmptyEntries));

           return strings;
       }
       

        internal string AddCustomerDataIntoDatabase(string customerData)
        {
            string res = "";
            bool flag = false;

            Transaction = TransactionMethod(Transaction);
            string data = customerData.Replace("\r\n", String.Empty).Replace(");", "));");
            List<string> LISTOFQUERY= SeperateCustomerScriptData(data);
            int count = 0;
            for(int i = 0; i < LISTOFQUERY.Count-1; i++)
            {
                try
                {
                    Query = LISTOFQUERY[i];
                    command = CommandMethod(command);
                    count += command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (LISTOFQUERY.Count > 0)
            {
                flag = true;
            }
           // Query = customerData;
            Transaction.Commit();
            if (flag)
            {
                res = "Database has been Updated Successfully";
            }

            if (res == "")
            {
                res = "Something wrong please try again.";
            }
            return res;
        }

        private bool IsLocalDBTable(string name)
        {
            if (name == "PrintCopySetup") return true;
            if (name == "PrinterSetup") return true;
            if (name == "UrlSetup") return true;

            return false;
        }

        internal string updateTable(string restaurantdata, string tableSchema)
        {
            string res = "";
            List<string> tableName = new List<string>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Query = String.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'  AND TABLE_SCHEMA='{0}';", Properties.Settings.Default.database);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                try
                {
                    string sr = Reader[0].ToString();
                    if (IsLocalTable(sr))
                    {
                        tableName.Add(sr);
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }
            Reader.Close();

            bool flag = false;

            Transaction = TransactionMethod(Transaction);

            foreach (string name in tableName)
            {
                if (IsLocalTable(name))
                {
                    Query = String.Format("DELETE FROM {0}; ", name);
                    command = CommandMethod(command);


                    try
                    {
                        flag = true;
                        int lastId = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }
            }
            Transaction.Commit();            
            Console.WriteLine("{0} seconds with one transaction.",
            stopwatch.Elapsed.TotalSeconds);

            if (tableName.Count() == 0)
            {
                flag = true;
            }

            if (flag)
            {
                flag = false;

                Console.WriteLine("{0} seconds with one transaction.",
                 stopwatch.Elapsed.TotalSeconds);

                flag = false;
                Query = restaurantdata; 

                Transaction = TransactionMethod(Transaction);

                command = CommandMethod(command);

                try
                {
                    flag = true;
                    int lastId = command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
                Transaction.Commit();
            }
            
            if (flag)
            {
                res = "Database has been Updated Successfully";
            }

            Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);

            if (res == "")
            {
                res = "Something wrong please try again.";
            }
            return res;
        }

        private bool IsLocalTable(string name){

            if (name == "rcs_restaurant") return true;
            if (name == "rcs_restaurant_kitchen") return true;
            if (name == "rcs_recipe_subcategories") return true;
            if (name == "rcs_recipe_subcategoy") return true;
            if (name == "rcs_recipe_category") return true;
            if (name == "rcs_recipe_type_categories") return true;
            if (name == "rcs_recipe_type") return true;
            if (name == "rcs_recipe_categories") return true;
            if (name == "rcs_recipe_types") return true;
            if (name == "rcs_attribute") return true;
            if (name == "rcs_online_recipe") return true;
            if (name == "rcs_online_recipe_category") return true;
            if (name == "rcs_option_item") return true;
            if (name == "rcs_option_items") return true;
            if (name == "rcs_package_recipe") return true;
            if (name == "rcs_package_category") return true;
            if (name == "rcs_package") return true;
            if (name == "rcs_restaurant_recipe_option") return true;
            if (name == "rcs_recipe_option") return true;
            if (name == "rcs_recipe_photo") return true;
            if (name == "rcs_recipe") return true;
            if (name == "rcs_recipes") return true;
            if (name == "rcs_extra_price") return true;
            if (name == "rcs_restaurant_table") return true;
            if (name == "rcs_delivery_charge") return true;
            if (name == "rcs_coverage_area") return true;
            if (name == "rcs_mod_setting") return true;
            if (name == "rcs_driver") return true;
            if (name == "rcs_restaurant_email") return true;
            return false;
        }

       public List<string> GetColumnNames(string tableName)
       {

           List<string> columns = new List<string>();
           Query = String.Format("SELECT * from {0};", tableName);
           command = CommandMethod(command);
           Reader = ReaderMethod(Reader, command);
           for (int i = 0; i < Reader.FieldCount; i++)
               columns.Add(Reader.GetName(i));
           Reader.Close();
           return columns;
       }
   }
}
