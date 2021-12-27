using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class RestaurantMenuBLL
    {
        internal ReceipeMenuItemButton GetRecipeByItemId(int recipeId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeByItemId(recipeId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeByItemId(recipeId);
            }

        }

        internal RecipeOptionItemButton GetRecipeOptionByOptionName(string optionName)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionByOptionName(optionName);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionByOptionName(optionName);
            }
        }
        internal RecipeOptionItemButton GetRecipeOptionByOptionId(int optionId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionByOptionId(Convert.ToInt32(optionId));
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionByOptionId(optionId);
            }
        }
        internal RecipePackageButton GetPackageByPackageId(int packageId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();

                return aRestaurantMenuDao.GetPackageByPackageId(packageId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackageByPackageId(packageId);
            }
        }

        internal string GetSubcategoryByCatAndSubcat(int categoryId, int subcategoryId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetSubcategoryByCatAndSubcat(categoryId, subcategoryId);

            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetSubcategoryByCatAndSubcat(categoryId, subcategoryId);
            }
        }

        public PackageCategoryButton GetPackagePackageCategory(PackageItemButton itemButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackagePackageCategory(itemButton);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackagePackageCategory(itemButton);
            }
        }

        internal List<RecipePackageButton> GetPackageByMenuType(int menuType)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackageByMenuType(menuType);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackageByMenuType(menuType);
            }
        }

        internal PackageItemButton GetRecipeByItemIdForPackage(int recipeId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeByItemIdForPackage(recipeId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeByItemIdForPackage(recipeId);
            }
        }

        internal PackageItemExtraPrice GetPackageItemPrice(int recipeId, int packageId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackageItemPrice(recipeId, packageId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetPackageItemPrice(recipeId, packageId);
            }
        }

        internal List<ReceipeTypeButton> GetRecipeType()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeType();
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeType();
            }
        }

        internal int GetCategoryByName(string name)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoryByName(name);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoryByName(name);
            }
        }

        internal List<int> GetCategoriesByName(string name)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                //RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                //return aRestaurantMenuDao.GetCategoryByName(name);
                return null;
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoriesByName(name);
            }
        }

        public ReceipeCategoryButton GetCategoryByCategoryId(int categoryId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoryByCategoryId(categoryId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoryByCategoryId(categoryId);
            }
        }

        public List<ReceipeCategoryButton> GetAllCategory()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetAllCategory();
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetAllCategory();
            }
        }

        internal List<ReceipeSubCategoryButton> GetAllSubcategory()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetAllSubcategory();
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetAllSubcategory();
            }
        }

        internal List<ReceipeMenuItemButton> AllRecipeButton()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.AllRecipeButton();
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.AllRecipeButton();
            }
        }

        public bool GetReceipeOptionsByItemId(int recipeMenuItemId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetReceipeOptionsByItemId(recipeMenuItemId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetReceipeOptionsByItemId(recipeMenuItemId);
            }
        }

        public string GetColorCode(string colorTheme)
        {
            if (colorTheme.ToLower() == "success")
            {
                return "#70ca63";
            }

            if (colorTheme.ToLower() == "primary")
            {
                return "#3078d7";
            }

            if (colorTheme.ToLower() == "info")
            {
                return "#3bafda";
            }

            if (colorTheme.ToLower() == "warning")
            {
                return "#f6bb42";
            }

            if (colorTheme.ToLower() == "danger")
            {
                return "#e9573f";
            }
            if (colorTheme.ToLower() == "alert")
            {
                return "#967adc";
            }
            if (colorTheme.ToLower() == "system")
            {
                return "#37bc9b";
            }
            return "#3078d7";
        }

        internal List<AttributeButton> GetAllAttributeButton()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetAllAttributeButton();
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetAllAttributeButton();
            }
        }

        internal List<RecipeOptionButton> GetRecipeOptionWhenItemClick(int itemId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionWhenItemClick(itemId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionWhenItemClick(itemId);
            }
        }

        public List<RecipeOptionItemButton> GetRecipeOptionItemByOptionId(RecipeOptionButton recipeButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionItemByOptionId(recipeButton);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionItemByOptionId(recipeButton);
            }
        }

        public List<RecipeOptionItemButton> GetRecipeOptionItemByOnlyId(RecipeOptionButton recipeButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionItemByOnlyId(recipeButton);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeOptionItemByOnlyId(recipeButton);
            }
        }

        public ReceipeMenuItemButton GetRecipeByCategoryAndSubcategory(int categoryId, int subCategoryId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeByCategoryAndSubcategory(categoryId, subCategoryId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeByCategoryAndSubcategory(categoryId, subCategoryId);
            }
        }

        public bool IsShowCategory(ReceipeCategoryButton aReceipeCategoryButton, List<ReceipeMenuItemButton> allRecipeButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.IsShowCategory(aReceipeCategoryButton, allRecipeButton);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.IsShowCategory(aReceipeCategoryButton, allRecipeButton);
            }
        }

        internal double GetPrice(int hasSubcategoryId, int p1, int p2, int recipeId=0)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetPrice(hasSubcategoryId, p1, p2);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetPrice(hasSubcategoryId, p1, p2,recipeId);
            }
        }

        internal string GetCategoryNameById(int starterId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoryNameById(starterId);
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetCategoryNameById(starterId);
            }
        }
        internal bool UpdatePrice(ReceipeMenuItemButton recepeId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
                int cout= aRestaurantMenuDao.PriceMenuUpdate(recepeId);
                if (cout>0)
                {
                    return true;
                }
            }
            else
            {
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                int cout = aRestaurantMenuDao.PriceMenuUpdate(recepeId);
                if (cout > 0)
                {
                    return true;
                }
            }
            return false;
        }

        internal int GetRecipeIdByCatAndSubcat(int categoryId, int subcategoryId)
        {
            //if (GlobalSetting.DbType == "SQLITE")
            //{
            //    //RestaurantMenuDAO aRestaurantMenuDao = new RestaurantMenuDAO();
            //    //return aRestaurantMenuDao.GetRecipeIdByCatAndSubcat(categoryId, subcategoryId);

            //}
            //else
            //{
                MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
                return aRestaurantMenuDao.GetRecipeIdByCatAndSubcat(categoryId, subcategoryId);
            //}
        }

        internal int GetTypeStatusByID(int Id)
        {
            MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
            return aRestaurantMenuDao.GetTypeStatusByID(Id);
        }

        internal string GetRecipeNameById(int Id)
        {

            MySqlRestaurantMenuDAO aRestaurantMenuDao = new MySqlRestaurantMenuDAO();
            return aRestaurantMenuDao.GetRecipeNameById(Id); 
          
        }
    }
}
