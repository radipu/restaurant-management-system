using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using DataTable = DevExpress.DataAccess.Native.DB.DataTable;

namespace TomaFoodRestaurant.Model
{
    public class LocalDataSet
    {
        public void GetOrderFromLocalDataBase(RestaurantOrderDAO order, List<OrderPackage> orderPackages, List<OrderItem> orderItems, List<CustomerRecentItemMD> customerRecentItemMds)
        {


        }



        public void GetOrder(System.Data.DataTable order,RestaurantOrder restaurantOrder)
        {
            List<OrderItem> aOrderItem = new List<OrderItem>();
            foreach (DataRow row in order.Rows)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.Id = Convert.ToInt16(row["Index"]);
                int ReceiptMenuId = Convert.ToInt32(row["Cat"]);

                string name = row["Name"].ToString().Replace("&rarr;", ",").Trim();
                string[] removeString = Regex.Replace(name, @"<[^>]+>|", "").Split(',');
                var IsPackage = row["Group"].ToString();
                orderItem.Group = row["Group"].ToString();
                if (IsPackage != "MultipleItem" && IsPackage != "Package")
                {
                    var optionIsExist = row["OptionId"];
                    if (optionIsExist.ToString() != string.Empty)
                    {
                        List<OptionJson> option = (List<OptionJson>)row["OptionId"];
                        var listWithOption = option.Where(a => a.NoOption == false).ToList();
                        string optionId = new OptionJsonConverter().Serialize(listWithOption);
                        orderItem.Options = optionId;
                        orderItem.MinusOptions =new OptionJsonConverter().Serialize(option.Where(a => a.NoOption).ToList());
                        orderItem.Options_ids = "";
                    }
                    orderItem.Name = removeString[0];
                }
                else if (IsPackage == "MultipleItem")
                {
                    string[] MultipleName = removeString[0].Split(':');
                    orderItem.Name = MultipleName[0];
                
                    List<PackageItem> package = (List<PackageItem>)row["Package"];
                 orderItem.MultipleMenu = GetMultipleMenu(package[0].MultipleItem, package[0].ItemId);
                    orderItem.PackageItems = GetPackageItem(package,orderItem);

                }orderItem.Price = Convert.ToInt16(row["QTY"]) * Convert.ToDouble(row["Price"]);
                orderItem.Quantity = Convert.ToInt16(row["QTY"]);
                orderItem.RecipeId = Convert.ToInt32(row["RecepiMenuId"]);



                if (IsPackage == "Package")
                {
                    List<PackageItem> package = (List<PackageItem>)row["Package"];
                    orderItem.PackageId = ReceiptMenuId;
                    orderItem.Name = row["EditName"].ToString();
                    //  orderItem.Name = row["Name"].ToString();
                    orderItem.PackageItems = GetPackageItem(package,orderItem);
                    
                    aOrderItem.Add(orderItem);


                }
                else
                {
                    aOrderItem.Add(orderItem);

                }
            }

            restaurantOrder.OrderItem = aOrderItem;
            //if (restaurantOrder.OrderNo>0)
            //{
            //    UpdateOrderJsonConvert(restaurantOrder);
            //     return;
            //}
            
            OrderJsonConvert(restaurantOrder);
        }
        public void UpdateOrderJsonConvert(RestaurantOrder oreRestaurantOrder)
        {
            var readFile = File.ReadAllText("Config/order.txt");
            readFile = "[" + readFile + "]";
            List<RestaurantOrder> json = JsonConvert.DeserializeObject<List<RestaurantOrder>>(readFile);

            var Order = json.FirstOrDefault(a => a.Id == oreRestaurantOrder.OrderNo);
            if (Order!=null)
            {
                json.Remove(Order);
                json.Add(oreRestaurantOrder);
            }
            foreach (RestaurantOrder restaurantOrder in json)
            {
                OrderJsonConvert(restaurantOrder);
            }
        }
        public List<OrderPackage> GetPackage(List<OrderItem> orderItems)
        {
            List<OrderPackage> package=new List<OrderPackage>();
            foreach (OrderItem orderItem in orderItems)
            {
                if (orderItem.Group=="Package")
                {
                    package.Add(new OrderPackage{
                        Id = orderItem.Id,
                        PackageId = orderItem.PackageId,
                        Extra_price = orderItem.ExtraPrice,
                        Name = orderItem.Name,
                        optionIndex = orderItem.Id,
                        OrderId = orderItem.OrderId,
                        Price = orderItem.Price,
                        Quantity = orderItem.Quantity,
                        PackageItem = GetPackageItem(orderItem.PackageItems)
                    }); 
                }
            
            }
            return package;
        }

