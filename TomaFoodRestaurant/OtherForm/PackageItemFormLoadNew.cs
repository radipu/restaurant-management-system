using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL;
using TomaFoodRestaurant.Model;
namespace TomaFoodRestaurant.OtherForm
{
    public partial class PackageItemFormLoadNew : UserControl
    {
        public MainFormView aMainFormView = null;
        public List<PackageItem> aPackageItemList = new List<PackageItem>();
        PackageCategoryButton tempPackage = new PackageCategoryButton();
        DataTable dtDataTable = new DataTable();
        static List<PackageItem> tempFixPackageItem = new List<PackageItem>();
        public List<RecipeOptionItemButton> aRecipeList = new List<RecipeOptionItemButton>();

        public string IsUpdate = "";

        //   List<ReceipeMenuItemButton> getallsuButtons = new RestaurantMenuBLL().AllRecipeButton().ToList();
        public PackageItemFormLoadNew(MainFormView viewMainFormView, RecipePackageButton mainPackageButton)
        {
            InitializeComponent();

            splashScreenManager2.ShowWaitForm();

            dtDataTable.Columns.Add(new DataColumn("Index", typeof(int)));
            dtDataTable.Columns.Add(new DataColumn("PackageItemName"));
            dtDataTable.Columns.Add(new DataColumn("EditName"));
            dtDataTable.Columns.Add(new DataColumn("PoptionId"));
            dtDataTable.Columns.Add(new DataColumn("POptionName"));
            dtDataTable.Columns.Add(new DataColumn("Class", typeof(PackageItem)));
            dtDataTable.Columns.Add(new DataColumn("Remove", typeof(string)));
            pacakgegridControl.DataSource = dtDataTable;
            tempFixPackageItem.Clear();

            aMainFormView = viewMainFormView;
            IsUpdate = mainPackageButton.PackageUpdateOrNot;

            // the code that you want to measure comes here



            LoadPackageCategory(mainPackageButton);


            //*** Fixed Item Add Initial******//




            lblPrice.Text = "£" + mainPackageButton.InPrice.ToString("F");
            //tableLayoutPanel1.SetColumnSpan(navBarFlowLayOut, 2);

            if (mainPackageButton.UpdatePackageList != null)
            {
                aPackageItemList = mainPackageButton.UpdatePackageList;

                foreach (PackageItem packageOptionItem in mainPackageButton.UpdatePackageList)
                {
                    if (packageOptionItem.PackageItemOptionList != null)
                    {
                        foreach (RecipeOptionMD recipeOptionMd in packageOptionItem.PackageItemOptionList)
                        {
                            aRecipeOptionMdList.Add(recipeOptionMd);

                        }
                    }



                }


                LoadPackageItemDetails();

                ItemCheckInitialPage();
            }



            splashScreenManager2.CloseWaitForm();

        }
        public void ResponsiveItem(int column, bool cardVisibleInvisible, TileControl tileControl1)
        {
            var size = Screen.PrimaryScreen.Bounds;


            if (size.Width == 1024)
            {

                if (cardVisibleInvisible)
                {
                    tileControl1.ItemPadding = new Padding(1);
                    tileControl1.ColumnCount = 10;

                    tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.ColumnStyles[0].Width = 450;

                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 24);

                    }
                    else
                    {
                        tileControl1.ItemSize = (size.Width / 24);
                    }
                    //else if (column > 4)
                    //{

                    //    tileControl1.ItemSize = (size.Width / 15) + 2;
                    //}
                    //else if (column > 3)
                    //{
                    //    tileControl1.ItemSize = (size.Width / 12) + 1;
                    //}
                    //else if (column > 2)
                    //{
                    //    tileControl1.ItemSize = (size.Width / 9) + 1;
                    //}
                }
                else
                {

                    tileControl1.ColumnCount = 16;

                    tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.ColumnStyles[0].Width = 750;
                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 16) - 2;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 10);
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 8) - 1;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 6) - 1;
                    }
                }



            }
            else
            {
                if (cardVisibleInvisible)
                {
                    tileControl1.ColumnCount = 12;
                    if (column >= 5)
                    {
                        tileControl1.ItemSize = (size.Width / 22) - 0;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 13) - 2;
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 12) - 16;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 7) - 22;
                    }
                    else
                    {
                        tileControl1.ItemSize = 120;
                    }
                }
                else
                {
                    tileControl1.ColumnCount = 16;


                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 23) - 0;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 13) - 2;
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 12) - 2;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 10) - 22;
                    }

                }


            }

        }
        private void tillMainControl_Click(object sender, EventArgs e)
        {

        }

        public int DoubleOpionCheck(PackageCategoryButton categoryButton, List<PackageCategoryButton> allCategoryButtons)
        {
            allCategoryButtons = allCategoryButtons.Where(a => a.CategoryId == categoryButton.CategoryId).ToList();
            return allCategoryButtons.Sum(a => a.Items);

        }
        RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
        private void LoadPackageCategory(RecipePackageButton aRecipePackageButton)
        {
            var watchTime = Stopwatch.StartNew();


            PackageBLL aPackageBll = new PackageBLL();
            List<PackageCategoryButton> tempList = aPackageBll.GetPackageCategory(aRecipePackageButton);
            bool fla = false;


            //var loadPackage = new PackageItemBLL().GetPackageItem(aRecipePackageButton, 0);

            foreach (PackageCategoryButton aPackageCategoryButton in tempList)
            {
                CutomeTileItem cutomTile = new CutomeTileItem();
                cutomTile.Text = aPackageCategoryButton.OptionName; cutomTile.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));
                cutomTile.AppearanceItem.Normal.BorderColor = Color.CornflowerBlue;
                cutomTile.AppearanceItem.Selected.BorderColor = Color.CornflowerBlue;
                cutomTile.AppearanceItem.Selected.BackColor = Color.Black;
                cutomTile.CategoryId = aPackageCategoryButton.CategoryId;
                cutomTile.AppearanceItem.Normal.Font = aPackageCategoryButton.Font;
                cutomTile.AppearanceItem.Selected.Font = aPackageCategoryButton.Font;
                
                //cutomTile.Items = DoubleOpionCheck(aPackageCategoryButton, tempList);
                //cutomTile.TextAlignment=TileItemContentAlignment.;

                cutomTile.ItemSize = TileItemSize.Wide;// cutomTile.Padding = new Padding(50, 10, 50, 50);
                cutomTile.PackageCategoryButton = aPackageCategoryButton;


                aPackageCategoryButton.Text = aPackageCategoryButton.OptionName.ToString();
                aPackageCategoryButton.RecipePackage = aRecipePackageButton;
                aPackageCategoryButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));
                aPackageCategoryButton.ForeColor = Color.White;
                aPackageCategoryButton.Margin = new Padding(1);
                aPackageCategoryButton.Padding = new Padding(1);
                aPackageCategoryButton.MinimumSize = new Size(aPackageCategoryButton.Width, 10);
                aPackageCategoryButton.Height = 20;


                cutomTile.ItemClick -= new TileItemClickEventHandler(PackageCategoryButton_Click);
                cutomTile.ItemClick += new TileItemClickEventHandler(PackageCategoryButton_Click);

                watchTime.Start();
                cutomTile.PackageCategoryButton = aPackageCategoryButton;

                if (CheckAutoAdd(aPackageCategoryButton))
                {

                    continue;
                }

                if (!fla)
                {
                    tempPackage = aPackageCategoryButton;
                    fla = true;
                }


                tileGroupTopBar.Items.Add(cutomTile);

                if (tileGroupTopBar.Items.Count == 1)
                {
                    cutomTile.AppearanceItem.Normal.BackColor = Color.Black;

                    LoadDetails(aPackageCategoryButton);
                    tileGroupTopBar.Items.Add(cutomTile);

                }

            }



        }

        private bool CheckAutoItemAdd(List<PackageItem> loadPackage, PackageCategoryButton aPackageCategoryButton)
        {
            var exist = loadPackage.FirstOrDefault(a => a.OptionName == aPackageCategoryButton.OptionName);
            if (exist == null)
            {
                return false;

            }
            return true;
        }
        public void ItemCheckInitialPage()
        {

            try
            {
                CutomeTileItem TopList = tileGroupTopBar.Items.OfType<CutomeTileItem>().FirstOrDefault(a => a.Appearance.BackColor == Color.Black);


                foreach (PackageItemButtonNew item in tileGroupContainer.Items)
                {
                    var gridPackageItem = dtDataTable.AsEnumerable().Select(a => a["Class"]);

                    foreach (PackageItem gridItem in gridPackageItem)
                    {
                        if (gridItem.ItemId == item.packageItemButtonNew.RecipeId && TopList.PackageCategoryButton.OptionName == gridItem.OptionName)
                        {
                            item.Checked = true;
                        }


                    }

                }
            }
            catch (Exception ex)
            {

                string excepion = ex.Message;
            }
           
        }
        private int hasSubcategoryId = 0;
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
        private PackageCategoryButton tempButton = new PackageCategoryButton();
        public List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        private bool CheckCategoryLimitLimit(PackageCategoryButton packageCategoryButton, PackageItemButton itemButton)
        {

            // ListOfOptionName and ListOfOptionId is Item Option But OptionName is Tab Link OptionName
            // *** To be Checked Database insert and Retirve position .............................

            //  List<PackageItem> items = aPackageItemList.Where(a => a.PackageId == itemButton.PackageId && a.ItemId==itemButton.RecipeId).ToList();

            List<PackageItem> ListOpExisitingPackageItem = dtDataTable.AsEnumerable().Select(a => (PackageItem)a["Class"]).ToList();
            // For Update Retrive  when checking 
            if (aPackageItemList.Count <= 0)
            {
                aPackageItemList = ListOpExisitingPackageItem;
                var option = ListOpExisitingPackageItem.Select(a => a.PackageItemOptionList).ToList();
                foreach (List<RecipeOptionMD> list in option)
                {
                    foreach (RecipeOptionMD recipeOptionMd in list)
                    {
                        aRecipeOptionMdList.Add(recipeOptionMd);
                    }

                }



            }
            List<PackageItem> items = aPackageItemList.Where(a => a.PackageId == itemButton.PackageId && a.CategoryId == itemButton.CategoryId).ToList();
            if (items.Count > 0 && ListOpExisitingPackageItem.Count > 0)
            {
                if (items.Sum(a => a.Qty) >= packageCategoryButton.Items && packageCategoryButton.Items > 0)
                {
                    PackageItem aItem = items[0];
                    aPackageItemList.RemoveAll(a => a.OptionsIndex == aItem.OptionsIndex);
                    aRecipeOptionMdList.RemoveAll(a => a.OptionsIndex == aItem.OptionsIndex);
                    if (tileGroupTopBar.Items.Count > 1)
                    {
                        NextPageSelect();
                    }

                    //  dtDataTable.Rows.RemoveAt(dtDataTable.Rows.Count-1);

                }
            }


            return true;
        }



        public void NextPageSelect()
        {
            try
            {
                hasSubcategoryId = 0;

                int index = 0;

                foreach (CutomeTileItem control in tileGroupTopBar.Items)
                {

                    if (control.AppearanceItem.Normal.BackColor == Color.Black)
                    {
                        //control.BackColor = Color.Black;
                        index = tileGroupTopBar.Items.IndexOf(control);
                        if (tileGroupTopBar.Items.Count - 1 == index)
                        {
                            CutomeTileItem category = (CutomeTileItem)tileGroupTopBar.Items[index];
                            control.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));
                            LoadDetails(category.PackageCategoryButton);

                          

                        }else
                        {
                            CutomeTileItem category = (CutomeTileItem)tileGroupTopBar.Items[index + 1];
                            index = index + 1;
                            control.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));
                            LoadDetails(category.PackageCategoryButton);


                        }


                    }
                    else
                    {
                        control.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));

                    }
                }

                tileGroupTopBar.Items[index].AppearanceItem.Normal.BackColor = Color.Black;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Index :" + ex.Message);this.aMainFormView.Activate();

            }

        }

        private bool CheckAutoAdd(PackageCategoryButton aPackageCategoryButton)
        {
            bool result = false;
            try
            {

                // PackageCategoryButton category = aPackageCategoryButton;
                List<PackageItemButton> allPackageItems = LoadDetailsForLoad(aPackageCategoryButton);

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
                            aItem.DeleteItem = true;

                            aRecipeList = new List<RecipeOptionItemButton>();
                            if (aPackageCategoryButton.ShowOption > 0)
                            {
                                List<RecipeOptionButton> OptionList = new RestaurantMenuBLL().GetRecipeOptionWhenItemClick(itemButton.RecipeId).ToList();

                                AllOptionViewForm allOptionViewForm = new AllOptionViewForm(aMainFormView.aGeneralInformation, OptionList, aPackageCategoryButton, this);

                                allOptionViewForm.ShowDialog();

                                //ItemOptionForm aItemOptionForm = new ItemOptionForm(itemButton.RecipeId);
                                //aItemOptionForm.ShowDialog();
                            }
                            if (CheckCategoryLimitLimit(itemButton.PackageCategoryButton, itemButton))
                            {
                                aPackageItemList.Add(aItem);
                                tempFixPackageItem.Add(aItem);
                                foreach (RecipeOptionItemButton recipe in aRecipeList)
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
                                    aOptionMD.Isoption = recipe.IsNooption;
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
                            aRecipeList = new List<RecipeOptionItemButton>();
                            aItem.DeleteItem = true;

                            if (aPackageCategoryButton.ShowOption > 0)
                            {
                                List<RecipeOptionButton> OptionList = new RestaurantMenuBLL().GetRecipeOptionWhenItemClick(itemButton.RecipeId).ToList();

                                AllOptionViewForm allOptionViewForm = new AllOptionViewForm(aMainFormView.aGeneralInformation, OptionList, aPackageCategoryButton, this);

                                allOptionViewForm.ShowDialog();

                                //ItemOptionForm aItemOptionForm = new ItemOptionForm(itemButton.RecipeId);
                                //aItemOptionForm.ShowDialog(
                            }

                            if (CheckCategoryLimitLimit(itemButton.PackageCategoryButton, itemButton))
                            {
                                aPackageItemList.Add(aItem);
                                tempFixPackageItem.Add(aItem);

                                foreach (RecipeOptionItemButton recipe in aRecipeList)
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
                                    aOptionMD.Isoption = recipe.IsNooption;
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
            }
            catch (Exception)
            {
                return false;

            }


            return result;




        }

        private void PackageCategoryButton_Click(object sen13, EventArgs e)
        {
            CutomeTileItem cateTileItem = sen13 as CutomeTileItem;

            //PackageCategoryButton category = sen13 as PackageCategoryButton;
            hasSubcategoryId = 0;

            foreach (TileItem control in tileGroupTopBar.Items)
            {
                if (control == cateTileItem)
                {
                    control.AppearanceItem.Normal.BackColor = Color.Black;

                }
                else
                {

                    control.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));
                }
            }


            LoadDetails(cateTileItem.PackageCategoryButton);

        }

        private void LoadDetails(PackageCategoryButton category)
        {
            try
            {
                LoadAllItem(category);
                ItemCheckInitialPage();
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.aMainFormView.Activate();
            }




        }

        private void PackageItemFormLoadNew_Load(object sender, EventArgs e)
        {


        }
        private void LoadAllItem(PackageCategoryButton category)
        {
            try
            {
                tileGroupContainer.Items.Clear();


                //receipeCategoryFlowLayoutPanel1.Visible = true;
                PackageBLL aPackageBll = new PackageBLL();
                DataSet DSs = new DataSet();
                DataTable DTs = new DataTable();
                //DataTable datable = new PackageBLL().GetPackageCategory1();

                EnumerableRowCollection<DataRow> Rows = aMainFormView.GetPackageCategory.AsEnumerable().Where(a => Convert.ToString(a["option_name"]) == category.OptionName && Convert.ToInt32(a["package_id"]) == category.PackageId);


                DTs = Rows.AsDataView().ToTable();
                // DTs = 
                List<PackageItemButton> aPackageItemButtonList = new List<PackageItemButton>();
                PackageItemButton packageItemButton = new PackageItemButton();
                int rowCount = 0;
                foreach (DataRow mainData in DTs.Rows)
                {
                    //

                    if (Convert.ToString(mainData["subcategory_id"]).Length == 0 || Convert.ToString(mainData["category_id"]).Length == 0)
                    {
                        DataSet DS = new DataSet();
                        DataTable DT = new DataTable(); string query = "";

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
                            packageItemButton.CategoryId = (int)Convert.ToInt64(mainData["category_id"]);
                            //packageItemButton.CategoryId = (int) Convert.ToInt64(mainData["category_id"]);
                        }

                        DT = aPackageBll.GetPackagerReceipeByPackageItem(packageItemButton);


                        ResponsiveItem(DT.Rows.Count, aMainFormView.dockPanel1.Visible, tillMainControl);

                        if (Convert.ToInt32(mainData["all_recipe"]) > 0)
                        {
                            foreach (DataRow dataRow1 in DT.Rows)
                            {

                                PackageItemButtonNew aPackageItemButton = new PackageItemButtonNew();
                                aPackageItemButton.Appearance.BorderColor = Color.Transparent;
                                aPackageItemButton.ItemSize = TileItemSize.Wide;
                                aPackageItemButton.TextAlignment = TileItemContentAlignment.MiddleCenter;

                                aPackageItemButton.packageItemButtonNew = new PackageItemButton();
                                aPackageItemButton.packageItemButtonNew.OptionName = Convert.ToString(mainData["option_name"]);
                                aPackageItemButton.packageItemButtonNew.ItemName = Convert.ToString(dataRow1["name"]);
                                aPackageItemButton.packageItemButtonNew.PackageId = Convert.ToInt32(mainData["package_id"]);
                                aPackageItemButton.packageItemButtonNew.RecipeId = Convert.ToInt32(dataRow1["id"]);
                                aPackageItemButton.packageItemButtonNew.AddPrice = Convert.ToDouble(dataRow1["out_price"]);
                                aPackageItemButton.packageItemButtonNew.ReciptName = Convert.ToString(dataRow1["receipt_name"]);
                                aPackageItemButton.packageItemButtonNew.SubCategoryId = Convert.ToInt32(dataRow1["subcategory_id"]);
                                aPackageItemButton.packageItemButtonNew.CategoryId = Convert.ToInt32(dataRow1["category_id"]);

                                aPackageItemButton.Text = dataRow1["name"].ToString();

                                aPackageItemButton.packageItemButtonNew.FreeOptionMds = category.FreeOptionMds;

                                aPackageItemButton.packageItemButtonNew.PackageCategoryButton = category;
                                aPackageItemButton.packageItemButtonNew.RecipePackageButton = category.RecipePackage;
                                // Control To be add///


                                aPackageItemButton.Appearance.ForeColor = Color.White;
                                aPackageItemButton.packageItemButtonNew.FreeOptionMds = category.FreeOptionMds;
                                aPackageItemButton.Appearance.BackColor = ColorTranslator.FromHtml("#967adc");
                                aPackageItemButton.packageItemButtonNew.PackageCategoryButton = category;
                                aPackageItemButton.packageItemButtonNew.RecipePackageButton = category.RecipePackage;
                                aPackageItemButton.ItemClick += new TileItemClickEventHandler(PackageItemButton_Click);

                                Font font = new Font(category.Font.FontFamily, 10);
                                aPackageItemButton.AppearanceItem.Normal.Font = font;
                                aPackageItemButton.AppearanceItem.Selected.Font = font;


                                tileGroupContainer.Items.Add(aPackageItemButton);
                                //rightRecepiflowLayoutPanel.Controls.Add(aPackageItemButton);
                                rowCount++;
                            }


                        }
                        else
                        {
                            foreach (DataRow dataRow1 in DT.Rows)
                            {

                                PackageItemButtonNew aPackageItemButton = new PackageItemButtonNew();
                                aPackageItemButton.AppearanceItem.Normal.BorderColor = Color.Transparent;
                                aPackageItemButton.AppearanceItem.Selected.BorderColor = Color.Transparent;

                                aPackageItemButton.ItemSize = TileItemSize.Wide;
                                aPackageItemButton.TextAlignment = TileItemContentAlignment.MiddleCenter;

                                aPackageItemButton.AppearanceItem.Normal.Font = new Font("Tahoma", 10);
                                aPackageItemButton.AppearanceSelected.Font = new Font("Tahoma", 10);
                                aPackageItemButton.packageItemButtonNew = new PackageItemButton();

                                aPackageItemButton.packageItemButtonNew.OptionName = Convert.ToString(dataRow1["option_name"]);
                                aPackageItemButton.packageItemButtonNew.ItemName = Convert.ToString(dataRow1["name"]);
                                aPackageItemButton.packageItemButtonNew.PackageId = Convert.ToInt32(dataRow1["package_id"]);
                                aPackageItemButton.packageItemButtonNew.RecipeId = Convert.ToInt32(dataRow1["recipe_id"]);
                                aPackageItemButton.packageItemButtonNew.AddPrice = Convert.ToDouble(dataRow1["add_price"]);
                                aPackageItemButton.packageItemButtonNew.ReciptName = Convert.ToString(dataRow1["receipt_name"]);
                                aPackageItemButton.packageItemButtonNew.SubCategoryId = Convert.ToInt32(dataRow1["subcategory_id"]);
                                aPackageItemButton.packageItemButtonNew.CategoryId = Convert.ToInt32(dataRow1["category_id"]);

                                aPackageItemButton.Text = dataRow1["name"].ToString();

                                aPackageItemButton.packageItemButtonNew.FreeOptionMds = category.FreeOptionMds;
                                aPackageItemButton.Appearance.BackColor = ColorTranslator.FromHtml("#967adc");
                                aPackageItemButton.packageItemButtonNew.PackageCategoryButton = category;
                                aPackageItemButton.packageItemButtonNew.RecipePackageButton = category.RecipePackage;

                                aPackageItemButton.ItemPress += new TileItemClickEventHandler(PackageItemButton_Click);
                                tileGroupContainer.Items.Add(aPackageItemButton);
                                // Control to be add //
                                //rightRecepiflowLayoutPanel.Controls.Add(aPackageItemButton);
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

                            //  category.SubCategory = Convert.ToString(mainData["subcategory_id"]);
                            //category.CategoryId = Convert.ToString(mainData["category_id"]);

                        }
                        DT = aPackageBll.GetPackagerReceipeByPackageItemWithSubCat(category);
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

                                aPackageItemButton.Text = datarow["name"].ToString();
                                aPackageItemButton.FreeOptionMds = category.FreeOptionMds;
                                aPackageItemButton.BackColor = ColorTranslator.FromHtml("#967adc"); aPackageItemButton.Font = new System.Drawing.Font("Tahoma", 11, FontStyle.Bold);
                                aPackageItemButton.ForeColor = Color.White;
                                aPackageItemButton.Width = 100;
                                aPackageItemButton.MaximumSize = new Size(120, 50);
                                aPackageItemButton.MinimumSize = new Size(120, 50);
                                aPackageItemButton.FlatAppearance.BorderSize = 0;
                                aPackageItemButton.FlatStyle = FlatStyle.Flat;
                                aPackageItemButton.AutoSize = true;
                                aPackageItemButton.Click += new EventHandler(PackageItemButton_Click);
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


                tileGroupSubCat.Items.Clear();

                foreach (ReceipeCategoryButton button in categoryList)
                {
                    if (button.HasSubcategory > 0)
                    {
                        tileGroupSubCat.Visible = true;

                        PackageItemButtonNew hasSubCatButton = new PackageItemButtonNew();
                        hasSubCatButton.Text = button.Text;



                        hasSubCatButton.TextAlignment = TileItemContentAlignment.MiddleCenter;
                        category.Font = new Font(button.Font.Name, 10, FontStyle.Bold);


                        hasSubCatButton.Appearance.Font = category.Font;
                        hasSubCatButton.AppearanceItem.Selected.Font = category.Font;



                        hasSubCatButton.ReceipeCategoryButton = button;
                        if (categoryList.Count > 1)
                        {
                            hasSubCatButton.Appearance.BackColor = button.BackColor;

                        }
                        else
                        {
                            hasSubCatButton.Appearance.BackColor = Color.Black;


                        }

                        hasSubCatButton.ItemSize = TileItemSize.Wide;
                        hasSubCatButton.Appearance.BorderColor = Color.Transparent;
                        hasSubCatButton.AppearanceItem.Selected.BorderColor = Color.Transparent;
                        hasSubCatButton.SubCategoriesList = subCategories;


                        hasSubCatButton.ItemClick -= btn_Read_MouseClick;
                        hasSubCatButton.ItemClick += btn_Read_MouseClick;

                        tileGroupSubCat.Items.Add(hasSubCatButton);

                        if (tileGroupContainer.Items.Count == 0)
                        {
                            CutomeTileItem CategoryButton = (CutomeTileItem)tileGroupTopBar.Items[0];
                            hasSubCatButton.PackageCategoryButton = new PackageCategoryButton();
                            hasSubCatButton.PackageCategoryButton = CategoryButton.PackageCategoryButton;
                            hasSubCatButton.CategoryId = hasSubCatButton.ReceipeCategoryButton.CategoryId;
                            hasSubcategoryId = hasSubCatButton.CategoryId;
                            LoadSubCatLoad(hasSubCatButton);
                        }
                        //receipeCategoryFlowLayoutPanel1.Controls.Add(button);
                    }
                }





                //  subCategoryList = subCategoryList.OrderBy(a => a.ItemName).ThenBy(a => a.Colorname).ToList();
                //subCategoryList = subCategoryList.OrderBy(a => a.Colorname).ToList();
                //ResponsiveItem(subCategoryList.Count, aMainFormView.dockPanel1.Visible, tillMainControl);
                //foreach (PackageItemButton button in subCategoryList)
                //{
                //    PackageItemButtonNew hasSubCatButton = new PackageItemButtonNew();
                //    hasSubCatButton.Text = button.Text;
                //    hasSubCatButton.packageItemButtonNew = button;

                //    if (subCategoryList.Count > 0)
                //    {
                //        hasSubCatButton.Appearance.BackColor = button.BackColor;
                //    }
                //    else
                //    {
                //        hasSubCatButton.Appearance.BackColor = Color.Black;
                //    }


                //    hasSubCatButton.ItemSize = TileItemSize.Wide;
                //    hasSubCatButton.Appearance.BorderColor = Color.Transparent;
                //    hasSubCatButton.AppearanceItem.Normal.Font = button.Font;

                //    hasSubCatButton.Appearance.TextOptions.VAlignment = VertAlignment.Center;

                //    hasSubCatButton.ItemClick += new TileItemClickEventHandler(PackageItemButton_Click);

                //    hasSubCatButton.RecipePackageButton = category.RecipePackage;
                //    hasSubCatButton.PackageCategoryButton = category;
                //    //  attributeFlowLayoutPanel.Controls.Add(button);
                //    tileGroupContainer.Items.Add(hasSubCatButton);

                //}
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.aMainFormView.Activate();

            }


        }

        private void btn_Read_MouseClick(object sender, EventArgs e)
        {
            PackageItemButtonNew aButton = sender as PackageItemButtonNew;
            aButton.AppearanceItem.Normal.BackColor = Color.Black;
            hasSubcategoryId = aButton.ReceipeCategoryButton.CategoryId;

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            foreach (PackageItemButtonNew tempButton in tileGroupSubCat.Items)
            {
                if (tempButton != aButton)
                {
                    var colorCode = aRestaurantMenuBll.GetColorCode(tempButton.ReceipeCategoryButton.Color);
                    tempButton.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(colorCode);

                }
            }
            foreach (CutomeTileItem control in tileGroupTopBar.Items)
            {
                if (control.AppearanceItem.Normal.BackColor == Color.Black)
                {
                    aButton.PackageCategoryButton = control.PackageCategoryButton;
                }
            }
            LoadSubCatLoad(aButton);
            //var subCatItem = tileGroupContainer.Items;


            // To be Continue///}
        }

        public void LoadSubCatLoad(PackageItemButtonNew aButton)
        {
            tileGroupContainer.Items.Clear();

            PackageBLL aPackageBll = new PackageBLL();


            List<PackageItemButton> subCategoryList = aPackageBll.GerSubCategoryList(aButton.SubCategoriesList, aButton.PackageCategoryButton);
            ResponsiveItem(subCategoryList.Count, aMainFormView.dockPanel1.Visible, tillMainControl);

            int count = subCategoryList.Count;
            if (count >= 1)
            {
                //int height = (count/10);
                //if (count%10 != 0) height += 1;

                //subCategoryList = subCategoryList.OrderBy(a => a.ItemName).ToList();
                //subCategoryList = subCategoryList.OrderBy(a => a.Colorname).ToList();
                foreach (PackageItemButton button in subCategoryList)
                {
                    button.Click += new EventHandler(PackageItemButton_Click);
                    button.MaximumSize = new Size(90, 50);
                    button.MinimumSize = new Size(90, 50);
                    button.Size = new Size(90, 50);

                    PackageItemButtonNew subCatPackageItemButtonNew = new PackageItemButtonNew();
                    subCatPackageItemButtonNew.packageItemButtonNew = new PackageItemButton();

                    subCatPackageItemButtonNew.packageItemButtonNew = button;
                    subCatPackageItemButtonNew.RecipePackageButton = aButton.PackageCategoryButton.RecipePackage;
                    subCatPackageItemButtonNew.PackageCategoryButton = aButton.PackageCategoryButton;
                    subCatPackageItemButtonNew.hasSubCategory = aButton.CategoryId;
                    subCatPackageItemButtonNew.Text = button.Text;
                    subCatPackageItemButtonNew.AppearanceItem.Normal.Font = new Font("Tahoma", 11);
                    subCatPackageItemButtonNew.AppearanceSelected.Font = new Font("Tahoma", 11);
                    subCatPackageItemButtonNew.Appearance.BackColor = button.BackColor;
                    subCatPackageItemButtonNew.AppearanceItem.Normal.BorderColor = Color.Transparent;
                    subCatPackageItemButtonNew.AppearanceItem.Selected.BorderColor = Color.Transparent;
                    subCatPackageItemButtonNew.ItemSize = TileItemSize.Wide;
                    subCatPackageItemButtonNew.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    subCatPackageItemButtonNew.ItemPress += new TileItemClickEventHandler(PackageItemButton_Click);
                    tileGroupContainer.Items.Add(subCatPackageItemButtonNew);


                }
            }

        }
        private int GetOptionIndex()
        {
            var list = tempFixPackageItem.Concat(aPackageItemList).ToList();

            //  int index2 = tempFixPackageItem.Count == 0 ? 0 : tempFixPackageItem.Max(a => a.OptionsIndex);
            int index2 = list.Count == 0 ? 0 : list.Max(a => a.OptionsIndex);
            return index2;
        }

        private void PackageItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
                PackageItemButtonNew Button = sender as PackageItemButtonNew;
                // ChangePackageColor(itemButton);
                foreach (PackageItemButtonNew tileItem in tileGroupContainer.Items)
                {
                    //List<object> packageItem =dtDataTable.AsEnumerable().Select((PackageItem)a=>a["Class"]).ToList();

                    if (Button == tileItem)
                    {
                        tileItem.Checked = true;
                    }
                    else
                    {
                        tileItem.Checked = false;
                    }
                }

                PackageItemButton itemButton = new PackageItemButton();
                itemButton = Button.packageItemButtonNew;
                if (Button.PackageCategoryButton != null || Button.ReceipeCategoryButton != null)
                {
                    itemButton.PackageCategoryButton = Button.PackageCategoryButton;
                    itemButton.RecipePackageButton = Button.RecipePackageButton;
                }




                //if (itemButton.SubCategoryId > 0 && hasSubcategoryId <= 0)
                //    return;

                PackageItem aItem = new PackageItem();


                bool notFound = false;
                if (itemButton.SubCategoryId > 0 && hasSubcategoryId > 0)
                {
                    string pName = aRestaurantMenuBll.GetSubcategoryByCatAndSubcat(hasSubcategoryId, itemButton.SubCategoryId);
                    if (pName != "")
                    {
                        itemButton.RecipeId = aRestaurantMenuBll.GetRecipeIdByCatAndSubcat(hasSubcategoryId, itemButton.SubCategoryId);

                        aItem.ItemName = pName;
                        aItem.ItemId = itemButton.RecipeId;
                        aItem.Price = aRestaurantMenuBll.GetPrice(hasSubcategoryId, itemButton.SubCategoryId, itemButton.PackageCategoryButton.PackageId, itemButton.RecipeId);
                        aItem.Qty = 1;
                        aItem.OptionName = itemButton.OptionName;
                        aItem.PackageId = itemButton.PackageCategoryButton.PackageId;
                        aItem.CategoryId = itemButton.CategoryId;
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


                    itemButton.CategoryId = itemButton.CategoryId;
                    itemButton.OptionName = itemButton.OptionName;
                    itemButton.PackageId = itemButton.PackageId;

                    PackageCategoryButton aPackageCategoryButton = aRestaurantMenuBll.GetPackagePackageCategory(itemButton);
                    aRecipeList = new List<RecipeOptionItemButton>();
                    if (itemButton.PackageCategoryButton.ShowOption > 0 || (aPackageCategoryButton.ShowOption > 0 && aRestaurantMenuBll.GetReceipeOptionsByItemId(itemButton.RecipeId)))
                    {

                        List<RecipeOptionButton> OptionList = new RestaurantMenuBLL().GetRecipeOptionWhenItemClick(itemButton.RecipeId).ToList();
                        AllOptionViewForm allOptionViewForm = new AllOptionViewForm(aMainFormView.aGeneralInformation, OptionList, aPackageCategoryButton, this);
                        allOptionViewForm.ShowDialog();

                        //ItemOptionForm aItemOptionForm = new ItemOptionForm(itemButton.RecipeId);
                        //aItemOptionForm.ShowDialog();
                    }


                    if (CheckCategoryLimitLimit(itemButton.PackageCategoryButton, itemButton))
                    {



                        int res = CheckDulicate(aItem, aRecipeList);

                        if (res > 0)
                        {
                            aPackageItemList.Where(a => a.OptionsIndex == res).ToList().ForEach(a => a.Qty += 1);

                            aPackageItemList.Where(a => a.OptionsIndex == res).ToList().ForEach(a => a.Price += itemButton.AddPrice);
                        }
                        else
                        {
                            aItem.OptionsIndex = GetOptionIndex() + 1;
                            aPackageItemList.Add(aItem);
                            if (itemButton.FreeOptionMds != null)
                            {
                                aRecipeList = GetFreeOptionPrice(itemButton.FreeOptionMds, aRecipeList);
                            }


                            foreach (RecipeOptionItemButton recipe in aRecipeList)
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
                                aOptionMD.Isoption = recipe.IsNooption;
                                aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                                aRecipeOptionMdList.Add(aOptionMD);
                            }

                        }
                    }

                    LoadPackageItemDetails();

                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().Message);

                this.aMainFormView.Activate();
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
            int result = 0;
            try
            {
                List<PackageItem> itemDetails = aPackageItemList.Where(a => a.ItemId == aOrderItemDetails.ItemId).ToList();
                if (itemDetails.Count == 0)
                {
                    return 0;
                }

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
                                if (!CheckWithoutMinusItemIsExits(list, recipe))
                                    res = false;
                                else cnt++;
                            }
                        }

                        if (cnt != 0 && cnt == aRecipeList.Count)
                        {
                            result = item.OptionsIndex;
                        }
                    }

                }

            }
            catch (Exception)
            {
                this.ParentForm.Activate();
                return 0;

            }
            return result;

        }
        private bool CheckWithoutMinusItemIsExits(RecipeOptionMD saveItem, RecipeOptionItemButton currentItem)
        {
            try
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
            catch (Exception)
            {

                return false;
            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadPackageItemDetails()
        {

            dtDataTable.Rows.Clear();

            string res = "";

            var packageButton = (RecipePackageButton)aMainFormView.flowLayoutPanel1.Controls[1];

            double TotalPrice = aRecipeOptionMdList.Sum(a => a.Price) + packageButton.InPrice;
            TotalPrice = aPackageItemList.Sum(a => a.Price) + TotalPrice;

            lblPrice.Text = "£ " + TotalPrice.ToString("F");
            var defaultItemChck = tempFixPackageItem.ToList();

            foreach (PackageItem item in aPackageItemList.Concat(defaultItemChck))
            {
                Label aLabel = new Label();
                aLabel.AutoSize = true;
                aLabel.Text += item.Qty + " X ";


                aLabel.Text += item.ItemName + "  ";
                if (item.Price > 0)
                {
                    aLabel.Text += (item.Qty * item.Price);
                }
                string bindItem = "<h4 style='font-size:12px;margin:0px; text-align:center'>" + item.ItemName + "</h4>";


                bindItem += "<table style='width:100%;'>";
                List<RecipeOptionMD> tempOptionlist = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex).ToList();
                foreach (RecipeOptionMD list in tempOptionlist)
                {

                    if (!string.IsNullOrEmpty(list.MinusOption))
                    {
                        if (list.InPrice > 0)
                        {
                            aLabel.Text += "</br> &#x2192;" + ("No " + list.MinusOption);

                        }else
                        {
                            aLabel.Text += "</br> &#x2192;" + ("No " + list.MinusOption);

                        }

                    }
                    if (!string.IsNullOrEmpty(list.Title))
                    {
                        if (list.InPrice > 0)
                        {
                            aLabel.Text += "</br> &#x2192;" + (list.Title + " +" + list.Price);
                            bindItem += "<tr>" + "<td>" + item.Qty + "</td>" + "<td style='text-align:left'>" + (list.Title + " +" + list.Price) + "</td>" + "</tr>";
                        }
                        else
                        {
                            aLabel.Text += "</br> &#x2192;" + (list.Title);
                            bindItem += "<tr>" + "<td>" + item.Qty + "</td>" + "<td style='text-align:left'>" + (list.Title) + "</td>" + "</tr>";
                        }

                    }

                    res += aLabel.Text + "</br>";
                    //res += bindItem + "</br>";

                    item.ListChildOptionName += "," + list.Title;

                    item.ListChildOptionId += "," + list.RecipeOPtionItemId;




                    // listUpdate.ItemName.Replace(item.ItemName,aLabel.Text);

                }
                bindItem += "</table>";

                item.PackageItemOptionList = tempOptionlist;


                DataRow dr = dtDataTable.NewRow();
                dr["Index"] = item.OptionsIndex;
                dr["PackageItemName"] = aLabel.Text;
                dr["EditName"] = item.ItemName;

                dr["POptionName"] = item.ListChildOptionName;

                dr["Class"] = item;
                dr["Remove"] = "Remove";

                dr["PoptionId"] = item.ListChildOptionId;

                if (dtDataTable.Rows.Count > 0)
                {
                    var IsExist = dtDataTable.AsEnumerable().FirstOrDefault(a => a["PackageItemName"].ToString() == aLabel.Text);
                    if (IsExist == null)
                    {
                        dtDataTable.Rows.Add(dr);
                    }
                }
                else
                {
                    dtDataTable.Rows.Add(dr);
                }

                res += aLabel + "\r\n";

            }


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFinish_Click(object sender, EventArgs e)
        {

            List<PackageItem> CardPackageItem = new List<PackageItem>();

            foreach (DataRow dataRow in dtDataTable.Rows)
            {
                PackageItem item = (PackageItem)dataRow["Class"];

                CardPackageItem.Add(item);


            }

            aMainFormView.AddToCardFixedItem(CardPackageItem, (RecipePackageButton)aMainFormView.flowLayoutPanel1.Controls[1], packagegridView, IsUpdate);
            List<RecipePackageButton> aRecipePackageButtons = new RestaurantMenuBLL().GetPackageByMenuType(0);
            aMainFormView.tileGroup1.Items.Clear();
            aMainFormView.ResponsiveItem(aRecipePackageButtons.Count, aMainFormView.dockPanel1.Visible, aMainFormView.tileControl1);
            aMainFormView.LoadPackage(aRecipePackageButtons);
            aMainFormView.tileControl1.Controls.Clear();
            aMainFormView.flowLayoutPanel1.Controls.Remove((RecipePackageButton)aMainFormView.flowLayoutPanel1.Controls[1]);


        }

        private void packagegridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Remove")
            {


                PackageItem package = (PackageItem)packagegridView.GetFocusedRowCellValue("Class");

                if (package.DeleteItem)
                {
                    MessageBox.Show("This Item is Fixed , You Can Not Remove this Item ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }


                for (int i = 0; i < package.PackageItemOptionList.Count; i++)
                {
                    aRecipeOptionMdList.Remove(package.PackageItemOptionList[i]);

                }



                aPackageItemList.Remove(package);

                packagegridView.DeleteRow(e.RowHandle);
                double optionPrice = aRecipeOptionMdList.Sum(a => a.InPrice);
                double pacakageItemPrice = aPackageItemList.Sum(a => a.Price);

                RecipePackageButton packageButton = (RecipePackageButton)aMainFormView.flowLayoutPanel1.Controls[1];
                lblPrice.Text = "£" + (optionPrice + packageButton.InPrice + pacakageItemPrice).ToString("F2");

                //  lblPrice.Text = TotalPrice.ToString("C2");


            }
        }


        private void packagegridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

        }

        private void pacakgegridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Down)
            {
                view.MoveNext();
                e.Handled = true;
            }
            if (e.KeyData == Keys.Up)
            {
                view.MovePrev();
                e.Handled = true;
            }
            if (e.KeyData == Keys.Left)
            {
                if (view.FocusedColumn.VisibleIndex > 0)
                {
                    view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex - 1];
                    e.Handled = true;
                }
            }
            if (e.KeyData == Keys.Right)
            {
                if (view.FocusedColumn.VisibleIndex < view.VisibleColumns.Count - 1)
                {
                    view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                    e.Handled = true;
                }
            }
        }

        private void pacakgegridControl_DoubleClick(object sender, EventArgs e)
        {
            EditForm.EditForm eidEditForm = new EditForm.EditForm(this);

            eidEditForm.ShowDialog();

        }
        //string editName = packagegridView.GetFocusedRowCellValue("EditName").ToString();

        //       PackageItem item = (PackageItem)packagegridView.GetFocusedRowCellValue("Class");


        //       item.ItemName = editName;

        //       packagegridView.SetFocusedRowCellValue("Class", item);
        //       packagegridView.SetFocusedRowCellValue("EditName", editName);
        //       packagegridView.SetFocusedRowCellValue("PackageItemName", editName);
    }
}
