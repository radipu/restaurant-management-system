using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class DistanceDAO : GatewayConnection
    {

        public Distance GetDistanceByPostcode(string restaurantPostCode, string Destination)
        {

            Distance distance = new Distance();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_distance where (Replace(source,' ','')='{0}' OR Replace(source,' ','')='{1}') AND (Replace(destination,' ','')='{2}' OR Replace(destination,' ','')='{3}' );",
                restaurantPostCode.ToUpper(), restaurantPostCode.ToLower(), Destination.ToUpper(), Destination.ToLower());

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);


            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                distance = ReaderToReadDistance(Reader);

            }


            return distance;
        }
        private Distance ReaderToReadDistance(IDataReader oReader)
        {
            Distance arcs_distance = new Distance();
            if (oReader["source"] != DBNull.Value)
            {
                arcs_distance.source = Convert.ToString(oReader["source"]);
            }
            if (oReader["destination"] != DBNull.Value)
            {
                arcs_distance.destination = Convert.ToString(oReader["destination"]);
            }
            if (oReader["distance"] != DBNull.Value)
            {
                arcs_distance.distance = Convert.ToDouble(oReader["distance"]);
            }

            if (oReader["id"] != DBNull.Value)
            {
                arcs_distance.Id = Convert.ToInt32(oReader["id"]);
            }


            return arcs_distance;
        }

        internal int InsertDistance(Distance distance)
        {
            int lastId = 0;

            Query = String.Format("INSERT INTO rcs_distance (source,destination,distance)" +
                         " VALUES ('{0}','{1}',{2});", distance.source, distance.destination, distance.distance);



            try
            {
                command = CommandMethod(command);

                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }


            return (int)lastId;
        }
    }
}
