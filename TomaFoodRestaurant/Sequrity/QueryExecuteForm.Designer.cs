namespace TomaFoodRestaurant.Sequrity
{
    partial class QueryExecuteForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.inputTextButton1 = new TomaFoodRestaurant.KeyboardButton.InputTextButton();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(49, 67);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(575, 200);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // inputTextButton1
            // 
            this.inputTextButton1.BgImageOnMouseDown = null;
            this.inputTextButton1.BgImageOnMouseUp = null;
            this.inputTextButton1.ClearField = false;
            this.inputTextButton1.ControlToInputText = null;
            this.inputTextButton1.FlatAppearance.BorderSize = 0;
            this.inputTextButton1.ForeColorOnMouseDown = System.Drawing.Color.White;
            this.inputTextButton1.ForeColorOnMouseUp = System.Drawing.Color.White;
            this.inputTextButton1.Location = new System.Drawing.Point(353, 273);
            this.inputTextButton1.Name = "inputTextButton1";
            this.inputTextButton1.RemoveLastChar = false;
            this.inputTextButton1.Size = new System.Drawing.Size(271, 50);
            this.inputTextButton1.TabIndex = 1;
            this.inputTextButton1.Text = "Execute Query";
            this.inputTextButton1.UseVisualStyleBackColor = true;
            this.inputTextButton1.Click += new System.EventHandler(this.inputTextButton1_Click);
            // 
            // QueryExecuteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 410);
            this.Controls.Add(this.inputTextButton1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "QueryExecuteForm";
            this.ShowInTaskbar = false;
            this.Text = "QueryExecuteForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private KeyboardButton.InputTextButton inputTextButton1;
    }
}