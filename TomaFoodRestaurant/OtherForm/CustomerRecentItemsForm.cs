using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CustomerRecentItemsForm : Form
    {
        private int customerId = 0;
        public static  List<ReceipeMenuItemButton> aReceipeMenuItemButtons = new List<ReceipeMenuItemButton>();
        public CustomerRecentItemsForm(int id)
        {
            InitializeComponent();
            customerId = id;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            aReceipeMenuItemButtons=new List<ReceipeMenuItemButton>();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CustomerRecentItemsForm_Load(object sender, EventArgs e)
        {
            LoadRecetAddedItems();
        }


        private void LoadRecetAddedItems()
        {
            RestaurantMenuBLL aRestaurantMenuBll=new RestaurantMenuBLL();
           CustomerRecentItemBLL aCustomerRecentItemBll=new CustomerRecentItemBLL();
            List<CustomerRecentItemMD> aCustomerRecentItemMds =
                aCustomerRecentItemBll.GetCustomerRecentItemMd(customerId);
            if (aCustomerRecentItemMds.Any())
            {
                foreach (CustomerRecentItemMD itemMd in aCustomerRecentItemMds)
                {
                    if (itemMd.recipe_id > 0)
                    {
                       
                        ReceipeMenuItemButton aMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(itemMd.recipe_id);
                        ReceipeCategoryButton aReceipeCategoryButton =aRestaurantMenuBll.GetCategoryByCategoryId(aMenuItemButton.CategoryId);
                        aMenuItemButton.Text = aMenuItemButton.ItemName;
                        aMenuItemButton.Width = 120;
                        aMenuItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
                        aMenuItemButton.ForeColor = Color.White;
                        aMenuItemButton.Height = 50;
                        aMenuItemButton.FlatStyle = FlatStyle.Flat;
                        aMenuItemButton.FlatAppearance.BorderSize = 0;
                        aMenuItemButton.Click += new EventHandler(ReceipeMenuItemButton_Click);
                        aMenuItemButton.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                        itemsflowLayoutPanel.Controls.Add(aMenuItemButton);
                    }
                    else if (false)
                    {
                        RecipePackageButton aRecipePackageButton = aRestaurantMenuBll.GetPackageByPackageId(itemMd.package_id);
                        aRecipePackageButton.Text = aRecipePackageButton.PackageName;
                        aRecipePackageButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
                        aRecipePackageButton.ForeColor = Color.White;
                        aRecipePackageButton.Height = 50;
                        aRecipePackageButton.Width = 120;
                        aRecipePackageButton.FlatStyle = FlatStyle.Flat;
                        aRecipePackageButton.FlatAppearance.BorderSize = 0;
                        aRecipePackageButton.Click += new EventHandler(aRecipePackageButton_Click);
                        itemsflowLayoutPanel.Controls.Add(aRecipePackageButton);
                    }
                }
           
            }
        }

        private List<RecipeOptionItemButton> GetAllItemWithOption(ReceipeMenuItemButton aReceipeMenuItemButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            int itemIndex = 0;
            bool flag = aRestaurantMenuBll.GetReceipeOptionsByItemId(aReceipeMenuItemButton.RecipeMenuItemId);
            ItemOptionForm.Status = "";
            ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();

            if (flag)
            {
                ItemOptionForm.Status = "";
                ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                ItemOptionForm aItemOptionForm = new ItemOptionForm(aReceipeMenuItemButton.RecipeMenuItemId);
                aItemOptionForm.ShowDialog();

            }
            if ((ItemOptionForm.Status == "cancel" || ItemOptionForm.Status == "") && flag) return new List<RecipeOptionItemButton>();


            List<RecipeOptionItemButton> aRecipeList = ItemOptionForm.aRecipeList;

            return aRecipeList;
        }

        private void aRecipePackageButton_Click(object sender, EventArgs e)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            RecipePackageButton button = sender as RecipePackageButton;
            if (button.BackColor == Color.Black)
            {
                button.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
            }
            else
            {
                button.BackColor = Color.Black;
            }
        }

        private void ReceipeMenuItemButton_Click(object sen5, EventArgs e)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            ReceipeMenuItemButton button = sen5 as ReceipeMenuItemButton;
            if (button.BackColor == Color.Black)
            {
                button.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
            }
            else
            {
            List<RecipeOptionItemButton> aRecipeList=GetAllItemWithOption(button);
                
                button.BackColor = Color.Black;
                button.aRecipeOptionItemButtons = aRecipeList;
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {

            aReceipeMenuItemButtons=new List<ReceipeMenuItemButton>();
            List<RecipePackageButton> aRecipePackages = new List<RecipePackageButton>();
            foreach (ReceipeMenuItemButton menu in itemsflowLayoutPanel.Controls.OfType<ReceipeMenuItemButton>())
            {
                if (menu.BackColor == Color.Black)
                {
                    aReceipeMenuItemButtons.Add(menu);
                }
            
            }
            foreach (RecipePackageButton package in itemsflowLayoutPanel.Controls.OfType<RecipePackageButton>())
            {
                if (package.BackColor == Color.Black)
                {
                    aRecipePackages.Add(package);
                }

            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
