namespace TomaFoodRestaurant.OtherForm
{
    partial class CallList
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
            this.flowLayoutPanelForCallLog = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelForCallLog
            // 
            this.flowLayoutPanelForCallLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanelForCallLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelForCallLog.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelForCallLog.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanelForCallLog.Location = new System.Drawing.Point(20, 20);
            this.flowLayoutPanelForCallLog.Name = "flowLayoutPanelForCallLog";
            this.flowLayoutPanelForCallLog.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanelForCallLog.Size = new System.Drawing.Size(933, 559);
            this.flowLayoutPanelForCallLog.TabIndex = 0;
            // 
            // CallList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.flowLayoutPanelForCallLog);
            this.Name = "CallList";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(973, 599);
            this.Load += new System.EventHandler(this.CallList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelForCallLog;

    }
}
