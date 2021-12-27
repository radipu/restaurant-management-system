using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
 public   class RecentCustomerReader
    {
        public CustomerRecentItemMD ReaderToReadCustomerRecentItem(DataTable oReader, int i)
        {
            CustomerRecentItemMD item = new CustomerRecentItemMD();
            if (oReader.Columns["customer_id"] != null)
            {
                item.customer_id = Convert.ToInt32(oReader.Rows[i]["customer_id"]);
            }

            if (oReader.Columns["package_id"] != null)
            {
                item.package_id = Convert.ToInt32(oReader.Rows[i]["package_id"]);
            }
            if (oReader.Columns["recipe_id"] != null)
            {
                item.recipe_id = Convert.ToInt32(oReader.Rows[i]["recipe_id"]);
            }
            if (oReader.Columns["time_added"] != null)
            {
                item.time_added = Convert.ToString(oReader.Rows[i]["time_added"]);
            }
            return item;

        }
    }
}
