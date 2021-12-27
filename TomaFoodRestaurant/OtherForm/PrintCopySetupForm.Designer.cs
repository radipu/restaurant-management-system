namespace TomaFoodRestaurant.OtherForm
{
    partial class PrintCopySetupForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.takeawayTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.savebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.collectionTextBox = new System.Windows.Forms.TextBox();
            this.tableCopyTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableCopyTextBox);
            this.groupBox1.Controls.Add(this.collectionTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.takeawayTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.savebutton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(191, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 252);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Print Copy Setup";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(114, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 22);
            this.label5.TabIndex = 10;
            this.label5.Text = "Table:";
            // 
            // takeawayTextBox
            // 
            this.takeawayTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.takeawayTextBox.Location = new System.Drawing.Point(183, 19);
            this.takeawayTextBox.Name = "takeawayTextBox";
            this.takeawayTextBox.Size = new System.Drawing.Size(339, 29);
            this.takeawayTextBox.TabIndex = 6;
            this.takeawayTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Takeaway:";
            // 
            // savebutton
            // 
            this.savebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.savebutton.FlatAppearance.BorderSize = 0;
            this.savebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savebutton.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebutton.Location = new System.Drawing.Point(425, 158);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(97, 46);
            this.savebutton.TabIndex = 5;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = false;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Collection:";
            // 
            // collectionTextBox
            // 
            this.collectionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collectionTextBox.Location = new System.Drawing.Point(183, 58);
            this.collectionTextBox.Name = "collectionTextBox";
            this.collectionTextBox.Size = new System.Drawing.Size(339, 29);
            this.collectionTextBox.TabIndex = 11;
            this.collectionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // tableCopyTextBox
            // 
            this.tableCopyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableCopyTextBox.Location = new System.Drawing.Point(183, 100);
            this.tableCopyTextBox.Name = "tableCopyTextBox";
            this.tableCopyTextBox.Size = new System.Drawing.Size(339, 29);
            this.tableCopyTextBox.TabIndex = 12;
            this.tableCopyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // PrintCopySetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Maiandra GD", 8.25F);
            this.Name = "PrintCopySetupForm";
            this.Size = new System.Drawing.Size(950, 474);
            this.Load += new System.EventHandler(this.PrintCopySetupForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tableCopyTextBox;
        private System.Windows.Forms.TextBox collectionTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox takeawayTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Label label2;
    }
}
