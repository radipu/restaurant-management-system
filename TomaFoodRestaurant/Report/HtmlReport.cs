using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using Newtonsoft.Json;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.Report
{

    public class HtmlReport
    {
        RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        RestaurantOrder aRestaurantOrder = new RestaurantOrder();

        public static List<OrderItemDetailsMD> aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
        public static List<RecipePackageMD> aRecipePackageMdList = new List<RecipePackageMD>();
        public static List<PackageItem> aPackageItemMdList = new List<PackageItem>();

        public static List<RecipeMultipleMD> aRecipeMultipleMdList = new List<RecipeMultipleMD>();
        public static List<MultipleItemMD> aMultipleItemMdList = new List<MultipleItemMD>();

        GeneralInformation aGeneralInformation = new GeneralInformation();
        List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static int OrderId = 0;
        List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
        PrintCopySetup aPrintCopySetup = new PrintCopySetup();
        public  HtmlReport(RestaurantInformation RestaurantInformation, RestaurantOrder saveOrder, GeneralInformation generalInformation)
        {
           try
           {
               aRestaurantInformation = RestaurantInformation;
               RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
               aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

               aRestaurantOrder = saveOrder;
               LoadAllSaveOrder();
               //LoadGeneralInformationIntoControl();
               GenerateDetails(saveOrder.Id, generalInformation);
           }
           catch (Exception ex)
           {

               MessageBox.Show(ex.GetBaseException().ToString());
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


                List<OrderItem> aOrderItems = aRestaurantOrderBLL.GetRestaurantOrderRecipeItems(aRestaurantOrder.Id);
                List<OrderPackage> aPackageItems = aRestaurantOrderBLL.GetRestaurantOrderPackage(aRestaurantOrder.Id);



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
                            aOrderItemDetails.ItemName = item.Name;
                            aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
                            aOrderItemDetails.OptionsIndex = optionIndex;
                            aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
                            aOrderItemDetails.Price = item.Price / item.Quantity;
                            aOrderItemDetails.Qty = item.Quantity;
                            aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                            aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
                            aOrderItemDetails.CatSortOrder = aReceipeCategoryButton.SortOrder;
                            aOrderItemDetails.TableNumber = aGeneralInformation.TableId;

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

        private void LoadSaveOption(OrderItem item, OrderItemDetailsMD aOrderItemDetails)
        {
            string[] optionList = item.Options_ids.Split(',');
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            if (optionList.Count() > 0)
            {
                for (int i = 0; i < optionList.Count(); i++)
                {
                    if (optionList[i] != null && optionList[i].Length > 0)
                    {
                        RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionId(Convert.ToInt32(optionList[i]));
                        if (recipe.RecipeOptionItemId > 0)
                        {
                            RecipeOptionMD aOptionMD = new RecipeOptionMD();
                            aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                            aOptionMD.TableNumber = 1;
                            aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                            aOptionMD.Title = recipe.Title;
                            //    aOptionMD.Type = recipe.RecipeOptionButton.Type;
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
        private double GetPackageItemPrice(int packageId, int optionIndex)
        {
            double sum = aPackageItemMdList.Where(a => a.PackageId == packageId && a.OptionsIndex == optionIndex).Sum(b => b.Price * b.Qty);
            return sum;
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
                            //if (deliveryButton.Text == "RES")
                            //{
                            //    aOptionMD.Price = recipe.InPrice;
                            //}
                            //else
                            //{
                            aOptionMD.Price = recipe.Price;
                            //  }

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
                                //if (deliveryButton.Text == "RES")
                                //{
                                //    aOptionMD.Price = recipe.InPrice;
                                //}
                                //else
                                //{
                                aOptionMD.Price = recipe.Price;
                                //}

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
                    aItem.Price = 0;
                    aItem.Qty = item.Quantity;
                    aItem.OptionName = string.IsNullOrEmpty(item.Options) ? itemButton.OptionName : item.Options;
                    aItem.PackageId = item.PackageId;
                    aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                    aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                    LoadSaveOptionForPackage(item, aItem);

                    aPackageItemMdList.Add(aItem);
                }
                else if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == 0) //item.RecipeId==aRecipePackage.OptionsIndex
                {

                    PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                    //    PackageItemButton itemButton = aVariousMethod.GetItemForPackageOrder(item.RecipeId,item.PackageId);
                    ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                    PackageItem aItem = new PackageItem();
                    aItem.ItemId = item.RecipeId;
                    aItem.ItemName = item.Name;
                    aItem.Price = 0;
                    aItem.Qty = item.Quantity;
                    aItem.OptionName = string.IsNullOrEmpty(item.Options) ? itemButton.OptionName : item.Options;
                    aItem.PackageId = item.PackageId;
                    aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                    aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                    LoadSaveOptionForPackage(item, aItem);
                    aPackageItemMdList.Add(aItem);
                }
            }

        }

        private void LoadSaveOptionForPackage(OrderItem item, PackageItem aOrderItemDetails)
        {
            string[] optionList = item.Options.Split(',');

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

            string[] optionList1 = item.MinusOptions.Split(',');

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
        private void LoadGeneralInformationIntoControl()
        {

       
            CustomerBLL aCustomerBll = new CustomerBLL();

            if (aGeneralInformation.CustomerId > 0)
            {
                RestaurantUsers aUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                GeneralInformationTemp.customerNameLabel.Text = aUser.Firstname + " " + aUser.Lastname;
            }
            else
            {
                GeneralInformationTemp.customerNameLabel.Text = "";
            }

            if (aRestaurantOrder.DeliveryTime != null)
            {
                GeneralInformationTemp.deliveryTimeLabel.Text = aRestaurantOrder.DeliveryTime.ToString();
            }
            if (aRestaurantOrder.OnlineOrder > 0)
            {
                GeneralInformationTemp.onlineOrderTimeLabel.Text = "Yes";
            }
            else
            {
                GeneralInformationTemp.onlineOrderTimeLabel.Text = "No";
            }

            if (aRestaurantOrder.OrderType == "Collect" || aRestaurantOrder.OrderType == "CLT" || aRestaurantOrder.OrderType == "WAIT")
            {
                GeneralInformationTemp.collectionRadioButton.Checked = true;
            }
            else if (aRestaurantOrder.OrderType == "DEL")
            {
                GeneralInformationTemp.deliveryRadioButton.Checked = true;
            }
            else
            {
                GeneralInformationTemp.dineInRadioButton.Checked = true;
            }

          

        }
        public string printHtml { get; set; }
        public string GenerateDetails(int result, GeneralInformation aGeneralInformation)
        {
            string printHeaderStr = "<div style='width:260px; font-family:tahoma, sans-serif'>";


            int papersize = 25;
            string reciept_font = "";

            RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
            RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

            int blankLine = 0;
            int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");


            reciept_font = aRestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 1).ToString();
            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();
            if (aRestaurantInformation.RecieptOption != "none")
            {
                printHeaderStr += "<div align='center' style='width: 250px; margin-bottom:5px;'><b>" + aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" + aRestaurantInformation.House + ", " + aRestaurantInformation.Address + "<br>" +
                aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" + aRestaurantInformation.VatRegNo + "</div>";
            }
            string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation);
            printHeaderStr += "<div  style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'><b>" + orderHistory + "</b></div></div>";
            printHeaderStr += "<div   style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'>" + DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</div></div>";


            if (aGeneralInformation.CustomerId > 0)
            {
                printHeaderStr += "<div style='float:left; max-width:250px;border-bottom:1px dashed; margin-bottom:5px; font-weight:bold; font-size:" + reciept_font + "px'>";
                RestaurantUsers aUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);


                printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";


                string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;


                string address = "";
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {
                    RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        if (ss.Count() > 0)
                        {address += "," + ss[0];
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


                if (aGeneralInformation.OrderType == "DEL")
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
                printHeaderStr += "<div style='float:left; max-width:185px; font-weight:bold; font-size:" + reciept_font + "px'>" + address + "</div>";
                if (!flag)
                {
                    printHeaderStr += "<div style='float:right; text-align:left; min-width:125px; font-weight:bold'>" + cell + "</div>";

                }

                printHeaderStr += "</div>";
            }


            string printStr = "";
            string printRecipeStr = "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;margin:0;padding:0;'>";
            string printFooterStr = "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;padding:0;margin:0;'>";


            //List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);

            aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
            aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

            // aOrderItemDetailsMDList = aOrderItemDetailsMDList.GroupBy(a => a.CategoryId).OrderBy(b => b.Key).SelectMany(c => c.OrderBy(d => d.CatSortOrder)).ToList();
            // aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();


            int catId = 0;
            bool startdas = false;
            foreach (OrderItemDetailsMD itemDetails in aOrderItemDetailsMDList)
            {

                if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                {
                    printRecipeStr += "<h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";

                    startdas = false;
                }

                if (aRestaurantInformation.MenuSeparation == 1 && catId != itemDetails.CategoryId && catId != 0)
                {
                    printRecipeStr += "<h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";

                }

                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:75%;'>" + itemDetails.Qty.ToString() + " " + itemDetails.ItemName + "</span><span style='text-align:right;float:right;width:22%;'>" + (itemDetails.Qty * itemDetails.Price).ToString("F02") + "</span></h3>";
                blankLine++;


                string options = "";
                List<RecipeOptionMD> aOption =
                    aRecipeOptionMdList.Where(
                        a =>
                            a.RecipeId == itemDetails.ItemId &&
                            a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                if (aOption.Count > 0)
                {

                    foreach (RecipeOptionMD option in aOption)
                    {
                        if (!string.IsNullOrEmpty(option.Title))
                        {
                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→" + option.Title + "</span><span style='text-align:right;float:right;width:22%;'>" + "" + "</span></h3>";
                            blankLine++;
                        }
                        if (!string.IsNullOrEmpty(option.MinusOption))
                        {
                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→No" + option.MinusOption + "</span><span style='text-align:right;float:right;width:22%;'>" + "" + "</span></h3>";
                            blankLine++;
                        }
                    }
                }




                if (aRestaurantInformation.MenuSeparation == 2)
                {
                    printRecipeStr += "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;'>&nbsp;</h3>";

                }


                catId = itemDetails.CategoryId;
                if (starterId == itemDetails.CategoryId)
                {
                    startdas = true;
                }



            }




            foreach (RecipePackageMD package in aRecipePackageMdList)
            {

                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:75%;'>" + package.Qty.ToString() + " " + package.PackageName + "</span><span style='text-align:right;float:right;width:22%;'>" + (package.Qty * package.UnitPrice).ToString("F02") + "</span></h3>";
                blankLine++;
                List<PackageItem> aPaItem = aPackageItemMdList.Where(a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex).ToList();
                List<PackageItem> aPaItemNew = new List<PackageItem>();
                foreach (PackageItem item in aPaItem)
                {
                    item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                    aPaItemNew.Add(item);
                }
                aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                foreach (PackageItem itemDetails in aPaItem)
                {
                    string packageItemPrice = itemDetails.Qty * itemDetails.Price > 0 ? (itemDetails.Qty * itemDetails.Price).ToString() : "";

                    printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + itemDetails.Qty.ToString() + "  " + itemDetails.ItemName + "</span><span style='text-align:right;float:right;width:22%;'>" + packageItemPrice + "</span></h3>";
                    blankLine++;
                    string options = "";
                    List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                    if (aOption.Count > 0)
                    {

                        foreach (RecipeOptionMD option in aOption)
                        {
                            if (!string.IsNullOrEmpty(option.Title))
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→" + option.Title + "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→No" + option.MinusOption + "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                blankLine++;
                            }

                        }
                    }


                }


            }


            foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
            {

                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:75%;'>" + package.Qty.ToString() + " " + package.MultiplePartName + "</span><span style='text-align:right;float:right;width:22%;'>" + (package.Qty * package.UnitPrice).ToString("F02") + "</span></h3>";
                blankLine++;
                List<MultipleItemMD> aPaItem =
                    aMultipleItemMdList.Where(
                        a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                        .ToList();
                int cnt = 0;
                foreach (MultipleItemMD itemDetails in aPaItem)
                {
                    cnt++;
                    string packageItemPrice = itemDetails.Qty * itemDetails.Price > 0
                        ? (itemDetails.Qty * itemDetails.Price).ToString()
                        : "";

                    printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " + itemDetails.ItemName + "</span><span style='text-align:right;float:right;width:22%;'>" + packageItemPrice + "</span></h3>";
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
                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→No" + option.Title + "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                blankLine++;
                            }
                            if (!string.IsNullOrEmpty(option.MinusOption))
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + "→No" + option.MinusOption + "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                blankLine++;
                            }

                        }
                    }


                }

            }



            double amount = GetTotalAmountDetails() + aGeneralInformation.CardFee -
                            aGeneralInformation.OrderDiscount - aGeneralInformation.ItemDiscount;


            if (blankLine < aRestaurantInformation.RecieptMinHeight)
            {
                for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                {
                    printRecipeStr += "<h3  style='text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                }


            }

            if (aGeneralInformation.OrderDiscount > 0)
            {

                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:35%;'>Discount</span><span style='text-align:right;float:right;width:62%;'>(" + aGeneralInformation.DiscountPercent.ToString("F02") + "%) £" + aGeneralInformation.OrderDiscount.ToString("F02") + "</span></h3>";


            }



            if (aGeneralInformation.CardFee > 0)
            {

                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:35%;'>S/C</span><span style='text-align:right;float:right;width:62%;'> £" + aGeneralInformation.CardFee.ToString("F02") + "</span></h3>";


            }

            if (aGeneralInformation.DeliveryCharge > 0)
            {

                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:35%;'>D/C</span><span style='text-align:right;float:right;width:62%;'> £" + aGeneralInformation.DeliveryCharge.ToString("F02") + "</span></h3>";
                amount += aGeneralInformation.DeliveryCharge;
            }


            printFooterStr += "<h3 style='font-weight:bold;margin:30px auto;padding-top:10px;border-top:1px dashed; font-size:" + reciept_font + "px'><span style='text-align:left;float:left;width:25%;'>" + DateTime.Now.ToString("hh:mmtt") + "</span><span style='text-align:right;float:right;width:72%;'>" + "TOTAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + amount.ToString("F02") + "</span></h3>";




            if (aRestaurantInformation.ThankYouMsg.Length > 5)
            {
                printFooterStr += "<h3  style='text-align:center;'>" + aRestaurantInformation.ThankYouMsg + "</h3>";

            }





            int printCopy = 1;

            printHeaderStr += "</div>";
            printRecipeStr += "</div>";
            printFooterStr += "<br/><br/></div>";
            printStr = printHeaderStr + printRecipeStr + printFooterStr;
            string str = "<html><head><style>html{width:100%;} body{width:300px;margin: 0 auto;}   h3 { padding: 0; margin: 0; }</style></head><body style='font-family:tahoma,sans-serif;padding:0;'>" + printStr + "</body></html>";
            printHtml = str;
            return printHtml;
            //webBrowser.DocumentText = str;
            //webBrowser.Refresh();                        
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

        private double GetTotalAmountDetails()
        {
            double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
            double amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
            double amount3 = aPackageItemMdList.Sum(a => a.Qty * a.Price);
            return (amount1 + amount2 + amount3);
        }

    }

   internal class GeneralInformationTemp
    {
       public static Label onlineOrderTimeLabel=null;
       public static RadioButton collectionRadioButton=null;
       public static RadioButton deliveryRadioButton=null;
       public static RadioButton dineInRadioButton=null;
       public static  Label customerNameLabel { get; set; }
       public static Label deliveryTimeLabel { get; set; }
    }


}
