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
    public class EmailModuleDAO : GatewayConnection
    {

        public EmailModule GetEmailModule()
        {

            EmailModule emailModule = new EmailModule();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT em.*, re.restaurant_id, re.status as isActive FROM rcs_restaurant_email re" +
                " JOIN rcs_email_moduels em ON em.id=re.email_module_id where re.status='{0}' AND em.status='{0}'; LIMIT 1", "active");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);


            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                emailModule = ReaderToReadEmailModule(Reader);

            }


            return emailModule;
        }

        private EmailModule ReaderToReadEmailModule(IDataReader oReader)
        {
            EmailModule emailModule = new EmailModule();

            if (oReader["slug"] != DBNull.Value)
            {
                emailModule.Slug = Convert.ToString(oReader["slug"]);
            }
            if (oReader["name"] != DBNull.Value)
            {
                emailModule.Name = Convert.ToString(oReader["name"]);
            }
            if (oReader["api_key"] != DBNull.Value)
            {
                emailModule.ApiKey = Convert.ToString(oReader["api_key"]);
            }
            if (oReader["api_secret"] != DBNull.Value)
            {
                emailModule.ApiSecret = Convert.ToString(oReader["api_secret"]);
            }
            if (oReader["api_url"] != DBNull.Value)
            {
                emailModule.ApiUrl = Convert.ToString(oReader["api_url"]);
            }
            if (oReader["isActive"] != DBNull.Value)
            {
                emailModule.Status = Convert.ToString(oReader["isActive"]);
            }

            if (oReader["id"] != DBNull.Value)
            {
                emailModule.Id = Convert.ToInt32(oReader["id"]);
            }

            if (oReader["restaurant_id"] != DBNull.Value)
            {
                emailModule.RestaurantId = Convert.ToInt32(oReader["restaurant_id"]);
            }

            return emailModule;
        }
    }
}
