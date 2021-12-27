using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;
using System.Text.RegularExpressions;
using System.IO;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlPostcode : MySqlGatewayConnection
    {

        private Postcode ReaderToReadPostcodeTable(IDataReader oReader)
        {
            Postcode arcs_postcode = new Postcode();
            if (oReader["id"] != DBNull.Value)
            {
                arcs_postcode.Id = Convert.ToInt64(oReader["id"]);
            }

            if (oReader["HouseName"] != DBNull.Value)
            {
                arcs_postcode.HouseName = Convert.ToString(oReader["HouseName"]);
            }
            if (oReader["HouseNumber"] != DBNull.Value)
            {
                arcs_postcode.HouseNumber = Convert.ToString(oReader["HouseNumber"]);
            }
            if (oReader["AddressLine1"] != DBNull.Value)
            {
                arcs_postcode.AddressLine1 = Convert.ToString(oReader["AddressLine1"]);
            }
            if (oReader["AddressLine2"] != DBNull.Value)
            {
                arcs_postcode.AddressLine2 = Convert.ToString(oReader["AddressLine2"]);
            }
            if (oReader["Town"] != DBNull.Value)
            {
                arcs_postcode.Town = Convert.ToString(oReader["Town"]);
            }
            if (oReader["Postcode"] != DBNull.Value)
            {
                arcs_postcode.PostCode = Convert.ToString(oReader["Postcode"]);
            }

            if (oReader["latitude"] != DBNull.Value)
            {
                arcs_postcode.Latitude = Convert.ToString(oReader["latitude"]);
            }

            if (oReader["longitude"] != DBNull.Value)
            {
                arcs_postcode.Longitude = Convert.ToString(oReader["longitude"]);
            }


            return arcs_postcode;
        }

        public List<Postcode> GetHouseByPostcode(string post_code)
        {

            List<Postcode> aPostcodes = new List<Postcode>();
             try
            { 
            post_code = post_code.Replace(" ", "");
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM vw_read_paf where Postcode = @postCode;");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@postCode", post_code);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                Postcode _postcode = ReaderToReadPostcodeTable(Reader);
                aPostcodes.Add(_postcode);

            }
            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            }
             catch (Exception exception)
             {
                 Query = File.ReadAllText("Config/postcode.txt").ToString();
                 command = CommandMethod(command);
                 command.ExecuteNonQuery();
             }
            return aPostcodes;
        }
         

        public int InsertPostcodeArray(List<Postcode> _postcodes)
        {
            int lastId = 0;
            int postcodes_count = _postcodes.Count / 500;
            NewProgressBar bar = new NewProgressBar(postcodes_count);
            bar.Show();
          //  Transaction = TransactionMethod(Transaction);
            try
            { 
                Query = File.ReadAllText("Config/postcode.txt");
                command = CommandMethod(command);
                command.ExecuteNonQuery();
                int i = 1;

                //foreach (Postcode postcodes in _postcodes)
                //{
                //    Query =
                //        String.Format(
                //            "INSERT INTO rcs_restaurant_postcode (`id`, `HouseNumber`, `HouseName`, `AddressLine1`, `AddressLine2`, `Town`, `Postcode`)" +
                //            " VALUES (@Id,@HouseNumber,@HouseName,@AddressLine1,@AddressLine2,@Town,@Postcode)");
                //    command = CommandMethod(command);
                //    command.Parameters.AddWithValue("@Id", postcodes.Id);
                //    command.Parameters.AddWithValue("@HouseNumber", postcodes.HouseNumber);
                //    command.Parameters.AddWithValue("@HouseName", postcodes.HouseName);
                //    command.Parameters.AddWithValue("@AddressLine1", postcodes.AddressLine1);
                //    command.Parameters.AddWithValue("@AddressLine2", postcodes.AddressLine2);
                //    command.Parameters.AddWithValue("@Town", postcodes.Town);
                //    command.Parameters.AddWithValue("@Postcode", postcodes.PostCode);
                //    lastId = command.ExecuteNonQuery();
                //    CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
                //    bar.progressBar(i++);
                //}

                int counter = 0;
                string sqlcommend = "";
                Query = "";
                Query = String.Format("INSERT INTO `rcs_restaurant_postcode` (`HouseNumber`, `HouseName`, `AddressLine1`, `AddressLine2`, `Town`, `Postcode`) VALUES ");
                Regex reg = new Regex("[*'\",_&#^@\'\"]");
                foreach (Postcode postcodes in _postcodes)
                {
                    int modresult = counter % 100;
                    if (counter < 100)
                    {
                        Query += "('" + reg.Replace(postcodes.HouseNumber, string.Empty) + "','" + reg.Replace(postcodes.HouseName, string.Empty) + "','" + reg.Replace(postcodes.AddressLine1, string.Empty) + "','" + reg.Replace(postcodes.AddressLine2, string.Empty) + "','" + reg.Replace(postcodes.Town, string.Empty) + "','" + postcodes.PostCode + "'),";
                        //    if (counter > 0)
                        //    {
                        //        string sql_commend = Query.Substring(0, Query.Length - 1);
                        //        Query = sql_commend;
                        //        command.ExecuteNonQuery();
                        //        CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
                        //        bar.progressBar(i++);
                        //        command = CommandMethod(command);
                        //        sqlcommend = "";
                        //        Query = String.Format("INSERT INTO rcs_restaurant_postcode (`id`, `HouseNumber`, `HouseName`, `AddressLine1`, `AddressLine2`, `Town`, `Postcode`) VALUES ");
                        //    }
                        //    }
                        //
                        // Query += "(@Id" + counter + ",@HouseNumber" + counter + ",@HouseName" + counter + ",@AddressLine1" + counter + ",@AddressLine2" + counter + ",@Town" + counter + ",@Postcode" + counter + "),";
                        //command = CommandMethod(command);
                        //command.Parameters.AddWithValue("@Id" + counter, postcodes.Id);
                        //command.Parameters.AddWithValue("@HouseNumber" + counter, postcodes.HouseNumber);
                        //command.Parameters.AddWithValue("@HouseName" + counter, postcodes.HouseName);
                        //command.Parameters.AddWithValue("@AddressLine1" + counter, postcodes.AddressLine1);
                        //command.Parameters.AddWithValue("@AddressLine2" + counter, postcodes.AddressLine2);
                        //command.Parameters.AddWithValue("@Town" + counter, postcodes.Town);
                        //command.Parameters.AddWithValue("@Postcode" + counter, postcodes.PostCode);
                        counter++;
                    }
                    else {
                        string sql_commend = Query.Substring(0, Query.Length - 1);
                        command = CommandMethod(command);
                        Query = sql_commend + ";";
                        command.ExecuteNonQuery();
                        CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
                       //break;
                    }
                }
            }
            catch (Exception ex) {
                bar.Close();
              //  Transaction.Rollback();
            }

          //  Transaction.Commit();
            return 1;
        }


        internal int InsertPostcodeData(string json)
        {
            int lastId = 0;
       //     int postcodes_count = _postcodes.Count / 500;
          
            //  Transaction = TransactionMethod(Transaction);
            try
            {
                Query = File.ReadAllText("Config/postcode.txt").ToString();
                command = CommandMethod(command);
                command.ExecuteNonQuery();
             //   CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                 string[]  sqlArray = Regex.Split(json,"\",\"");  
                  for (int i = 0; i < sqlArray.Count(); i++)
            {
                string jsonNew = sqlArray[i].Replace("\\", "");
                jsonNew = jsonNew.Replace("[", "");
                jsonNew = jsonNew.Replace("]", "");
                
                Query = jsonNew.Replace("\"", "");
                command = CommandMethod(command);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
               
              //  CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
                     
            }
            catch (Exception ex)
            {
              //  bar.Close();
               
            } 
            return 1;
        }

       



    }
}
