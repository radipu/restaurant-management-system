using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.DAL.CombineReader;
//using Stripe;
using Newtonsoft.Json.Linq;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlRestaurantOrderDAO : MySqlGatewayServerConnection
    {
        #region  Order, Order Item, Order Package, Online Order (Insert, Update, Delete)

        RestaurantOrderReader _restaurantOrderReader = new RestaurantOrderReader();
        string MainConnectionString = Properties.Settings.Default.connString;

        private string queryRcs_Order =
                " `id`, `user_id` ,  `customer_id`,`restaurant_id`, Cast(rcs_order.order_time as DATETIME) as order_time,Cast(rcs_order.delivery_time as DATETIME) as delivery_time  ," +
                "Cast(rcs_order.payment_date as DATETIME) as  payment_date,`status` ,`payment_method` ,`payment_module` ,`delivery_address` ,`comments` ,`delivery_cost`,`vat` ,`total_cost`,`person` ,`order_table` ," +
                "`discount` ,`order_type` ,`order_status` ,`receipt` ,`comment` ,`cart_information` ,`customer_name` ,`coupon` ,`special_offer_item` ,`online_order` ,`online_order_status` ,`customer_email` ,`card_fee`,"
                + "`online_order_id` ,`cash_amount` ,`card_amount` ,`driver_id` ,`order_no` ,`update_time` ,`served_by` ,`is_sync` ,`service_charge`,`discount_amount`";

        internal string getOrderPayment(int orderID)
        {

           // JObject payment = new JObject();
            string payment = "";
            Query = String.Format("SELECT * FROM rcs_order_payments where order_id='{0}';", orderID);
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read())
            {

                try
                {
                    payment = Convert.ToString(Reader["payment_reference"]);
                   
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            } 

            bool readConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return payment;
        }


        internal OrderPayments getOrderPaymentDetailsByreservationId(int orderID)
        {
            OnlineOrderBLL aOnlineOrderBll = new OnlineOrderBLL();
            OrderPayments aPayment = new OrderPayments();
            string payment = "";
            Query = String.Format("SELECT * FROM rcs_order_payments where booking_id='{0}';", orderID);
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read())
            {

                try
                {
                    aPayment = ReadOrderPayment(Reader); 

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            bool readConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return aPayment;
        }
        public OrderPayments ReadOrderPayment(IDataReader oReader)
        {
            OrderPayments arcs_order = new OrderPayments();
            if (oReader["id"] != null)
            {
                arcs_order.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["order_id"] != DBNull.Value)
            {
                arcs_order.order_id = Convert.ToInt32(oReader["order_id"]);
            }
            if (oReader["booking_id"] != DBNull.Value)
            {
                arcs_order.booking_id = Convert.ToInt32(oReader["booking_id"]);
            }
            if (oReader["payment_reference"] != null)
            {
                arcs_order.payment_reference = Convert.ToString(oReader["payment_reference"]);
            }
            if (oReader["refund_reference"] != null)
            {
                arcs_order.refund_reference = Convert.ToString(oReader["refund_reference"]);
            }

            if (oReader["refund_amount"] != DBNull.Value)
            {
                arcs_order.refund_amount = Convert.ToDouble(oReader["refund_amount"]);
            }
            if (oReader["order_info"] != null)
            {
                arcs_order.order_info = Convert.ToString(oReader["order_info"]);
            }
            if (oReader["booking_amount"] != DBNull.Value)
            {
                arcs_order.booking_amount = Convert.ToDouble(oReader["booking_amount"]);
            }

            if (oReader["created"] != null)
            {
                arcs_order.created = Convert.ToDateTime(oReader["created"]);
            }
            if (oReader["updated"] != null)
            {
                arcs_order.updated = Convert.ToDateTime(oReader["updated"]);
            }
            return arcs_order;
        }

        internal int InsertRestaurantOrder(RestaurantOrder aRestaurantOrder)
        {

            long lastId = 0;

            string deliveryTime = "0000-00-00 00:00:00";
           
            string payTime = "0000-00-00 00:00:00";
            if (aRestaurantOrder.PaymentDate != null && aRestaurantOrder.PaymentDate != DateTime.Parse("0001-01-01 00:00:00"))
            {

                payTime = aRestaurantOrder.PaymentDate.ToString("yyyy-MM-dd HH:mm");
            }


            string datetimeNew = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            Query = String.Format("INSERT INTO rcs_order" +
                     "(user_id,customer_id ,restaurant_id,order_time,delivery_time,payment_date,status," +
                       " payment_method,payment_module ,delivery_address ,comments ,delivery_cost ,vat,total_cost,person," +
                         "order_table ,discount,order_type,order_status,receipt,comment,cart_information,customer_name," +
                         "coupon,online_order,online_order_status,special_offer_item," + "customer_email,card_fee,online_order_id," +
                         "cash_amount,card_amount,driver_id,order_no,update_time,served_by,is_sync,service_charge,discount_amount)" +

                         " VALUES(@user_id,@customer_id,@restaurant_id,@order_time,@delivery_time,@payment_date," +
                            "@status,@payment_method,@payment_module,@delivery_address,@comments,@delivery_cost," +
                            "@vat,@total_cost,@person,@order_table,@discount,@order_type,@order_status,@receipt,@comment," +
                            "@cart_information,@customer_name,@coupon,@online_order,@online_order_status," +
                            "@special_offer_item,@customer_email,@card_fee,@online_order_id,@cash_amount," +
                            "@card_amount,@driver_id,@order_no,@update_time,@served_by,@is_sync,@service_charge,@discount_amount)");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@user_id", aRestaurantOrder.UserId);
                command.Parameters.AddWithValue("@customer_id", aRestaurantOrder.CustomerId);
                command.Parameters.AddWithValue("@restaurant_id", aRestaurantOrder.RestaurantId);
                command.Parameters.AddWithValue("@order_time", aRestaurantOrder.OrderTime);
                command.Parameters.AddWithValue("@delivery_time", aRestaurantOrder.DeliveryTime);
                command.Parameters.AddWithValue("@payment_date", DateTime.Now);
                command.Parameters.AddWithValue("@status", aRestaurantOrder.Status);
                command.Parameters.AddWithValue("@payment_method", aRestaurantOrder.PaymentMethod ?? "");
                command.Parameters.AddWithValue("@payment_module", aRestaurantOrder.PaymentModule ?? "");
                //command.Parameters.AddWithValue("@d9", aRestaurantOrder.PaymentModule);
                command.Parameters.AddWithValue("@delivery_address", aRestaurantOrder.DeliveryAddress ?? "");
                command.Parameters.AddWithValue("@comments", aRestaurantOrder.Comments ?? "");
                command.Parameters.AddWithValue("@delivery_cost", GlobalVars.numberRound(aRestaurantOrder.DeliveryCost,2));
                command.Parameters.AddWithValue("@vat", aRestaurantOrder.Vat); ;
                command.Parameters.AddWithValue("@total_cost", GlobalVars.numberRound(aRestaurantOrder.TotalCost,2));
                command.Parameters.AddWithValue("@person", aRestaurantOrder.Person);
                command.Parameters.AddWithValue("@order_table", aRestaurantOrder.OrderTable);
                command.Parameters.AddWithValue("@discount", GlobalVars.numberRound(aRestaurantOrder.Discount, 2));
                command.Parameters.AddWithValue("@order_type", aRestaurantOrder.OrderType ?? "");
                command.Parameters.AddWithValue("@order_status", aRestaurantOrder.OrderStatus ?? "");
                command.Parameters.AddWithValue("@receipt", 1);
                command.Parameters.AddWithValue("@comment", aRestaurantOrder.Comment ?? "");
                command.Parameters.AddWithValue("@cart_information", aRestaurantOrder.CartInformation ?? "");
                command.Parameters.AddWithValue("@customer_name", aRestaurantOrder.CustomerName ?? "");
                command.Parameters.AddWithValue("@coupon", aRestaurantOrder.Coupon ?? "");
                command.Parameters.AddWithValue("@online_order", aRestaurantOrder.OnlineOrder);
                command.Parameters.AddWithValue("@online_order_status", aRestaurantOrder.OnlineOrderStatus ?? "pending");
                command.Parameters.AddWithValue("@customer_email", aRestaurantOrder.CustomerEmail ?? "");
                command.Parameters.AddWithValue("@card_fee", aRestaurantOrder.CardFee);
                command.Parameters.AddWithValue("@online_order_id", aRestaurantOrder.OnlineOrderId);
                command.Parameters.AddWithValue("@cash_amount", GlobalVars.numberRound(aRestaurantOrder.CashAmount,2));
                command.Parameters.AddWithValue("@card_amount", GlobalVars.numberRound(aRestaurantOrder.CardAmount,2)); 
                command.Parameters.AddWithValue("@driver_id", aRestaurantOrder.DriverId);
                command.Parameters.AddWithValue("@order_no", aRestaurantOrder.OrderNo);
                command.Parameters.AddWithValue("@special_offer_item", aRestaurantOrder.SpecialOfferItem ?? "");
                command.Parameters.AddWithValue("@update_time", aRestaurantOrder.UpdateTime);
                command.Parameters.AddWithValue("@served_by", aRestaurantOrder.ServedBy ?? "");
                command.Parameters.AddWithValue("@is_sync", aRestaurantOrder.IsSync);
                command.Parameters.AddWithValue("@service_charge", aRestaurantOrder.ServiceCharge);
                command.Parameters.AddWithValue("@discount_amount", GlobalVars.numberRound(aRestaurantOrder.DiscountAmount,2));



                int id = command.ExecuteNonQuery();
                Query = String.Format("select max(id) from rcs_order");
                command = CommandMethod(command);
                lastId = (long)Convert.ToInt64(command.ExecuteScalar());

                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            //  mytransaction.Commit();
            return (int)lastId;
        }

        internal bool updateOrderPayment(int orderID,string refarance,double amount)
        {
            Query = String.Format("update rcs_order_payments set refund_reference=@refund_reference,refund_amount=@refund_amount where order_id=@order_id");

            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@order_id", orderID);
                command.Parameters.AddWithValue("@refund_reference", refarance);
                command.Parameters.AddWithValue("@refund_amount", amount);
                int id = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return true;

        }

        internal bool InsertOrderPayment(OrderPayments currentcharge)
        { 
            Query = String.Format("INSERT INTO rcs_order_payments" +
                     "(order_id,payment_reference)" +
                         " VALUES(@order_id,@payment_reference)");
            try
            {
                
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@order_id", currentcharge.order_id);
                command.Parameters.AddWithValue("@payment_reference", currentcharge.payment_reference);
                int id = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }             
            return true;
        }

        public static string utf8Encode(string utf8String)
        {
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            //return System.Convert.ToBase64String(plainTIn sha extBytes);

            //byte[] bytes = Encoding.Default.GetBytes(plainText);
            //return Encoding.UTF8.GetString(bytes);


            string propEncodeString = string.Empty;

            byte[] utf8_Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                utf8_Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8_Bytes, 0, utf8_Bytes.Length);
        }



        internal int InsertRestaurantOrderItem(List<OrderItem> aOrderItems)
        {            
            int lastId = 0;
            List<int> insertIds = new List<int>();
            int orderId = 0;
            bool edited = false;
            string datetimeNew = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                foreach (OrderItem item in aOrderItems)
                {
                            orderId = item.OrderId;
                            try
                            {
                                if (item.PreOrderId > 0) {
                        edited = true;
                                    insertIds.Add(item.Id);
                                    Query = String.Format("update rcs_order_item set quantity =@Quantity, price =@Price,name =@Name,options_minus =@MinusOptions,options =@Options,extra_price =@ExtraPrice where id=@id");

                                    command = CommandMethod(command);
                                    command.Parameters.AddWithValue("@Name", item.Name ?? "");
                                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                                    command.Parameters.AddWithValue("@Price", item.Price);
                                    command.Parameters.AddWithValue("@Options", item.Options ?? "");
                                    command.Parameters.AddWithValue("@MinusOptions", item.MinusOptions ?? "");
                                    command.Parameters.AddWithValue("@ExtraPrice", item.ExtraPrice);
                                    command.Parameters.AddWithValue("@id", item.Id);
                                    lastId = command.ExecuteNonQuery();

                 
                                }
                                else
                                {
                                    Query = String.Format(
                                             "INSERT INTO rcs_order_item (order_id,recipe_id,package_id,name,quantity,price,extra_price,sent_to_kitchen" +
                                             ",kitchen_processing,kitchen_done,last_modify_time,options,options_minus,multiple_menu,order_package_id)"
                                             +
                                             "VALUES(@OrderId,@RecipeId,@PackageId,@Name,@Quantity,@Price ,@ExtraPrice,@SentToKitchen," +
                                             "@KitchenProcessing,@KitchenDone,@LastModifyTime,@Options,@MinusOptions,@multiplemenu,@orderPackageId)");

                                    command = CommandMethod(command);
                                    command.Parameters.AddWithValue("@OrderId", item.OrderId);
                                    command.Parameters.AddWithValue("@RecipeId", item.RecipeId);
                                    command.Parameters.AddWithValue("@PackageId", item.PackageId);
                                    command.Parameters.AddWithValue("@Name", item.Name ?? "");
                                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                                    command.Parameters.AddWithValue("@Price", item.Price);
                                    command.Parameters.AddWithValue("@ExtraPrice", item.ExtraPrice);
                                    command.Parameters.AddWithValue("@SentToKitchen", item.Quantity);
                                    command.Parameters.AddWithValue("@KitchenProcessing", item.KitchenProcessing);
                                    command.Parameters.AddWithValue("@KitchenDone", item.KitchenDone);
                                    command.Parameters.AddWithValue("@LastModifyTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    command.Parameters.AddWithValue("@Options", item.Options ?? "");
                                    command.Parameters.AddWithValue("@MinusOptions", item.MinusOptions ?? "");
                                    command.Parameters.AddWithValue("@orderPackageId", item.orderPackageId);
                                    command.Parameters.AddWithValue("@multiplemenu", item.MultipleMenu ?? "");
                                    lastId = command.ExecuteNonQuery();

                        Query = String.Format("select max(id) from rcs_order_item");
                        command = CommandMethod(command);

                        int last_Id =  Convert.ToInt32(command.ExecuteScalar());
                        insertIds.Add(last_Id);
 
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }
            if (edited)
            {

                var paramValue = string.Join(",", insertIds);
                //paramValue.Replace(',',"\\','")
                Query = String.Format("delete from rcs_order_item where `id` NOT IN ({0}) AND `order_id`=@order_id;", paramValue);
                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@order_id", orderId);
                  //  command.Parameters.AddWithValue("@items_id", paramValue);
                    int id = command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return (int)lastId;
        }

        internal List<KeyValuePair<int, int>> InsertOrderPackage(List<OrderPackage> aOrderPackage)
        {
            long lastId = 0;
            List<KeyValuePair<int, int>> packageIds = new List<KeyValuePair<int, int>>();


            foreach (OrderPackage package in aOrderPackage)
            {

                Query = String.Format(
                        "INSERT INTO rcs_order_package(order_id,package_id,name,quantity,price,extra_price)" +
                        " VALUES (@order_id ,@package_id ,@name,@quantity,@price,@extra_price)");
                //package.OrderId, package.PackageId, package.Name, package.Quantity, package.Price, package.Extra_price);

                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@order_id", package.OrderId);
                    command.Parameters.AddWithValue("@package_id", package.PackageId);
                    command.Parameters.AddWithValue("@name", package.Name);
                    command.Parameters.AddWithValue("@quantity", package.Quantity);

                    command.Parameters.AddWithValue("@price", GlobalVars.numberRound(package.Price, 2));
                    command.Parameters.AddWithValue("@extra_price", package.Extra_price);
                    lastId = command.ExecuteNonQuery();

                    Query = String.Format("select max(id) from rcs_order_package");
                    command = CommandMethod(command);
                    lastId = (long)Convert.ToInt64(command.ExecuteScalar());
                    if (package.optionIndex > 0)
                    {
                        // For Old Form version package optionIndex

                        packageIds.Add(new KeyValuePair<int, int>(package.optionIndex, (int)lastId));
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
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return packageIds;
        }



      //  internal int InsertRestaurantOrderPackage(List<OrderPackage> aOrderPackage)
        internal List<KeyValuePair<int, int>> InsertRestaurantOrderPackage(List<OrderPackage> aOrderPackage)
        {
            long lastId = 0;
            List<KeyValuePair<int, int>> packageIds = new List<KeyValuePair<int, int>>();


            foreach (OrderPackage package in aOrderPackage)
            {

                Query =  String.Format("INSERT INTO rcs_order_package(order_id,package_id,name,quantity,price,extra_price)" +
                        " VALUES (@order_id , @package_id,@name,@quantity,@price,@extra_price)");

                try
                {
                    
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@order_id", package.OrderId);
                    command.Parameters.AddWithValue("@package_id", package.PackageId);
                    command.Parameters.AddWithValue("@name", package.Name ?? "");
                    command.Parameters.AddWithValue("@quantity", package.Quantity);
                    command.Parameters.AddWithValue("@price", GlobalVars.numberRound(package.Price, 2));
                    command.Parameters.AddWithValue("@extra_price", package.Extra_price);

                    lastId = command.ExecuteNonQuery();
                    Query = String.Format("select max(id) from rcs_order_package");
                    command = CommandMethod(command);

                    lastId = (long)Convert.ToInt64(command.ExecuteScalar());
                    packageIds.Add(new KeyValuePair<int, int>(package.Id, (int)lastId));
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return packageIds;
        }

        internal RestaurantOrder  GetRestaurantOrder(int tableId, string status)
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where order_table={0} AND status='{1}' ;", tableId, status);
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command); DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            // dataRow = command.ExecuteReader();
            if (DT.Rows.Count > 0)
            {

                try
                {
                  aRestaurantTable = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, 0);
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
            return aRestaurantTable;
        }

        internal List<OrderItem> GetRestaurantOrderRecipeItems(int orderId)
        {
            List<OrderItem> aRestaurantItem = new List<OrderItem>();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_item where order_id={0};", orderId);


            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
                rowCount++;
            }            
            return aRestaurantItem;
        }


        internal List<OrderPackage> GetRestaurantOrderPackage(int orderId)
        {
            List<OrderPackage> aRestaurantItem = new List<OrderPackage>();
            Query = String.Format("SELECT * FROM rcs_order_package where order_id={0};", orderId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
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
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return aRestaurantItem;
        }
        internal DataTable GetRestaurantOrderPackageDataTableSync(int orderId)
        {
            DataTable DT = new DataTable();
            Query = String.Format("SELECT * FROM rcs_order_package where order_id={0};", orderId);
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            // dataRow = command.ExecuteReader();
            DT.Load(Reader);
            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return DT;
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
                        Query = String.Format(
                                "INSERT INTO rcs_order_item ( order_id , recipe_id , package_id , name , quantity , price , extra_price , sent_to_kitchen " +
                                ", kitchen_processing , kitchen_done , last_modify_time , options , options_minus , order_package_id,multiple_menu)" +
                                "VALUES(@order_id,@recipe_id,@package_id,@name,@quantity,@price ,@extra_price,@sent_to_kitchen,@kitchen_processing,@kitchen_done,@last_modify_time,@options," +
                                "@options_minus,@order_package_id,@multiple_menu)");
                        //,
                        //item.OrderId, item.RecipeId, item.PackageId, item.Name.Replace("'", " "), item.Quantity, item.Price, item.ExtraPrice, item.SentToKitchen, item.KitchenProcessing, item.KitchenDone
                        //, item.LastModifyTime, item.Options, item.MinusOptions, item.orderPackageId, item.Options_ids);
                        // }
                    }
                    else
                    {
                        Query = String.Format("update rcs_order_item set   quantity =@quantity, price =@price,  name =@name where id=@id");
                    }
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
                        command.Parameters.AddWithValue("@multiple_menu", item.MultipleMenu ?? "");
                        int lastId = command.ExecuteNonQuery();
                      
                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }




                }
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


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
            var it = aItems.SingleOrDefault(a => a.Options == item.Options);
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
                        Query = String.Format("INSERT INTO rcs_order_package(order_id,package_id,name,quantity,price,extra_price) " +
                                              "VALUES (@order_id ,@package_id,@name ,@quantity,@price,@extra_price)");
                    }
                    else
                    {
                        Query = String.Format("update rcs_order_package set quantity=@quantity,price=@price where id=@id");
                    }

                    try
                    {
                        command = CommandMethod(command);
                        command.Parameters.AddWithValue("@order_id", package.OrderId);
                        command.Parameters.AddWithValue("@package_id", package.PackageId);
                        command.Parameters.AddWithValue("@name", package.Name);
                        command.Parameters.AddWithValue("@price", GlobalVars.numberRound(package.Price, 2));
                        command.Parameters.AddWithValue("@extra_price", package.Extra_price);
                        command.Parameters.AddWithValue("@quantity", package.Quantity);

                        command.Parameters.AddWithValue("@id", package.Id);
                        long lastId = command.ExecuteNonQuery();
                       

                        Query = String.Format("select max(id) from rcs_order_package");
                        command = CommandMethod(command);

                        lastId = (long)Convert.ToInt64(command.ExecuteScalar());
                        
                        if (package.optionIndex > 0)
                        {// For Old Form version package optionIndex
                            if (package.Id <= 0)
                            {
                                packageIds.Add(new KeyValuePair<int, int>(package.optionIndex, (int)lastId));
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
                        }



                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }


                }
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                return packageIds;
            }
            catch (Exception ex)
            {
                return null;
            }
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
            bool DbConnection1 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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




                Query = String.Format("update rcs_order set  status=@status where id=@id");


                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@id", orderId);
                    command.Parameters.AddWithValue("@status", "paid");

                    int lastId = command.ExecuteNonQuery();
                    bool DbConnection1 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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


                Query = String.Format("UPDATE rcs_order SET " +
                              " payment_date =@payment_date,status = @status ,payment_method = @payment_method" +
                              ",payment_module = @payment_module,comments = @comments,vat = @vat," +
                               "total_cost =@total_cost,person =@person,order_table =@order_table," +
                               "discount =@discount,order_type = @order_type,order_status = @order_status ," +
                               "comment = @comment,cart_information = @cart_information ," +
                               "card_fee = @card_fee,card_amount = @card_amount,cash_amount=@cash_amount," +
                               "coupon=@coupon,is_sync=@is_sync,online_order_id=@online_order_id," +
                               "service_charge=@service_charge,discount_amount=@discount_amount,driver_id=@driverId,served_by=@served_by,delivery_address=@delivery_address  WHERE id=@id");


                try{
                    command = CommandMethod(command);
                    // command.Parameters.AddWithValue("@payment_date", Convert.ToDateTime(aRestaurantOrder.PaymentDate));
                    command.Parameters.AddWithValue("@payment_date", DateTime.Now);
                    command.Parameters.AddWithValue("@status", aRestaurantOrder.Status ?? "");
                    command.Parameters.AddWithValue("@payment_method", aRestaurantOrder.PaymentMethod ?? "");
                    command.Parameters.AddWithValue("@payment_module", aRestaurantOrder.PaymentModule ?? "");
                    command.Parameters.AddWithValue("@comments", aRestaurantOrder.Comments ?? "");
                    command.Parameters.AddWithValue("@vat", aRestaurantOrder.Vat);
                    command.Parameters.AddWithValue("@total_cost", GlobalVars.numberRound(aRestaurantOrder.TotalCost,2));
                    command.Parameters.AddWithValue("@person", aRestaurantOrder.Person);
                    command.Parameters.AddWithValue("@order_table", aRestaurantOrder.OrderTable);
                    command.Parameters.AddWithValue("@discount", aRestaurantOrder.Discount);
                    command.Parameters.AddWithValue("@order_type", aRestaurantOrder.OrderType ?? "");
                    command.Parameters.AddWithValue("@order_status", aRestaurantOrder.OrderStatus ?? "");
                 // command.Parameters.AddWithValue("@receipt", aRestaurantOrder.Receipt);
                    command.Parameters.AddWithValue("@comment", aRestaurantOrder.Comment ?? "");
                    command.Parameters.AddWithValue("@cart_information", aRestaurantOrder.CartInformation ?? "");
                    command.Parameters.AddWithValue("@card_fee", aRestaurantOrder.CardFee);
                    command.Parameters.AddWithValue("@card_amount", GlobalVars.numberRound(aRestaurantOrder.CardAmount,2));
                    command.Parameters.AddWithValue("@cash_amount", GlobalVars.numberRound(aRestaurantOrder.CashAmount,2));
                    command.Parameters.AddWithValue("@online_order_status", aRestaurantOrder.OnlineOrderStatus ?? "pending");
                    command.Parameters.AddWithValue("@is_sync", aRestaurantOrder.IsSync);
                    command.Parameters.AddWithValue("@service_charge", aRestaurantOrder.ServiceCharge);
                    command.Parameters.AddWithValue("@coupon", aRestaurantOrder.Coupon);
                    command.Parameters.AddWithValue("@id", aRestaurantOrder.Id);
                    command.Parameters.AddWithValue("@online_order_id", aRestaurantOrder.OnlineOrderId);
                    command.Parameters.AddWithValue("@discount_amount", aRestaurantOrder.DiscountAmount);
                    command.Parameters.AddWithValue("@driverId", aRestaurantOrder.DriverId);
                    command.Parameters.AddWithValue("@served_by", aRestaurantOrder.ServedBy);
                    command.Parameters.AddWithValue("@delivery_address", aRestaurantOrder.DeliveryAddress ?? "");

                    int lastId = command.ExecuteNonQuery();
                    bool DbConnection1 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
        internal DataTable GetRestaurantOrderByDate(DateTime startDate, DateTime endDate, RestaurantOrder order)
        {
            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            string sDate = startDate.ToString("yyyy-MM-dd HH:mm:ss");
            string eDate = endDate.ToString("yyyy-MM-dd HH:mm:ss");
            DataTable DT = new DataTable();
            try
            {
                DataSet DS = new DataSet(); 
                Query = String.Format("SELECT rcs_order.id, rcs_order.customer_id, Cast(rcs_order.order_time as DATETIME) as OrderTime, rcs_order.status," +
                        "rcs_order.payment_method, rcs_order.payment_module, rcs_order.delivery_address, rcs_order.total_cost, rcs_order.order_table," +
                        "rcs_order.order_type,rcs_order.order_status,rcs_order.cash_amount, rcs_order.card_amount, rcs_order.user_id, rcs_order.restaurant_id," +
                        "rcs_order.comments, rcs_order.delivery_cost, rcs_order.vat, rcs_order.person, rcs_order.discount, rcs_order.receipt," +
                        "rcs_order.comment, rcs_order.cart_information, rcs_order.coupon, rcs_order.online_order, rcs_order.online_order_status," +
                        "rcs_order.customer_email,rcs_order.customer_name, rcs_order.card_fee, rcs_order.online_order_id, rcs_order.driver_id, rcs_order.order_no, rcs_users.usertype," +
                        "CONCAT(rcs_users.firstname,' ', rcs_users.lastname,' ',rcs_users.mobilephone,' ',rcs_users.homephone,'@#@',rcs_users.house,' ',rcs_users.address,',',rcs_users.city,' ',rcs_users.postcode) as firstname, rcs_restaurant_table.name "+ 
                        "FROM rcs_order LEFT JOIN `rcs_users` ON `rcs_users`.`id`=`rcs_order`.`customer_id` JOIN `rcs_restaurant` ON `rcs_order`.`restaurant_id` = `rcs_restaurant`.`id` LEFT JOIN `rcs_restaurant_table` ON `rcs_restaurant_table`.`id`=`rcs_order`.`order_table` " +
                        "where " +
                        ((order.PaymentMethod != "All") ? "rcs_order.payment_method='{4}' and " : "")
                        + ((order.Status != "All") ? " rcs_order.status='{2}' and " : "")
                        + ((order.OrderType != "All" && order.OrderType != "Online") ? " rcs_order.order_type='{3}' and " : "")
                        + ((order.OrderType != "All" && order.OrderType == "Online") ? " rcs_order.online_order > 0 and " : "")
                        + " rcs_order.order_time >= '{0}' AND  rcs_order.order_time <= '{1}' order by  rcs_order.id desc;",sDate,eDate,order.Status,order.OrderType,order.PaymentMethod);

                Adapter = GetAdapter(Adapter);
                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0]; 
                bool DbConnection1 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
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
            Query = String.Format("SELECT  " + queryRcs_Order + " FROM rcs_order where customer_id={0} LIMIT 1;", customerId);

            command = CommandMethod(command);


            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where id={0};", orderId);

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@id", orderId);

            Reader = ReaderMethod(Reader, command);
            //DataTable dataTable = new DataTable("rcs_order");
            DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


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
        internal DataTable GetRestaurantOrderByOrderOnlineSyncId(int orderId)
        {
            RestaurantOrder aRestaurantTable = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where id={0};", orderId);

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@id", orderId);

            Reader = ReaderMethod(Reader, command);
            //DataTable dataTable = new DataTable("rcs_order");
            DT.Load(Reader);

            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);



            return DT;
        }
        internal RestaurantOrder GetRestaurantOrderByOnlineOrder(long onlineOrderId)
        {
            RestaurantOrder aRestaurantOrder = new RestaurantOrder();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where online_order_id=@online_order_id");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@online_order_id", onlineOrderId);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
            Query = String.Format("SELECT  A.* ,CONCAT(rcs_users.firstname,rcs_users.lastname,',',rcs_users.mobilephone,rcs_users.homephone,rcs_users.house,rcs_users.address) as cusname FROM rcs_order A left join rcs_users on  A.customer_id=rcs_users.id   where online_order_status='{0}' AND online_order={1};", status, onlineOrder);
            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);
           
            DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
            using (MySqlConnection c = new MySqlConnection(MainConnectionString))
            {
                c.Open();
                //  using (SQLiteTransaction mytransaction = c.BeginTransaction())
                {

                    string query = String.Format("delete from rcs_order_item where order_id=@order_id");

                    using (MySqlCommand command = new MySqlCommand(query, c))
                    {

                        try
                        {
                            command.Parameters.AddWithValue("@order_id", orderId);
                            lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }


                    }

                    query = String.Format("delete from rcs_order_package where order_id=@order_id");

                    using (MySqlCommand command = new MySqlCommand(query, c))
                    {

                        try
                        {
                            command.Parameters.AddWithValue("@order_id", orderId);
                            lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }


                    }
                    query = String.Format("delete from rcs_order where id=@order_id");

                    using (MySqlCommand command = new MySqlCommand(query, c))
                    {

                        try
                        {
                            command.Parameters.AddWithValue("@order_id", orderId);
                            lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                    }
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
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return (int)lastId > 0 ? "Yes" : "No";
        }

        internal string UpdateOnlineOrder(int orderId, string status)
        {
            int lastId = 0;
            try
            {
                Query = String.Format("UPDATE rcs_order SET online_order_status=@status  WHERE online_order_id=@id");

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@id", orderId);
                command.Parameters.AddWithValue("@status", status);

                lastId = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


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
            string from_Date = fromDate.ToString("yyyy-MM-dd HH:mm");
             string to_Date = toDate.ToString("yyyy-MM-dd HH:mm");

            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where order_time >=@toDate AND order_time <= @FromDate AND customer_id=@Customer");

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@toDate", to_Date);
            command.Parameters.AddWithValue("@FromDate", from_Date);
            command.Parameters.AddWithValue("@Customer", customerId);
            Reader = ReaderMethod(Reader, command);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);



            try
            {
                DT.Load(Reader);
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

        public int GetMaxOrderNumber(DateTime toDate, DateTime fromDate)
        {

            int maxOrder = 0;
            
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT MAX(order_no) AS order_no FROM rcs_order where order_time >=@toDate AND order_time <@fromDate");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@toDate", toDate);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                try
                {
                    maxOrder = Convert.ToInt32(Reader["order_no"]);
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }

            return maxOrder + 1;


        }

        public List<RestaurantOrder> GetRestaurantOrderByDateForOrderNo(DateTime toDate, DateTime fromDate)
        {

            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where order_time >=@toDate AND order_time <@fromDate");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@toDate", toDate);
            command.Parameters.AddWithValue("@fromDate", fromDate);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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




        //all result come where is_sync less then 0 
        public List<RestaurantOrder> GetFinishedOrder(string status, int onlineStatus)
        {
            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where order_status=@order_status AND online_order=@online_order  AND is_sync<=@is_sync limit 25");
            // Query = String.Format("SELECT " + queryRcs_Order + " FROM rcs_order where order_status=@order_status  AND is_sync=@is_sync limit 70");


            command = CommandMethod(command);
            command.Parameters.AddWithValue("@order_status", status);
            command.Parameters.AddWithValue("@is_sync", "0");
            command.Parameters.AddWithValue("@online_order", "0");
            Reader = ReaderMethod(Reader, command);

            DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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
            return aRestaurantOrders;
        }

        public string DeleteOnlineOrder()
        {
            int lastId = 0;
            Query = String.Format("delete from rcs_order where  online_order=@online_order");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@online_order", 1);
                lastId = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
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
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return sort_order;
        }

        internal int GetRecipeTypeIdByCategoryId(int catId)
        {
            int typeId = 0;
            Query = String.Format("SELECT recipe_type FROM rcs_recipe_category where id=@catId");
            command = CommandMethod(command);
            command.Parameters.AddWithValue("@catid", catId);
            Reader = ReaderMethod(Reader, command);
            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {
                try
                {
                    typeId = Convert.ToInt32(Reader["recipe_type"]);
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            return typeId;
        }

        internal string DeleteAllOrderByDate(DateTime toDateTime)
        {
            int lastId = 0;

            Query = String.Format("delete from rcs_order_item  where last_modify_time <=@TodateTime");


            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@TodateTime", toDateTime);

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
                command.Parameters.AddWithValue("@toDateTime", toDateTime);

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
                command.Parameters.AddWithValue("@toDateTime", toDateTime);

                lastId = command.ExecuteNonQuery();

                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            Query = String.Format("delete FROM rcs_order_payments where updated <=@toDateTime");
            try
            {

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@toDateTime", toDateTime);
                lastId = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return (int)lastId > 0 ? "Yes" : "No";
        }

        internal List<RestaurantOrder> GetAllOrder(string type = "DEL" , string status="pending")
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


            string sDate = fromDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string eDate = toDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            if (status != "ALL")
            {
                Query = String.Format("SELECT rcs_users.postcode,Concat(rcs_postcode.latitude,',',rcs_postcode.longitude) Position ,rcs_order.*,CONCAT((case when(rcs_order.customer_id > 0)  then CONCAT(rcs_users.firstname,' ', rcs_users.lastname,',')  else rcs_order.customer_name END),(case when(rcs_order.delivery_address = '')  then CONCAT(rcs_users.mobilephone,rcs_users.homephone,'@#@',rcs_users.full_address,',',rcs_users.postcode)  else rcs_order.delivery_address END)) AS firstname  FROM rcs_order LEFT OUTER JOIN  rcs_users ON rcs_order.customer_id = rcs_users.id LEFT OUTER JOIN  rcs_restaurant_table ON rcs_order.order_table =rcs_restaurant_table.id  left JOIN rcs_postcode ON rcs_postcode.postcode=UPPER(rcs_users.postcode) where rcs_order.order_type ='{0}' AND  " +
                 "rcs_order.status='{3}' AND  DATE_FORMAT(rcs_order.order_time,'%Y-%m-%d %H:%i') between '{1}' AND '{2}';", type, sDate, eDate, status);
            }
            else
            {
                Query = String.Format("SELECT rcs_users.postcode,Concat(rcs_postcode.latitude,',',rcs_postcode.longitude) Position ,rcs_order.*,CONCAT((case when(rcs_order.customer_id > 0)  then CONCAT(rcs_users.firstname,' ', rcs_users.lastname,',')  else rcs_order.customer_name END),(case when(rcs_order.delivery_address ='')  then CONCAT(rcs_users.mobilephone,rcs_users.homephone,'@#@',rcs_users.full_address,',',rcs_users.postcode)  else rcs_order.delivery_address END)) AS firstname  FROM rcs_order LEFT OUTER JOIN  rcs_users ON rcs_order.customer_id = rcs_users.id LEFT OUTER JOIN  rcs_restaurant_table ON rcs_order.order_table =rcs_restaurant_table.id  left JOIN rcs_postcode ON rcs_postcode.postcode=UPPER(rcs_users.postcode) where rcs_order.order_type ='{0}' AND  " +
                          "DATE_FORMAT(rcs_order.order_time,'%Y-%m-%d %H:%i') between '{1}' AND '{2}';", type, sDate, eDate);
            }

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            int rowCount = 0;
            //dataRow = command.ExecuteReader();
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantOrder aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, rowCount,"DEL");
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


            Query = String.Format("update rcs_order_item set  `kitchen_done`=`quantity`  where recipe_id=@recipe_id AND  order_id=@order_id;");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@recipe_id", recipeId);
                command.Parameters.AddWithValue("@order_id", orderId);
                int lastId = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
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
            command.Parameters.AddWithValue("@orderId", orderId);

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

            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return kitchen_done;
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
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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


            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            return (int)lastId > 0 ? true : false;
        }

        internal bool DeleteItemsAndPackageByOrderId(int orderId)
        {

            int lastId = 0;
            using (var c = Connection)
            {

                using (MySqlTransaction mytransaction = c.BeginTransaction())
                {
                    //string query = String.Format("delete from rcs_order_item where order_id=@order_id");
                    //using (MySqlCommand command = new MySqlCommand(query, c))
                    //{
                    //    try
                    //    {
                    //        command.Parameters.AddWithValue("@order_id", orderId);
                    //        int count = command.ExecuteNonQuery();
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    //        aErrorReportBll.SendErrorReport(ex.ToString());
                    //    }
                    //}

                    string query1 = String.Format("delete from rcs_order_package where order_id=@order_id");
                    using (MySqlCommand command = new MySqlCommand(query1, c))
                    {
                        try
                        {
                            command.Parameters.AddWithValue("@order_id", orderId);
                            int count = command.ExecuteNonQuery();
                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                    }
                    mytransaction.Commit();
                }
            }
            return true;
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

          
            bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return DT;
        }

        internal bool UpdateKitchenStatus(int orderId)
        {

            //Query = String.Format("update `rcs_order_item` AS oi  JOIN `rcs_recipe` AS r ON `oi`.recipe_id = `r`.id set `oi`.`sent_to_kitchen`= `oi`.`quantity`, `oi`.`kitchen_processing`= `oi`.`quantity` WHERE r.kitchen_section = 0 AND `oi`.order_id =@order_id;");
            
            //for mangrove only, we need to update all recipe for them 
            Query = String.Format("update `rcs_order_item` AS oi  JOIN `rcs_recipe` AS r ON `oi`.recipe_id = `r`.id set `oi`.`sent_to_kitchen`= `oi`.`quantity`, `oi`.`kitchen_processing`= `oi`.`quantity` WHERE `oi`.order_id =@order_id;");
            
            try
            {
                command = CommandMethod(command); 
                command.Parameters.AddWithValue("@order_id", orderId);
                  int lastId = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return true;
        }



        internal bool UpdateKitchenStatusForAutoprint(int orderId,List<int> itemIds)
        {
            //string items_array = string.Join(",", itemIds);

            //Query = String.Format("UPDATE `rcs_order_item` SET `sent_to_kitchen`= `quantity`, `kitchen_processing`= `quantity` WHERE  order_id =@order_id  AND id IN (" + items_array + ");");
            Query = String.Format("update `rcs_order_item`  set `sent_to_kitchen`=`quantity`,`kitchen_processing`=`quantity`  where  order_id=@order_id;");
            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@order_id", orderId);                
                int lastId = command.ExecuteNonQuery();
                bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return true;
        }

        internal bool UpdateOrderStatus(RestaurantOrder aRestaurantOrder)
        {
            try
            {
                Query = String.Format("UPDATE rcs_order SET " +"receipt =@receipt  WHERE id=@id");
                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@receipt", aRestaurantOrder.Receipt); 
                    command.Parameters.AddWithValue("@id", aRestaurantOrder.Id); 
                    int lastId = command.ExecuteNonQuery();

                    bool DbConnection1 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

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

        internal bool UpdateOrderDelTime(RestaurantOrder aRestaurantOrder)
        {
            try
            {
                Query = String.Format("UPDATE rcs_order SET " + "delivery_time =@receipt  WHERE id=@id");
                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@receipt", aRestaurantOrder.DeliveryTime);
                    command.Parameters.AddWithValue("@id", aRestaurantOrder.Id);
                    int lastId = command.ExecuteNonQuery();
                    bool DbConnection1 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
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

        //all result come where is_sync less then 0 
        public List<RestaurantOrder> GetAllAutoPrintOrder()
        {  
            List<RestaurantOrder> aRestaurantOrders = new List<RestaurantOrder>();
            if (Properties.Settings.Default.autoPrint != "")
            {
                try
                {

                    DataSet DS = new DataSet();
                    DataTable DT = new DataTable();
                    Query = String.Format("SELECT " + queryRcs_Order + " FROM `rcs_order` where served_by like concat(@served_by,'%') AND served_by not like '%-save' limit 1");

                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@served_by", Properties.Settings.Default.autoPrint);
                    Reader = ReaderMethod(Reader, command);


                    DT.Load(Reader);
                    bool DbConnection = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                    int rowCount = 0;
                    while (DT.Rows.Count > rowCount)
                    {
                        RestaurantOrder aRestaurantOrder = _restaurantOrderReader.ReaderToReadRestaurantOrder(DT, rowCount);
                        aRestaurantOrders.Add(aRestaurantOrder);
                        rowCount++;

                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }
            return aRestaurantOrders;
        }


    }
}
