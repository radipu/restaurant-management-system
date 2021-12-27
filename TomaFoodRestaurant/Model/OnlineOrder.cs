using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.DAL.CommonMethod;

namespace TomaFoodRestaurant.Model
{
    public class OnlineOrder
    {
       
          public void SaveOnlineOrder(List<OrderItem> orderItems, List<OrderPackage> orderPackage, List<RestaurantUsers> onlineCustomer, List<RestaurantOrder> onlineOrder)
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            
           foreach (RestaurantOrder order in onlineOrder)
            {
                order.IsSync = 1;
                bool checkOrder = CheckOnlineOrder(order);
                if (checkOrder) continue;

                RestaurantUsers customer = null;
                if (order.CustomerId > 0)
                {
                    customer = onlineCustomer.FirstOrDefault(a => a.Id == order.CustomerId);

                    //if (customer.Mobilephone != "")
                    //{
                    //    phnTrack = "mobilephone";
                    //    cellNumber = customer.Mobilephone;
                    //}
                    //else if (customer.Homephone != "")
                    //{
                    //    phnTrack = "homephone";
                    //    cellNumber = customer.Homephone;
                    //}

                    customer.GcmRegId = "";
                    try
                    {
                        //                    RestaurantUsers user = aCustomerBll.GetRestaurantCustomerByHomePhone(cellNumber, phnTrack);
                        RestaurantUsers user = aCustomerBll.GetResturantCustomerByCustomerId(customer.Id);

                        if (user != null && user.Id > 0)
                        {
                            customer.Id = user.Id;
                            customer.IsUpdate = 1;
                            int res = aCustomerBll.UpdateRestaurantCustomer(customer);
                            order.CustomerId = customer.Id;
                        }
                        else
                        {
                            customer.IsUpdate = 1;
                            //customer.Id = 
                            int res = aCustomerBll.InsertNewCustomer(customer);
                            //    int res = aCustomerBll.InsertRestaurantCustomer(customer);
                            order.CustomerId = res;
                        }

                    }
                    catch (Exception ex)
                    {

                        ErrorReportBLL errorReport = new ErrorReportBLL();
                        errorReport.SendErrorReport(ex.GetBaseException().ToString());
                        Console.WriteLine(ex.GetBaseException() + " " + DateTime.Now.TimeOfDay);

                    }
                }

                List<OrderItem> aOrderItems = orderItems.Where(a => a.OrderId == order.Id).ToList();
                List<OrderPackage> aOrderPackage = orderPackage.Where(a => a.OrderId == order.Id).ToList();

                DateTime stDate = DateTime.Now.Date;
                DateTime endDate = stDate.AddDays(1);


                int orderNo = aRestaurantOrderBLL.GetMaxOrderNumber(stDate, endDate);
                order.OrderNo = orderNo;

                int result = aRestaurantOrderBLL.InsertRestaurantOrder(order);
                aOrderItems.ForEach(b => b.OrderId = result);
                aOrderPackage.ForEach(a => a.OrderId = result);
                
                //var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);
                //if (result2 != null)
                //orderItem.orderPackageId = packageIds.Where(i => i.Key == itemDetails.OptionsIndex).Select(i => i.Value).First();
                //List<OrderItem> aOrderItems = GetOrderItem(result, result2);
                //int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);

                var orderPackageIds = aRestaurantOrderBLL.InsertRestaurantOrderPackage(aOrderPackage);
               List<OrderItem> aaOrderItems = new List<OrderItem>();
               foreach (OrderItem orderItem in aOrderItems)
               {
                   if (orderItem.orderPackageId > 0)
                   {
                       orderItem.orderPackageId = orderPackageIds.Where(a => a.Key == orderItem.orderPackageId).Select(i => i.Value).First();
                   }
                   aaOrderItems.Add(orderItem);
               }

                aRestaurantOrderBLL.InsertRestaurantOrderItem(aaOrderItems);
                Console.WriteLine("Order succefully added :" );

                if(order.OrderType.ToLower() == "in")
                {
                    RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                    RestaurantOrder aRestaurantOrder = aVariousMethod.GetRestaurantOrderByOrderId(result);

                    aRestaurantOrderBLL.UpdateOnlineOrder(Convert.ToInt32(Convert.ToInt32(aRestaurantOrder.OnlineOrderId)), "accepted");

                    if (aRestaurantOrder.OrderTable > 0)
                    {
                        var aRestaurantTableBll = new RestaurantTableBLL();
                        RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(Convert.ToInt32(aRestaurantOrder.OrderTable));
                        if (aRestaurantTable.CurrentStatus == "available")
                        {
                            aRestaurantTable.Person = aRestaurantOrder.Person;
                            string date = DateTime.Now.ToString();
                            aRestaurantTable.UpdateTime = DateTime.Now;
                            aRestaurantTable.CurrentStatus = "busy";
                            aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                        }
                    }
                   // autoPrint(aRestaurantOrder);
                }
            }
        }

