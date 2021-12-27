using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using TomaFoodRestaurant.Model;
using Newtonsoft.Json;

namespace TomaFoodRestaurant.BLL
{
   public class OnlineOrderBLL
    {

        public RestaurantOrder GetOnlineRestaurantOrder(JObject oReader)
        {
            RestaurantOrder arcs_order = new RestaurantOrder();
            if (oReader["id"] != null)
            {
                arcs_order.Id = Convert.ToInt32(oReader["id"]);}
            if (oReader["user_id"] != null)
            {
                arcs_order.UserId = Convert.ToInt32(oReader["user_id"]);
            }
            if (oReader["customer_id"] != null)
            {arcs_order.CustomerId = Convert.ToInt32(oReader["customer_id"]);
            }
            if (oReader["restaurant_id"] != null)
            {
                arcs_order.RestaurantId = Convert.ToInt32(oReader["restaurant_id"]);
            }
            try
            {
                if (oReader["order_time"] != null)
                {
                   arcs_order.OrderTime = DateTime.Parse((oReader["order_time"]).ToString());
                }
            }
            catch (Exception ex)
            {

                arcs_order.OrderTime = DateTime.Now.Date;
            }
            try
            {
                if (oReader["delivery_time"] != null)
                {
                    arcs_order.DeliveryTime = Convert.ToDateTime(oReader["delivery_time"]);
                }
            }
            catch (Exception ex)
            {
                //arcs_order.DeliveryTime = null
            }
            try
            {
                if (oReader["payment_date"] != null)
                {
                    arcs_order.PaymentDate = DateTime.Parse((oReader["payment_date"]).ToString());
                }
            }
            catch (Exception ex)
            {
                arcs_order.PaymentDate = DateTime.Now.Date;
            }
            if (oReader["status"] != null)
            {
                arcs_order.Status = Convert.ToString(oReader["status"]);
            }
            if (oReader["payment_method"] != null)
            {
                arcs_order.PaymentMethod = Convert.ToString(oReader["payment_method"]);
            }
            if (oReader["payment_module"] != null)
            {
                arcs_order.PaymentModule = Convert.ToString(oReader["payment_module"]);
            }
            if (oReader["delivery_address"] != null)
            {
                arcs_order.DeliveryAddress = Convert.ToString(oReader["delivery_address"]);
            }
            if (oReader["comments"] != null)
            {
                arcs_order.Comments = Convert.ToString(oReader["comments"]);
            }
            if (oReader["delivery_cost"] != null)
            {
                arcs_order.DeliveryCost = Convert.ToDouble(oReader["delivery_cost"]);
            }
            if (oReader["vat"] != null)
            {
                arcs_order.Vat = Convert.ToDouble(oReader["vat"]);
            }
            if (oReader["total_cost"] != null)
            {
                arcs_order.TotalCost = Convert.ToDouble(oReader["total_cost"]);
            }
            if (oReader["person"] != null)
            {
                arcs_order.Person = Convert.ToInt32(oReader["person"]);
            }
            if (oReader["order_table"] != null)
            {
                arcs_order.OrderTable = Convert.ToInt32(oReader["order_table"]);
            }
            if (oReader["discount"] != null)
            {
                arcs_order.Discount = Convert.ToDouble(oReader["discount"]);
            }
            if (oReader["order_type"] != null)
            {
                arcs_order.OrderType = Convert.ToString(oReader["order_type"]);
            }
            if (oReader["order_status"] != null)
            {
                arcs_order.OrderStatus = Convert.ToString(oReader["order_status"]);
            }
            if (oReader["receipt"] != null)
            {
                arcs_order.Receipt = Convert.ToInt64(oReader["receipt"]);
            }
            if (oReader["comment"] != null)
            {
                arcs_order.Comment = Convert.ToString(oReader["comment"]);
            }
            if (oReader["cart_information"] != null)
            {
                arcs_order.CartInformation = Convert.ToString(oReader["cart_information"]);
            }
            if (oReader["customer_name"] != null)
            {
                arcs_order.CustomerName = Convert.ToString(oReader["customer_name"]);
            }
            if (oReader["coupon"] != null)
            {arcs_order.Coupon = Convert.ToString(oReader["coupon"]);
            }
            if (oReader["online_order"] != null)
            {
                arcs_order.OnlineOrder = Convert.ToInt64(oReader["online_order"]);
            }
            if (oReader["online_order_status"] != null)
            {
                arcs_order.OnlineOrderStatus = Convert.ToString(oReader["online_order_status"]);
            }
            if (oReader["customer_email"] != null)
            {
                arcs_order.CustomerEmail = Convert.ToString(oReader["customer_email"]);
            }
            if (oReader["card_fee"] != null)
            {
                arcs_order.CardFee = Convert.ToDouble(oReader["card_fee"]);
            }
            if (oReader["online_order_id"] != null)
            {
                arcs_order.OnlineOrderId = Convert.ToInt64(oReader["online_order_id"]);
            }
            if (oReader["cash_amount"] != null)
            {
                arcs_order.CashAmount = Convert.ToDouble(oReader["cash_amount"]);
            }
            if (oReader["card_amount"] != null)
            {
                arcs_order.CardAmount = Convert.ToDouble(oReader["card_amount"]);
            }
            if (oReader["driver_id"] != null)
            {
                arcs_order.DriverId = Convert.ToInt32(oReader["driver_id"]);
            }
            if (oReader["order_no"] != null)
            {
                arcs_order.OrderNo = Convert.ToInt32(oReader["order_no"]);
            }
            if (oReader["discount_amount"] != null)
            {
                arcs_order.DiscountAmount = Convert.ToDouble(oReader["discount_amount"]);
            }
            arcs_order.OnlineOrderId = arcs_order.Id;
            arcs_order.IsSync = 1;
            return arcs_order;
        }


