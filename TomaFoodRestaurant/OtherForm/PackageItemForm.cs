using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using System.Data.SQLite;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class PackageItemForm : Form
    {
        RecipePackageButton aRecipePackageButton = new RecipePackageButton();
        int packageId;
        int hasSubcategoryId = 0;
        public static int packageQty = 1;
        PackageCategoryButton tempPackage = new PackageCategoryButton();
        public static string status = "";
        public static List<PackageItem> aPackageItemList = new List<PackageItem>();
        public static List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static List<FreeOptionMD> AOptionMds = new List<FreeOptionMD>();
        public DataTable GetPackageCategory = new DataTable();

        public PackageItemForm(RecipePackageButton package)
        {
            InitializeComponent();
            aRecipePackageButton = package;
            ShowInTaskbar = false;

            PackageBLL packageBll = new PackageBLL();

      //  GetPackageCategory = packageBll.GetPackageCategory();
          GetPackageCategory = packageBll.GetPackageCategory1();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            status = "cancel";
            this.Close();
        }

        private void PackageItemForm_Load(object sender, EventArgs e)
        {
            packageNamelabel1.Text = (aRecipePackageButton.OnlineName).ToUpper();
            LoadPackageCategory();
            if (tempPackage != null && tempPackage.PackageId > 0)
                LoadDetails(tempPackage);
        }

        private void LoadDetails(PackageCategoryButton category)
        {
            int hasSubcategoryId = 0;
            LoadAllItem(category);

            //if (category.SubCategory.Trim().Length == 0)
            //{
            //    LoadPackageItemWithoytSubCategory(category);

            //    //since we are adding all the items on the attributtePanel when no subcategory
            //    //we can clear the rightRecipe controls
            //    rightRecepiflowLayoutPanel.Controls.Clear();
            //    rightRecepiflowLayoutPanel.Width = 0;
            //    rightRecepiflowLayoutPanel.Height = 0;
            //}
            //else if (category.SubCategory.Trim().Length > 0)
            //{
            //    LoadAllSubCategory(category);
            //}

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            category.BackColor = Color.Black;
            category.ForeColor = Color.White;
            foreach (PackageCategoryButton tempButton in packageCategoryFlowLayoutPanel.Controls.OfType<PackageCategoryButton>())
            {
                if (tempButton != category)
                {
                    tempButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));
                    tempButton.ForeColor = Color.White;
                }
            }
        }

        //************** Tuhin Edit Package *******************
        public bool CheckAutoPackageItemAdd(List<PackageItem> packageItems, PackageCategoryButton aPackageCategoryButton)
        {
            var IsExistItemAdd = packageItems.FirstOrDefault(a => a.CategoryId == Convert.ToInt32(aPackageCategoryButton.CategoryId));
            if (IsExistItemAdd!=null)
            {
                return true;
            }
            return false;
        }
        
        //************** Tuhin Edit Package *******************

        private void LoadPackageCategory()
        {
            PackageBLL aPackageBll = new PackageBLL();
            List<PackageCategoryButton> tempList = aPackageBll.GetPackageCategory(aRecipePackageButton);
            bool fla = false; 
            foreach (PackageCategoryButton aPackageCategoryButton in tempList)
            {
                aPackageCategoryButton.Click += new EventHandler(PackageCategoryButton_Click);
                aPackageCategoryButton.Text = aPackageCategoryButton.OptionName.ToString();
                aPackageCategoryButton.RecipePackage = aRecipePackageButton; 
                if (CheckAutoAdd(aPackageCategoryButton))
                {
                    continue;
                }

                if (!fla)
                {
                    tempPackage = aPackageCategoryButton;
                    fla = true;
                }
                packageCategoryFlowLayoutPanel.Controls.Add(aPackageCategoryButton);
            }
        }
        
        private bool CheckAutoAdd(PackageCategoryButton aPackageCategoryButton)
        {
            bool result = false;
            PackageCategoryButton category = aPackageCategoryButton;
            List<PackageItemButton> allPackageItems = LoadDetailsForLoad(category);
            if (aPackageCategoryButton.ShowOption == 0)
            {
                if ((aPackageCategoryButton.Items == allPackageItems.Count || aPackageCategoryButton.Items == 0))
                {
                    foreach (var itemButton in allPackageItems)
                    {
                        result = true;
                        PackageItem aItem = new PackageItem();
                        aItem.ItemId = itemButton.RecipeId;
                        aItem.ItemName = itemButton.ReciptName;
                        aItem.Price = itemButton.AddPrice;
                        aItem.Qty = 1;
                        aItem.OptionName = itemButton.OptionName;
                        aItem.PackageId = itemButton.PackageId;
                        aItem.CategoryId = itemButton.CategoryId;
                        aItem.SubcategoryId = itemButton.SubCategoryId;
                        aItem.OptionsIndex = GetOptionIndex() + 1;
                        aItem.PackageItemOptionsIndex = aItem.OptionsIndex;
                        ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                        if (aPackageCategoryButton.ShowOption > 0)
                        {

                            ItemOptionForm aItemOptionForm = new ItemOptionForm(itemButton.RecipeId);
                            aItemOptionForm.ShowDialog();
                        }

                        if (CheckCategoryLimitLimit(itemButton.PackageCategoryButton, itemButton))
                        {
                            aPackageItemList.Add(aItem);
                            foreach (RecipeOptionItemButton recipe in ItemOptionForm.aRecipeList)
                            {
                                RecipeOptionMD aOptionMD = new RecipeOptionMD();
                                aOptionMD.RecipeId = aItem.ItemId;
                                aOptionMD.TableNumber = 1;
                                aOptionMD.RecipeOptionId = recipe.RecipeOptionId;

                                if (!string.IsNullOrEmpty(recipe.MinusTitle))
                                {
                                    aOptionMD.MinusOption = recipe.MinusTitle;
                                }
                                else if (!string.IsNullOrEmpty(recipe.Title))
                                {
                                    aOptionMD.Title = recipe.Title;
                                }

                                aOptionMD.Type = recipe.RecipeOptionButton.Type;
                                aOptionMD.Price = recipe.Price;
                                aOptionMD.InPrice = recipe.InPrice;
                                aOptionMD.Qty = 1;
                                aOptionMD.OptionsIndex = aItem.OptionsIndex;
                                aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                                aRecipeOptionMdList.Add(aOptionMD);
                            }
                        }
                    }
                    LoadPackageItemDetails();

                }
                else if ((aPackageCategoryButton.Items > allPackageItems.Count && allPackageItems.Count == 1))
                {
                    foreach (var itemButton in allPackageItems)
                    {
                        result = true;
                        PackageItem aItem = new PackageItem();
                        aItem.ItemId = itemButton.RecipeId;
                        aItem.ItemName = itemButton.ReciptName;
                        aItem.Price = itemButton.AddPrice;
                        aItem.Qty = aPackageCategoryButton.Items;
                        aItem.OptionName = itemButton.OptionName;
                        aItem.PackageId = itemButton.PackageId;
                        aItem.CategoryId = itemButton.CategoryId;
                        aItem.SubcategoryId = itemButton.SubCategoryId;
                        aItem.OptionsIndex = GetOptionIndex() + 1;
                        aItem.PackageItemOptionsIndex = aItem.OptionsIndex;
                        ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();

                        if (aPackageCategoryButton.ShowOption > 0)
                        {
                            ItemOptionForm aItemOptionForm = new ItemOptionForm(itemButton.RecipeId);
                            aItemOptionForm.ShowDialog();
                        }

                        if (CheckCategoryLimitLimit(itemButton.PackageCategoryButton, itemButton))
                        {
                            aPackageItemList.Add(aItem);
                            foreach (RecipeOptionItemButton recipe in ItemOptionForm.aRecipeList)
                            {
                                RecipeOptionMD aOptionMD = new RecipeOptionMD();
                                aOptionMD.RecipeId = aItem.ItemId;
                                aOptionMD.TableNumber = 1;
                                aOptionMD.RecipeOptionId = recipe.RecipeOptionId;

                                if (!string.IsNullOrEmpty(recipe.MinusTitle))
                                {
                                    aOptionMD.MinusOption = recipe.MinusTitle;
                                }
                                else if (!string.IsNullOrEmpty(recipe.Title))
                                {
                                    aOptionMD.Title = recipe.Title;
                                }

                                aOptionMD.Type = recipe.RecipeOptionButton.Type;
                                aOptionMD.Price = recipe.Price;
                                aOptionMD.InPrice = recipe.InPrice;
                                aOptionMD.Qty = aPackageCategoryButton.Items;
                                aOptionMD.OptionsIndex = aItem.OptionsIndex;
                                aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                                aRecipeOptionMdList.Add(aOptionMD);
                            }
                        }
                    }
                    LoadPackageItemDetails();
                }
            }

            return result;

        }

        private void LoadAllItem(PackageCategoryButton category)
        {
            receipeCategoryFlowLayoutPanel1.Visible = true;
            PackageBLL aPackageBll = new PackageBLL();
            DataSet DSs = new DataSet();
            DataTable DTs = new DataTable();

            EnumerableRowCollection<DataRow> Rows = GetPackageCategory.AsEnumerable().Where(a => Convert.ToString(a["option_name"]) == category.OptionName && Convert.ToInt32(a["package_id"]) == category.PackageId);

            DTs = Rows.AsDataView().ToTable();
         
            PackageItemButton packageItemButton = new PackageItemButton();

            List<PackageItemButton> aPackageItemButtonList = new List<PackageItemButton>();
            attributeFlowLayoutPanel.Controls.Clear();
            rightRecepiflowLayoutPanel.Controls.Clear();

            attributeFlowLayoutPanel.Size = new Size(0, 0);
            rightRecepiflowLayoutPanel.Size = new Size(0, 0);

            int rowCount = 0;
            foreach (DataRow mainData in DTs.Rows)
            {
                if (Convert.ToString(mainData["subcategory_id"]).Length == 0 || Convert.ToString(mainData["category_id"]).Length == 0)
                {
                    DataSet DS = new DataSet(); DataTable DT = new DataTable();
                    string query = "";

                    EnumerableRowCollection<DataRow> dataView;

                    if (Convert.ToInt32(mainData["all_recipe"]) > 0)
                    {
                        packageItemButton.CategoryId = Convert.ToInt32(mainData["category_id"]);
                        packageItemButton.RecipeId = Convert.ToInt32(mainData["all_recipe"]);
                    }
                    else
                    {
                        packageItemButton.PackageId = category.PackageId;
                        packageItemButton.OptionName = category.OptionName;
                        packageItemButton.CategoryId = Convert.ToInt32(mainData["category_id"]);
                    }

                    DT = aPackageBll.GetPackagerReceipeByPackageItem(packageItemButton);

                    if (Convert.ToInt32(mainData["all_recipe"]) > 0)
                    {
                        foreach (DataRow dataRow1 in DT.Rows)
                        {
                            PackageItemButton aPackageItemButton = new PackageItemButton();
                            aPackageItemButton.OptionName = Convert.ToString(mainData["option_name"]);
                            aPackageItemButton.ItemName = Convert.ToString(dataRow1["name"]);
                            aPackageItemButton.PackageId = Convert.ToInt32(mainData["package_id"]);
                            aPackageItemButton.RecipeId = Convert.ToInt32(dataRow1["id"]);
                            aPackageItemButton.AddPrice = Convert.ToDouble(dataRow1["out_price"]);
                            aPackageItemButton.ReciptName = Convert.ToString(dataRow1["receipt_name"]);
                            aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow1["subcategory_id"]);
                            aPackageItemButton.CategoryId = Convert.ToInt32(dataRow1["category_id"]);
                            aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                            aPackageItemButton.Text = dataRow1["name"].ToString();
                            aPackageItemButton.MinimumSize = new Size(120, 50);
                            aPackageItemButton.MaximumSize = new Size(120, 50);
                            aPackageItemButton.FlatAppearance.BorderSize = 0;
                            aPackageItemButton.FlatStyle = FlatStyle.Flat;
                            aPackageItemButton.AutoSize = true;
                            aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                            aPackageItemButton.ForeColor = Color.White;
                            aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                            aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                            aPackageItemButton.PackageCategoryButton = category;
                            aPackageItemButton.RecipePackageButton = category.RecipePackage;
                            rightRecepiflowLayoutPanel.Controls.Add(aPackageItemButton);
                            rowCount++;
                        }
                    }
                    else
                    {
                        foreach (DataRow dataRow1 in DT.Rows)
                        {
                            PackageItemButton aPackageItemButton = new PackageItemButton();
                            aPackageItemButton.OptionName = Convert.ToString(dataRow1["option_name"]);
                            aPackageItemButton.ItemName = Convert.ToString(dataRow1["name"]);
                            aPackageItemButton.PackageId = Convert.ToInt32(dataRow1["package_id"]);
                            aPackageItemButton.RecipeId = Convert.ToInt32(dataRow1["recipe_id"]);
                            aPackageItemButton.AddPrice = Convert.ToDouble(dataRow1["add_price"]);
                            aPackageItemButton.ReciptName = Convert.ToString(dataRow1["receipt_name"]);
                            aPackageItemButton.SubCategoryId = Convert.ToInt32(dataRow1["subcategory_id"]);
                            aPackageItemButton.CategoryId = Convert.ToInt32(dataRow1["category_id"]);
                            aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                            aPackageItemButton.Text = dataRow1["name"].ToString();
                            aPackageItemButton.MinimumSize = new Size(120, 50);
                            aPackageItemButton.MaximumSize = new Size(120, 50);
                            aPackageItemButton.FlatAppearance.BorderSize = 0;
                            aPackageItemButton.FlatStyle = FlatStyle.Flat;
                            aPackageItemButton.AutoSize = true;
                            aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                            aPackageItemButton.ForeColor = Color.White;
                            aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                            aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                            aPackageItemButton.PackageCategoryButton = category;
                            aPackageItemButton.RecipePackageButton = category.RecipePackage;
                            rightRecepiflowLayoutPanel.Controls.Add(aPackageItemButton);
                            rowCount++;
                        }
                    }
                }
                else
                {
                    DataTable DT = new DataTable();

                    if (Convert.ToInt32(mainData["all_recipe"]) > 0)
                    {
                        category.all_recipe = Convert.ToInt32(mainData["all_recipe"]);
                        category.SubCategoryId = Convert.ToString(mainData["subcategory_id"]);
                    }
                    else
                    {
                      category.SubCategory = category.SubCategory;

                       // category.SubCategoryId = Convert.ToString(mainData["subcategory_id"]);
                       //category.CategoryId = Convert.ToString(mainData["category_id"]);
                       //category.CategoryId = Convert.ToString(mainData["category_id"]);
                    }

                    DT = aPackageBll.GetPackagerReceipeByPackageItemWithSubCat(category);

                    var rows = (from row in DT.AsEnumerable()
                                orderby row["name"] ascending
                                orderby row["sort_order"] ascending
                                select row);
                    DT = rows.AsDataView().ToTable();

                    if (Convert.ToInt32(mainData["all_recipe"]) > 0)
                    {
                        foreach (DataRow datarow in DT.Rows)
                        {
                            PackageItemButton aPackageItemButton = new PackageItemButton();
                            aPackageItemButton.OptionName = Convert.ToString(mainData["option_name"]);
                            aPackageItemButton.ItemName = Convert.ToString(datarow["name"]);
                            aPackageItemButton.PackageId = Convert.ToInt32(mainData["package_id"]);
                            aPackageItemButton.RecipeId = Convert.ToInt32(datarow["recipe_id"]);
                            aPackageItemButton.SubCategoryId = Convert.ToInt32(datarow["subcategory_id"]);
                            aPackageItemButton.CategoryId = Convert.ToInt32(datarow["category_id"]);
                            aPackageItemButton.AddPrice = category.AddPrice;
                            aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                            aPackageItemButton.Text = datarow["name"].ToString();
                            aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                            aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                            aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                            aPackageItemButton.ForeColor = Color.White;
                            aPackageItemButton.Width = 100;
                            aPackageItemButton.MaximumSize = new Size(120, 50);
                            aPackageItemButton.MinimumSize = new Size(120, 50);
                            aPackageItemButton.FlatAppearance.BorderSize = 0;
                            aPackageItemButton.FlatStyle = FlatStyle.Flat;
                            aPackageItemButton.AutoSize = true;
                            aPackageItemButtonList.Add(aPackageItemButton);
                        }
                    }
                    else
                    {
                        foreach (DataRow datarow in DT.Rows)
                        {
                            PackageItemButton aPackageItemButton = new PackageItemButton();
                            aPackageItemButton.OptionName = category.OptionName;
                            aPackageItemButton.ItemName = Convert.ToString(datarow["name"]);
                            aPackageItemButton.PackageId = category.PackageId;
                            aPackageItemButton.RecipeId = Convert.ToInt32(datarow["recipe_id"]);
                            aPackageItemButton.SubCategoryId = Convert.ToInt32(datarow["subcategory_id"]);
                            aPackageItemButton.CategoryId = Convert.ToInt32(datarow["category_id"]);
                            aPackageItemButton.AddPrice = category.AddPrice;
                            aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                            aPackageItemButton.Text = datarow["name"].ToString();
                            aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                            aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc");
                            aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                            aPackageItemButton.ForeColor = Color.White;

                            aPackageItemButton.MinimumSize = new Size(120, 50);
                            aPackageItemButton.MaximumSize = new Size(120, 50);
                            aPackageItemButton.FlatAppearance.BorderSize = 0;
                            aPackageItemButton.FlatStyle = FlatStyle.Flat;
                            aPackageItemButton.AutoSize = true;
                            aPackageItemButtonList.Add(aPackageItemButton);
                        }
                    }
                }
            }

            aPackageItemButtonList = aPackageItemButtonList.Distinct().ToList();

            List<int> catogries = aPackageBll.GetCategory(category.CategoryId);

            catogries = aPackageBll.MargeCategoty(catogries, aPackageItemButtonList);

            List<int> subCategories = aPackageBll.GetCategory(category.SubCategory);

            subCategories = aPackageBll.MergeSubcategory(subCategories, aPackageItemButtonList);

            List<ReceipeCategoryButton> categoryList = aPackageBll.GetCategoryList(catogries);

            receipeCategoryFlowLayoutPanel1.Controls.Clear();
            foreach (ReceipeCategoryButton button in categoryList)
            {
                if (button.HasSubcategory > 0)
                {
                    button.MouseClick += btn_Read_MouseClick;
                    receipeCategoryFlowLayoutPanel1.Controls.Add(button);
                }
            }
            receipeCategoryFlowLayoutPanel1.Location = new Point(21, 70);
            List<PackageItemButton> subCategoryList = aPackageBll.GerSubCategoryList(subCategories, category);

            int count = subCategoryList.Count;
            if (count > 0)
            {
                int height = (count / 10);
                if (count % 10 != 0)

                    height += 1;

              // subCategoryList = subCategoryList.OrderBy(a => a.ReciptName).ToList();
              // subCategoryList = subCategoryList.OrderBy(a => a.).ThenBy(a=>a.ItemName).ToList();
              // subCategoryList = subCategoryList.OrderBy(a => a.ReciptName).ToList();
               
                foreach (PackageItemButton button in subCategoryList)
                {
                    button.Click += new EventHandler(PackageItemButton_Click);
                    button.MaximumSize = new Size(120,50);
                    button.MinimumSize = new Size(90, 50);
                    button.Size = new Size(120, 50);
                    button.UseCompatibleTextRendering = true;
                    button.RecipePackageButton = category.RecipePackage;
                    button.PackageCategoryButton = category;
                    attributeFlowLayoutPanel.Controls.Add(button);

                }
                attributeFlowLayoutPanel.Location = new Point(receipeCategoryFlowLayoutPanel1.Location.X + receipeCategoryFlowLayoutPanel1.Width, attributeFlowLayoutPanel.Location.Y);
                if (rowCount > 0)
                {
                    attributeFlowLayoutPanel.Width = (buttonPanel.Location.X - receipeCategoryFlowLayoutPanel1.Width);
                    rightRecepiflowLayoutPanel.Location = new Point(attributeFlowLayoutPanel.Location.X, attributeFlowLayoutPanel.Location.Y + attributeFlowLayoutPanel.Height + 50);
                    rightRecepiflowLayoutPanel.Height = (rowCount / 3) * 60;
                    rightRecepiflowLayoutPanel.Width = attributeFlowLayoutPanel.Width;
                }
                else
                {                    
                    attributeFlowLayoutPanel.Width = (buttonPanel.Location.X - receipeCategoryFlowLayoutPanel1.Width - 50);
                    //rightRecepiflowLayoutPanel.Width = 0;
                }
            }
            else
            {                
                rightRecepiflowLayoutPanel.Location = new Point(receipeCategoryFlowLayoutPanel1.Location.X + receipeCategoryFlowLayoutPanel1.Width, 100);
                rightRecepiflowLayoutPanel.Width = (buttonPanel.Location.X - receipeCategoryFlowLayoutPanel1.Width - 50);
            }

            //rightRecepiflowLayoutPanel.BackColor = Color.BlueViolet;
            //attributeFlowLayoutPanel.BackColor = Color.Brown;
        }

        private List<PackageItemButton> LoadDetailsForLoad(PackageCategoryButton category)
        {
            List<PackageItemButton> allPackageItems = new List<PackageItemButton>();
            int hasSubcategoryId = 0;
            if (category.SubCategory.Trim().Length == 0)
            {
                allPackageItems = LoadPackageItemWithoytSubCategoryWhenFormLoad(category);
            }
            else if (category.SubCategory.Trim().Length > 0)
            {
                allPackageItems = LoadAllSubCategoryWhenFormLoad(category);
            }

            return allPackageItems;
        }

        private List<PackageItemButton> LoadAllSubCategoryWhenFormLoad(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();
            List<PackageItemButton> subCategoryList = aPackageBll.GetAllSubCategoryWhenFormLoad(category);
            List<PackageItemButton> aPackageItemButtons = new List<PackageItemButton>();
            foreach (PackageItemButton button in subCategoryList)
            {
                button.Click += new EventHandler(PackageItemButton_Click);
                button.RecipePackageButton = category.RecipePackage;
                button.PackageCategoryButton = category;
                aPackageItemButtons.Add(button);
            }
            return aPackageItemButtons;

        }

        private List<PackageItemButton> LoadPackageItemWithoytSubCategoryWhenFormLoad(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();
            return aPackageBll.GetPackageItemWithoytSubCategoryWhenFormLoad(category);
        }

        private void PackageCategoryButton_Click(object sen13, EventArgs e)
        {
            PackageCategoryButton category = sen13 as PackageCategoryButton;
            hasSubcategoryId = 0;
            LoadDetails(category);
        }

        private void LoadAllSubCategory(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();

            receipeCategoryFlowLayoutPanel1.Visible = true;

            List<PackageItemButton> aPackageItemButtonList = aPackageBll.GetAllSubCategory(category);
            //  foreach (PackageItemButton aPackageItemButton in aPackageItemButtonList)
            //  {
            ////      aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);

            //  }

            List<int> catogries = aPackageBll.GetCategory(category.CategoryId);

            catogries = aPackageBll.MargeCategoty(catogries, aPackageItemButtonList);

            List<int> subCategories = aPackageBll.GetCategory(category.SubCategory);

            subCategories = aPackageBll.MergeSubcategory(subCategories, aPackageItemButtonList);

            List<ReceipeCategoryButton> categoryList = aPackageBll.GetCategoryList(catogries);
            receipeCategoryFlowLayoutPanel1.Controls.Clear();
            foreach (ReceipeCategoryButton button in categoryList)
            {
                button.MouseClick += btn_Read_MouseClick;
                receipeCategoryFlowLayoutPanel1.Controls.Add(button);
            }
            receipeCategoryFlowLayoutPanel1.Location = new Point(21, 75);

            List<PackageItemButton> subCategoryList = aPackageBll.GerSubCategoryList(subCategories, category);
            attributeFlowLayoutPanel.Controls.Clear();

            int count = subCategoryList.Count;
            int height = (count / 10);
            if (count % 10 != 0) height += 1;
            attributeFlowLayoutPanel.Height = (height * 70);

            foreach (PackageItemButton button in subCategoryList)
            {
                button.Click += new EventHandler(PackageItemButton_Click);
                button.RecipePackageButton = category.RecipePackage;
                button.PackageCategoryButton = category;
                attributeFlowLayoutPanel.Controls.Add(button);
            }
            attributeFlowLayoutPanel.Location = new Point(receipeCategoryFlowLayoutPanel1.Location.X + receipeCategoryFlowLayoutPanel1.Width, attributeFlowLayoutPanel.Location.Y);

            attributeFlowLayoutPanel.Width = (buttonPanel.Location.X - receipeCategoryFlowLayoutPanel1.Width - 10);
        }

        private void btn_Read_MouseClick(object sender, EventArgs e)
        {
            ReceipeCategoryButton aButton = sender as ReceipeCategoryButton;
            aButton.BackColor = Color.Black;
            hasSubcategoryId = aButton.CategoryId;
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            foreach (ReceipeCategoryButton tempButton in receipeCategoryFlowLayoutPanel1.Controls)
            {
                if (tempButton != aButton)
                {
                    tempButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(tempButton.Color));
                }
            }
        }

        private void LoadPackageItemWithoytSubCategory(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();

            List<PackageItemButton> aPackageItemButtons = aPackageBll.GetPackageItemWithoytSubCategory(category);

            attributeFlowLayoutPanel.Controls.Clear();
            int count = aPackageItemButtons.Count;
            int height = (count / 10);
            if (count % 10 != 0) height += 1;
            attributeFlowLayoutPanel.Height = (height * 50);
            receipeCategoryFlowLayoutPanel1.Controls.Clear();
            receipeCategoryFlowLayoutPanel1.Visible = false;
            foreach (PackageItemButton aPackageItemButton in aPackageItemButtons)
            {
                aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
                attributeFlowLayoutPanel.Controls.Add(aPackageItemButton);
            }
            attributeFlowLayoutPanel.Location = new Point(21, 75);
            attributeFlowLayoutPanel.Width = (buttonPanel.Location.X - receipeCategoryFlowLayoutPanel1.Width - 10);
        }

        private void PackageItemButton_Click(object sen15, EventArgs e)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            PackageItemButton itemButton = sen15 as PackageItemButton;
            // ChangePackageColor(itemButton);

            if (itemButton.SubCategoryId > 0 && hasSubcategoryId <= 0) return;
            PackageItem aItem = new PackageItem();

            bool notFound = false;
            if (itemButton.SubCategoryId > 0 && hasSubcategoryId > 0)
            {
                string pName = aRestaurantMenuBll.GetSubcategoryByCatAndSubcat(hasSubcategoryId, itemButton.SubCategoryId);
                if (pName != "")
                {
                    itemButton.RecipeId = aRestaurantMenuBll.GetRecipeIdByCatAndSubcat(hasSubcategoryId, itemButton.SubCategoryId);
                    aItem.CategoryId = hasSubcategoryId;
                    aItem.ItemName = pName;
                    aItem.ItemId = itemButton.RecipeId;
                    aItem.Price = aRestaurantMenuBll.GetPrice(hasSubcategoryId, itemButton.SubCategoryId,itemButton.PackageCategoryButton.PackageId, itemButton.RecipeId);
                    aItem.Qty = 1;
                    aItem.OptionName = itemButton.PackageCategoryButton.OptionName;
                    aItem.PackageId = itemButton.PackageCategoryButton.PackageId;
                    //aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                }
                else
                {
                    MessageBox.Show("Item not found.");
                    notFound = true;
                }
            }
            else
            {
                aItem.ItemId = itemButton.RecipeId;
                aItem.ItemName = itemButton.ReciptName;
                aItem.Price = itemButton.AddPrice;
                aItem.Qty = 1;
                aItem.OptionName = itemButton.OptionName;
                aItem.PackageId = itemButton.PackageId;
                aItem.CategoryId = itemButton.CategoryId;
                aItem.SubcategoryId = itemButton.SubCategoryId;
            }
            if (!notFound)
            {
                PackageCategoryButton aPackageCategoryButton = aRestaurantMenuBll.GetPackagePackageCategory(itemButton);  
                ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                if (itemButton.PackageCategoryButton.ShowOption > 0 && aPackageCategoryButton.ShowOption > 0 && aRestaurantMenuBll.GetReceipeOptionsByItemId(itemButton.RecipeId))
                {

                    ItemOptionForm aItemOptionForm = new ItemOptionForm(itemButton.RecipeId);
                    aItemOptionForm.ShowDialog();
                }

                if (CheckCategoryLimitLimit(itemButton.PackageCategoryButton, itemButton))
                {
                    int res = CheckDulicate(aItem, ItemOptionForm.aRecipeList);

                    if (res > 0)
                    {
                        aPackageItemList.Where(a => a.OptionsIndex == res).ToList().ForEach(a => a.Qty += 1);
                     //   aPackageItemList.Where(a => a.OptionsIndex == res).ToList().ForEach(a => a.Price += itemButton.AddPrice);
                    }
                    else
                    {
                        aItem.OptionsIndex = GetOptionIndex() + 1;
                        aItem.PackageItemOptionsIndex = aItem.OptionsIndex;
                        aPackageItemList.Add(aItem);

                        //if (itemButton.FreeOptionMds != null)
                        //{
                        //    ItemOptionForm.aRecipeList = GetFreeOptionPrice(itemButton.FreeOptionMds,ItemOptionForm.aRecipeList);
                        //}


                        foreach (RecipeOptionItemButton recipe in ItemOptionForm.aRecipeList)
                        {
                            RecipeOptionMD aOptionMD = new RecipeOptionMD();
                            aOptionMD.RecipeId = aItem.ItemId;
                            aOptionMD.TableNumber = 1;
                            aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                            if (!string.IsNullOrEmpty(recipe.MinusTitle))
                            {
                                aOptionMD.MinusOption = recipe.MinusTitle;
                            }
                            else if (!string.IsNullOrEmpty(recipe.Title))
                            {
                                aOptionMD.Title = recipe.Title;
                            }
                            aOptionMD.Type = recipe.RecipeOptionButton.Type;
                            aOptionMD.Price = recipe.Price;
                            aOptionMD.InPrice = recipe.InPrice;
                            aOptionMD.Qty = 1;
                            aOptionMD.OptionsIndex = aItem.OptionsIndex;
                            aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                            aOptionMD.PackageItemOptionsIndex = aItem.OptionsIndex;
                            aRecipeOptionMdList.Add(aOptionMD);
                        }
                    }
                }
                LoadPackageItemDetails();
            }
        }

        private List<RecipeOptionItemButton> GetFreeOptionPrice(List<FreeOptionMD> freeoptionLimitList, List<RecipeOptionItemButton> optionList)
        {
            foreach (FreeOptionMD freeOption in freeoptionLimitList)
            {
                List<RecipeOptionItemButton> aRecipeOptionItemButtons = optionList.Where(a => a.RecipeOptionId == freeOption.CategoryId).ToList();
                int count = freeOption.FreeLimit;
                int cnt = 0;
                foreach (RecipeOptionItemButton item in aRecipeOptionItemButtons)
                {
                    cnt++;
                    if (cnt <= count)
                    {
                        item.InPrice = 0;
                        item.Price = 0;
                    }
                }
            }
            return optionList;
        }

        private int CheckDulicate(PackageItem aOrderItemDetails, List<RecipeOptionItemButton> aRecipeList)
        {
            List<PackageItem> itemDetails = aPackageItemList.Where(a => a.ItemId == aOrderItemDetails.ItemId).ToList();
            if (itemDetails.Count == 0)
            {
                return 0;
            }
            int result = 0;
            foreach (PackageItem item in itemDetails)
            {
                List<RecipeOptionMD> aRList = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex).ToList();
                if (aRecipeList.Count == 0 && aRList.Count == 0)
                {
                    return item.OptionsIndex;
                }

                int cnt = 0;
                bool res = true;
                if (aRList.Count == aRecipeList.Count)
                {
                    foreach (RecipeOptionMD list in aRList)
                    {
                        bool flag = false;
                        foreach (RecipeOptionItemButton recipe in aRecipeList)
                        {
                            if (!CheckWithoutMinusItemIsExits(list, recipe)) res = false;
                            else cnt++;
                        }
                    }

                    if (cnt != 0 && cnt == aRecipeList.Count)
                    {
                        result = item.OptionsIndex;
                    }
                }
            }
            return result;
        }
        private bool CheckWithoutMinusItemIsExits(RecipeOptionMD saveItem, RecipeOptionItemButton currentItem)
        {
            if (saveItem.Title != null)
            {
                if (saveItem.Title != currentItem.Title) return false;
            }
            if (currentItem.MinusTitle != null)
            {
                if (saveItem.MinusOption == currentItem.MinusTitle) return false;
            }

            if (currentItem.Title != null && currentItem.Title != null && saveItem.MinusOption != currentItem.MinusTitle)
            {
                return false;
            }

            return true;
        }

        private void LoadPackageItemDetails()
        {
            itemDetailsFlowLayoutPanel1.Controls.Clear();
            string res = "";
            // int package_qty = Convert.ToInt32(comboBoxQty.Text);
            foreach (PackageItem item in aPackageItemList)
            {
                Label aLabel = new Label();
                aLabel.AutoSize = true;
                aLabel.Text += (item.Qty) + " X ";
                aLabel.Text += item.ItemName + "  ";
                if (item.Price > 0)
                {
                    aLabel.Text += (item.Qty * item.Price);
                }

                List<RecipeOptionMD> tempOptionlist = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex).ToList();
                foreach (RecipeOptionMD list in tempOptionlist)
                {
                    if (!string.IsNullOrEmpty(list.MinusOption))
                    {
                        if (list.InPrice > 0)
                        {
                            aLabel.Text += "\r\n  " + ("No " + list.MinusOption);
                        }
                        else aLabel.Text += "\r\n  " + ("No " + list.MinusOption);

                    }
                    if (!string.IsNullOrEmpty(list.Title))
                    {
                        if (list.Price > 0)
                        {
                            aLabel.Text += "\r\n  " + (list.Title + " +" + list.Price);
                        }
                        else aLabel.Text += "\r\n  " + (list.Title);
                    }
                }
                itemDetailsFlowLayoutPanel1.Controls.Add(aLabel);
                res += aLabel + "\r\n";
            }
            buttonPanel.Location = new Point(itemDetailsFlowLayoutPanel1.Location.X, itemDetailsFlowLayoutPanel1.Size.Height + itemDetailsFlowLayoutPanel1.Location.Y);
        }

        private void LoadPackageItemDetailsForplusMinus()
        {
            itemDetailsFlowLayoutPanel1.Controls.Clear();
            string res = "";
            int package_qty = Convert.ToInt32(comboBoxQty.Text);
            foreach (PackageItem item in aPackageItemList)
            {
                Label aLabel = new Label();
                aLabel.AutoSize = true;
                aLabel.Text += (item.Qty * package_qty) + " X ";
                aLabel.Text += item.ItemName + "  ";
                if (item.Price > 0)
                {
                    aLabel.Text += (item.Qty * item.Price) * package_qty;
                }

                List<RecipeOptionMD> tempOptionlist = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex).ToList();
                foreach (RecipeOptionMD list in tempOptionlist)
                {
                    if (!string.IsNullOrEmpty(list.MinusOption))
                    {
                        if (list.InPrice > 0)
                        {
                            aLabel.Text += "\r\n  " + ("No " + list.MinusOption);
                        }
                        else aLabel.Text += "\r\n  " + ("No " + list.MinusOption);
                    }
                    if (!string.IsNullOrEmpty(list.Title))
                    {
                        if (list.Price > 0)
                        {
                            aLabel.Text += "\r\n  " + (list.Title + " +" + list.Price);
                        }
                        else aLabel.Text += "\r\n  " + (list.Title);
                    }
                }
                itemDetailsFlowLayoutPanel1.Controls.Add(aLabel);
                res += aLabel + "\r\n";
            }
            buttonPanel.Location = new Point(itemDetailsFlowLayoutPanel1.Location.X, itemDetailsFlowLayoutPanel1.Size.Height + itemDetailsFlowLayoutPanel1.Location.Y);
        }

        private bool CheckCategoryLimitLimit(PackageCategoryButton packageCategoryButton, PackageItemButton itemButton)
        {
            int catLimit = Convert.ToInt32(comboBoxQty.Text);
            List<PackageItem> items = aPackageItemList.Where(a => a.PackageId == itemButton.PackageId && a.OptionName == itemButton.PackageCategoryButton.OptionName).ToList();
            if (items.Sum(a => a.Qty) >= (packageCategoryButton.Items * catLimit) && packageCategoryButton.Items > 0)
            {
                PackageItem aItem = items[0];
                aPackageItemList.RemoveAll(a => a.OptionsIndex == aItem.OptionsIndex);
                aRecipeOptionMdList.RemoveAll(a => a.OptionsIndex == aItem.OptionsIndex);
            }
            return true;
        }

        private int GetOptionIndex()
        {
            int index2 = aPackageItemList.Count == 0 ? 0 : aPackageItemList.Max(a => a.OptionsIndex);
            return index2;
        }

        private void ChangeColor(PackageItemButton itemButton)
        {
            itemButton.BackColor = Color.Black;
            itemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
            itemButton.ForeColor = Color.White;
            itemButton.FlatAppearance.BorderSize = 0;
            itemButton.FlatStyle = FlatStyle.Flat;
            itemButton.AutoSize = true;

            foreach (PackageItemButton tempButton in attributeFlowLayoutPanel.Controls.OfType<PackageItemButton>())
            {
                if (tempButton != itemButton)
                {
                    tempButton.BackColor = ColorTranslator.FromHtml("#967adc");
                }
            }
        }

        private void ChangePackageColor(PackageItemButton itemButton)
        {
            itemButton.BackColor = Color.Black;
            itemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
            itemButton.ForeColor = Color.White;
            itemButton.FlatAppearance.BorderSize = 0;
            itemButton.FlatStyle = FlatStyle.Flat;
            itemButton.AutoSize = true;

            foreach (PackageItemButton tempButton in attributeFlowLayoutPanel.Controls.OfType<PackageItemButton>())
            {
                if (tempButton != itemButton)
                {
                    tempButton.BackColor = ColorTranslator.FromHtml("#967adc");
                }
            }           
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (aPackageItemList.Count <= 0)
            {
                MessageBox.Show("Please Select Atleast one item");
                return;
            }
            packageQty = Convert.ToInt32(comboBoxQty.Text);
            status = "ok"; 
        //    aPackageItemList.ForEach(a => a.Price = (a.Price / a.Qty) * (a.Qty * packageQty));
        //    aPackageItemList.ForEach(a => a.Qty = a.Qty * packageQty);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(comboBoxQty.Text) > 2)
            {
                comboBoxQty.Text = (Convert.ToInt32(comboBoxQty.Text) - 1).ToString();
            }
            else {
                comboBoxQty.Text = "1";
            }
            LoadPackageItemDetailsForplusMinus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(comboBoxQty.Text) >= 2)
            //{
                comboBoxQty.Text = (Convert.ToInt32(comboBoxQty.Text) + 1).ToString();
            LoadPackageItemDetailsForplusMinus();
            //}
            //else
            //{
            //    comboBoxQty.Text = "2";
            //}
        }
    }

    public class FreeOptionMD
    {
        public int CategoryId { set; get; }
        public int FreeLimit { set; get; }
    }
}