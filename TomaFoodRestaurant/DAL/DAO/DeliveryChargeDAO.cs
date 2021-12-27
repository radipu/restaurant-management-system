using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class DeliveryChargeDAO : GatewayConnection
    {
        public bool CheckDeliveryCharge(int restaurantId)
        {

            Query = String.Format("SELECT * FROM rcs_delivery_charge where  restaurant_id={0};", restaurantId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            
            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {
                return true;
            }
            
            return false;
        }

        private DelvaryCharge ReaderToReadDelvaryCharge(DataTable oReader,int i)
        {
            DelvaryCharge delvaryCharge = new DelvaryCharge();

            if (oReader.Rows[i]["id"] != DBNull.Value)
            {
                delvaryCharge.Id = Convert.ToInt32(oReader.Rows[i]["id"]);
            }
            if (oReader.Rows[i]["restaurant_id"] != DBNull.Value)
            {
                delvaryCharge.RestaurantId = Convert.ToInt32(oReader.Rows[i]["restaurant_id"]);
            }
            if (oReader.Rows[i]["from"] != DBNull.Value)
            {
                delvaryCharge.from = Convert.ToInt32(oReader.Rows[i]["from"]);
            }
            if (oReader.Rows[i]["to"] != DBNull.Value)
            {
                delvaryCharge.to = Convert.ToInt32(oReader.Rows[i]["to"]);
            }
            if (oReader.Rows[i]["amount"] != DBNull.Value)
            {
                delvaryCharge.amount = Convert.ToDouble(oReader.Rows[i]["amount"]);
            }

            return delvaryCharge;
        }

        public DelvaryCharge GetDeliveryChargeByDistance(double distance, int restaurantId)
        {

            DelvaryCharge aDelvaryCharge = new DelvaryCharge();
            Query = String.Format("SELECT * FROM rcs_delivery_charge where  (\"from\" <={0}  AND \"to\">={0}  AND  restaurant_id={1});", distance, restaurantId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DataTable dt=new DataTable();
            dt.Load(Reader);
            if (dt.Rows.Count>0)
            {
                aDelvaryCharge = ReaderToReadDelvaryCharge(dt,0);

                
            }

          

            return aDelvaryCharge;
        }
    }
}
