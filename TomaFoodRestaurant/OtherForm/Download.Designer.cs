namespace TomaFoodRestaurant.OtherForm
{
    partial class Download
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
            this.btnDownload = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelPerc = new System.Windows.Forms.Label();
            this.labelDownloaded = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.labelMsg = new System.Windows.Forms.Label();
            this.panelbody = new System.Windows.Forms.Panel();
            this.panelMsg.SuspendLayout();
            this.panelbody.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(220)))));
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDownload.Location = new System.Drawing.Point(494, 175);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(160, 34);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = " Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 48);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(622, 18);
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeed.Location = new System.Drawing.Point(528, 73);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(0, 18);
            this.labelSpeed.TabIndex = 4;
            // 
            // labelPerc
            // 
            this.labelPerc.AutoSize = true;
            this.labelPerc.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPerc.Location = new System.Drawing.Point(291, 121);
            this.labelPerc.Name = "labelPerc";
            this.labelPerc.Size = new System.Drawing.Size(0, 18);
            this.labelPerc.TabIndex = 4;
            // 
            // labelDownloaded
            // 
            this.labelDownloaded.AutoSize = true;
            this.labelDownloaded.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDownloaded.Location = new System.Drawing.Point(13, 73);
            this.labelDownloaded.Name = "labelDownloaded";
            this.labelDownloaded.Size = new System.Drawing.Size(0, 18);
            this.labelDownloaded.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(29)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancel.Location = new System.Drawing.Point(22, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Skip";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelMsg
            // 
            this.panelMsg.BackColor = System.Drawing.Color.White;
            this.panelMsg.Controls.Add(this.labelMsg);
            this.panelMsg.Controls.Add(this.labelDownloaded);
            this.panelMsg.Controls.Add(this.labelPerc);
            this.panelMsg.Controls.Add(this.labelSpeed);
            this.panelMsg.Controls.Add(this.progressBar);
            this.panelMsg.Location = new System.Drawing.Point(13, 4);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(641, 147);
            this.panelMsg.TabIndex = 5;
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsg.Location = new System.Drawing.Point(30, 48);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(576, 18);
            this.labelMsg.TabIndex = 5;
            this.labelMsg.Text = "New version of software is available.Please update your application.";
            // 
            // panelbody
            // 
            this.panelbody.BackColor = System.Drawing.Color.White;
            this.panelbody.Controls.Add(this.panelMsg);
            this.panelbody.Controls.Add(this.btnCancel);
            this.panelbody.Controls.Add(this.btnDownload);
            this.panelbody.Location = new System.Drawing.Point(12, 12);
            this.panelbody.Name = "panelbody";
            this.panelbody.Size = new System.Drawing.Size(667, 219);
            this.panelbody.TabIndex = 7;
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.ClientSize = new System.Drawing.Size(691, 243);
            this.ControlBox = false;
            this.Controls.Add(this.panelbody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Download";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelMsg.ResumeLayout(false);
            this.panelMsg.PerformLayout();
            this.panelbody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelPerc;
        private System.Windows.Forms.Label labelDownloaded;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelMsg;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Panel panelbody;
    }
}