        public OrderPayments GetOnlineOrderPayment(JToken oReader)
        {
            OrderPayments arcs_order = new OrderPayments();
            if (oReader["id"] != null)
            {
                arcs_order.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["order_id"] != null)
            {
                arcs_order.order_id = Convert.ToInt32(oReader["order_id"]);
            }
            if (oReader["booking_id"] != null)
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

            if (oReader["refund_amount"] != null)
            {
                arcs_order.refund_amount = Convert.ToDouble(oReader["refund_amount"]);
            }

            if (oReader["order_info"] != null)
            {
                arcs_order.order_info = Convert.ToString(oReader["order_info"]);
            }

            if (oReader["booking_amount"] != null)
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

        public List<RestaurantUsers> GetOnlineCustomer(JToken oReader)
        {
            List<RestaurantUsers> onlineCustomer = new List<RestaurantUsers>();
            RestaurantUsers arcs_users = new RestaurantUsers();
            if (oReader["id"] != null)
            {
                arcs_users.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["usertype"] != null)
            {
                arcs_users.Usertype = Convert.ToString(oReader["usertype"]);
            }
            if (oReader["firstname"] != null)
            {
                arcs_users.Firstname = Convert.ToString(oReader["firstname"]);
            }
            if (oReader["lastname"] != null)
            {
                arcs_users.Lastname = Convert.ToString(oReader["lastname"]);
            }
            if (oReader["email"] != null)
            {
                arcs_users.Email = Convert.ToString(oReader["email"]);
            }
            if (oReader["username"] != null)
            {
                arcs_users.Username = Convert.ToString(oReader["username"]);
            }
            if (oReader["password"] != null)
            {
                arcs_users.Password = Convert.ToString(oReader["password"]);
            }
            if (oReader["manage_password"] != null)
            {
                arcs_users.ManagePassword = Convert.ToString(oReader["manage_password"]);
            }
            if (oReader["house"] != null)
            {
                arcs_users.House = Convert.ToString(oReader["house"]);
            }
            if (oReader["address"] != null)
            {
                arcs_users.Address = Convert.ToString(oReader["address"]);
            }
            if (oReader["homephone"] != null)
            {
                arcs_users.Homephone = Convert.ToString(oReader["homephone"]);
            }
            if (oReader["workphone"] != null)
            {
                arcs_users.Workphone = Convert.ToString(oReader["workphone"]);
            }
            if (oReader["mobilephone"] != null)
            {
                arcs_users.Mobilephone = Convert.ToString(oReader["mobilephone"]);
            }
            if (oReader["city"] != null)
            {
                arcs_users.City = Convert.ToString(oReader["city"]);
            }
            if (oReader["county"] != null)
            {
                arcs_users.County = Convert.ToString(oReader["county"]);
            }
            if (oReader["postcode"] != null)
            {
                arcs_users.Postcode = Convert.ToString(oReader["postcode"]);
            }
            if (oReader["status"] != null)
            {
                arcs_users.Status = Convert.ToString(oReader["status"]);
            }
            if (oReader["activation_key"] != null)
            {
                arcs_users.ActivationKey = Convert.ToString(oReader["activation_key"]);
            }
            if (oReader["logged_in"] != null)
            {
                arcs_users.LoggedIn = Convert.ToInt64(oReader["logged_in"]);
            }
            if (oReader["mac_address"] != null)
            {
                arcs_users.MacAddress = Convert.ToString(oReader["mac_address"]);
            }
            try
            {
                if (oReader["last_activity"] != null)
                {
                    arcs_users.LastActivity = Convert.ToDateTime(oReader["last_activity"]);
                }
            }
            catch (Exception ex) { }

            if (oReader["can_manage"] != null)
            {
                arcs_users.CanManage = Convert.ToInt64(oReader["can_manage"]);
            }
            if (oReader["use_java"] != null)
            {
                arcs_users.UseJava = Convert.ToInt64(oReader["use_java"]);
            }
            if (oReader["check_online_order"] != null)
            {
                arcs_users.CheckOnlineOrder = Convert.ToInt64(oReader["check_online_order"]);
            }
            arcs_users.IsUpdate = 1;
            arcs_users.Name = arcs_users.Firstname + " " + arcs_users.Lastname;
            onlineCustomer.Add(arcs_users);
            
            return onlineCustomer;

        }

        public List<OrderPackage> GetOnlinePackage(JToken package)
        {
            List<OrderPackage> orderPackage = new List<OrderPackage>();
            foreach (var oReader in package)
            {
                OrderPackage arcs_order_package = new OrderPackage();
                if (oReader["id"] != null)
                {
                    arcs_order_package.Id = Convert.ToInt32(oReader["id"]);
                }
                if (oReader["order_id"] != null)
                {
                    arcs_order_package.OrderId = Convert.ToInt32(oReader["order_id"]);
                }
                if (oReader["package_id"] != null)
                {
                    arcs_order_package.PackageId = Convert.ToInt32(oReader["package_id"]);
                }
                if (oReader["name"] != null)
                {
                    arcs_order_package.Name = Convert.ToString(oReader["name"]);
                }
                if (oReader["quantity"] != null)
                {
                    arcs_order_package.Quantity = Convert.ToInt32(oReader["quantity"]);
                }
                if (oReader["price"] != null){
                    
                    //**** For Online order sum = PackagePrice + ExtraPrice 

                    arcs_order_package.Price = (Convert.ToDouble(oReader["price"]) * arcs_order_package.Quantity) + Convert.ToDouble(oReader["extra_price"]);
                }
                if (oReader["extra_price"] != null)
                {
                    arcs_order_package.Extra_price =0;
                }
                
                orderPackage.Add(arcs_order_package);

            }
            return orderPackage;
        }

        public List<OrderItem> GetOnlineItems(JToken items)
        {

            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in items)
            {
                OrderItem arcs_order_item = new OrderItem();
                if (item["id"] != null)
                {
                    arcs_order_item.Id = Convert.ToInt32(item["id"]);
                }
                if (item["order_id"] != null)
                {
                    arcs_order_item.OrderId = Convert.ToInt32(item["order_id"]);
                }
                if (item["recipe_id"] != null)
                {
                    arcs_order_item.RecipeId = Convert.ToInt32(item["recipe_id"]);
                }
                if (item["package_id"] != null)
                {
                    arcs_order_item.PackageId = Convert.ToInt32(item["package_id"]);
                }
                if (item["name"] != null)
                {
                    arcs_order_item.Name = Convert.ToString(item["name"]);
                }
                if (item["quantity"] != null)
                {
                    arcs_order_item.Quantity = Convert.ToInt32(item["quantity"]);
                }
                if (item["price"] != null)
                {
                    arcs_order_item.Price = Convert.ToDouble(item["price"]) * arcs_order_item.Quantity;
                }
                if (item["extra_price"] != null)
                {
                    arcs_order_item.ExtraPrice = Convert.ToDouble(item["extra_price"]);
                }
                if (item["sent_to_kitchen"] != null)
                {
                    arcs_order_item.SentToKitchen = Convert.ToInt32(item["sent_to_kitchen"]);
                }
                if (item["kitchen_processing"] != null)
                {
                    arcs_order_item.KitchenProcessing = Convert.ToInt32(item["kitchen_processing"]);
                }
                if (item["kitchen_done"] != null)
                {
                    arcs_order_item.KitchenDone = Convert.ToInt32(item["kitchen_done"]);
                }
                try
                {
                    if (item["last_modify_time"] != null)
                    {
                        arcs_order_item.LastModifyTime = DateTime.Now.Date;
                    }
                }
                catch (Exception ex)
                {
                    arcs_order_item.LastModifyTime = DateTime.Now.Date;
                }
                if (item["options"] != null)
                {
                     arcs_order_item.Options = item["options"].ToString();
                }
                //if (item["options"] != null)
                //{
                //    arcs_order_item.Options = Convert.ToString(item["options"]);
                //    //if (arcs_order_item.Options.Contains('+'))
                //    //{
                //    //    arcs_order_item.Options = arcs_order_item.Options.Replace('+', ',');
                //    //}
                //}
                try
                {
                    if (item["options_minus"] != null)
                    {
                        arcs_order_item.MinusOptions = Convert.ToString(item["options_minus"]);
                        if (arcs_order_item.MinusOptions.Contains('+'))
                        {
                            arcs_order_item.MinusOptions = arcs_order_item.MinusOptions.Replace('+', ',');
                        }
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (item["multiple_menu"] != null)
                    {
                        arcs_order_item.MultipleMenu = Convert.ToString(item["multiple_menu"]);
                    }
                }
                catch (Exception)
                {


                }
                if (item["order_package_id"] != null)
                {
                    arcs_order_item.orderPackageId = Convert.ToInt32(item["order_package_id"]);
                }
                orderItems.Add(arcs_order_item);

            }

            return orderItems;
        }

    }
}
