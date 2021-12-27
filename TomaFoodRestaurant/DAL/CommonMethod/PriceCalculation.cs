using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.DAL.CommonMethod
{
    public class PriceCalculation
    {
        private mainForm mainForm;
        private MainFormView responsinvePage;
       public PriceCalculation(mainForm singlePage,MainFormView responsive)
        {
           if (singlePage!=null)
           {
               this.mainForm = singlePage;
           }
           else if (responsive != null)
           {
               this.responsinvePage = responsive;
           }
       }
        public GeneralInformation CalculateDiscount(OrderDiscount aOrderDiscount,GeneralInformation aGeneralInformation)
       {
        
            if (aOrderDiscount.DiscountArea == "Order")
            {
                string[] categoryStringList = GlobalSetting.RestaurantInformation.ExcludeDiscount.Split(',');

                double amount1 = 0;
                double amount2 = 0;
                double amount3 = 0;

                double orderTotal = 0;

                double totalAmount = 0.0;
                //if (mainForm!=null)
                //{
                //    totalAmount = mainForm.GetTotalAmountDetails();
                //}
                //else
                if (responsinvePage!=null)
                {
                    totalAmount = (double) responsinvePage.ShopingCard();
                }
                if (mainForm!=null)
                {
                    

                    orderTotal = mainForm.GetTotalAmountDetails();
                    orderTotal = GlobalVars.numberRound(orderTotal, 2);
                    foreach (RecipeTypeDetails control1 in mainForm.orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                    {
                        foreach (deatilsControls details in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                        {
                            if (!(Array.IndexOf(categoryStringList, details.CategoryId.ToString()) > -1))
                            {

                                amount1 += ((Convert.ToDouble(details.priceTextBox.Text)) * Convert.ToDouble(details.qtyTextBox.Text));
                            }
                        }
                    }
                    if (!mainForm.aRestaurantInformation.ExcludeDiscount.ToLower().Contains("package"))
                    {
                        amount2 = mainForm.aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
                        amount3 = mainForm.aPackageItemMdList.Sum(a => a.Qty * a.Price);
                    }


                     totalAmount = GlobalVars.numberRound(amount1 + amount2 + amount3,2);
                }
                else if (responsinvePage != null)
                {
                     var dt = responsinvePage.dataTable.AsEnumerable().ToList();

                    orderTotal = dt.Sum(a => Convert.ToDouble(a["Total"]));

                    foreach (DataRow row in dt)
                    {
                        
                            if (!(Array.IndexOf(categoryStringList, row["Cat"].ToString()) > -1))
                            {
                                if (row["Package"].ToString() == "")
                                {

                                    amount1 += ((Convert.ToDouble(row["Total"])));
                                }
                                else {
                                    if (!categoryStringList.Contains("Package"))
                                    {
                                        amount1 += ((Convert.ToDouble(row["Total"])));
                                    }
                                }
                            }
                    }
                    //if (!categoryStringList.Contains("package"))
                    //{
                    //    amount2 = MainFormView.aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
                    //    amount3 = MainFormView.aPackageItemMdList.Sum(a => a.Qty * a.Price);
                    //}


                    totalAmount = amount1 + amount2 + amount3;

                    //double totalAmount = (double)ShopingCard();
                    ////  double totalAmount = amount1 + amount2 + amount3;
                    //if (aOrderDiscount.DiscountType == "Fixed")
                    //{
                    //    if (totalAmount > 0)
                    //    {
                    //        aGeneralInformation.DiscountPercent = (aOrderDiscount.Amount * 100) / totalAmount;
                    //    }
                    //    discountAmount = aOrderDiscount.Amount;
                    //}
                }

                if (aOrderDiscount.DiscountType.ToLower() == "persent")
                {

                    aGeneralInformation.DiscountType = "percent";
                    aGeneralInformation.DiscountPercent = aOrderDiscount.Amount;
                    aGeneralInformation.DiscountFlat =(((totalAmount * aOrderDiscount.Amount) / 100));
                 

                }
                else if (aOrderDiscount.DiscountType.ToLower() == "percent")
                {
                    aGeneralInformation.DiscountType = "percent";
                    aGeneralInformation.DiscountPercent = aOrderDiscount.Amount;
                    aGeneralInformation.DiscountFlat = GlobalVars.numberRound(((totalAmount * aOrderDiscount.Amount) / 100),2);
                }
                else
                {
                    aGeneralInformation.DiscountType = "flat";
                    aGeneralInformation.DiscountPercent = 0.0;
                    aGeneralInformation.OrderDiscount = 0.0;
                    aGeneralInformation.DiscountFlat = 0.0;
                    if (mainForm!=null)
                    {
                        mainForm.discountButton.Text = "Disc\r\n0.0";
                    }
                    else if (responsinvePage != null)
                    {
                        responsinvePage.discountButton.Text = "Disc\r\n0.0";
                    }
                   
                    if (totalAmount > 0)
                    {
                        aGeneralInformation.DiscountPercent = (aOrderDiscount.Amount * 100) / totalAmount;
                        aGeneralInformation.OrderDiscount = GlobalVars.numberRound(aOrderDiscount.Amount,2);
                        aGeneralInformation.DiscountFlat = GlobalVars.numberRound(aOrderDiscount.Amount,2);
                    }
                }
                if (mainForm != null)
                {
                    double newDisAmount = GlobalVars.numberRound(aGeneralInformation.DiscountFlat, 2);
                   
                    mainForm.discountButton.Text = "Disc " + aGeneralInformation.DiscountPercent.ToString("F02") + "%\r\n" +GlobalVars.numberRound(newDisAmount, 2).ToString("F02");
                    totalAmount = GlobalVars.numberRound(orderTotal - newDisAmount, 2);
                    mainForm.totalAmountLabel.Text = "£" + Math.Abs(totalAmount).ToString("F02");

                }
                else if (responsinvePage != null)
                {
                    aGeneralInformation.OrderDiscount = aGeneralInformation.DiscountFlat;
                    responsinvePage.discountButton.Text = "Disc " + aGeneralInformation.DiscountPercent.ToString("F02") + "%\r\n" + aGeneralInformation.DiscountFlat.ToString("F02");
                    //totalAmount = orderTotal - aGeneralInformation.OrderDiscount;
                    totalAmount = orderTotal - aGeneralInformation.DiscountFlat;

                    responsinvePage.totalAmountLabel.Text = "£" + Math.Abs(totalAmount).ToString("F02");
                }
                   
                return aGeneralInformation;
            }
            else if (aOrderDiscount.DiscountArea == "Line")
            {
                if (mainForm != null)
                {
                 mainForm.LineDiscount(aOrderDiscount);
                }
                else if (responsinvePage != null)
                {
                    responsinvePage.LineDiscount(aOrderDiscount);
                }
               
            }
            return aGeneralInformation;
        }
    }
}
