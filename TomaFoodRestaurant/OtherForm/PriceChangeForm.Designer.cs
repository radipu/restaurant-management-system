namespace TomaFoodRestaurant.OtherForm
{
    partial class PriceChangeForm
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceChangeForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cmbSearchRcptName = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbCategory = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.windowsUIButtonPanel1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSearchRcptName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Controls.Add(this.panel1);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(997, 493);
            this.groupControl1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 105);
            this.gridControl1.MainView = this.gridView2;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.gridControl1.Size = new System.Drawing.Size(997, 388);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.ActiveFilterEnabled = false;
            this.gridView2.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gridView2.Appearance.EvenRow.BackColor2 = System.Drawing.Color.AliceBlue;
            this.gridView2.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView2.Appearance.EvenRow.Options.UseTextOptions = true;
            this.gridView2.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.OddRow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gridView2.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView2.Appearance.OddRow.Options.UseTextOptions = true;
            this.gridView2.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView2.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.DimGray;
            this.gridView2.AppearancePrint.EvenRow.BackColor2 = System.Drawing.Color.White;
            this.gridView2.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView2.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridView2.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gridView2.AppearancePrint.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gridView2.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.Yellow;
            this.gridView2.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.gridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView2.OptionsFind.AlwaysVisible = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView2.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView2.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView2_RowClick);
            this.gridView2.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView2_RowCellClick);
            this.gridView2.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView2_SelectionChanged);
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView2_FocusedRowChanged);
            this.gridView2.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView2_CellValueChanged);
            this.gridView2.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView2_CellValueChanging);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "RECEPT NAME";
            this.gridColumn1.FieldName = "ReceiptName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 471;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "IN PRICE";
            this.gridColumn2.DisplayFormat.FormatString = "F2";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "InPrice";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 241;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn3.Caption = "OUT PRICE";
            this.gridColumn3.DisplayFormat.FormatString = "F2";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "OutPrice";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.AllowSize = false;
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 196;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Action";
            this.gridColumn4.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSize = false;
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Width = 87;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("repositoryItemHyperLinkEdit1.Appearance.Image")));
            this.repositoryItemHyperLinkEdit1.Appearance.Options.UseImage = true;
            this.repositoryItemHyperLinkEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemHyperLinkEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Image = ((System.Drawing.Image)(resources.GetObject("repositoryItemHyperLinkEdit1.Image")));
            this.repositoryItemHyperLinkEdit1.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBoxCategory);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.cmbSearchRcptName);
            this.panel1.Controls.Add(this.cmbCategory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(997, 55);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Category";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(102, 14);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(207, 31);
            this.comboBoxCategory.TabIndex = 3;
            this.comboBoxCategory.SelectedValueChanged += new System.EventHandler(this.cmbCategory_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(153)))), ((int)(((byte)(177)))));
            this.simpleButton1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(192)))), ((int)(((byte)(222)))));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseBorderColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(813, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(178, 43);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "CHANGE PRICE";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cmbSearchRcptName
            // 
            this.cmbSearchRcptName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSearchRcptName.EditValue = "Search By Receipt Name";
            this.cmbSearchRcptName.Location = new System.Drawing.Point(402, -4);
            this.cmbSearchRcptName.Name = "cmbSearchRcptName";
            this.cmbSearchRcptName.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbSearchRcptName.Properties.Appearance.BorderColor = System.Drawing.Color.Red;
            this.cmbSearchRcptName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.cmbSearchRcptName.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.cmbSearchRcptName.Properties.Appearance.Options.UseBackColor = true;
            this.cmbSearchRcptName.Properties.Appearance.Options.UseBorderColor = true;
            this.cmbSearchRcptName.Properties.Appearance.Options.UseFont = true;
            this.cmbSearchRcptName.Properties.Appearance.Options.UseForeColor = true;
            this.cmbSearchRcptName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("cmbSearchRcptName.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.cmbSearchRcptName.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.cmbSearchRcptName.Properties.NullText = "Search By Receipt Name";
            this.cmbSearchRcptName.Properties.PopupFormSize = new System.Drawing.Size(258, 0);
            this.cmbSearchRcptName.Properties.ShowClearButton = false;
            this.cmbSearchRcptName.Properties.ShowFooter = false;
            this.cmbSearchRcptName.Properties.View = this.gridView1;
            this.cmbSearchRcptName.Size = new System.Drawing.Size(390, 32);
            this.cmbSearchRcptName.TabIndex = 1;
            this.cmbSearchRcptName.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCategory.EditValue = "Select Category";
            this.cmbCategory.Location = new System.Drawing.Point(559, 18);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbCategory.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.cmbCategory.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCategory.Properties.Appearance.Options.UseFont = true;
            this.cmbCategory.Properties.Appearance.Options.UseForeColor = true;
            this.cmbCategory.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbCategory.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("cmbCategory.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.cmbCategory.Properties.NullText = "Select Category";
            this.cmbCategory.Properties.PopupFormSize = new System.Drawing.Size(300, 250);
            this.cmbCategory.Properties.ShowClearButton = false;
            this.cmbCategory.Properties.ShowFooter = false;
            this.cmbCategory.Properties.View = this.searchLookUpEdit1View;
            this.cmbCategory.Size = new System.Drawing.Size(217, 32);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.Visible = false;
            this.cmbCategory.EditValueChanged += new System.EventHandler(this.cmbCategory_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CategoryName,
            this.CategoryId});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // CategoryName
            // 
            this.CategoryName.Caption = "CategoryName";
            this.CategoryName.FieldName = "CategoryName";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.Visible = true;
            this.CategoryName.VisibleIndex = 0;
            // 
            // CategoryId
            // 
            this.CategoryId.Caption = "CategoryId";
            this.CategoryId.FieldName = "CategoryId";
            this.CategoryId.Name = "CategoryId";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(220)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.windowsUIButtonPanel1);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(997, 50);
            this.panelControl1.TabIndex = 0;
            // 
            // windowsUIButtonPanel1
            // 
            this.windowsUIButtonPanel1.AppearanceButton.Normal.BackColor = System.Drawing.Color.Red;
            this.windowsUIButtonPanel1.AppearanceButton.Normal.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.windowsUIButtonPanel1.AppearanceButton.Normal.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.windowsUIButtonPanel1.AppearanceButton.Normal.Options.UseBackColor = true;
            this.windowsUIButtonPanel1.AppearanceButton.Normal.Options.UseForeColor = true;
            this.windowsUIButtonPanel1.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", ((System.Drawing.Image)(resources.GetObject("windowsUIButtonPanel1.Buttons"))), -1, DevExpress.XtraBars.Docking2010.ImageLocation.BeforeText, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", false, -1, true, null, true, false, true, null, null, -1, false, false)});
            this.windowsUIButtonPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.windowsUIButtonPanel1.Location = new System.Drawing.Point(0, 0);
            this.windowsUIButtonPanel1.Name = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Size = new System.Drawing.Size(65, 50);
            this.windowsUIButtonPanel1.TabIndex = 1;
            this.windowsUIButtonPanel1.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(556, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menus/Foods";
            // 
            // PriceChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 493);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PriceChangeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PriceChangeForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PriceChangeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSearchRcptName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbSearchRcptName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn CategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn CategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label label2;
    }
}