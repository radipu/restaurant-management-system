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

namespace TomaFoodRestaurant.DAL.DAO
{
    public class RestaurantMenuDAO : GatewayConnection
    {
        internal RecipeOptionItemButton GetRecipeOptionByOptionId(int optionId)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            RecipeOptionItemButton aRecipeOptionItemButton = new RecipeOptionItemButton();

            Query = String.Format("select *  FROM   rcs_option_item where id='{0}' ;", optionId);


            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {

                aRecipeOptionItemButton.RecipeOptionItemId = Convert.ToInt32(Reader["id"]);
                aRecipeOptionItemButton.ParentOptionId = Convert.ToInt32(Reader["parent_option_id"]);
                aRecipeOptionItemButton.RecipeOptionId = Convert.ToInt32(Reader["recipe_option_id"]);
                aRecipeOptionItemButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aRecipeOptionItemButton.Title = Convert.ToString(Reader["title"]);
                aRecipeOptionItemButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aRecipeOptionItemButton.Price = Convert.ToDouble(Reader["price"]);

                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipeOptionItemButton.InPrice = aRecipeOptionItemButton.Price;
                }
                aRecipeOptionItemButton.Text = Reader["title"].ToString();
                aRecipeOptionItemButton.Height = 45;
                aRecipeOptionItemButton.Width = 120;
                aRecipeOptionItemButton.BackColor = ColorTranslator.FromHtml("#337ab7");

            }
            return aRecipeOptionItemButton;}
        internal ReceipeMenuItemButton GetRecipeByItemId(int recipeId)
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            ReceipeMenuItemButton aReceipeMenuItemButton = new ReceipeMenuItemButton();

            Query = String.Format("SELECT * FROM rcs_recipe where id= {0};", recipeId);
            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);

            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {
                aReceipeMenuItemButton.RecipeMenuItemId = Convert.ToInt32(Reader["id"]);
                aReceipeMenuItemButton.ParentId = Convert.ToInt32(Reader["parent_id"]);
                aReceipeMenuItemButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aReceipeMenuItemButton.CategoryId = Convert.ToInt32(Reader["category_id"]);
                aReceipeMenuItemButton.SubCategoryId = Convert.ToInt32(Reader["subcategory_id"]);
                aReceipeMenuItemButton.ItemName = (Reader["name"].ToString());
                aReceipeMenuItemButton.ReceiptName = (Reader["receipt_name"].ToString());
                aReceipeMenuItemButton.ShortDescrip = (Reader["shortdesc"]).ToString();
                aReceipeMenuItemButton.LongDescrip = (Reader["longdesc"].ToString());
                aReceipeMenuItemButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aReceipeMenuItemButton.OutPrice = Convert.ToDouble(Reader["out_price"]);

                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aReceipeMenuItemButton.InPrice = aReceipeMenuItemButton.OutPrice;
                }

                aReceipeMenuItemButton.DiscountPrice = Convert.ToDouble(Reader["discount_price"]);
                aReceipeMenuItemButton.SortOrder = Convert.ToInt32(Reader["sort_order"]);
                aReceipeMenuItemButton.KitchenSection = Convert.ToInt32(Reader["kitchen_section"]);
                aReceipeMenuItemButton.IsExclusixe = Convert.ToInt32(Reader["is_exclusive"]);
                aReceipeMenuItemButton.Hot = Convert.ToInt32(Reader["hot"]);
                aReceipeMenuItemButton.Nut = Convert.ToInt32(Reader["nut"]);
                aReceipeMenuItemButton.ShowCategory = Convert.ToInt32(Reader["show_category"]);
                aReceipeMenuItemButton.ButtonWidth = Convert.ToInt32(Reader["width"]);
                aReceipeMenuItemButton.ButtonHeight = Convert.ToInt32(Reader["height"]);
                aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize),
                    urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
            }
            return aReceipeMenuItemButton;
        }

        internal RecipeOptionItemButton GetRecipeOptionByOptionName(string optionName)
        {

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            RecipeOptionItemButton aRecipeOptionItemButton = new RecipeOptionItemButton();

            Query = String.Format("select *  FROM   rcs_option_item where title='{0}' ;", optionName);


            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {

                aRecipeOptionItemButton.RecipeOptionItemId = Convert.ToInt32(Reader["id"]);
                aRecipeOptionItemButton.ParentOptionId = Convert.ToInt32(Reader["parent_option_id"]);
                aRecipeOptionItemButton.RecipeOptionId = Convert.ToInt32(Reader["recipe_option_id"]);
                aRecipeOptionItemButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aRecipeOptionItemButton.Title = Convert.ToString(Reader["title"]);
                aRecipeOptionItemButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aRecipeOptionItemButton.Price = Convert.ToDouble(Reader["price"]);

                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipeOptionItemButton.InPrice = aRecipeOptionItemButton.Price;
                }
                aRecipeOptionItemButton.Text = Reader["title"].ToString();
                aRecipeOptionItemButton.Height = 45;
                aRecipeOptionItemButton.Width = 120;
                aRecipeOptionItemButton.BackColor = ColorTranslator.FromHtml("#337ab7");

            }
            return aRecipeOptionItemButton;
        }

        internal RecipePackageButton GetPackageByPackageId(int packageId)
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            RecipePackageButton aRecipePackageButton = new RecipePackageButton();

            Query = String.Format("SELECT * FROM rcs_package where id= {0};", packageId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                aRecipePackageButton.PackageId = Convert.ToInt32(Reader["id"]);
                aRecipePackageButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aRecipePackageButton.RecipeTypeId = Convert.ToInt32(Reader["recipe_type"]);
                aRecipePackageButton.PackageName = Convert.ToString(Reader["name"]);
                aRecipePackageButton.Description = Convert.ToString(Reader["description"]);
                aRecipePackageButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aRecipePackageButton.OutPrice = Convert.ToDouble(Reader["out_price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipePackageButton.InPrice = aRecipePackageButton.OutPrice;
                }
                aRecipePackageButton.CustomPackage = Convert.ToInt32(Reader["custom_package"]);
                aRecipePackageButton.ItemLimit = Convert.ToInt32(Reader["total_item"]);
                aRecipePackageButton.SortOrder = Convert.ToInt32(Reader["sort_order"]);
                aRecipePackageButton.OnlineName = Convert.ToString(Reader["online_name"]);
                aRecipePackageButton.DisplayTop = Convert.ToInt32(Reader["display_top"]);
                aRecipePackageButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                //  aRecipePackageButton.Click += new EventHandler(RecipePackageButton_Click);
                aRecipePackageButton.Text = Reader["name"].ToString();
                aRecipePackageButton.Height = 36;
                aRecipePackageButton.BackColor = Color.BlanchedAlmond;
            }


            return aRecipePackageButton;
        }
        internal RecipePackageButton GetPackageByP()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            RecipePackageButton aRecipePackageButton = new RecipePackageButton();

            Query = String.Format("SELECT * FROM rcs_package");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                aRecipePackageButton.PackageId = Convert.ToInt32(Reader["id"]);
                aRecipePackageButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aRecipePackageButton.RecipeTypeId = Convert.ToInt32(Reader["recipe_type"]);
                aRecipePackageButton.PackageName = Convert.ToString(Reader["name"]);
                aRecipePackageButton.Description = Convert.ToString(Reader["description"]);
                aRecipePackageButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aRecipePackageButton.OutPrice = Convert.ToDouble(Reader["out_price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipePackageButton.InPrice = aRecipePackageButton.OutPrice;
                }
                aRecipePackageButton.CustomPackage = Convert.ToInt32(Reader["custom_package"]);
                aRecipePackageButton.ItemLimit = Convert.ToInt32(Reader["total_item"]);
                aRecipePackageButton.SortOrder = Convert.ToInt32(Reader["sort_order"]);
                aRecipePackageButton.OnlineName = Convert.ToString(Reader["online_name"]);
                aRecipePackageButton.DisplayTop = Convert.ToInt32(Reader["display_top"]);
                aRecipePackageButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                //  aRecipePackageButton.Click += new EventHandler(RecipePackageButton_Click);
                aRecipePackageButton.Text = Reader["name"].ToString();
                aRecipePackageButton.Height = 36;
                aRecipePackageButton.BackColor = Color.BlanchedAlmond;
            }


            return aRecipePackageButton;
        }



        internal string GetSubcategoryByCatAndSubcat(int categoryId, int subcategoryId)
        {
            string margeName = "";
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT name FROM rcs_recipe where category_id= {0} and  subcategory_id= {1};", categoryId, subcategoryId);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    margeName = Convert.ToString(Reader["name"]);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }


            return margeName;
        }




        public PackageCategoryButton GetPackagePackageCategory(PackageItemButton itemButton)
        {

            //   SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT * FROM rcs_package_category where category_id like '%{0}%'  AND package_id={1} AND option_name='{2}'", itemButton.CategoryId, itemButton.PackageId, itemButton.OptionName);

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];



            PackageCategoryButton aPackageCategoryButton = new PackageCategoryButton();

            foreach (DataRow dataRow in DT.Rows)
            {

                // MessageBox.Show(Convert.ToString(dataRow["option_name"]));


                aPackageCategoryButton.PackageId = Convert.ToInt32(dataRow["package_id"]);
                aPackageCategoryButton.CategoryId = Convert.ToString(dataRow["category_id"]);
                aPackageCategoryButton.AddPrice = Convert.ToDouble(dataRow["add_price"]);
                aPackageCategoryButton.SubCategory = Convert.ToString(dataRow["subcategory_id"]);
                aPackageCategoryButton.Items = Convert.ToInt32(dataRow["items"]);
                aPackageCategoryButton.OptionName = Convert.ToString(dataRow["option_name"]);
                aPackageCategoryButton.AllRecipe = Convert.ToInt32(dataRow["all_recipe"]);
                aPackageCategoryButton.Font = new System.Drawing.Font("Segoe UI", 14);
                aPackageCategoryButton.FlatAppearance.BorderSize = 0;
                aPackageCategoryButton.Height = 50;
                aPackageCategoryButton.FlatStyle = FlatStyle.Flat;
                aPackageCategoryButton.AutoSize = true;
                aPackageCategoryButton.ShowOption = Convert.ToInt32(dataRow["show_option"]);
                aPackageCategoryButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);


            }

            return aPackageCategoryButton;

        }

        internal List<RecipePackageButton> GetPackageByMenuType(int menuType)
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            List<RecipePackageButton> aRecipePackageButtons = new List<RecipePackageButton>();

            if (menuType == 0)
            {
                Query = String.Format("SELECT * FROM rcs_package");

            }
            else
            {
                Query = String.Format("SELECT * FROM rcs_package where recipe_type= {0};", menuType);

            } 
           

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read())
            {
                RecipePackageButton aRecipePackageButton = new RecipePackageButton();
                aRecipePackageButton.PackageId = Convert.ToInt32(Reader["id"]);
                aRecipePackageButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aRecipePackageButton.RecipeTypeId = Convert.ToInt32(Reader["recipe_type"]);
                aRecipePackageButton.PackageName = Convert.ToString(Reader["name"]);
                aRecipePackageButton.Description = Convert.ToString(Reader["description"]);
                aRecipePackageButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aRecipePackageButton.OutPrice = Convert.ToDouble(Reader["out_price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipePackageButton.InPrice = aRecipePackageButton.OutPrice;
                }
                aRecipePackageButton.CustomPackage = Convert.ToInt32(Reader["custom_package"]);
                aRecipePackageButton.ItemLimit = Convert.ToInt32(Reader["total_item"]);
                aRecipePackageButton.SortOrder = Convert.ToInt32(Reader["sort_order"]);
                aRecipePackageButton.OnlineName = Convert.ToString(Reader["online_name"]);
                aRecipePackageButton.DisplayTop = Convert.ToInt32(Reader["display_top"]);
                aRecipePackageButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                //  aRecipePackageButton.Click += new EventHandler(RecipePackageButton_Click);
                aRecipePackageButton.Text = Reader["name"].ToString();
                aRecipePackageButton.Height = 36;
                aRecipePackageButton.BackColor = Color.BlanchedAlmond;
                aRecipePackageButtons.Add(aRecipePackageButton);
            }

            aRecipePackageButtons = aRecipePackageButtons.OrderBy(a => a.SortOrder).ToList();
            return aRecipePackageButtons;
        }

        internal PackageItemButton GetRecipeByItemIdForPackage(int recipeId)
        {
            PackageItemButton aPackageItemButton = new PackageItemButton();
            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_recipe.name, rcs_recipe.subcategory_id,rcs_recipe.category_id,rcs_recipe.receipt_name," +
                                                  "rcs_recipe.receipt_name, rcs_package_recipe.package_id,rcs_package_recipe.recipe_id" +
                                                   " FROM rcs_package_recipe INNER JOIN  rcs_recipe ON rcs_package_recipe.recipe_id = rcs_recipe.id" +
                                                  " where rcs_recipe.id={0};", recipeId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {

                aPackageItemButton.OptionName = Convert.ToString(Reader["option_name"]);
                aPackageItemButton.ItemName = Convert.ToString(Reader["name"]);
                aPackageItemButton.PackageId = Convert.ToInt32(Reader["package_id"]);
                aPackageItemButton.RecipeId = Convert.ToInt32(Reader["recipe_id"]);
                aPackageItemButton.AddPrice = Convert.ToDouble(Reader["add_price"]);
                aPackageItemButton.ReciptName = Convert.ToString(Reader["receipt_name"]);
                aPackageItemButton.SubCategoryId = Convert.ToInt32(Reader["subcategory_id"]);
                aPackageItemButton.CategoryId = Convert.ToInt32(Reader["category_id"]);
                // aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                aPackageItemButton.Text = Reader["name"].ToString();
                aPackageItemButton.Height = 36;
                aPackageItemButton.BackColor = Color.Chartreuse;
                //  aPackageItemButton.PackageCategoryButton = category;
                // aPackageItemButton.RecipePackageButton = category.RecipePackage;
            }

            return aPackageItemButton;
        }

        internal PackageItemExtraPrice GetPackageItemPrice(int recipeId, int packageId)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            PackageItemExtraPrice aPackageItemButton = new PackageItemExtraPrice();


           // Query = String.Format("SELECT from GetPackageItemPrice where rcs_package_recipe.recipe_id={0} AND rcs_package_recipe.package_id={1}", recipeId, packageId);

            Query = String.Format("SELECT rcs_package_recipe.option_name,rcs_package_recipe.add_price, rcs_package_category.add_price as ex_price," +
                                                    "rcs_package_recipe.package_id,rcs_package_recipe.recipe_id FROM rcs_package_recipe " +
                                                    "INNER JOIN  rcs_package_category ON rcs_package_category.option_name = rcs_package_recipe.option_name  " +
                                                    "AND rcs_package_category.package_id = rcs_package_recipe.package_id " +
                                                    "where rcs_package_recipe.recipe_id={0} AND rcs_package_recipe.package_id={1}", recipeId, packageId);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {

                aPackageItemButton.OptionName = Convert.ToString(Reader["option_name"]);
                aPackageItemButton.PackageId = Convert.ToInt32(Reader["package_id"]);
                aPackageItemButton.RecipeId = Convert.ToInt32(Reader["recipe_id"]);
                aPackageItemButton.AddPrice = Convert.ToDouble(Reader["add_price"]);
                aPackageItemButton.CategoryAddPrice = Convert.ToDouble(Reader["ex_price"]);
                if (aPackageItemButton.AddPrice <= 0)
                {
                    aPackageItemButton.AddPrice = aPackageItemButton.CategoryAddPrice;
                }
            }


            if (aPackageItemButton.RecipeId <= 0)
            {
                aPackageItemButton.RecipeId = 0;
                aPackageItemButton.AddPrice = 0;
            }
            return aPackageItemButton;
        }


        internal List<ReceipeTypeButton> GetRecipeType()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            List<ReceipeTypeButton> aRecipeTypeList = new List<ReceipeTypeButton>();
            Query = "SELECT * FROM rcs_recipe_type;";
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

                ReceipeTypeButton aReceipeTypeButton = new ReceipeTypeButton();
                aReceipeTypeButton.TypeId = Convert.ToInt32(dataRow["id"]);
                aReceipeTypeButton.ParentTypeId = Convert.ToInt32(dataRow["parent_type_id"]);
                aReceipeTypeButton.TypeName = dataRow["name"].ToString();
                aReceipeTypeButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aReceipeTypeButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                aReceipeTypeButton.MergeItems = Convert.ToInt32(dataRow["merge_items"]);
                aReceipeTypeButton.CategoryWidth = Convert.ToInt32(dataRow["category_width"]);
                aReceipeTypeButton.PackageWidth = Convert.ToInt32(dataRow["package_width"]);
                aReceipeTypeButton.SubcategoryWidth = Convert.ToInt32(dataRow["subcategory_width"]);
                aReceipeTypeButton.Text = dataRow["name"].ToString();
                aReceipeTypeButton.Height = 50;
                aReceipeTypeButton.FlatAppearance.BorderSize = 0;
                aReceipeTypeButton.FlatStyle = FlatStyle.Flat;
                aReceipeTypeButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aReceipeTypeButton.ForeColor = Color.White;
                aReceipeTypeButton.AutoSize = true;

                try
                {
                    aReceipeTypeButton.BackColor = ColorTranslator.FromHtml("#3078d7");
                    aReceipeTypeButton.MinimumSize = new Size(120, 50);
                    aReceipeTypeButton.Margin = new Padding(0, 0, 10, 0);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

                aRecipeTypeList.Add(aReceipeTypeButton);

            }

            return aRecipeTypeList;
        }

        internal int GetCategoryByName(string name)
        {
            int starterId = 0;
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT rcs_recipe_category.id,case when(rcs_recipe_category.name='') then rcs_recipe_categories.name else rcs_recipe_category.name end AS name FROM rcs_recipe_category  left join  rcs_recipe_categories on rcs_recipe_categories.id= rcs_recipe_category.parent_category_id where case when(rcs_recipe_category.name='') then rcs_recipe_categories.name='{0}' else rcs_recipe_category.name='{0}' end;", name);


            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    starterId = Convert.ToInt32(Reader["id"]);


                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }


            return starterId;
        }

        public ReceipeCategoryButton GetCategoryByCategoryId(int categoryId)
        {

            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();


            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            Query = String.Format("SELECT * FROM rcs_recipe_category where id= {0};", categoryId);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];


            var Rows = (from row in DT.AsEnumerable()
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();


            ReceipeCategoryButton aReceipeCategoryButton = new ReceipeCategoryButton();
            foreach (DataRow dataRow in DT.Rows)
            {


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
                aReceipeCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aReceipeCategoryButton.Margin = new Padding(1, 1, 1, 1);

                aReceipeCategoryButton.Height = 36;
                // aReceipeCategoryButton.Width = 100;
                aReceipeCategoryButton.Text = dataRow["name"].ToString();




            }
            return aReceipeCategoryButton;
        }

        public List<ReceipeCategoryButton> GetAllCategory()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            List<ReceipeCategoryButton> allCategory = new List<ReceipeCategoryButton>();

            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            // string query = String.Format("SELECT * FROM rcs_recipe_category;");
            Query = String.Format("select rcs_recipe_categories.id as parent_id,rcs_recipe_category.id, rcs_recipe_category.parent_category_id, rcs_recipe_category.recipe_type," +
              "rcs_recipe_category.restaurant_id, rcs_recipe_category.sort_order, rcs_recipe_category.max_row, rcs_recipe_category.color, rcs_recipe_category.width, rcs_recipe_category.height," +
             "case when(rcs_recipe_category.name='') then rcs_recipe_categories.name else rcs_recipe_category.name end AS name, case when(rcs_recipe_category.description='') then rcs_recipe_categories.description else rcs_recipe_category.description end AS description," +
            "case  when(rcs_recipe_category.has_subcategory<=0 AND rcs_recipe_categories.has_subcategory>0) then rcs_recipe_categories.has_subcategory else rcs_recipe_category.has_subcategory  end AS has_subcategory from rcs_recipe_category  left join  rcs_recipe_categories on rcs_recipe_categories.id= rcs_recipe_category.parent_category_id;");

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

                aReceipeCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aReceipeCategoryButton.Margin = new Padding(1, 1, 1, 1);

                aReceipeCategoryButton.Height = 36;
                // aReceipeCategoryButton.Width = 100;
                aReceipeCategoryButton.Text = dataRow["name"].ToString();

                allCategory.Add(aReceipeCategoryButton);




            }
            allCategory = allCategory.AsEnumerable().OrderBy(d => d.SortOrder).ThenBy(a => a.CategoryName).GroupBy(d => d.CategoryName).SelectMany(g => g).ToList();
            // allCategory = allCategory.GroupBy(a => a.CategoryName).SelectMany(b=>b).ToList();
            return allCategory;
        }

        internal List<ReceipeSubCategoryButton> GetAllSubcategory()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();


            // SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("select rcs_recipe_subcategory.id,rcs_recipe_subcategory.parent_subcategory_id,rcs_recipe_subcategory.restaurant_id,rcs_recipe_subcategory.recipe_type,rcs_recipe_subcategory.sort_order,rcs_recipe_subcategory.online_sort_order,rcs_recipe_subcategory.exclude_table,rcs_recipe_subcategory.hot,rcs_recipe_subcategory.nut,rcs_recipe_subcategory.color,rcs_recipe_subcategory.width,rcs_recipe_subcategory.height,CASE WHEN rcs_recipe_subcategory.title='' THEN  rcs_recipe_subcategories.title ELSE  rcs_recipe_subcategory.title END AS title,CASE WHEN rcs_recipe_subcategory.description='' THEN rcs_recipe_subcategories.description ELSE rcs_recipe_subcategory.description END AS description,CASE WHEN rcs_recipe_subcategory.name='' THEN rcs_recipe_subcategories.name ELSE rcs_recipe_subcategory.name END AS name from rcs_recipe_subcategory left join rcs_recipe_subcategories on rcs_recipe_subcategories.id = rcs_recipe_subcategory.parent_subcategory_id");

            Adapter = GetAdapter(Adapter);


            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];



            //  urls = GetUrls(); 
            var Rows = (from row in DT.AsEnumerable()
                        orderby row["name"] ascending
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();

            List<ReceipeSubCategoryButton> allSubcategory = new List<ReceipeSubCategoryButton>();
            foreach (DataRow dataRow in DT.Rows)
            {
                ReceipeSubCategoryButton aReceipeSubCategoryButton = new ReceipeSubCategoryButton();


                aReceipeSubCategoryButton.SubCategoryId = Convert.ToInt32(dataRow["id"]);
                aReceipeSubCategoryButton.ParentSubcategoryId = Convert.ToInt32(dataRow["parent_subcategory_id"]);
                aReceipeSubCategoryButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aReceipeSubCategoryButton.RecipeTypeId = Convert.ToInt32(dataRow["recipe_type"]);
                aReceipeSubCategoryButton.Title = Convert.ToString(dataRow["title"]);
                aReceipeSubCategoryButton.SubCategoryName = Convert.ToString(dataRow["name"]);
                aReceipeSubCategoryButton.Description = Convert.ToString(dataRow["description"]);
                aReceipeSubCategoryButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                aReceipeSubCategoryButton.OnlineSortOrder = Convert.ToInt32(dataRow["online_sort_order"]);
                aReceipeSubCategoryButton.ExcludeTable = Convert.ToInt32(dataRow["exclude_table"]);
                aReceipeSubCategoryButton.Hot = Convert.ToInt32(dataRow["hot"]);
                aReceipeSubCategoryButton.Nut = Convert.ToInt32(dataRow["nut"]);
                aReceipeSubCategoryButton.ButtonColor = Convert.ToString(dataRow["color"]);
                aReceipeSubCategoryButton.ButtonHeight = Convert.ToInt32(dataRow["height"]);//height
                if (aReceipeSubCategoryButton.Title == "Create Your Own")
                {
                    int wi = Convert.ToInt32(dataRow["width"]);
                }
                aReceipeSubCategoryButton.ButtonWidth = Convert.ToInt32(dataRow["width"]);
                aReceipeSubCategoryButton.ForeColor = Color.White;
                aReceipeSubCategoryButton.Padding = new Padding(0, 0, 0, 0);
                aReceipeSubCategoryButton.Margin = new Padding(1, 1, 1, 1);
                aReceipeSubCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aReceipeSubCategoryButton.FlatStyle = FlatStyle.Flat;
                aReceipeSubCategoryButton.FlatAppearance.BorderSize = 0;
                aReceipeSubCategoryButton.Text = dataRow["title"].ToString();
                aReceipeSubCategoryButton.Height = aReceipeSubCategoryButton.ButtonHeight;
                allSubcategory.Add(aReceipeSubCategoryButton);

            }
            //  allSubcategory = allSubcategory.AsEnumerable().OrderBy(d => d.SortOrder).ToList();
            //  allSubcategory = allSubcategory.AsEnumerable().OrderBy(a=>a.SubCategoryId>0).GroupBy(a => a.ButtonColor).ToList();

            List<ReceipeSubCategoryButton> ss = allSubcategory.GroupBy(x => x.ButtonColor).Select(g => g.FirstOrDefault()).ToList();
            List<ReceipeSubCategoryButton> newSubcategory = new List<ReceipeSubCategoryButton>();
            foreach (ReceipeSubCategoryButton colorBUtton in ss)
            {
                List<ReceipeSubCategoryButton> tempCat =
                    allSubcategory.Where(a => a.ButtonColor == colorBUtton.ButtonColor).ToList();
                newSubcategory.AddRange(tempCat);
            }
            return newSubcategory;
        }

        internal List<ReceipeMenuItemButton> AllRecipeButton()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            List<ReceipeMenuItemButton> allRecipeButton = new List<ReceipeMenuItemButton>();

            //   string query = String.Format("SELECT * FROM rcs_recipe where category_id= {0};", aReceipeCategoryButton.CategoryId);
            Query = String.Format("SELECT * FROM rcs_recipe;");

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];

            var Rows = (from row in DT.AsEnumerable()
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();
            // aFlowLayoutPanel.AutoSize = true;
            foreach (DataRow dataRow in DT.Rows)
            {
                ReceipeMenuItemButton aReceipeMenuItemButton = new ReceipeMenuItemButton();
                aReceipeMenuItemButton.RecipeMenuItemId = Convert.ToInt32(dataRow["id"]);
                aReceipeMenuItemButton.ParentId = Convert.ToInt32(dataRow["parent_id"]);
                aReceipeMenuItemButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aReceipeMenuItemButton.CategoryId = Convert.ToInt32(dataRow["category_id"]);
                aReceipeMenuItemButton.SubCategoryId = Convert.ToInt32(dataRow["subcategory_id"]);
                aReceipeMenuItemButton.ItemName = (dataRow["name"].ToString());
                aReceipeMenuItemButton.ReceiptName = (dataRow["receipt_name"].ToString());
                aReceipeMenuItemButton.ShortDescrip = (dataRow["shortdesc"]).ToString();
                aReceipeMenuItemButton.LongDescrip = (dataRow["longdesc"].ToString());
                aReceipeMenuItemButton.InPrice = Convert.ToDouble(dataRow["in_price"]);
                aReceipeMenuItemButton.OutPrice = Convert.ToDouble(dataRow["out_price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aReceipeMenuItemButton.InPrice = aReceipeMenuItemButton.OutPrice;
                }
                aReceipeMenuItemButton.DiscountPrice = Convert.ToDouble(dataRow["discount_price"]);
                aReceipeMenuItemButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                aReceipeMenuItemButton.KitchenSection = Convert.ToInt32(dataRow["kitchen_section"]);
                aReceipeMenuItemButton.IsExclusixe = Convert.ToInt32(dataRow["is_exclusive"]);
                aReceipeMenuItemButton.Hot = Convert.ToInt32(dataRow["hot"]);
                aReceipeMenuItemButton.Nut = Convert.ToInt32(dataRow["nut"]);
                aReceipeMenuItemButton.ShowCategory = Convert.ToInt32(dataRow["show_category"]);
                if (aReceipeMenuItemButton.ShowCategory == 0)
                {
                    aReceipeMenuItemButton.ShowCategory = aReceipeMenuItemButton.CategoryId;
                }
                aReceipeMenuItemButton.ButtonWidth = Convert.ToInt32(dataRow["width"]);
                aReceipeMenuItemButton.ButtonHeight = Convert.ToInt32(dataRow["height"]);
                aReceipeMenuItemButton.FlatStyle = FlatStyle.Flat;
                aReceipeMenuItemButton.FlatAppearance.BorderSize = 0;
                aReceipeMenuItemButton.ForeColor = Color.White;
                aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);
                aReceipeMenuItemButton.Height = aReceipeMenuItemButton.ButtonHeight;
                aReceipeMenuItemButton.Text = dataRow["name"].ToString();

                allRecipeButton.Add(aReceipeMenuItemButton);



            }

            return allRecipeButton;

        }

        public bool GetReceipeOptionsByItemId(int recipeMenuItemId)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            bool flag = false;
            int itemIndex = 0;

            Query = String.Format("select *  FROM   rcs_recipe_option as ro INNER JOIN rcs_restaurant_recipe_option as rro ON ro.id = rro.option_id  where rro.recipe_id= {0} order by ro.title ;",
                   recipeMenuItemId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);


            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {
                flag = true;
            }

            return flag;
        }

        public List<AttributeButton> GetAllAttributeButton()
        {
            GlobalUrl urls = new GlobalUrl();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            urls = aGlobalUrlBll.GetUrls();


            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            List<AttributeButton> attributeButtons = new List<AttributeButton>();


            Query = String.Format("SELECT * FROM rcs_attribute;");

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];



            var Rows = (from row in DT.AsEnumerable()
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();

            int count = DT.Rows.Count;
            int height = (count / 10);
            if (count % 10 != 0) height += 1;



            //    Font newFont = new Font(Font.FontFamily, 14,FontStyle.Bold);
            foreach (DataRow dataRow in DT.Rows)
            {
                AttributeButton aAttributeButton = new AttributeButton();
                aAttributeButton.AttributeId = Convert.ToInt32(dataRow["id"]);
                aAttributeButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aAttributeButton.AttributeName = Convert.ToString(dataRow["name"]);
                aAttributeButton.Price = Convert.ToDouble(dataRow["price"]);
                aAttributeButton.AttributeUnit = Convert.ToString(dataRow["unit"]);
                aAttributeButton.SortOrder = Convert.ToInt32(dataRow["sort_order"]);
                aAttributeButton.Discount = Convert.ToDouble(dataRow["discount"]);
                aAttributeButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize),
                    urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                if (dataRow.Table.Columns.Contains("color"))
                {
                    aAttributeButton.AttributeColor = Convert.ToString(dataRow["color"]);
                    aAttributeButton.BackColor =
                        ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(aAttributeButton.AttributeColor));
                    aAttributeButton.ForeColor = Color.White;
                }
                else
                {
                    aAttributeButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("#967adc"));

                    aAttributeButton.ForeColor = Color.White;
                }

                //   aAttributeButton.Font = newFont;
                //  aAttributeButton.Click += new EventHandler(AttributeButton_Click);
                aAttributeButton.Text = dataRow["name"].ToString();
                aAttributeButton.Height = 60;
                aAttributeButton.FlatAppearance.BorderSize = 0;
                aAttributeButton.FlatStyle = FlatStyle.Flat;
                aAttributeButton.AutoSize = true;
                attributeButtons.Add(aAttributeButton);
                //attributeFlowLayoutPanel.Controls.Add(aAttributeButton);



            }
            return attributeButtons;

        }

        public List<RecipeOptionButton> GetRecipeOptionWhenItemClick(int itemId)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();

            Query = String.Format("select *  FROM   rcs_recipe_option as ro INNER JOIN rcs_restaurant_recipe_option as rro ON ro.id = rro.option_id  where rro.recipe_id= {0} order by ro.title ;", itemId);

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];

            var Rows = (from row in DT.AsEnumerable()
                        orderby row["id"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();

            int count = DT.Rows.Count;
            int height = 0;
            height += count;
            //  if (count % 10 != 0) height += 3;

            // 

            List<RecipeOptionButton> aRecipeOptionButtonList = new List<RecipeOptionButton>();
            foreach (DataRow dataRow in DT.Rows)
            {

                RecipeOptionButton aAttributeButton = new RecipeOptionButton();
                aAttributeButton.RecipeOptionId = Convert.ToInt32(dataRow["id"]);
                aAttributeButton.Title = Convert.ToString(dataRow["title"]);
                aAttributeButton.Type = Convert.ToString(dataRow["type"]);
                aAttributeButton.ItemLImit = Convert.ToInt32(dataRow["limit"]);
                aAttributeButton.IsOnline = Convert.ToInt32(dataRow["is_online"]);
                aAttributeButton.IsOffline = Convert.ToInt32(dataRow["is_offline"]);
                aAttributeButton.Price = Convert.ToDouble(dataRow["price"]);
                aAttributeButton.InPrice = Convert.ToDouble(dataRow["in_price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aAttributeButton.InPrice = aAttributeButton.Price;
                }
                aAttributeButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                if (aAttributeButton.Type == "multiple" && aAttributeButton.ItemLImit <= 0)
                {
                    aAttributeButton.ItemLImit = 10000;
                }
                aAttributeButton.Text = dataRow["title"].ToString();
                aAttributeButton.PlusMinus = Convert.ToInt32("0" + dataRow["plusminus"]);
                aAttributeButton.Height = 45;
                aAttributeButton.Width = 120;
                aAttributeButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aRecipeOptionButtonList.Add(aAttributeButton);

                // attributeFlowLayoutPanel.Controls.Add(aAttributeButton);

            }

            return aRecipeOptionButtonList;
        }

        public List<RecipeOptionItemButton> GetRecipeOptionItemByOptionId(RecipeOptionButton recipeButton)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();
            DS = new DataSet();
            DT = new DataTable();

            List<RecipeOptionItemButton> aRecipeOptionItemButtons = new List<RecipeOptionItemButton>();
            Query = String.Format("select *  FROM rcs_option_item where recipe_option_id={0} ;", recipeButton.RecipeOptionId);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);


            DT = DS.Tables[0];
            foreach (DataRow dataRow in DT.Rows)
            {

                RecipeOptionItemButton aRecipeOptionItemButton = new RecipeOptionItemButton();
                aRecipeOptionItemButton.RecipeOptionItemId = Convert.ToInt32(dataRow["id"]);
                aRecipeOptionItemButton.ParentOptionId = Convert.ToInt32(dataRow["parent_option_id"]);
                aRecipeOptionItemButton.RecipeOptionId = Convert.ToInt32(dataRow["recipe_option_id"]);
                aRecipeOptionItemButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aRecipeOptionItemButton.Title = Convert.ToString(dataRow["title"]);
                aRecipeOptionItemButton.InPrice = Convert.ToDouble(dataRow["in_price"]);
                aRecipeOptionItemButton.Price = Convert.ToDouble(dataRow["price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipeOptionItemButton.InPrice = aRecipeOptionItemButton.Price;
                }

                if (aRecipeOptionItemButton.InPrice <= 0)
                {
                    aRecipeOptionItemButton.InPrice = recipeButton.InPrice;
                    aRecipeOptionItemButton.Price = recipeButton.Price;
                    if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                    {
                        aRecipeOptionItemButton.InPrice = recipeButton.Price;
                    }

                }
                aRecipeOptionItemButton.RecipeOptionButton = recipeButton;
                aRecipeOptionItemButton.Text = dataRow["title"].ToString();
                //   aRecipeOptionItemButton.Click += new EventHandler(RecipeOptionItemButton_Click);
                aRecipeOptionItemButton.Height = 45;
                aRecipeOptionItemButton.Width = 120;
                aRecipeOptionItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aRecipeOptionItemButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aRecipeOptionItemButton.FlatStyle = FlatStyle.Flat;
                aRecipeOptionItemButton.FlatAppearance.BorderSize = 0;
                aRecipeOptionItemButton.ForeColor = Color.White;
                aRecipeOptionItemButtons.Add(aRecipeOptionItemButton);

            }

            return aRecipeOptionItemButtons;
        }
        public List<RecipeOptionItemButton> GetRecipeOptionItemByOnlyId(RecipeOptionButton recipeButton)
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();
            DS = new DataSet();
            DT = new DataTable();

            List<RecipeOptionItemButton> aRecipeOptionItemButtons = new List<RecipeOptionItemButton>();

            Query = String.Format("select *  FROM rcs_option_item where id={0} ;", recipeButton.RecipeOptionId);

            Adapter = GetAdapter(Adapter);

            DS.Reset();
            Adapter.Fill(DS);


            DT = DS.Tables[0];
            foreach (DataRow dataRow in DT.Rows)
            {

                RecipeOptionItemButton aRecipeOptionItemButton = new RecipeOptionItemButton();
                aRecipeOptionItemButton.RecipeOptionItemId = Convert.ToInt32(dataRow["id"]);
                aRecipeOptionItemButton.ParentOptionId = Convert.ToInt32(dataRow["parent_option_id"]);
                aRecipeOptionItemButton.RecipeOptionId = Convert.ToInt32(dataRow["recipe_option_id"]);
                aRecipeOptionItemButton.RestaurantId = Convert.ToInt32(dataRow["restaurant_id"]);
                aRecipeOptionItemButton.Title = Convert.ToString(dataRow["title"]);
                aRecipeOptionItemButton.InPrice = Convert.ToDouble(dataRow["in_price"]);
                aRecipeOptionItemButton.Price = Convert.ToDouble(dataRow["price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aRecipeOptionItemButton.InPrice = aRecipeOptionItemButton.Price;
                }

                if (aRecipeOptionItemButton.InPrice <= 0)
                {
                    aRecipeOptionItemButton.InPrice = recipeButton.InPrice;
                    aRecipeOptionItemButton.Price = recipeButton.Price;
                    if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                    {
                        aRecipeOptionItemButton.InPrice = recipeButton.Price;
                    }

                }
                aRecipeOptionItemButton.RecipeOptionButton = recipeButton;
                aRecipeOptionItemButton.Text = dataRow["title"].ToString();
                //   aRecipeOptionItemButton.Click += new EventHandler(RecipeOptionItemButton_Click);
                aRecipeOptionItemButton.Height = 45;
                aRecipeOptionItemButton.Width = 120;
                aRecipeOptionItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                aRecipeOptionItemButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aRecipeOptionItemButton.FlatStyle = FlatStyle.Flat;
                aRecipeOptionItemButton.FlatAppearance.BorderSize = 0;
                aRecipeOptionItemButton.ForeColor = Color.White;
                aRecipeOptionItemButtons.Add(aRecipeOptionItemButton);

            }

            return aRecipeOptionItemButtons;
        }
        public ReceipeMenuItemButton GetRecipeByCategoryAndSubcategory(int categoryId, int subCategoryId)
        {
            GlobalUrl urls = new GlobalUrl();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            ReceipeMenuItemButton aReceipeMenuItemButton = new ReceipeMenuItemButton();

            Query = String.Format("SELECT * FROM rcs_recipe where category_id= {0} AND subcategory_id={1};", categoryId, subCategoryId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {
                aReceipeMenuItemButton.RecipeMenuItemId = Convert.ToInt32(Reader["id"]);
                aReceipeMenuItemButton.ParentId = Convert.ToInt32(Reader["parent_id"]);
                aReceipeMenuItemButton.RestaurantId = Convert.ToInt32(Reader["restaurant_id"]);
                aReceipeMenuItemButton.CategoryId = Convert.ToInt32(Reader["category_id"]);
                aReceipeMenuItemButton.SubCategoryId = Convert.ToInt32(Reader["subcategory_id"]);
                aReceipeMenuItemButton.ItemName = (Reader["name"].ToString());
                aReceipeMenuItemButton.ReceiptName = (Reader["receipt_name"].ToString());
                aReceipeMenuItemButton.ShortDescrip = (Reader["shortdesc"]).ToString();
                aReceipeMenuItemButton.LongDescrip = (Reader["longdesc"].ToString());
                aReceipeMenuItemButton.InPrice = Convert.ToDouble(Reader["in_price"]);
                aReceipeMenuItemButton.OutPrice = Convert.ToDouble(Reader["out_price"]);
                if (GlobalSetting.RestaurantInformation.RestaurantType.ToLower() == "takeaway".ToLower())
                {
                    aReceipeMenuItemButton.InPrice = aReceipeMenuItemButton.OutPrice;
                }
                aReceipeMenuItemButton.DiscountPrice = Convert.ToDouble(Reader["discount_price"]);
                aReceipeMenuItemButton.SortOrder = Convert.ToInt32(Reader["sort_order"]);
                aReceipeMenuItemButton.KitchenSection = Convert.ToInt32(Reader["kitchen_section"]);
                aReceipeMenuItemButton.IsExclusixe = Convert.ToInt32(Reader["is_exclusive"]);
                aReceipeMenuItemButton.Hot = Convert.ToInt32(Reader["hot"]);
                aReceipeMenuItemButton.Nut = Convert.ToInt32(Reader["nut"]);
                aReceipeMenuItemButton.ShowCategory = Convert.ToInt32(Reader["show_category"]);
                aReceipeMenuItemButton.ButtonWidth = Convert.ToInt32(Reader["width"]);
                aReceipeMenuItemButton.ButtonHeight = Convert.ToInt32(Reader["height"]);
                // aReceipeMenuItemButton.RecipeTypeId = aReceipeSubCategoryButton.RecipeTypeId;

                aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

            }

            return aReceipeMenuItemButton;
        }

        public bool IsShowCategory(ReceipeCategoryButton aReceipeCategoryButton, List<ReceipeMenuItemButton> allRecipeButton)
        {
            var aFlowLayoutPanel = new FlowLayoutPanel();

            // 
            //    SQLiteDataAdapter DB;

            DataSet DS = new DataSet();
            DataTable DT = new DataTable();


            Query = String.Format("SELECT * FROM rcs_recipe where show_category= {0}  AND subcategory_id = 0;", aReceipeCategoryButton.CategoryId);

            Adapter = GetAdapter(Adapter);
            DS.Reset();
            Adapter.Fill(DS);
            DT = DS.Tables[0];




            var Rows = (from row in DT.AsEnumerable()
                        orderby row["sort_order"] ascending
                        select row);
            DT = Rows.AsDataView().ToTable();

            if (DT.Rows.Count > 0) return true;

            if (allRecipeButton.Any(a => a.ShowCategory == aReceipeCategoryButton.CategoryId)) return true;

            return false;


        }

        internal double GetPrice(int categoryId, int subcategoryId, int packageId)
        {

            double price = 0.0;
            int RecipeId = 0;

            Query = String.Format("SELECT add_price FROM rcs_package_category where category_id like  '%{0}%' and  subcategory_id like  '%{1}%'  AND  package_id= {2};", categoryId, subcategoryId, packageId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    price = Convert.ToInt32(Reader["add_price"]);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }


            if (price == 0)
            {

                Query = String.Format("SELECT id FROM rcs_recipe where category_id= {0} and  subcategory_id= {1};", categoryId, subcategoryId);

                command = CommandMethod(command);
                Reader = ReaderMethod(Reader, command);

                // dataRow = command.ExecuteReader();
                while (Reader.Read())
                {

                    try
                    {
                        RecipeId = Convert.ToInt32(Reader["id"]);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }


                if (RecipeId > 0)
                {

                    Query = String.Format("SELECT add_price FROM rcs_package_recipe where recipe_id= {0} AND  package_id= {1};", RecipeId, packageId);

                    command = CommandMethod(command);
                    Reader = ReaderMethod(Reader, command);

                    while (Reader.Read())
                    {

                        try
                        {
                            price = Convert.ToDouble(Reader["add_price"]);


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }


                    }

                }


            }

            return price;



        }

        internal string GetCategoryNameById(int catId)
        {
            string starterId = "";
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT name FROM rcs_recipe_category where id= {0};", catId);


            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);

            // dataRow = command.ExecuteReader();
            while (Reader.Read())
            {

                try
                {
                    starterId = Convert.ToString(Reader["name"]);


                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }


            }


            return starterId;
        }

        public int PriceMenuUpdate(ReceipeMenuItemButton ReceipeMenuItemButton)
        {
            Query = "update rcs_recipe set receipt_name='"+ReceipeMenuItemButton.ReceiptName+"',in_price='" + ReceipeMenuItemButton.InPrice + "',out_price='" + ReceipeMenuItemButton.OutPrice + "'where id='" + ReceipeMenuItemButton.RecipeMenuItemId + "'";
            command = CommandMethod(command);
            int id = command.ExecuteNonQuery();
            return id;
        }
    }
}
