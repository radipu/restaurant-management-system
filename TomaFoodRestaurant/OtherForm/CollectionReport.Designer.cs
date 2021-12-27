namespace TomaFoodRestaurant.OtherForm
{
    partial class CollectionReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cltOrdersDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIEW = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DONE = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cltOrdersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cltOrdersDataGridView
            // 
            this.cltOrdersDataGridView.AllowUserToAddRows = false;
            this.cltOrdersDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cltOrdersDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.cltOrdersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cltOrdersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cltOrdersDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.cltOrdersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cltOrdersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderId,
            this.Sl,
            this.OrderTime,
            this.Customer,
            this.Total,
            this.VIEW,
            this.DONE});
            this.cltOrdersDataGridView.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cltOrdersDataGridView.Location = new System.Drawing.Point(11, 53);
            this.cltOrdersDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.cltOrdersDataGridView.Name = "cltOrdersDataGridView";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cltOrdersDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.cltOrdersDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cltOrdersDataGridView.RowTemplate.Height = 40;
            this.cltOrdersDataGridView.Size = new System.Drawing.Size(994, 432);
            this.cltOrdersDataGridView.TabIndex = 10;
            this.cltOrdersDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cltOrdersDataGridView_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "COLLECTION LIST ";
            // 
            // OrderId
            // 
            this.OrderId.DataPropertyName = "OrderId";
            this.OrderId.HeaderText = "OrderId";
            this.OrderId.Name = "OrderId";
            this.OrderId.Visible = false;
            // 
            // Sl
            // 
            this.Sl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Sl.DataPropertyName = "Sl";
            this.Sl.HeaderText = "Sl";
            this.Sl.Name = "Sl";
            this.Sl.Width = 35;
            // 
            // OrderTime
            // 
            this.OrderTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OrderTime.DataPropertyName = "OrderTime";
            this.OrderTime.HeaderText = "OrderTime";
            this.OrderTime.Name = "OrderTime";
            this.OrderTime.Width = 150;
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "Customer";
            this.Customer.HeaderText = "Customer";
            this.Customer.Name = "Customer";
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.Width = 70;
            // 
            // VIEW
            // 
            this.VIEW.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkTurquoise;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.VIEW.DefaultCellStyle = dataGridViewCellStyle2;
            this.VIEW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.VIEW.HeaderText = "VIEW";
            this.VIEW.Name = "VIEW";
            this.VIEW.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VIEW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.VIEW.Text = "VIEW";
            this.VIEW.ToolTipText = "VIEW";
            this.VIEW.UseColumnTextForButtonValue = true;
            // 
            // DONE
            // 
            this.DONE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.DONE.DefaultCellStyle = dataGridViewCellStyle3;
            this.DONE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DONE.HeaderText = "DONE";
            this.DONE.Name = "DONE";
            this.DONE.Text = "DONE";
            this.DONE.ToolTipText = "DONE";
            this.DONE.UseColumnTextForButtonValue = true;
            this.DONE.Visible = false;
            // 
            // CollectionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cltOrdersDataGridView);
            this.Name = "CollectionReport";
            this.Size = new System.Drawing.Size(1019, 499);
            this.Load += new System.EventHandler(this.CollectionReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cltOrdersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView cltOrdersDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sl;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn VIEW;
        private System.Windows.Forms.DataGridViewButtonColumn DONE;
    }
}
