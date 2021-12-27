using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.KeyboardButton
{
    public partial class KeyButton : InputTextButton
    {
        private TextBox controlToInputText;
        private bool removeLastChar = false;

        private string defaultStateText;
        private string changeStateText;
        private bool showDefaultState = true;



        public KeyButton()
        {
            //Font font = new Font("Arial", 14);
            //base.BackgroundImage = RMSUI.Properties.Resources.inp_button_img_up_1;
            //base.BackgroundImageLayout = ImageLayout.Tile;
            //base.Width = 50;
            //base.Height = 50;
            //base.BgImageOnMouseDown = RMSUI.Properties.Resources.inp_btn_img_down_1;
            //base.BgImageOnMouseUp = RMSUI.Properties.Resources.inp_button_img_up_1;
            //base.ForeColorOnMouseDown = Color.White;
            //base.FlatAppearance.BorderSize = 1;
            //base.Font = font;
            //base.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (controlToInputText != null)
            {

                if (removeLastChar)
                {

                    if (controlToInputText.Text != "")
                    {
                        string stRemove = controlToInputText.Text.ToString();

                        string aa = stRemove.Remove(stRemove.Length - 1, 1);

                        controlToInputText.Text = aa;

                    }

                    else
                    {
                        controlToInputText.Text = "";

                    }
                }

                else
                {
                    if (this.Text == "Space")
                    {
                        controlToInputText.AppendText(" ");
                    }
                    else if (this.Text == "ENTER")
                    {
                        controlToInputText.AppendText("\r\n");
                    }

                    else controlToInputText.AppendText(this.Text);
                }

            }



        }

        public new TextBox ControlToInputText
        {
            get { return controlToInputText; }
            set { controlToInputText = value; }
        }
        
        public  new bool RemoveLastChar
        {
            get { return removeLastChar; }
            set { removeLastChar = value; }
        }

        public   string DefaultStateText
        {
            get { return defaultStateText; }
            set
            {
                defaultStateText = value;
                Text = value;

            }

        }



        public string ChangeStateText
        {
            get { return this.changeStateText; }
            set
            {
                changeStateText = value;


            }

        }

        public void AlterState()
        {
            if (showDefaultState)
            {
                showDefaultState = false;
                Text = changeStateText;
            }
            else
            {
                showDefaultState = true;
                Text = defaultStateText;
            }


        }

    }
}
