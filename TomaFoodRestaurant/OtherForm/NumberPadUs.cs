using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class NumberPadUs : UserControl
    {
        private TextBox textbox;
        public NumberPadUs()
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

                bck_space.ControlToInputText = this.textbox;

                kbk_q.ControlToInputText = this.textbox;
                kbk_w.ControlToInputText = this.textbox;
                kbk_e.ControlToInputText = this.textbox;
                kbk_r.ControlToInputText = this.textbox;
                kbk_t.ControlToInputText = this.textbox;
                kbk_y.ControlToInputText = this.textbox;
                kbk_u.ControlToInputText = this.textbox;
                kbk_i.ControlToInputText = this.textbox;
                kbk_o.ControlToInputText = this.textbox;
                kbk_p.ControlToInputText = this.textbox;

                kbk_a.ControlToInputText = this.textbox;
                kbk_s.ControlToInputText = this.textbox;
                kbk_d.ControlToInputText = this.textbox;
                kbk_f.ControlToInputText = this.textbox;
                kbk_g.ControlToInputText = this.textbox;
                kbk_h.ControlToInputText = this.textbox;
                kbk_j.ControlToInputText = this.textbox;
                kbk_k.ControlToInputText = this.textbox;
                kbk_l.ControlToInputText = this.textbox;

                kbk_z.ControlToInputText = this.textbox;
                kbk_x.ControlToInputText = this.textbox;
                kbk_c.ControlToInputText = this.textbox;
                kbk_v.ControlToInputText = this.textbox;
                kbk_b.ControlToInputText = this.textbox;
                kbk_n.ControlToInputText = this.textbox;
                kbk_m.ControlToInputText = this.textbox;
                kbk_comma.ControlToInputText = this.textbox;
                kbk_dot.ControlToInputText = this.textbox;
                space.ControlToInputText = this.textbox;
                enter.ControlToInputText = this.textbox;


                kbk_at.ControlToInputText = this.textbox;
                kbk_highpen.ControlToInputText = this.textbox;
                kbk_underscore.ControlToInputText = this.textbox;
                kbk_slash.ControlToInputText = this.textbox;
                kbk_pound.ControlToInputText = this.textbox;
                kbk_dotagain.ControlToInputText = this.textbox;

                kbk_bst.ControlToInputText = this.textbox;
                kbk_bed.ControlToInputText = this.textbox;
                kbk_del.ControlToInputText = this.textbox;


            }
        }

        public TextBox textBox { get; set; }

        private void rmsToogleButton1_Click(object sender, EventArgs e)
        {
            kbk_a.AlterState();
            kbk_b.AlterState();
            kbk_c.AlterState();
            kbk_d.AlterState();
            kbk_e.AlterState();
            kbk_f.AlterState();
            kbk_g.AlterState();
            kbk_h.AlterState();
            kbk_i.AlterState();
            kbk_j.AlterState();
            kbk_k.AlterState();
            kbk_l.AlterState();
            kbk_m.AlterState();
            kbk_n.AlterState();
            kbk_o.AlterState();
            kbk_p.AlterState();
            kbk_q.AlterState();
            kbk_r.AlterState();
            kbk_s.AlterState();
            kbk_t.AlterState();
            kbk_u.AlterState();
            kbk_v.AlterState();
            kbk_w.AlterState();
            kbk_x.AlterState();
            kbk_y.AlterState();
            kbk_z.AlterState();

            kbn_0.AlterState();
            kbn_1.AlterState();
            kbn_2.AlterState();
            kbn_3.AlterState();
            kbn_4.AlterState();
            kbn_5.AlterState();
            kbn_6.AlterState();
            kbn_7.AlterState();
            kbn_8.AlterState();
            kbn_9.AlterState();

            kbk_comma.AlterState();
            kbk_dot.AlterState();


            kbk_at.AlterState();
            kbk_highpen.AlterState();
            kbk_underscore.AlterState();
            kbk_slash.AlterState();
            kbk_pound.AlterState();
            kbk_dotagain.AlterState();

            kbk_bst.AlterState();
            kbk_bed.AlterState();
        }
    }
}
