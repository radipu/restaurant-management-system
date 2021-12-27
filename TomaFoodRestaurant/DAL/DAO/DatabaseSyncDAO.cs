using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class DatabaseSyncDAO : GatewayConnection
    {
        
        internal string DeleteAllTable(string restaurantdata, string tableSchema)
        {

            string res = "";
            try
            {
                List<string> tableName = new List<string>();


                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Query = String.Format("SELECT * FROM sqlite_master WHERE type=@table");

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@table", "IN");
                Transaction = TransactionMethod(Transaction);
                Reader = ReaderMethod(Reader, command);

                // dataRow = command.ExecuteReader();
                while (Reader.Read())
                {

                    try{
                        string sr = Reader[2].ToString();
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
                //   dataRow.Close();

                bool flag = false;



                foreach (string name in tableName)
                {
                    if (name != "sqlite_sequence" && !IsLocalDBTable(name))
                    {
                        Query = String.Format("DROP TABLE IF EXISTS '{0}'; ", name);


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





                Console.WriteLine("{0} seconds with one transaction.",
                stopwatch.Elapsed.TotalSeconds);

                if (tableName.Count() == 0)
                {
                    flag = true;
                }

                if (flag)
                {
                    flag = false;

                    Query = tableSchema;

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

             

                Console.WriteLine("{0} seconds with one transaction.",
                 stopwatch.Elapsed.TotalSeconds);

                flag = false;
                Query = restaurantdata;


                command = CommandMethod(command);



                try
                {
                    flag = true;
                    int lastId = command.ExecuteNonQuery();

                }
                catch (Exception exception)
                {
                    Transaction.Rollback();
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
                // ************ Table Schema **************************

                string tblSchemaUpdate = File.ReadAllText("Config/sqlite_db.txt");


                Query = tblSchemaUpdate;
                Transaction = TransactionMethod(Transaction);

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

                // ************ Table Schema **************************

                


                if (flag)
                {
                    res = "Database has been Updated Successfully";
                }


                Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);

                if (res == "")
                {
                    res = "Something wrong please try again.";}

            }
            catch (Exception ex)
            {ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
                Transaction.Rollback();

            }


            return res;
        }


        public List<string> SqlLiteSeparation(List<string> queryData)
        {
            List<string> newlist = new List<string>();

            int count = 0;
            int j = 0; string query = "";
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


        internal string AddCustomerDataIntoDatabase(string customerData)
        {
            string res = "";
            bool flag = false;
            string data = customerData.Replace("\r\n", String.Empty).Replace(");", "));");

          
            string[] remove = { ");" };
            List<string> strings = new List<string>(data.Split(remove, StringSplitOptions.RemoveEmptyEntries));

           // Query = customerData;

           
            int lastId = 0;
            for (int i = 0; i < strings.Count-1; i++)
            {
              
                try
                {
                    Query = strings[i];

                    command = CommandMethod(command);
                    lastId+= command.ExecuteNonQuery();
                    
                    flag = true;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            
       
            if (lastId>0)
            {
                if (flag)
                {
                    res = "Database has been Updated Successfully";
                }

 
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
            Connection.Open();using (var tra = Connection.BeginTransaction())
            {
                
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    string query = String.Format("SELECT * FROM sqlite_master WHERE type='{0}';", "IN");
                    command = new SQLiteCommand(query, Connection, tra);
                    Reader = command.ExecuteReader();
                    while (Reader.Read())
                    {
                        string sr = Reader[2].ToString();
                        if (IsLocalTable(sr))
                        {
                            tableName.Add(sr);
                        }
                       
                    }
                    Reader.Close();
                    bool flag = false;
                    foreach (string name in tableName)
                    {
                        if (name != "sqlite_sequence" && IsLocalTable(name))
                        {
                            query = String.Format("Delete from  {0}; DELETE FROM SQLITE_SEQUENCE WHERE name='{0}';",
                                name);

                            command = new SQLiteCommand(query, Connection, tra);
                            flag = true;
                            int lastId = command.ExecuteNonQuery();


                        }
                    }
                    if (tableName.Count() == 0)
                    {
                        flag = true;
                    }

                    if (flag)
                    {


                        flag = false;
                        query = restaurantdata;
                        command = new SQLiteCommand(query, Connection, tra);
                        flag = true;
                        int lastId = command.ExecuteNonQuery();



                        if (flag)
                        {
                            res = "Database has been Updated Successfully";
                        }

                    }
                    
                    tra.Commit();
                }
                catch (Exception exception)
                {
                    tra.Rollback();

                    Connection.Close();

                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());

                }



            }

            //using (SQLiteConnection c = new SQLiteConnection(GlobalSetting.ConnectionString, true))
            //{
            //    c.Open();
            //    var stopwatch = new Stopwatch();
            //    stopwatch.Start();



            //    using (SQLiteCommand command = new SQLiteCommand(query, c))
            //    {

            //        using (SQLiteDataReader dataRow = command.ExecuteReader())
            //        {
            //            // dataRow = command.ExecuteReader();
            //            while (dataRow.Read())
            //            {

            //                try
            //                {
            //                    string sr = dataRow[2].ToString();
            //                    if (IsLocalTable(sr))
            //                    {
            //                        tableName.Add(sr);
            //                    }
            //                     dataRow.Close();

            //                }
            //                catch (Exception exception)
            //                {
            //                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //                    aErrorReportBll.SendErrorReport(exception.ToString());
            //                }


            //            }
            //            dataRow.Close();

            //            bool flag = false;
            //            using (SQLiteTransaction mytransaction = c.BeginTransaction())
            //            {
            //                foreach (string name in tableName)
            //                {
            //                    if (name != "sqlite_sequence" && IsLocalTable(name))
            //                    {
            //                        query = String.Format("Delete from  {0}; DELETE FROM SQLITE_SEQUENCE WHERE name='{0}';", name);

            //                        using (SQLiteCommand command1 = new SQLiteCommand(query, c,mytransaction))
            //                        {

            //                            try
            //                            {
            //                                flag = true;
            //                                int lastId = command1.ExecuteNonQuery();
            //                                mytransaction.Commit();
            //                            }
            //                            catch (Exception ex)
            //                            {
            //                                mytransaction.Rollback();
            //                                MessageBox.Show(ex.ToString());
            //                            }


            //                        }
            //                    }
            //                }

            //            }

            //            Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);

            //            if (tableName.Count() == 0)
            //            {
            //                flag = true;
            //            }
            //            if (flag)
            //            {
            //                //flag = false;

            //                //query = tableSchema;
            //                //using (SQLiteTransaction mytransaction = c.BeginTransaction())
            //                //{
            //                //    using (SQLiteCommand command2 = new SQLiteCommand(query, c))
            //                //    {

            //                //        try
            //                //        {
            //                //            flag = true;
            //                //            int lastId = command2.ExecuteNonQuery();

            //                //        }
            //                //        catch (Exception ex)
            //                //        {
            //                //            MessageBox.Show(ex.ToString());
            //                //        }


            //                //    }
            //                //    mytransaction.Commit();
            //                //}

            //                //Console.WriteLine("{0} seconds with one transaction.",stopwatch.Elapsed.TotalSeconds);

            //                flag = false;
            //                query = restaurantdata;

            //                using (SQLiteTransaction mytransaction = c.BeginTransaction())
            //                {
            //                    using (SQLiteCommand command2 = new SQLiteCommand(query, c,mytransaction))
            //                    {
            //                        try
            //                        {
            //                            flag = true;
            //                            int lastId = command2.ExecuteNonQuery();
            //                            mytransaction.Commit();
            //                        }
            //                        catch (Exception exception)
            //                        {
            //                            mytransaction.Rollback();
            //                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //                            aErrorReportBll.SendErrorReport(exception.ToString());
            //                        }

            //                    }

            //                }


            //                if (flag)
            //                {
            //                    res = "Database has been Updated Successfully";
            //                }

            //            }

            //        }

            //    }
            //    Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);

            //    c.Close();

            //}
            if (res == "")
            {
                res = "Something wrong please try again.";
            }
            return res;
        }


        internal string updateTable1(string restaurantdata, string tableSchema)
        {
            string res = "";

            List<string> tableName = new List<string>();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Query = String.Format("SELECT * FROM sqlite_master WHERE type='{0}';", "IN");
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read())
            {
                try
                {
                    string sr = Reader[2].ToString();
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

            //   Transaction = TransactionMethod(Transaction);

            foreach (string name in tableName)
            {
                if (name != "sqlite_sequence" && IsLocalTable(name))
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
            Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);
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
                Query = "";
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
                //}

                //Transaction = TransactionMethod(Transaction);
                //using (SQLiteCommand command2 = new SQLiteCommand(Query, c))
                //{
                //    try
                //    {
                //        flag = true;
                //        int lastId = command2.ExecuteNonQuery();

                //    }
                //    catch (Exception exception)
                //    {
                //        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                //        aErrorReportBll.SendErrorReport(exception.ToString());
                //    }

                //}
                //Transaction.Commit();
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

        //private bool IsLocalTable(string name)
        //{
        //    if (name == "PrintCopySetup") return true;
        //    if (name == "PrinterSetup") return true;
        //    if (name == "UrlSetup") return true;
        //    if (name == "rcs_booking") return true;
        //    if (name == "rcs_restaurant_user") return true;
        //    if (name == "rcs_users") return true;
        //    if (name == "rcs_membership") return true;
        //    if (name == "rcs_membership_type") return true;
        //    if (name == "rcs_user_review") return true;
        //    if (name == "rcs_user_types") return true;
        //    if (name == "rcs_restaurant_table") return true;
        //    if (name == "rcs_restaurant_license") return true;
        //    if (name == "rcs_restaurant") return true;
        //    if (name == "rcs_reserve_table") return true;
        //    if (name == "rcs_postcode") return true;
        //    if (name == "rcs_payment_module") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true;
        //    if (name == "rcs_recipe_types") return true; 

        //    return false;
        //}

        private bool IsLocalTable(string name)
        {

            if (name == "rcs_restaurant") return true;
            if (name == "rcs_restaurant_license") return true;
            if (name == "rcs_recipe_subcategories") return true;
            if (name == "rcs_recipe_subcategory") return true;
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
           // if (name == "rcs_restaurant_table") return true;  
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
