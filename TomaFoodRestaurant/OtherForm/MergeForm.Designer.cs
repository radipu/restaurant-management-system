using System.Drawing;

namespace TomaFoodRestaurant.OtherForm
{
    partial class MergeForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tabledataGridView = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toMergeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mergeButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.demergeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabledataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1006, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table Merge Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabledataGridView
            // 
            this.tabledataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabledataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tabledataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabledataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabledataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.tabledataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.tabledataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabledataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tabledataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.tabledataGridView.Location = new System.Drawing.Point(12, 51);
            this.tabledataGridView.Name = "tabledataGridView";
            this.tabledataGridView.RowTemplate.Height = 50;
            this.tabledataGridView.Size = new System.Drawing.Size(830, 601);
            this.tabledataGridView.TabIndex = 1;
            this.tabledataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.tabledataGridView_DataBindingComplete);
            // 
            // select
            // 
            this.select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.NullValue = false;
            this.select.DefaultCellStyle = dataGridViewCellStyle2;
            this.select.HeaderText = "Select";
            this.select.MinimumWidth = 10;
            this.select.Name = "select";
            this.select.Width = 50;
            // 
            // toMergeComboBox
            // 
            this.toMergeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toMergeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toMergeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toMergeComboBox.ForeColor = System.Drawing.Color.Black;
            this.toMergeComboBox.FormattingEnabled = true;
            this.toMergeComboBox.Location = new System.Drawing.Point(906, 308);
            this.toMergeComboBox.Name = "toMergeComboBox";
            this.toMergeComboBox.Size = new System.Drawing.Size(70, 32);
            this.toMergeComboBox.TabIndex = 2;
            this.toMergeComboBox.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(859, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Merge Table Number";
            this.label2.Visible = false;
            // 
            // mergeButton
            // 
            this.mergeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mergeButton.FlatAppearance.BorderSize = 0;
            this.mergeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mergeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mergeButton.ForeColor = System.Drawing.Color.White;
            this.mergeButton.Location = new System.Drawing.Point(848, 53);
            this.mergeButton.Name = "mergeButton";
            this.mergeButton.Size = new System.Drawing.Size(148, 58);
            this.mergeButton.TabIndex = 6;
            this.mergeButton.Text = "Merge";
            this.mergeButton.UseVisualStyleBackColor = false;
            this.mergeButton.Click += new System.EventHandler(this.mergeButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(187)))), ((int)(((byte)(66)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(10, 669);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(151, 51);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // demergeButton
            // 
            this.demergeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.demergeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.demergeButton.FlatAppearance.BorderSize = 0;
            this.demergeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.demergeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.demergeButton.ForeColor = System.Drawing.Color.White;
            this.demergeButton.Location = new System.Drawing.Point(848, 126);
            this.demergeButton.Name = "demergeButton";
            this.demergeButton.Size = new System.Drawing.Size(148, 58);
            this.demergeButton.TabIndex = 8;
            this.demergeButton.Text = "Demerge";
            this.demergeButton.UseVisualStyleBackColor = false;
            this.demergeButton.Click += new System.EventHandler(this.demergeButton_Click);
            // 
            // MergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.demergeButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.mergeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toMergeComboBox);
            this.Controls.Add(this.tabledataGridView);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergeForm";
            this.ShowInTaskbar = false;
            this.Text = "MergeForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MergeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabledataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView tabledataGridView;
        private System.Windows.Forms.ComboBox toMergeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mergeButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button demergeButton;
    }
}