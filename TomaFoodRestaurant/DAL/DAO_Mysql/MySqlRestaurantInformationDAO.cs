using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
   public class MySqlRestaurantInformationDAO:MySqlGatewayConnection
    {
        public RestaurantInformation GetRestaurantInformation()
        {
            Query = String.Format("SELECT * FROM rcs_restaurant;");
            RestaurantInformation aRestaurantInformation = new RestaurantInformation();

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                aRestaurantInformation = ReadRestaurantInformation(Reader);
            }

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return aRestaurantInformation;

        }

        internal StripeDetails GetStripeDetails()
        {
            StripeDetails stripeDetails = new StripeDetails();   
            Query = String.Format("SELECT `key`,`value` FROM rcs_mod_setting  where `group`='stripe_payment';");
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read())
            {
               
                try
                {
                    if (Convert.ToString(Reader["key"]) == "PUBLISHABLEAPIKEY")
                    {
                        stripeDetails.publishKey = Convert.ToString(Reader["value"]);
                    }
                    
                }
                catch (Exception exception)
                {
                    stripeDetails.publishKey = "";
                }


                try
                {
                    if (Convert.ToString(Reader["key"]) == "APIKEY")
                    {
                        stripeDetails.apiKey = Convert.ToString(Reader["value"]);
                    }

                }
                catch (Exception exception)
                {
                    stripeDetails.apiKey = "";
                }

                try
                {
                    if (Convert.ToString(Reader["key"]) == "ACCOUNTID")
                    {
                        stripeDetails.accNumber = Convert.ToString(Reader["value"]);
                    }
                }
                catch (Exception exception)
                {
                    stripeDetails.accNumber = "";
                }
                try
                {
                    if (Convert.ToString(Reader["key"]) == "APPLICATIONFEES")
                    {
                        stripeDetails.accFee = Convert.ToDouble(Reader["value"]);
                    }

                }
                catch (Exception exception)
                {
                    stripeDetails.accFee = 0;
                }
                try
                {
                    if (Convert.ToString(Reader["key"]) == "FEETYPE")
                    {
                        stripeDetails.feeType = Convert.ToString(Reader["value"]);
                    }

                }
                catch (Exception exception)
                {
                    stripeDetails.feeType = "flat";
                }
            }
            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return stripeDetails;
        }

        private RestaurantInformation ReadRestaurantInformation(IDataReader oReader)
        {
            RestaurantInformation arcs_restaurant = new RestaurantInformation();
            if (oReader["id"] != DBNull.Value)
            {
                arcs_restaurant.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["restaurant_category_id"] != DBNull.Value)
            {
                arcs_restaurant.RestaurantCategoryId = Convert.ToInt32(oReader["restaurant_category_id"]);
            }
            if (oReader["theme_name"] != DBNull.Value)
            {
                arcs_restaurant.ThankYouMsg = Convert.ToString(oReader["theme_name"]);
            }
            if (oReader["owner"] != DBNull.Value)
            {
                arcs_restaurant.Owner = Convert.ToInt32(oReader["owner"]);
            }
            if (oReader["restaurant_type"] != DBNull.Value)
            {
                arcs_restaurant.RestaurantType = Convert.ToString(oReader["restaurant_type"]);
            }
            if (oReader["restaurant_name"] != DBNull.Value)
            {
                arcs_restaurant.RestaurantName = Convert.ToString(oReader["restaurant_name"]);
            }
            if (oReader["restaurant_summary"] != DBNull.Value)
            {
                arcs_restaurant.RestaurantSummary = Convert.ToString(oReader["restaurant_summary"]);
            }
            if (oReader["house"] != DBNull.Value)
            {
                arcs_restaurant.House = Convert.ToString(oReader["house"]);
            }
            if (oReader["address"] != DBNull.Value)
            {
                arcs_restaurant.Address = Convert.ToString(oReader["address"]);
            }
            if (oReader["city"] != DBNull.Value)
            {
                arcs_restaurant.City = Convert.ToString(oReader["city"]);
            }
            if (oReader["county"] != DBNull.Value)
            {
                arcs_restaurant.Country = Convert.ToString(oReader["county"]);
            }
            if (oReader["postcode"] != DBNull.Value)
            {
                arcs_restaurant.Postcode = Convert.ToString(oReader["postcode"]);
            }
            if (oReader["phone"] != DBNull.Value)
            {
                arcs_restaurant.Phone = Convert.ToString(oReader["phone"]);
            }
            if (oReader["fax"] != DBNull.Value)
            {
                arcs_restaurant.Fax = Convert.ToString(oReader["fax"]);
            }
            if (oReader["email"] != DBNull.Value)
            {
                arcs_restaurant.Email = Convert.ToString(oReader["email"]);
            }
            if (oReader["website"] != DBNull.Value)
            {
                arcs_restaurant.Website = Convert.ToString(oReader["website"]);
            }
            if (oReader["status"] != DBNull.Value)
            {
                arcs_restaurant.Status = Convert.ToString(oReader["status"]);
            }
            if (oReader["vat"] != DBNull.Value)
            {
                arcs_restaurant.Vat = Convert.ToDouble(oReader["vat"]);
            }
            if (oReader["menu_max_row"] != DBNull.Value)
            {
                arcs_restaurant.MenuMaxRow = Convert.ToInt32(oReader["menu_max_row"]);
            }
            if (oReader["expire"] != DBNull.Value)
            {
                arcs_restaurant.Expire = Convert.ToInt64(oReader["expire"]);
            }
            if (oReader["package_max_row"] != DBNull.Value)
            {
                arcs_restaurant.PackageMaxRow = Convert.ToInt32(oReader["package_max_row"]);
            }
            if (oReader["vat_reg_no"] != DBNull.Value)
            {
                arcs_restaurant.VatRegNo = Convert.ToString(oReader["vat_reg_no"]);
            }
            if (oReader["logo"] != DBNull.Value)
            {
                arcs_restaurant.Logo = Convert.ToString(oReader["logo"]);
            }
            if (oReader["menu_drag"] != DBNull.Value)
            {
                arcs_restaurant.MenuDrag = Convert.ToInt64(oReader["menu_drag"]);
            }
            if (oReader["min_order"] != DBNull.Value)
            {
                arcs_restaurant.MinOrder = Convert.ToDouble(oReader["min_order"]);
            }
            if (oReader["delivery_from"] != DBNull.Value)
            {
                arcs_restaurant.DeliveryFrom = Convert.ToString(oReader["delivery_from"]);
            }
            if (oReader["delivery_to"] != DBNull.Value)
            {
                arcs_restaurant.DeliveryTo = Convert.ToString(oReader["delivery_to"]);
            }
            if (oReader["collection_from"] != DBNull.Value)
            {
                arcs_restaurant.CollectionFrom = Convert.ToString(oReader["collection_from"]);
            }
            if (oReader["collection_to"] != DBNull.Value)
            {
                arcs_restaurant.CollectionTo = Convert.ToString(oReader["collection_to"]);
            }
            if (oReader["delivery_time"] != DBNull.Value)
            {
                try
                {
                    arcs_restaurant.DeliveryTime = Convert.ToString(oReader["delivery_time"]);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            if (oReader["is_halal"] != DBNull.Value)
            {
                arcs_restaurant.IsHalal = Convert.ToInt64(oReader["is_halal"]);
            }
            if (oReader["report_closing_hour"] != DBNull.Value)
            {
                arcs_restaurant.ReportClosingHour = Convert.ToInt32(oReader["report_closing_hour"]);
            }
            if (oReader["report_closing_min"] != DBNull.Value)
            {
                arcs_restaurant.ReportClosingMin = Convert.ToInt32(oReader["report_closing_min"]);
            }
            if (oReader["url"] != DBNull.Value)
            {
                arcs_restaurant.Url = Convert.ToString(oReader["url"]);
            }
            if (oReader["thank_you_msg"] != DBNull.Value)
            {
                arcs_restaurant.ThankYouMsg = Convert.ToString(oReader["thank_you_msg"]);
            }
            if (oReader["discount_type"] != DBNull.Value)
            {
                arcs_restaurant.DiscountType = Convert.ToString(oReader["discount_type"]);
            }
            if (oReader["delivery_charge"] != DBNull.Value)
            {
                arcs_restaurant.DeliveryCharge = Convert.ToDouble(oReader["delivery_charge"]);
            }
            if (oReader["description"] != DBNull.Value)
            {
                arcs_restaurant.DescriptionText = Convert.ToString(oReader["description"]);
            }
            if (oReader["card_fee"] != DBNull.Value)
            {
                arcs_restaurant.CardFee = Convert.ToDouble(oReader["card_fee"]);
            }
            if (oReader["card_min_order"] != DBNull.Value)
            {
                arcs_restaurant.CardMinOrder = Convert.ToDouble(oReader["card_min_order"]);
            }
            if (oReader["is_busy"] != DBNull.Value)
            {
                arcs_restaurant.IsBusy = Convert.ToInt32(oReader["is_busy"]);
            }
            if (oReader["payment_option"] != DBNull.Value)
            {
                arcs_restaurant.PaymentOption = Convert.ToString(oReader["payment_option"]);
            }
            if (oReader["min_order_delivery"] != DBNull.Value)
            {
                arcs_restaurant.MinOrderDelivery = Convert.ToDouble(oReader["min_order_delivery"]);
            }
            if (oReader["service_option"] != DBNull.Value)
            {
                arcs_restaurant.ServiceOption = Convert.ToString(oReader["service_option"]);
            }
            if (oReader["default_order_type"] != DBNull.Value)
            {
                arcs_restaurant.DefaultOrderType = Convert.ToString(oReader["default_order_type"]);
            }
            if (oReader["in_logo"] != DBNull.Value)
            {
                arcs_restaurant.InLogo = Convert.ToString(oReader["in_logo"]);
            }
            if (oReader["reciept_option"] != DBNull.Value)
            {
                arcs_restaurant.RecieptOption = Convert.ToString(oReader["reciept_option"]);
            }
            if (oReader["menu_separation"] != DBNull.Value)
            {
                arcs_restaurant.MenuSeparation = Convert.ToInt32(oReader["menu_separation"]);
            }
            if (oReader["collection_time"] != DBNull.Value)
            {
                arcs_restaurant.CollectionTime = Convert.ToString(oReader["collection_time"]);
            }
            if (oReader["server_call_button"] != DBNull.Value)
            {
                arcs_restaurant.ServerCallButton = Convert.ToInt64(oReader["server_call_button"]);
            }
            if (oReader["pre_order"] != DBNull.Value)
            {
                arcs_restaurant.PreOrder = Convert.ToInt64(oReader["pre_order"]);
            }
            if (oReader["show_option_inline"] != DBNull.Value)
            {
                arcs_restaurant.ShowOptionInline = Convert.ToInt64(oReader["show_option_inline"]);
            }
            if (oReader["exclude_discount"] != DBNull.Value)
            {
                arcs_restaurant.ExcludeDiscount = Convert.ToString(oReader["exclude_discount"]);
            }
            if (oReader["reciept_font"] != DBNull.Value)
            {
                arcs_restaurant.RecieptFont = Convert.ToString(oReader["reciept_font"]);
            }
            if (oReader["confirm_payment"] != DBNull.Value)
            {
                arcs_restaurant.ConfirmPayment = Convert.ToInt64(oReader["confirm_payment"]);
            }
            if (oReader["update_required"] != DBNull.Value)
            {
                arcs_restaurant.UpdateRequired = Convert.ToInt32(oReader["update_required"]);
            }
            if (oReader["current_version"] != DBNull.Value)
            {
                arcs_restaurant.CurrentVersion = Convert.ToString(oReader["current_version"]);
            }
            if (oReader["show_order_number"] != DBNull.Value)
            {
                arcs_restaurant.ShowOrderNumber = Convert.ToInt64(oReader["show_order_number"]);
            }
            if (oReader["default_order_status"] != DBNull.Value)
            {
                arcs_restaurant.DefaultOrderStatus = Convert.ToString(oReader["default_order_status"]);
            }
            if (oReader["print_copy"] != DBNull.Value)
            {
                arcs_restaurant.PrintCopy = Convert.ToInt32(oReader["print_copy"]);
            }
            if (oReader["use_java"] != DBNull.Value)
            {
                arcs_restaurant.UseJava = Convert.ToInt64(oReader["use_java"]);
            }
            if (oReader["local_ip"] != DBNull.Value)
            {
                arcs_restaurant.LocalIp = Convert.ToString(oReader["local_ip"]);
            }
            if (oReader["reciept_min_height"] != DBNull.Value)
            {
                arcs_restaurant.RecieptMinHeight = Convert.ToInt32(oReader["reciept_min_height"]);
            }
            //del_print_copy
            if (oReader["del_print_copy"] != DBNull.Value)
            {
                arcs_restaurant.DelPrintCopy = Convert.ToInt32(oReader["del_print_copy"]);
            }
            if (oReader["in_print_copy"] != DBNull.Value)
            {
                arcs_restaurant.DineInPrintCopy = Convert.ToInt32(oReader["in_print_copy"]);
            }
            if (oReader["multiple_part"] != DBNull.Value)
            {
                arcs_restaurant.MultiplePart = Convert.ToInt32(oReader["multiple_part"]);
            }
            //   require_served
            //try
            //{

            //    if (oReader["sync_type"] != DBNull.Value)
            //    {
            //        arcs_restaurant.SyncType = Convert.ToString(oReader["sync_type"]);  
            //    }
            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}

            try
            {
                if (oReader["require_served"] != DBNull.Value)
                {
                    arcs_restaurant.RequireServed = Convert.ToInt32(oReader["require_served"]);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            //try
            //{
            //    if (oReader["is_service_charge"] != DBNull.Value)
            //    {
            //        arcs_restaurant.IsServiceCharge = Convert.ToInt32(oReader["is_service_charge"]);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}
            try
            {
                if (oReader["Is_sync_order"] != DBNull.Value)
                {
                    arcs_restaurant.IsSyncOrder = Convert.ToInt32(oReader["Is_sync_order"]);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            try
            {
                if (oReader["Is_sync_customer"] != DBNull.Value)
                {
                    arcs_restaurant.IsSyncCustomer = Convert.ToInt32(oReader["Is_sync_customer"]);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return arcs_restaurant;
        }

        internal bool updateRestaurantInformation(RestaurantInformation restaurantInformation)
        {

            int lastId = 0;
            try
            {

                Query = String.Format("UPDATE rcs_restaurant SET  del_print_copy=@del_print_copy,in_print_copy=@in_print_copy,print_copy=@print_copy,pre_order=@pre_order,is_busy=@is_busy,delivery_time=@delivery_time, collection_time=@collection_time, service_option=@service_option");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@delivery_time", restaurantInformation.DeliveryTime);
                    command.Parameters.AddWithValue("@collection_time", restaurantInformation.CollectionTime);
                    command.Parameters.AddWithValue("@service_option", restaurantInformation.ServiceOption);
                    command.Parameters.AddWithValue("@is_busy", restaurantInformation.IsBusy);
                    command.Parameters.AddWithValue("@pre_order", restaurantInformation.PreOrder);
                    command.Parameters.AddWithValue("@delivery_charge", restaurantInformation.DeliveryCharge);
                    command.Parameters.AddWithValue("@show_order_number", restaurantInformation.ShowOrderNumber);
                    command.Parameters.AddWithValue("@del_print_copy", restaurantInformation.DelPrintCopy);
                    command.Parameters.AddWithValue("@in_print_copy", restaurantInformation.DineInPrintCopy);
                    command.Parameters.AddWithValue("@print_copy", restaurantInformation.PrintCopy);

                    lastId = command.ExecuteNonQuery();

                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());

            }

            return lastId > 0;

        }

        internal bool UpdateRestaurantLicense(RestaurantSync aRestaurantSync)
        {

            int lastId = 0;
            try
            {

                Query = String.Format("UPDATE rcs_restaurant SET expire=@expire, update_required=@update_required");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@expire", aRestaurantSync.expire);
                    command.Parameters.AddWithValue("@update_required", aRestaurantSync.update_required);

                    lastId = command.ExecuteNonQuery();

                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());

            }

            return lastId > 0;

        }



       internal DataTable getKitchenData()
       {
           RestaurantOrder aRestaurantTable = new RestaurantOrder();
           //  SQLiteDataAdapter DB;
           DataSet DS = new DataSet();
           DataTable DT = new DataTable();
           Query = String.Format("SELECT * FROM rcs_restaurant_kitchen;");

           command = CommandMethod(command);
           Reader = ReaderMethod(Reader, command);
           DT.Load(Reader);

           CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
           return DT;
       }



         internal DataTable loadKitchenItems(int kichenId, string type)
        {
           RestaurantOrder aRestaurantTable = new RestaurantOrder();
           DataSet DS = new DataSet();
           DataTable DT = new DataTable();
           if (type == "pending")
           {
               Query = String.Format("SELECT `rcs_order_item`.`id`, `rcs_order_item`.`order_id`, `rcs_order_item`.`options`,`rcs_order_item`.`options_minus`, `rcs_order_item`.`recipe_id`, `rcs_order_item`.`package_id`, `rcs_order_item`.`name`, `rcs_order_item`.`quantity`, `rcs_order_item`.`sent_to_kitchen`, `rcs_order_item`.`kitchen_processing`, `rcs_order_item`.`kitchen_done`, `rcs_order_item`.`order_package_id`,`rcs_recipe`.`kitchen_section`, `rcs_order`.`order_time`, `rcs_order`.`delivery_time`, `rcs_order`.`cart_information`, `rcs_order`.`order_type`, `rcs_order_item`.`name` as recipe_name, `rcs_package`.`name` as package_name, `rcs_restaurant_table`.`name` as table_name, `rcs_order`.`person` as no_person FROM (`rcs_order_item`) JOIN `rcs_order` ON `rcs_order`.`id`=`rcs_order_item`.`order_id` JOIN `rcs_recipe` ON `rcs_recipe`.`id`=`rcs_order_item`.`recipe_id` LEFT JOIN `rcs_restaurant_table` ON `rcs_restaurant_table`.`id`=`rcs_order`.`order_table` LEFT JOIN `rcs_package` ON `rcs_package`.`id`=`rcs_order_item`.`package_id` WHERE `rcs_recipe`.`kitchen_section` = '" + kichenId + "' AND `rcs_order`.`order_status` != 'finished' AND `rcs_order_item`.`quantity` > 0 AND `rcs_order_item`.`quantity` >  `rcs_order_item`.`kitchen_processing` ORDER BY `rcs_order`.`delivery_time` DESC, `rcs_order`.`order_time` DESC,`rcs_order_item`.`last_modify_time` DESC ");
            }
           else if (type == "processing")
           {
               Query = String.Format("SELECT `rcs_order_item`.`id`, `rcs_order_item`.`order_id`, `rcs_order_item`.`options`,`rcs_order_item`.`options_minus`, `rcs_order_item`.`recipe_id`, `rcs_order_item`.`package_id`, `rcs_order_item`.`name`, `rcs_order_item`.`quantity`, `rcs_order_item`.`sent_to_kitchen`, `rcs_order_item`.`kitchen_processing`, `rcs_order_item`.`kitchen_done`, `rcs_order_item`.`order_package_id`,`rcs_recipe`.`kitchen_section`, `rcs_order`.`order_time`, `rcs_order`.`delivery_time`, `rcs_order`.`cart_information`, `rcs_order`.`order_type`, `rcs_order_item`.`name` as recipe_name, `rcs_package`.`name` as package_name, `rcs_restaurant_table`.`name` as table_name, `rcs_order`.`person` as no_person FROM (`rcs_order_item`) JOIN `rcs_order` ON `rcs_order`.`id`=`rcs_order_item`.`order_id` JOIN `rcs_recipe` ON `rcs_recipe`.`id`=`rcs_order_item`.`recipe_id` LEFT JOIN `rcs_restaurant_table` ON `rcs_restaurant_table`.`id`=`rcs_order`.`order_table` LEFT JOIN `rcs_package` ON `rcs_package`.`id`=`rcs_order_item`.`package_id` WHERE `rcs_recipe`.`kitchen_section` = '" + kichenId + "' AND `rcs_order`.`order_status` != 'finished' AND `rcs_order_item`.`kitchen_processing` > 0 AND `rcs_order_item`.`kitchen_processing` >  `rcs_order_item`.`kitchen_done` ORDER BY `rcs_order`.`delivery_time` DESC, `rcs_order`.`order_time` DESC,`rcs_order_item`.`last_modify_time` DESC ");
            }
           else
           {
               Query = String.Format("SELECT `rcs_order_item`.`id`, `rcs_order_item`.`order_id`, `rcs_order_item`.`options`,`rcs_order_item`.`options_minus`, `rcs_order_item`.`recipe_id`, `rcs_order_item`.`package_id`, `rcs_order_item`.`name`, `rcs_order_item`.`quantity`, `rcs_order_item`.`sent_to_kitchen`, `rcs_order_item`.`kitchen_processing`, `rcs_order_item`.`kitchen_done`, `rcs_order_item`.`order_package_id`,`rcs_recipe`.`kitchen_section`, `rcs_order`.`order_time`, `rcs_order`.`delivery_time`, `rcs_order`.`cart_information`, `rcs_order`.`order_type`, `rcs_order_item`.`name` as recipe_name, `rcs_package`.`name` as package_name, `rcs_restaurant_table`.`name` as table_name, `rcs_order`.`person` as no_person FROM (`rcs_order_item`) JOIN `rcs_order` ON `rcs_order`.`id`=`rcs_order_item`.`order_id` JOIN `rcs_recipe` ON `rcs_recipe`.`id`=`rcs_order_item`.`recipe_id` LEFT JOIN `rcs_restaurant_table` ON `rcs_restaurant_table`.`id`=`rcs_order`.`order_table` LEFT JOIN `rcs_package` ON `rcs_package`.`id`=`rcs_order_item`.`package_id` WHERE `rcs_recipe`.`kitchen_section` = '" + kichenId + "' AND `rcs_order`.`order_status` != 'finished' AND `rcs_order_item`.`kitchen_done` > 0 AND `rcs_order_item`.`quantity` = `rcs_order_item`.`kitchen_done` ORDER BY `rcs_order`.`delivery_time` DESC, `rcs_order`.`order_time` DESC,`rcs_order_item`.`last_modify_time` DESC  LIMIT 10");
           }
          
           command = CommandMethod(command);
           Reader = ReaderMethod(Reader, command);
           DT.Load(Reader);

           CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
           return DT;
         
         }

        internal bool checkdistanceServiceStatus()
        { 
             
                bool status = false;
            try
            {
                Query = String.Format("SELECT * FROM rcs_restaurant_services where service_id=1 &&   restaurant_id={0};", GlobalSetting.RestaurantInformation.Id);
                command = CommandMethod(command);
                Reader = ReaderMethod(Reader, command);
                while (Reader.Read())
                {
                    status = true;
                }
                bool readConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception ex)
            {
                return false;
            }
            return status;
        }

            internal bool UpdateKichen(int id, string currentStatus, int qty)
         {


             int lastId = 0;
             try
             {
                 if (currentStatus == "pending")
                 {
                     Query = String.Format("UPDATE `rcs_order_item` SET `kitchen_processing` = kitchen_processing+" + qty + "  WHERE `id` =  '" + id + "'");
                
                 }
                 else {
                     Query = String.Format("UPDATE `rcs_order_item` SET `kitchen_done` = kitchen_done+" + qty + "  WHERE `id` =  '" + id + "'");

                 }
                 try
                 {
                     command = CommandMethod(command);  
                     lastId = command.ExecuteNonQuery();

                     bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                 }
                 catch (Exception exception)
                 {
                     ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                     aErrorReportBll.SendErrorReport(exception.ToString());
                 }

             }
             catch (Exception ex)
             {
                 ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                 aErrorReportBll.SendErrorReport(ex.ToString());

             }

             return lastId > 0;

         }


    }
}
