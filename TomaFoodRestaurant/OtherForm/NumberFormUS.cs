using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class NumberFormUS :UserControl
    {
        private TextBox textbox;
        public NumberFormUS()
        {
            InitializeComponent();
        }

        public TextBox ControlToInputText
        {
            get { return this.textbox; }
            set
            {
                this.textbox = value;
                kbn_1.ControlToInputText = this.textbox;
                kbn_2.ControlToInputText = this.textbox;
                kbn_3.ControlToInputText = this.textbox;
                kbn_4.ControlToInputText = this.textbox;
                kbn_5.ControlToInputText = this.textbox;
                kbn_6.ControlToInputText = this.textbox;
                kbn_7.ControlToInputText = this.textbox;
                kbn_8.ControlToInputText = this.textbox;
                kbn_9.ControlToInputText = this.textbox;
                kbn_0.ControlToInputText = this.textbox;
                kbk_dot.ControlToInputText = this.textbox;
                bck_space.ControlToInputText = this.textbox;
                kbn_00.ControlToInputText = this.textbox;

                


            }
        }

        public TextBox textBox { get; set; }

    }
}
