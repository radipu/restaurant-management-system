using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class CoverageAreaDAO : GatewayConnection
    {
        CoverageAreaReader _coverageAreaReader=new CoverageAreaReader();
        public List<AreaCoverage> GetCoverageArea()
        {

            List<AreaCoverage> aAreaCoverages = new List<AreaCoverage>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_coverage_area;");

            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                AreaCoverage coverage = _coverageAreaReader.ReaderToReadAreaCoverage(Reader);
                aAreaCoverages.Add(coverage);

            }



            return aAreaCoverages;
        }

        //   $coverage_area = $this->db->where("(`restaurant_id` = $restaurant_id ) AND (REPLACE(postcode, ' ', '') = '$up_postcode' OR REPLACE(postcode, ' ', '') = '$postcode')", null, false)->get('coverage_area')->row();
        //   $area_charge = $this->db->where("(`restaurant_id` = $restaurant_id ) AND (REPLACE(postcode, ' ', '') = '$_up_postcode' OR REPLACE(postcode, ' ', '') = '$_postcode')", null, false)->get('coverage_area')->row();

        public AreaCoverage GetCoverageAreaByPostcode(string postCode, int restaurantId)
        {

            AreaCoverage coverage = new AreaCoverage();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_coverage_area where (Replace(postcode,' ','')=@postCode OR Replace(postcode,' ','')=@postCodeLower)  AND restaurant_id=@restaurantId");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@postCode", postCode);
            command.Parameters.AddWithValue("@postCodeLower", postCode.ToLower());
            command.Parameters.AddWithValue("@restaurantId", restaurantId);

            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                coverage = _coverageAreaReader.ReaderToReadAreaCoverage(Reader);

            }



            return coverage;
        }
     
    }
}
