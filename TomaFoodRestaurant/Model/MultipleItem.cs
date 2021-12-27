using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.XtraGrid;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.Model
{
  public  class MultipleItem
    {
      public DataTable multipleItemLoad= new DataTable();
      public int itemLimit = 0;
      public int IsCheckedQuater(Panel panel)
      {
          itemLimit = 0;
          foreach (Control control in panel.Controls)
          {
              string nameOfMultipleButon = control.Name;
              if ((nameOfMultipleButon == "half" && control.Visible) || (nameOfMultipleButon == "quater" && control.Visible))
              {
                  if (nameOfMultipleButon=="half")
                  {
                      itemLimit = 2;
                  }
                  else
                  {
                      itemLimit = 4;
                  }

                  return itemLimit;
              }
          }
          return itemLimit;
      }

      public void LoadMulipleItemLoad(GridControl gridControl)
      {
          
          multipleItemLoad = new DataTable();
          multipleItemLoad.Columns.Add("Qty", typeof(int));
          multipleItemLoad.Columns.Add("RecepiMenuId", typeof(int));
          multipleItemLoad.Columns.Add("MultipleItemName", typeof(string));
          multipleItemLoad.Columns.Add("Price", typeof(double));
          multipleItemLoad.Columns.Add("CartItem", typeof(CartItem));

          multipleItemLoad.Rows.Add(GetLoadRow());
          multipleItemLoad.Rows.Clear();gridControl.DataSource = multipleItemLoad;
        

      
      }

        public DataRow GetLoadRow()
        {
            DataRow row = multipleItemLoad.NewRow();
            row["MultipleItemName"] = "<h1>Test</h1>";
            return row;
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
      public int  AddToMultipleCart(CartItem aReceipeSubCategoryButton)
      {
         DataRow row = multipleItemLoad.NewRow();
        
          aReceipeSubCategoryButton=OptionItemAdd(aReceipeSubCategoryButton);
          row["Qty"] = 1;
          row["MultipleItemName"] = aReceipeSubCategoryButton.ReceipeMenuItemButton.ItemName;
          row["Price"] = aReceipeSubCategoryButton.ReceipeMenuItemButton.InPrice.ToString("F02");
          row["RecepiMenuId"] = aReceipeSubCategoryButton.ReceipeMenuItemButton.RecipeMenuItemId;
          row["CartItem"] = aReceipeSubCategoryButton;
          if (multipleItemLoad.Rows.Count>=itemLimit-1)
          {
              multipleItemLoad.Rows.Add(row);
             
             // MessageBox.Show("your limit is over", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
              return 1;
          }
          multipleItemLoad.Rows.Add(row);
          return 0;
      }
      public CartItem OptionItemAdd(CartItem aCartItem){
          string option = "";
          double price = 0.0;

          foreach (var opvalue in aCartItem.ReceipeMenuItemButton.OptionJson)
          {
              option += "&nbsp;&rarr;" + opvalue.optionName + "</br>";
              price += opvalue.optionPrice;
          }
          aCartItem.ReceipeMenuItemButton.InPrice += price;
          aCartItem.ReceipeMenuItemButton.ItemName =aCartItem.ReceipeMenuItemButton.ShortDescrip+ "</br>" + option;
          return aCartItem;
      }

      public List<PackageItem> GetAllMultipleItem(DataTable table)
      {
          List<MultipleItemMD> ListOfMultipleItem=new List<MultipleItemMD>();

          foreach (DataRow dataRow in table.Rows)
          {
              CartItem item = (CartItem) dataRow["CartItem"];
              ListOfMultipleItem.Add(new MultipleItemMD
              {
                  CategoryId = item.ReceipeMenuItemButton.CategoryId,
                  ItemId = item.ReceipeMenuItemButton.RecipeMenuItemId,
                  ItemName = item.ReceipeMenuItemButton.ItemName,
                  OptionName = ""+new OptionJsonConverter().Serialize(item.ReceipeMenuItemButton.OptionJson)+"",
                  Price = item.ReceipeMenuItemButton.InPrice,
                  Qty = item.Quantity,
                  RecipeTypeId = item.ReceipeMenuItemButton.RecipeTypeId,
                  
              });

              

          }
          List<PackageItem> list = new List<PackageItem>();
          list.Add(new PackageItem(){MultipleItem = ListOfMultipleItem});

        return list;
      }

      public List<PackageItem> CollectMultipleItemForLoad(List<MultipleItemMD> ReceipeMDlist )
      {
          List<PackageItem> multipleItem=new List<PackageItem>();
          multipleItem.Add(new PackageItem() { MultipleItem = ReceipeMDlist });

          return multipleItem;
      } 

      public List<MultipleItemMD> LoadMultilpleItemsList(RecipeMultipleMD aOrderItemDetails, List<MultipleMenuDetailsJsonResponsivePage> aMultipleMenuDetailsJsons)
      {
          List<MultipleItemMD> MList=new List<MultipleItemMD>();
          try
          {
              foreach (MultipleMenuDetailsJsonResponsivePage multipleItem in aMultipleMenuDetailsJsons)
              {
                  MultipleItemMD itemMd = new MultipleItemMD();
                  itemMd.CategoryId = multipleItem.category_id;
                  itemMd.ItemId = multipleItem.id;
                  itemMd.ItemName = multipleItem.name;
                  itemMd.OptionsIndex = aOrderItemDetails.OptionsIndex;
                  itemMd.Price = 0;
                  itemMd.Qty = (int)multipleItem.quantity;
                  itemMd.SubcategoryId = aOrderItemDetails.CategoryId;
                  itemMd.OptionName = multipleItem.options;
                  itemMd.OptionList = new OptionJsonConverter().DeSerialize(multipleItem.options);
                  MList.Add(itemMd);

                  //LoadOptionsMultiple(aOrderItemDetails, itemMd, multipleItem);
              }

          }
          catch (Exception exception)
          {
              ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
              aErrorReportBll.SendErrorReport(exception.ToString());
          }

          return MList;

      }
      private string MultipleItemBuild(RecipeMultipleMD multipleMd, List<MultipleItemMD> multipleItem)
      {
          int i = 0;
          string TotalItemBuild = "<h4 style='margin:0px;text-align:left;'>" + multipleMd.MultiplePartName + "</h4>";
          foreach (MultipleItemMD multipleItemMd in multipleItem)
          {
              string name = "<p style='margin:0px; text-align:left;'> :&nbsp; " + multipleItemMd.ItemName + "</p>";
              string option = "";
              foreach (OptionJson optionItem in multipleItemMd.OptionList)
              {
                  option += "&rarr;" + optionItem.optionName+"</br>";
              }

              TotalItemBuild +=name+option;
          }
         
          return TotalItemBuild;

      }

      public DataTable OnlyMultipleOrder(List<RecipeMultipleMD> aMultipleItemMdList, DataTable dataTable, List<ReceipeCategoryButton> GetAllCatergory)
      {
          DataRow dr = dataTable.NewRow();

          foreach (RecipeMultipleMD itemDetailsMd in aMultipleItemMdList)
          {
              dr = dataTable.NewRow();
              dr[0] = dataTable.Rows.Count + 1;
              dr[1] = itemDetailsMd.CategoryId;
              dr[2] = itemDetailsMd.Qty;
              List<OptionJson> jsonOption = new List<OptionJson>();
              dr[3] = MultipleItemBuild(itemDetailsMd, itemDetailsMd.MultipleItem);
              dr["Package"] = CollectMultipleItemForLoad(itemDetailsMd.MultipleItem);

              //if (itemDetailsMd.MultipleItem!=null)
              //{

              //    jsonOption =new OptionJsonConverter().DeSerialize(itemDetailsMd.OptionName).ToList();
              //    string htmlText = "<h4 style='font-size:12px;margin:0px; text-align:left'>" + itemDetailsMd.ItemName + "</h4>";
              //    string Option = itemDetailsMd.OptionName;
              //    // List<string> optionName = new List<string>();
              //    string removeString = Regex.Replace(Option, @"<[^>]+>|", "");

              //    if (removeString.Contains(',') ||  jsonOption.Count > 0)
              //    {
              //        htmlText += "<table style='width:100%;'>";

              //        for (int i = 0; i < jsonOption.Count; i++)
              //        {


              //            if (jsonOption[i].price > 0)
              //            {
              //                htmlText += "<tr>" + "<td>" + "&rarr;" + jsonOption[i].qty + "</td>" +
              //                            "<td  style='text-align:left'>" + jsonOption[i].optionName + "</td>" +
              //                            " <td style='text-align:right'>£ " + jsonOption[i].price + "</td>" + "</tr>";


              //            }
              //            else
              //            {
              //                htmlText += "<tr>" + "<td>" + "&rarr;" + jsonOption[i].qty + "</td>" + "<td  style='text-align:left'>" + jsonOption[i].optionName + "</td>" + " <td style='text-align:right'></td>" + "</tr>";

              //            }


                        
              //            // htmlText += "&rarr;" + result[i] + "</br>";
              //        }


              //        htmlText += "</table>";
              //        dr[3] = htmlText;
              //    }
              //    else
              //    {
              //        //htmlText += "&rarr;" + removeString + "</br>";
              //        dr[3] = htmlText;
              //    }


              //}
              //else
              //{
              //    dr[3] = itemDetailsMd.ItemName;//  dr["OptionId"] = itemDetailsMd.OptionList;

              //}
              dr["OptionId"] = jsonOption;
              dr[4] = itemDetailsMd.UnitPrice;
              dr[5] = Convert.ToInt16(dr[2]) * Convert.ToDecimal(dr[4]);
              dr[6] = Convert.ToUInt64(itemDetailsMd.ItemId);
              dr[7] = Convert.ToUInt64(itemDetailsMd.ItemId);
              dr[8] = itemDetailsMd.MultiplePartName;
              dr["Group"] = "MultipleItem";
              var GroupName = GetAllCatergory.FirstOrDefault(a => a.CategoryId == itemDetailsMd.CategoryId);
              dr["GroupName"] = GroupName.Text;
              dr["SortOrder"] = GroupName.SortOrder;
              dataTable.Rows.Add(dr);


             
          }

          return dataTable;
      }
    }
}
