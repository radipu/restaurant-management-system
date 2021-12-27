namespace TomaFoodRestaurant.OtherForm
{
    partial class KitchenControl
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
            this.components = new System.ComponentModel.Container();
            this.TopFayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CompletedLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ProcessingLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PendingLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopFayoutPanel
            // 
            this.TopFayoutPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TopFayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopFayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TopFayoutPanel.Name = "TopFayoutPanel";
            this.TopFayoutPanel.Size = new System.Drawing.Size(926, 46);
            this.TopFayoutPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.CompletedLayoutPanel);
            this.panel1.Controls.Add(this.ProcessingLayoutPanel);
            this.panel1.Controls.Add(this.PendingLayoutPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 435);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gold;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "PENDING ITEMS";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightGreen;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Tahoma", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(608, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(318, 37);
            this.button3.TabIndex = 7;
            this.button3.Text = "COMPLETED ITEMS";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(291, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(317, 37);
            this.button2.TabIndex = 5;
            this.button2.Text = "PROCESSING ITEMS";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // CompletedLayoutPanel
            // 
            this.CompletedLayoutPanel.AutoScroll = true;
            this.CompletedLayoutPanel.BackColor = System.Drawing.Color.LightGreen;
            this.CompletedLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.CompletedLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.CompletedLayoutPanel.Location = new System.Drawing.Point(607, 0);
            this.CompletedLayoutPanel.Name = "CompletedLayoutPanel";
            this.CompletedLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 35, 10, 0);
            this.CompletedLayoutPanel.Size = new System.Drawing.Size(319, 435);
            this.CompletedLayoutPanel.TabIndex = 1;
            this.CompletedLayoutPanel.WrapContents = false;
            // 
            // ProcessingLayoutPanel
            // 
            this.ProcessingLayoutPanel.AutoScroll = true;
            this.ProcessingLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            this.ProcessingLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ProcessingLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ProcessingLayoutPanel.Location = new System.Drawing.Point(291, 0);
            this.ProcessingLayoutPanel.Name = "ProcessingLayoutPanel";
            this.ProcessingLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 35, 10, 0);
            this.ProcessingLayoutPanel.Size = new System.Drawing.Size(316, 435);
            this.ProcessingLayoutPanel.TabIndex = 2;
            this.ProcessingLayoutPanel.WrapContents = false;
            // 
            // PendingLayoutPanel
            // 
            this.PendingLayoutPanel.AutoScroll = true;
            this.PendingLayoutPanel.BackColor = System.Drawing.Color.Gold;
            this.PendingLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.PendingLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PendingLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.PendingLayoutPanel.Name = "PendingLayoutPanel";
            this.PendingLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 35, 10, 0);
            this.PendingLayoutPanel.Size = new System.Drawing.Size(291, 435);
            this.PendingLayoutPanel.TabIndex = 0;
            this.PendingLayoutPanel.WrapContents = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // KitchenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopFayoutPanel);
            this.Name = "KitchenControl";
            this.Size = new System.Drawing.Size(926, 481);
            this.Load += new System.EventHandler(this.KitchenControl_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion 

        private System.Windows.Forms.FlowLayoutPanel TopFayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel PendingLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel ProcessingLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel CompletedLayoutPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1; 
         

    }
}
