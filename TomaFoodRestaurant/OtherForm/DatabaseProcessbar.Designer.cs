namespace TomaFoodRestaurant.OtherForm
{
    partial class DatabaseProcessbar
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
            this.components = new System.ComponentModel.Container();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::TomaFoodRestaurant.OtherForm.WaitForm2), true, true);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 42);
            this.panel1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Properties.ShowTitle = true;
            this.progressBar1.Size = new System.Drawing.Size(470, 42);
            this.progressBar1.TabIndex = 0;
            // 
            // DatabaseProcessbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 42);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DatabaseProcessbar";
            this.ShowInTaskbar = false;
            this.Text = "DatabaseProcessbar";
            this.Load += new System.EventHandler(this.DatabaseProcessbar_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.ProgressBarControl progressBar1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}