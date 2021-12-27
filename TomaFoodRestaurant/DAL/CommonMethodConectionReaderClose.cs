using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TomaFoodRestaurant.DAL
{
    public class CommonMethodConectionReaderClose
    {
        public static bool Connection_ReaderClose(MySqlConnection connection, MySqlDataReader reader)
        {
           // return true;
            try
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
       }
    }
}
