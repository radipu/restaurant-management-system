namespace TomaFoodRestaurant.OtherForm
{
    partial class OnlineOrderForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.onlineOrdersDataGridView = new System.Windows.Forms.DataGridView();
            this.OrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnlineOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accept = new System.Windows.Forms.DataGridViewButtonColumn();
            this.reject = new System.Windows.Forms.DataGridViewButtonColumn();
            this.acceptTime = new System.Windows.Forms.DataGridViewButtonColumn();
            this.view = new System.Windows.Forms.DataGridViewButtonColumn();
            this.goBackButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.onlineOrdersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // onlineOrdersDataGridView
            // 
            this.onlineOrdersDataGridView.AllowUserToAddRows = false;
            this.onlineOrdersDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.onlineOrdersDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.onlineOrdersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.onlineOrdersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.onlineOrdersDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.onlineOrdersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.onlineOrdersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.onlineOrdersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderId,
            this.OnlineOrder,
            this.Sl,
            this.OrderTime,
            this.Customer,
            this.Type,
            this.Method,
            this.Total,
            this.accept,
            this.reject,
            this.acceptTime,
            this.view});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.onlineOrdersDataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.onlineOrdersDataGridView.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.onlineOrdersDataGridView.Location = new System.Drawing.Point(2, 13);
            this.onlineOrdersDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.onlineOrdersDataGridView.Name = "onlineOrdersDataGridView";
            this.onlineOrdersDataGridView.ReadOnly = true;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineOrdersDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.onlineOrdersDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineOrdersDataGridView.RowTemplate.Height = 40;
            this.onlineOrdersDataGridView.Size = new System.Drawing.Size(1013, 412);
            this.onlineOrdersDataGridView.TabIndex = 9;
            this.onlineOrdersDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.onlineOrdersDataGridView_CellContentClick);
            this.onlineOrdersDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.onlineOrdersDataGridView_DataBindingComplete);
            // 
            // OrderId
            // 
            this.OrderId.DataPropertyName = "OrderId";
            this.OrderId.HeaderText = "OrderId";
            this.OrderId.Name = "OrderId";
            this.OrderId.ReadOnly = true;
            this.OrderId.Visible = false;
            // 
            // OnlineOrder
            // 
            this.OnlineOrder.DataPropertyName = "OnlineOrder";
            this.OnlineOrder.HeaderText = "OnlineOrder";
            this.OnlineOrder.Name = "OnlineOrder";
            this.OnlineOrder.ReadOnly = true;
            this.OnlineOrder.Visible = false;
            // 
            // Sl
            // 
            this.Sl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Sl.DataPropertyName = "Sl";
            this.Sl.HeaderText = "Sl";
            this.Sl.Name = "Sl";
            this.Sl.ReadOnly = true;
            this.Sl.Width = 35;
            // 
            // OrderTime
            // 
            this.OrderTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OrderTime.DataPropertyName = "OrderTime";
            this.OrderTime.HeaderText = "OrderTime";
            this.OrderTime.Name = "OrderTime";
            this.OrderTime.ReadOnly = true;
            this.OrderTime.Width = 150;
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "Customer";
            this.Customer.HeaderText = "Customer";
            this.Customer.Name = "Customer";
            this.Customer.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 50;
            // 
            // Method
            // 
            this.Method.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Method.DataPropertyName = "Method";
            this.Method.HeaderText = "Method";
            this.Method.Name = "Method";
            this.Method.ReadOnly = true;
            this.Method.Width = 80;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 70;
            // 
            // accept
            // 
            this.accept.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.accept.DefaultCellStyle = dataGridViewCellStyle3;
            this.accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accept.HeaderText = "Accept";
            this.accept.Name = "accept";
            this.accept.ReadOnly = true;
            this.accept.Text = "Accept";
            this.accept.ToolTipText = "Accept";
            this.accept.UseColumnTextForButtonValue = true;
            this.accept.Visible = false;
            this.accept.Width = 80;
            // 
            // reject
            // 
            this.reject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.reject.DefaultCellStyle = dataGridViewCellStyle4;
            this.reject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reject.HeaderText = "Reject";
            this.reject.Name = "reject";
            this.reject.ReadOnly = true;
            this.reject.Text = "Reject";
            this.reject.ToolTipText = "Reject";
            this.reject.UseColumnTextForButtonValue = true;
            this.reject.Visible = false;
            this.reject.Width = 80;
            // 
            // acceptTime
            // 
            this.acceptTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.acceptTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.acceptTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptTime.HeaderText = "Accept Time";
            this.acceptTime.Name = "acceptTime";
            this.acceptTime.ReadOnly = true;
            this.acceptTime.Text = "Accept Time";
            this.acceptTime.ToolTipText = "Accept Time";
            this.acceptTime.UseColumnTextForButtonValue = true;
            this.acceptTime.Visible = false;
            // 
            // view
            // 
            this.view.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.view.DefaultCellStyle = dataGridViewCellStyle6;
            this.view.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.view.HeaderText = "View & Accept";
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.Text = "View & Accept";
            this.view.ToolTipText = "View & Accept";
            this.view.UseColumnTextForButtonValue = true;
            this.view.Width = 180;
            // 
            // goBackButton
            // 
            this.goBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.goBackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.goBackButton.FlatAppearance.BorderSize = 0;
            this.goBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goBackButton.ForeColor = System.Drawing.Color.White;
            this.goBackButton.Location = new System.Drawing.Point(2, 432);
            this.goBackButton.Name = "goBackButton";
            this.goBackButton.Size = new System.Drawing.Size(87, 41);
            this.goBackButton.TabIndex = 10;
            this.goBackButton.Text = "Go Back";
            this.goBackButton.UseVisualStyleBackColor = false;
            this.goBackButton.Click += new System.EventHandler(this.goBackButton_Click);
            // 
            // OnlineOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 573);
            this.Controls.Add(this.goBackButton);
            this.Controls.Add(this.onlineOrdersDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OnlineOrderForm";
            this.ShowInTaskbar = false;
            this.Text = "OnlineOrderForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnlineOrderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.onlineOrdersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView onlineOrdersDataGridView;
        private System.Windows.Forms.Button goBackButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnlineOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sl;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn accept;
        private System.Windows.Forms.DataGridViewButtonColumn reject;
        private System.Windows.Forms.DataGridViewButtonColumn acceptTime;
        private System.Windows.Forms.DataGridViewButtonColumn view;
    }
}