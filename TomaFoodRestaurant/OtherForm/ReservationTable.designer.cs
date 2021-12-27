namespace TomaFoodRestaurant.OtherForm
{
    partial class ReservationTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReservationTable));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.totalAcceptedOrderPersonlabel = new System.Windows.Forms.Label();
            this.totalAcceptOrderLabel = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.totalPendingOrderPersonlabel = new System.Windows.Forms.Label();
            this.totalPendingOrderLabel = new System.Windows.Forms.Label();
            this.newReservationButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.buttonLoadReservation = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.reservationDataGridView = new System.Windows.Forms.DataGridView();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReservationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contacts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noOfPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReservedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.online_reservation_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arrived = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Accept = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Reject = new System.Windows.Forms.DataGridViewButtonColumn();
            this.edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reservationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.newReservationButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.toDateTimePicker);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.fromDateTimePicker);
            this.panel1.Controls.Add(this.statusComboBox);
            this.panel1.Controls.Add(this.buttonLoadReservation);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 114);
            this.panel1.TabIndex = 23;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.panel4.Controls.Add(this.totalAcceptedOrderPersonlabel);
            this.panel4.Controls.Add(this.totalAcceptOrderLabel);
            this.panel4.Location = new System.Drawing.Point(356, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(419, 64);
            this.panel4.TabIndex = 29;
            // 
            // totalAcceptedOrderPersonlabel
            // 
            this.totalAcceptedOrderPersonlabel.AutoSize = true;
            this.totalAcceptedOrderPersonlabel.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAcceptedOrderPersonlabel.ForeColor = System.Drawing.Color.White;
            this.totalAcceptedOrderPersonlabel.Location = new System.Drawing.Point(3, 35);
            this.totalAcceptedOrderPersonlabel.Name = "totalAcceptedOrderPersonlabel";
            this.totalAcceptedOrderPersonlabel.Size = new System.Drawing.Size(33, 20);
            this.totalAcceptedOrderPersonlabel.TabIndex = 2;
            this.totalAcceptedOrderPersonlabel.Text = "---";
            // 
            // totalAcceptOrderLabel
            // 
            this.totalAcceptOrderLabel.AutoSize = true;
            this.totalAcceptOrderLabel.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAcceptOrderLabel.ForeColor = System.Drawing.Color.White;
            this.totalAcceptOrderLabel.Location = new System.Drawing.Point(3, 4);
            this.totalAcceptOrderLabel.Name = "totalAcceptOrderLabel";
            this.totalAcceptOrderLabel.Size = new System.Drawing.Size(33, 20);
            this.totalAcceptOrderLabel.TabIndex = 1;
            this.totalAcceptOrderLabel.Text = "---";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(202)))), ((int)(((byte)(99)))));
            this.panel6.Controls.Add(this.totalPendingOrderPersonlabel);
            this.panel6.Controls.Add(this.totalPendingOrderLabel);
            this.panel6.Location = new System.Drawing.Point(6, 43);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(325, 64);
            this.panel6.TabIndex = 28;
            // 
            // totalPendingOrderPersonlabel
            // 
            this.totalPendingOrderPersonlabel.AutoSize = true;
            this.totalPendingOrderPersonlabel.BackColor = System.Drawing.Color.Transparent;
            this.totalPendingOrderPersonlabel.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPendingOrderPersonlabel.ForeColor = System.Drawing.Color.White;
            this.totalPendingOrderPersonlabel.Location = new System.Drawing.Point(3, 38);
            this.totalPendingOrderPersonlabel.Name = "totalPendingOrderPersonlabel";
            this.totalPendingOrderPersonlabel.Size = new System.Drawing.Size(33, 20);
            this.totalPendingOrderPersonlabel.TabIndex = 2;
            this.totalPendingOrderPersonlabel.Text = "---";
            // 
            // totalPendingOrderLabel
            // 
            this.totalPendingOrderLabel.AutoSize = true;
            this.totalPendingOrderLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalPendingOrderLabel.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPendingOrderLabel.ForeColor = System.Drawing.Color.White;
            this.totalPendingOrderLabel.Location = new System.Drawing.Point(3, 4);
            this.totalPendingOrderLabel.Name = "totalPendingOrderLabel";
            this.totalPendingOrderLabel.Size = new System.Drawing.Size(33, 20);
            this.totalPendingOrderLabel.TabIndex = 1;
            this.totalPendingOrderLabel.Text = "---";
            // 
            // newReservationButton
            // 
            this.newReservationButton.BackColor = System.Drawing.Color.SlateBlue;
            this.newReservationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newReservationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newReservationButton.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newReservationButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.newReservationButton.Location = new System.Drawing.Point(789, 45);
            this.newReservationButton.Name = "newReservationButton";
            this.newReservationButton.Size = new System.Drawing.Size(206, 62);
            this.newReservationButton.TabIndex = 27;
            this.newReservationButton.Text = "NEW \r\nRESERVATION";
            this.newReservationButton.UseVisualStyleBackColor = false;
            this.newReservationButton.Click += new System.EventHandler(this.newReservationButton_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 19);
            this.label7.TabIndex = 26;
            this.label7.Text = "Date: ";
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.CalendarFont = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateTimePicker.Location = new System.Drawing.Point(356, 10);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(296, 27);
            this.toDateTimePicker.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(337, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "-";
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.CalendarFont = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateTimePicker.Location = new System.Drawing.Point(71, 10);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(260, 27);
            this.fromDateTimePicker.TabIndex = 23;
            // 
            // statusComboBox
            // 
            this.statusComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.statusComboBox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusComboBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "ALL",
            "Pending",
            "Accepted"});
            this.statusComboBox.Location = new System.Drawing.Point(658, 9);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(117, 26);
            this.statusComboBox.TabIndex = 13;
            // 
            // buttonLoadReservation
            // 
            this.buttonLoadReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(220)))));
            this.buttonLoadReservation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadReservation.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadReservation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonLoadReservation.Location = new System.Drawing.Point(789, 7);
            this.buttonLoadReservation.Name = "buttonLoadReservation";
            this.buttonLoadReservation.Size = new System.Drawing.Size(206, 36);
            this.buttonLoadReservation.TabIndex = 0;
            this.buttonLoadReservation.Text = "SEARCH";
            this.buttonLoadReservation.UseVisualStyleBackColor = false;
            this.buttonLoadReservation.Click += new System.EventHandler(this.buttonLoadReservation_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Image = global::TomaFoodRestaurant.Properties.Resources.reorder;
            this.button1.Location = new System.Drawing.Point(949, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 36);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonLoadReservation_Click);
            // 
            // reservationDataGridView
            // 
            this.reservationDataGridView.AllowUserToAddRows = false;
            this.reservationDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.reservationDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.reservationDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reservationDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reservationDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.reservationDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.reservationDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.reservationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reservationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SL,
            this.ReservationId,
            this.Customer,
            this.contacts,
            this.noOfPerson,
            this.ReservedTime,
            this.Notes,
            this.Type,
            this.Status,
            this.online_reservation_id,
            this.Arrived,
            this.Accept,
            this.Reject,
            this.edit});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reservationDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.reservationDataGridView.Location = new System.Drawing.Point(8, 128);
            this.reservationDataGridView.Name = "reservationDataGridView";
            this.reservationDataGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.reservationDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.reservationDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.reservationDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reservationDataGridView.RowTemplate.Height = 40;
            this.reservationDataGridView.Size = new System.Drawing.Size(998, 437);
            this.reservationDataGridView.TabIndex = 24;
            this.reservationDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.reservationDataGridView_CellContentClick);
            this.reservationDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.reservationDataGridView_DataBindingComplete);
            // 
            // SL
            // 
            this.SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SL.DataPropertyName = "SL";
            this.SL.HeaderText = "SL";
            this.SL.Name = "SL";
            this.SL.Width = 40;
            // 
            // ReservationId
            // 
            this.ReservationId.DataPropertyName = "ReserveId";
            this.ReservationId.HeaderText = "ReservationId";
            this.ReservationId.Name = "ReservationId";
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "FirstName";
            this.Customer.HeaderText = "Customer";
            this.Customer.Name = "Customer";
            // 
            // contacts
            // 
            this.contacts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.contacts.DataPropertyName = "Phone";
            this.contacts.HeaderText = "Contacts";
            this.contacts.Name = "contacts";
            this.contacts.Width = 140;
            // 
            // noOfPerson
            // 
            this.noOfPerson.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.noOfPerson.DataPropertyName = "noOfPeople";
            this.noOfPerson.HeaderText = "No Of Person";
            this.noOfPerson.Name = "noOfPerson";
            // 
            // ReservedTime
            // 
            this.ReservedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ReservedTime.DataPropertyName = "reservedDate";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.ReservedTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.ReservedTime.HeaderText = "Reservation Time";
            this.ReservedTime.Name = "ReservedTime";
            this.ReservedTime.Width = 160;
            // 
            // Notes
            // 
            this.Notes.DataPropertyName = "specialNotes";
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Reservation Type";
            this.Type.Name = "Type";
            this.Type.Visible = false;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // online_reservation_id
            // 
            this.online_reservation_id.DataPropertyName = "online_reservation_id";
            this.online_reservation_id.HeaderText = "online_reservation_id";
            this.online_reservation_id.Name = "online_reservation_id";
            this.online_reservation_id.Visible = false;
            // 
            // Arrived
            // 
            this.Arrived.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Arrived.DataPropertyName = "Arrived";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Arrived.DefaultCellStyle = dataGridViewCellStyle4;
            this.Arrived.HeaderText = "IsArrived ";
            this.Arrived.Name = "Arrived";
            this.Arrived.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Arrived.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Arrived.Text = "";
            this.Arrived.Visible = false;
            // 
            // Accept
            // 
            this.Accept.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            this.Accept.DefaultCellStyle = dataGridViewCellStyle5;
            this.Accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Accept.HeaderText = "Accept";
            this.Accept.Name = "Accept";
            this.Accept.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Accept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Accept.Text = "Accept";
            this.Accept.ToolTipText = "Accept";
            this.Accept.UseColumnTextForButtonValue = true;
            this.Accept.Width = 121;
            // 
            // Reject
            // 
            this.Reject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.Reject.DefaultCellStyle = dataGridViewCellStyle6;
            this.Reject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reject.HeaderText = "Reject";
            this.Reject.Name = "Reject";
            this.Reject.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Reject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Reject.Text = "Reject";
            this.Reject.ToolTipText = "Reject";
            this.Reject.UseColumnTextForButtonValue = true;
            this.Reject.Width = 121;
            // 
            // edit
            // 
            this.edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.edit.DefaultCellStyle = dataGridViewCellStyle7;
            this.edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edit.HeaderText = "Edit";
            this.edit.Name = "edit";
            this.edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.edit.Text = "Edit";
            this.edit.ToolTipText = "Update";
            this.edit.UseColumnTextForButtonValue = true;
            this.edit.Width = 121;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle11.NullValue")));
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewImageColumn1.HeaderText = "Accept";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.ToolTipText = "Accept";
            this.dataGridViewImageColumn1.Width = 121;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle12.NullValue")));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewImageColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewImageColumn2.HeaderText = "Reject";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 124;
            // 
            // ReservationTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.reservationDataGridView);
            this.Controls.Add(this.panel1);
            this.Name = "ReservationTable";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(1020, 573);
            this.Load += new System.EventHandler(this.ReservationTable_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reservationDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadReservation;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView reservationDataGridView;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button newReservationButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label totalPendingOrderLabel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label totalAcceptedOrderPersonlabel;
        private System.Windows.Forms.Label totalAcceptOrderLabel;
        private System.Windows.Forms.Label totalPendingOrderPersonlabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReservationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn contacts;
        private System.Windows.Forms.DataGridViewTextBoxColumn noOfPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReservedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn online_reservation_id;
        private System.Windows.Forms.DataGridViewButtonColumn Arrived;
        private System.Windows.Forms.DataGridViewButtonColumn Accept;
        private System.Windows.Forms.DataGridViewButtonColumn Reject;
        private System.Windows.Forms.DataGridViewButtonColumn edit;
    }
}
