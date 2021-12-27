using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Newtonsoft.Json;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using System.Net;
using System.IO;
using TomaFoodRestaurant.OtherForm;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using TomaFoodRestaurant.DAL.DAO;
using Mailjet.Client.TransactionalEmails;

namespace TomaFoodRestaurant.DAL.CommonMethod
{
 public   class CommonOrderDetials
    {
        RestaurantOrder aRestaurantOrder = new RestaurantOrder();
        List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static List<OrderItemDetailsMD> aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
        public static List<RecipePackageMD> aRecipePackageMdList = new List<RecipePackageMD>();
        public static List<PackageItem> aPackageItemMdList = new List<PackageItem>();
        public static List<RecipeMultipleMD> aRecipeMultipleMdList = new List<RecipeMultipleMD>();
        public static List<MultipleItemMD> aMultipleItemMdList = new List<MultipleItemMD>();

        GeneralInformation aGeneralInformation = new GeneralInformation();
        RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        public static int OrderId = 0;
        List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
        PrintCopySetup aPrintCopySetup = new PrintCopySetup();
        RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
        public List<int> ItemIds = new List<int>();

        string font_size = "16";

        public CommonOrderDetials(RestaurantOrder saveOrder)
        {
            aRestaurantOrder = saveOrder;
            urls = new GlobalUrlBLL().GetUrls();

        }

        GlobalUrl urls=new GlobalUrl();

        private string GetRejectMessage()
        {
            string sr = "Deal is unavailable.";
            return sr;
        }

        public async Task<string> RejectOrderAsync(RestaurantInformation aRestaurantInformation, bool firebase = true)
        {
            try
            {
                if (firebase)
                {
                    RejectCauseForm.status = "";
                    RejectCauseForm.message = "";
                    RejectCauseForm aRejectCauseForm = new RejectCauseForm();
                    aRejectCauseForm.causesTextBox.Text = GetRejectMessage();
                    aRejectCauseForm.ShowDialog();
                    if (RejectCauseForm.status == "ok")
                    {
                        if (RejectCauseForm.refund)
                        {
                            Refund refundFrom = new Refund(aRestaurantOrder.TotalCost, (int)aRestaurantOrder.OnlineOrderId);
                            refundFrom.ShowDialog();

                        }

                        EmailSettings newSetting = new EmailSettings();
                        newSetting.sender_email = "no-reply@tomafood.net";
                        newSetting.sender_name = aRestaurantInformation.RestaurantName;

                        MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                        RestaurantUsers aRestaurantUser = aCustomerDAO.GetResturantCustomerByCustomerId(aRestaurantOrder.CustomerId);
                        newSetting.to_email = aRestaurantUser.Email;
                        newSetting.to_name = aRestaurantUser.Name;
                        newSetting.order_type = aRestaurantOrder.OrderType;
                        newSetting.subject = "Sorry! Order unsuccessful";
                        newSetting.msg = "<p>Hi " + newSetting.to_name + "</p><p>Unfortunately, we are unable to process your order at this time for the following reason.</p><p>" + RejectCauseForm.message + "</p><p>Sorry for any inconvenience caused.</p></p><p>For any query please contact on " + GlobalSetting.RestaurantInformation.Phone + ".</p><p>Thanks again.</p><p>Kind Regards<br> " + GlobalSetting.RestaurantInformation.RestaurantName + "<br> " + GlobalSetting.RestaurantInformation.Website + "</p>";

                        await SendEmail.send(newSetting);
                        await FireBaseOrderAcceptOrReject(aRestaurantInformation.Url, Convert.ToInt32(aRestaurantOrder.OnlineOrderId), "rejected");
                        string res = aRestaurantOrderBLL.DeleteOrderByOrderId(Convert.ToInt32(aRestaurantOrder.Id));
                        if (res == "Yes")
                        {
                            return res;
                        }
                    }
                }
                else
                {
                    RejectCauseForm.status = "";
                    RejectCauseForm.message = "";
                    RejectCauseForm aRejectCauseForm = new RejectCauseForm();
                    aRejectCauseForm.causesTextBox.Text = GetRejectMessage();
                    aRejectCauseForm.ShowDialog();
                    if (RejectCauseForm.status == "ok")
                    {
                        string postData = "reject_cause=" + RejectCauseForm.message;
                        string url = urls.AcceptUrl + "restaurantcontrol/request/crud/reject_online_order/" + aRestaurantInformation.Id + "/" + +Convert.ToInt32(aRestaurantOrder.OnlineOrderId);
                        string result = SendOnlineOrderStatus(Convert.ToInt32(aRestaurantOrder.OnlineOrderId), postData, url);
                        if (result == Convert.ToInt32(aRestaurantOrder.OnlineOrderId).ToString())
                        {
                            string res = aRestaurantOrderBLL.DeleteOrderByOrderId(Convert.ToInt32(aRestaurantOrder.Id));
                            if (res == "Yes")
                            {
                                return res;
                            }
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                new ErrorReportBLL().SendErrorReport(exception.GetBaseException().ToString());
            }


            return "No";
        }

        

        public static async Task FireBaseOrderAcceptOrReject(string url,int order_id,string status = "accepted")
        {
            var client = new FirebaseClient(GlobalVars.firebaseUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(GlobalVars.firebaseAuth)
            });
            try
            {
                var observable = client
                  .Child(url)
                  .Child("orders")                  
                  .Child("order-" + order_id.ToString())
                  .Child("online_order_status")
                  .PutAsync<string>(status);
            }
            catch(Exception ex)
            {

            }
        
        }

        public async Task AcceptOrderAsync(RestaurantInformation aRestaurantInformation,string time,bool firebase = true)
        {
  
         string postData = "deliver_in=" + time;

         try{
                if (firebase) {

                    //if customer id is in the order send email, IN order otherwise
                    if (aRestaurantOrder.CustomerId > 0) 
                    {
                        EmailSettings newSetting = new EmailSettings();
                        newSetting.sender_email = "no-reply@tomafood.net";
                        newSetting.sender_name = aRestaurantInformation.RestaurantName;

                        MySqlCustomerDAO aCustomerDAO = new MySqlCustomerDAO();
                        RestaurantUsers aRestaurantUser = aCustomerDAO.GetResturantCustomerByCustomerId(aRestaurantOrder.CustomerId);

                        newSetting.to_email = aRestaurantUser.Email;
                        newSetting.to_name = aRestaurantUser.Name;
                        newSetting.order_type = aRestaurantOrder.OrderType;
                        newSetting.subject = "Order Confirmation";
                        if (time == "")
                        {
                            //DateTime aTime = DateTime.Now.AddMinutes(aRestaurantInformation.DeliveryTime);

                            newSetting.msg = "<p>Hi " + newSetting.to_name + ",</p><p>Thank you for placing your order with us today.</p><p>Your order has been received and will be ready to " + (newSetting.order_type == "CLT" ? "collect" : "deliver") + " ASAP.</p><p>For any query please contact on " + GlobalSetting.RestaurantInformation.Phone + ".</p><p>Thanks again.</p><p>Kind Regards<br> " + GlobalSetting.RestaurantInformation.RestaurantName + "<br> " + GlobalSetting.RestaurantInformation.Website + "</p>";

                        }
                        else
                        {
                            newSetting.msg = "<p>Hi " + newSetting.to_name + ",</p><p>Thank you for placing your order with us today.</p><p>Your order has been received and will be ready to " + (newSetting.order_type == "CLT" ? "collect" : "deliver") + " at " + time + " approximately.</p><p>For any query please contact on " + GlobalSetting.RestaurantInformation.Phone + ".</p><p>Thanks again.</p><p>Kind Regards<br> " + GlobalSetting.RestaurantInformation.RestaurantName + "<br> " + GlobalSetting.RestaurantInformation.Website + "</p>";

                        }

                        await SendEmail.send(newSetting);
                    }
                    
                    await FireBaseOrderAcceptOrReject(aRestaurantInformation.Url,Convert.ToInt32(aRestaurantOrder.OnlineOrderId));
                    aRestaurantOrderBLL.UpdateOnlineOrder(Convert.ToInt32(Convert.ToInt32(aRestaurantOrder.OnlineOrderId)), "accepted");

                    if(aRestaurantOrder.OrderTable > 0)
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
                }
                else
                {
                    string url = urls.AcceptUrl + "restaurantcontrol/request/crud/accept_online_order/" + aRestaurantInformation.Id + "/" + Convert.ToInt32(aRestaurantOrder.OnlineOrderId);
                    string result = SendOnlineOrderStatus(Convert.ToInt32(aRestaurantOrder.OnlineOrderId), postData, url);

                    if (result == Convert.ToString(aRestaurantOrder.OnlineOrderId))
                    {
                        string res = aRestaurantOrderBLL.UpdateOnlineOrder(Convert.ToInt32(result), "accepted");
                        if (res == "Yes")
                        {
                          //  return res;

                        }

                    }

                }
            

            
         }
         catch (Exception ex)
         {

               // return "Failed";

         }
       
       
     }



        //public void AcceptOrder(RestaurantInformation aRestaurantInformation, string time, bool firebase = false)
        //{

        //    string postData = "deliver_in=" + time;

        //    try
        //    {
        //        if (firebase)
        //        {

        //           // await sendEmailByMailjet("no-reply@tomafood.net", "muzahid.kus.ban@gmail.com", "", "test email");

        //        }
        //        else
        //        {
        //            string url = urls.AcceptUrl + "restaurantcontrol/request/crud/accept_online_order/" + aRestaurantInformation.Id + "/" + Convert.ToInt32(aRestaurantOrder.OnlineOrderId);
        //            string result = SendOnlineOrderStatus(Convert.ToInt32(aRestaurantOrder.OnlineOrderId), postData, url);

        //            if (result == Convert.ToString(aRestaurantOrder.OnlineOrderId))
        //            {
        //                string res = aRestaurantOrderBLL.UpdateOnlineOrder(Convert.ToInt32(result), "accepted");
        //                if (res == "Yes")
        //                {
        //                //    return res;

        //                }

        //            }

        //        }

        //       /// return result;
        //    }
        //    catch (Exception ex)
        //    {
        //      //  return "Failed";

        //    }


        //}


        private string SendOnlineOrderStatus(int onlineOrderId, string data, string url)
        {
         string postData = data;
         byte[] byteArray = Encoding.UTF8.GetBytes(postData);

         // Post the data to the right place.
         Uri target = new Uri(url);
         WebRequest request = WebRequest.Create(target);

         request.Method = "POST";
         request.ContentType = "application/x-www-form-urlencoded";
         request.ContentLength = byteArray.Length;

         using (var dataStream = request.GetRequestStream())
         {
             dataStream.Write(byteArray, 0, byteArray.Length);
         }

         string result = string.Empty;
         using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
         {
             using (Stream responseStream = response.GetResponseStream())
             {
                 using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                 {
                     result = readStream.ReadToEnd();
                 }
             }
         }

         return result;
     }
     private static string GetOrdinalSuffix(int num)
     {
         if (num.ToString().EndsWith("11")) return "th";
         if (num.ToString().EndsWith("12")) return "th";
         if (num.ToString().EndsWith("13")) return "th";
         if (num.ToString().EndsWith("1")) return (" " + num + "st");
         if (num.ToString().EndsWith("2")) return (" " + num + "nd");
         if (num.ToString().EndsWith("3")) return (" " + num + "rd");
         return (" " + num + "th");
     }


        private string GenerateDetails_running(int result)
        {
            string printHeaderStr = "<table width='250' style='text-align:center'>";

            int papersize = 25;
            string reciept_font = "";

            RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
            RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            RestaurantTable aRestaurantTable = new RestaurantTableBLL().GetRestaurantTableByTableId(aGeneralInformation.TableId);

            aGeneralInformation.TableNumber = aRestaurantTable.Name;
            int blankLine = 0;
            int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");

            reciept_font = aRestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
            string reciept_font_exlgr = "26";

            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

            if (aRestaurantInformation.RecieptOption == "logo_title")
            {
                string path = @"Image/" + aRestaurantInformation.Id + "_website_logo.png";
                if (File.Exists(path))
                {
                    var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(path)));
                    printHeaderStr += "<tr><td colspan='2' style='text-align:center'><img style='width:120px; height: 60px' src='" + imageString + "'></td></tr>";
                }
            }
            string dashLine = "-------------------------------------";

            if (aRestaurantInformation.RecieptOption != "none")
            {
                printHeaderStr += "<tr><td colspan='2' style='text-align:center'><b>" +
                                     aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" +
                                     aRestaurantInformation.House + ", " + aRestaurantInformation.Address + "<br>" +
                                     aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" +
                                     aRestaurantInformation.VatRegNo + "</td></tr>";
            }
            printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
            string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation);
            printHeaderStr += "<tr><td colspan='2' style='text-align:center;font-size:" + reciept_font_lgr + "px'><b>" + orderHistory + "</b></td></tr>";
            printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
            printHeaderStr += "<tr><td colspan='2'><span style='text-align:center;'>" + DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</span></td></tr>";
            printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
            RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
            if (aORder.CustomerId > 0)
            {
                printHeaderStr += "<tr><td colspan='2' style='text-align:left'>";
                printHeaderStr += "<span style='text-align:left;font-weight:bold; font-size:" + reciept_font + "px'>";
                RestaurantUsers aUser = aCustomerBll.GetResturantCustomerByCustomerId(aORder.CustomerId);
                printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";
                string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;
                string address = "";
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {

                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        if (ss.Count() > 0)
                        {
                            address += "," + ss[0];
                        }
                        if (ss.Count() > 1)
                        {
                            address += ", " + ss[1];
                        }
                        if (ss.Count() > 2)
                        {
                            address += "<br>" + ss[2];
                        }
                        if (ss.Count() > 3)
                        {
                            address += ", " + ss[3];
                        }
                    }

                }