       private bool CheckOnlineOrder(RestaurantOrder order)
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantOrder rcsOrder = aRestaurantOrderBLL.GetRestaurantOrderByOnlineOrder(order.OnlineOrderId);
            if (rcsOrder != null && rcsOrder.OnlineOrderId > 0) 
                return true;
              return false;

        }



        public static void fireBaseOrderAsync(string res_url)
        {
            var client = new FirebaseClient(GlobalVars.firebaseUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(GlobalVars.firebaseAuth)
            });

            try
            {
                var observable = client
               .Child(res_url)
               .Child("orders")
               .AsObservable<object>();
                observable.Subscribe(d => new OnlineOrder().GetOnlineOrder($"{d.Object.ToString()}"));
                Console.WriteLine("Background Process " + string.Format(DateTime.Now.ToString()));
            }
            catch (Exception ex)
            {
                GlobalVars.isOnlineOrderTimer = true;
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
                //Console.WriteLine("Background Rerun " + string.Format(DateTime.Now.ToString()));
                //await fireBaseOrderAsync(res_url);
            }
        }


        public static async Task manualGetOnlineOrder() {

            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            
            var firebase = new FirebaseClient(GlobalVars.firebaseUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(GlobalVars.firebaseAuth)
            });

            var orders = await firebase
               .Child(aRestaurantInformation.Url)
               .Child("orders")
               .OnceAsync<object>();

            foreach (var order in orders)
            {
                new OnlineOrder().GetOnlineOrder($"{order.Object.ToString()}");
            }

        }
       

        public void GetOnlineOrder(string text = "")
        {
           
            try
            {
              

                if (!(text.Contains("accepted") || text.Contains("rejected") || text.Contains("\"id\": 0")))
                {

                    RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                    RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                    string json = "[" + text + "]";
                    dynamic objects = JArray.Parse(json);
                    List<OrderItem> orderItems = new List<OrderItem>();
                    List<OrderPackage> orderPackage = new List<OrderPackage>();
                    List<RestaurantUsers> onlineCustomer = new List<RestaurantUsers>();
                    List<RestaurantOrder> onlineOrder = new List<RestaurantOrder>();
                    OnlineOrderBLL aOnlineOrderBll = new OnlineOrderBLL();
                    foreach (JObject ject in objects)
                    {
                        RestaurantOrder order = aOnlineOrderBll.GetOnlineRestaurantOrder(ject);
                        onlineOrder.Add(order);

                        if (@ject["payments"] != null)
                        {
                            var orderPayment = ject["payments"];
                            OrderPayments order_payment = aOnlineOrderBll.GetOnlineOrderPayment(orderPayment);
                            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                            aRestaurantOrderDao.InsertOrderPayment(order_payment);
                        }



                        if (@ject["order_package"] != null)
                        {
                            var package = ject["order_package"];
                            List<OrderPackage> packages = aOnlineOrderBll.GetOnlinePackage(package);
                            orderPackage.AddRange(packages);
                        }
                        var item = ject["order_items"];
                        List<OrderItem> items = aOnlineOrderBll.GetOnlineItems(item);
                        orderItems.AddRange(items);

                        var customer = ject["customer_info"];
                        if (customer != null)
                        {                            
                            List<RestaurantUsers> customers = aOnlineOrderBll.GetOnlineCustomer(customer);
                            onlineCustomer.AddRange(customers);
                        }
                    }
                    if (onlineOrder.Count > 0)
                    {
                        if (onlineOrder.Count > 0)
                        {
                            SaveOnlineOrder(orderItems, orderPackage, onlineCustomer, onlineOrder);


                         

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



        }
    }
}
