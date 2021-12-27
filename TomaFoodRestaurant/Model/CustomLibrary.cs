using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GHospital_Care.CustomLibry
{
   public class CustomLibrary
    {
       public DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();
           // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }
           // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }
            
           // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }
            return table;
        }

       public DataTable ToDataTable<T>(List<T> items)

        {
           DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name,prop.PropertyType);
             }
             foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
           //put a breakpoint here and check datatable
           return dataTable;
        }
    
    }
}
