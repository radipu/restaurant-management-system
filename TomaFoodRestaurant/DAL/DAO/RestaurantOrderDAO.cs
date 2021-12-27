using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class RestaurantOrderDAO : GatewayConnection
    {
        RestaurantOrderReader _restaurantOrderReader = new RestaurantOrderReader();
        #region  Order, Order Item, Order Package, Online Order (Insert, Update, Delete)


        string MainConnectionString = GlobalSetting.serverConnectionString;
        //  string MainConnectionString = GlobalSetting.ConnectionString;

        internal int InsertRestaurantOrder(RestaurantOrder aRestaurantOrder)
        {

            long lastId = 0;
            
            //if (aRestaurantOrder.CardAmount > 0 || aRestaurantOrder.OnlineOrder > 0)
            //{
            //    aRestaurantOrder.Receipt = 1;
            //}
            //else
            //{
            //    aRestaurantOrder.Receipt = 0;

            //}

            //Query = String.Format("INSERT INTO rcs_order ([user_id],[customer_id] ,[restaurant_id],[order_time],[delivery_time],[payment_date],[status]," +
            //                       " [payment_method],[payment_module] ,[delivery_address] ,[comments] ,[delivery_cost] ,[vat],[total_cost],[person],[order_table] ,[discount]," +
            //                       "[order_type],[order_status],[receipt],[comment],[cart_information],[customer_name],[coupon],[online_order],[online_order_status],[customer_email]," +
            //                       "[card_fee],[online_order_id],[cash_amount],[card_amount],[driver_id],[order_no],[special_offer_item],[update_time],[served_by], [is_sync],[service_charge])" +

            //                       "VALUES(@user_id,@customer_id,@restaurant_id,@order_time,@delivery_time,@payment_date,@status,@payment_method,@payment_module," +
            //               "@delivery_address,@comments,@delivery_cost,@vat,@total_cost,@person,@order_table,@order_type,@order_status,@receipt,@comment,@cart_information,@customer_name,@coupon,@online_order,@online_order_status," +
            //                              "@special_offer_item,@customer_email,@card_fee,@online_order_id,@cash_amount,@card_amount,@driver_id,@order_no,@special_offer_item,@update_time,@served_by,@is_sync,@service_charge)");

            Query = String.Format("INSERT INTO rcs_order " +
                      "(user_id,customer_id ,restaurant_id,order_time,delivery_time,payment_date,status," +
                        " payment_method,payment_module ,delivery_address ,comments ,delivery_cost ,vat,total_cost,person," +
                          "order_table ,discount,order_type,order_status,receipt,comment,cart_information,customer_name," +
                          "coupon,online_order,online_order_status,special_offer_item," + "customer_email,card_fee,online_order_id," +
                          "cash_amount,card_amount,driver_id,order_no,update_time,served_by,is_sync,service_charge)" +

                          " VALUES(@user_id,@customer_id,@restaurant_id,@order_time,@delivery_time,@payment_date," +
                             "@status,@payment_method,@payment_module,@delivery_address,@comments,@delivery_cost," +
                             "@vat,@total_cost,@person,@order_table,@discount,@order_type,@order_status,@receipt,@comment," +
                             "@cart_information,@customer_name,@coupon,@online_order,@online_order_status," +
                             "@special_offer_item,@customer_email,@card_fee,@online_order_id,@cash_amount," +
                             "@card_amount,@driver_id,@order_no,@update_time,@served_by,@is_sync,@service_charge)");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@user_id", aRestaurantOrder.UserId);
                command.Parameters.AddWithValue("@customer_id", aRestaurantOrder.CustomerId);
                command.Parameters.AddWithValue("@restaurant_id", aRestaurantOrder.RestaurantId);
                command.Parameters.AddWithValue("@order_time", aRestaurantOrder.OrderTime.ToString(TimeFormatCustom.Format));
                command.Parameters.AddWithValue("@delivery_time", aRestaurantOrder.DeliveryTime.ToString(TimeFormatCustom.Format));
                command.Parameters.AddWithValue("@payment_date", DateTime.Now.ToString(TimeFormatCustom.Format));
                command.Parameters.AddWithValue("@status", aRestaurantOrder.Status);
                command.Parameters.AddWithValue("@payment_method", aRestaurantOrder.PaymentMethod ?? "");
                command.Parameters.AddWithValue("@payment_module", aRestaurantOrder.PaymentModule ?? "");
                //command.Parameters.AddWithValue("@d9", aRestaurantOrder.PaymentModule);
                command.Parameters.AddWithValue("@delivery_address", aRestaurantOrder.DeliveryAddress ?? "");
                command.Parameters.AddWithValue("@comments", aRestaurantOrder.Comments ?? "");
                command.Parameters.AddWithValue("@delivery_cost", aRestaurantOrder.DeliveryCost);
                command.Parameters.AddWithValue("@vat", aRestaurantOrder.Vat);
                command.Parameters.AddWithValue("@total_cost", aRestaurantOrder.TotalCost);
                command.Parameters.AddWithValue("@person", aRestaurantOrder.Person);
                command.Parameters.AddWithValue("@order_table", aRestaurantOrder.OrderTable);
                command.Parameters.AddWithValue("@discount", aRestaurantOrder.Discount);
                command.Parameters.AddWithValue("@order_type", aRestaurantOrder.OrderType ?? "");
                command.Parameters.AddWithValue("@order_status", aRestaurantOrder.OrderStatus ?? "");
                command.Parameters.AddWithValue("@receipt", 1);
                command.Parameters.AddWithValue("@comment", aRestaurantOrder.Comment ?? "");
                command.Parameters.AddWithValue("@cart_information", aRestaurantOrder.CartInformation ?? "");
                command.Parameters.AddWithValue("@customer_name", aRestaurantOrder.CustomerName ?? "");
                command.Parameters.AddWithValue("@coupon", aRestaurantOrder.Coupon ?? "");
                command.Parameters.AddWithValue("@online_order", aRestaurantOrder.OnlineOrder);
                command.Parameters.AddWithValue("@online_order_status", aRestaurantOrder.OnlineOrderStatus ?? "");
                command.Parameters.AddWithValue("@customer_email", aRestaurantOrder.CustomerEmail ?? "");
                command.Parameters.AddWithValue("@card_fee", aRestaurantOrder.CardFee);
                command.Parameters.AddWithValue("@online_order_id", aRestaurantOrder.OnlineOrderId);
                command.Parameters.AddWithValue("@cash_amount", aRestaurantOrder.CashAmount);
                command.Parameters.AddWithValue("@card_amount", aRestaurantOrder.CardAmount);
                command.Parameters.AddWithValue("@driver_id", aRestaurantOrder.DriverId);
                command.Parameters.AddWithValue("@order_no", aRestaurantOrder.OrderNo);
                command.Parameters.AddWithValue("@special_offer_item", aRestaurantOrder.SpecialOfferItem ?? "");
                command.Parameters.AddWithValue("@update_time", aRestaurantOrder.UpdateTime.ToString(TimeFormatCustom.Format));
                command.Parameters.AddWithValue("@served_by", aRestaurantOrder.ServedBy);
                command.Parameters.AddWithValue("@is_sync", aRestaurantOrder.IsSync);
                command.Parameters.AddWithValue("@service_charge", aRestaurantOrder.ServiceCharge);

                int id = command.ExecuteNonQuery();
                Query = String.Format("select max(id) from rcs_order");
                command = CommandMethod(command);
                lastId = (long)command.ExecuteScalar();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            //  mytransaction.Commit();
            return (int)lastId;
        }

        internal int InsertRestaurantOrderItem(List<OrderItem> aOrderItems)
        {
            long lastId = 0;


            foreach (OrderItem item in aOrderItems)
            {

                Query =
                    String.Format(
                        "INSERT INTO rcs_order_item (order_id,recipe_id,package_id,name,quantity,price,extra_price,sent_to_kitchen" +
                        ",kitchen_processing,kitchen_done,last_modify_time,options,options_minus,multiple_menu,order_package_id)"

                        +
                        "VALUES(@OrderId,@RecipeId,@PackageId,@Name,@Quantity,@Price ,@ExtraPrice,@SentToKitchen," +
                        "@KitchenProcessing,@KitchenDone,@LastModifyTime,@Options,@MinusOptions,@multiplemenu,@orderPackageId)");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@OrderId", item.OrderId);
                    command.Parameters.AddWithValue("@RecipeId", item.RecipeId);
                    command.Parameters.AddWithValue("@PackageId", item.PackageId);command.Parameters.AddWithValue("@Name", item.Name ?? "");
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.Parameters.AddWithValue("@ExtraPrice", item.ExtraPrice);
                    command.Parameters.AddWithValue("@SentToKitchen", item.SentToKitchen);
                    command.Parameters.AddWithValue("@KitchenProcessing", item.KitchenProcessing);
                    command.Parameters.AddWithValue("@KitchenDone", item.KitchenDone);
                    command.Parameters.AddWithValue("@LastModifyTime", item.LastModifyTime);
                    command.Parameters.AddWithValue("@Options", item.Options ?? "");
                    command.Parameters.AddWithValue("@MinusOptions", item.MinusOptions ?? "");
                    command.Parameters.AddWithValue("@orderPackageId", item.orderPackageId);
                    command.Parameters.AddWithValue("@multiplemenu", item.MultipleMenu ?? "");
                    

                    lastId = command.ExecuteNonQuery();
                    }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

            }




            return (int)lastId;
        }

        internal List<KeyValuePair<int, int>> InsertOrderPackage(List<OrderPackage> aOrderPackage)
        {
            long lastId = 0;
            List<KeyValuePair<int, int>> packageIds = new List<KeyValuePair<int, int>>();


            foreach (OrderPackage package in aOrderPackage)
            {

                Query =
                    String.Format(
                        "INSERT INTO rcs_order_package([order_id],[package_id],[name],[quantity],[price],[extra_price])" +
                        " VALUES (@order_id ,@package_id ,@name,@quantity,@price,@extra_price)");
                //package.OrderId, package.PackageId, package.Name, package.Quantity, package.Price, package.Extra_price);

                try
                {
                    command = CommandMethod(command);

                    command.Parameters.AddWithValue("@order_id", package.OrderId);

                    command.Parameters.AddWithValue("@package_id", package.PackageId);

                    command.Parameters.AddWithValue("@name", package.Name);

                    command.Parameters.AddWithValue("@quantity", package.Quantity);

                    command.Parameters.AddWithValue("@price", package.Price);

                    command.Parameters.AddWithValue("@extra_price", package.Extra_price);

                    lastId = command.ExecuteNonQuery();

                    Query = String.Format("select max(id) from rcs_order_package");
                    command = CommandMethod(command);

                    lastId = (long)command.ExecuteScalar();

                    if (package.optionIndex > 0)
                    {
                        // For Old Form version package optionIndex

                        packageIds.Add(new KeyValuePair<int, int>(package.optionIndex, (int) lastId));
                    }
                    else
                    {
                        // For Old New version package Identy using PackageId
                        packageIds.Add(new KeyValuePair<int, int>(package.PackageId, (int)lastId));
                    }
             

                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }



            }
            return packageIds;
        }


        internal List<KeyValuePair<int, int>> InsertRestaurantOrderPackage(List<OrderPackage> aOrderPackage)
        {
            long lastId = 0;
            List<KeyValuePair<int, int>> packageIds = new List<KeyValuePair<int, int>>();


            foreach (OrderPackage package in aOrderPackage)
            {

                Query =
                    String.Format(
                        "INSERT INTO rcs_order_package([order_id],[package_id],[name],[quantity],[price],[extra_price])" +
                        " VALUES (@order_id , @package_id,@name,@quantity,@price,@extra_price)");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@order_id", package.OrderId);
                    command.Parameters.AddWithValue("@package_id", package.PackageId);
                    command.Parameters.AddWithValue("@name", package.Name ?? "");
                    command.Parameters.AddWithValue("@quantity", package.Quantity);
                    command.Parameters.AddWithValue("@price", package.Price);
                    command.Parameters.AddWithValue("@extra_price", package.Extra_price);

                    lastId = command.ExecuteNonQuery();
                    packageIds.Add(new KeyValuePair<int, int>(package.Id, (int)lastId));
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }


            return packageIds;
        }

        internal RestaurantOrder GetRestaurantOrder(int tableId, string status)
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where order_table=@order_table AND status=@status");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_table", tableId);
            command.Parameters.AddWithValue("@status", status);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);



            try
            {
                if (DT.Rows.Count > 0)
                {
                    aRestaurantTable = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, 0);
                }


            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }






            return aRestaurantTable;
        }

        internal List<OrderItem> GetRestaurantOrderRecipeItems(int orderId)
        {
            List<OrderItem> aRestaurantItem = new List<OrderItem>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_item where order_id=@order_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_id", orderId);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    OrderItem aRestaurantTable = _restaurantOrderReader.ReaderToReadOrderItem(DT, rowCount);
                    aRestaurantItem.Add(aRestaurantTable);

                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
                rowCount++;}

            return aRestaurantItem;
        }

        internal List<OrderPackage> GetRestaurantOrderPackage(int orderId)
        {
            List<OrderPackage> aRestaurantItem = new List<OrderPackage>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_package where order_id=@order_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_id", orderId);
            Reader = ReaderMethod(Reader, command);
            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    OrderPackage aRestaurantTable = _restaurantOrderReader.ReaderToReadOrderPackage(Reader);
                    aRestaurantItem.Add(aRestaurantTable);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }

            return aRestaurantItem;
        }

        internal bool UpdateRestaurantOrderItem(List<OrderItem> aOrderItems, int orderId)
        {
           try
                {
                    foreach (OrderItem item in aOrderItems)
                    {
                        List<OrderItem> aItems = GetRestaurantOrderRecipeItemByItemId(orderId, item.RecipeId);
                        int itemId = CheckOrder(item, aItems);

                        Query = "";

                        if (itemId <= 0)
                        {
                            Query =
                                String.Format(
                                    "INSERT INTO rcs_order_item ([order_id],[recipe_id],[package_id],[name],[quantity],[price],[extra_price],[sent_to_kitchen]" +
                                    ",[kitchen_processing],[kitchen_done],[last_modify_time],[options],[options_minus],[order_package_id])" +
                                    "VALUES(@order_id,@recipe_id,@package_id,@name,@quantity,@price ,@extra_price,@sent_to_kitchen,@kitchen_processing,@kitchen_done,@last_modify_time,@options," +
                                    "@options_minus,@order_package_id)");
                            //     ,
                            //item.OrderId, item.RecipeId, item.PackageId, item.Name.Replace("'", " "), item.Quantity, item.Price, item.ExtraPrice, item.SentToKitchen, item.KitchenProcessing, item.KitchenDone
                            //, item.LastModifyTime, item.Options, item.MinusOptions, item.orderPackageId, item.Options_ids);
                            // }
                        }
                        else
                        {
                            Query = String.Format("update rcs_order_item set  [quantity]=@quantity,[price]=@price, [name]=@name where id=@id");

                        }
                        //using (SQLiteCommand command = new SQLiteCommand(query, c))
                        //{
                        //    c.Open();

                        try
                        {
                            command = CommandMethod(command);
                            command.Parameters.AddWithValue("@id", itemId);
                            command.Parameters.AddWithValue("@order_id", item.OrderId);
                            command.Parameters.AddWithValue("@recipe_id", item.RecipeId);
                            command.Parameters.AddWithValue("@package_id", item.PackageId);
                            command.Parameters.AddWithValue("@name", item.Name.Replace("'", " "));
                            command.Parameters.AddWithValue("@quantity", item.Quantity);
                            command.Parameters.AddWithValue("@price", item.Price);
                            command.Parameters.AddWithValue("@extra_price", item.ExtraPrice);
                            command.Parameters.AddWithValue("@sent_to_kitchen", item.SentToKitchen);
                            command.Parameters.AddWithValue("@kitchen_processing", item.KitchenProcessing);
                            command.Parameters.AddWithValue("@kitchen_done", item.KitchenDone);
                            command.Parameters.AddWithValue("@last_modify_time", item.LastModifyTime);
                            command.Parameters.AddWithValue("@options", item.Options ?? "");
                            command.Parameters.AddWithValue("@options_minus", item.MinusOptions ?? "");
                            command.Parameters.AddWithValue("@order_package_id", item.orderPackageId);



                            int lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }





                    }


                    return true;
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                    return false;
                }
 
        }

        private int CheckOrder(OrderItem item, List<OrderItem> aItems)
        {
            var it = aItems.FirstOrDefault(a => Convert.ToString(a.Options).Trim() == item.Options);
            if (it != null && it.Id > 0)
            {
                return it.Id;
            }
            return 0;
        }
        internal List<KeyValuePair<int, int>> UpdateRestaurantOrderPackage(List<OrderPackage> aOrderPackage, int orderId)
        {
            try
            {
                List<KeyValuePair<int, int>> packageIds = new List<KeyValuePair<int, int>>();


                foreach (OrderPackage package in aOrderPackage)
                {
                    // OrderPackage aPackage = GetRestaurantOrderPackageByPackageID(orderId, package.PackageId);

                    string query = "";

                    if (package.Id <= 0)
                    {
                        Query = String.Format("INSERT INTO rcs_order_package([order_id],[package_id],[name],[quantity],[price],[extra_price]) " +
                                              "VALUES (@order_id ,@package_id,@name ,@quantity,@price,@extra_price)");
                    }
                    else
                    {
                        Query = String.Format("update rcs_order_package set  [quantity]=@quantity,[price]=@price where id=@id");
                    }

                    try
                    {
                        command = CommandMethod(command);
                        command.Parameters.AddWithValue("@order_id", package.OrderId);
                        command.Parameters.AddWithValue("@package_id", package.PackageId);
                        command.Parameters.AddWithValue("@name", package.Name);
                        command.Parameters.AddWithValue("@price", package.Price);
                        command.Parameters.AddWithValue("@extra_price", package.Extra_price);
                        command.Parameters.AddWithValue("@quantity", package.Quantity);

                        command.Parameters.AddWithValue("@id", package.Id);

                        long lastId = command.ExecuteNonQuery();


                        Query = String.Format("select max(id) from rcs_order_package");
                        command = CommandMethod(command);

                        lastId = (long)command.ExecuteScalar();
                        if (package.optionIndex > 0)
                        {// For Old Form version package optionIndex
                            if (package.Id <= 0)
                            {
                                packageIds.Add(new KeyValuePair<int, int>(package.optionIndex, (int) lastId));
                            }
                            else
                            {
                                packageIds.Add(new KeyValuePair<int, int>(package.optionIndex, package.Id));
                            }
                           
                        }
                        else
                        {
                            // For Old New version package Identy using PackageId
                            packageIds.Add(new KeyValuePair<int, int>(package.PackageId, (int)lastId));
                        }}
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }


                }
                return packageIds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private OrderPackage GetRestaurantOrderPackageByPackageID(int orderId, int packageId)
        {
            OrderPackage aRestaurantItem = new OrderPackage();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_package where order_id=@order_id AND package_id=@package_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_id", orderId);
            command.Parameters.AddWithValue("@package_id", packageId);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read())
            {

                try
                {
                    aRestaurantItem = _restaurantOrderReader.ReaderToReadOrderPackage(Reader);
                    // aRestaurantItem.Add(aRestaurantTable);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }


            return aRestaurantItem;
        }

        internal List<OrderItem> GetRestaurantOrderRecipeItemByItemId(int orderId, int recipeId)
        {
            List<OrderItem> aRestaurantItems = new List<OrderItem>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_item where order_id=@order_id AND recipe_id=@package_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_id", orderId);
            command.Parameters.AddWithValue("@package_id", recipeId);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    OrderItem aRestaurantItem = _restaurantOrderReader.ReaderToReadOrderItem(DT, rowCount);
                    aRestaurantItems.Add(aRestaurantItem);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

                rowCount++;
            }


            return aRestaurantItems;
        }


        internal bool UpdateRestaurantOrderItem(int orderId)
        {
            try
            {




                Query = String.Format("update rcs_order set  [status]=@status where id=@id");


                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@id", orderId);
                    command.Parameters.AddWithValue("@status", "paid");

                    int lastId = command.ExecuteNonQuery();
                    if (lastId > 0)
                    {
                        return true;
                    }

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        internal bool UpdateRestaurantOrder(RestaurantOrder aRestaurantOrder)
        {
            try
            {

                //if (aRestaurantOrder.Comment != null)
                //{
                //    aRestaurantOrder.Comment = aRestaurantOrder.Comment.Replace("'", " "); ;
                //}
                //if (aRestaurantOrder.Comments != null)
                //{
                //    aRestaurantOrder.Comments = aRestaurantOrder.Comments.Replace("'", " "); ;
                //}



                Query = String.Format("UPDATE [rcs_order] SET " +
                              " [payment_date] =@payment_date,[status] = @status ,[payment_method] = @payment_method" +
                              ",[payment_module] = @payment_module,[comments] = @comments,[vat] = @vat," +
                               "[total_cost] =@total_cost,[person] =@person,[order_table] =@order_table," +
                               "[discount] =@discount,[order_type] = @order_type,[order_status] = @order_status ," +
                               "[receipt] =@receipt,[comment] = @comment,[cart_information] = @cart_information ," +
                               "[card_fee] = @card_fee,[card_amount] = @card_amount,[cash_amount]=@cash_amount," +
                               "[online_order_status]=@online_order_status,[is_sync]=@is_sync,[online_order_id]=@online_order_id," +
                               "[service_charge]=@service_charge WHERE id=@id");


                try{
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@payment_date", DateTime.Now.ToString(TimeFormatCustom.Format));
                    // command.Parameters.AddWithValue("@payment_date", Convert.ToDateTime(aRestaurantOrder.PaymentDate));
                    command.Parameters.AddWithValue("@status", aRestaurantOrder.Status ?? "");
                    command.Parameters.AddWithValue("@payment_method", aRestaurantOrder.PaymentMethod ?? "");
                    command.Parameters.AddWithValue("@payment_module", aRestaurantOrder.PaymentModule ?? "");
                    command.Parameters.AddWithValue("@comments", aRestaurantOrder.Comments ?? "");
                    command.Parameters.AddWithValue("@vat", aRestaurantOrder.Vat);
                    command.Parameters.AddWithValue("@total_cost", aRestaurantOrder.TotalCost);
                    command.Parameters.AddWithValue("@person", aRestaurantOrder.Person);
                    command.Parameters.AddWithValue("@order_table", aRestaurantOrder.OrderTable);
                    command.Parameters.AddWithValue("@discount", aRestaurantOrder.Discount);
                    command.Parameters.AddWithValue("@order_type", aRestaurantOrder.OrderType ?? "");
                    command.Parameters.AddWithValue("@order_status", aRestaurantOrder.OrderStatus ?? "");
                    command.Parameters.AddWithValue("@receipt", aRestaurantOrder.Receipt);
                    command.Parameters.AddWithValue("@comment", aRestaurantOrder.Comment ?? "");
                    command.Parameters.AddWithValue("@cart_information", aRestaurantOrder.CartInformation ?? "");
                    command.Parameters.AddWithValue("@card_fee", aRestaurantOrder.CardFee);
                    command.Parameters.AddWithValue("@card_amount", aRestaurantOrder.CardAmount);
                    command.Parameters.AddWithValue("@cash_amount", aRestaurantOrder.CashAmount);
                    command.Parameters.AddWithValue("@online_order_status", aRestaurantOrder.OnlineOrderStatus ?? "");
                    command.Parameters.AddWithValue("@is_sync", aRestaurantOrder.IsSync);
                    command.Parameters.AddWithValue("@service_charge", aRestaurantOrder.ServiceCharge);
                    command.Parameters.AddWithValue("@id", aRestaurantOrder.Id);
                    command.Parameters.AddWithValue("@online_order_id", aRestaurantOrder.OnlineOrderId);
                    int lastId = command.ExecuteNonQuery();

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        internal DataTable GetRestaurantOrderByDate(DateTime startDate, DateTime endDate)
        {
            DataTable DT = new DataTable();
            try
            {
                //  SQLiteDataAdapter DB;strftime('%s', date) BETWEEN strftime('%s', start_date) AND strftime('%s', end_date)
                DataSet DS = new DataSet();

                Query =
                    String.Format(
                        "SELECT rcs_order.id, rcs_order.customer_id, strftime(rcs_order.order_time) as OrderTime, strftime(rcs_order.delivery_time) as DeliveryTime, strftime(rcs_order.payment_date) as PaymentTime, rcs_order.status," +
                        "rcs_order.payment_method, rcs_order.payment_module, rcs_order.delivery_address, rcs_order.total_cost, rcs_order.order_table," +
                        "rcs_order.order_type,rcs_order.order_status,rcs_order.cash_amount, rcs_order.card_amount, rcs_order.user_id, rcs_order.restaurant_id," +
                        "rcs_order.comments, rcs_order.delivery_cost, rcs_order.vat, rcs_order.person, rcs_order.discount, rcs_order.receipt," +
                        "rcs_order.comment, rcs_order.cart_information, rcs_order.coupon, rcs_order.online_order, rcs_order.online_order_status," +
                        "rcs_order.customer_email, rcs_order.card_fee, rcs_order.online_order_id, rcs_order.driver_id, rcs_order.order_no, rcs_users.usertype," +
                        "printf('%s %s, %s, %s, %s  %s', rcs_users.firstname, rcs_users.lastname,rcs_users.mobilephone,rcs_users.homephone,rcs_users.house,rcs_users.address) as firstname, rcs_restaurant_table.name FROM rcs_order LEFT OUTER JOIN  rcs_users ON rcs_order.customer_id =" +
                        "rcs_users.id LEFT OUTER JOIN  rcs_restaurant_table ON rcs_order.order_table =rcs_restaurant_table.id  order by  rcs_order.id desc;"
                        );
                // string query = String.Format("select * from GetRestaurantOrderByDate");

                Adapter = GetAdapter(Adapter);
                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return DT;

        }

        internal RestaurantOrder GetRestaurantOrderByCustomerId(int customerId)
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT  * FROM rcs_order where customer_id=@customer_id  LIMIT 1");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@customer_id", customerId);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);

            try
            {
                if (DT.Rows.Count > 0)
                {
                    aRestaurantTable = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, 0);

                }

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }


            return aRestaurantTable;
        }

        internal RestaurantOrder GetRestaurantOrderByOrderId(int orderId)
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where id=@id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@id", orderId);

            Reader = ReaderMethod(Reader, command);
            //DataTable dataTable = new DataTable("rcs_order");
            DT.Load(Reader);


            try
            {
                if (DT.Rows.Count > 0)
                {
                    aRestaurantTable = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, 0);


                }






            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



            return aRestaurantTable;
        }

        internal bool DeleteItemsAndPackage(List<int> canceItem, List<int> cancelPackage)
        {
            int lastId = 0;

            for (int i = 0; i < canceItem.Count; i++)
            {
                Query = String.Format("delete from rcs_order_item where id=@id");



                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@id", canceItem[i]);
                    lastId = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }


            }


            for (int i = 0; i < cancelPackage.Count; i++)
            {
                Query = String.Format("delete from rcs_order_package where id=@id");

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@id", cancelPackage[i]);
                    lastId = command.ExecuteNonQuery();

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }


            return (int)lastId > 0 ? true : false;
        }
        internal bool DeleteItemsAndPackage(int orderId)
        {
            int lastId = 0;

            Query = String.Format("delete from rcs_order_item where order_id=@id");

            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", orderId);
                lastId = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }

            Query = String.Format("delete from rcs_order_package where order_id=@id");

            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", orderId);
                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



            return (int)lastId > 0 ? true : false;
        }
        internal RestaurantOrder GetRestaurantOrderByOnlineOrder(long onlineOrderId)
        {
            RestaurantOrder aRestaurantOrder = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where online_order_id=@online_order_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@online_order_id", onlineOrderId);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            // dataRow = command.ExecuteReader();
            try
            {
                if (DT.Rows.Count > 0)
                {

                    aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, 0);

                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }






            return aRestaurantOrder;
        }


        internal List<RestaurantOrder> GetRestaurantOrderForOnline(int onlineOrder, string status)
        {
            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT  A.* ,printf('%s %s, %s, %s, %s  %s', rcs_users.firstname, rcs_users.lastname,rcs_users.mobilephone,rcs_users.homephone,rcs_users.house,rcs_users.address) as cusname FROM rcs_order A left join rcs_users on " +
                                  " A.customer_id=rcs_users.id where online_order_status=@online_order_status AND online_order=@online_order");
            // Query = String.Format("select * from GetRestaurantOrderForOnline where online_order_status=@online_order_status AND online_order=@online_order");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@online_order_status", status);
            command.Parameters.AddWithValue("@online_order", onlineOrder);


            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);

            // dataRow = command.ExecuteReader();
            int rowcount = 0;
            while (DT.Rows.Count > rowcount)
            {

                try
                {
                    RestaurantOrder aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrderForOnline(DT, rowcount);
                    aRestaurantOrders.Add(aRestaurantOrder);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

                rowcount++;
            }

            return aRestaurantOrders;
        }



        internal string DeleteOrderByOrderId(int orderId)
        {
            int lastId = 0;
            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {
                c.Open();
                //  using (SQLiteTransaction mytransaction = c.BeginTransaction())
                {

                    Query = String.Format("delete from rcs_order_item where order_id=@order_id");

                    try
                    {
                        command = CommandMethod(command);
                        command.Parameters.AddWithValue("@order_id", orderId);
                        lastId = command.ExecuteNonQuery();

                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }

                    Query = String.Format("delete from rcs_order_package where order_id=@order_id");



                    try
                    {
                        command = CommandMethod(command);
                        command.Parameters.AddWithValue("@order_id", orderId);


                        lastId = command.ExecuteNonQuery();

                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }


                    Query = String.Format("delete from rcs_order where id=@id");



                    try
                    {
                        command = CommandMethod(command);
                        command.Parameters.AddWithValue("@id", orderId);

                        lastId = command.ExecuteNonQuery();

                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }




                    //  mytransaction.Commit();
                }

            }
            return (int)lastId > 0 ? "Yes" : "No";
        }
        internal string DeleteAllOrder()
        {
            int lastId = 0;

            Query = String.Format("delete from rcs_order_item");


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





            Query = String.Format("delete from rcs_order_package");



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



            Query = String.Format("delete from rcs_order");


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

            return (int)lastId > 0 ? "Yes" : "No";
        }

        internal string UpdateOnlineOrder(int orderId, string status)
        {
            int lastId = 0;
            try
            {




                Query = String.Format("UPDATE rcs_order SET online_order_status=@status  WHERE online_order_id=@orderId");


                try
                {
                    command = CommandMethod(command);

                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@orderId", orderId);
                    lastId = command.ExecuteNonQuery();

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }








            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return (int)lastId > 0 ? "Yes" : "No";
        }

        internal RestaurantOrder GetRestaurantOrderByCustomerAndDate(DateTime toDate, DateTime fromDate, int customerId)
        {

            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where order_time >=@toDate AND order_time <@FromDate AND customer_id=@Customer");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@toDate", toDate.ToString(TimeFormatCustom.Format));
            command.Parameters.AddWithValue("@FromDate", fromDate.ToString(TimeFormatCustom.Format));
            command.Parameters.AddWithValue("@id", customerId);

            Reader = ReaderMethod(Reader, command);



            try
            {
                DT.Load(Reader);
                aRestaurantTable = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, 0);

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }



            return aRestaurantTable;


        }

        public List<RestaurantOrder> GetRestaurantOrderByDateForOrderNo(DateTime toDate, DateTime fromDate)
        {

            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where order_time >=@toDate AND order_time <@fromDate");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@toDate", toDate.ToString(TimeFormatCustom.Format));
            command.Parameters.AddWithValue("@fromDate", fromDate.ToString(TimeFormatCustom.Format));
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);

            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantOrder aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, rowCount);
                    aRestaurantOrders.Add(aRestaurantOrder);

                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }

                rowCount++;

            }

            return aRestaurantOrders;


        }


        public List<RestaurantOrder> GetFinishedOrder(string status, int onlineStatus)
        {
            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where order_status=@order_status AND online_order=@online_order  AND is_sync=@is_sync limit 5");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_status", status);
            command.Parameters.AddWithValue("@is_sync", "0");
            command.Parameters.AddWithValue("@online_order", onlineStatus);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantOrder aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, rowCount);
                    aRestaurantOrders.Add(aRestaurantOrder);

                    rowCount++;
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }



            }
            Reader.Close();

            return aRestaurantOrders;
        }
        public string DeleteOnlineOrder()
        {
            int lastId = 0;


            Query = String.Format("delete from rcs_order where  online_order=@online_order ");



            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@online_order", 1);

                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }





            return (int)lastId > 0 ? "Yes" : "No";
        }

        #endregion

        internal int GetSortOrderByCategory(int catId)
        {
            int sort_order = 0;

            Query = String.Format("SELECT rcs_recipe_category.sort_order FROM rcs_recipe_category where rcs_recipe_category.id=@catId");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@catid", catId);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    sort_order = Convert.ToInt32(Reader["sort_order"]);


                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }

            }

            return sort_order;


        }





        internal string DeleteAllOrderByDate(DateTime toDateTime)
        {
            int lastId = 0;

            Query = String.Format("delete from rcs_order_item  where last_modify_time <=@TodateTime");


            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@TodateTime", toDateTime.ToString(TimeFormatCustom.Format));

                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



            Query = String.Format("delete from rcs_order_package  WHERE rcs_order_package.order_id IN (select id from rcs_order where rcs_order.order_time <=@toDateTime)");


            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@toDateTime", toDateTime.ToString(TimeFormatCustom.Format));

                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }





            Query = String.Format("delete from rcs_order  where order_time <=@toDateTime");


            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@toDateTime", toDateTime.ToString(TimeFormatCustom.Format));

                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



            return (int)lastId > 0 ? "Yes" : "No";

        }

        internal List<RestaurantOrder> GetAllOrder(string type = "DEL")
        {
            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();

            DateTime fromDateTime = DateTime.Today;
            DateTime toDateTime = DateTime.Today.AddDays(1);

            if (GlobalSetting.RestaurantInformation.ReportClosingHour > 12)
            {
                toDateTime = toDateTime.AddHours(-(24 - GlobalSetting.RestaurantInformation.ReportClosingHour));
                toDateTime = toDateTime.AddMinutes(-GlobalSetting.RestaurantInformation.ReportClosingMin);
            }
            else
            {
                toDateTime = toDateTime.AddHours(GlobalSetting.RestaurantInformation.ReportClosingHour);
                toDateTime = toDateTime.AddMinutes(GlobalSetting.RestaurantInformation.ReportClosingMin);
            }

            var fromDate = fromDateTime.ToString(TimeFormatCustom.Format);
            var toDate = toDateTime.ToString(TimeFormatCustom.Format);

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT rcs_order.*,case when(rcs_order.customer_id>0) then printf('%s %s, %s, %s, %s  %s', rcs_users.firstname, rcs_users.lastname,rcs_users.mobilephone,rcs_users.homephone,rcs_users.house,rcs_users.address)" +
                                  "  else rcs_order.customer_name end AS firstname FROM rcs_order LEFT OUTER JOIN  " +
                                  "rcs_users ON rcs_order.customer_id =rcs_users.id LEFT OUTER JOIN  rcs_restaurant_table ON rcs_order.order_table =rcs_restaurant_table.id " +
                                  "  where rcs_order.order_type =@type AND  rcs_order.status='pending' AND rcs_order.order_time >= @fromDate AND  rcs_order.order_time <= @toDate");
            // Query = String.Format("select * from GetAllOrder   where rcs_order.order_type =@type AND  rcs_order.status='pending' AND rcs_order.order_time >= @fromDate AND  rcs_order.order_time <= @toDate");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            command.Parameters.AddWithValue("@toDate",toDate);

            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);

            int rowCount = 0;
            // dataRow = command.ExecuteReader();
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantOrder aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, rowCount);
                    aRestaurantOrders.Add(aRestaurantOrder);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

                rowCount++;

            }




            return aRestaurantOrders;



        }

        internal bool UpdateOrderItemKitchenStatus(int recipeId, int orderId)
        {


            Query = String.Format("update rcs_order_item set  [kitchen_done]=[quantity]  where recipe_id=@recipe_id AND  order_id=@order_id");



            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@recipe_id", recipeId);
                command.Parameters.AddWithValue("@order_id", orderId);

                int lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return true;
        }

        internal int GetOrderItemKitchenStatus(int p, int orderId)
        {
            int kitchen_done = 0;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT kitchen_done FROM rcs_order_item where recipe_id=@recipe_id AND  order_id=@orderId");

            command = CommandMethod(command);

            command.Parameters.AddWithValue("@recipe_id", p);
            command.Parameters.AddWithValue("@orderId", p);

            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                try
                {
                    kitchen_done = Convert.ToInt32(Reader["kitchen_done"]);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            return kitchen_done;
        }

        internal bool DeleteItemsAndPackageByOrderId(int orderId)
        {

            int lastId = 0;
            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {
                c.Open();
                 using (SQLiteTransaction mytransaction = c.BeginTransaction())
                {
                    string query = String.Format("delete from rcs_order_item where order_id={0}", orderId);

                    using (SQLiteCommand command = new SQLiteCommand(query, c))
                    {

                        try
                        {

                         int count=   command.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(ex.ToString());
                        }


                    }

                    string query1 = String.Format("delete from rcs_order_package where order_id={0}", orderId);

                    using (SQLiteCommand command = new SQLiteCommand(query1, c))
                    {

                        try
                        {

                            int count=command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                        
                    }
                
                mytransaction.Commit();
                     }
                c.Close();}
            return true;

        }

        public DataTable GetRestaurantOrderByOrderOnlineSyncId(int orderId)
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order where id={0};", orderId);

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@id", orderId);

            Reader = ReaderMethod(Reader, command);
            //DataTable dataTable = new DataTable("rcs_order");
            DT.Load(Reader);
            return DT;
        }

        public DataTable GetRestaurantOrderPackageDataTableSync(int orderId)
        {
            List<OrderPackage> aRestaurantItem = new List<OrderPackage>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_package where order_id={0};", orderId);

            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);
            // dataRow = command.ExecuteReader();
            DT.Load(Reader);

            Reader.Close();
            return DT;
        }

        public DataTable GetRestaurantOrderRecipeItemsTest(int orderId)
        {
            List<OrderItem> aRestaurantItem = new List<OrderItem>();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_item where order_id={0};", orderId);


            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            int rowCount = 0;

            Reader.Close();

            return DT;
        }
    }
}
