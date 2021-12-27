namespace TomaFoodRestaurant.OtherForm
{
    partial class RecipeTypeDetails
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
            this.recipeTypelabel = new System.Windows.Forms.Label();
            this.typeflowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.recipeTypeAmountlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // recipeTypelabel
            // 
            this.recipeTypelabel.AutoSize = true;
            this.recipeTypelabel.Location = new System.Drawing.Point(3, 0);
            this.recipeTypelabel.Name = "recipeTypelabel";
            this.recipeTypelabel.Size = new System.Drawing.Size(13,35);
            this.recipeTypelabel.TabIndex = 0;
            this.recipeTypelabel.Text = "--";
            this.recipeTypelabel.TextChanged += new System.EventHandler(this.recipeTypelabel_TextChanged);
            this.recipeTypelabel.Click += new System.EventHandler(this.recipeTypelabel_Click);
            this.recipeTypelabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            // 
            // typeflowLayoutPanel1
            // 
            this.typeflowLayoutPanel1.AutoSize = true;
            this.typeflowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.typeflowLayoutPanel1.Location = new System.Drawing.Point(2, 16);
            this.typeflowLayoutPanel1.Name = "typeflowLayoutPanel1";
            this.typeflowLayoutPanel1.Size = new System.Drawing.Size(240, 20);
         //   this.typeflowLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          
            this.typeflowLayoutPanel1.TabIndex = 1;
            this.typeflowLayoutPanel1.WrapContents = false;
            // 
            // recipeTypeAmountlabel
            // 
            this.recipeTypeAmountlabel.AutoSize = true;
            this.recipeTypeAmountlabel.BackColor = System.Drawing.SystemColors.Control;
            this.recipeTypeAmountlabel.Location = new System.Drawing.Point(201, 0);
            this.recipeTypeAmountlabel.Name = "recipeTypeAmountlabel";
            this.recipeTypeAmountlabel.Size = new System.Drawing.Size(13, 35);
            this.recipeTypeAmountlabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          
            this.recipeTypeAmountlabel.TabIndex = 2;
            this.recipeTypeAmountlabel.Text = "--";
            // 
            // RecipeTypeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.recipeTypeAmountlabel);
            this.Controls.Add(this.recipeTypelabel);
            this.Controls.Add(this.typeflowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RecipeTypeDetails";
            this.Size = new System.Drawing.Size(245, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel typeflowLayoutPanel1;
        public System.Windows.Forms.Label recipeTypelabel;
        public System.Windows.Forms.Label recipeTypeAmountlabel;
    }
}
