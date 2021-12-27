using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.BLL
{
    public class PackageItemBLL
    {
        public List<PackageItem> GetPackageItem(RecipePackageButton recipePackageButton,int index)
        {
            PackageBLL aPackageBll = new PackageBLL();
            PackageCategoryButton categoryButton = new PackageCategoryButton();
            List<PackageItem> PackageList = aPackageBll.GetAutoPackageItem(recipePackageButton);
           List<PackageItem> aPackageItemList = new List<PackageItem>();
            foreach (PackageItem packageItem in PackageList)
            {

                categoryButton.PackageId = recipePackageButton.PackageId;
                categoryButton.OptionName = packageItem.ItemName;

                List<PackageItemButton> recepieList = aPackageBll.LoadAllItemForAutoCheck(categoryButton);
                foreach (PackageItemButton item in recepieList)
                {
                    aPackageItemList.Add(new PackageItem { OptionName = item.OptionName,OrgQty =  packageItem.Qty == 0 ? 1 : packageItem.Qty, OptionsIndex = index, PackageId = categoryButton.PackageId, CategoryId = item.CategoryId, CategorySortOrder = 0, ExtraPrice = item.AddPrice, Qty = packageItem.Qty == 0 ? 1 : packageItem.Qty, ItemName = item.ItemName, ItemId = item.RecipeId });
                   
                }
               

            }





            return aPackageItemList;
        }

     


    }
}
