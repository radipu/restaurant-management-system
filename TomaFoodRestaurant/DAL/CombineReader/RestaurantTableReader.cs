using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
  public  class RestaurantTableReader
    {
      public RestaurantTable ReaderToReadRestaurantTable(DataTable oReader, int i)
      {
          RestaurantTable arcs_restaurant_table = new RestaurantTable();
          if (oReader.Rows[i]["id"] != DBNull.Value)
          {
              arcs_restaurant_table.Id = Convert.ToInt32(oReader.Rows[i]["id"]);
          }
          if (oReader.Rows[i]["restaurant_id"] != DBNull.Value)
          {
              arcs_restaurant_table.RestaurantId = Convert.ToInt32(oReader.Rows[i]["restaurant_id"]);
          }
          if (oReader.Rows[i]["name"] != DBNull.Value)
          {
              arcs_restaurant_table.Name = Convert.ToString(oReader.Rows[i]["name"]);
          }
          if (oReader.Rows[i]["person"] != DBNull.Value)
          {
              arcs_restaurant_table.Person = Convert.ToInt32(oReader.Rows[i]["person"]);
          }
          if (oReader.Rows[i]["table_shape"] != DBNull.Value)
          {
              arcs_restaurant_table.TableShape = Convert.ToString(oReader.Rows[i]["table_shape"]);
          }
          if (oReader.Rows[i]["sort_order"] != DBNull.Value)
          {
              arcs_restaurant_table.SortOrder = Convert.ToInt32(oReader.Rows[i]["sort_order"]);
          }
          if (oReader.Rows[i]["current_status"] != DBNull.Value)
          {
              arcs_restaurant_table.CurrentStatus = Convert.ToString(oReader.Rows[i]["current_status"]);
          }
          try
          {
              if (oReader.Rows[i]["update_time"] != DBNull.Value && (string) oReader.Rows[i]["update_time"] != "")
              {
                 arcs_restaurant_table.UpdateTime = Convert.ToDateTime(oReader.Rows[i]["update_time"]);
              }
          }
          catch (Exception exception)
          {
              arcs_restaurant_table.UpdateTime = DateTime.Now;

              //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
              //aErrorReportBll.SendErrorReport(exception.ToString());
          }

          //MergeStatus
          try
          {
              if (oReader.Rows[i]["MergeStatus"] != DBNull.Value)
              {
                  arcs_restaurant_table.MergeStatus = Convert.ToInt32(oReader.Rows[i]["MergeStatus"]);
              }
          }
          catch (Exception exception)
          {
              ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
              aErrorReportBll.SendErrorReport(exception.ToString());
          }

          if (arcs_restaurant_table.CurrentStatus == "bill")
          {
              arcs_restaurant_table.CurrentStatus = "busy";
              arcs_restaurant_table.IsBill = true;
          }


          return arcs_restaurant_table;
      }

    }
}
