namespace TomaFoodRestaurant.OtherForm
{
    partial class SwapForm
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
            this.fromSwapComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toComboBox = new System.Windows.Forms.ComboBox();
            this.swapButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fromSwapComboBox
            // 
            this.fromSwapComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromSwapComboBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.fromSwapComboBox.FormattingEnabled = true;
            this.fromSwapComboBox.Location = new System.Drawing.Point(244, 95);
            this.fromSwapComboBox.Name = "fromSwapComboBox";
            this.fromSwapComboBox.Size = new System.Drawing.Size(262, 32);
            this.fromSwapComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Table";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(994, 47);
            this.label2.TabIndex = 2;
            this.label2.Text = "Swap Information";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(525, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "To Table";
            // 
            // toComboBox
            // 
            this.toComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toComboBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.toComboBox.FormattingEnabled = true;
            this.toComboBox.Location = new System.Drawing.Point(624, 95);
            this.toComboBox.Name = "toComboBox";
            this.toComboBox.Size = new System.Drawing.Size(262, 32);
            this.toComboBox.TabIndex = 3;
            // 
            // swapButton
            // 
            this.swapButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.swapButton.FlatAppearance.BorderSize = 0;
            this.swapButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.swapButton.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.swapButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.swapButton.Location = new System.Drawing.Point(700, 147);
            this.swapButton.Name = "swapButton";
            this.swapButton.Size = new System.Drawing.Size(186, 52);
            this.swapButton.TabIndex = 5;
            this.swapButton.Text = "Swap";
            this.swapButton.UseVisualStyleBackColor = false;
            this.swapButton.Click += new System.EventHandler(this.swapButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(488, 147);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(186, 52);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // SwapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.swapButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fromSwapComboBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SwapForm";
            this.ShowInTaskbar = false;
            this.Text = "SwapForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SwapForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox fromSwapComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox toComboBox;
        private System.Windows.Forms.Button swapButton;
        private System.Windows.Forms.Button closeButton;
    }
}