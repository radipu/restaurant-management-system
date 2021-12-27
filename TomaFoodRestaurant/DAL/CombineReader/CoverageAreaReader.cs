using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
  public  class CoverageAreaReader
    {
      public AreaCoverage ReaderToReadAreaCoverage(IDataReader oReader)
      {
          AreaCoverage arcs_coverage_area = new AreaCoverage();
          if (oReader["restaurant_id"] != DBNull.Value)
          {
              arcs_coverage_area.RestaurantId = Convert.ToInt32(oReader["restaurant_id"]);
          }
          if (oReader["postcode"] != DBNull.Value)
          {
              arcs_coverage_area.Postcode = Convert.ToString(oReader["postcode"]);
          }
          if (oReader["delivery_charge"] != DBNull.Value)
          {
              arcs_coverage_area.DeliveryCharge = Convert.ToDouble(oReader["delivery_charge"]);
          }
          return arcs_coverage_area;
      }
    }
}
