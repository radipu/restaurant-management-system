namespace TomaFoodRestaurant.OtherForm
{
    partial class CallerDetailsShow
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
            this.deliveryButton = new System.Windows.Forms.Button();
            this.fullAddressTextBox = new System.Windows.Forms.TextBox();
            this.collectionButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deliveryButton
            // 
            this.deliveryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.deliveryButton.FlatAppearance.BorderSize = 0;
            this.deliveryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deliveryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deliveryButton.ForeColor = System.Drawing.Color.Transparent;
            this.deliveryButton.Location = new System.Drawing.Point(168, 202);
            this.deliveryButton.Margin = new System.Windows.Forms.Padding(0);
            this.deliveryButton.Name = "deliveryButton";
            this.deliveryButton.Size = new System.Drawing.Size(132, 54);
            this.deliveryButton.TabIndex = 32;
            this.deliveryButton.Text = "Delivery";
            this.deliveryButton.UseVisualStyleBackColor = false;
            this.deliveryButton.Click += new System.EventHandler(this.deliveryButton_Click);
            // 
            // fullAddressTextBox
            // 
            this.fullAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.fullAddressTextBox.Location = new System.Drawing.Point(12, 36);
            this.fullAddressTextBox.Multiline = true;
            this.fullAddressTextBox.Name = "fullAddressTextBox";
            this.fullAddressTextBox.ReadOnly = true;
            this.fullAddressTextBox.Size = new System.Drawing.Size(269, 138);
            this.fullAddressTextBox.TabIndex = 31;
            // 
            // collectionButton
            // 
            this.collectionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.collectionButton.FlatAppearance.BorderSize = 0;
            this.collectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.collectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collectionButton.ForeColor = System.Drawing.Color.Transparent;
            this.collectionButton.Location = new System.Drawing.Point(321, 202);
            this.collectionButton.Margin = new System.Windows.Forms.Padding(0);
            this.collectionButton.Name = "collectionButton";
            this.collectionButton.Size = new System.Drawing.Size(132, 54);
            this.collectionButton.TabIndex = 30;
            this.collectionButton.Text = "Collection";
            this.collectionButton.UseVisualStyleBackColor = false;
            this.collectionButton.Click += new System.EventHandler(this.collectionButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(87)))), ((int)(((byte)(63)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(12, 202);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 54);
            this.button1.TabIndex = 33;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.ForeColor = System.Drawing.Color.Transparent;
            this.editButton.Location = new System.Drawing.Point(293, 81);
            this.editButton.Margin = new System.Windows.Forms.Padding(0);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(132, 54);
            this.editButton.TabIndex = 34;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // CallerDetailsShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 344);
            this.ControlBox = false;
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deliveryButton);
            this.Controls.Add(this.fullAddressTextBox);
            this.Controls.Add(this.collectionButton);
            this.Name = "CallerDetailsShow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CallerDetailsShow";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button deliveryButton;
        private System.Windows.Forms.TextBox fullAddressTextBox;
        private System.Windows.Forms.Button collectionButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button editButton;
    }
}