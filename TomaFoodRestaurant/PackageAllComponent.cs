using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Grid;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant
{
 public   class PackageAllComponent
    {
     public static PackageItem FixedPackageItemBind(List<PackageItem> allPackageItems, RecipePackageButton packageButton,GridView packageGrid)
     {
         PackageItem packageItem=new PackageItem();
         string bindItem = "<h4 style='font-size:12px;margin:0px; text-align:left'>" + packageButton.PackageName + "</h4>";
         bindItem += "<table style='width:100%;'>";
       
         foreach (PackageItem allPackageItem in allPackageItems)
         {

             if (packageGrid != null)
             {
                
                 for (int i = 0; i < packageGrid.RowCount; i++)
                 {
                     PackageItem GetItemName =(PackageItem) packageGrid.GetRowCellValue(i, "Class");
                     var Optionid = packageGrid.GetRowCellValue(i, "PoptionId").ToString();
                     if (allPackageItem==GetItemName)
                     {
                         if (Optionid.Length == 0)
                         {bindItem += "<tr>" + "<td>" + allPackageItem.Qty + "</td>" + " <td style='text-align:left'>" + allPackageItem.ItemName + "</td>" + "</tr>";

                         }
                         else
                         {
                             var OptionName = packageGrid.GetRowCellValue(i, "PackageItemName").ToString();
                             bindItem += "<tr>" + "<td>" + allPackageItem.Qty + "</td>" + " <td style='text-align:left'>" + OptionName + "</td>" + "</tr>";

                         }
                         break;
                     }

                    
                 }


                 
                 
                 //bindItem += "<tr>" + "<td>" + allPackageItem.Qty + "</td>" + " <td style='text-align:left'>" + allPackageItem.ItemName+"</br>"+GetItemName+ "</td>" + "</tr>";

                 //allPackageItem.ItemName = source.AsEnumerable().FirstOrDefault(Convert.ToInt32(["Index"])==allPackageItem.OptionsIndex).;
             }
             else
             {
                 if (allPackageItem.PackageItemOptionList == null)
                 {
                     bindItem += "<tr>" + "<td>" + allPackageItem.Qty + "</td>" + " <td style='text-align:left'>" + allPackageItem.ItemName + "</td>" + "</tr>";

                 }
                 else
                 {
                     if (allPackageItem.PackageItemOptionList.Count > 0)
                     {
                         string htmlBind = allPackageItem.ItemName;
                         for (int i = 0; i < allPackageItem.PackageItemOptionList.Count; i++)
                         {
                             PackageItem GetItemName = (PackageItem)allPackageItem;

                             var Optionid = allPackageItem.PackageItemOptionList[i].RecipeOptionId.ToString();

                             if (allPackageItem == GetItemName)
                             {
                                 if (Optionid.Length == 0)
                                 {
                                     bindItem += "<tr>" + "<td>" + allPackageItem.Qty + "</td>" + " <td style='text-align:center'>" + allPackageItem.ItemName + "</td>" + "</tr>";
                                 }
                                 else
                                 {
                                     if (allPackageItem.PackageItemOptionList[i].Price > 0)
                                     {
                                         var OptionName = allPackageItem.PackageItemOptionList[i].Title + " + " +allPackageItem.PackageItemOptionList[i].Price;
                                         htmlBind += "</br>" + "&rarr;" + OptionName;
                                     }
                                     else
                                     {
                                         var OptionName = allPackageItem.PackageItemOptionList[i].Title;
                                         htmlBind += "</br>" + "&rarr;" + OptionName;
                                     }
                                      // bindItem += "<tr>" + "<td>" + allPackageItem.PackageItemOptionList[i].Qty + "</td>" +" <td style='text-align:left'>" + OptionName + "</td>" + "</tr>";
                                     
                                 }

                             }
                         }
                         bindItem += "<tr>" + "<td>" + allPackageItem.Qty +" X " + "</td>" + " <td style='text-align:left'>" + htmlBind + "</td>" + "</tr>";

                     }
                     else
                     {
                         bindItem += "<tr>" + "<td>" + allPackageItem.Qty + "</td>" + " <td style='text-align:left'>" + allPackageItem.ItemName + "</td>" + "</tr>";

                     }
                 }
                
                
                
             }
            

             
         }

         bindItem += "</table>";
         packageItem.ItemName = bindItem;
        
        return packageItem;
     }


    
 }
}
