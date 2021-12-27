using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class PackageDAO : GatewayConnection
    {
        public List<PackageCategoryButton> GetPackageCategory(RecipePackageButton aRecipePackageButton)
        {

            //   SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT * FROM rcs_package_category where package_id= {0} ;", aRecipePackageButton.PackageId);

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];




            List<PackageCategoryButton> aList = new List<PackageCategoryButton>();

            foreach (DataRow dataRow in DT.Rows)
            {

                // MessageBox.Show(Convert.ToString(dataRow["option_name"]));
                PackageCategoryButton aPackageCategoryButton = new PackageCategoryButton();

                aPackageCategoryButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                aPackageCategoryButton.CategoryId = Convert.ToString(dataRow["category_id"]);
                aPackageCategoryButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                aPackageCategoryButton.SubCategory = Convert.ToString(dataRow["subcategory_id"]);
                aPackageCategoryButton.Items = Convert.ToInt32(dataRow["items"]);
                aPackageCategoryButton.OptionName = Convert.ToString(dataRow["option_name"]);
                aPackageCategoryButton.AllRecipe = Convert.ToInt32(dataRow["all_recipe"]);
                aPackageCategoryButton.RecipeTypeId = aRecipePackageButton.RecipeTypeId;
                aPackageCategoryButton.ShowOption = Convert.ToInt32(dataRow["show_option"]);
                aPackageCategoryButton.Font = new System.Drawing.Font("Segoe UI", 14);

                string freeOptionId = Convert.ToString(dataRow["free_option_id"]);
                string freeOptionLimit = Convert.ToString(dataRow["free_option_limit"]);
                aPackageCategoryButton.FreeOptionMds = GetFreeOption(freeOptionId, freeOptionLimit);


                aPackageCategoryButton.FlatAppearance.BorderSize = 0;
                aPackageCategoryButton.Height = 50;
                aPackageCategoryButton.MinimumSize = new Size(120, 50);
                aPackageCategoryButton.FlatStyle = FlatStyle.Flat;
                aPackageCategoryButton.AutoSize = true;

                aList.Add(aPackageCategoryButton);


            }

            List<PackageCategoryButton> tempList = new List<PackageCategoryButton>();
            foreach (PackageCategoryButton list in aList)
            {
                PackageCategoryButton item = list;
                bool flag = false;
                foreach (PackageCategoryButton cat in aList)
                {

                    if (cat.OptionName == list.OptionName && list != cat)
                    {
                        if (cat.CategoryId != null)
                        {
                            item.CategoryId += "," + cat.CategoryId;
                        }
                        if (cat.SubCategory != "")
                        {
                            item.SubCategory += "," + cat.SubCategory;
                        }
                        flag = true;
                    }


                }

                var cnt = tempList.SingleOrDefault(a => a.OptionName == item.OptionName);
                if (cnt == null)
                {

                    tempList.Add(item);
                }

            }

            return tempList;

        }

        public DataTable GetPackageCategory()
        {

            //   SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT * FROM rcs_package_category ");

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];



            return DT;

        }
        public DataTable GetAllRecipe()
        {

            //   SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT * FROM rcs_recipe; ");

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];



            return DT;

        }
        public List<FreeOptionMD> GetFreeOption(string freeOptionId, string freeOptionLimit)
        {
            string[] idList = freeOptionId.Split(',');
            string[] limitList = freeOptionLimit.Split(',');

            List<FreeOptionMD> aOptionMds = new List<FreeOptionMD>();
            for (int i = 0; i < idList.Count() && idList[i].Length > 0; i++)
            {
                try
                {
                    FreeOptionMD aFreeOptionMd = new FreeOptionMD();
                    aFreeOptionMd.CategoryId = Convert.ToInt32(idList[i].Trim());
                    aFreeOptionMd.FreeLimit = Convert.ToInt32(limitList[i].Trim());
                    aOptionMds.Add(aFreeOptionMd);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

            }

            return aOptionMds;

        }

        public List<PackageItemButton> LoadAllSubCategoryWhenFormLoad(PackageCategoryButton category)
        {

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id," +
                                         "rcs_recipe.receipt_name, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                                          " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                                         " where rcs_package_recipe.option_name='{1}' AND rcs_package_recipe.package_id={0};",
                                         category.PackageId, category.OptionName);



            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];




            List<PackageItemButton> aPackageItemButtonList = new List<PackageItemButton>();
            foreach (DataRow dataRow in DT.Rows)
            {


                PackageItemButton aPackageItemButton = new PackageItemButton();
                aPackageItemButton.OptionName = category.OptionName;
                aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                aPackageItemButton.PackageId = category.PackageId;
                aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                aPackageItemButton.AddPrice = category.AddPrice;
                aPackageItemButton.Text = dataRow["name"].ToString();
                aPackageItemButton.Height = 50;

                aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aPackageItemButtonList.Add(aPackageItemButton);

            }



            List<int> catogries = GetCategory(category.CategoryId);

            catogries = MargeCategoty(catogries, aPackageItemButtonList);

            List<int> subCategories = GetCategory(category.SubCategory);

            subCategories = MergeSubcategory(subCategories, aPackageItemButtonList);

            List<PackageItemButton> subCategoryList = GerSubCategoryList(subCategories, category);


            List<PackageItemButton> aPackageItemButtons = new List<PackageItemButton>();
            foreach (PackageItemButton button in subCategoryList)
            {
                button.RecipePackageButton = category.RecipePackage;
                button.PackageCategoryButton = category;
                aPackageItemButtons.Add(button);
            }
            return aPackageItemButtons;

        }
        public List<PackageItemButton> LoadAllPackageReceipeItemForAutoCheck(PackageCategoryButton category)
        {

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id," +
                                         "rcs_recipe.receipt_name, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                                          " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                                         " where rcs_package_recipe.option_name='{1}' AND rcs_package_recipe.package_id={0};",
                                         category.PackageId, category.OptionName);



            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];




            List<PackageItemButton> aPackageItemButtonList = new List<PackageItemButton>();
            foreach (DataRow dataRow in DT.Rows)
            {


                PackageItemButton aPackageItemButton = new PackageItemButton();
                aPackageItemButton.OptionName = category.OptionName;
                aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                aPackageItemButton.PackageId = category.PackageId;
                aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                aPackageItemButton.AddPrice = category.AddPrice;
                aPackageItemButton.Text = dataRow["option_name"].ToString();
                aPackageItemButton.Height = 50;

                aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aPackageItemButtonList.Add(aPackageItemButton);

            }



            return aPackageItemButtonList;
        }
        public List<PackageItem> GetAutoAddPackageItemList(RecipePackageButton category)
        {
            Query = String.Format(
                "SELECT count(rcs_package_recipe.id) OptionCount, rcs_package_recipe.id,rcs_package_category.items,rcs_package_recipe.option_name FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id inner join rcs_package_category  on rcs_package_category.option_name=rcs_package_recipe.option_name and rcs_package_category.package_id=rcs_package_recipe.package_id where rcs_package_recipe.package_id='" + category.PackageId + " ' and rcs_package_category.show_option=0 group by rcs_package_recipe.option_name having  (count(rcs_package_recipe.id)=rcs_package_category.items) or rcs_package_category.items=0 or count(rcs_package_recipe.id)=1 ;");

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];
            List<PackageItem> package_OptionNameList = new List<PackageItem>();

            if (DT.Rows.Count > 0)
            {

                foreach (DataRow dataRow in DT.Rows)
                {
                    package_OptionNameList.Add(new PackageItem { ItemName = dataRow["option_name"].ToString(), Qty = Convert.ToInt32(dataRow["items"]) });
                    //   package_OptionNameList.Add(dataRow["option_name"].ToString());



                }
            }
            return package_OptionNameList;


        }
      
        public List<ReceipeCategoryButton> GetCategoryList(List<int> catogries)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            string query = "";
            List<ReceipeCategoryButton> aList = new List<ReceipeCategoryButton>();

            for (int i = 0; i < catogries.Count; i++)
            {



                Query = String.Format("SELECT rcs_recipe_category.id,rcs_recipe_category.parent_category_id,rcs_recipe_category.recipe_type,rcs_recipe_category.restaurant_id,rcs_recipe_category.sort_order,rcs_recipe_category.max_row,rcs_recipe_category.color,rcs_recipe_category.width,rcs_recipe_category.height,rcs_recipe_category.description,rcs_recipe_category.has_subcategory,case when(rcs_recipe_category.name='') then rcs_recipe_categories.name else rcs_recipe_category.name end AS name  FROM rcs_recipe_category   left join  rcs_recipe_categories on rcs_recipe_categories.id= rcs_recipe_category.parent_category_id  where rcs_recipe_category.id= {0};", catogries[i]);

                Adapter = GetAdapter(Adapter);
                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];


                var Rows = (from row in DT.AsEnumerable()
                            orderby row["sort_order"] ascending
                            select row);
                DT = Rows.AsDataView().ToTable();


                foreach (DataRow dataRow in DT.Rows)
                {

                    ReceipeCategoryButton aReceipeCategoryButton = new ReceipeCategoryButton();
                    aReceipeCategoryButton.CategoryId = Convert.ToInt32(dataRow["id"]);
                    aReceipeCategoryButton.ParentCategoryId = Convert.ToInt32(dataRow["parent_category_id"]);
                    aReceipeCategoryButton.ReceipeTypeId = Convert.ToInt32(dataRow["recipe_type"]);
                    aReceipeCategoryButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                    aReceipeCategoryButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                    aReceipeCategoryButton.MaxRow = Convert.ToInt32(dataRow["max_row"]);
                    aReceipeCategoryButton.Color = Convert.ToString(dataRow["color"]);
                    aReceipeCategoryButton.CategoryWidth = Convert.ToInt32(dataRow["width"]);
                    aReceipeCategoryButton.CategoryHeight = Convert.ToInt32(dataRow["height"]);
                    aReceipeCategoryButton.CategoryName = Convert.ToString(dataRow["name"]);
                    aReceipeCategoryButton.Description = Convert.ToString(dataRow["description"]);
                    aReceipeCategoryButton.HasSubcategory = Convert.ToInt32(dataRow["has_subcategory"]);
                    aReceipeCategoryButton.Height = Convert.ToInt32(dataRow["height"]);
                    aReceipeCategoryButton.Width = 120;
                    aReceipeCategoryButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(aReceipeCategoryButton.Color));
                    aReceipeCategoryButton.FlatAppearance.BorderSize = 0;
                    aReceipeCategoryButton.FlatStyle = FlatStyle.Flat;
                    aReceipeCategoryButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                    aReceipeCategoryButton.ForeColor = Color.White;
                    aReceipeCategoryButton.AutoSize = true;


                    aReceipeCategoryButton.Text = dataRow["name"].ToString();
                    aList.Add(aReceipeCategoryButton);
                }
            }
            return aList;
        }
        public List<int> MergeSubcategory(List<int> cato, List<PackageItemButton> aPackageItemButtonList)
        {
            foreach (PackageItemButton item in aPackageItemButtonList)
            {
                if (item.SubCategoryId > 0)
                {
                    cato.Add(item.SubCategoryId);
                }
            }
            cato = cato.Distinct().ToList();
            cato.Sort();
            return cato;
        }
        public List<int> MargeCategoty(List<int> cato, List<PackageItemButton> aPackageItemButtonList)
        {
            foreach (PackageItemButton item in aPackageItemButtonList)
            {
                if (item.CategoryId > 0)
                {
                    cato.Add(item.CategoryId);
                }
            }
            cato = cato.Distinct().ToList();
            cato.Sort();
            return cato;

        }
        public List<int> GetCategory(string cat)
        {
            string[] category = cat.Split(',');
            List<int> catlist = new List<int>();


            for (int i = 0; i < category.Count(); i++)
            {
                int item;
                if (int.TryParse(category[i], out item))
                {
                    catlist.Add(item);
                }
            }


            return catlist;

        }

        public List<PackageItemButton> GerSubCategoryList(List<int> subCategories, PackageCategoryButton category)
        {


            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            List<PackageItemButton> aList = new List<PackageItemButton>();
            for (int i = 0; i < subCategories.Count; i++)
            {

                Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id," +
                                                                "rcs_recipe.receipt_name,rcs_recipe_subcategory.sort_order,rcs_recipe_subcategory.color,rcs_recipe_subcategory.width,rcs_recipe_subcategory.height, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id, rcs_recipe_subcategory.title" +
                                                                 " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id Inner Join rcs_recipe_subcategory on  rcs_recipe.subcategory_id = rcs_recipe_subcategory.id" +
                                                                " where rcs_package_recipe.package_id={0} AND  rcs_recipe.subcategory_id={1}  Group By rcs_recipe.subcategory_id ;",
                                                                category.PackageId, subCategories[i]);




                Adapter = GetAdapter(Adapter);

                DS.Reset();
                Adapter.Fill(DS);
                DT = DS.Tables[0];





                var Rows = (from row in DT.AsEnumerable()
                            orderby row["title"] ascending
                            orderby row["sort_order"] ascending
                            select row);
                DT = Rows.AsDataView().ToTable();
                System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
                foreach (DataRow dataRow in DT.Rows)
                {


                    PackageItemButton aPackageItemButton = new PackageItemButton();
                    aPackageItemButton.OptionName = Convert.ToString(dataRow["option_name"]);
                    aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                    aPackageItemButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                    aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                    aPackageItemButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                    aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                    aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                    aPackageItemButton.ReciptName = Convert.ToString(dataRow["receipt_name"]);
                    aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                    aPackageItemButton.Text = dataRow["title"].ToString();
                    aPackageItemButton.Height = 40;// Convert.ToInt32(dataRow["height"]);
                    if (workingRectangle.Width > 1280)
                    {
                        aPackageItemButton.Width = 280;
                    }
                    else
                    {
                        aPackageItemButton.Width = 150;
                    }

                    aPackageItemButton.FlatAppearance.BorderSize = 0;
                    aPackageItemButton.FlatStyle = FlatStyle.Flat;
                    aPackageItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(Convert.ToString(dataRow["color"])));
                    aPackageItemButton.Colorname = Convert.ToString(dataRow["color"]);
                    aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                    aPackageItemButton.ForeColor = Color.White;
                    aPackageItemButton.AutoSize = true;

                    aList.Add(aPackageItemButton);


                }
            }

            return aList;



            /*

          
            // SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            List<PackageItemButton> aList = new List<PackageItemButton>();
            for (int i = 0; i < subCategories.Count; i++)
            {
                using (SQLiteConnection c = new SQLiteConnection(GlobalSetting.ConnectionString, true))
                {
                  /*  string query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id,rcs_recipe.receipt_name," +
                                                 "rcs_recipe.receipt_name, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id, rcs_recipe_subcategory.title" +
                                                  " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id Inner Join rcs_recipe_subcategory on  rcs_recipe.subcategory_id = rcs_recipe_subcategory.id" +
                                                 " where rcs_package_recipe.option_name='{1}' AND rcs_package_recipe.package_id={0} AND  rcs_recipe.subcategory_id={2}  Group By rcs_recipe.subcategory_id ;",
                                                 category.PackageId, category.OptionName, subCategories[i]);


                    string query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id," +
                                                                     "rcs_recipe.receipt_name,rcs_recipe_subcategory.sort_order,rcs_recipe_subcategory.color,rcs_recipe_subcategory.width,rcs_recipe_subcategory.height, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id, rcs_recipe_subcategory.title" +
                                                                      " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id Inner Join rcs_recipe_subcategory on  rcs_recipe.subcategory_id = rcs_recipe_subcategory.id" +
                                                                     " where rcs_package_recipe.package_id={0} AND  rcs_recipe.subcategory_id={1}  Group By rcs_recipe.subcategory_id ;",
                                                                     category.PackageId, subCategories[i]);



                    using (SQLiteCommand command = new SQLiteCommand(query, c))
                    {

                        using (SQLiteDataAdapter DB = new SQLiteDataAdapter(query, c))
                        {
                            DS.Reset();
                            DB.Fill(DS);
                            DT = DS.Tables[0];
                        }

                    }
                }

                foreach (DataRow dataRow in DT.Rows)
                {


                    PackageItemButton aPackageItemButton = new PackageItemButton();
                    aPackageItemButton.OptionName = Convert.ToString(dataRow["option_name"]);
                    aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                    aPackageItemButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                    aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                    aPackageItemButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                    aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                    aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                    aPackageItemButton.ReciptName = Convert.ToString(dataRow["receipt_name"]);
                    aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                    aPackageItemButton.Text = dataRow["title"].ToString();
                    aPackageItemButton.Height = 50;
                    aPackageItemButton.FlatAppearance.BorderSize = 0;
                    aPackageItemButton.FlatStyle = FlatStyle.Flat;
                    aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                    aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                    aPackageItemButton.ForeColor = Color.White;
                    aPackageItemButton.AutoSize = true;

                    aList.Add(aPackageItemButton);


                }
            }

            return aList;
            */

        }

        public List<PackageItemButton> GetPackageItemWithoytSubCategoryWhenFormLoad(PackageCategoryButton category)
        {

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.receipt_name, rcs_recipe.subcategory_id,rcs_recipe.category_id,rcs_recipe.receipt_name," +
                                         "rcs_recipe.receipt_name,rcs_recipe.sort_order, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                                          " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                                         " where rcs_package_recipe.option_name='{1}' AND rcs_package_recipe.package_id={0};",
                                         category.PackageId, category.OptionName);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];

            List<PackageItemButton> aPackageItemButtons = new List<PackageItemButton>();
            foreach (DataRow dataRow in DT.Rows)
            {


                PackageItemButton aPackageItemButton = new PackageItemButton();
                aPackageItemButton.OptionName = Convert.ToString(dataRow["option_name"]);
                aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                aPackageItemButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                aPackageItemButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                aPackageItemButton.ReciptName = Convert.ToString(dataRow["receipt_name"]);
                aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                aPackageItemButton.Text = dataRow["name"].ToString();
                aPackageItemButton.Height = 50;
                aPackageItemButton.Width = 200;
                aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aPackageItemButton.PackageCategoryButton = category;
                aPackageItemButton.RecipePackageButton = category.RecipePackage;
                aPackageItemButtons.Add(aPackageItemButton);

            }

            return aPackageItemButtons;
        }

        public List<PackageItemButton> GetPackageItemWithoytSubCategory(PackageCategoryButton category)
        {

            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.receipt_name, rcs_recipe.subcategory_id,rcs_recipe.category_id,rcs_recipe.receipt_name,rcs_package_category.items," +
                                           "rcs_recipe.receipt_name, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                                            " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                                           " LEFT JOIN rcs_package_category ON rcs_package_category.package_id =  rcs_package_recipe.package_id AND rcs_package_category.option_name = rcs_package_recipe.option_name where  rcs_package_recipe.option_name='{1}' AND   rcs_package_recipe.package_id={0} group by  rcs_package_recipe.id order by rcs_package_recipe.option_name;",
                                           category.PackageId, category.OptionName);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];

            List<PackageItemButton> aPackageItemButtons = new List<PackageItemButton>();
            string pre_option_name = "";
            foreach (DataRow dataRow in DT.Rows)
            {



                int package_count = 0;
                packageCount pCount = new packageCount();
                pCount.optionName = Convert.ToString(dataRow["option_name"]);
                pCount.items = Convert.ToInt32(dataRow["items"]);

                PackageItemButton aPackageItemButton = new PackageItemButton();
                foreach (DataRow Row in DT.Rows)
                {



                    if (Convert.ToString(dataRow["option_name"]) == Convert.ToString(Row["option_name"]))
                    {
                        package_count++;
                    }


                }
                pCount.total = package_count;

                aPackageItemButton.CountPackageItem = pCount.getCount();

                aPackageItemButton.OptionName = Convert.ToString(dataRow["option_name"]);
                aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                aPackageItemButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                aPackageItemButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                aPackageItemButton.ReciptName = Convert.ToString(dataRow["receipt_name"]);
                aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                //aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                aPackageItemButton.Text = dataRow["name"].ToString();
                aPackageItemButton.Height = 50;
                aPackageItemButton.FlatAppearance.BorderSize = 0;
                aPackageItemButton.FlatStyle = FlatStyle.Flat;
                aPackageItemButton.AutoSize = true;
                aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                aPackageItemButton.ForeColor = Color.White;
                aPackageItemButton.FreeOptionMds = category.FreeOptionMds;

                aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aPackageItemButton.PackageCategoryButton = category;
                aPackageItemButton.RecipePackageButton = category.RecipePackage;
                aPackageItemButtons.Add(aPackageItemButton);

            }
            return aPackageItemButtons;
        }

        public List<PackageItemButton> GetAllSubCategory(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id," +
                                         "rcs_recipe.receipt_name,rcs_recipe.sort_order, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                                          " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                                         " where rcs_package_recipe.option_name='{1}' AND rcs_package_recipe.package_id={0};",
                                         category.PackageId, category.OptionName);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];

            var Rows = (from row in DT.AsEnumerable()
                        orderby row["name"] ascending
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();
            List<PackageItemButton> aPackageItemButtonList = new List<PackageItemButton>();
            foreach (DataRow dataRow in DT.Rows)
            {


                PackageItemButton aPackageItemButton = new PackageItemButton();
                aPackageItemButton.OptionName = category.OptionName;
                aPackageItemButton.ItemName = Convert.ToString(dataRow["name"]);
                aPackageItemButton.PackageId = category.PackageId;
                aPackageItemButton.RecipeId = Convert.ToInt32(dataRow["recipe_id"]);
                aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                aPackageItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                aPackageItemButton.AddPrice = category.AddPrice;
                aPackageItemButton.Text = dataRow["name"].ToString();
                aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                aPackageItemButton.ForeColor = Color.White;
                aPackageItemButton.FlatAppearance.BorderSize = 0;
                aPackageItemButton.FlatStyle = FlatStyle.Flat;
                aPackageItemButton.AutoSize = true;

                aPackageItemButtonList.Add(aPackageItemButton);

            }

            return aPackageItemButtonList;

        }

        public List<RecipePackageButton> GetPackageByRecipeType(ReceipeTypeButton aReceipeTypeButton)
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT * FROM rcs_package where recipe_type= {0};", aReceipeTypeButton.TypeId);


            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];



            var Rows = (from row in DT.AsEnumerable()
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();

            //    packageTopFlowLayoutPanel.Controls.Clear();
            List<RecipePackageButton> aRecipePackageButtons = new List<RecipePackageButton>();
            foreach (DataRow dataRow in DT.Rows)
            {
                RecipePackageButton aRecipePackageButton = new RecipePackageButton();

                aRecipePackageButton.PackageId = Convert.ToInt32(dataRow["id"]);
                aRecipePackageButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aRecipePackageButton.RecipeTypeId = Convert.ToInt32(dataRow["recipe_type"]);
                aRecipePackageButton.PackageName = Convert.ToString(dataRow["name"]);
                aRecipePackageButton.Description = Convert.ToString(dataRow["description"]);
                aRecipePackageButton.InPrice = Convert.ToDouble(dataRow["in_price"]);
                aRecipePackageButton.OutPrice = Convert.ToDouble(dataRow["out_price"]);

                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipePackageButton.InPrice = aRecipePackageButton.OutPrice;
                }
                aRecipePackageButton.CustomPackage = Convert.ToInt32(dataRow["custom_package"]);
                aRecipePackageButton.ItemLimit = Convert.ToInt32(dataRow["total_item"]);
                aRecipePackageButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                aRecipePackageButton.OnlineName = Convert.ToString(dataRow["online_name"]);
                aRecipePackageButton.DisplayTop = Convert.ToInt32(dataRow["display_top"]);

                aRecipePackageButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                aRecipePackageButton.Text = dataRow["name"].ToString();
                aRecipePackageButton.Height = 36;
                aRecipePackageButton.Width = 100;
                aRecipePackageButton.BackColor = Color.Teal;
                aRecipePackageButton.ForeColor = Color.White;
                aRecipePackageButton.Margin = new Padding(3, 3, 3, 3);
                aRecipePackageButton.FlatStyle = FlatStyle.Flat;
                aRecipePackageButton.FlatAppearance.BorderSize = 0;
                aRecipePackageButtons.Add(aRecipePackageButton);
                // subCategoryMenuItemFlowLayoutPanel.Controls.Add(aReceipeSubCategoryButton);
            }
            return aRecipePackageButtons;
        }


        public List<PackageCategoryButton> GetPackageCategoryWhereNoOption(RecipePackageButton aRecipePackageButton)
        {

            //   SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT * FROM rcs_package_category where package_id= {0} ;", aRecipePackageButton.PackageId);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];


            var Rows = (from row in DT.AsEnumerable()
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();


            List<PackageCategoryButton> aList = new List<PackageCategoryButton>();

            bool isShowOption = false;
            foreach (DataRow dataRow in DT.Rows)
            {

                // MessageBox.Show(Convert.ToString(dataRow["option_name"]));
                PackageCategoryButton aPackageCategoryButton = new PackageCategoryButton();

                aPackageCategoryButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                aPackageCategoryButton.CategoryId = Convert.ToString(dataRow["category_id"]);
                aPackageCategoryButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                aPackageCategoryButton.SubCategory = Convert.ToString(dataRow["subcategory_id"]);
                aPackageCategoryButton.Items = Convert.ToInt32(dataRow["items"]);
                aPackageCategoryButton.OptionName = Convert.ToString(dataRow["option_name"]);
                aPackageCategoryButton.AllRecipe = Convert.ToInt32(dataRow["all_recipe"]);
                aPackageCategoryButton.ShowOption = Convert.ToInt32(dataRow["show_option"]);
                aPackageCategoryButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                aPackageCategoryButton.RecipeTypeId = aRecipePackageButton.RecipeTypeId;

                aPackageCategoryButton.Width = 200;
                if (aPackageCategoryButton.ShowOption > 0 && !isShowOption)
                {
                    isShowOption = true;
                    aList = new List<PackageCategoryButton>();
                }
                else if (!isShowOption)
                {
                    aList.Add(aPackageCategoryButton);

                }
            }

            List<PackageCategoryButton> tempList = new List<PackageCategoryButton>();
            foreach (PackageCategoryButton list in aList)
            {
                PackageCategoryButton item = list;
                bool flag = false;


                foreach (PackageCategoryButton cat in aList)
                {

                    if (cat.OptionName == list.OptionName && list != cat)
                    {
                        if (cat.CategoryId != null)
                        {
                            item.CategoryId += "," + cat.CategoryId;
                        }
                        if (cat.SubCategory != "")
                        {
                            item.SubCategory += "," + cat.SubCategory;
                        }
                        flag = true;
                    }


                }

                var cnt = tempList.SingleOrDefault(a => a.OptionName == item.OptionName);
                if (cnt == null)
                {

                    tempList.Add(item);
                }

            }
            return tempList;
        }

        public DataTable Get_AllOption(int optionId)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT * FROM rcs_recipe_option where id={0}", optionId);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];
            return DT;
        }

        public DataTable GetPackagerReceipeByPackageItem(PackageItemButton catid)
        {
            DataTable DT = new DataTable();

            if (catid.RecipeId > 0)
            {
                Query = String.Format("SELECT id,name,out_price,in_price,receipt_name,subcategory_id,category_id" +
                                      " FROM rcs_recipe WHERE  rcs_recipe.category_id IN ({0});",
                    Convert.ToString(catid.CategoryId));


            }
            else
            {
                Query =
                    String.Format(
                        "SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.receipt_name, rcs_recipe.subcategory_id,rcs_recipe.category_id,rcs_recipe.receipt_name," +
                        "rcs_recipe.receipt_name, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                        " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                        " where rcs_package_recipe.option_name='{1}' AND rcs_package_recipe.package_id={0} AND rcs_recipe.category_id IN ({2});",
                        catid.PackageId, catid.OptionName, Convert.ToString(catid.CategoryId));
            }

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            Reader.Close();
          
            return DT;
        }

        public DataTable GetPackagerReceipeByPackageItemWithSubCat(PackageCategoryButton catid)
        {
            DataTable DT = new DataTable();

            if (catid.all_recipe > 0)
            {
                Query =  String.Format("SELECT rcs_recipe.* FROM rcs_recipe WHERE rcs_recipe.subcategory_id IN (@subcategory_id) AND rcs_recipe.category_id IN (@category_id);");
                //, Convert.ToString(mainData["subcategory_id"]),Convert.ToString(mainData["category_id"]);


            }
            else
            {
                Query = String.Format(
                                "SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id," +
                                "rcs_recipe.receipt_name,rcs_recipe.sort_order,  rcs_package_recipe.package_id,rcs_package_recipe.recipe_id FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = " +
                                "rcs_recipe.id where rcs_package_recipe.option_name=@option_name AND rcs_package_recipe.package_id=@option_name AND rcs_recipe.subcategory_id IN (@subcategory_id) AND rcs_recipe.category_id IN (@subcategory_id) order by rcs_recipe.name ASC , rcs_recipe.sort_order asc;");
                    
            }

            command = CommandMethod(command);
            command.Parameters.AddWithValue("@option_name", catid.OptionName);
            command.Parameters.AddWithValue("@package_id", catid.PackageId);
            command.Parameters.AddWithValue("@subcategory_id", catid.SubCategoryId);
            command.Parameters.AddWithValue("@category_id", catid.CategoryId.TrimEnd(','));

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            Reader.Close();

            return DT;
        }
    }


    class packageCount
    {
        public string optionName { set; get; }
        public int items { set; get; }
        public int total { set; get; }

        public int getCount()
        {

            if (this.items > this.total)
            {
                return this.items;
            }

            if (this.items == 0)
            {
                return 1;
            }


            return this.total / this.items;
        }
    }
}
