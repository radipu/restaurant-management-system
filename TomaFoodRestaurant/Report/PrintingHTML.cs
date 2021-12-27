using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.Report
{
    public class PrintingHTML
    {
        public static string PackageItemHtml(List<PackageItem> packageItems)
        {
            // string heading = "<tr >" + "<td colspan='2'>" + packageItems[0].RecipePackageButton.PackageItemName + "</td>" + "</tr>";


            string table = "<table>";

            string itemLine = "";
            foreach (PackageItem item in packageItems)
            {

                itemLine += "<tr>" + "<td>" + item.Qty + "</td>" + "<td>" + item.ItemName + "</td>" + "</tr>";

            }

            table += itemLine + "</table>";


            return table;
        }

        public void PrintingOrder(RestaurantOrder aRestaurantOrder, bool isPrint, bool status, MainFormView mainFormView, PaymentDetails aPaymentDetails,bool IsKitichinePrint=false)
        {
            try
            {
                RestaurantInformation aRestaurantInformation = GlobalSetting.RestaurantInformation;
                int starterId = new RestaurantMenuBLL().GetCategoryByName("Starter");
                GeneralInformation aGeneralInformation = mainFormView.aGeneralInformation;
                DataTable dataTable = mainFormView.dataTable;

                List<ReceipeTypeButton> aReceipeTypeButton = new RestaurantMenuBLL().GetRecipeType().ToList();
               

                string printHeaderStr = "<div style='width:260px; font-family:tahoma, sans-serif'>";
                int papersize = 25;
                string reciept_font = "";
                int blankLine = 0;
                List<ReportData> listOfReport = new List<ReportData>();
                reciept_font = aRestaurantInformation.RecieptFont;
                reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
                string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 1).ToString();
                string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();
                if (aRestaurantInformation.RecieptOption == "logo_title")
                {
                    string path = @"Image/" + aRestaurantInformation.Id + "_website_logo.png";

                    if (File.Exists(path))
                    {
                        var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(path)));

                        printHeaderStr += "<div align='center' style='width: 250px;'><img style='width:250px; height: 60px' src='" + imageString + "'></div>";

                    }


                }
                if (aRestaurantInformation.RecieptOption != "none")
                {
                    printHeaderStr += "<div align='center' style='width: 250px; margin-bottom:5px;'><b>" +
                                      aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" +
                                      aRestaurantInformation.House + ", " + aRestaurantInformation.Address + "<br>" +
                                      aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" +
                                      aRestaurantInformation.VatRegNo + "</div>";
                }
                string orderHistory = new RestaurantOrderBLL().GetOrderHistory(papersize, aRestaurantOrder.Id, aGeneralInformation, mainFormView.timeButton.Text);
                printHeaderStr +=
                    "<div  style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'><b>" +
                    orderHistory + "</b></div></div>";

                if (isPrint)
                {
                    printHeaderStr +=
                        "<div   style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'>" +
                        DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</div></div>";
                }
                

                if (aGeneralInformation.CustomerId > 0)
                {
                    printHeaderStr +=
                        "<div style='float:left; max-width:250px;border-bottom:1px dashed; margin-bottom:5px; font-weight:bold; font-size:" +
                        reciept_font + "px'>";

                    RestaurantUsers aUser = mainFormView.restaurantUsers;
                    if (aUser.Id == 0)
                    {
                        UserLoginBLL aCustomerBll = new UserLoginBLL();
                       aUser = aCustomerBll.GetUserByUserId(aGeneralInformation.CustomerId);
                    }
                    printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";
                    string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;
                    string address = "";
                    bool flag = false;
                    if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                    {
                        //RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                        if (!string.IsNullOrEmpty(aRestaurantOrder.DeliveryAddress))
                        {
                            string[] ss = aRestaurantOrder.DeliveryAddress.Split(',');
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

                    if (aGeneralInformation.DeliveryCharge > 0 && mainFormView.deliveryButton.Text == "DEL" && mainFormView.deliveryButton.BackColor == Color.Black)
                    {
                        if (address == "")
                        {
                            if (string.IsNullOrEmpty(aUser.FullAddress))
                            {

                                address += "" + aUser.House + " " + aUser.Address;
                                address += " " + aUser.City + "<br>" + aUser.Postcode;
                            }
                            else
                            {

                                address += "" + aUser.House + " " + aUser.FullAddress + "<br>" + aUser.Postcode;

                            }
                        }
                    }



                    printHeaderStr += "<div style='float:left; max-width:185px; font-weight:bold; font-size:" + reciept_font +
                                      "px'>" + address + "</div>";
                    if (!flag)
                    {
                        printHeaderStr += "<div style='float:right; text-align:left; min-width:125px; font-weight:bold'>" +
                                          cell + "</div>";

                    }

                    printHeaderStr += "</div>";
                }


                foreach (PrinterSetup printer in mainFormView.PrinterSetups)
                {
                    if (printer.PrintStyle != "Receipt") continue;

                    List<OrderItemDetailsMD> OrderMdList = new List<OrderItemDetailsMD>();
                    List<RecipePackageMD> packageMds = new List<RecipePackageMD>();
                    List<PackageItem> mPackageItems = new List<PackageItem>();
                    List<RecipeMultipleMD> aOrderItemDetailsMDList = new List<RecipeMultipleMD>();
                    List<RecipeOptionMD> recipeOptionMds = new List<RecipeOptionMD>();

                    List<RecipeTypeDetails> aListTypeDetails = new List<RecipeTypeDetails>();

                    string printStr = "";
                    string printRecipeStr =
                        "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;margin:0;padding:0;'>";
                    string printFooterStr =
                        "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;padding:0;margin:0;'>";

                    //List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);

                    List<PrintContent> aPrintContentsHead = new List<PrintContent>();
                    List<PrintContent> aPrintContentsMid = new List<PrintContent>();

                    PrintContent aPrintContent = new PrintContent();

                    PrintFormat aPrintFormat = new PrintFormat(papersize);
                    PrintFormat aPrintFormat1 = new PrintFormat(papersize - 10);
                    PrintFormat aPrintFormat2 = new PrintFormat(papersize - 2);
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = "\r\n" + aPrintFormat.CreateDashedLine();
                    aPrintContentsHead.Add(aPrintContent);

                    aPrintContent = new PrintContent();
                    //aPrintContent.StringLine = "\r\n" + orderHistory;
                    aPrintContentsHead.Add(aPrintContent);
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = "\r\n" + aPrintFormat.CreateDashedLine();
                    aPrintContentsHead.Add(aPrintContent);


                    dataTable = dataTable.AsEnumerable().OrderBy(a => a["SortOrder"]).CopyToDataTable();
                    var maxStater = dataTable.AsEnumerable().Count(a => a["Cat"].ToString() == starterId.ToString());
                    if (maxStater > 0)
                    {
                        dataTable = dataTable.AsEnumerable().OrderByDescending(a => a["SortOrder"]).CopyToDataTable();
                        //   var test = dataTable.AsEnumerable().GroupBy(a => Convert.ToInt32(a["Cat"]) == starterId).ToList();
                    }
                    bool startdas = false;
                    
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Label recipeTypeAmountlabel = new Label();
                        Label receipTypeLabel = new Label();

                        recipeTypeAmountlabel.Text = Convert.ToDouble(dataTable.Rows[i]["Total"]).ToString("N");
                        //int kitichineDone = Convert.ToInt16(dataTable.Rows[i]["KitichineDone"]);
                        receipTypeLabel.Text = dataTable.Rows[i]["EditName"].ToString();
                        var CatId = dataTable.Rows[i]["Cat"];
                        var GroupTitle = new RestaurantMenuBLL().GetAllCategory().FirstOrDefault(a => a.CategoryId == Convert.ToInt32(CatId));
                      

                        string IsPackgage = dataTable.Rows[i]["Group"].ToString();

                        if (IsPackgage == "Package")
                        {
                            List<PackageItem> packageItem = (List<PackageItem>)dataTable.Rows[i]["Package"];

                            string packageItemString = "";
                            foreach (PackageItem item in packageItem)
                            {
                                string optionName = "";
                                string kitchineOption = "";

                                if (item.PackageItemOptionList != null)
                                {
                                    if (item.PackageItemOptionList.Count > 0)
                                    {
                                        for (int j = 0; j < item.PackageItemOptionList.Count; j++)
                                        {
                                            optionName += "→" + "&nbsp;" + item.PackageItemOptionList[j].Title + (item.PackageItemOptionList[j].Price > 0 ? "+" + item.PackageItemOptionList[j].Price.ToString("F02") : "") + "<br/>";
                                            kitchineOption += "→" + "&nbsp;" + item.PackageItemOptionList[j].Title + "<br/>";


                                        }
                                        optionName = optionName.Remove(optionName.Length - 5);
                                        kitchineOption = kitchineOption.Remove(kitchineOption.Length - 5);


                                    }

                                }
                                packageItemString += "<h3 style='font-weight:bold;margin: 0px 10px; font-size:" + reciept_font_small +
                                                     "px'><span style='text-align:left;'>" +
                                                     item.Qty +
                                                     "</span><span style='text-align:left;'>&nbsp;&nbsp;" +
                                                     item.ItemName + "</br>" + optionName + "</span></h3>";
                                blankLine++;

                                listOfReport.Add(new ReportData
                                {

                                    // ItemName = gridViewAddtocard.GetRowCellValue(i, "Name").ToString(),
                                    Name = receipTypeLabel.Text,
                                    Index = i,
                                    Qty = item.Qty,ItemName = item.ItemName,
                                    ItemQty = Convert.ToInt32(dataTable.Rows[i]["QTY"]),
                                    Price = Convert.ToDouble(dataTable.Rows[i]["Total"]),
                                    OptionName = optionName,
                                    GroupTitle = dataTable.Rows[i]["GroupName"].ToString(),
                                    Group = dataTable.Rows[i]["Group"].ToString(),
                                    Id = i.ToString(),
                                    CatId = item.CategoryId,
                                    KitchineOption = kitchineOption,
                                    SortOrder = item.CategorySortOrder,
                                    ReceipeTypeId = 0
                                });
                            }
                            packageMds.Add(new RecipePackageMD()
                            {
                                RecipeTypeId = Convert.ToInt16(dataTable.Rows[i][7]),
                                PackageName = receipTypeLabel.Text,
                                Qty = Convert.ToInt32(dataTable.Rows[i]["QTY"])
                                ,
                                packageItemList = packageItem ,
                                PackageId = Convert.ToInt16(dataTable.Rows[i][1]),
                              KitichineDone = Convert.ToInt16(dataTable.Rows[i]["KitichineDone"])
                            });
                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:70%;'>" +
                                              Convert.ToInt32(dataTable.Rows[i]["QTY"]) + "" + receipTypeLabel.Text +
                                              "</span><span style='text-align:right;float:right;width:22%;'>" +
                                              recipeTypeAmountlabel.Text +
                                              "</span></h3>";
                            blankLine++;
                            printRecipeStr += packageItemString;
                        }
                        else
                        {
                            if (IsPackgage == "MultipleItem")
                            {
                                string multipleItem = Convert.ToString(dataTable.Rows[i]["Name"]);
                                receipTypeLabel.Text = multipleItem;
                                receipTypeLabel.Text = Regex.Replace(receipTypeLabel.Text, "<hr/>", "", RegexOptions.Singleline);
                                printRecipeStr += "<p style='font-weight:bold;'>" + "<span style='text-align:right;float:left;'>" +
                                                  Convert.ToInt32(dataTable.Rows[i]["QTY"]) + "</span>" + "<span style='text-align:left;margin-left:5px;float:left;width:72%;'>" +
                                                  receipTypeLabel.Text + "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  recipeTypeAmountlabel.Text + "</span></p>";

                                listOfReport.Add(new ReportData
                                {

                                    Name = receipTypeLabel.Text,
                                    Index = i,
                                    Qty = 0,
                                    ItemName = receipTypeLabel.Text,
                                    ItemQty = Convert.ToInt16(mainFormView.gridViewAddtocard.GetRowCellValue(i, "QTY")),
                                    Price = Convert.ToDouble(mainFormView.gridViewAddtocard.GetRowCellValue(i, "Total")),
                                    OptionName = "",
                                    GroupTitle ="",
                                    KitchineOption = "",
                                    Group = IsPackgage,
                                    Id = "-1",
                                    CatId = 0,
                                    SortOrder = -1,
                                    ReceipeTypeId = Convert.ToInt32(mainFormView.gridViewAddtocard.GetRowCellValue(i, "ReceipTypeId"))});

                            }
                            string name = Convert.ToString(dataTable.Rows[i]["Name"]).Replace("&rarr;", ",");

                            string removeString = Regex.Replace(name, @"<[^>]+>|", "");
                            string optionName = "";
                            string kitchineOptionName = "";
                            var option = dataTable.Rows[i]["OptionId"];
                            List<OptionJson> listOption=new List<OptionJson>();
                            if (option != System.DBNull.Value)
                            {

                                listOption = (List<OptionJson>)option;
                                int j = 0; 
                              

                                foreach (OptionJson optionJson in listOption)
                                {
                                    j++;

                                    string optionMenu = "&rarr;" + "&nbsp;" + optionJson.optionQty + " " + optionJson.optionName + (optionJson.optionPrice > 0 ? "+" + optionJson.optionPrice.ToString("F02") : "");
                                    string optionMenukitchine = "&rarr;" + "&nbsp;" + optionJson.optionQty + " " + optionJson.optionName;

                                    if (optionJson.optionPrice > 0)
                                    {
                                        optionMenu = "&rarr;" + "&nbsp;" + optionJson.optionQty + " " + optionJson.optionName + (optionJson.optionPrice > 0 ? "+" + optionJson.optionPrice.ToString("F02") : "");
                                    }

                                    if (listOption.Count == j)
                                    {
                                        optionName += optionMenu;
                                        kitchineOptionName += optionMenukitchine;
                                    }
                                    else
                                    {
                                        optionName += optionMenu + "<br/>";

                                        kitchineOptionName += optionMenukitchine + "<br/>";
                                    }
                                    
                                }

                            }
                            bool recipeTypelabel = false;
                            if (recipeTypelabel)
                            {
                                if (recipeTypelabel == false)
                                {

                                    printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                      "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                      receipTypeLabel.Text +
                                                      "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                      recipeTypeAmountlabel.Text + "</span></h3>";

                                }
                                else
                                {
                                    printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                      "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                      receipTypeLabel.Text +
                                                      "</span><span style='text-align:right;float:right;width:8%;'></span></h3>";

                                }
                                printRecipeStr +=
                                    "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;width:75%;'>&nbsp;</h3>";
                            }
                            int catId = 0;



                            if (GroupTitle != null && IsPackgage != "MultipleItem")
                            {

                                if (isPrint)
                                {

                                    printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                      "px'><span style='text-align:left;float:left;width:70%;'>" +
                                                      Convert.ToInt16(dataTable.Rows[i]["QTY"]) + " " + receipTypeLabel.Text +
                                                      "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                      recipeTypeAmountlabel.Text +
                                                      "</span></h3>";
                                    blankLine++;
                                }
                                else
                                {
                                    printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                      "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                      Convert.ToInt16(dataTable.Rows[i]["QTY"]) + " " + receipTypeLabel.Text +
                                                      "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                      recipeTypeAmountlabel.Text +
                                                      "</span></h3>";
                                    blankLine++;
                                }

                                if (optionName != string.Empty)
                                {
                                    printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                      reciept_font_small +
                                                      "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                      optionName +
                                                      "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                      "" + "</span></h3>";
                                    blankLine++;
                                }
                                if (aRestaurantInformation.MenuSeparation == 2)
                                {
                                    printRecipeStr +=
                                        "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;'>&nbsp;</h3>";
                                }

                                if (starterId == GroupTitle.CategoryId)
                                {
                                    maxStater--;
                                }
                                if (aRestaurantInformation.MenuSeparation == 3 && starterId == GroupTitle.CategoryId && maxStater == 0)
                                {
                                    printRecipeStr += "<h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";

                                }

                                listOfReport.Add(new ReportData
                                {

                                    Name = receipTypeLabel.Text,
                                    Index = i,
                                    Qty = 0,
                                    ItemName = receipTypeLabel.Text,
                                    ItemQty = Convert.ToInt16(mainFormView.gridViewAddtocard.GetRowCellValue(i, "QTY")),
                                    Price = Convert.ToDouble(mainFormView.gridViewAddtocard.GetRowCellValue(i, "Total")),
                                    OptionName = optionName,
                                    GroupTitle = mainFormView.gridViewAddtocard.GetRowCellValue(i, "GroupName").ToString(),
                                    KitchineOption = kitchineOptionName,
                                    Group = dataTable.Rows[i]["Group"].ToString(),
                                    Id = "-1",
                                    CatId = GroupTitle.CategoryId,
                                    SortOrder = GroupTitle.SortOrder,
                                    ReceipeTypeId = GroupTitle.ReceipeTypeId
                                });
                                OrderMdList.Add(new OrderItemDetailsMD
                                {
                                    CategoryId = GroupTitle.CategoryId,
                                    CatSortOrder = GroupTitle.SortOrder,
                                    RecipeTypeId = GroupTitle.ReceipeTypeId,
                                  
                                    Qty = Convert.ToInt16(dataTable.Rows[i]["QTY"]),
                                    ItemId = GroupTitle.ReceipeTypeId,
                                    ItemName = receipTypeLabel.Text,
                                    KitchenDone = Convert.ToInt16(dataTable.Rows[i]["KitichineDone"]),
                                    ItemOption = listOption.ToList()


                                });
                                ReceipeTypeButton receipeTypeButton=new ReceipeTypeButton();
                                receipeTypeButton.SortOrder = GroupTitle.SortOrder;
                                receipeTypeButton.Text = aReceipeTypeButton.FirstOrDefault(a=>a.TypeId==GroupTitle.ReceipeTypeId).Text;
                                if (aListTypeDetails.Count(a => a.RecipeTypeId == GroupTitle.ReceipeTypeId)==0)
                                {
                                    aListTypeDetails.Add(new RecipeTypeDetails
                                    {

                                        ReceipeTypeButton = receipeTypeButton,
                                        recipeTypeAmountlabel = recipeTypeAmountlabel,
                                        recipeTypelabel = receipTypeLabel,
                                        RecipeTypeId = GroupTitle.ReceipeTypeId
                                    });
                                }
                               
                            }

                          }
                        

                    }

                    if (aRestaurantOrder.isKitchenPrint && !(isPrint || aRestaurantOrder.isFinalize))
                    {
                        mainFormView.GenerateKitchenCopy(OrderMdList, packageMds, aListTypeDetails, listOfReport, aRestaurantOrder.Id);

                        if (IsKitichinePrint)
                        {
                            continue;
                        }
                        // KitchinePrint(listOfReport, mainFormView.PrinterSetups, aRestaurantOrder, aGeneralInformation, mainFormView.timeButton.Text);

                    }
                    
                   
                    double amount = mainFormView.GetTotalAmountDetails();

                    if (blankLine < aRestaurantInformation.RecieptMinHeight)
                    {
                        for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                        {printRecipeStr +=
                                "<h3  style='text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                        }


                    }

                    if (aGeneralInformation.OrderDiscount > 0)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>Discount</span><span style='text-align:right;float:right;width:62%;'>(" +
                                          aGeneralInformation.DiscountPercent.ToString("F02") + "%) £" +
                                          aGeneralInformation.OrderDiscount.ToString("F02") + "</span></h3>";


                    }



                    if (aGeneralInformation.CardFee > 0)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>S/C </span><span style='text-align:right;float:right;width:62%;'> £" +
                                          aGeneralInformation.CardFee.ToString("F02") + "</span></h3>";


                    }

                    if (aGeneralInformation.DeliveryCharge > 0 && mainFormView.deliveryButton.Text == "DEL" &&
                        mainFormView.deliveryButton.BackColor == Color.Black && mainFormView.collectionButton.BackColor != Color.Black)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>D/C</span><span style='text-align:right;float:right;width:62%;'> £" +
                                          aGeneralInformation.DeliveryCharge.ToString("F02") + "</span></h3>";
                    }

                    if (mainFormView.customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(mainFormView.customTotalTextBox.Text) > 0)
                    {
                        amount = Convert.ToDouble(mainFormView.customTotalTextBox.Text);
                    }


                    printFooterStr +=
                        "<h3 style='font-weight:bold;margin:30px auto;padding-top:10px;border-top:1px dashed; font-size:" +
                        reciept_font + "px'><span style='text-align:left;float:left;width:30%;'>" +
                        DateTime.Now.ToString("h:mm tt") + "</span><span style='text-align:right;float:right;width:70%;'>" +
                        "TOTAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + amount.ToString("F02") + "</span></h3>";


                    if (aRestaurantOrder.OnlineOrder > 0)
                    {
                        if (aRestaurantOrder.CardAmount > 0)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aRestaurantOrder.CardAmount.ToString("F02") + "</span></h3>";

                        }
                        else
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>ORDER NOT PAID</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></h3>";

                        }


                    }
                    else
                    {

                        if (aPaymentDetails.CashAmount > 0 && aPaymentDetails.CardAmount > 0 && status)
                        {

                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";

                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";

                        }
                        else if (status)
                        {


                            if (aPaymentDetails.CardAmount > 0)
                            {

                                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                                  "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";

                            }

                            else
                            {
                                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                                  "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";
                            }

                        }
                        if (status)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>CHANGE</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.ChangeAmount.ToString("F02") + "</span></h3>";
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px;border-top:1px dashed;'><span style='text-align:center;float:left;width:95%;'>PAID ORDER </span></h3>";
                        }


                    }


                    if (mainFormView.commentTextBox.Text != "Comment" && mainFormView.commentTextBox.Text.Trim().Length > 0)
                    {
                        printFooterStr += "<h3  style='border-bottom:1px dashed;text-align:center;'>" + mainFormView.commentTextBox.Text +
                                          "</h3>";

                    }
                    if (aRestaurantInformation.ThankYouMsg.Length > 5)
                    {
                        printFooterStr += "<h3  style='text-align:center;'>" + aRestaurantInformation.ThankYouMsg + "</h3>";

                    }

                    if (isPrint && aGeneralInformation.Person > 1)
                    {
                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'>" + "Total split " +
                                          aGeneralInformation.Person + " ways £" +
                                          (amount / aGeneralInformation.Person).ToString("F02") + "</h3>";


                    }



                    int printCopy = 1;
                    if (printer.PrintStyle == "Receipt")
                    {
                        if (aGeneralInformation.OrderType == "IN")
                        {
                            printCopy = aRestaurantInformation.DineInPrintCopy;
                        }
                        else if (aGeneralInformation.OrderType == "Takeaway" || aGeneralInformation.OrderType == "CLT")
                        {
                            printCopy = aRestaurantInformation.PrintCopy;
                        }
                        else
                        {
                            printCopy = aRestaurantInformation.DelPrintCopy;
                        }

                    }



                    printHeaderStr += "</div>";
                    printRecipeStr += "</div>";
                    printFooterStr += "<br/><br/></div>";
                    printStr = printHeaderStr + printRecipeStr + printFooterStr;

                    MainFormView.SetDefaultPrinter(printer.PrinterName);


                    for (int i = 0; i < printCopy; i++)
                    {
                        try
                        {

                            string str =
                                "<html><head><style>html, body { padding: 0; margin: 0 }</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" +
                                printStr + "</body></html>";
                            WebBrowser wbPrintString = new WebBrowser() { DocumentText = string.Empty };
                            wbPrintString.Document.Write(str);
                            wbPrintString.Document.Title = "";



                            string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
                            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);

                            if (key != null)
                            {
                                //string old_footer = key.GetValue("footer");//string old_header = key.GetValue("header");
                                key.SetValue("footer", "");
                                key.SetValue("header", "");
                                key.SetValue("margin_bottom", "0");
                                key.SetValue("margin_left", "0.15");
                                key.SetValue("margin_right", "0");
                                key.SetValue("margin_top", "0");

                                //wbPrintString.ShowPrintDialog();

                                wbPrintString.Print();
                                wbPrintString.Dispose();

                            }

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Selected printer not exist!", "Printer Setup Warning", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }



            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetBaseException().ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        public void PrintingOrderForSinglePage(RestaurantOrder aRestaurantOrder, bool isPrint, bool status, mainForm mainFormView, PaymentDetails aPaymentDetails)
        {
            try
            {
                string printHeaderStr = "<div style='width:260px; font-family:tahoma, sans-serif'>";

                int papersize = 25;
                string reciept_font = "";
                if (aRestaurantOrder.isKitchenPrint && !(isPrint || aRestaurantOrder.isFinalize))
                {
                    mainFormView.GenerateKitchenCopy(aRestaurantOrder.Id, status, aPaymentDetails);
                }
                GeneralInformation aGeneralInformation = mainFormView.aGeneralInformation;
                RestaurantUsers aUser = mainFormView.restaurantUsers;

                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();


                RestaurantInformation aRestaurantInformation = GlobalSetting.RestaurantInformation;


                int blankLine = 0;
                int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");

                List<RecipeTypeDetails> aListTypeDetails = mainFormView.orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();

                reciept_font = aRestaurantInformation.RecieptFont;
                reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
                string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 1).ToString();
                string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();
                if (aRestaurantInformation.RecieptOption != "none")
                {
                    printHeaderStr += "<div align='center' style='width: 250px; margin-bottom:5px;'><b>" +
                                      aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" +
                                      aRestaurantInformation.House + ", " + aRestaurantInformation.Address + "<br>" +
                                      aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" +
                                      aRestaurantInformation.VatRegNo + "</div>";
                }

                string orderHistory = aVariousMethod.GetOrderHistory(papersize, aRestaurantOrder.Id, aGeneralInformation, mainFormView.timeButton.Text);
                printHeaderStr +=
                    "<div  style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'><b>" +
                    orderHistory + "</b></div></div>";
                if (isPrint)
                {
                    printHeaderStr +=
                        "<div   style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'>" +
                        DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</div></div>";
                }


                if (aGeneralInformation.CustomerId > 0)
                {
                    printHeaderStr +=
                        "<div style='float:left; max-width:250px;border-bottom:1px dashed; margin-bottom:5px; font-weight:bold; font-size:" +
                        reciept_font + "px'>";


                    printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";


                    string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;


                    string address = "";
                    bool flag = false;
                    if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                    {

                        if (!string.IsNullOrEmpty(aRestaurantOrder.DeliveryAddress))
                        {
                            string[] ss = aRestaurantOrder.DeliveryAddress.Split(',');
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


                    if (aGeneralInformation.DeliveryCharge > 0 && mainFormView.deliveryButton.Text == "DEL" &&
                        mainFormView.deliveryButton.BackColor == Color.Black)
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



                    printHeaderStr += "<div style='float:left; max-width:185px; font-weight:bold; font-size:" + reciept_font +
                                      "px'>" + address + "</div>";
                    if (!flag)
                    {
                        printHeaderStr += "<div style='float:right; text-align:left; min-width:125px; font-weight:bold'>" +
                                          cell + "</div>";

                    }

                    printHeaderStr += "</div>";
                }
                else if (mainFormView.customerTextBox.Text != "" && mainFormView.customerTextBox.Text != "Search Customer")
                {
                    printHeaderStr +=
                        "<div  style='width: 250px;border-bottom:1px dashed;margin-bottom:10px; '><div style='text-align:left;font-size:" +
                        reciept_font + "px'><b>" + mainFormView.customerTextBox.Text + "</b></div></div>";
                }


                foreach (PrinterSetup printer in mainFormView.PrinterSetups)
                {
                    string printStr = "";
                    string printRecipeStr =
                        "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;margin:0;padding:0;'>";
                    string printFooterStr =
                        "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;padding:0;margin:0;'>";

                    if (printer.PrintStyle != "Receipt") continue;
                    Dictionary<int,string> recipeTypes = mainFormView.GetRecipeTypes(printer);

                    mainForm.aOrderItemDetailsMDList = mainForm.aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ThenBy(a => a.CatSortOrder).ToList();

                    // aOrderItemDetailsMDList = aOrderItemDetailsMDList.GroupBy(a => a.CategoryId).OrderBy(b => b.Key).SelectMany(c => c.OrderBy(d => d.CatSortOrder)).ToList();
                    // aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
                    
                    int catId = 0;
                    bool startdas = false;
                    try
                    {
                        aListTypeDetails = aListTypeDetails.OrderBy(a => a.ReceipeTypeButton.SortOrder).ToList();

                    }
                    catch (Exception es)
                    {
                    }

                    foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                    {
                        if (typeDetails.recipeTypelabel.Visible)
                        {
                            if (typeDetails.typeflowLayoutPanel1.Visible == false)
                            {

                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                  typeDetails.recipeTypelabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  typeDetails.recipeTypeAmountlabel.Text + "</span></h3>";

                            }
                            else
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                  typeDetails.recipeTypelabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:8%;'></span></h3>";

                            }
                            printRecipeStr +=
                                "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;width:75%;'>&nbsp;</h3>";
                        }
                        if (typeDetails.typeflowLayoutPanel1.Visible)
                        {

                            foreach (OrderItemDetailsMD itemDetails in mainForm.aOrderItemDetailsMDList)
                            {

                                if (itemDetails.RecipeTypeId == typeDetails.RecipeTypeId && recipeTypes.ContainsKey(itemDetails.RecipeTypeId))
                                {
                                    if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                                    {
                                        printRecipeStr +=
                                            "<h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                                        startdas = false;
                                    }
                                    //if (aRestaurantInformation.MenuSeparation == 1 && catId != itemDetails.CategoryId && catId != 0)
                                    //{
                                    //    printRecipeStr += "<h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                                    //}


                                    if (isPrint)
                                    {

                                        printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                          "px'><span style='text-align:left;float:left;width:70%;'>" +
                                                          itemDetails.Qty + " " + itemDetails.ItemFullName +
                                                          "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                          (itemDetails.Qty * itemDetails.Price).ToString("F02") +
                                                          "</span></h3>";
                                        blankLine++;
                                    }
                                    else
                                    {
                                        printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                          "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                          itemDetails.Qty.ToString() + " " + itemDetails.ItemName +
                                                          "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                          (itemDetails.Qty * itemDetails.Price).ToString("F02") +
                                                          "</span></h3>";
                                        blankLine++;
                                    }

                                    string options = "";
                                    List<RecipeOptionMD> aOption = mainFormView.aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                                    if (aOption.Count > 0)
                                    {

                                        foreach (RecipeOptionMD option in aOption)
                                        {
                                            if (!string.IsNullOrEmpty(option.Title))
                                            {
                                                if (startdas)
                                                {

                                                }
                                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                                  reciept_font_small +
                                                                  "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                                  "→" + option.Title + "<span style='text-style:italic'>" + (option.Price > 0 ? "+" + option.Price.ToString("F02") : "") + "</span>" +
                                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                                  "" + "</span></h3>";
                                                blankLine++;
                                            } if (!string.IsNullOrEmpty(option.MinusOption))
                                            {
                                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                                  reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                                  "→No" + option.MinusOption +
                                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                                  "" + "</span></h3>";
                                                blankLine++;
                                            }
                                        }
                                    }
                                    if (aRestaurantInformation.MenuSeparation == 2)
                                    {
                                        printRecipeStr +=
                                            "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;'>&nbsp;</h3>";
                                    }


                                    if (starterId == itemDetails.CategoryId)
                                    {
                                        startdas = true;
                                    }



                                }


                            }
                        }

                    }



                    foreach (RecipePackageMD package in mainForm.aRecipePackageMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {

                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:75%;'>" +
                                              package.Qty.ToString() + " " + package.PackageName +
                                              "</span><span style='text-align:right;float:right;width:22%;'>" +
                                              (package.Qty * package.UnitPrice).ToString("F02") + "</span></h3>";
                            blankLine++;
                            List<PackageItem> aPaItem =
                                mainForm.aPackageItemMdList.Where(
                                        a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                                    .ToList();

                            // Need to be process Change ************************************

                            List<PackageItem> aPaItemNew = new List<PackageItem>();
                            foreach (PackageItem item in aPaItem)
                            {
                                item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                aPaItemNew.Add(item);
                            }
                            aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();
                            // Need to be process Change ***********************************


                            foreach (PackageItem itemDetails in aPaItem)
                            {
                                string packageItemPrice = itemDetails.Qty * itemDetails.Price > 0
                                    ? (itemDetails.Qty * itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                  itemDetails.Qty + "  " + itemDetails.ItemName +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  packageItemPrice + "</span></h3>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    mainFormView.aRecipeOptionMdList.Where(
                                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {

                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→" + option.Title +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→No" + option.MinusOption +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }

                                    }
                                }


                            }


                        }
                    }


                    foreach (RecipeMultipleMD package in mainForm.aRecipeMultipleMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {
                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:75%;'>" +
                                              package.Qty.ToString() + " " + package.MultiplePartName +
                                              "</span><span style='text-align:right;float:right;width:22%;'>" +
                                              (package.Qty * package.UnitPrice).ToString("F02") + "</span></h3>";
                            blankLine++;
                            List<MultipleItemMD> aPaItem = mainForm.aMultipleItemMdList.Where(a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex).ToList();
                            int cnt = 0;
                            foreach (MultipleItemMD itemDetails in aPaItem)
                            {
                                cnt++;
                                string packageItemPrice = itemDetails.Qty * itemDetails.Price > 0
                                    ? (itemDetails.Qty * itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                  (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                                  itemDetails.ItemName +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  packageItemPrice + "</span></h3>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    mainFormView.aRecipeOptionMdList.Where(
                                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {

                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→No" + option.Title +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→No" + option.MinusOption +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }

                                    }
                                }


                            }


                        }
                    }





                    double amount = mainFormView.GetTotalAmountDetails() + aGeneralInformation.CardFee -
                                    aGeneralInformation.OrderDiscount - aGeneralInformation.ItemDiscount;


                    if (blankLine < aRestaurantInformation.RecieptMinHeight)
                    {
                        for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                        {
                            printRecipeStr +=
                                "<h3  style='text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                        }


                    }

                    if (aGeneralInformation.OrderDiscount > 0)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>Discount</span><span style='text-align:right;float:right;width:62%;'>(" +
                                          aGeneralInformation.DiscountPercent.ToString("F02") + "%) £" +
                                          aGeneralInformation.OrderDiscount.ToString("F02") + "</span></h3>";


                    }



                    if (aGeneralInformation.CardFee > 0)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>S/C </span><span style='text-align:right;float:right;width:62%;'> £" +
                                          aGeneralInformation.CardFee.ToString("F02") + "</span></h3>";


                    }

                    if (aGeneralInformation.DeliveryCharge > 0 && mainFormView.deliveryButton.Text == "DEL" &&
                        mainFormView.deliveryButton.BackColor == Color.Black && mainFormView.collectionButton.BackColor != Color.Black)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>D/C</span><span style='text-align:right;float:right;width:62%;'> £" +
                                          aGeneralInformation.DeliveryCharge.ToString("F02") + "</span></h3>";
                        amount += aGeneralInformation.DeliveryCharge;
                    }

                    if (mainFormView.customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(mainFormView.customTotalTextBox.Text) > 0)
                    {
                        amount = Convert.ToDouble(mainFormView.customTotalTextBox.Text);
                    }


                    printFooterStr +=
                        "<h3 style='font-weight:bold;margin:30px auto;padding-top:10px;border-top:1px dashed; font-size:" +
                        reciept_font + "px'><span style='text-align:left;float:left;width:25%;'>" +
                        DateTime.Now.ToString("hh:mmtt") + "</span><span style='text-align:right;float:right;width:72%;'>" +
                        "TOTAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + amount.ToString("F02") + "</span></h3>";



                    if (aRestaurantOrder.OnlineOrder > 0)
                    {
                        if (aRestaurantOrder.CardAmount > 0)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aRestaurantOrder.CardAmount.ToString("F02") + "</span></h3>";

                        }
                        else
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>ORDER NOT PAID</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></h3>";

                        }


                    }
                    else
                    {

                        if (aPaymentDetails.CashAmount > 0 && aPaymentDetails.CardAmount > 0 && status)
                        {

                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";

                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";

                        }
                        else if (status)
                        {


                            if (aPaymentDetails.CardAmount > 0)
                            {

                                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                                  "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";

                            }

                            else
                            {
                                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                                  "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";
                            }

                        }
                        if (status)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>CHANGE</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.ChangeAmount.ToString("F02") + "</span></h3>";
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px;border-top:1px dashed;'><span style='text-align:center;float:left;width:95%;'>PAID ORDER </span></h3>";
                        }


                    }


                    if (mainFormView.commentTextBox.Text != "Comment" && mainFormView.commentTextBox.Text.Trim().Length > 0)
                    {
                        printFooterStr += "<h3  style='border-bottom:1px dashed;text-align:center;'>" + mainFormView.commentTextBox.Text +
                                          "</h3>";

                    }
                    if (aRestaurantInformation.ThankYouMsg.Length > 5)
                    {
                        printFooterStr += "<h3  style='text-align:center;'>" + aRestaurantInformation.ThankYouMsg + "</h3>";

                    }

                    if (isPrint && aGeneralInformation.Person > 1)
                    {
                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'>" + "Total split " +
                                          aGeneralInformation.Person + " ways £" +
                                          (amount / aGeneralInformation.Person).ToString("F02") + "</h3>";


                    }



                    int printCopy = 1;
                    if (printer.PrintStyle == "Receipt")
                    {
                        if (aGeneralInformation.OrderType == "IN")
                        {
                            printCopy = aRestaurantInformation.DineInPrintCopy;
                        }
                        else if (aGeneralInformation.OrderType == "Takeaway" || aGeneralInformation.OrderType == "CLT")
                        {
                            printCopy = aRestaurantInformation.PrintCopy;
                        }
                        else
                        {
                            printCopy = aRestaurantInformation.DelPrintCopy;
                        }

                    }



                    printHeaderStr += "</div>";
                    printRecipeStr += "</div>";
                    printFooterStr += "<br/><br/></div>";
                    printStr = printHeaderStr + printRecipeStr + printFooterStr;

                    mainForm.SetDefaultPrinter(printer.PrinterName);

                    //ReportDataSource data = new ReportDataSource();



                    for (int i = 0; i < printCopy; i++)
                    {
                        try
                        {

                            //List<ReportData> parameter = new List<ReportData> { new ReportData { Header = str } };
                            //data.Value = str;
                            //new DynamicReportMethod().GetReceipprint(data, parameter, true);

                            //ReportDataSource data = new ReportDataSource();

                            //string str = "<html><head><style>html, body { padding: 0; margin: 0 }</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" + printStr + "</body></html>";
                            //List<ReportData> parameter = new List<ReportData> { new ReportData { Header = str} };
                            //new DynamicReportMethod().GetReceipprint(data, parameter, false);
                            string str =
                                "<html><head><style>html, body { padding: 0; margin: 0 }</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" +
                                printStr + "</body></html>";
                            //ConvertHtmlToImage(str);

                            //   pd1.Print();

                            WebBrowser wbPrintString = new WebBrowser() { DocumentText = string.Empty };
                            wbPrintString.Document.Write(str);
                            wbPrintString.Document.Title = "";

                            string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
                            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);

                            if (key != null)
                            {
                                //string old_footer = key.GetValue("footer");
                                //string old_header = key.GetValue("header");
                                key.SetValue("footer", "");
                                key.SetValue("header", "");
                                key.SetValue("margin_bottom", "0");
                                key.SetValue("margin_left", "0.15");
                                key.SetValue("margin_right", "0");
                                key.SetValue("margin_top", "0");
                                key.SetValue("Print_Background", "false");
                                // wbPrintString.ShowPrintPreviewDialog();
                                wbPrintString.Print();
                                wbPrintString.Dispose();

                            }


                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Selected printer not exist!", "Printer Setup Warning", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetBaseException().ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<int> GetRecipeTypes(string recipeTypes)
        {
            string[] list = recipeTypes.Split(',');
            List<int> types = new List<int>();

            for (int i = 0; i < list.Count(); i++)
            {
                int type = Convert.ToInt32("0" + list[i]);
                types.Add(type);
            }

            return types;
        }


        public void KitchinePrint(List<ReportData> ListOfItem, List<PrinterSetup> PrinterSetups, RestaurantOrder order,GeneralInformation aGeneralInformation,string timer)
        {
            int papersize = 25;
            string reciept_font = "";

            string printHeaderStr =
                "<div style='width:260px; font-family:Arial, Helvetica, sans-serif;padding:0px 0px 0px 10px;'>";
            RestaurantInformation aRestaurantInformation = GlobalSetting.RestaurantInformation;
            
            bool flag = false;


            int blankLine = 0;
            reciept_font = "20";
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 1).ToString();
            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();
            string orderHistory = new RestaurantOrderBLL().GetOrderHistory(papersize, order.Id, aGeneralInformation,timer, true);
            printHeaderStr += "<div   style='width: 250px;'><div style='text-align:center;font-size:15px'>" +
                              DateTime.Now.ToString("ddd,dd/MM/yyyy HH:mm") + "</div></div>";
            printHeaderStr +=
                "<div  style='width: 250px;border-bottom:2px double;'><div style='text-align:center;'><b>" +
                orderHistory + "</b></div></div>";

            foreach (PrinterSetup printer in PrinterSetups)
            {

                if (printer.PrintStyle == "Receipt")
                {
                    continue;
                }

                string printStr = "";
                string printRecipeStr =
                    "<div style='width:250px; font-family:Arial, Helvetica, sans-serif; font-weight:lighter;line-height:26px;margin:0;padding:0px 0px 0px 10px;'>";
                string printFooterStr =
                    "<div style='width:250px; font-family:Arial, Helvetica, sans-serif; font-weight:lighter;line-height:26px;padding:0px 0px 0px 10px;margin:0;'>";

                List<int> recipeTypes = GetRecipeTypes(printer.RecipeTypeList);


                int catId = 0;
                bool startdas = false;
                try{

                    ListOfItem = ListOfItem.OrderBy(a => a.SortOrder).ToList();

                }
                catch (Exception es)
                {
                }

                foreach (ReportData typeDetails in ListOfItem)
                {if (aRestaurantInformation.MenuSeparation == 5 && recipeTypes.Contains(typeDetails.ReceipeTypeId))
                    {
                        // printRecipeStr += "<h3  style='text-align:left;font-size:10px;line-height:12px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                        printRecipeStr +=
                            "<div style='border-top:1px dashed;width:250px;margin-top:3px;'><h3  style='text-align:center;font-size:12px;line-height:15px;margin:0px auto 3px;width:" +
                            typeDetails.GroupTitle.Length * 12 +
                            "px;background:white !important;box-shadow: inset 0 0 0 1000px white;'>" +
                            typeDetails.GroupTitle.ToUpper() + "</h3></div>";

                    }
                    if (typeDetails.Group == "SingleItem")
                    {


                        if (aRestaurantInformation.MenuSeparation == 3)
                        {
                            printRecipeStr +=
                                "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                            startdas = false;
                        }
                        if ((aRestaurantInformation.MenuSeparation == 1 || aRestaurantInformation.MenuSeparation == 4) &&catId != typeDetails.CatId && catId != 0)
                        {
                            printRecipeStr +=
                                "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin:3px 0px;padding:0;'>&nbsp;</h3>";

                        }
                        if (aRestaurantInformation.MenuSeparation == 4 && catId != typeDetails.CatId)
                        {
                            string catName = new RestaurantMenuBLL().GetCategoryNameById(typeDetails.CatId);
                            printRecipeStr += "<hr/><br/>";
                            printRecipeStr += "<h3  style='text-align:center;font-size:15px;line-height:20px;width:" +
                                              catName.Length * 12 +
                                              "px;background:white !important;box-shadow: inset 0 0 0 1000px white;'>" +
                                              catName.ToUpper() + "</h3>";

                        }

                        RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

                        //int KitchenDone = aRestaurantOrderBLL.GetOrderItemKitchenStatus(typeDetails.ReceipeTypeId,order.Id);
                        //if (KitchenDone == 0)
                        //{
                          //  bool result1 = aRestaurantOrderBLL.UpdateOrderItemKitchenStatus(typeDetails.ReceipeTypeId,order.Id);

                            printRecipeStr += "<h3 style='font-weight:lighter;line-height:21px; font-size:" +
                                              reciept_font +
                                              "px'><span style='text-align:left;width:95%;text-transform: capitalize;'>" +
                                              typeDetails.ItemQty + " " + typeDetails.ItemName.ToLower() + "</br>" + "<p style='margin: 0px 10px; font-size:"+(Convert.ToInt16(reciept_font)-5)+"px'>" + typeDetails.KitchineOption + "</span></h3>";


                            blankLine++;
                            string options = "";
                          

                            if (aRestaurantInformation.MenuSeparation == 2)
                            {
                                printRecipeStr +=
                                    "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;'>&nbsp;</h3>";
                            }





                       // }
                    }
                    else if (typeDetails.Group == "MultipleItem")
                    {
                        printRecipeStr += "<h3 style='font-weight:lighter;line-height:21px;'>" +
                                          "<span style='text-align:left;width:100%;text-transform: capitalize;'>" +
                                            "<span style='float:left;width:5%;'>" + typeDetails.ItemQty + "</span> <span style='float:left;'>" + typeDetails.ItemName.ToLower() + "</span>" + "</br>" + "</span></h3>";


                        blankLine++;
                    }

                }
                if (blankLine < aRestaurantInformation.RecieptMinHeight)
                {
                    for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                    {
                        printRecipeStr +=
                            "<h3  style='text-align:left;font-size:20px;line-height:22px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                    }
                    printRecipeStr +=
                        "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                }


                //if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                //{
                //    printFooterStr +=
                //        "<h3  style='border-bottom:1px dashed;text-align:center;text-transform: capitalize;'>" +
                //        commentTextBox.Text.ToLower() + "</h3>";

                //}



                int printCopy = 1;

                printHeaderStr += "</div>";
                printRecipeStr += "</div>";
                printFooterStr += "<br/><br/></div>";
                printStr = printHeaderStr + printRecipeStr + printFooterStr;



                string str =
                            "<html><head><style>html, body { padding: 0; margin: 0 }</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" +
                            printStr + "</body></html>";
                WebBrowser wbPrintString = new WebBrowser() { DocumentText = string.Empty };
                wbPrintString.Document.Write(str);
                wbPrintString.Document.Title = "";



                string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);

                if (key != null)
                {
                    //string old_footer = key.GetValue("footer");//string old_header = key.GetValue("header");
                    key.SetValue("footer", "");
                    key.SetValue("header", "");
                    key.SetValue("margin_bottom", "0");
                    key.SetValue("margin_left", "0.15");
                    key.SetValue("margin_right", "0");
                    key.SetValue("margin_top", "0");

                    //wbPrintString.ShowPrintDialog();

                    wbPrintString.Print();
                    wbPrintString.Dispose();

                }
                
            }


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
    }
}
