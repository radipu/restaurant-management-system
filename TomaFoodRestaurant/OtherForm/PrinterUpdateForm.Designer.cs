namespace TomaFoodRestaurant.OtherForm
{
    partial class PrinterUpdateForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.printCopyTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.printerAddressTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.updatebutton = new System.Windows.Forms.Button();
            this.printerNamecomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.printCopyTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.printerAddressTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.updatebutton);
            this.groupBox1.Controls.Add(this.printerNamecomboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(68, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 235);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Printer";
            // 
            // printCopyTextBox
            // 
            this.printCopyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printCopyTextBox.Location = new System.Drawing.Point(183, 112);
            this.printCopyTextBox.Name = "printCopyTextBox";
            this.printCopyTextBox.Size = new System.Drawing.Size(339, 29);
            this.printCopyTextBox.TabIndex = 8;
            this.printCopyTextBox.Text = "1";
            this.printCopyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.printCopyTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(71, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Print Copy:";
            // 
            // printerAddressTextBox
            // 
            this.printerAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printerAddressTextBox.Location = new System.Drawing.Point(183, 18);
            this.printerAddressTextBox.Name = "printerAddressTextBox";
            this.printerAddressTextBox.Size = new System.Drawing.Size(339, 29);
            this.printerAddressTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Printer Address:";
            // 
            // updatebutton
            // 
            this.updatebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatebutton.Location = new System.Drawing.Point(425, 163);
            this.updatebutton.Name = "updatebutton";
            this.updatebutton.Size = new System.Drawing.Size(97, 43);
            this.updatebutton.TabIndex = 5;
            this.updatebutton.Text = "Update";
            this.updatebutton.UseVisualStyleBackColor = true;
            this.updatebutton.Click += new System.EventHandler(this.updatebutton_Click);
            // 
            // printerNamecomboBox
            // 
            this.printerNamecomboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printerNamecomboBox.FormattingEnabled = true;
            this.printerNamecomboBox.Location = new System.Drawing.Point(182, 66);
            this.printerNamecomboBox.Name = "printerNamecomboBox";
            this.printerNamecomboBox.Size = new System.Drawing.Size(340, 32);
            this.printerNamecomboBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Printer Name:";
            // 
            // PrinterUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 427);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrinterUpdateForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Printer Update Form";
            this.Load += new System.EventHandler(this.PrinterUpdateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox printCopyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox printerAddressTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button updatebutton;
        private System.Windows.Forms.ComboBox printerNamecomboBox;
        private System.Windows.Forms.Label label2;
    }
}