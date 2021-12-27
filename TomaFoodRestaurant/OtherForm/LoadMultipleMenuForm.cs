using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class LoadMultipleMenuForm : Form
    {
        private bool isquater = false;
        GlobalUrl urls = new GlobalUrl();
        ReceipeCategoryButton hasSubcategory=new ReceipeCategoryButton();
        public static List<OrderItemDetailsMD> aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
        public static List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static string status = "";
        public LoadMultipleMenuForm(int categoryId)
        {
            InitializeComponent();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
           
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            urls = aGlobalUrlBll.GetUrls();
            hasSubcategory = aRestaurantMenuBll.GetCategoryByCategoryId(categoryId);
            List<ReceipeSubCategoryButton> allSubcategoryButton1 = aRestaurantMenuBll.GetAllSubcategory();
            GetSubCategory(allSubcategoryButton1, hasSubcategory);
          

        }

 

        private void LoadMultipleMenuForm_Load(object sender, EventArgs e)
        {
            packageNamelabel1.Text = (hasSubcategory.CategoryName).ToUpper();
            LoadMultipleTypeBUtton();
        }

        private void LoadMultipleTypeBUtton()
        {
            if (isquater)
            {
                quaterRoundButton.BackgroundImage = Properties.Resources.quarter;
                halfRoundButton.BackgroundImage = Properties.Resources.half_grey;
            }
            else
            {
                quaterRoundButton.BackgroundImage = Properties.Resources.Quarter_grey;
                halfRoundButton.BackgroundImage = Properties.Resources.half;
            }
        }
        private void halfRoundButton_Click(object sender, EventArgs e)
        {
            if (aOrderItemDetailsMDList.Count > 2)
            {
                return;
            }
            isquater = false;
            LoadMultipleTypeBUtton();
        }

        private void quaterRoundButton_Click(object sender, EventArgs e)
        {
            isquater = true;
            LoadMultipleTypeBUtton();
        }

        private void GetSubCategory(List<ReceipeSubCategoryButton> allSubcategoryButton, ReceipeCategoryButton categoryButton)
        {
       
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            List<ReceipeSubCategoryButton> tempSubcategory = allSubcategoryButton.Where(a => a.RecipeTypeId == categoryButton.ReceipeTypeId).ToList();
            tempSubcategory = tempSubcategory.OrderBy(a => a.SubCategoryName).ToList();
            tempSubcategory = tempSubcategory.OrderBy(a => a.SortOrder).ToList();
            tempSubcategory = tempSubcategory.Where(a=>a.SubCategoryId>0).OrderBy(b => b.ButtonColor).ToList();
            foreach (ReceipeSubCategoryButton aReceipeSubCategoryButton in tempSubcategory)
            {
                aReceipeSubCategoryButton.AutoSize = true;
                string font_size = urls.fontSize;
                aReceipeSubCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                aReceipeSubCategoryButton.Click -= new EventHandler(ReceipeSubCategoryButton_Click);
                aReceipeSubCategoryButton.Click += new EventHandler(ReceipeSubCategoryButton_Click);
                aReceipeSubCategoryButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(aReceipeSubCategoryButton.ButtonColor));
                if (aReceipeSubCategoryButton.Height < 30)
                {
                    aReceipeSubCategoryButton.Height = 50;
                }
         
                itemsFlowLayoutPanel.Controls.Add(aReceipeSubCategoryButton);
            }
        }


        private void ReceipeSubCategoryButton_Click(object sen4, EventArgs e)
        {

            RestaurantMenuBLL aRestaurantMenuBll=new RestaurantMenuBLL();
            ReceipeSubCategoryButton aReceipeSubCategoryButton = sen4 as ReceipeSubCategoryButton;
            ReceipeMenuItemButton aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByCategoryAndSubcategory(hasSubcategory.CategoryId, aReceipeSubCategoryButton.SubCategoryId);
         
            if (aReceipeMenuItemButton == null || aReceipeMenuItemButton.RecipeMenuItemId <= 0)
            {
                MessageBox.Show("Item not found");
                return;
            }
            else aReceipeMenuItemButton.RecipeTypeId = aReceipeSubCategoryButton.RecipeTypeId;

            int itemIndex = GetAllItemWithOption(aReceipeMenuItemButton);
            LoadItemDetails();
        }
        private int CheckDulicate(OrderItemDetailsMD aOrderItemDetails, List<RecipeOptionItemButton> aRecipeList)
        {
            List<OrderItemDetailsMD> itemDetails = aOrderItemDetailsMDList.Where(a => a.ItemId == aOrderItemDetails.ItemId).ToList();
            if (itemDetails.Count == 0)
            {

                return 0;
            }

            return 1;

       
        }


        private int GetOptionIndex()
        {
            int index = 0;
            index = aOrderItemDetailsMDList.Count == 0 ? 0 : aOrderItemDetailsMDList.Max(a => a.OptionsIndex);
            int index2 = 0;
            if (index > index2) return index;
            else return index2;
        }

        private int GetAllItemWithOption(ReceipeMenuItemButton aReceipeMenuItemButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll=new RestaurantMenuBLL();
            int itemIndex = 0;
            bool  flag = aRestaurantMenuBll.GetReceipeOptionsByItemId(aReceipeMenuItemButton.RecipeMenuItemId);

            if (ExistsLimit())
            {
                MessageBox.Show("You have already selected all items for the menu");
                return 2;
            }


            ItemOptionForm.Status = "";
            ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();

            if (flag)
            {
                ItemOptionForm.Status = "";
                ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                ItemOptionForm aItemOptionForm = new ItemOptionForm(aReceipeMenuItemButton.RecipeMenuItemId);
                aItemOptionForm.ShowDialog();

            }
            if ((ItemOptionForm.Status == "cancel" || ItemOptionForm.Status == "") && flag) return 0;





            List<RecipeOptionItemButton> aRecipeList = ItemOptionForm.aRecipeList;
            int optionIndex = GetOptionIndex();

            OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();
            aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
            aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
            aOrderItemDetails.ItemName = aReceipeMenuItemButton.ReceiptName;
            aOrderItemDetails.OptionsIndex = optionIndex + 1;
            aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
            aOrderItemDetails.Price = aReceipeMenuItemButton.InPrice;
            aOrderItemDetails.Qty = 1;
            aOrderItemDetails.RecipeTypeId = aReceipeMenuItemButton.RecipeTypeId;
            aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
            aOrderItemDetails.TableNumber = 1;

            int index = CheckDulicate(aOrderItemDetails, aRecipeList);

            if (index > 0)
            {
                MessageBox.Show("You already added the item");
                return 0;
            }
            else if (index <= 0)
            {


                itemIndex = optionIndex + 1;
                aOrderItemDetailsMDList.Add(aOrderItemDetails);
                foreach (RecipeOptionItemButton recipe in aRecipeList)
                {

                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aReceipeMenuItemButton.RecipeMenuItemId;
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
                    aOptionMD.OptionsIndex = optionIndex + 1;
                    aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                    aRecipeOptionMdList.Add(aOptionMD);
                }

            }
            return itemIndex;


        }

        private bool ExistsLimit()
        {
            int itemLimit = 0;
            if (isquater)
            {
                itemLimit = 4;
            }
            else
            {
                itemLimit = 2;
            }

            int qty = aOrderItemDetailsMDList.Sum(a => a.Qty);
            if (qty >= itemLimit) return true;
            return false;
        }

        private void LoadItemDetails()
        {
            if (aOrderItemDetailsMDList.Count > 2)
            {
                halfRoundButton.Enabled = false;
            }

            itemDetailsFlowLayoutPanel1.Controls.Clear();
            string res = "";
            foreach (OrderItemDetailsMD item in aOrderItemDetailsMDList)
            {

                Label aLabel = new Label();
                aLabel.AutoSize = true;
                aLabel.Text += item.Qty + " X ";
                aLabel.Text += item.ItemName + "  ";
                if (item.Price > 0)
                {
                    //aLabel.Text += (item.Qty * item.Price);
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
                        if (list.InPrice > 0)
                        {
                            aLabel.Text += "\r\n  " + (list.Title);
                        }
                        else aLabel.Text += "\r\n  " + (list.Title);
                    }
                }
                itemDetailsFlowLayoutPanel1.Controls.Add(aLabel);
                res += aLabel + "\r\n";

            }
            buttonPanel.Location = new Point(itemDetailsFlowLayoutPanel1.Location.X, itemDetailsFlowLayoutPanel1.Size.Height + itemDetailsFlowLayoutPanel1.Location.Y);
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            status = "cancel";
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int itemLimit = isquater ? 4 : 2;
            if (aOrderItemDetailsMDList.Count < itemLimit)
            {
                MessageBox.Show("Please select correct number of items");
                return;
            }
            status = "ok";
            this.Close();
        }
    }
}
