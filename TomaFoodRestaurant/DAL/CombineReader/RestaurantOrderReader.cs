using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.CombineReader
{
    public class RestaurantOrderReader
    {
        public RestaurantOrder ReaderToReadRestaurantOrder(DataTable oReader, int i, string orderType = null)
        {
            RestaurantOrder arcs_order = new RestaurantOrder();
            if (oReader.Rows[i]["id"] != DBNull.Value)
            {
                arcs_order.Id = Convert.ToInt32(oReader.Rows[i]["id"]);
            }
            if (oReader.Rows[i]["user_id"] != DBNull.Value)
            {
                arcs_order.UserId = Convert.ToInt32(oReader.Rows[i]["user_id"]);
            }
            if (oReader.Rows[i]["customer_id"] != DBNull.Value)
            {
                arcs_order.CustomerId = Convert.ToInt32(oReader.Rows[i]["customer_id"]);
            }
            if (oReader.Rows[i]["restaurant_id"] != DBNull.Value)
            {
                arcs_order.RestaurantId = Convert.ToInt32(oReader.Rows[i]["restaurant_id"]);
            }
            if (orderType != null)
            {
                if (oReader.Rows[i]["postcode"] != DBNull.Value)
                {
                    arcs_order.PostCode = Convert.ToString(oReader.Rows[i]["postcode"]);
                }
                if (oReader.Rows[i]["Position"] != DBNull.Value)
                {
                    arcs_order.Position = Convert.ToString(oReader.Rows[i]["Position"]);
                }
                if (oReader.Rows[i]["firstname"] != DBNull.Value)
                {
                    arcs_order.FormatedAddtress = Convert.ToString(oReader.Rows[i]["firstname"]);
                }
            }

            try
            {
                if (oReader.Rows[i]["order_time"] != DBNull.Value)
                {
                    arcs_order.OrderTime = DateTime.Parse((oReader.Rows[i]["order_time"]).ToString());

                    //     arcs_order.OrderTime = Convert.ToDateTime(oReader.Rows[i]["order_time"].ToString());// DateTime.Parse((oReader.Rows[i]["order_time"]).ToString());
                }
            }
            catch (Exception exception)
            {
                arcs_order.OrderTime = DateTime.Now;
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            try
            {
                if (oReader.Rows[i]["delivery_time"] != DBNull.Value && oReader.Rows[i]["delivery_time"].ToString() != "0001-01-01 00:00:00")
                {
                    arcs_order.DeliveryTime = Convert.ToDateTime(oReader.Rows[i]["delivery_time"].ToString());
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            //try
            //{
            //    if (oReader.Rows[i]["payment_date"] != DBNull.Value && oReader.Rows[i]["payment_date"].ToString() != "")
            //    {
            //        arcs_order.PaymentDate = Convert.ToDateTime(Convert.ToString(oReader.Rows[i]["payment_date"]));// DateTime.Parse((oReader.Rows[i]["payment_date"]).ToString()); //

            //        //string format = "ddd dd MMM h:mm tt yyyy";
            //       //arcs_order.PaymentDate = DateTime.ParseExact(oReader.Rows[i]["order_time"].ToString(), format,CultureInfo.InvariantCulture);

            //    }
            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}

            if (oReader.Rows[i]["status"] != DBNull.Value)
            {
                arcs_order.Status = Convert.ToString(oReader.Rows[i]["status"]);
            }
            if (oReader.Rows[i]["payment_method"] != DBNull.Value)
            {
                arcs_order.PaymentMethod = Convert.ToString(oReader.Rows[i]["payment_method"]);
            }
            if (oReader.Rows[i]["payment_module"] != DBNull.Value)
            {
                arcs_order.PaymentModule = Convert.ToString(oReader.Rows[i]["payment_module"]);
            }
            if (oReader.Rows[i]["delivery_address"] != DBNull.Value)
            {
                arcs_order.DeliveryAddress = Convert.ToString(oReader.Rows[i]["delivery_address"]);
            }
            if (oReader.Rows[i]["comments"] != DBNull.Value)
            {
                arcs_order.Comments = Convert.ToString(oReader.Rows[i]["comments"]);
            }
            if (oReader.Rows[i]["delivery_cost"] != DBNull.Value)
            {
                arcs_order.DeliveryCost = Convert.ToDouble(oReader.Rows[i]["delivery_cost"]);
            }
            if (oReader.Rows[i]["vat"] != DBNull.Value)
            {
                arcs_order.Vat = Convert.ToDouble(oReader.Rows[i]["vat"]);
            }
            if (oReader.Rows[i]["total_cost"] != DBNull.Value)
            {
                arcs_order.TotalCost = Convert.ToDouble(oReader.Rows[i]["total_cost"]);
            }
            if (oReader.Rows[i]["person"] != DBNull.Value)
            {
                arcs_order.Person = Convert.ToInt32(oReader.Rows[i]["person"]);
            }
            if (oReader.Rows[i]["order_table"] != DBNull.Value)
            {
                arcs_order.OrderTable = Convert.ToInt32(oReader.Rows[i]["order_table"]);
            }
            if (oReader.Rows[i]["discount"] != DBNull.Value)
            {
                arcs_order.Discount = Convert.ToDouble(oReader.Rows[i]["discount"]);
            }
            if (oReader.Rows[i]["order_type"] != DBNull.Value)
            {
                arcs_order.OrderType = Convert.ToString(oReader.Rows[i]["order_type"]);
            }
            if (oReader.Rows[i]["order_status"] != DBNull.Value)
            {
                arcs_order.OrderStatus = Convert.ToString(oReader.Rows[i]["order_status"]);
            }
            if (oReader.Rows[i]["receipt"] != DBNull.Value)
            {
                arcs_order.Receipt = Convert.ToInt64(oReader.Rows[i]["receipt"]);
            }
            if (oReader.Rows[i]["comment"] != DBNull.Value)
            {
                arcs_order.Comment = Convert.ToString(oReader.Rows[i]["comment"]);
            }
            if (oReader.Rows[i]["cart_information"] != DBNull.Value)
            {
                arcs_order.CartInformation = Convert.ToString(oReader.Rows[i]["cart_information"]);
            }
            try
            {


                if (oReader.Rows[i]["customer_name"] != DBNull.Value && oReader.Rows[i]["customer_name"].ToString() != "")
                {
                    arcs_order.CustomerName = Convert.ToString(oReader.Rows[i]["customer_name"]);
                }

            }
            catch (Exception exce)
            {
                arcs_order.CustomerName = "";
                //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                //aErrorReportBll.SendErrorReport(exce.ToString());
            }

            if (oReader.Rows[i]["coupon"] != DBNull.Value)
            {
                arcs_order.Coupon = Convert.ToString(oReader.Rows[i]["coupon"]);
            }
            if (oReader.Rows[i]["online_order"] != DBNull.Value)
            {
                arcs_order.OnlineOrder = Convert.ToInt64(oReader.Rows[i]["online_order"]);
            }
            if (oReader.Rows[i]["online_order_status"] != DBNull.Value)
            {
                arcs_order.OnlineOrderStatus = Convert.ToString(oReader.Rows[i]["online_order_status"]);
            }
            if (oReader.Rows[i]["customer_email"] != DBNull.Value)
            {
                arcs_order.CustomerEmail = Convert.ToString(oReader.Rows[i]["customer_email"]);
            }
            if (oReader.Rows[i]["card_fee"] != DBNull.Value)
            {
                arcs_order.CardFee = Convert.ToDouble(oReader.Rows[i]["card_fee"]);
            }
            if (oReader.Rows[i]["online_order_id"] != DBNull.Value)
            {
                arcs_order.OnlineOrderId = Convert.ToInt64(oReader.Rows[i]["online_order_id"]);
            }
            if (oReader.Rows[i]["cash_amount"] != DBNull.Value)
            {
                arcs_order.CashAmount = Convert.ToDouble(oReader.Rows[i]["cash_amount"]);
            }
            if (oReader.Rows[i]["card_amount"] != DBNull.Value)
            {
                arcs_order.CardAmount = Convert.ToDouble(oReader.Rows[i]["card_amount"]);
            }
            if (oReader.Rows[i]["driver_id"] != DBNull.Value)
            {
                arcs_order.DriverId = Convert.ToInt32(oReader.Rows[i]["driver_id"]);
            }
            if (oReader.Rows[i]["order_no"] != DBNull.Value)
            {
                arcs_order.OrderNo = Convert.ToInt32(oReader.Rows[i]["order_no"]);
            }
            if (oReader.Rows[i]["is_sync"] != DBNull.Value)
            {
                arcs_order.IsSync = Convert.ToInt32(oReader.Rows[i]["order_no"]);
            }
            if (oReader.Rows[i]["discount_amount"] != DBNull.Value)
            {
                arcs_order.DiscountAmount = Convert.ToDouble(oReader.Rows[i]["discount_amount"]);
            }
            try
            {
                if (oReader.Rows[i]["special_offer_item"] != DBNull.Value)
                {
                    arcs_order.SpecialOfferItem = Convert.ToString(oReader.Rows[i]["special_offer_item"]);
                }
                if (oReader.Rows[i]["update_time"] != DBNull.Value)
                {
                    arcs_order.UpdateTime = Convert.ToDateTime(oReader.Rows[i]["update_time"].ToString());
                }
                if (oReader.Rows[i]["served_by"] != DBNull.Value)
                {
                    arcs_order.ServedBy = Convert.ToString(oReader.Rows[i]["served_by"]);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            try
            {
                if (oReader.Rows[i]["service_charge"] != DBNull.Value)
                {
                    arcs_order.ServiceCharge = Convert.ToDouble(oReader.Rows[i]["service_charge"]);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return arcs_order;
        }

        public OrderItem ReaderToReadOrderItem(DataTable oReader, int i)
        {
            OrderItem arcs_order_item = new OrderItem();

            arcs_order_item.Id = Convert.ToInt32(oReader.Rows[i]["id"]);


            arcs_order_item.OrderId = Convert.ToInt32(oReader.Rows[i]["order_id"]);


            arcs_order_item.RecipeId = Convert.ToInt32(oReader.Rows[i]["recipe_id"]);

            arcs_order_item.PackageId = Convert.ToInt32(oReader.Rows[i]["package_id"]);

            arcs_order_item.Name = Convert.ToString(oReader.Rows[i]["name"]);

            arcs_order_item.Quantity = Convert.ToInt32(oReader.Rows[i]["quantity"]);

            arcs_order_item.Price = Convert.ToDouble(oReader.Rows[i]["price"]);
            arcs_order_item.ExtraPrice = Convert.ToDouble(oReader.Rows[i]["extra_price"]);

            arcs_order_item.SentToKitchen = Convert.ToInt32(oReader.Rows[i]["sent_to_kitchen"]);

            arcs_order_item.KitchenProcessing = Convert.ToInt32(oReader.Rows[i]["kitchen_processing"]);

            arcs_order_item.KitchenDone = Convert.ToInt32(oReader.Rows[i]["kitchen_done"]);

            arcs_order_item.LastModifyTime = Convert.ToDateTime(oReader.Rows[i]["last_modify_time"]);

            arcs_order_item.Options = Convert.ToString(oReader.Rows[i]["options"]).Trim();
            arcs_order_item.MinusOptions = Convert.ToString(oReader.Rows[i]["options_minus"]);

            arcs_order_item.MultipleMenu = Convert.ToString(oReader.Rows[i]["multiple_menu"]);

            arcs_order_item.orderPackageId = Convert.ToInt32(oReader.Rows[i]["order_package_id"]);



            return arcs_order_item;
        }

        public OrderPackage ReaderToReadOrderPackage(IDataReader oReader)
        {
            OrderPackage arcs_order_package = new OrderPackage();
            if (oReader["id"] != DBNull.Value)
            {
                arcs_order_package.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["order_id"] != DBNull.Value)
            {
                arcs_order_package.OrderId = Convert.ToInt32(oReader["order_id"]);
            }
            if (oReader["package_id"] != DBNull.Value)
            {
                arcs_order_package.PackageId = Convert.ToInt32(oReader["package_id"]);
            }
            if (oReader["name"] != DBNull.Value)
            {
                arcs_order_package.Name = Convert.ToString(oReader["name"]);
            }
            if (oReader["quantity"] != DBNull.Value)
            {
                arcs_order_package.Quantity = Convert.ToInt32(oReader["quantity"]);
            }
            if (oReader["price"] != DBNull.Value)
            {
                arcs_order_package.Price = Convert.ToDouble(oReader["price"]);
            }
            if (oReader["extra_price"] != DBNull.Value)
            {
                arcs_order_package.Extra_price = Convert.ToDouble(oReader["extra_price"]);
            }
            return arcs_order_package;
        }

        public RestaurantOrder ReaderToReadRestaurantOrderForOnline(DataTable oReader, int i)
        {
            RestaurantOrder arcs_order = new RestaurantOrder();

            try
            {

                arcs_order.Id = Convert.ToInt32(oReader.Rows[i]["id"]);

                arcs_order.UserId = Convert.ToInt32(oReader.Rows[i]["user_id"]);

                arcs_order.CustomerId = Convert.ToInt32(oReader.Rows[i]["customer_id"]);

                arcs_order.RestaurantId = Convert.ToInt32(oReader.Rows[i]["restaurant_id"]);


                arcs_order.OrderTime = DateTime.Parse((oReader.Rows[i]["order_time"]).ToString());
                arcs_order.DeliveryTime = Convert.ToDateTime(oReader.Rows[i]["delivery_time"]);

                arcs_order.PaymentDate = DateTime.Parse((oReader.Rows[i]["payment_date"]).ToString());



                arcs_order.Status = Convert.ToString(oReader.Rows[i]["status"]);

                arcs_order.PaymentMethod = Convert.ToString(oReader.Rows[i]["payment_method"]);

                arcs_order.PaymentModule = Convert.ToString(oReader.Rows[i]["payment_module"]);

                arcs_order.DeliveryAddress = Convert.ToString(oReader.Rows[i]["delivery_address"]);

                arcs_order.Comments = Convert.ToString(oReader.Rows[i]["comments"]);

                arcs_order.DeliveryCost = Convert.ToDouble(oReader.Rows[i]["delivery_cost"]);

                arcs_order.Vat = Convert.ToDouble(oReader.Rows[i]["vat"]);

                arcs_order.TotalCost = Convert.ToDouble(oReader.Rows[i]["total_cost"]);

                arcs_order.Person = Convert.ToInt32(oReader.Rows[i]["person"]);

                arcs_order.OrderTable = Convert.ToInt32(oReader.Rows[i]["order_table"]);

                arcs_order.Discount = Convert.ToDouble(oReader.Rows[i]["discount"]);

                arcs_order.OrderType = Convert.ToString(oReader.Rows[i]["order_type"]);

                arcs_order.OrderStatus = Convert.ToString(oReader.Rows[i]["order_status"]);

                arcs_order.Receipt = Convert.ToInt64(oReader.Rows[i]["receipt"]);

                arcs_order.Comment = Convert.ToString(oReader.Rows[i]["comment"]);

                arcs_order.CartInformation = Convert.ToString(oReader.Rows[i]["cart_information"]);

                arcs_order.CustomerName = Convert.ToString(oReader.Rows[i]["cusname"]);

                arcs_order.Coupon = Convert.ToString(oReader.Rows[i]["coupon"]);

                arcs_order.OnlineOrder = Convert.ToInt64(oReader.Rows[i]["online_order"]);

                arcs_order.OnlineOrderStatus = Convert.ToString(oReader.Rows[i]["online_order_status"]);

                arcs_order.CustomerEmail = Convert.ToString(oReader.Rows[i]["customer_email"]);

                arcs_order.CardFee = Convert.ToDouble(oReader.Rows[i]["card_fee"]);

                arcs_order.OnlineOrderId = Convert.ToInt64(oReader.Rows[i]["online_order_id"]);

                arcs_order.CashAmount = Convert.ToDouble(oReader.Rows[i]["cash_amount"]);

                arcs_order.CardAmount = Convert.ToDouble(oReader.Rows[i]["card_amount"]);

                arcs_order.DriverId = Convert.ToInt32(oReader.Rows[i]["driver_id"]);

                arcs_order.OrderNo = Convert.ToInt32(oReader.Rows[i]["order_no"]);

                arcs_order.SpecialOfferItem = Convert.ToString(oReader.Rows[i]["special_offer_item"]);

                arcs_order.UpdateTime = Convert.ToDateTime(oReader.Rows[i]["update_time"]);

                arcs_order.ServedBy = Convert.ToString(oReader.Rows[i]["served_by"]);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return arcs_order;
        }
    }
}
