using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
   public class ExtraPriceDAO:GatewayConnection
    {

       public ExtraPriceModel GetExtraPrice()
       {

           DataSet DS = new DataSet();
           DataTable DT = new DataTable();
           ExtraPriceModel aExtraPriceModel = new ExtraPriceModel();
          
               Query = String.Format("SELECT * FROM rcs_extra_price;");

               command = CommandMethod(command);
               Reader = ReaderMethod(Reader, command);

                     
                       while (Reader.Read()) // Read() returns true if there is still a result line to read
                       {
                           aExtraPriceModel.Price_1 = Convert.ToDouble(Reader["price_1"]);
                           aExtraPriceModel.Price_2 = Convert.ToDouble(Reader["price_2"]);
                           aExtraPriceModel.Price_3 = Convert.ToDouble(Reader["price_3"]);
                           aExtraPriceModel.Price_4 = Convert.ToDouble(Reader["price_4"]);
                           aExtraPriceModel.Price_5 = Convert.ToDouble(Reader["price_5"]);
                           aExtraPriceModel.Price_6 = Convert.ToDouble(Reader["price_6"]);


                       }
                  
        

           return aExtraPriceModel;
       }

    

    }
}
