using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DevExpress.Data.Linq;
using DevExpress.Utils;
using DevExpress.Utils.OAuth.Provider;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit.API.Word;
using Newtonsoft.Json.Linq;
//using RestSharp;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
#region Change Price Keyboard
using Application = System.Windows.Forms.Application;
#endregion Change Price Keyboard
namespace TomaFoodRestaurant.OtherForm
{
    public partial class PriceChangeForm : Form
    {
        List<ReceipeCategoryButton> getAllCatgory = new RestaurantMenuBLL().GetAllCategory().ToList();
        List<ReceipeMenuItemButton> getallPrice = new RestaurantMenuBLL().AllRecipeButton().ToList();

        #region Change Price Keyboard
        OthersMethod aOthersMethod = new OthersMethod();
        GlobalUrl urls = new GlobalUrl();
        #endregion Change Price Keyboard
        public PriceChangeForm()
        {
            InitializeComponent();
        }
        
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            this.Close();
        }

        private void PriceChangeForm_Load(object sender, EventArgs e)
        {

            comboBoxCategory.DataSource = getAllCatgory;

            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryId";
            comboBoxCategory.SelectedIndex = 0;
           
            //cmbCategory.Properties.DataSource = getAllCatgory;
            //cmbCategory.Properties.DisplayMember = "CategoryName";
            //cmbCategory.Properties.ValueMember = "CategoryId";
            //cmbCategory.Properties.View.OptionsView.ShowIndicator = false;
            //cmbCategory.Properties.View.OptionsView.ShowColumnHeaders = false;

            //cmbCategory.EditValue = getAllCatgory[0].CategoryName; 
          //  cmbCategory.SelectedText = getAllCatgory[0].CategoryName;
            RestaurantInformation information=new RestaurantInformationBLL().GetRestaurantInformation();
            if (information.RestaurantType == "takeaway")
            {
                gridColumn2.Visible = false;
            }
            gridView2.OptionsSelection.MultiSelect = true;
            gridView2.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
             
            gridControl1.DataSource = getallPrice.Where(a => a.CategoryId == Convert.ToInt32(getAllCatgory[0].CategoryId)).OrderBy(a => a.ItemName).ToList();
        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            ReceipeCategoryButton btn = comboBoxCategory.SelectedItem as ReceipeCategoryButton;
            gridControl1.DataSource = getallPrice.Where(a => a.CategoryId == Convert.ToInt32(btn.CategoryId)).OrderBy(a => a.ItemName).ToList();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            HyperLinkEdit edit = (HyperLinkEdit) sender;
            ReceipeMenuItemButton editMenuPrice=new ReceipeMenuItemButton();
            editMenuPrice.InPrice = (double)gridView2.GetFocusedRowCellValue("InPrice");
            editMenuPrice.OutPrice = (double)gridView2.GetFocusedRowCellValue("OutPrice");
            editMenuPrice.ReceiptName = (string)gridView2.GetFocusedRowCellValue("ReceiptName");
            editMenuPrice.RecipeMenuItemId = Convert.ToInt32(edit.Text);
            bool message=new RestaurantMenuBLL().UpdatePrice(editMenuPrice);
            if (message)
            {
                MessageBox.Show("Price updated.");
            }
            else
            {
                MessageBox.Show("Price update failed.");
            }        
        }

        GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
      
        RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
        CustomerBLL aCustomerBll = new CustomerBLL();

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var jsonSerialiser = new JavaScriptSerializer();

            string jsonFormat = "";
            string multiple = "";
            List<ReceipeItemChangePrice> allList=new List<ReceipeItemChangePrice>();
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    ReceipeMenuItemButton editMenuPrice = new ReceipeMenuItemButton();
                    editMenuPrice.InPrice = (double)gridView2.GetRowCellValue(i, "InPrice");
                    editMenuPrice.OutPrice = (double)gridView2.GetRowCellValue(i, "OutPrice");
                    editMenuPrice.ReceiptName = (string)gridView2.GetRowCellValue(i, "ReceiptName");
                    editMenuPrice.RecipeMenuItemId = Convert.ToInt32(gridView2.GetRowCellValue(i, "RecipeMenuItemId"));

                    bool message = new RestaurantMenuBLL().UpdatePrice(editMenuPrice);
                    if (message)
                    {
                        allList.Add(new ReceipeItemChangePrice { restaurant_id = GlobalSetting.RestaurantInformation.Id, recipe_id = editMenuPrice.RecipeMenuItemId, recipe_name = editMenuPrice.ReceiptName, in_price = editMenuPrice.InPrice, out_price = editMenuPrice.OutPrice });
                    }

                }
                if (OthersMethod.CheckForInternetConnection())
                {
                    var json = "recipeItems=" + jsonSerialiser.Serialize(allList);

                    string url = aGlobalUrlBll.GetUrls().AcceptUrl + "restaurantcontrol/request/tpos_changeprice";
                    var result = new OrderSyncroniseBLL().NewUpdateMenu(json, url);

                    if (result != "0")
                    {
                        MessageBox.Show("Successfully changed the recipe details.");
                    }
                }
            }
            else {
                MessageBox.Show("Please select an item for update.");
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridView2.SelectRow(e.RowHandle);
            if (e.Column.FieldName == "InPrice")
            {
                double orgInprice = (double)gridView2.GetFocusedRowCellValue("InPrice");
                if (orgInprice.Equals(e.Value)){
                   gridView2.SelectRow(e.RowHandle);
                }
            }
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    gridView2.SelectRow(i);
                }
            }

            #region Checkbox Selection

            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (hi.InRowCell)
                {
                    view.FocusedRowHandle = hi.RowHandle;
                    view.FocusedColumn = hi.Column;
                    view.ShowEditor();
                    CheckEdit edit = (view.ActiveEditor as CheckEdit);
                    if (edit != null)
                    {
                        edit.Toggle();
                        (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                    }
                }
            }
            #endregion Checkbox Selection

            //      gridView2.SelectRow(e.RowHandle);
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    gridView2.SelectRow(i);
                }
            }
            gridView2.SelectRow(e.RowHandle);
        }

        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    gridView2.SelectRow(i);
                }
            }

            // Row edit = (Row)sender;
            //    //gridView2.SelectRow(edit.Index);
            //
         }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    gridView2.SelectRow(i);
                }
            }
            gridView2.SelectRow(e.FocusedRowHandle);
        }

        private void gridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            #region Checkbox NumberForm Keyboard

            if (e.Column.FieldName == "InPrice" || e.Column.FieldName == "OutPrice")
            {
                urls = aGlobalUrlBll.GetUrls();
                try
                {
                    aOthersMethod.KeyBoardClose();
                    if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
                    {
                        Point aPoint = new Point(75, 300);
                        NumberForm aNumberForm = new NumberForm(aPoint);
                        aNumberForm.Show();
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            #endregion Checkbox NumberForm Keyboard

            #region Checkbox NumberPad Keyboard

            if (e.Column.FieldName == "ReceiptName")
            {
                urls = aGlobalUrlBll.GetUrls();
                try
                {
                    aOthersMethod.NumberPadClose();
                    if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                    {
                        Point aPoint = new Point(0, 600);
                        NumberPad aNumberPad = new NumberPad(aPoint);
                        aNumberPad.Show();
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            #endregion Checkbox NumberPad Keyboard
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;

                return Params;
            }
        }
    }
}