        private List<PackageItem> GetPackageItem(List<PackageItemNew> orderItemPackageItems)
        {
            List<PackageItem> packageItemNew = new List<PackageItem>();
            int i = 1;
            foreach (PackageItemNew value in orderItemPackageItems)
            {
                PackageItem aPackageItemNew = new PackageItem();
                aPackageItemNew.Id = i++;
                aPackageItemNew.CategoryId = value.CategoryId;
                aPackageItemNew.CategorySortOrder = value.CategorySortOrder;
                aPackageItemNew.DeleteItem = value.DeleteItem;
                aPackageItemNew.EditName = value.EditName;
                aPackageItemNew.ExtraPrice = value.ExtraPrice;
                aPackageItemNew.ItemFullName = value.ItemFullName;
                aPackageItemNew.ItemId = value.ItemId;
                aPackageItemNew.ItemName = value.ItemName;
                aPackageItemNew.ListChildOptionId = value.ListChildOptionId;
                aPackageItemNew.ListChildOptionName = value.ListChildOptionName;
                aPackageItemNew.MinusOption = value.MinusOption;
                aPackageItemNew.CategorySortOrder = value.CategoryId;
                aPackageItemNew.OptionId = value.OptionId;
                aPackageItemNew.MultipleItem = value.MultipleItem;
                aPackageItemNew.OptionsIndex = value.CategoryId;
               
                aPackageItemNew.PackageItemOptionList = value.PackageItemOptionList;
                aPackageItemNew.Type = value.Type;
                aPackageItemNew.Qty = value.Qty;
                aPackageItemNew.Price = value.Price;
                aPackageItemNew.OptionName = value.OptionName;
                aPackageItemNew.SubcategoryId = value.CategoryId;
                aPackageItemNew.PackageId = value.PackageId;
                packageItemNew.Add(aPackageItemNew);
            }

            return packageItemNew;
        }


        public void OrderJsonConvert(RestaurantOrder order){
            try
            {
                var json =JsonConvert.SerializeObject(order);
                var readFile = File.Exists("Config/order.txt");
                if (readFile)
                {
                    if (File.ReadAllText("Config/order.txt").Length>0)
                    {
                        json =","+json;
                    }
                    File.AppendAllText("Config/order.txt",json);
                }
                else
                {
                     File.WriteAllText("Config/order.txt", json);
                }
               }catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }

        public List<PackageItemNew> GetPackageItem(List<PackageItem> packageItem,OrderItem orderItem)
        {
            int i = 1;
            List<PackageItemNew> packageItemNew = new List<PackageItemNew>();
            foreach (PackageItem value in packageItem)
            {
                i++;
                PackageItemNew aPackageItemNew = new PackageItemNew();
                aPackageItemNew.Id = value.Id;
                aPackageItemNew.PackageId = orderItem.PackageId;

                aPackageItemNew.CategoryId = value.CategoryId;
                aPackageItemNew.CategorySortOrder = value.CategorySortOrder;
                aPackageItemNew.DeleteItem = value.DeleteItem;aPackageItemNew.EditName = value.EditName;
                aPackageItemNew.ExtraPrice = value.ExtraPrice;
                aPackageItemNew.ItemFullName = value.ItemFullName;
                aPackageItemNew.ItemId = value.ItemId;
                aPackageItemNew.ItemName = value.ItemName;
                aPackageItemNew.ListChildOptionId = value.ListChildOptionId;
                aPackageItemNew.ListChildOptionName = value.ListChildOptionName;
                aPackageItemNew.MinusOption = value.MinusOption;
                aPackageItemNew.CategorySortOrder = value.CategoryId;
                aPackageItemNew.OptionId = value.OptionId;
                aPackageItemNew.MultipleItem = value.MultipleItem;
                aPackageItemNew.OptionsIndex = value.CategoryId;
                aPackageItemNew.MultipleItem = value.MultipleItem;
                aPackageItemNew.PackageItemOptionList = value.PackageItemOptionList;
                aPackageItemNew.Type = value.Type;
                aPackageItemNew.Qty = value.Qty;
                aPackageItemNew.Price = value.Price;
                aPackageItemNew.OptionName = value.OptionName;
                aPackageItemNew.SubcategoryId = value.SubcategoryId;
                packageItemNew.Add(aPackageItemNew);
            }

            return packageItemNew;
        }


        private string GetMultipleMenu(List<MultipleItemMD> itemDetails, int TypeId)
        {

            List<MultipleMenuDetailsJsonResponsivePage> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJsonResponsivePage>();
            foreach (MultipleItemMD itemMd in itemDetails)
            {
                MultipleMenuDetailsJsonResponsivePage aJson = new MultipleMenuDetailsJsonResponsivePage();
                aJson.category_id = itemMd.CategoryId;
                aJson.id = itemMd.ItemId;
                aJson.kitchen_section = 0;

                string[] name = itemMd.ItemName.Replace("</br>", "#").Split('#');

                aJson.name = name[0];
                aJson.options = itemMd.OptionName;
                aJson.minus_options = null;
                aJson.price = itemMd.Price;
                aJson.quantity = itemMd.Qty;
                aJson.recipe_type_id = TypeId;
                aJson.sort_order = 0;

                aMultipleMenuDetailsJsons.Add(aJson);
            }

            var json = JsonConvert.SerializeObject(aMultipleMenuDetailsJsons);

            return json;

        }

        public static bool CheckServer()
        {

            return true;
        }

       
    }


}