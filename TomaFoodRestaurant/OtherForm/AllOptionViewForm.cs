using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class AllOptionViewForm : Form
    {
        private PackageItemFormLoadNew packageForm;


        public AllOptionViewForm(GeneralInformation aInformation, List<RecipeOptionButton> optionList, PackageCategoryButton aPackageCategoryButton, PackageItemFormLoadNew aPackageItemButtonNewForm)
        {
            InitializeComponent();

            aGeneralInformation = aInformation;

            packageForm = aPackageItemButtonNewForm;

            ItemOptionLoad(optionList, new ReceipeMenuItemButton(), "Type", false);
        }


        GridControl gridControlOption = new GridControl() { Dock = DockStyle.Right, Width = 290 };
        private GridView gridViewOption;
        XtraTabControl tabControl = new XtraTabControl { Dock = DockStyle.Fill };
        DataTable optionItemDataTable = new DataTable();
        private DataRow optionDataRow;
        GeneralInformation aGeneralInformation = new GeneralInformation();


        FlowLayoutPanel FooterControl = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Padding = new Padding(5),
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        private void optionRow_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Plus")
            {
                string price = gridViewOption.GetFocusedRowCellValue("Price").ToString();
                int Qty = Convert.ToInt32(gridViewOption.GetFocusedRowCellValue("Qty"));
                string Group = gridViewOption.GetFocusedRowCellValue("Group").ToString();
                CartItem CartItemButton = (CartItem)gridViewOption.GetFocusedRowCellValue("CartItem");

                string tabPageName = tabControl.SelectedTabPage.Text;
                if (tabPageName == Group)
                {

                    int TotalQty = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == Group).Sum(a => Convert.ToInt32(a["Qty"]));
                    if (CartItemButton.RecipeOptionItemButton.RecipeOptionButton.ItemLImit > 100)
                    {

                        if (TotalQty >= CartItemButton.Index){
                            return;
                        }

                        if (price == string.Empty)
                        {
                            gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                            return;
                        }

                        //**************************************************************************************************************

                        decimal Orgprice = Convert.ToDecimal(gridViewOption.GetFocusedRowCellValue("OrgPrice"));
                        gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                        gridViewOption.SetFocusedRowCellValue("Price", ((Qty + 1) * Orgprice).ToString("n2"));

                    }
                    else
                    {
                        if (CartItemButton.RecipeOptionItemButton.RecipeOptionButton.ItemLImit <= TotalQty)
                        {
                            return;
                        }
                        if (price == string.Empty)
                        {
                            gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                            return;
                        }

                        decimal Orgprice = Convert.ToDecimal(gridViewOption.GetFocusedRowCellValue("OrgPrice"));
                        gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                        gridViewOption.SetFocusedRowCellValue("Price", ((Qty + 1) * Orgprice).ToString("n2"));
                    }

                }






                //PackageItem package = (PackageItem)packagegridView.GetFocusedRowCellValue("Class");

                //if (package.DeleteItem)
                //{
                //    MessageBox.Show("This Item is Fixed , You Can Not Remove this Item ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //    return;
                //}


                //for (int i = 0; i < package.PackageItemOptionList.Count; i++)
                //{
                //    aRecipeOptionMdList.Remove(package.PackageItemOptionList[i]);

                //}

                //double optionPrice = aRecipeOptionMdList.Sum(a => a.InPrice);

                //RecipePackageButton packageButton = (RecipePackageButton)aMainFormView.flowLayoutPanel1.Controls[1];
                //lblPrice.Text = "£" + (optionPrice + packageButton.InPrice).ToString("F");


                //aPackageItemList.Remove(package);

                //packagegridView.DeleteRow(e.RowHandle);

                //  lblPrice.Text = TotalPrice.ToString("C2");


            }
            else if (e.Column.FieldName == "Minus")
            {
                string price = gridViewOption.GetFocusedRowCellValue("Price").ToString();

                int Qty = Convert.ToInt32(gridViewOption.GetFocusedRowCellValue("Qty"));
                if (Qty > 1)
                {
                    if (price == string.Empty)
                    {
                        gridViewOption.SetFocusedRowCellValue("Qty", Qty - 1); return;

                    }
                    decimal Orgprice = Convert.ToDecimal(gridViewOption.GetFocusedRowCellValue("OrgPrice"));
                    gridViewOption.SetFocusedRowCellValue("Qty", Qty - 1);
                    gridViewOption.SetFocusedRowCellValue("Price", ((Qty - 1) * Orgprice).ToString("n2"));

                }


            }

        }
        private void OptionAdd(object sender, TileItemEventArgs e)
        {
            try
            {
                CartItem OptionItem = sender as CartItem;

                if (OptionItem.Checked)
                {

                    for (int i = 0; i < gridViewOption.DataRowCount; i++)
                    {
                        var ExistCheckOptionId = optionDataRow.Table.Rows[i].Field<string>("OptionId");

                        if (ExistCheckOptionId == OptionItem.RecipeOptionItemButton.RecipeOptionItemId.ToString())
                        {

                            optionDataRow.Table.Rows[i].Delete();

                            OptionItem.Checked = false;

                            return;

                        }
                    }

                }
                else
                {
                    OptionItem.Checked = true;

                }



                string type = OptionItem.RecipeOptionItemButton.RecipeOptionButton.Type;
                int limit = OptionItem.RecipeOptionItemButton.RecipeOptionButton.ItemLImit;

                var selectTabpageControlCount = tabControl.SelectedTabPage.Controls;

                //  int limit = 3;

                if (type == "multiple")
                {

                    gridViewOption.Columns["Group"].GroupIndex = 0;
                    int rowCount = 0;
                    for (int i = 0; i < gridViewOption.DataRowCount; i++)
                    {
                        var groupRowCount = gridViewOption.GetGroupRowValue(i, gridViewOption.Columns["Group"]);
                        if (groupRowCount.ToString() == tabControl.SelectedTabPage.Text)
                        {
                            int rowQty = Convert.ToInt32(gridViewOption.GetGroupRowValue(i, gridViewOption.Columns["Qty"]));
                            rowCount = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == tabControl.SelectedTabPage.Text)
                                   .Sum(a => Convert.ToInt32(a["Qty"]));

                            if (rowCount >= limit || rowCount >= OptionItem.Index)
                            {

                                OptionItem.Checked = false;
                                return;
                            }
                        }
                    }


                    optionDataRow = optionItemDataTable.NewRow();
                    optionDataRow[0] = optionItemDataTable.Rows.Count + 1;
                    optionDataRow[1] = OptionItem.RecipeOptionItemButton.Title;
                    optionDataRow[2] = tabControl.SelectedTabPage.Text;
                    if (aGeneralInformation.TableId > 0)
                    {
                        if (OptionItem.RecipeOptionItemButton.InPrice > 0)
                        {
                            optionDataRow[3] =
                                Convert.ToDecimal(OptionItem.RecipeOptionItemButton.InPrice).ToString("C2").Substring(1);
                            optionDataRow["OrgPrice"] =
                                Convert.ToDecimal(OptionItem.RecipeOptionItemButton.InPrice).ToString("C2").Substring(1);

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;

                        }
                    }
                    else
                    {
                        if (OptionItem.RecipeOptionItemButton.Price > 0)
                        {
                            optionDataRow[3] = Convert.ToDecimal(OptionItem.RecipeOptionItemButton.Price).ToString("C2").Substring(1);
                            optionDataRow["OrgPrice"] = Convert.ToDecimal(OptionItem.RecipeOptionItemButton.Price).ToString("C2").Substring(1);

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;

                        }
                    }
                  


                    




                    optionDataRow[4] = OptionItem.RecipeOptionItemId;
                    optionDataRow["Qty"] = 1;
                    optionDataRow["CartItem"] = OptionItem;
                    optionItemDataTable.Rows.Add(optionDataRow);

                    // rowCount = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == tabControl.SelectedTabPage.Text).Sum(a => Convert.ToInt32(a["Qty"]));
                    if (rowCount == limit - 1 || rowCount == OptionItem.Index - 1)
                    {

                        if (tabControl.TabPages.Count - 1 != tabControl.SelectedTabPageIndex)
                        {
                            tabControl.TabPages[tabControl.SelectedTabPageIndex + 1].PageEnabled = true;
                            tabControl.SelectedTabPage = tabControl.TabPages[tabControl.SelectedTabPageIndex + 1];

                            if (tabControl.TabPages.Count - 1 == tabControl.SelectedTabPageIndex)
                            {
                                btnAddToCard.Visible = true;
                                btnNextPrevious.Text = "PREVIOUS";
                            }
                            else
                            {
                                btnAddToCard.Visible = false;
                                btnNextPrevious.Text = "NEXT";
                            }
                        }
                        else
                        {
                            TempOptionListAdd(OptionItem.MenuType, false);
                        }

                        return;
                    }

                }
                else if (type == "single")
                {


                    gridViewOption.Columns["Group"].GroupIndex = 0;
                    int rowCount = 0;
                    for (int i = 0; i < gridViewOption.DataRowCount; i++)
                    {
                        var groupRowCount = gridViewOption.GetGroupRowValue(i, gridViewOption.Columns["Group"]);
                        if (groupRowCount.ToString() == tabControl.SelectedTabPage.Text)
                        {

                            rowCount = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == tabControl.SelectedTabPage.Text)
                                    .Sum(a => Convert.ToInt32(a["Qty"]));
                            if (rowCount == 1)
                            {

                                OptionItem.Checked = false;

                                return;
                            }
                        }
                    }


                    optionDataRow = optionItemDataTable.NewRow();
                    optionDataRow[0] = optionItemDataTable.Rows.Count + 1;
                    optionDataRow[1] = OptionItem.RecipeOptionItemButton.Title;
                    optionDataRow[2] = tabControl.SelectedTabPage.Text;

                    if (aGeneralInformation.TableId > 0)
                    {
                        if (OptionItem.RecipeOptionItemButton.InPrice > 0)
                        {
                            optionDataRow[3] =
                                Convert.ToDecimal(OptionItem.RecipeOptionItemButton.InPrice).ToString("C2").Substring(1);
                            optionDataRow["OrgPrice"] =
                                Convert.ToDecimal(OptionItem.RecipeOptionItemButton.InPrice).ToString("C2").Substring(1);

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;

                        }
                    }
                    else
                    {
                        if (OptionItem.RecipeOptionItemButton.Price > 0)
                        {
                            optionDataRow[3] = Convert.ToDecimal(OptionItem.RecipeOptionItemButton.Price).ToString("C2").Substring(1);
                            optionDataRow["OrgPrice"] = Convert.ToDecimal(OptionItem.RecipeOptionItemButton.Price).ToString("C2").Substring(1);

                        }
                        else{
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;

                        }
                    }

                    optionDataRow[4] = OptionItem.RecipeOptionItemId;
                    optionDataRow["Qty"] = 1;
                    optionDataRow["CartItem"] = OptionItem;

                    optionItemDataTable.Rows.Add(optionDataRow);
                    try
                    {


                        if (tabControl.TabPages.Count - 1 != tabControl.SelectedTabPageIndex)
                        {
                            tabControl.TabPages[tabControl.SelectedTabPageIndex + 1].PageEnabled = true;
                            tabControl.SelectedTabPage = tabControl.TabPages[tabControl.SelectedTabPageIndex + 1];

                            if (tabControl.TabPages.Count - 1 == tabControl.SelectedTabPageIndex)
                            {
                                btnAddToCard.Visible = true;
                                btnNextPrevious.Text = "PREVIOUS";

                            }
                            else
                            {
                                btnAddToCard.Visible = false;
                                btnNextPrevious.Text = "NEXT";

                            }

                        }
                        else
                        {
                            btnAddToCard.Visible = true;
                            btnNextPrevious.Visible = false;

                            TempOptionListAdd(OptionItem.MenuType, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());

                    }


                }

                gridControlOption.DataSource = optionItemDataTable;
                gridViewOption.ExpandAllGroups();


            }
            catch (Exception ex)
            {

                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }



        }
        private void ItemOptionLoad(List<RecipeOptionButton> itemButtons, ReceipeMenuItemButton aMenuItemButton, string MenuType, bool IsUpdate)
        {

            ReceipeMenuItemButton topButton = new ReceipeMenuItemButton();
            topButton.Text = aMenuItemButton.ItemName;
            topButton.CategoryId = aMenuItemButton.CategoryId;
            topButton.RecipeTypeId = aMenuItemButton.RecipeMenuItemId;
            topButton.Font = aMenuItemButton.Font;
            topButton.AutoSize = true;
            topButton.MinimumSize = new Size(100, pannelTopBar.Height - 2);
            topButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            topButton.BackColor = aMenuItemButton.BackColor;
            topButton.ForeColor = Color.White;
            topButton.FlatStyle = FlatStyle.Flat;
            topButton.Margin = new Padding(0);
            topButton.Padding = new Padding(0);
            if (MenuType == "Type")
            {
                flowLayoutPanel1.Controls.Add(topButton);

                // topButton.Click -= TypeButtonOnClick;
                //  topButton.Click += TypeButtonOnClick;
            }
            else
            {

                topButton.Click += (sender, args) =>
                {
                    Control exitsButton = (Control)flowLayoutPanel1.Controls[0];
                    CartItem item = new CartItem();

                    item.Recipeid = topButton.RecipeTypeId;
                    item.CategoryId = topButton.CategoryId;
                    item.Appearance.Font = exitsButton.Font;
                    item.Text = exitsButton.Text;
                    //  CategoryLoad(item, dynamicTableLayoutPanel);
                };
                flowLayoutPanel1.Controls.Add(topButton);
            }



            TileControl optionControl;
            TileGroup tileGroup;


            // dynamicTableLayoutPanel = new CustomFlowLayoutPanel();
            tileGroup1.Items.Clear();
            FooterControl.Controls.Clear();
            tileControl1.Controls.Clear();
            tabControl.Controls.Clear();
            tabControl.TabPages.Clear();


            foreach (var group in itemButtons)
            {
                optionControl = new TileControl
                {
                    Dock = DockStyle.Fill,
                    AllowSelectedItem = false,
                    AllowDrag = false,
                    ItemCheckMode = TileItemCheckMode.None,
                    ItemAppearanceSelected = { ForeColor = Color.White },
                    HorizontalContentAlignment = HorzAlignment.Near,
                    Orientation = Orientation.Vertical,
                    VerticalContentAlignment = VertAlignment.Top,
                    ItemPadding = new Padding(0)
                };
                optionControl.ColumnCount = 10;
                optionControl.ItemPadding = new Padding(1);
                optionControl.ItemSize = optionControl.Width / 4;
                optionControl.IndentBetweenGroups = 8;
                tileGroup = new TileGroup();
                // To be Checked

                if (group.PlusMinus == 1)
                {
                    TileGroup ToptileGroup = new TileGroup();
                    optionControl.Groups.Add(ToptileGroup);
                    CartItem optionItem = new CartItem();
                    optionItem.ItemSize = TileItemSize.Wide;
                    optionItem.ReceipeMenuItemButton = aMenuItemButton;
                    optionItem.AppearanceItem.Selected.Font = group.Font;
                    optionItem.Appearance.BorderColor = Color.Transparent;
                    optionItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    optionItem.Appearance.BackColor = Color.CornflowerBlue;
                    optionItem.Appearance.Font = group.Font;
                    optionItem.Text = "No";

                    // optionItem.ItemClick -= new TileItemClickEventHandler(OptionAdd);
                    //optionItem.ItemClick += new TileItemClickEventHandler(OptionAdd);
                    optionItem.ItemClick += (sender, args) =>
                    {

                        var tileGroupName = tabControl.TabPages[tabControl.SelectedTabPageIndex].Controls;
                        TileControl tileControl = (TileControl)tileGroupName[0];

                        if (optionItem.Checked == false)
                        {


                            if (tileControl.Groups.Count > 0)
                            {
                                var tileItem = tileControl.Groups[1].Items;
                                for (int i = 0; i < tileItem.Count; i++)
                                {
                                    CartItem OptionItem = (CartItem)tileItem[i];

                                    if (!OptionItem.Checked)
                                    {
                                        if (OptionItem.RecipeOptionItemButton.Title.Contains("No"))
                                        {
                                            OptionItem.RecipeOptionItemButton.Title = OptionItem.RecipeOptionItemButton.Title.ToString().Remove(0, 2);
                                            OptionItem.Text = OptionItem.Text.Remove(0, 2);
                                        }
                                        OptionItem.Text = optionItem.Text + " " + OptionItem.Text;
                                        OptionItem.RecipeOptionItemButton.Title = "No " + OptionItem.RecipeOptionItemButton.Title;
                                        OptionItem.RecipeOptionItemButton.Price = 0.0;
                                        OptionItem.RecipeOptionItemButton.InPrice = 0.0;
                                        OptionItem.IsNooption = true;
                                        OptionItem.Checked = false;
                                    }


                                    //  string html = "<span style='color:red;'>1/2 Bf Burg</span></br>&rarr;No ";

                                }
                            }


                            //   var  items = tabControl.SelectedTabPage.Controls[0].Controls;

                            optionItem.Appearance.BackColor = Color.Black;

                            optionItem.Checked = true;
                        }
                        else if (optionItem.Checked)
                        {
                            if (tileControl.Groups.Count > 0)
                            {
                                var tileItem = tileControl.Groups[1].Items;
                                for (int i = 0; i < tileItem.Count; i++)
                                {
                                    CartItem OptionItem = (CartItem)tileItem[i];
                                    if (!OptionItem.Checked)
                                    {
                                        if (OptionItem.Text.Contains("No"))
                                        {
                                            OptionItem.Text = OptionItem.Text.Remove(0, 2).Trim();
                                            OptionItem.RecipeOptionItemButton.Title = OptionItem.RecipeOptionItemButton.Title.Remove(0, 2).Trim();

                                            OptionItem.RecipeOptionItemButton.Price = OptionItem.Price;
                                            OptionItem.RecipeOptionItemButton.InPrice = OptionItem.InPrice;
                                            OptionItem.Checked = false;
                                            OptionItem.IsNooption = false;
                                        }

                                    }
                                }
                            }
                            optionItem.Appearance.BackColor = Color.CornflowerBlue;

                            optionItem.Checked = false;

                        }

                    };
                    ToptileGroup.Items.Add(optionItem);
                }

                optionControl.Groups.Add(tileGroup);
                var RecipeOptionItemButton_List = new RestaurantMenuBLL().GetRecipeOptionItemByOptionId(group);
                foreach (RecipeOptionItemButton recipeOptionItemButton in RecipeOptionItemButton_List)
                {

                    CartItem optionItem = new CartItem();
                    optionItem.ItemSize = TileItemSize.Wide;
                    optionItem.ReceipeMenuItemButton = aMenuItemButton;
                    optionItem.AppearanceItem.Selected.Font = recipeOptionItemButton.Font;
                    optionItem.Appearance.BorderColor = Color.Transparent;
                    optionItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    optionItem.Appearance.BackColor = recipeOptionItemButton.BackColor;
                    optionItem.Appearance.Font = recipeOptionItemButton.Font;
                    optionItem.Text = recipeOptionItemButton.Title;
                    optionItem.RecipeOptionItemButton = recipeOptionItemButton;
                    string mixText = "<span style='color:red;'>" + topButton.Text + "</span></br>&rarr;" + recipeOptionItemButton.Title;
                    optionItem.Title = mixText;
                    optionItem.RecipeOptionItemId = recipeOptionItemButton.RecipeOptionItemId;
                    optionItem.RecipeOptionId = recipeOptionItemButton.RecipeOptionId;
                    optionItem.Index = RecipeOptionItemButton_List.Count;
                    optionItem.InPrice = recipeOptionItemButton.InPrice;
                    optionItem.Price = recipeOptionItemButton.Price;

                    optionItem.ItemClick -= new TileItemClickEventHandler(OptionAdd);
                    optionItem.ItemClick += new TileItemClickEventHandler(OptionAdd);
                    tileGroup.Items.Add(optionItem);

                }

                //tabControl.Controls.Add(optionControl);


                var tabPage = tabControl.TabPages.Add(group.Title);

                tabPage.PageEnabled = false;
                tileGroup.Name = tabPage.Text;

                tabPage.Controls.Add(optionControl);


            }

            var windowSize = Screen.PrimaryScreen.Bounds.Height - 200;
            tabControl.Dock = DockStyle.Fill;
            tabControl.TabPages[0].PageEnabled = true;

            tabControl.SelectedPageChanged += (sender, args) =>
            {
                if (tabControl.SelectedTabPageIndex == tabControl.TabPages.Count - 1)
                {
                    btnAddToCard.Visible = true;
                    btnNextPrevious.Text = "PREVIOUS";
                }
                else if (tabControl.SelectedTabPageIndex == 0)
                {
                    btnNextPrevious.Text = "NEXT";
                    btnNextPrevious.BackColor = Color.Coral;
                    btnAddToCard.Visible = false;
                }


            };

            PanelControl panelControl = new PanelControl { Dock = DockStyle.Top, Height = windowSize };
            optionItemDataTable = new DataTable();

            optionItemDataTable.Columns.Add(new DataColumn("Index"));
            optionItemDataTable.Columns.Add(new DataColumn("OptionName"));
            optionItemDataTable.Columns.Add(new DataColumn("Group"));
            optionItemDataTable.Columns.Add(new DataColumn("Price"));
            optionItemDataTable.Columns.Add(new DataColumn("OptionId"));
            optionItemDataTable.Columns.Add(new DataColumn("Qty"));
            optionItemDataTable.Columns.Add(new DataColumn("OrgPrice"));
            optionItemDataTable.Columns.Add(new DataColumn("CartItem", typeof(CartItem)));



            OptionGridView();


            panelControl.Controls.Add(gridControlOption);
            panelControl.Controls.Add(tabControl);
            tileGroup1.Control.AddControl(panelControl);


            indexClick = 0;
            foreach (Button button in NextPreviousButtonAdd(tabControl.TabPages.Count, MenuType, IsUpdate))
            {


                FooterControl.Controls.Add(button);


            }
            if (tabControl.TabPages.Count == 1)
            {

                FooterControl.Controls[1].Visible = true;
                //FooterControl.Controls[2].Visible = false;
            }


            tileGroup1.Control.AddControl(FooterControl);

        }
        public List<Button> NextPreviousButtonAdd(int tabPageCount, string MenuType, bool IsUpdate)
        {
            List<Button> ListOfButton = new List<Button>();



            btnNextPrevious = new Button
            {
                Text = "NEXT",
                Dock = DockStyle.Top,
                Name = "btnPreviousNext",
                FlatStyle = FlatStyle.Flat,
                Width = 300,
                Height = 50,
                BackColor = Color.Coral,
                FlatAppearance = { BorderColor = Color.AliceBlue, BorderSize = 1 },
                Font = new Font("Arial", 10),

                ForeColor = Color.White
            };
            btnAddToCard = new Button
            {
                Text = "FINISH",
                Dock = DockStyle.Top,
                Name = "Finish",
                FlatStyle = FlatStyle.Flat,
                Width = 300,
                Height = 50,
                BackColor = Color.LimeGreen,
                Visible = false,
                Font = new Font("Arial", 10),
                FlatAppearance = { BorderColor = Color.AliceBlue, BorderSize = 1 },
                ForeColor = Color.White
            };
            btnNextPrevious.Click += new EventHandler((sender, args) =>
            {
                try
                {
                    int currentSelectedTab = tabControl.SelectedTabPageIndex;





                    if (btnNextPrevious.Text == "NEXT")
                    {
                        tabControl.TabPages[currentSelectedTab + 1].PageEnabled = true;
                        tabControl.SelectedTabPageIndex = currentSelectedTab + 1;

                        if (tabControl.TabPages.Count - 1 == tabControl.SelectedTabPageIndex)
                        {
                            btnAddToCard.Visible = true;
                            btnNextPrevious.Text = "PREVIOUS";


                        }
                        else
                        {
                            btnAddToCard.Visible = false;
                        }
                    }
                    else if (btnNextPrevious.Text == "PREVIOUS")
                    {
                        tabControl.SelectedTabPageIndex = currentSelectedTab - 1;

                        if (tabControl.SelectedTabPageIndex == 0)
                        {
                            btnAddToCard.Visible = false;
                            btnNextPrevious.Text = "NEXT";
                        }
                        else
                        {
                            btnAddToCard.Visible = false;

                        }
                    }





                }
                catch (Exception)
                {


                }




            });

            btnAddToCard.Click += new EventHandler((sender, args) =>
            {
                try
                {
                    TempOptionListAdd(MenuType, IsUpdate);
                    this.Close();
                }
                catch (Exception)
                {


                }


            });

            ListOfButton.Add(btnNextPrevious);
            ListOfButton.Add(btnAddToCard);


            return ListOfButton;
        }
        public void TempOptionListAdd(string MenuType, bool IsUpdate)
        {

            packageForm.aRecipeList.Clear();
            for (int i = 0; i < gridViewOption.DataRowCount; i++)
            {
                // var option= gridViewOption.GetRowCellValue(i, "CartItem");
                CartItem optionItem = (CartItem)gridViewOption.GetRowCellValue(i, "CartItem");

                if (optionItem.RecipeOptionItemButton.Price>0.0)
                {
                    double optionPrice = Convert.ToDouble(gridViewOption.GetRowCellValue(i, "Price"));
                    optionItem.RecipeOptionItemButton.Price = optionPrice;
                }
                optionItem.RecipeOptionItemButton.IsNooption = optionItem.IsNooption;
                packageForm.aRecipeList.Add(optionItem.RecipeOptionItemButton);

                // PackageItemFormLoadNew.aRecipeOptionMdList.Add(optionItem.RecipeOptionItemButton.RecipeOptionButton);
            }

        }

        void GridView_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            GridView gridView = sender as GridView;

            var groupColumnCellValue = gridView.GetGroupRowValue(e.RowHandle, info.Column);
            string groupColumnCellValueStr = Convert.ToString(groupColumnCellValue);

            info.GroupText = gridView.GetGroupSummaryText(e.RowHandle) + ": " + groupColumnCellValueStr;
            //info.GroupText = info.Column.Caption + ": " + groupColumnCellValueStr + " " + gridView.GetGroupSummaryText(e.RowHandle);
        }

        private GridView OptionGridView()
        {

            gridViewOption = new GridView();


            gridViewOption.OptionsView.ShowGroupPanel = false;
            gridViewOption.OptionsView.ShowGroupExpandCollapseButtons = true;
            gridViewOption.OptionsView.ShowColumnHeaders = false;
            gridViewOption.OptionsView.ShowIndicator = false;
            Font font = new Font("Tahoma", 10);
            gridViewOption.OptionsBehavior.Editable = false;
            gridViewOption.Appearance.Row.Font = font;
            gridViewOption.RowHeight = 30;
            GridColumn Plus = new GridColumn() { FieldName = "Plus", ColumnEdit = packageForm.aMainFormView.repositoryItemHyperLinkEdit1, Caption = "Plus", Visible = true, Width = 5 };
            GridColumn Count = new GridColumn() { FieldName = "Qty", Caption = "Qty", Width = 3, Visible = true };
            GridColumn OptionName = new GridColumn() { FieldName = "OptionName", Caption = "OptionName", Visible = true };
            GridColumn Index = new GridColumn() { FieldName = "Index", Caption = "Name", Visible = false };
            GridColumn OptionId = new GridColumn() { FieldName = "OptionId", Caption = "OptionId", Visible = false };
            GridColumn Group = new GridColumn() { FieldName = "Group", Caption = "Group", Visible = true, GroupIndex = 0 };
            GridColumn Price = new GridColumn() { FieldName = "Price", Caption = "Price", Visible = true, Width = 20 };
            GridColumn Minus = new GridColumn() { FieldName = "Minus", ColumnEdit = packageForm.aMainFormView.repositoryMinus, Caption = "Minus", Visible = true, Width = 10 };
            GridColumn OrgPrice = new GridColumn() { FieldName = "OrgPrice", Caption = "OrgPrice", Visible = false, Width = 10 };
            GridColumn CartItem = new GridColumn() { FieldName = "CartItem", Caption = "Minus", Visible = false, Width = 10 };

            gridViewOption.Columns.Add(Minus);

            gridViewOption.Columns.Add(Count);
            gridViewOption.Columns.Add(Index);
            gridViewOption.Columns.Add(OptionName);
            gridViewOption.Columns.Add(Index);
            gridViewOption.Columns.Add(Group);
            gridViewOption.Columns.Add(Price);
            gridViewOption.Columns.Add(OptionId);
            gridViewOption.Columns.Add(Plus);
            gridViewOption.Columns.Add(OrgPrice);
            gridViewOption.Columns.Add(CartItem);

            gridViewOption.CustomDrawGroupRow -= GridView_CustomDrawGroupRow;
            gridViewOption.CustomDrawGroupRow += GridView_CustomDrawGroupRow;
            gridViewOption.RowCellClick -= optionRow_RowCellClick;
            gridViewOption.RowCellClick += optionRow_RowCellClick;
            gridViewOption.ExpandAllGroups();

            gridControlOption.MainView = gridViewOption;
            optionDataRow = null;
            gridControlOption.DataSource = optionDataRow;
            //gridViewOption.Columns["Index"].Visible = false;
            //gridViewOption.Columns["Price"].Width = 20;
            //gridViewOption.Columns["OptionId"].Visible = false;
            //gridViewOption.ExpandAllGroups();





            gridViewOption.GroupCount = 1;
            gridViewOption.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            item1.FieldName = "Group";
            item1.SummaryType = DevExpress.Data.SummaryItemType.Count;
            item1.DisplayFormat = "{0}";
            gridViewOption.GroupSummary.Add(item1);
            return gridViewOption;
        }
        private int indexClick = 0;
        private Button btnAddToCard;
        private Button btnNextPrevious;




        private void AllOptionViewForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
