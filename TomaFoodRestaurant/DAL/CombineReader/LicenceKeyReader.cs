using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
    public class LicenceKeyReader
    {
        public LicenceKey ReaderToReadrcs_restaurant_license(DataTable oReader, int i)
        {
            LicenceKey arcs_restaurant_license = new LicenceKey();
            if (oReader.Rows[i]["restaurant_id"] != DBNull.Value)
            {
                arcs_restaurant_license.restaurant_id = Convert.ToInt32(oReader.Rows[i]["restaurant_id"]);
            }
            if (oReader.Rows[i]["license_code"] != DBNull.Value)
            {
                arcs_restaurant_license.license_code = Convert.ToString(oReader.Rows[i]["license_code"]);
            }
            if (oReader.Rows[i]["is_installed"] != DBNull.Value)
            {
                arcs_restaurant_license.is_installed = Convert.ToInt64(oReader.Rows[i]["is_installed"]);
            }
            if (oReader.Rows[i]["date_installed"] != DBNull.Value)
            {
                arcs_restaurant_license.date_installed = Convert.ToString(oReader.Rows[i]["date_installed"]);
            }

            if (oReader.Rows[i]["hardware_info"] != DBNull.Value)
            {
                arcs_restaurant_license.hardware_info = Convert.ToString(oReader.Rows[i]["hardware_info"]);
            }

            if (oReader.Rows[i]["IsonlineOrderCheck"] != DBNull.Value)
            {
                arcs_restaurant_license.onlineConnect = Convert.ToString(oReader.Rows[i]["IsonlineOrderCheck"]);
            } 
            if (oReader.Rows[i]["ViewPage"] != DBNull.Value)
            {
                arcs_restaurant_license.viewpage = Convert.ToString(oReader.Rows[i]["ViewPage"]);
            }
            if (oReader.Rows[i]["IstillEnable"] != DBNull.Value)
            {
                arcs_restaurant_license.till = Convert.ToString(oReader.Rows[i]["IstillEnable"]);
            }
            if (oReader.Rows[i]["IsReservationCheck"] != DBNull.Value)
            {
                arcs_restaurant_license.IsReservationCheck = Convert.ToBoolean(oReader.Rows[i]["IsReservationCheck"]);
            }
            if (oReader.Rows[i]["IsCardVisible"] != DBNull.Value)
            {
                arcs_restaurant_license.IsCardVisible = Convert.ToBoolean(oReader.Rows[i]["IsCardVisible"]);
            }
            if (oReader.Rows[i]["IsCallerId"] != DBNull.Value)
            {
                arcs_restaurant_license.IsCallerId = Convert.ToBoolean(oReader.Rows[i]["IsCallerId"]);
            }

            //if (oReader["app_info"] != DBNull.Value)
            //{
            //    arcs_restaurant_license.app_info = Convert.ToString(oReader["app_info"]);
            //}

            //if (oReader["online_info"] != DBNull.Value)
            //{
            //    arcs_restaurant_license.online_info = Convert.ToString(oReader["online_info"]);
            //}
            return arcs_restaurant_license;
        }
    }
}
