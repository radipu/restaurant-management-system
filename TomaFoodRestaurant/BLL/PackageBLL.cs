using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class PackageBLL
    {
        public DataTable GetPackagerReceipeByPackageItem(PackageItemButton catid)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackagerReceipeByPackageItem(catid);
            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackagerReceipeByPackageItem(catid);
            }

        }
        public DataTable GetPackagerReceipeByPackageItemWithSubCat(PackageCategoryButton catid)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackagerReceipeByPackageItemWithSubCat(catid);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackagerReceipeByPackageItemWithSubCat(catid);
            }

        }
        public DataTable GetOptionAll(int id)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.Get_AllOption(id);

            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.Get_AllOption(id);

            }


        }
        public DataTable GetAllRecipe()
        {

            if (GlobalSetting.DbType == "SQLITE")
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetAllRecipe();

            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetAllRecipe();
            }



        }

        public List<PackageCategoryButton> GetPackageCategory(RecipePackageButton aRecipePackageButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackageCategory(aRecipePackageButton);
            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackageCategory(aRecipePackageButton);
            }

        }
        public DataTable GetPackageCategory()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackageCategory();

            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackageCategory();
            }


        }

        private DataTable NewData = new DataTable();
        public DataTable GetPackageCategory1()
        {

            DataTable AllCategory = GetPackageCategory();
            NewData = AllCategory.Clone();

            foreach (DataRow list in AllCategory.Rows)
            {

                var CatId = list["category_id"].ToString();
                if (CatId.Contains(','))
                {
                    string[] multiCategory = CatId.Split(',');
                    SeperateCategory(multiCategory, list);
                }
                else
                {
                    NewData.Rows.Add(list.ItemArray);
                    //  NewData.Rows.Add(list);

                }


            }
            return NewData;
        }

        public void SeperateCategory(string[] Category, DataRow row)
        {
            foreach (string cat in Category)
            {
                row["category_id"] = cat;
                NewData.Rows.Add(row.ItemArray);
            }
        }
        public List<ReceipeCategoryButton> GetCategoryList(List<int> catogries)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetCategoryList(catogries);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetCategoryList(catogries);


            }


        }

        public List<PackageItem> GetAutoPackageItem(RecipePackageButton categoryButton)
        {         
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetAutoAddPackageItemList(categoryButton);
        }
        public List<PackageItemButton> LoadAllItemForAutoCheck(PackageCategoryButton categoryButton)
        {
            if (GlobalSetting.DbType != "SQLITE")
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.LoadAllPackageReceipeItemForAutoCheck(categoryButton);

            }
            else
            {
                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.LoadAllPackageReceipeItemForAutoCheck(categoryButton);

            }
           
        } 
        public List<int> MergeSubcategory(List<int> cato, List<PackageItemButton> aPackageItemButtonList)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.MergeSubcategory(cato, aPackageItemButtonList);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.MergeSubcategory(cato, aPackageItemButtonList);



            }

        }

        public List<int> MargeCategoty(List<int> cato, List<PackageItemButton> aPackageItemButtonList)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.MergeSubcategory(cato, aPackageItemButtonList);



            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.MergeSubcategory(cato, aPackageItemButtonList);




            }


        }

        public List<int> GetCategory(string cat)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetCategory(cat);



            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetCategory(cat);
            }
           


        }

        public List<PackageItemButton> GetAllSubCategoryWhenFormLoad(PackageCategoryButton category)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.LoadAllSubCategoryWhenFormLoad(category);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.LoadAllSubCategoryWhenFormLoad(category);
            }
          

        }

        public List<PackageItemButton> GetPackageItemWithoytSubCategoryWhenFormLoad(PackageCategoryButton category)
        {

            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackageItemWithoytSubCategoryWhenFormLoad(category);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackageItemWithoytSubCategoryWhenFormLoad(category);
            }
           

        }

        public List<PackageItemButton> GetPackageItemWithoytSubCategory(PackageCategoryButton category)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackageItemWithoytSubCategory(category);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackageItemWithoytSubCategory(category);
            }
           
       

        }

        public List<PackageItemButton> GerSubCategoryList(List<int> subCategories, PackageCategoryButton category)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GerSubCategoryList(subCategories, category);


            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GerSubCategoryList(subCategories, category);
            }
           


        }

        public List<PackageItemButton> GetAllSubCategory(PackageCategoryButton category)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetAllSubCategory(category);

            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetAllSubCategory(category);
            }
           


        }

        public List<RecipePackageButton> GetPackageByRecipeType(ReceipeTypeButton aReceipeTypeButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackageByRecipeType(aReceipeTypeButton);

            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackageByRecipeType(aReceipeTypeButton);
            }
           

           


        }
        public int CheckPackageItemDulicate(PackageItem aOrderItemDetails, List<RecipeOptionItemButton> aRecipeList, int PackageId, List<PackageItem> aPackageItemMdList, List<RecipeOptionMD> aRecipeOptionMdList, int index)
        {
            List<PackageItem> itemDetails = aPackageItemMdList.Where(a => a.ItemId == aOrderItemDetails.ItemId && a.PackageId == PackageId && a.OptionsIndex == index).ToList();
            if (itemDetails.Count == 0)
            {

                return 0;
            }

            int result = 0;
            foreach (PackageItem item in itemDetails)
            {

                List<RecipeOptionMD> aRList = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex && a.PackageItemOptionsIndex == item.PackageItemOptionsIndex).ToList();
                if (aRecipeList.Count == 0 && aRList.Count == 0)
                {
                    return item.ItemId;
                }

                int cnt = 0;
                if (aRList.Count == aRecipeList.Count)
                {
                    foreach (RecipeOptionMD list in aRList)
                    {
                        bool flag = false;
                        foreach (RecipeOptionItemButton recipe in aRecipeList)
                        {
                            if (list.RecipeOPtionItemId == recipe.RecipeOptionItemId) cnt++;
                        }
                    }

                    if (cnt != 0 && cnt == aRecipeList.Count)
                    {
                        result = item.ItemId;
                    }
                }

            }
            return result;

        } 

        public List<PackageCategoryButton> GetPackageCategoryWhereNoOption(RecipePackageButton aRecipePackageButton)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {


                PackageDAO aPackageDao = new PackageDAO();
                return aPackageDao.GetPackageCategoryWhereNoOption(aRecipePackageButton);

            }
            else
            {
                MySqlPackageDAO aPackageDao = new MySqlPackageDAO();
                return aPackageDao.GetPackageCategoryWhereNoOption(aRecipePackageButton);
            }
           

           
           

        }
    }
}
