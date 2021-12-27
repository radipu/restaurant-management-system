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
    public partial class ItemOptionForm : Form
    {
        GlobalUrl urls = new GlobalUrl(); 

        int itemId;
        public static List<RecipeOptionItemButton> aRecipeList = new List<RecipeOptionItemButton>();
        public static string Status = "";
        List<OptionItemSelect> aOptionItemSelectList = new List<OptionItemSelect>();
        public ItemOptionForm(int id)
        {
            InitializeComponent();
            itemId = id;
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            ReceipeMenuItemButton item = aRestaurantMenuBll.GetRecipeByItemId(id);
            label1.Text =("Options for "+ item.ReceiptName).ToUpper();
        }

        private void ItemOptionForm_Load(object sender, EventArgs e)
        {

            RestaurantMenuBLL aRestaurantMenuBll=new RestaurantMenuBLL();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            int cnt1 = 0;
            int cnt = 0; int cntMinius = 0;

            List<RecipeOptionButton> aRecipeOptionButtonList = aRestaurantMenuBll.GetRecipeOptionWhenItemClick(itemId);
            int count = aRecipeOptionButtonList.Count;
            int height = 0,nheight = 0;
            foreach (RecipeOptionButton recipeButton in aRecipeOptionButtonList)
                {
                    List<RecipeOptionItemButton> aRecipeOptionItemButtons = aRestaurantMenuBll.GetRecipeOptionItemByOptionId(recipeButton);
                    List<RecipeOptionItemButton> aRecipeOptionItemButtons1 = aRestaurantMenuBll.GetRecipeOptionItemByOptionId(recipeButton);
                    Font newFont = new Font(Font.FontFamily, 20);
                
                    Label aLabel = new Label();
                    aLabel.Font = newFont;
                    aLabel.Text =recipeButton.Title;
                    aLabel.AutoSize = false;
                    aLabel.Height = 35;
                    aLabel.ForeColor = Color.Brown;

                    aLabel.Width = attributeFlowLayoutPanel.Size.Width - 10;
                 
                    attributeFlowLayoutPanel.Controls.Add(aLabel);
                   // int cnt = aRecipeOptionItemButtons.Count;
                    height += 60;

                    OptionItemSelect aOptionItemSelect = new OptionItemSelect();
                    aOptionItemSelect.RecipeOptionId = recipeButton.RecipeOptionId;
                    if (recipeButton.Type == "single")
                    {
                        aOptionItemSelect.RecipeSelectLimit = 1;
                    }
                    else
                    {
                        aOptionItemSelect.RecipeSelectLimit = recipeButton.ItemLImit;
                    }
                    aOptionItemSelect.RecipeSelect = 0;
                    aOptionItemSelect.RecipeSelect1 = 0;
                    aOptionItemSelectList.Add(aOptionItemSelect);
                    foreach (RecipeOptionItemButton aRecipeOptionItemButton in aRecipeOptionItemButtons)
                    { 
                        aRecipeOptionItemButton.AutoSize = true;
                        aRecipeOptionItemButton.Click += new EventHandler(RecipeOptionItemButton_Click);
                        attributeFlowLayoutPanel.Controls.Add(aRecipeOptionItemButton);
                        if (cnt++ % 5 == 0)
                            height += 60;
                    }

                 
                    if (recipeButton.PlusMinus == 1)
                    {
                        Label tempLabel = new Label();
                        tempLabel.Font = newFont;
                        tempLabel.Text = "No " + recipeButton.Title;
                        tempLabel.AutoSize = false;
                        tempLabel.Height = 35;
                        tempLabel.ForeColor = Color.Brown;
                        tempLabel.Width = noflowLayoutPanel1.Size.Width - 10;
                        noflowLayoutPanel1.Controls.Add(tempLabel);
                        cnt1 += 1;
                        nheight += 60;
                        if (cnt1 == 0) {
                            nheight = 60;
                        }
                        cntMinius = 1;
                    }

                    foreach (RecipeOptionItemButton button in aRecipeOptionItemButtons1)
                    {
                        button.AutoSize = true;
                        button.Click += new EventHandler(RecipeOptionItemButtonNo_Click);
                        if (recipeButton.PlusMinus == 1)
                        { 
                            button.MinusTitle = button.Title;
                            noflowLayoutPanel1.Controls.Add(button);
                            if (cnt1++ % 5 == 0)
                                nheight += 60;
                        }
                    }
            }            
            
            noflowLayoutPanel1.Height =nheight;
            attributeFlowLayoutPanel.Height = height;
            noflowLayoutPanel1.Location = new Point(noflowLayoutPanel1.Location.X, attributeFlowLayoutPanel.Size.Height + attributeFlowLayoutPanel.Location.Y);
            noflowLayoutPanel1.Visible = true;

            //noflowLayoutPanel1.Controls.Add(addButton);
            //noflowLayoutPanel1.Controls.Add(cancelButton);

            if (cntMinius > 0)
            {
                minusOptionButton.Visible = true;
                noflowLayoutPanel1.Visible = false;
                //CheckOptionViewAndMinusView();

            }
            else {
                minusOptionButton.Visible = false;
                //addButton.Location = new Point(300, noflowLayoutPanel1.Size.Height + noflowLayoutPanel1.Location.Y);
                //cancelButton.Location = new Point(500, noflowLayoutPanel1.Size.Height + noflowLayoutPanel1.Location.Y);
            }
        }

        private void CheckOptionViewAndMinusView()
        {
            if (minusOptionButton.BackColor != Color.Black)
            {
                attributeFlowLayoutPanel.Visible = true;
                noflowLayoutPanel1.Visible = false;

                //buttonPanel.Location = new Point(buttonPanel.Location.X, attributeFlowLayoutPanel.Size.Height + attributeFlowLayoutPanel.Location.Y);
            }
            else
            {
                attributeFlowLayoutPanel.Visible = false;
                noflowLayoutPanel1.Visible = true;
                noflowLayoutPanel1.Location = new Point(34, 99);
                //buttonPanel.Location = new Point(buttonPanel.Location.X, noflowLayoutPanel1.Size.Height + noflowLayoutPanel1.Location.Y);
            }
        }   

        private void RecipeOptionItemButton_Click(object sen11, EventArgs e)
                                                                                                          {
           
            RecipeOptionItemButton aButton = sen11 as RecipeOptionItemButton;
            if (aButton.BackColor == Color.Black)
            {
                aButton.BackColor = ColorTranslator.FromHtml("#967adc");
                UpdateLimit(aButton, -1);
                LoadAllAddedOptions(aButton, false, aButton.Text);
            }
            else if (CheckLimit(aButton))
            {
                LoadAllAddedOptions(aButton,true, aButton.Text);
                aButton.BackColor = Color.Black;
                UpdateLimit(aButton, 1);
            }
          

        }

        private void LoadAllAddedOptions(RecipeOptionItemButton aButton, bool status, string buttonText)
        {

            if (!status)
            {
                foreach ( RecipeOptionItemButton1 button in selectedOptionsFlowLayoutPanel.Controls.OfType<RecipeOptionItemButton1>())
                {
                    if (button.Text == buttonText)
                    {
                        selectedOptionsFlowLayoutPanel.Controls.Remove(button);
                        return;
                    }
                }

            }
            else
            {

                RecipeOptionItemButton1 aRecipeOptionItemButton1 = new RecipeOptionItemButton1();
                aRecipeOptionItemButton1.RecipeOptionItemButton = aButton;
                aRecipeOptionItemButton1.AutoSize = true;
                aRecipeOptionItemButton1.Height = 45;
                aRecipeOptionItemButton1.BackColor = ColorTranslator.FromHtml("#967adc");
                aRecipeOptionItemButton1.FlatStyle = FlatStyle.Flat;
                aRecipeOptionItemButton1.FlatAppearance.BorderSize = 0;
                aRecipeOptionItemButton1.ForeColor = Color.White;
                aRecipeOptionItemButton1.Text = buttonText;
                aRecipeOptionItemButton1.Name = aButton.Title;
                selectedOptionsFlowLayoutPanel.Controls.Add(aRecipeOptionItemButton1);
            }
        }
        
        private void RecipeOptionItemButtonNo_Click(object sen11, EventArgs e)
        {
            RecipeOptionItemButton aButton = sen11 as RecipeOptionItemButton;
            string buttonText = "No " + aButton.Text;
            if (aButton.BackColor == Color.Black)
            {
                aButton.BackColor = ColorTranslator.FromHtml("#967adc");
                UpdateLimit1(aButton, -1);
                LoadAllAddedOptions(aButton, false, buttonText);
            }
            else
            {
             
                LoadAllAddedOptions(aButton, true, buttonText);
                aButton.BackColor = Color.Black;
                UpdateLimit1(aButton, 1);
            }
        }

        private bool CheckLimit(RecipeOptionItemButton aButton)
        {
            OptionItemSelect tempOptionItemSelect = aOptionItemSelectList.FirstOrDefault(a => a.RecipeOptionId == aButton.RecipeOptionId);
         
            if (tempOptionItemSelect.RecipeSelectLimit <= tempOptionItemSelect.RecipeSelect)
            {

                if (tempOptionItemSelect.RecipeSelectLimit ==1)
                {
               
                            foreach (RecipeOptionItemButton1 button in selectedOptionsFlowLayoutPanel.Controls.OfType<RecipeOptionItemButton1>())
                            {
                                selectedOptionsFlowLayoutPanel.Controls.Remove(button);                       
                            }
                            foreach (RecipeOptionItemButton tempButton in attributeFlowLayoutPanel.Controls.OfType<RecipeOptionItemButton>())
                            {
                                if (tempButton.BackColor == Color.Black)
                                {
                                    tempButton.BackColor = ColorTranslator.FromHtml("#967adc");
                                }

                            }

                            foreach (RecipeOptionItemButton tempButton in noflowLayoutPanel1.Controls.OfType<RecipeOptionItemButton>())
                            {
                                if (tempButton.BackColor == Color.Black)
                                {
                                    tempButton.BackColor = ColorTranslator.FromHtml("#967adc");
                                }

                            }
                            return true;
                        }

               return false;
            }
            return true;
        }

        private void UpdateLimit(RecipeOptionItemButton aButton, int qty)
        {
            OptionItemSelect tempOptionItemSelect = aOptionItemSelectList.FirstOrDefault
                (a => a.RecipeOptionId == aButton.RecipeOptionId);
                   tempOptionItemSelect.RecipeSelect += qty;
        }

        private bool CheckLimit1(RecipeOptionItemButton aButton)
        {
            OptionItemSelect tempOptionItemSelect = aOptionItemSelectList.FirstOrDefault
                (a => a.RecipeOptionId == aButton.RecipeOptionId);
            if (tempOptionItemSelect.RecipeSelectLimit <= tempOptionItemSelect.RecipeSelect1)
                return false;            
            return true;
        }

        private void UpdateLimit1(RecipeOptionItemButton aButton, int qty)
        {
            OptionItemSelect tempOptionItemSelect = aOptionItemSelectList.FirstOrDefault
                (a => a.RecipeOptionId == aButton.RecipeOptionId);
            tempOptionItemSelect.RecipeSelect1 += qty;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Status = "ok";
            foreach (RecipeOptionItemButton tempButton in attributeFlowLayoutPanel.Controls.OfType<RecipeOptionItemButton>())
            {
                if (tempButton.BackColor == Color.Black)
                {
                    aRecipeList.Add(tempButton);
                }
            }
            foreach (RecipeOptionItemButton tempButton in noflowLayoutPanel1.Controls.OfType<RecipeOptionItemButton>())
            {
                if(tempButton.BackColor == Color.Black)
                {
                    aRecipeList.Add(tempButton);
                }
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Status = "cancel";
            this.Close();
        }

        private void minusOptionButton_Click(object sender, EventArgs e)
        {
            if (minusOptionButton.BackColor == Color.Black)
            {
                minusOptionButton.BackColor = Color.FromArgb(59,175,218);
            }
            else
            {
                minusOptionButton.BackColor = Color.Black;
            }
            CheckOptionViewAndMinusView();
        }
    }
}