                if (aORder.OrderType == "DEL")
                {
                    if (address == "")
                    {
                        if (string.IsNullOrEmpty(aUser.FullAddress))
                        {
                            address += "" + aUser.House + " " + aUser.Address;
                            address += "," + aUser.City + "<br>" + aUser.Postcode;
                        }
                        else
                        {

                            address += "" + aUser.House + "," + aUser.FullAddress + "<br>" + aUser.Postcode;
                        }
                    }
                }

                printHeaderStr += "</span></td></tr>";


                if (!flag)
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:left;font-size:" + reciept_font + "px'>" + address + "<br/>" + cell + "</td></tr>";
                }
                else
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:left;font-size:" + reciept_font + "px'>" + address + "</td></tr>";
                }
                printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
            }
            printHeaderStr += "</table>";

            string printStr = "";
            string printFooterStr = "";

            //List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);

            aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
            aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

            string printRecipeStr = "<table    width='250'   align='left'>";
            int catId = 0;
            bool startdas = false;
            foreach (OrderItemDetailsMD itemDetails in aOrderItemDetailsMDList)
            {

                if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                {
                    printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                    startdas = false;
                }

                if (aRestaurantInformation.MenuSeparation == 1 && catId != itemDetails.CategoryId && catId != 0)
                {
                    printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";

                }

                string recipe_name = itemDetails.ItemName;

                if (aORder.OnlineOrder > 0)
                {
                    recipe_name = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);
                    if (recipe_name == "")
                        recipe_name = itemDetails.ItemName;
                }
                printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font + "px'>" + itemDetails.Qty + " " + getStringForPrint(recipe_name) + "</td><td align='right'>" + (itemDetails.Qty * itemDetails.Price).ToString("F02") + "</td></tr>";


                blankLine++;
                string options = "";
                List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                if (aOption.Count > 0)
                {

                    foreach (RecipeOptionMD option in aOption)
                    {
                        //if (!string.IsNullOrEmpty(option.Title))
                        //{
                        //    printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→" + option.Title + "</span><span style='text-align:right;float:right;width:22%;'>" + "" + "</span></h3>";
                        //    blankLine++;
                        //}
                        //if (!string.IsNullOrEmpty(option.MinusOption))
                        //{
                        //    printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→No" + option.MinusOption + "</span><span style='text-align:right;float:right;width:22%;'>" + "" + "</span></h3>";
                        //    blankLine++;
                        //}
                        if (!string.IsNullOrEmpty(option.Title))
                        {
                            printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                              reciept_font_small +
                                              "px'><span style='padding-left:10px'>" +
                                              "→" + getStringForPrint(option.Title) + "</span></td><td align='right'><span style='text-style:italic'>&nbsp;</span>" +
                                              "</td></tr>";
                            blankLine++;
                        }
                        if (!string.IsNullOrEmpty(option.MinusOption))
                        {
                            printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                              reciept_font_small + "px'><span style='padding-left:10px'>" +
                                              getStringForPrint("→No" + option.MinusOption) +
                                             "</span></td><td>&nbsp;</td></tr>";
                            blankLine++;
                        }

                    }
                }


                if (aRestaurantInformation.MenuSeparation == 2)
                {
                    printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                }


                catId = itemDetails.CategoryId;
                if (starterId == itemDetails.CategoryId)
                {
                    startdas = true;
                }



            }

            foreach (RecipePackageMD package in aRecipePackageMdList)
            {

                printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font + "px'>" +
                                              package.Qty.ToString() + " " + getStringForPrint(package.PackageName) +
                                              "</td><td align='right'>" +
                                              (package.Qty * package.UnitPrice).ToString("F02") + "</td></tr>";
                blankLine++;
                List<PackageItem> aPaItem =
                    aPackageItemMdList.Where(
                        a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                        .ToList();
                List<PackageItem> aPaItemNew = new List<PackageItem>();
                foreach (PackageItem item in aPaItem)
                {
                    item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                    aPaItemNew.Add(item);
                }
                aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                foreach (PackageItem itemDetails in aPaItem)
                {
                    string packageItemPrice = itemDetails.Price > 0
                        ? (itemDetails.Price).ToString("F02")
                        : "";

                    printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                      "px'>" +
                                      itemDetails.Qty.ToString() + "  " + getStringForPrint(itemDetails.ItemName) +
                                      "</td><td align='right'>" +
                                      packageItemPrice + "</td></tr>";
                    blankLine++;
                    string options = "";
                    List<RecipeOptionMD> aOption =
                        aRecipeOptionMdList.Where(
                            a => a.RecipeId == itemDetails.ItemId && a.RecipeOPtionItemId == itemDetails.Id)
                            .ToList();
                    if (aOption.Count > 0)
                    {

                        foreach (RecipeOptionMD option in aOption)
                        {
                            if (!string.IsNullOrEmpty(option.Title))
                            {
                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                  getStringForPrint("→" + option.Title) +
                                                  "</span></td><td align='right'>" + option.Price + "</td></tr>";
                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {
                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                  reciept_font_small +
                                                  "px'><span style='text-align:left;padding-left:10px'>" +
                                                  getStringForPrint("→No" + option.MinusOption) +
                                                  "</span></td><td></td></tr>";
                                blankLine++;
                            }

                        }
                    }


                }


            }


            foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
            {

                printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font +
                                                   "px'>" +
                                                   package.Qty.ToString() + " " + getStringForPrint(package.MultiplePartName) +
                                                   "</td><td align='right'>" +
                                                   (package.Qty * package.UnitPrice).ToString("F02") + "</td></tr>";
                blankLine++;
                List<MultipleItemMD> aPaItem =
                    aMultipleItemMdList.Where(
                        a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                        .ToList();
                int cnt = 0;
                foreach (MultipleItemMD itemDetails in aPaItem)
                {
                    cnt++;
                    string packageItemPrice = itemDetails.Price > 0
                        ? (itemDetails.Price).ToString()
                        : "";

                    printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                      "px'>" +
                                      (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                      getStringForPrint(itemDetails.ItemName) +
                                      "</td><td></td></tr>";
                    blankLine++;
                    string options = "";
                    List<RecipeOptionMD> aOption =
                        aRecipeOptionMdList.Where(
                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                            .ToList();
                    if (aOption.Count > 0)
                    {

                        foreach (RecipeOptionMD option in aOption)
                        {
                            if (!string.IsNullOrEmpty(option.Title))
                            {
                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                  reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                  getStringForPrint(option.Title) +
                                                  "</span></td><td></td></tr>";
                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {
                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                  reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                  getStringForPrint("→No" + option.MinusOption) +
                                                  "</span></td><td></td></tr>";
                                blankLine++;
                            }
                        }
                    }


                }

            }



            double amount = aORder.TotalCost;


            if (blankLine < aRestaurantInformation.RecieptMinHeight)
            {
                for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                {
                    printRecipeStr += "<tr><td>&nbsp;</td><td>&nbsp;</td></tr>";
                }
            }


            if (aORder.DiscountAmount > 0)
            {
                printFooterStr += "<tr><td   colspan='2'  style='font-weight:bold; font-size:" + reciept_font + "px;text-align:right;'>Discount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + aORder.DiscountAmount.ToString("F02") + "</td></tr>";
            }



            if (aGeneralInformation.CardFee > 0)
            {
                printFooterStr += "<tr><td   colspan='2'  align='right' style='font-weight:bold;text-align:right; font-size:" + reciept_font + "px'>S/C &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + aGeneralInformation.CardFee.ToString("F02") + "</td></tr>";
            }

            if (aGeneralInformation.DeliveryCharge > 0)
            {
                printFooterStr += "<tr><td  colspan='2' align='right' style='font-weight:bold;text-align:right; font-size:" + reciept_font + "px'>D/C &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + aGeneralInformation.DeliveryCharge.ToString("F02") + "</td></tr>";
            }

            printFooterStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>" +
                    "<tr><td  colspan='2' style='font-weight:bold;font-size:" + reciept_font + "px;text-align:right;'>" + aRestaurantOrder.OrderTime.ToString("hh:mmtt") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                    "TOTAL&nbsp;£" + amount.ToString("F02") + "</td></tr>";

            printFooterStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";


            if (aORder.CardAmount > 0)
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    printFooterStr += "<tr><td colspan='2' style='text-align:center;'><span style='font-weight:bold;font-size:" + reciept_font_exlgr + "px;text-align:center'> PAID BY CARD </span></td></tr>";
                }
                else
                {
                    printFooterStr += "<tr><td colspan='2' style='text-align:center;'><span style='font-weight:bold;font-size:" + reciept_font_exlgr + "px;text-align:center'> CARD ORDER </span></td></tr>";

                }
            }
            else
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    printFooterStr += "<tr><td colspan='2' style='text-align:center;'><span style='font-weight:bold;font-size:" + reciept_font_exlgr + "px;text-align:center'> PAID ORDER </span></td></tr>";

                }
                else
                {
                    printFooterStr += "<tr><td colspan='2' style='text-align:center;'><span style='font-weight:bold;font-size:" + reciept_font_exlgr + "px;text-align:center'> ORDER IS NOT PAID </span></td></tr>";

                }

            }


            if (aORder.Comment.Length > 3)
            {
                printFooterStr += "<tr><td style='text-align:center;' colspan='2'>" + aORder.Comment +
                                             "</td></tr>";
            }

            if (aRestaurantInformation.ThankYouMsg.Length > 5)
            {
                printFooterStr += "<tr><td style='font-weight:bold;text-align:center;font-size:" + reciept_font + "px' colspan='2'>" + aRestaurantInformation.ThankYouMsg +
                                           "</td></tr>";

            }

            if (aRestaurantInformation.ShowOrderNumber > 0)
            {

                printFooterStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" + font_size + "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                printFooterStr += "<tr><td style='font-weight:bold;text-align:center;font-size:30px' colspan='2'>" + aRestaurantOrder.OrderNo.ToString("D2") + "</td></tr>";

            }







            int printCopy = 1;
            printStr = printHeaderStr + printRecipeStr + printFooterStr + "</table>";
            //string str = "<html><head><meta charset='UTF-8'><style>html, body ,h3, h2, h4 , span, b { padding: 0; margin: 0; } td{min-width:40px;} td{line-height:15px;} td:nth-child(2){text-align: right;float: left;width:70px;}</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" + printStr + "</body></html>";

            string str = "<html><head><meta charset='UTF-8'><style>html, body ,h3, h2, h4 , span, b { padding: 0; margin: 0; } td{min-width:80px;line-height:15px;font-weight:bold;font-size:" + reciept_font + "px;}   td{line-height:15px;min-width:80px;} td:nth-child(2){text-align: right;float: left;width:80px;font-weight:bold;}</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;width:250px;'>" + printStr + "</body></html>";

            return str;



        }


        public OrderItemMerged getExistingOrderItem(List<OrderItemMerged> orderItems, OrderItemDetailsMD orderItem = null, PackageItem packageItem = null)
        {
            var existingItem = new OrderItemMerged();
            if (orderItem != null)
            {
                existingItem = orderItems.Where(a => a.ItemId == orderItem.ItemId && a.ItemName == orderItem.ItemName).SingleOrDefault();
                if (existingItem != null)
                {
                    //check if options is there
                    List<RecipeOptionMD> aOptions = aRecipeOptionMdList.Where(a => a.RecipeId == orderItem.ItemId &&
                                           a.OptionsIndex == orderItem.OptionsIndex).ToList();
                    //option is there we cannot increase the qty
                    if (aOptions.Count > 0)
                    {
                        return null;
                    }
                }
            }
            else
            {
                existingItem = orderItems.Where(a => a.ItemId == packageItem.ItemId && a.ItemName == packageItem.ItemName).SingleOrDefault();
                //check if options is there
                List<RecipeOptionMD> aOption =
                                        aRecipeOptionMdList.Where(
                                            a => a.RecipeId == packageItem.ItemId && a.OptionsIndex == packageItem.OptionsIndex && a.PackageItemOptionsIndex == packageItem.PackageItemOptionsIndex)
                                            .ToList();
                if (aOption.Count > 0)
                {
                    return null;
                }
            }


            return existingItem;
        }



        public string GenerateDetails(int result)
        {

            int papersize = 25;
            string reciept_font = "";

            RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
            RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            RestaurantTable aRestaurantTable = new RestaurantTableBLL().GetRestaurantTableByTableId(aGeneralInformation.TableId);

            aGeneralInformation.TableNumber = aRestaurantTable.Name;
            int blankLine = 0;
            int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");
            if (starterId == 0)
            {
                starterId = aRestaurantMenuBLL.GetCategoryByName("Starters");
            }
            reciept_font = aRestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

            PrintContent aPrintContent = new PrintContent();
            PrintFormat aPrintFormat = new PrintFormat(22);

            List<PrintContent> aPrintContentsMid = new List<PrintContent>();
            if (aRestaurantInformation.RecieptOption != "none")
            {
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.RestaurantName.ToUpper()) + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.House + " " + aRestaurantInformation.Address) + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.Postcode) + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("TEL:" + aRestaurantInformation.Phone) + "\n";
                aPrintContentsMid.Add(aPrintContent);

                if (aRestaurantInformation.VatRegNo != "")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.VatRegNo.ToUpper()) + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
                aPrintContentsMid.Add(aPrintContent);

            }

            string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(orderHistory) + "\n";
            aPrintContentsMid.Add(aPrintContent);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(DateTime.Now.ToString("dddd,dd/MM/yyyy")) + "\n";
            aPrintContentsMid.Add(aPrintContent);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);

            if (aORder.CustomerId > 0)
            {

                RestaurantUsers aUser = aCustomerBll.GetResturantCustomerByCustomerId(aORder.CustomerId);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aUser.Firstname + " " + aUser.Lastname + "\r\n";
                aPrintContentsMid.Add(aPrintContent);


                string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;


                string address = "";
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {

                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        if (ss.Count() > 0)
                        {
                            address += "," + ss[0];
                        }
                        if (ss.Count() > 1)
                        {
                            address += ", " + ss[1];
                        }
                        if (ss.Count() > 2)
                        {
                            address += " " + ss[2];
                        }
                        if (ss.Count() > 3)
                        {
                            address += ", " + ss[3];
                        }
                    }

                }


                if (aORder.OrderType == "DEL")
                {
                    if (address == "")
                    {
                        if (string.IsNullOrEmpty(aUser.FullAddress))
                        {

                            address += "" + aUser.House + " " + aUser.Address;
                            address += "," + aUser.City + " " + aUser.Postcode;
                        }
                        else
                        {

                            address += "" + aUser.House + "," + aUser.FullAddress + " " + aUser.Postcode;
                        }
                    }
                }


                if (!flag)
                {
                    address += " " + cell;
                }
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(address) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }
            else if (aORder.CustomerName != null)
            {
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aORder.CustomerName) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }
            string printStr = "";
            string printFooterStr = "";

            //List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);

            //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
            //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

            List<OrderItemMerged> newOrderItemList = new List<OrderItemMerged>();

            //show packages if merged print set to true
            if (aRestaurantInformation.UseJava > 0)
            {
                var pkgCount = 0;
                foreach (RecipePackageMD package in aRecipePackageMdList)
                {

                    pkgCount++;
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName + " " + (package.Qty * package.UnitPrice).ToString("F02"), (package.Qty * package.UnitPrice).ToString("F02").Length) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                if (pkgCount > 0)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
            }

            foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
            {
                //check if the same item is already stored
                var exitingItem = getExistingOrderItem(newOrderItemList, item_details);

                if (exitingItem != null)
                {
                    exitingItem.Qty += item_details.Qty;
                    exitingItem.Price += item_details.Price * item_details.Qty;
                }
                else
                {
                    OrderItemMerged orderItemMerged = new OrderItemMerged();
                    orderItemMerged.CategoryId = item_details.CategoryId;
                    orderItemMerged.CatSortOrder = item_details.CatSortOrder;
                    orderItemMerged.ItemFullName = item_details.ItemFullName;
                    orderItemMerged.ItemName = item_details.ItemName;
                    orderItemMerged.ItemId = item_details.ItemId;
                    orderItemMerged.ItemOption = item_details.ItemOption;
                    orderItemMerged.KitchenDone = item_details.KitchenDone;
                    orderItemMerged.KitchenProcessing = item_details.KitchenProcessing;
                    orderItemMerged.KitchenSection = item_details.KitchenSection;
                    orderItemMerged.OptionsIndex = item_details.OptionsIndex;
                    orderItemMerged.OptionName = item_details.OptionName;
                    orderItemMerged.OptionNoOption = item_details.OptionNoOption;
                    orderItemMerged.Option_ids = item_details.Option_ids;
                    orderItemMerged.Price = item_details.Price * item_details.Qty;
                    orderItemMerged.Qty = item_details.Qty;
                    orderItemMerged.RecipeTypeId = item_details.RecipeTypeId;
                    orderItemMerged.sendToKitchen = item_details.sendToKitchen;
                    orderItemMerged.SortOrder = item_details.SortOrder;
                    newOrderItemList.Add(orderItemMerged);
                }
            }


            //if merge print true, then merge package items together
            if (aRestaurantInformation.UseJava > 0)
            {
                List<PackageItem> aPaItem =
                    aPackageItemMdList.Where(
                        a => a.PackageId > 0)
                        .ToList();
                List<PackageItem> aPaItemNew = new List<PackageItem>();
                foreach (PackageItem item in aPaItem)
                {

                    //item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                    //aPaItemNew.Add(item);

                    //check if the same item is already stored
                    var exitingItem = getExistingOrderItem(newOrderItemList, null, item);
                    if (exitingItem != null)
                    {
                        exitingItem.Qty += item.Qty;
                        exitingItem.Price += item.Price * item.Qty;
                    }
                    else
                    {

                        OrderItemMerged orderItemMerged = new OrderItemMerged();
                        orderItemMerged.CategoryId = item.CategoryId;
                        orderItemMerged.CatSortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                        orderItemMerged.ItemFullName = item.ItemFullName;
                        orderItemMerged.ItemName = item.ItemName;
                        orderItemMerged.ItemId = item.ItemId;
                        orderItemMerged.KitchenProcessing = item.kitchenProcessing;
                        orderItemMerged.OptionsIndex = item.PackageItemOptionsIndex;
                        orderItemMerged.OptionName = item.OptionName;
                        orderItemMerged.OptionNoOption = item.MinusOption;
                        orderItemMerged.Price = item.Price * item.Qty;
                        orderItemMerged.Qty = item.Qty;
                        newOrderItemList.Add(orderItemMerged);
                    }
                }

            }

            newOrderItemList = newOrderItemList.OrderBy(a => a.SortOrder).ToList();
            newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();

            int catId = 0;
            bool startdas = false;
            foreach (OrderItemMerged itemDetails in newOrderItemList)
            {

                if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    startdas = false;
                }

                if (aRestaurantInformation.MenuSeparation == 1 && catId != itemDetails.CategoryId && catId != 0)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                string recipe_name = itemDetails.ItemName;

                if (aORder.OnlineOrder > 0)
                {
                    recipe_name = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);
                    if (recipe_name == "")
                        recipe_name = itemDetails.ItemName;
                }


                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString(itemDetails.Qty + " " + recipe_name + "  " + itemDetails.Price.ToString("F02"), itemDetails.Price.ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                blankLine++;


                string options = "";
                List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                if (aOption.Count > 0)
                {

                    foreach (RecipeOptionMD option in aOption)
                    {

                        if (!string.IsNullOrEmpty(option.Title))
                        {

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullString("  → " + option.Title + (option.Price > 0 ? "+" + option.Price.ToString("F02") : "")) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            blankLine++;
                        }
                        if (!string.IsNullOrEmpty(option.MinusOption))
                        {

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullString("  →No " + option.MinusOption) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);


                            blankLine++;
                        }

                    }
                }


                if (aRestaurantInformation.MenuSeparation == 2)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }


                catId = itemDetails.CategoryId;
                if (starterId == itemDetails.CategoryId)
                {
                    startdas = true;
                }



            }

            if (aRestaurantInformation.UseJava <= 0)
            {
                foreach (RecipePackageMD package in aRecipePackageMdList)
                {

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName + " " + (package.Qty * package.UnitPrice).ToString("F02"), (package.Qty * package.UnitPrice).ToString("F02").Length) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    blankLine++;
                    List<PackageItem> aPaItem = aPackageItemMdList.Where(
                            a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                            .ToList();

                    List<PackageItem> aPaItemNew = new List<PackageItem>();
                    foreach (PackageItem item in aPaItem)
                    {
                        item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                        aPaItemNew.Add(item);
                    }
                    aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                    foreach (PackageItem itemDetails in aPaItem)
                    {
                        string packageItemPrice = itemDetails.Price > 0
                            ? (itemDetails.Price).ToString("F02")
                            : "";


                        string recipeName = itemDetails.ItemName;
                        if (aORder.OnlineOrder > 0)
                        {
                            recipeName = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);

                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString(" " + " " + itemDetails.Qty.ToString() + " " + recipeName + " " + " " + packageItemPrice, packageItemPrice.Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);

                        blankLine++;
                        List<RecipeOptionMD> aOption =
                            aRecipeOptionMdList.Where(
                                a => a.RecipeId == itemDetails.ItemId && a.RecipeOPtionItemId == itemDetails.Id)
                                .ToList();

                        if (aOption.Count > 0)
                        {

                            foreach (RecipeOptionMD option in aOption)
                            {
                                if (!string.IsNullOrEmpty(option.Title))
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_fullString(" \r→" + option.Title) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);


                                    blankLine++;
                                }
                                if (!string.IsNullOrEmpty(option.MinusOption))
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_fullString("  \r→No" + option.MinusOption) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);


                                    blankLine++;
                                }

                            }
                        }


                    }


                }

            }


            foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
            {



                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.MultiplePartName + " " + " " + (package.Qty * package.UnitPrice).ToString("F02"), (package.Qty * package.UnitPrice).ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                blankLine++;
                List<MultipleItemMD> aPaItem =
                    aMultipleItemMdList.Where(
                        a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                        .ToList();
                int cnt = 0;
                foreach (MultipleItemMD itemDetails in aPaItem)
                {
                    cnt++;
                    string packageItemPrice = itemDetails.Price > 0
                        ? (itemDetails.Price).ToString()
                        : "";



                    string recipeName = itemDetails.ItemName;
                    if (aORder.OnlineOrder > 0)
                    {
                        recipeName = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);

                    }

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_leftString((cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                      recipeName) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    blankLine++;
                    string options = "";
                    List<RecipeOptionMD> aOption =
                        aRecipeOptionMdList.Where(
                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                            .ToList();
                    if (aOption.Count > 0)
                    {

                        foreach (RecipeOptionMD option in aOption)
                        {
                            if (!string.IsNullOrEmpty(option.Title))
                            {

                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_leftString("\r→" + option.Title + " " + " " + (option.Price).ToString("F02")) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);



                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {

                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_leftString("\r→No" + option.MinusOption) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);


                                blankLine++;
                            }
                        }
                    }


                }

            }



            double amount = aORder.TotalCost;


            if (blankLine < aRestaurantInformation.RecieptMinHeight)
            {
                for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = "  \r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
            }

            double subamount = GetTotalAmountDetails();
            subamount = GlobalVars.numberRound(subamount, 2);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);
            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.get_alignmentString(" SUBTOTAL  £" + subamount.ToString("F02"), ("£" + subamount.ToString("F02")).Length) + "\r\n";
            aPrintContentsMid.Add(aPrintContent);

            if (aORder.DiscountAmount > 0)
            {
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString("Discount  " + aORder.DiscountAmount.ToString("F02"), aORder.DiscountAmount.ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

            }



            if (aGeneralInformation.CardFee > 0)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString("S/C " + aGeneralInformation.CardFee.ToString("F02"), aGeneralInformation.CardFee.ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }

            if (aGeneralInformation.DeliveryCharge > 0)
            {
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString("D/C  " + aGeneralInformation.DeliveryCharge.ToString("F02"), aGeneralInformation.DeliveryCharge.ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);
            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.get_alignmentString(aRestaurantOrder.OrderTime.ToString("hh:mmtt") + "  TOTAL  £" + amount.ToString("F02"), ("£" + amount.ToString("F02")).Length) + "\r\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);


            if (aORder.CardAmount > 0)
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" PAID BY CARD") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }
                else
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("  CARD ORDER ") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }
            }
            else
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" PAID ORDER") + "\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                else
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" ORDER IS NOT PAID ") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }

            }


            if (aORder.Comment != null)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aORder.Comment) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

            }

            if (aRestaurantInformation.ThankYouMsg.Length > 5)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aRestaurantInformation.ThankYouMsg) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);


            }

            if (aRestaurantInformation.ShowOrderNumber > 0)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantOrder.OrderNo.ToString("D2")) + "\n";
                aPrintContentsMid.Add(aPrintContent);
            }

            string allpage = "";
            for (int i = 0; i < aPrintContentsMid.Count; i++)
            {
                allpage += aPrintContentsMid[i].StringLine;
            }
            return allpage;
        }



     public string getStringForPrint(string str)
     {
         string newStr = "";

         string[]  strArray = str.Split(' ');
         int arrayLeng = strArray.Length;
         int newLen = 0;
         for (int i = 0; i < arrayLeng; i++)
         {
             newLen += strArray[i].Length;
             if (newLen < 18)
             {
                 newStr += strArray[i] + " ";
             }
             else
             {
                 newLen = 0;
                 newStr += "<br>&nbsp;&nbsp;&nbsp;" + strArray[i] + " ";
             }
         }
               
         //int leng = str.Length;

         //if (leng > 20)
         //{
         //    newStr = str.Substring(0, 20).ToString() + "<br>" + str.Substring(20, leng - 20).ToString();
         //}
         return newStr;
     }
     public string LoadAllOrder()
     {
         LoadAllSaveOrder();
         if (Properties.Settings.Default.enableWebPrint)
         {
             return GenerateDetails_running(aRestaurantOrder.Id);
         }
         else
         {

             return GenerateDetails(aRestaurantOrder.Id);
         }

     }
     private void LoadAllSaveOrder()
     {


         RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
         RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

         aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
         aRecipePackageMdList = new List<RecipePackageMD>();
         aPackageItemMdList = new List<PackageItem>();
         aRecipeOptionMdList = new List<RecipeOptionMD>();

         aRecipeMultipleMdList = new List<RecipeMultipleMD>();
         aGeneralInformation.OrderId = aRestaurantOrder.Id;
         aGeneralInformation.CustomerId = aRestaurantOrder.CustomerId;
         aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
         try
         {
             aGeneralInformation.DeliveryTime = aRestaurantOrder.DeliveryTime.ToShortDateString();
         }
         catch (Exception ex) { }



         if (aRestaurantOrder != null && aRestaurantOrder.Id > 0)
         {
             aGeneralInformation.OrderId = aRestaurantOrder.Id;
             aGeneralInformation.CustomerId = aRestaurantOrder.CustomerId;
             aGeneralInformation.OrderDiscount = aRestaurantOrder.Discount;
             aGeneralInformation.DeliveryCharge = aRestaurantOrder.DeliveryCost;
             aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
             aGeneralInformation.DeliveryTime = aRestaurantOrder.DeliveryTime.ToString();
             aGeneralInformation.Person = aRestaurantOrder.Person;
             aGeneralInformation.TableId = aRestaurantOrder.OrderTable;
             aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
             aGeneralInformation.ServiceCharge = aRestaurantOrder.ServiceCharge;


                List<OrderItem> aOrderItems = new List<OrderItem>();
                aOrderItems = aRestaurantOrderBLL.GetRestaurantOrderRecipeItems(aRestaurantOrder.Id);
                List<OrderPackage> aPackageItems = new List<OrderPackage>();
                aPackageItems = aRestaurantOrderBLL.GetRestaurantOrderPackage(aRestaurantOrder.Id);



                int optionIndex = 0;
             foreach (OrderItem item in aOrderItems)
             {
                 try
                 {
                     if (item.PackageId <= 0 && String.IsNullOrEmpty(item.MultipleMenu))
                     {
                         optionIndex++;
                         ReceipeMenuItemButton aReceipeMenuItemButton =
                             aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                         ReceipeCategoryButton aReceipeCategoryButton =
                             aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                         OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();

                         aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                         aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                         aOrderItemDetails.OrderItemId = item.Id;
                         aOrderItemDetails.ItemName = item.Name;
                         aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
                         aOrderItemDetails.OptionsIndex = optionIndex;
                         aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
                         aOrderItemDetails.Price = item.Price / item.Quantity;
                         aOrderItemDetails.Qty = item.Quantity;
                         aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                         aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
                         aOrderItemDetails.CatSortOrder = aReceipeCategoryButton.SortOrder;
                         aOrderItemDetails.KitchenProcessing = item.KitchenProcessing;
                         aOrderItemDetails.TableNumber = aGeneralInformation.TableId;
                            aOrderItemDetails.OptionName = item.Options;
                            aOrderItemDetails.OptionNoOption = item.MinusOptions;

                         LoadSaveOption(item, aOrderItemDetails);

                         aOrderItemDetailsMDList.Add(aOrderItemDetails);
                     }
                     else if (item.PackageId <= 0 && !String.IsNullOrEmpty(item.MultipleMenu))
                     {
                         try
                         {
                             optionIndex++;
                             ReceipeMenuItemButton aReceipeMenuItemButton =
                                 aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                             ReceipeCategoryButton aReceipeCategoryButton =
                                 aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                             RecipeMultipleMD aOrderItemDetails = new RecipeMultipleMD();
                             aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                             aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;

                             aOrderItemDetails.OrderItemId = item.Id;
                             aOrderItemDetails.MultiplePartName = item.Name;
                             aOrderItemDetails.OptionsIndex = optionIndex;
                             aOrderItemDetails.UnitPrice = item.Price / item.Quantity;
                             aOrderItemDetails.Qty = item.Quantity;
                             aOrderItemDetails.RestaurantId = aRestaurantInformation.Id;
                             aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;

                             List<MultipleMenuDetailsJson> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJson>();
                             try
                             {
                                 aMultipleMenuDetailsJsons = JsonConvert.DeserializeObject<List<MultipleMenuDetailsJson>>(item.MultipleMenu);
                             }
                             catch (Exception exception)
                             {
                                 ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                 aErrorReportBll.SendErrorReport(exception.ToString());
                             }

                             aOrderItemDetails.ItemLimit = aMultipleMenuDetailsJsons.Count;
                             LoadMultilpleItemsList(aOrderItemDetails, aMultipleMenuDetailsJsons);
                             aRecipeMultipleMdList.Add(aOrderItemDetails);
                         }
                         catch (Exception exception)
                         {
                             ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                             aErrorReportBll.SendErrorReport(exception.ToString());
                         }


                     }
                 }
                 catch (Exception exception)
                 {
                     ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                     aErrorReportBll.SendErrorReport(exception.ToString());
                 }
             }

             foreach (OrderPackage package in aPackageItems)
             {
                 optionIndex++;
                 RecipePackageButton tempRecipePackageButton = aRestaurantMenuBll.GetPackageByPackageId(package.PackageId);
                 RecipePackageMD aRecipePackage = new RecipePackageMD();
                 aRecipePackage.Description = tempRecipePackageButton.Description;
                 aRecipePackage.OptionsIndex = optionIndex;
                 aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
                 aRecipePackage.PackageName = package.Name;
                 aRecipePackage.Qty = package.Quantity;
                 aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
                 aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;

                 LoadPackageItem(aOrderItems, aRecipePackage, package.Id);
                 aRecipePackage.UnitPrice = (package.Price - GetPackageItemPrice(package.PackageId, optionIndex)) / package.Quantity;
                 aRecipePackageMdList.Add(aRecipePackage);
             }




         }



     }
     private double GetPackageItemPrice(int packageId, int optionIndex)
     {
         double sum = aPackageItemMdList.Where(a => a.PackageId == packageId && a.OptionsIndex == optionIndex).Sum(b => b.Price * b.Qty);
         return sum;
     }
     private void LoadPackageItem(List<OrderItem> aOrderItems, RecipePackageMD aRecipePackage, int packageId)
     {

         RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
         foreach (OrderItem item in aOrderItems)
         {

             if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == packageId && item.orderPackageId > 0) //item.RecipeId==aRecipePackage.OptionsIndex
             {

                 PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                 //    PackageItemButton itemButton = aVariousMethod.GetItemForPackageOrder(item.RecipeId,item.PackageId);
                 ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                 PackageItem aItem = new PackageItem();
                 aItem.ItemId = item.RecipeId;
                 aItem.ItemName = item.Name;
                 aItem.Price = item.Price;
                 aItem.OrderItemId = item.Id;
                 aItem.ExtraPrice = item.ExtraPrice;
                 aItem.Qty = item.Quantity;
                 aItem.OptionName = item.Options;
                    aItem.MinusOption = item.MinusOptions;
                 aItem.PackageId = item.PackageId;
                 aItem.CategoryId = menuItem.CategoryId;
                 aItem.SubcategoryId = menuItem.SubCategoryId;
                 aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                 aItem.kitchenProcessing = item.KitchenProcessing;
                 aItem.Id = item.Id;
                 LoadSaveOptionForPackage(item, aItem);

                 aPackageItemMdList.Add(aItem);
             }
             else if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == 0) //item.RecipeId==aRecipePackage.OptionsIndex
             {

                 PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                 //PackageItemButton itemButton = aVariousMethod.GetItemForPackageOrder(item.RecipeId,item.PackageId);
                 ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                 PackageItem aItem = new PackageItem();
                 aItem.ItemId = item.RecipeId;
                 aItem.ItemName = item.Name;
                 aItem.Price = 0;
                 aItem.Qty = item.Quantity;
                    aItem.OrderItemId = item.Id;
                    aItem.OptionName = string.IsNullOrEmpty(item.Options) ? itemButton.OptionName : item.Options;
                 aItem.PackageId = item.PackageId;
                 aItem.CategoryId = itemButton.CategoryId;
                 aItem.SubcategoryId = itemButton.SubCategoryId;
                 aItem.kitchenProcessing = item.KitchenProcessing;
                 aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                 aItem.Id = item.Id;
                 LoadSaveOptionForPackage(item, aItem);
                 aPackageItemMdList.Add(aItem);
             }
         }

     }
     private void LoadSaveOptionForPackage(OrderItem item, PackageItem aOrderItemDetails)
     {
         List<OptionJson> ListOfOption = new OptionJsonConverter().DeSerialize(item.Options);
         List<OptionJson> ListOfOption1 = new OptionJsonConverter().DeSerialize(item.MinusOptions);

         // string[] optionList = item.Options.Split(',');

         RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
         if (ListOfOption != null)
         {
             for (int i = 0; i < ListOfOption.Count; i++)
             {
                 
                     RecipeOptionMD aOptionMD = new RecipeOptionMD();
                     aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                     aOptionMD.TableNumber = 1;
                     aOptionMD.RecipeOptionId = Convert.ToInt32(ListOfOption[i].optionId);
                     aOptionMD.Title = ListOfOption[i].optionName;
                     aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                     aOptionMD.Price = ListOfOption[i].optionPrice;
                     aOptionMD.Isoption = ListOfOption[i].NoOption;
                     aOptionMD.InPrice = ListOfOption[i].optionPrice;
                     aOptionMD.Qty = ListOfOption[i].optionQty;
                      aOptionMD.RecipeOPtionItemId = item.Id;
                    aRecipeOptionMdList.Add(aOptionMD);
                 
             }
            
         }
         if (ListOfOption1 != null)
         {
             for (int i = 0; i < ListOfOption1.Count(); i++)
             {
                 RecipeOptionMD aOptionMD = new RecipeOptionMD();
                 aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                 aOptionMD.TableNumber = 1;
                 aOptionMD.RecipeOptionId = Convert.ToInt32(ListOfOption1[i].optionId);
                 aOptionMD.Title = ListOfOption1[i].optionName;
                 aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                 aOptionMD.Price = 0;
                 aOptionMD.Isoption = ListOfOption1[i].NoOption;
                 aOptionMD.InPrice = 0;
                 aOptionMD.Qty = ListOfOption1[i].optionQty; 
                 aOptionMD.RecipeOPtionItemId = item.Id;
                 aRecipeOptionMdList.Add(aOptionMD);
                 
              

             }


         }


     }
     private void LoadMultilpleItemsList(RecipeMultipleMD aOrderItemDetails, List<MultipleMenuDetailsJson> aMultipleMenuDetailsJsons)
     {

         try
         {
             foreach (MultipleMenuDetailsJson multipleItem in aMultipleMenuDetailsJsons)
             {
                 MultipleItemMD itemMd = new MultipleItemMD();
                 itemMd.CategoryId = multipleItem.category_id;
                 itemMd.ItemId = multipleItem.id;
                 itemMd.ItemName = multipleItem.name;
                 itemMd.OptionsIndex = aOrderItemDetails.OptionsIndex;
                 itemMd.Price = 0;
                 itemMd.Qty = (int)multipleItem.quantity;
                 itemMd.SubcategoryId = aOrderItemDetails.CategoryId;
                 aMultipleItemMdList.Add(itemMd);
                 LoadOptions(aOrderItemDetails, itemMd, multipleItem);
             }

         }
         catch (Exception exception)
         {
             ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
             aErrorReportBll.SendErrorReport(exception.ToString());
         }



     }
     private void LoadOptions(RecipeMultipleMD recipeMultiple, MultipleItemMD aOrderItemDetails, MultipleMenuDetailsJson json)
     {
         List<string> optionList = json.options;


         RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
         if (optionList.Any())
         {
             for (int i = 0; i < optionList.Count(); i++)
             {
                 if (optionList[i] != null && optionList[i].Length > 0)
                 {
                     RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList[i].Trim());
                     if (recipe.RecipeOptionItemId > 0)
                     {
                         RecipeOptionMD aOptionMD = new RecipeOptionMD();
                         aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                         aOptionMD.TableNumber = 1;
                         aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                         aOptionMD.Title = recipe.Title;
                         aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                         aOptionMD.Price = recipe.Price;
                         aOptionMD.InPrice = recipe.InPrice;
                         aOptionMD.Qty = 1;
                         aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                         aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                         aRecipeOptionMdList.Add(aOptionMD);
                     }
                 }
             }
         }

         List<string> optionList1 = json.minus_options;

         if (optionList1.Any())
         {
             for (int i = 0; i < optionList1.Count(); i++)
             {
                 if (optionList1[i] != null && optionList1[i].Length > 0)
                 {
                     string op = optionList1[i];
                     RecipeOptionMD tempOptionMd = aRecipeOptionMdList.FirstOrDefault(a => a.Title == optionList1[i] && a.OptionsIndex == aOrderItemDetails.OptionsIndex);
                     if (tempOptionMd != null && tempOptionMd.RecipeId > 0)
                     {
                         tempOptionMd.MinusOption = optionList1[i];
                     }
                     else
                     {
                         RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList1[i].Trim());
                         if (recipe.RecipeOptionItemId > 0)
                         {
                             RecipeOptionMD aOptionMD = new RecipeOptionMD();
                             aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                             aOptionMD.TableNumber = 1;
                             aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                             aOptionMD.MinusOption = optionList1[i];
                             aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                             aOptionMD.Price = recipe.Price;
                             aOptionMD.InPrice = recipe.InPrice;
                             aOptionMD.Qty = 1;
                             aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                             aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                             aRecipeOptionMdList.Add(aOptionMD);
                         }
                     }
                 }
             }
         }
     }

     private void LoadSaveOption(OrderItem item, OrderItemDetailsMD aOrderItemDetails)
     {
         List<OptionJson> optionList = new OptionJsonConverter().DeSerialize(item.Options);

         RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
         if (optionList != null)
         {
             for (int i = 0; i < optionList.Count; i++)
             {
                 RecipeOptionMD aOptionMD = new RecipeOptionMD();
                 aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                 aOptionMD.TableNumber = 1;aOptionMD.RecipeOptionId = Convert.ToInt32(optionList[i].optionId);
                 aOptionMD.Title = optionList[i].optionName;
                 aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                 aOptionMD.Price = optionList[i].optionPrice;
                 aOptionMD.Isoption = optionList[i].NoOption;
                 aOptionMD.InPrice = optionList[i].optionPrice;
                 aOptionMD.Qty = optionList[i].optionQty;
                 aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                 aOptionMD.RecipeOPtionItemId = aOrderItemDetails.ItemId;
                 aRecipeOptionMdList.Add(aOptionMD);
             }
         }

         List<OptionJson> optionList1 = new OptionJsonConverter().DeSerialize(item.MinusOptions);


         if (optionList1 != null)
         {
             for (int i = 0; i < optionList1.Count; i++)
             {
                 RecipeOptionMD aOptionMD = new RecipeOptionMD();
                 aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                 aOptionMD.TableNumber = 1;
                 aOptionMD.RecipeOptionId = Convert.ToInt32(optionList1[i].optionId);
                 aOptionMD.Title = optionList1[i].optionName;
                 aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                 aOptionMD.Price = 0;
                 aOptionMD.Isoption = optionList1[i].NoOption;
                 aOptionMD.InPrice = 0;
                 aOptionMD.Qty = optionList1[i].optionQty;
                 aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                 aOptionMD.RecipeOPtionItemId = aOrderItemDetails.ItemId;
                 aRecipeOptionMdList.Add(aOptionMD);
             }
         }
     }

        public Dictionary<int, string> GetRecipeTypes(PrinterSetup recipeTypes)
        {
            Dictionary<int, string> types = new Dictionary<int, string>();
            string[] list = recipeTypes.RecipeTypeList.Split(',');
            string[] name = recipeTypes.RecipeNames.Split(',');

            for (int i = 0; i < list.Count(); i++)
            {
                int type = Convert.ToInt32("0" + list[i]);
                string typeName = name[i];
                types.Add(type, typeName);
            }
            return types;
        }

        public void GenerateKitchenCopyString(int result, PrinterSetup printer, bool full_print = false)
        {

            int papersize = 25;
            string reciept_font = "";

            RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
            RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            RestaurantTable aRestaurantTable = new RestaurantTableBLL().GetRestaurantTableByTableId(aGeneralInformation.TableId);

            aGeneralInformation.TableNumber = aRestaurantTable.Name;
            int blankLine = 0;
            int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");
            if (starterId == 0)
            {
                starterId = aRestaurantMenuBLL.GetCategoryByName("Starters");
            }
            reciept_font = aRestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
            string reciept_font_exlgr = "26";

            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

            //if (aRestaurantInformation.RecieptOption == "logo_title")
            //{
            //    string path = @"Image/" + aRestaurantInformation.Id + "_website_logo.png";
            //    if (File.Exists(path))
            //    {
            //        var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(path)));
            //        printHeaderStr += "<tr><td colspan='2' style='text-align:center'><img style='width:120px; height: 60px' src='" + imageString + "'></td></tr>";
            //    }
            //}

            PrintContent aPrintContent = new PrintContent();
            PrintFormat aPrintFormat = new PrintFormat(22);
            List<PrintContent> aPrintContentsMid = new List<PrintContent>();
            string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation);


            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(printer.PrinterAddress.ToUpper()) + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(orderHistory) + "\n";
            aPrintContentsMid.Add(aPrintContent);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(DateTime.Now.ToString("dddd,dd/MM/yyyy")) + "\n";
            aPrintContentsMid.Add(aPrintContent);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);

            if (aORder.CustomerId > 0)
            {

                RestaurantUsers aUser = aCustomerBll.GetResturantCustomerByCustomerId(aORder.CustomerId);
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aUser.Firstname + " " + aUser.Lastname + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
                string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;
                string address = "";
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {
                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        if (ss.Count() > 0)
                        {
                            address += "," + ss[0];
                        }
                        if (ss.Count() > 1)
                        {
                            address += ", " + ss[1];
                        }
                        if (ss.Count() > 2)
                        {
                            address += " " + ss[2];
                        }
                        if (ss.Count() > 3)
                        {
                            address += ", " + ss[3];
                        }
                    }

                }
                if (aORder.OrderType == "DEL")
                {
                    if (address == "")
                    {
                        if (string.IsNullOrEmpty(aUser.FullAddress))
                        {

                            address += "" + aUser.House + " " + aUser.Address;
                            address += "," + aUser.City + " " + aUser.Postcode;
                        }
                        else
                        {

                            address += "" + aUser.House + "," + aUser.FullAddress + " " + aUser.Postcode;
                        }
                    }
                }
                if (!flag)
                {
                    address += " " + cell;
                }
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(address) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }
            else if (aORder.CustomerName != null)
            {
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aORder.CustomerName) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }
            string printStr = "";
            string printFooterStr = "";

            //List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);
            
            //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
            //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();

            Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);

            List<ReceipeTypeButton> aRecipeTypeList = aRestaurantMenuBll.GetRecipeType();

            List<OrderItemMerged> newOrderItemList = new List<OrderItemMerged>();

            //show packages if merged print set to true
            if (aRestaurantInformation.UseJava > 0)
            {
                var pkgCount = 0;
                foreach (RecipePackageMD package in aRecipePackageMdList)
                {
                    if (recipeTypes.ContainsKey(package.RecipeTypeId))
                    {
                        pkgCount++;
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName, 0) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                }
                if (pkgCount > 0)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
            }

            if (full_print)
            {
                foreach(OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                {
                    OrderItemMerged orderItemMerged = new OrderItemMerged();
                    orderItemMerged.CategoryId = item_details.CategoryId;
                    orderItemMerged.CatSortOrder = item_details.CatSortOrder;
                    orderItemMerged.ItemFullName = item_details.ItemFullName;
                    orderItemMerged.ItemName = item_details.ItemName;
                    orderItemMerged.ItemId = item_details.ItemId;
                    orderItemMerged.ItemOption = item_details.ItemOption;
                    orderItemMerged.KitchenDone = item_details.KitchenDone;
                    orderItemMerged.KitchenProcessing = item_details.KitchenProcessing;
                    orderItemMerged.KitchenSection = item_details.KitchenSection;
                    orderItemMerged.OptionsIndex = item_details.OptionsIndex;
                    orderItemMerged.OptionName = item_details.OptionName;
                    orderItemMerged.OptionNoOption = item_details.OptionNoOption;
                    orderItemMerged.Option_ids = item_details.Option_ids;
                    orderItemMerged.Price = item_details.Price * item_details.Qty;
                    orderItemMerged.Qty = item_details.Qty;
                    orderItemMerged.RecipeTypeId = item_details.RecipeTypeId;
                    orderItemMerged.sendToKitchen = item_details.sendToKitchen;
                    orderItemMerged.SortOrder = item_details.SortOrder;
                    newOrderItemList.Add(orderItemMerged);
                }                
            }
            else
            {

                foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                {
                    if (recipeTypes.ContainsKey(item_details.RecipeTypeId))
                    {
                        OrderItemMerged orderItemMerged = new OrderItemMerged();
                        orderItemMerged.CategoryId = item_details.CategoryId;
                        orderItemMerged.CatSortOrder = item_details.CatSortOrder;
                        orderItemMerged.ItemFullName = item_details.ItemFullName;
                        orderItemMerged.ItemName = item_details.ItemName;
                        orderItemMerged.ItemId = item_details.ItemId;
                        orderItemMerged.ItemOption = item_details.ItemOption;
                        orderItemMerged.KitchenDone = item_details.KitchenDone;
                        orderItemMerged.KitchenProcessing = item_details.KitchenProcessing;
                        orderItemMerged.KitchenSection = item_details.KitchenSection;
                        orderItemMerged.OptionsIndex = item_details.OptionsIndex;
                        orderItemMerged.OptionName = item_details.OptionName;
                        orderItemMerged.OptionNoOption = item_details.OptionNoOption;
                        orderItemMerged.Option_ids = item_details.Option_ids;
                        orderItemMerged.Price = item_details.Price * item_details.Qty;
                        orderItemMerged.Qty = item_details.Qty;
                        orderItemMerged.RecipeTypeId = item_details.RecipeTypeId;
                        orderItemMerged.sendToKitchen = item_details.sendToKitchen;
                        orderItemMerged.SortOrder = item_details.SortOrder;
                        newOrderItemList.Add(orderItemMerged);
                    }
                }

            }

            //if merge print true, then merge package items together
            if (aRestaurantInformation.UseJava > 0)
            {
                List<PackageItem> aPaItem =
                    aPackageItemMdList.Where(
                        a => a.PackageId > 0)
                        .ToList();
                List<PackageItem> aPaItemNew = new List<PackageItem>();
                foreach (PackageItem item in aPaItem)
                {
                    if (!recipeTypes.ContainsKey(aVariousMethod.GetRecipeTypeIdByCategory(item.CategoryId)))
                    {
                        continue;
                    }

                    //check if the same item is already stored
                    var exitingItem = getExistingOrderItem(newOrderItemList, null, item);
                    if (exitingItem != null)
                    {
                        exitingItem.Qty += item.Qty;
                        exitingItem.Price += item.Price * item.Qty;
                    }
                    else
                    {
                        OrderItemMerged orderItemMerged = new OrderItemMerged();
                        orderItemMerged.CategoryId = item.CategoryId;
                        orderItemMerged.CatSortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                        orderItemMerged.ItemFullName = item.ItemFullName;
                        orderItemMerged.ItemName = item.ItemName;
                        orderItemMerged.ItemId = item.ItemId;
                        orderItemMerged.KitchenProcessing = item.kitchenProcessing;
                        orderItemMerged.OptionsIndex = item.PackageItemOptionsIndex;
                        orderItemMerged.OptionName = item.OptionName;
                        orderItemMerged.OptionNoOption = item.MinusOption;
                        orderItemMerged.Price = item.Price * item.Qty;
                        orderItemMerged.Qty = item.Qty;
                        newOrderItemList.Add(orderItemMerged);
                    }
                }

            }

            newOrderItemList = newOrderItemList.OrderBy(a => a.SortOrder).ToList();
            newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();

            int catId = 0;
            bool startdas = false;
            bool printerPrintStatus = false;

            foreach (OrderItemMerged itemDetails in newOrderItemList)
            {
                printerPrintStatus = true;
                if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    startdas = false;
                }

                if (aRestaurantInformation.MenuSeparation == 1 && catId != itemDetails.CategoryId && catId != 0)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                string recipe_name = itemDetails.ItemName;

                if (aORder.OnlineOrder > 0)
                {
                    recipe_name = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);
                    if (recipe_name == "")
                        recipe_name = itemDetails.ItemName;
                }


                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(itemDetails.Qty + " " + recipe_name) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                blankLine++;


                string options = "";
                List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                if (aOption.Count > 0)
                {

                    foreach (RecipeOptionMD option in aOption)
                    {

                        if (!string.IsNullOrEmpty(option.Title))
                        {

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullString("  → " + option.Title) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            blankLine++;
                        }
                        if (!string.IsNullOrEmpty(option.MinusOption))
                        {

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullString("  →No " + option.MinusOption) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);


                            blankLine++;
                        }

                    }
                }


                if (aRestaurantInformation.MenuSeparation == 2)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }


                catId = itemDetails.CategoryId;
                if (starterId == itemDetails.CategoryId)
                {
                    startdas = true;
                }



            }

            if (aRestaurantInformation.UseJava <= 0)
            {
                foreach (RecipePackageMD package in aRecipePackageMdList)
                {

                    printerPrintStatus = true;
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(package.Qty.ToString() + " " + package.PackageName) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);


                    blankLine++;
                    List<PackageItem> aPaItem =
                        aPackageItemMdList.Where(
                            a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                            .ToList();
                    List<PackageItem> aPaItemNew = new List<PackageItem>();
                    foreach (PackageItem item in aPaItem)
                    {
                        item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                        aPaItemNew.Add(item);
                    }
                    aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                    foreach (PackageItem itemDetails in aPaItem)
                    {
                        string packageItemPrice = itemDetails.Price > 0
                            ? (itemDetails.Price).ToString()
                            : "";


                        string recipeName = itemDetails.ItemName;
                        if (aORder.OnlineOrder > 0)
                        {
                            recipeName = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);

                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + " " + itemDetails.Qty.ToString() + " " + recipeName) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);

                        blankLine++;
                        string options = "";
                        List<RecipeOptionMD> aOption =
                            aRecipeOptionMdList.Where(
                                a => a.RecipeId == itemDetails.ItemId && a.RecipeOPtionItemId == itemDetails.Id)
                                .ToList();
                        if (aOption.Count > 0)
                        {

                            foreach (RecipeOptionMD option in aOption)
                            {
                                if (!string.IsNullOrEmpty(option.Title))
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + " \r→" + option.Title) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);


                                    blankLine++;
                                }
                                if (!string.IsNullOrEmpty(option.MinusOption))
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + "  \r→No" + option.MinusOption) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);
                                    blankLine++;
                                }

                            }
                        }


                    }


                }
            }


            foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
            {


                printerPrintStatus = true;
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(package.Qty.ToString() + " " + package.MultiplePartName) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                blankLine++;
                List<MultipleItemMD> aPaItem =
                    aMultipleItemMdList.Where(
                        a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                        .ToList();
                int cnt = 0;
                foreach (MultipleItemMD itemDetails in aPaItem)
                {
                    cnt++;
                    string packageItemPrice = itemDetails.Price > 0
                        ? (itemDetails.Price).ToString()
                        : "";



                    string recipeName = itemDetails.ItemName;
                    if (aORder.OnlineOrder > 0)
                    {
                        recipeName = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);

                    }

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_leftString((cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                      recipeName) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    blankLine++;
                    string options = "";
                    List<RecipeOptionMD> aOption =
                        aRecipeOptionMdList.Where(
                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                            .ToList();
                    if (aOption.Count > 0)
                    {

                        foreach (RecipeOptionMD option in aOption)
                        {
                            if (!string.IsNullOrEmpty(option.Title))
                            {

                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_leftString("\r→" + option.Title + " " + " " + (option.Price).ToString("F02")) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {

                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_leftString("\r→No" + option.MinusOption) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                blankLine++;
                            }
                        }
                    }


                }

            }

            int numOfLine = blankLine;
            double subamount = GetTotalAmountDetails();
            subamount = GlobalVars.numberRound(subamount, 2);

            if (blankLine < aRestaurantInformation.RecieptMinHeight)
            {
                for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = " " + " " + "  " + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    numOfLine++;
                }
            }

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);
            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.get_alignmentString(" SUBTOTAL  £" + subamount.ToString("F02"), ("£" + subamount.ToString("F02")).Length) + "\r\n";
            aPrintContentsMid.Add(aPrintContent);

            double amount = aORder.TotalCost;

            if (aGeneralInformation.DiscountFlat > 0)
            {
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString("Discount (" + aGeneralInformation.DiscountPercent.ToString("F02") + "%) " + aGeneralInformation.DiscountFlat.ToString("F02"), aGeneralInformation.DiscountFlat.ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
                numOfLine++;
            }

            if (aGeneralInformation.CardFee > 0)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_alignmentString("S/C " + aGeneralInformation.CardFee.ToString("F02"), aGeneralInformation.CardFee.ToString("F02").Length) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
                numOfLine++;

            }

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);
            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.get_alignmentString(aRestaurantOrder.OrderTime.ToString("hh:mmtt") + "  TOTAL  £" + amount.ToString("F02"), ("£" + amount.ToString("F02")).Length) + "\r\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);





            if (aORder.CardAmount > 0)
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" PAID BY CARD") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }
                else
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("  CARD ORDER ") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }
            }
            else
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" PAID ORDER") + "\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                else
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" ORDER IS NOT PAID ") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }

            }

            if (aORder.Comment.Length > 3)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aORder.Comment) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

            }



            if (aRestaurantInformation.ShowOrderNumber > 0)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantOrder.OrderNo.ToString("D2")) + "\n";
                aPrintContentsMid.Add(aPrintContent);
            }


            string allpage = "";
            for (int i = 0; i < aPrintContentsMid.Count; i++)
            {
                allpage += aPrintContentsMid[i].StringLine;
            }

            //return allpage;
            if (printerPrintStatus)
            {

                PrintMethods kitPrintMethods = new PrintMethods(true, false, printer.printerMargin);
                kitPrintMethods.USBPrint(allpage, printer.PrinterName, printer.PrintCopy);
            }


        }



        public void KitchenPrint(int result, PrinterSetup printer, bool full_print = false)
        {

            int papersize = 25;
            string reciept_font = "";

            RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
            RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            RestaurantTable aRestaurantTable = new RestaurantTableBLL().GetRestaurantTableByTableId(aGeneralInformation.TableId);

            aGeneralInformation.TableNumber = aRestaurantTable.Name;
            int blankLine = 0;
            List<int> starterIds = aRestaurantMenuBLL.GetCategoriesByName("Starter");
            if (starterIds.Count == 0)
            {
                starterIds = aRestaurantMenuBLL.GetCategoriesByName("Starters");
            }
            reciept_font = aRestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
         
            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

            //if (aRestaurantInformation.RecieptOption == "logo_title")
            //{
            //    string path = @"Image/" + aRestaurantInformation.Id + "_website_logo.png";
            //    if (File.Exists(path))
            //    {
            //        var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(path)));
            //        printHeaderStr += "<tr><td colspan='2' style='text-align:center'><img style='width:120px; height: 60px' src='" + imageString + "'></td></tr>";
            //    }
            //}

            PrintContent aPrintContent = new PrintContent();
            PrintFormat aPrintFormat = new PrintFormat(22);
            List<PrintContent> aPrintContentsMid = new List<PrintContent>();
            string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation);


            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(printer.PrinterAddress.ToUpper()) + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(orderHistory) + "\n";
            aPrintContentsMid.Add(aPrintContent);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(DateTime.Now.ToString("dddd,dd/MM/yyyy")) + "\n";
            aPrintContentsMid.Add(aPrintContent);


            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
            aPrintContentsMid.Add(aPrintContent);

            RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);

            if (aORder.CustomerId > 0)
            {

                RestaurantUsers aUser = aCustomerBll.GetResturantCustomerByCustomerId(aORder.CustomerId);
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aUser.Firstname + " " + aUser.Lastname + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
                string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;
                string address = "";
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {
                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        if (ss.Count() > 0)
                        {
                            address += "," + ss[0];
                        }
                        if (ss.Count() > 1)
                        {
                            address += ", " + ss[1];
                        }
                        if (ss.Count() > 2)
                        {
                            address += " " + ss[2];
                        }
                        if (ss.Count() > 3)
                        {
                            address += ", " + ss[3];
                        }
                    }

                }
                if (aORder.OrderType == "DEL")
                {
                    if (address == "")
                    {
                        if (string.IsNullOrEmpty(aUser.FullAddress))
                        {
                            address += "" + aUser.House + " " + aUser.Address;
                            address += "," + aUser.City + " " + aUser.Postcode;
                        }
                        else
                        {
                            address += "" + aUser.House + "," + aUser.FullAddress + " " + aUser.Postcode;
                        }
                    }
                }
                if (!flag)
                {
                    address += " " + cell;
                }

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(address) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }
            else if (aORder.CustomerName != null)
            {
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aORder.CustomerName) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);
            }
            string printStr = "";
            string printFooterStr = "";

            //List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);

            aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
            aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();

            Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);

            List<ReceipeTypeButton> aRecipeTypeList = aRestaurantMenuBll.GetRecipeType();

            List<OrderItemMerged> newOrderItemList = new List<OrderItemMerged>();
            int FoundItemcount = 0;

            //show packages if merged print set to true
            if (aRestaurantInformation.UseJava > 0)
            {
                var pkgCount = 0;
                foreach (RecipePackageMD package in aRecipePackageMdList)
                {
                    if (recipeTypes.ContainsKey(package.RecipeTypeId))
                    {
                        pkgCount++;
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName, 0) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                }
                if (pkgCount > 0)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                }

                //if merge print true, then merge package items together                        
                List<PackageItem> aPaItem =
                    aPackageItemMdList.Where(
                        a => a.PackageId > 0)
                        .ToList();

                foreach (PackageItem item in aPaItem)
                {
                    if (!recipeTypes.ContainsKey(aVariousMethod.GetRecipeTypeIdByCategory(item.CategoryId)))
                    {
                        continue;
                    }

                    var exitingItem = getExistingOrderItem(newOrderItemList, null, item);

                    //&& a.OptionsIndex == item.PackageItemOptionsIndex
                    if (exitingItem != null)
                    {
                        exitingItem.Qty += item.Qty;
                        exitingItem.Price += item.Price * item.Qty;
                    }
                    else
                    {
                        OrderItemMerged orderItemMerged = new OrderItemMerged();
                        orderItemMerged.CategoryId = item.CategoryId;
                        orderItemMerged.CatSortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                        orderItemMerged.ItemFullName = item.ItemFullName;
                        orderItemMerged.ItemName = item.ItemName;
                        orderItemMerged.ItemId = item.ItemId;
                        orderItemMerged.KitchenProcessing = item.kitchenProcessing;
                        orderItemMerged.OptionsIndex = item.PackageItemOptionsIndex;
                        orderItemMerged.OptionName = null;
                        orderItemMerged.OptionNoOption = item.MinusOption;
                        orderItemMerged.Price = item.Price * item.Qty;
                        orderItemMerged.Qty = item.Qty;
                        newOrderItemList.Add(orderItemMerged);
                    }
                }
            }


            foreach (OrderItemDetailsMD itemDetails in aOrderItemDetailsMDList)
            {
                if (full_print)
                {
                    //check if the same item is already stored
                    var exitingItem = getExistingOrderItem(newOrderItemList, itemDetails);
                    if (exitingItem != null)
                    {
                        exitingItem.Qty += itemDetails.Qty;
                        exitingItem.Price += itemDetails.Price * itemDetails.Qty;
                    }
                    else
                    {
                        OrderItemMerged orderItemMerged = new OrderItemMerged();
                        orderItemMerged.CategoryId = itemDetails.CategoryId;
                        orderItemMerged.CatSortOrder = itemDetails.CatSortOrder;
                        orderItemMerged.ItemFullName = itemDetails.ItemFullName;
                        orderItemMerged.ItemName = itemDetails.ItemName;
                        orderItemMerged.ItemId = itemDetails.ItemId;
                        orderItemMerged.ItemOption = itemDetails.ItemOption;
                        orderItemMerged.KitchenDone = itemDetails.KitchenDone;
                        orderItemMerged.KitchenProcessing = itemDetails.KitchenProcessing;
                        orderItemMerged.KitchenSection = itemDetails.KitchenSection;
                        orderItemMerged.OptionsIndex = itemDetails.OptionsIndex;
                        orderItemMerged.OptionName = itemDetails.OptionName;
                        orderItemMerged.OptionNoOption = itemDetails.OptionNoOption;
                        orderItemMerged.Option_ids = itemDetails.Option_ids;
                        orderItemMerged.Price = itemDetails.Price * itemDetails.Qty;
                        orderItemMerged.Qty = itemDetails.Qty;
                        orderItemMerged.RecipeTypeId = itemDetails.RecipeTypeId;
                        orderItemMerged.sendToKitchen = itemDetails.sendToKitchen;
                        orderItemMerged.SortOrder = itemDetails.SortOrder;
                        newOrderItemList.Add(orderItemMerged);
                    }
                    continue;
                }
                if (recipeTypes.ContainsKey(itemDetails.RecipeTypeId))
                {
                    if ((itemDetails.KitchenProcessing) < itemDetails.Qty)
                    {
                        //check if the same item is already stored
                        var exitingItem = getExistingOrderItem(newOrderItemList, itemDetails);
                        if (exitingItem != null)
                        {
                            exitingItem.Qty += itemDetails.Qty;
                            exitingItem.Price += itemDetails.Price * itemDetails.Qty;
                        }
                        else
                        {
                            OrderItemMerged orderItemMerged = new OrderItemMerged();
                            orderItemMerged.CategoryId = itemDetails.CategoryId;
                            orderItemMerged.CatSortOrder = itemDetails.CatSortOrder;
                            orderItemMerged.ItemFullName = itemDetails.ItemFullName;
                            orderItemMerged.ItemName = itemDetails.ItemName;
                            orderItemMerged.ItemId = itemDetails.ItemId;
                            orderItemMerged.ItemOption = itemDetails.ItemOption;
                            orderItemMerged.KitchenDone = itemDetails.KitchenDone;
                            orderItemMerged.KitchenProcessing = itemDetails.KitchenProcessing;
                            orderItemMerged.KitchenSection = itemDetails.KitchenSection;
                            orderItemMerged.OptionsIndex = itemDetails.OptionsIndex;
                            orderItemMerged.OptionName = itemDetails.OptionName;
                            orderItemMerged.OptionNoOption = itemDetails.OptionNoOption;
                            orderItemMerged.Option_ids = itemDetails.Option_ids;
                            orderItemMerged.Price = itemDetails.Price * itemDetails.Qty;
                            orderItemMerged.Qty = itemDetails.Qty;
                            orderItemMerged.RecipeTypeId = itemDetails.RecipeTypeId;
                            orderItemMerged.sendToKitchen = itemDetails.sendToKitchen;
                            orderItemMerged.SortOrder = itemDetails.SortOrder;
                            newOrderItemList.Add(orderItemMerged);
                        }
                    }
                }
            }

            
            int catId = 0;
            bool startdas = false;
            bool printerPrintStatus = false;

            newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();

            foreach (OrderItemMerged itemDetails in newOrderItemList)
            {
                printerPrintStatus = true;
                if (aRestaurantInformation.MenuSeparation == 3 && startdas && !starterIds.Contains(itemDetails.CategoryId))
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    startdas = false;
                }

                if (aRestaurantInformation.MenuSeparation == 1 && catId != itemDetails.CategoryId && catId != 0)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                string recipe_name = itemDetails.ItemName;

                if (aORder.OnlineOrder > 0)
                {
                    recipe_name = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);
                    if (recipe_name == "")
                        recipe_name = itemDetails.ItemName;
                }


                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen((aORder.OrderTable > 0 ? (itemDetails.Qty - (itemDetails.KitchenProcessing)) : itemDetails.Qty) + " " + recipe_name) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                blankLine++;


                string options = "";
                List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                if (aOption.Count > 0)
                {

                    foreach (RecipeOptionMD option in aOption)
                    {

                        if (!string.IsNullOrEmpty(option.Title))
                        {

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullString("  → " + option.Title) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            blankLine++;
                        }
                        if (!string.IsNullOrEmpty(option.MinusOption))
                        {

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullString("  →No " + option.MinusOption) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);


                            blankLine++;
                        }

                    }
                }


                if (aRestaurantInformation.MenuSeparation == 2)
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                }


                catId = itemDetails.CategoryId;
                if (starterIds.Contains(itemDetails.CategoryId))
                {
                    startdas = true;
                }



            }

            if (aRestaurantInformation.UseJava <= 0)
            {

                foreach (RecipePackageMD package in aRecipePackageMdList)
                {


                    List<PackageItem> aPaItem =
                      aPackageItemMdList.Where(
                          a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                          .ToList();
                    List<PackageItem> aPaItemNew = new List<PackageItem>();
                    foreach (PackageItem item in aPaItem)
                    {

                        if (item.kitchenProcessing < item.Qty)
                        {
                            item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                            aPaItemNew.Add(item);

                            ItemIds.Add(item.OrderItemId);
                        }
                    }
                    if (aPaItemNew.Count() > 0)
                    {

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(package.Qty.ToString() + " " + package.PackageName) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);


                        blankLine++;

                        aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                        foreach (PackageItem itemDetails in aPaItem)
                        {
                            printerPrintStatus = true;
                            string packageItemPrice = itemDetails.Price > 0
                            ? (itemDetails.Price).ToString()
                            : "";


                            string recipeName = itemDetails.ItemName;
                            if (aORder.OnlineOrder > 0)
                            {
                                recipeName = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);

                            }

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + " " + (itemDetails.Qty > itemDetails.kitchenProcessing ? (itemDetails.Qty - itemDetails.kitchenProcessing).ToString() : itemDetails.Qty.ToString()) + " " + recipeName) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            blankLine++;
                            string options = "";
                            List<RecipeOptionMD> aOption =
                                aRecipeOptionMdList.Where(
                                    a => a.RecipeId == itemDetails.ItemId && a.RecipeOPtionItemId == itemDetails.Id)
                                    .ToList();
                            if (aOption.Count > 0)
                            {

                                foreach (RecipeOptionMD option in aOption)
                                {
                                    if (!string.IsNullOrEmpty(option.Title))
                                    {
                                        aPrintContent = new PrintContent();
                                        aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + " \r→" + option.Title) + "\r\n";
                                        aPrintContentsMid.Add(aPrintContent);


                                        blankLine++;
                                    }
                                    if (!string.IsNullOrEmpty(option.MinusOption))
                                    {
                                        aPrintContent = new PrintContent();
                                        aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + "  \r→No" + option.MinusOption) + "\r\n";
                                        aPrintContentsMid.Add(aPrintContent);
                                        blankLine++;
                                    }

                                }
                            }


                        }

                    }
                }
            }


            foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
            {


                printerPrintStatus = true;
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(package.Qty.ToString() + " " + package.MultiplePartName) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                blankLine++;
                List<MultipleItemMD> aPaItem =
                    aMultipleItemMdList.Where(
                        a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                        .ToList();
                int cnt = 0;
                foreach (MultipleItemMD itemDetails in aPaItem)
                {
                    cnt++;
                    string packageItemPrice = itemDetails.Price > 0
                        ? (itemDetails.Price).ToString()
                        : "";



                    string recipeName = itemDetails.ItemName;
                    if (aORder.OnlineOrder > 0)
                    {
                        recipeName = aRestaurantMenuBLL.GetRecipeNameById(itemDetails.ItemId);

                    }

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_leftString((cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                      recipeName) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    blankLine++;
                    string options = "";
                    List<RecipeOptionMD> aOption =
                        aRecipeOptionMdList.Where(
                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                            .ToList();
                    if (aOption.Count > 0)
                    {

                        foreach (RecipeOptionMD option in aOption)
                        {
                            if (!string.IsNullOrEmpty(option.Title))
                            {

                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_leftString("\r→" + option.Title + " " + " " + (option.Price).ToString("F02")) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {

                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_leftString("\r→No" + option.MinusOption) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                blankLine++;
                            }
                        }
                    }


                }

            }

            double amount = aORder.TotalCost;

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);
            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.get_alignmentString(aRestaurantOrder.OrderTime.ToString("hh:mmtt") + "  TOTAL  £" + amount.ToString("F02"), ("£" + amount.ToString("F02")).Length) + "\r\n";
            aPrintContentsMid.Add(aPrintContent);

            aPrintContent = new PrintContent();
            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
            aPrintContentsMid.Add(aPrintContent);


            if(aORder.CardAmount > 0)
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" PAID BY CARD") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }
                else
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("  CARD ORDER ") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }
            }
            else
            {
                if (aORder.Status.ToLower() == "paid")
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" PAID ORDER") + "\n";
                    aPrintContentsMid.Add(aPrintContent);

                }
                else
                {
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(" ORDER IS NOT PAID ") + "\n";
                    aPrintContentsMid.Add(aPrintContent);
                }

            }

            if(aORder.Comment != null)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aORder.Comment) + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

            }



            if (aRestaurantInformation.ShowOrderNumber > 0)
            {

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                aPrintContentsMid.Add(aPrintContent);

                aPrintContent = new PrintContent();
                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantOrder.OrderNo.ToString("D2")) + "\n";
                aPrintContentsMid.Add(aPrintContent);
            }


            string allpage = "";
            for (int i = 0; i < aPrintContentsMid.Count; i++)
            {
                allpage += aPrintContentsMid[i].StringLine;
            }

            //return allpage;
            if (printerPrintStatus)
            {

                PrintMethods kitPrintMethods = new PrintMethods(true, false, printer.printerMargin);
                kitPrintMethods.USBPrint(allpage, printer.PrinterName, printer.PrintCopy);


            }



        }

        public double GetTotalAmountDetails()
        {
            double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
            double amount2 = aRecipePackageMdList.Sum(a => (a.Qty * a.UnitPrice));
            double amount3 = aPackageItemMdList.Sum(a => a.Price);
            double amount4 = aRecipeMultipleMdList.Sum(a => a.Qty * a.UnitPrice);
            return GlobalVars.numberRound(Convert.ToDouble(amount1 + amount2 + amount3 + amount4), 2);
        }

        public void autoPrint(RestaurantOrder aRestaurantOrder,bool onlyKitchen= false)
        {

            try
            {
                List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();            
                bool needKitchenUpdate = false;
                foreach (PrinterSetup kprinter in PrinterSetups)
                {
                    if (onlyKitchen)
                    {
                        needKitchenUpdate = true;
                        if (kprinter.PrintStyle.ToLower() == "kitchen")
                        {
                            LoadAllSaveOrder();
                            KitchenPrint(aRestaurantOrder.Id, kprinter);
                        }
                    }
                    else
                    {
                        if (kprinter.PrintStyle.ToLower() == "kitchen")
                        {
                            needKitchenUpdate = true;
                            LoadAllSaveOrder();
                            KitchenPrint(aRestaurantOrder.Id, kprinter);
                        }
                        else
                        {

                            bool isBillPrint = kprinter.PrintStyle == "Bill" ? true : false;
                            PrintMethods tempPrintMethods = new PrintMethods(false, false, kprinter.printerMargin, isBillPrint);                            
                            string printStr = LoadAllOrder();
                            tempPrintMethods.USBPrint(printStr, kprinter.PrinterName, kprinter.PrintCopy);
                        }
                    }
                }

                if (needKitchenUpdate)
                {

                    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                    aRestaurantOrderBLL.UpdateKitchenStatusForAutoprint(aRestaurantOrder.Id, ItemIds);
                    ItemIds = new List<int>();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Printer not found.Please add kitchen printer.");
            }
        }
    }
}
