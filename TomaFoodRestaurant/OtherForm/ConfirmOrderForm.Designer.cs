using System.Drawing;

namespace TomaFoodRestaurant.OtherForm
{
    partial class ConfirmOrderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmOrderForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.paidAmountTextBox = new System.Windows.Forms.TextBox();
            this.exactButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cardTextBox = new System.Windows.Forms.TextBox();
            this.cashTextBox = new System.Windows.Forms.TextBox();
            this.printButton = new System.Windows.Forms.Button();
            this.cardPaymentButton = new System.Windows.Forms.Button();
            this.doNotPrintButton = new System.Windows.Forms.Button();
            this.orderTotalLabel = new System.Windows.Forms.Label();
            this.changeAmountLabel = new System.Windows.Forms.Label();
            this.servedByTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cardFeeButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "TOTAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(-1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 27);
            this.label2.TabIndex = 10;
            this.label2.Text = "CHANGE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(220)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.Transparent;
            this.backButton.Location = new System.Drawing.Point(11, 374);
            this.backButton.Margin = new System.Windows.Forms.Padding(0);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(280, 161);
            this.backButton.TabIndex = 21;
            this.backButton.Text = "BACK";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(306, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 431);
            this.panel1.TabIndex = 22;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.Location = new System.Drawing.Point(15, 362);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(258, 60);
            this.button5.TabIndex = 20;
            this.button5.Text = "£50";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.CoinButton_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.Location = new System.Drawing.Point(15, 297);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(258, 60);
            this.button6.TabIndex = 19;
            this.button6.Text = "£20";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.CoinButton_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.Location = new System.Drawing.Point(15, 232);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(258, 60);
            this.button7.TabIndex = 18;
            this.button7.Text = "£10";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.CoinButton_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.ForeColor = System.Drawing.Color.Black;
            this.button11.Image = ((System.Drawing.Image)(resources.GetObject("button11.Image")));
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button11.Location = new System.Drawing.Point(15, 102);
            this.button11.Margin = new System.Windows.Forms.Padding(0);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(258, 60);
            this.button11.TabIndex = 15;
            this.button11.Text = "£2";
            this.button11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.CoinButton_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button12.Location = new System.Drawing.Point(15, 37);
            this.button12.Margin = new System.Windows.Forms.Padding(0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(258, 60);
            this.button12.TabIndex = 14;
            this.button12.Text = "£1";
            this.button12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.CoinButton_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.Black;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.Location = new System.Drawing.Point(15, 167);
            this.button8.Margin = new System.Windows.Forms.Padding(0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(258, 60);
            this.button8.TabIndex = 17;
            this.button8.Text = "£5";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.CoinButton_Click);
            // 
            // paidAmountTextBox
            // 
            this.paidAmountTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.paidAmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.paidAmountTextBox.Font = new System.Drawing.Font("Verdana", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidAmountTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.paidAmountTextBox.Location = new System.Drawing.Point(8, 64);
            this.paidAmountTextBox.Name = "paidAmountTextBox";
            this.paidAmountTextBox.Size = new System.Drawing.Size(283, 36);
            this.paidAmountTextBox.TabIndex = 24;
            this.paidAmountTextBox.Text = "0";
            this.paidAmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.paidAmountTextBox.Click += new System.EventHandler(this.numberForm_Open);
            this.paidAmountTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.paidAmountTextBox_MouseClick);
            this.paidAmountTextBox.TextChanged += new System.EventHandler(this.paidAmountTextBox_TextChanged);
            // 
            // exactButton
            // 
            this.exactButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(211)))), ((int)(((byte)(124)))));
            this.exactButton.FlatAppearance.BorderSize = 0;
            this.exactButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exactButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exactButton.ForeColor = System.Drawing.Color.Transparent;
            this.exactButton.Location = new System.Drawing.Point(605, 171);
            this.exactButton.Margin = new System.Windows.Forms.Padding(0);
            this.exactButton.Name = "exactButton";
            this.exactButton.Size = new System.Drawing.Size(198, 60);
            this.exactButton.TabIndex = 23;
            this.exactButton.Text = "EXACT AMOUNT";
            this.exactButton.UseVisualStyleBackColor = false;
            this.exactButton.Click += new System.EventHandler(this.exactButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(110)))), ((int)(((byte)(81)))));
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.ForeColor = System.Drawing.Color.Transparent;
            this.resetButton.Location = new System.Drawing.Point(606, 242);
            this.resetButton.Margin = new System.Windows.Forms.Padding(0);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(197, 60);
            this.resetButton.TabIndex = 22;
            this.resetButton.Text = "RESET";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(307, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 31);
            this.label3.TabIndex = 23;
            this.label3.Text = "CASH PAYMENT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(-2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(344, 27);
            this.label4.TabIndex = 25;
            this.label4.Text = " SPLIT ORDER";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cardTextBox);
            this.panel2.Controls.Add(this.cashTextBox);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(11, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 132);
            this.panel2.TabIndex = 24;
            // 
            // cardTextBox
            // 
            this.cardTextBox.BackColor = System.Drawing.Color.White;
            this.cardTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardTextBox.ForeColor = System.Drawing.Color.Black;
            this.cardTextBox.Location = new System.Drawing.Point(152, 67);
            this.cardTextBox.Name = "cardTextBox";
            this.cardTextBox.Size = new System.Drawing.Size(111, 28);
            this.cardTextBox.TabIndex = 26;
            this.cardTextBox.Text = "CARD";
            this.cardTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cardTextBox_MouseClick);
            this.cardTextBox.TextChanged += new System.EventHandler(this.cardTextBox_TextChanged);
            this.cardTextBox.Enter += new System.EventHandler(this.cardTextBox_Enter);
            this.cardTextBox.Leave += new System.EventHandler(this.cardTextBox_Leave);
            // 
            // cashTextBox
            // 
            this.cashTextBox.BackColor = System.Drawing.Color.White;
            this.cashTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cashTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashTextBox.ForeColor = System.Drawing.Color.Black;
            this.cashTextBox.Location = new System.Drawing.Point(13, 67);
            this.cashTextBox.Name = "cashTextBox";
            this.cashTextBox.Size = new System.Drawing.Size(121, 28);
            this.cashTextBox.TabIndex = 25;
            this.cashTextBox.Text = "CASH";
            this.cashTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cashTextBox_MouseClick);
            this.cashTextBox.TextChanged += new System.EventHandler(this.cashTextBox_TextChanged);
            this.cashTextBox.Enter += new System.EventHandler(this.cashTextBox_Enter);
            this.cashTextBox.Leave += new System.EventHandler(this.cashTextBox_Leave);
            // 
            // printButton
            // 
            this.printButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(146)))), ((int)(((byte)(236)))));
            this.printButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.printButton.FlatAppearance.BorderSize = 0;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Arial", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.ForeColor = System.Drawing.Color.White;
            this.printButton.Location = new System.Drawing.Point(808, 260);
            this.printButton.Margin = new System.Windows.Forms.Padding(0);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(191, 275);
            this.printButton.TabIndex = 26;
            this.printButton.Text = "PRINT";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // cardPaymentButton
            // 
            this.cardPaymentButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.cardPaymentButton.FlatAppearance.BorderSize = 0;
            this.cardPaymentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cardPaymentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardPaymentButton.ForeColor = System.Drawing.Color.Transparent;
            this.cardPaymentButton.Location = new System.Drawing.Point(306, 460);
            this.cardPaymentButton.Margin = new System.Windows.Forms.Padding(0);
            this.cardPaymentButton.Name = "cardPaymentButton";
            this.cardPaymentButton.Size = new System.Drawing.Size(291, 75);
            this.cardPaymentButton.TabIndex = 27;
            this.cardPaymentButton.Text = "CARD PAYMENT";
            this.cardPaymentButton.UseVisualStyleBackColor = false;
            this.cardPaymentButton.Click += new System.EventHandler(this.cardPaymentButton_Click);
            // 
            // doNotPrintButton
            // 
            this.doNotPrintButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.doNotPrintButton.FlatAppearance.BorderSize = 0;
            this.doNotPrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doNotPrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doNotPrintButton.ForeColor = System.Drawing.Color.Black;
            this.doNotPrintButton.Location = new System.Drawing.Point(606, 399);
            this.doNotPrintButton.Margin = new System.Windows.Forms.Padding(0);
            this.doNotPrintButton.Name = "doNotPrintButton";
            this.doNotPrintButton.Size = new System.Drawing.Size(197, 136);
            this.doNotPrintButton.TabIndex = 28;
            this.doNotPrintButton.Text = "DO NOT\r\n PRINT\r\n";
            this.doNotPrintButton.UseVisualStyleBackColor = false;
            this.doNotPrintButton.Click += new System.EventHandler(this.doNotPrintButton_Click);
            // 
            // orderTotalLabel
            // 
            this.orderTotalLabel.BackColor = System.Drawing.Color.White;
            this.orderTotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 46F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderTotalLabel.Location = new System.Drawing.Point(-1, 29);
            this.orderTotalLabel.Name = "orderTotalLabel";
            this.orderTotalLabel.Size = new System.Drawing.Size(389, 105);
            this.orderTotalLabel.TabIndex = 29;
            this.orderTotalLabel.Text = "0";
            this.orderTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // changeAmountLabel
            // 
            this.changeAmountLabel.BackColor = System.Drawing.Color.White;
            this.changeAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeAmountLabel.Location = new System.Drawing.Point(-1, 27);
            this.changeAmountLabel.Name = "changeAmountLabel";
            this.changeAmountLabel.Size = new System.Drawing.Size(186, 51);
            this.changeAmountLabel.TabIndex = 30;
            this.changeAmountLabel.Text = "£0";
            this.changeAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // servedByTextBox
            // 
            this.servedByTextBox.BackColor = System.Drawing.Color.White;
            this.servedByTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servedByTextBox.ForeColor = System.Drawing.Color.Black;
            this.servedByTextBox.Location = new System.Drawing.Point(11, 42);
            this.servedByTextBox.Name = "servedByTextBox";
            this.servedByTextBox.Size = new System.Drawing.Size(250, 40);
            this.servedByTextBox.TabIndex = 31;
            this.servedByTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.servedByTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.servedByTextBox_MouseClick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(274, 27);
            this.label5.TabIndex = 32;
            this.label5.Text = "SERVED BY";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardFeeButton
            // 
            this.cardFeeButton.BackColor = System.Drawing.Color.SeaGreen;
            this.cardFeeButton.FlatAppearance.BorderSize = 0;
            this.cardFeeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cardFeeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardFeeButton.ForeColor = System.Drawing.Color.White;
            this.cardFeeButton.Location = new System.Drawing.Point(605, 312);
            this.cardFeeButton.Margin = new System.Windows.Forms.Padding(0);
            this.cardFeeButton.Name = "cardFeeButton";
            this.cardFeeButton.Size = new System.Drawing.Size(198, 75);
            this.cardFeeButton.TabIndex = 33;
            this.cardFeeButton.Text = "ADD CARD FEE";
            this.cardFeeButton.UseVisualStyleBackColor = false;
            this.cardFeeButton.Click += new System.EventHandler(this.cardFeeButton_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.servedByTextBox);
            this.panel3.Location = new System.Drawing.Point(13, 262);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(277, 100);
            this.panel3.TabIndex = 34;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.orderTotalLabel);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(605, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(391, 135);
            this.panel4.TabIndex = 35;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.changeAmountLabel);
            this.panel5.Location = new System.Drawing.Point(808, 170);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(188, 79);
            this.panel5.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(8, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(282, 31);
            this.label6.TabIndex = 37;
            this.label6.Text = "RECEIVED AMOUNT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.paidAmountTextBox);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.exactButton);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.cardFeeButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.doNotPrintButton);
            this.Controls.Add(this.cardPaymentButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.backButton);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfirmOrderForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox paidAmountTextBox;
        private System.Windows.Forms.Button exactButton;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox cardTextBox;
        private System.Windows.Forms.TextBox cashTextBox;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button cardPaymentButton;
        private System.Windows.Forms.Button doNotPrintButton;
        private System.Windows.Forms.Label orderTotalLabel;
        private System.Windows.Forms.Label changeAmountLabel;
        private System.Windows.Forms.TextBox servedByTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cardFeeButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
    }
}