namespace TomaFoodRestaurant.OtherForm
{
    partial class DeliveryMaplocationview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.inputTextButton1 = new TomaFoodRestaurant.KeyboardButton.InputTextButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1161, 640);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.inputTextButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 640);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1161, 52);
            this.panel1.TabIndex = 2;
            // 
            // inputTextButton1
            // 
            this.inputTextButton1.BackColor = System.Drawing.Color.OrangeRed;
            this.inputTextButton1.BgImageOnMouseDown = null;
            this.inputTextButton1.BgImageOnMouseUp = null;
            this.inputTextButton1.ClearField = true;
            this.inputTextButton1.ControlToInputText = null;
            this.inputTextButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputTextButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.inputTextButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inputTextButton1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextButton1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.inputTextButton1.ForeColorOnMouseDown = System.Drawing.Color.White;
            this.inputTextButton1.ForeColorOnMouseUp = System.Drawing.Color.White;
            this.inputTextButton1.Location = new System.Drawing.Point(0, 0);
            this.inputTextButton1.Name = "inputTextButton1";
            this.inputTextButton1.RemoveLastChar = false;
            this.inputTextButton1.Size = new System.Drawing.Size(1161, 52);
            this.inputTextButton1.TabIndex = 0;
            this.inputTextButton1.Text = "CLOSE";
            this.inputTextButton1.UseVisualStyleBackColor = false;
            this.inputTextButton1.Click += new System.EventHandler(this.inputTextButton1_Click);
            // 
            // DeliveryMaplocationview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 692);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DeliveryMaplocationview";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DeliveryMaplocationview_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KeyboardButton.InputTextButton inputTextButton1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}
