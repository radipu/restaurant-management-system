namespace TomaFoodRestaurant.OtherForm
{
    partial class ResetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.resetButton = new System.Windows.Forms.Button();
            this.paawordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(220)))));
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.resetButton.Location = new System.Drawing.Point(373, 356);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(171, 36);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset all Orders";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // paawordTextBox
            // 
            this.paawordTextBox.Font = new System.Drawing.Font("Arial Narrow", 14.25F);
            this.paawordTextBox.Location = new System.Drawing.Point(145, 34);
            this.paawordTextBox.Name = "paawordTextBox";
            this.paawordTextBox.PasswordChar = '*';
            this.paawordTextBox.Size = new System.Drawing.Size(252, 29);
            this.paawordTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter admin password\r\nto reset orders.";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Red;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cancelButton.Location = new System.Drawing.Point(14, 356);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 36);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.resetButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.paawordTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 418);
            this.panel1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button10);
            this.panel2.Controls.Add(this.button11);
            this.panel2.Controls.Add(this.button12);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(319, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 264);
            this.panel2.TabIndex = 7;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Black;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button7.Location = new System.Drawing.Point(81, 197);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(133, 56);
            this.button7.TabIndex = 11;
            this.button7.Text = "CLEAR";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Black;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button9.Location = new System.Drawing.Point(12, 197);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(63, 56);
            this.button9.TabIndex = 9;
            this.button9.Text = "0";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Black;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button10.Location = new System.Drawing.Point(151, 11);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(63, 56);
            this.button10.TabIndex = 8;
            this.button10.Text = "9";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Black;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button11.Location = new System.Drawing.Point(81, 11);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(63, 56);
            this.button11.TabIndex = 7;
            this.button11.Text = "8";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Black;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button12.Location = new System.Drawing.Point(12, 11);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(63, 56);
            this.button12.TabIndex = 6;
            this.button12.Text = "7";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Black;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.Location = new System.Drawing.Point(81, 73);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(63, 56);
            this.button5.TabIndex = 4;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(151, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 56);
            this.button4.TabIndex = 5;
            this.button4.Text = "6";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Black;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Location = new System.Drawing.Point(12, 73);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(63, 56);
            this.button6.TabIndex = 3;
            this.button6.Text = "4";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(151, 135);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 56);
            this.button3.TabIndex = 2;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(81, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 56);
            this.button2.TabIndex = 1;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.numberButton_click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(12, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.numberButton_click);
            // 
            // ResetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(591, 445);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResetForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox paawordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}