using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlDeliveryChargeDAO : MySqlGatewayConnection
    {
        public bool CheckDeliveryCharge(int restaurantId)
        {

            Query = String.Format("SELECT * FROM rcs_delivery_charge where  restaurant_id={0};", restaurantId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            bool hasData = false;
            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {
                hasData = true;
            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return hasData;
        }

        private DelvaryCharge ReaderToReadDelvaryCharge(DataTable oReader, int i)
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
            Query = String.Format("SELECT * FROM rcs_delivery_charge where  (`from` <={0}  AND `to`>={0}  AND  restaurant_id={1});", distance, restaurantId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DataTable dt = new DataTable();
            dt.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            if (dt.Rows.Count > 0)
            {
                aDelvaryCharge = ReaderToReadDelvaryCharge(dt, 0);


            }



            return aDelvaryCharge;
        }
        public List<DriverInformaiton> GetDriverInformation()
        {

            DriverInformaiton aDelvaryCharge = new DriverInformaiton();
            Query = String.Format("SELECT * FROM rcs_driver");
            List<DriverInformaiton> driverList = new List<DriverInformaiton>();

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DataTable dt = new DataTable();
            dt.Load(Reader);
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            if (dt.Rows.Count > 0)
            {
              
                foreach (DataRow dataRow in dt.Rows)
                {
                    DriverInformaiton informaiton = new DriverInformaiton();

                    informaiton.DriverId = dataRow["id"].ToString();
                    informaiton.DriverName = dataRow["name"].ToString();
                    informaiton.PhoneNo = dataRow["phone"].ToString();
                    informaiton.IsAvailable = Convert.ToBoolean(dataRow["is_available"]);
                    informaiton.Text = dataRow["name"].ToString();
                    informaiton.AppearanceItem.Normal.BackColor = Color.DodgerBlue;
                    informaiton.AppearanceItem.Selected.BackColor = Color.DodgerBlue;
                    informaiton.AppearanceItem.Normal.BorderColor = Color.White;
                    informaiton.TextAlignment=TileItemContentAlignment.TopCenter;informaiton.AppearanceItem.Normal.Font=new Font("Tahoma",10);
                    informaiton.AppearanceItem.Selected.Font = new Font("Tahoma", 10);
                    driverList.Add(informaiton);
                }


            }



            return driverList;
        }
    }
}
