using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.KeyboardButton
{
    public class InputTextButton : RMSGenericButton
    {

        private TextBox controlToInputText;
        private bool removeLastChar = false;
        private bool clearFiled = false;

        public InputTextButton()
        {
            base.FlatAppearance.BorderSize = 0;
            base.Width = 50;
            base.Height = 50;

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (controlToInputText != null)
            {
                if (clearFiled)
                {
                    controlToInputText.Clear();
                }
                else if (removeLastChar)
                {
                    //MessageBox.Show("dfdffddf");
                    //controlToInputText.Text= "\b";

                    //controlToInputText.Text.Remove(controlToInputText.Text.Length - 1, 1).ToString();
                }
                else
                {
                    controlToInputText.AppendText(this.Text);
                }

            }
        }

        public TextBox ControlToInputText
        {
            get { return this.controlToInputText; }
            set { this.controlToInputText = value; }

        }
        public bool ClearField
        {
            get { return this.clearFiled; }
            set { this.clearFiled = value; }
        }
        public bool RemoveLastChar
        {
            get { return this.removeLastChar; }
            set { this.removeLastChar = value; }
        }

    }
}
