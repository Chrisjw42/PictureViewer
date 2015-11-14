namespace WfaPictureViewer
{
    partial class BatchSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param deaultName="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabBatch = new System.Windows.Forms.TabControl();
            this.tabFile = new System.Windows.Forms.TabPage();
            this.batchFileFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.batchFileFlowHead = new System.Windows.Forms.Panel();
            this.chkBatchFileType = new System.Windows.Forms.Panel();
            this.grpBatchFileName = new System.Windows.Forms.GroupBox();
            this.radBatchFileNameCurrent = new System.Windows.Forms.RadioButton();
            this.radBatchFileNameNew = new System.Windows.Forms.RadioButton();
            this.txtBatchFileName = new System.Windows.Forms.TextBox();
            this.grpBatchFileFolder = new System.Windows.Forms.GroupBox();
            this.radBatchFileFolderCurrent = new System.Windows.Forms.RadioButton();
            this.radBatchFileFolderNew = new System.Windows.Forms.RadioButton();
            this.btnBatchFileExportGetPath = new System.Windows.Forms.Button();
            this.chkBatchFileExport = new System.Windows.Forms.CheckBox();
            this.comboBatchFileExportType = new System.Windows.Forms.ComboBox();
            this.lblBatchFileExportType = new System.Windows.Forms.Label();
            this.batchFileFlowSelection = new System.Windows.Forms.Panel();
            this.lblBatchFileImagesInstructions = new System.Windows.Forms.Label();
            this.chkBatchFileProcessAll = new System.Windows.Forms.CheckBox();
            this.batchFileSelectionList = new System.Windows.Forms.CheckedListBox();
            this.tabTransform = new System.Windows.Forms.TabPage();
            this.chkBatchTransform = new System.Windows.Forms.CheckBox();
            this.tabAdjustments = new System.Windows.Forms.TabPage();
            this.batchAdjFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.batchAdjFlowHead = new System.Windows.Forms.Panel();
            this.lblBatchAdjInstructions = new System.Windows.Forms.Label();
            this.batchAdjFlowTransparency = new System.Windows.Forms.Panel();
            this.txtBatchAdjTransparencyInput = new System.Windows.Forms.NumericUpDown();
            this.lblBatchAdjTransparencyInstructions = new System.Windows.Forms.Label();
            this.chkBatchAdjTransparency = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.batchFilterFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBatchEffectsIstructions = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radBatchFilterGrayscaleAvg = new System.Windows.Forms.RadioButton();
            this.chkBatchFilterGrayscale = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radBatchFilterGrayscaleLum = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkBatchFilterSepia = new System.Windows.Forms.CheckBox();
            this.tabChannels = new System.Windows.Forms.TabPage();
            this.lblBatchChannelsType = new System.Windows.Forms.Label();
            this.comboBatchChannelsType = new System.Windows.Forms.ComboBox();
            this.chkBatchChannelsBypass = new System.Windows.Forms.CheckBox();
            this.chkBatchChannelsAlpha = new System.Windows.Forms.CheckBox();
            this.btnBatchChannelsAll = new System.Windows.Forms.Button();
            this.lblBatchChannelsSelect = new System.Windows.Forms.Label();
            this.btnBatchChannelsA = new System.Windows.Forms.Button();
            this.btnBatchChannelsB = new System.Windows.Forms.Button();
            this.btnBatchChannelsG = new System.Windows.Forms.Button();
            this.btnBatchChannelsR = new System.Windows.Forms.Button();
            this.btnBatch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblBatchFileFolder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabBatch.SuspendLayout();
            this.tabFile.SuspendLayout();
            this.batchFileFlow.SuspendLayout();
            this.chkBatchFileType.SuspendLayout();
            this.grpBatchFileName.SuspendLayout();
            this.grpBatchFileFolder.SuspendLayout();
            this.batchFileFlowSelection.SuspendLayout();
            this.tabTransform.SuspendLayout();
            this.tabAdjustments.SuspendLayout();
            this.batchAdjFlow.SuspendLayout();
            this.batchAdjFlowHead.SuspendLayout();
            this.batchAdjFlowTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchAdjTransparencyInput)).BeginInit();
            this.tabFilters.SuspendLayout();
            this.batchFilterFlow.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabChannels.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBatch
            // 
            this.tabBatch.Controls.Add(this.tabFile);
            this.tabBatch.Controls.Add(this.tabTransform);
            this.tabBatch.Controls.Add(this.tabAdjustments);
            this.tabBatch.Controls.Add(this.tabFilters);
            this.tabBatch.Controls.Add(this.tabChannels);
            this.tabBatch.Location = new System.Drawing.Point(4, 3);
            this.tabBatch.Name = "tabBatch";
            this.tabBatch.SelectedIndex = 0;
            this.tabBatch.Size = new System.Drawing.Size(589, 471);
            this.tabBatch.TabIndex = 9;
            // 
            // tabFile
            // 
            this.tabFile.Controls.Add(this.batchFileFlow);
            this.tabFile.Location = new System.Drawing.Point(4, 22);
            this.tabFile.Name = "tabFile";
            this.tabFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabFile.Size = new System.Drawing.Size(581, 445);
            this.tabFile.TabIndex = 0;
            this.tabFile.Text = "File";
            this.tabFile.UseVisualStyleBackColor = true;
            // 
            // batchFileFlow
            // 
            this.batchFileFlow.AutoScroll = true;
            this.batchFileFlow.Controls.Add(this.batchFileFlowHead);
            this.batchFileFlow.Controls.Add(this.chkBatchFileType);
            this.batchFileFlow.Controls.Add(this.batchFileFlowSelection);
            this.batchFileFlow.Dock = System.Windows.Forms.DockStyle.Top;
            this.batchFileFlow.Location = new System.Drawing.Point(3, 3);
            this.batchFileFlow.Name = "batchFileFlow";
            this.batchFileFlow.Size = new System.Drawing.Size(575, 422);
            this.batchFileFlow.TabIndex = 0;
            // 
            // batchFileFlowHead
            // 
            this.batchFileFlowHead.BackColor = System.Drawing.Color.WhiteSmoke;
            this.batchFileFlowHead.Location = new System.Drawing.Point(3, 3);
            this.batchFileFlowHead.Name = "batchFileFlowHead";
            this.batchFileFlowHead.Size = new System.Drawing.Size(550, 25);
            this.batchFileFlowHead.TabIndex = 0;
            // 
            // chkBatchFileType
            // 
            this.chkBatchFileType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkBatchFileType.Controls.Add(this.grpBatchFileName);
            this.chkBatchFileType.Controls.Add(this.grpBatchFileFolder);
            this.chkBatchFileType.Controls.Add(this.chkBatchFileExport);
            this.chkBatchFileType.Controls.Add(this.comboBatchFileExportType);
            this.chkBatchFileType.Controls.Add(this.lblBatchFileExportType);
            this.chkBatchFileType.Location = new System.Drawing.Point(3, 34);
            this.chkBatchFileType.Name = "chkBatchFileType";
            this.chkBatchFileType.Size = new System.Drawing.Size(550, 151);
            this.chkBatchFileType.TabIndex = 1;
            // 
            // grpBatchFileName
            // 
            this.grpBatchFileName.Controls.Add(this.radBatchFileNameCurrent);
            this.grpBatchFileName.Controls.Add(this.radBatchFileNameNew);
            this.grpBatchFileName.Controls.Add(this.txtBatchFileName);
            this.grpBatchFileName.Location = new System.Drawing.Point(12, 100);
            this.grpBatchFileName.Name = "grpBatchFileName";
            this.grpBatchFileName.Size = new System.Drawing.Size(526, 36);
            this.grpBatchFileName.TabIndex = 42;
            this.grpBatchFileName.TabStop = false;
            // 
            // radBatchFileNameCurrent
            // 
            this.radBatchFileNameCurrent.AutoSize = true;
            this.radBatchFileNameCurrent.Checked = true;
            this.radBatchFileNameCurrent.Location = new System.Drawing.Point(6, 12);
            this.radBatchFileNameCurrent.Name = "radBatchFileNameCurrent";
            this.radBatchFileNameCurrent.Size = new System.Drawing.Size(125, 17);
            this.radBatchFileNameCurrent.TabIndex = 38;
            this.radBatchFileNameCurrent.TabStop = true;
            this.radBatchFileNameCurrent.Text = "Use current file name";
            this.radBatchFileNameCurrent.UseVisualStyleBackColor = true;
            this.radBatchFileNameCurrent.CheckedChanged += new System.EventHandler(this.radBatchFileNameCurrent_CheckedChanged);
            // 
            // radBatchFileNameNew
            // 
            this.radBatchFileNameNew.AutoSize = true;
            this.radBatchFileNameNew.Location = new System.Drawing.Point(201, 12);
            this.radBatchFileNameNew.Name = "radBatchFileNameNew";
            this.radBatchFileNameNew.Size = new System.Drawing.Size(53, 17);
            this.radBatchFileNameNew.TabIndex = 39;
            this.radBatchFileNameNew.Text = "New: ";
            this.radBatchFileNameNew.UseVisualStyleBackColor = true;
            this.radBatchFileNameNew.CheckedChanged += new System.EventHandler(this.radBatchFileNameNew_CheckedChanged);
            // 
            // txtBatchFileName
            // 
            this.txtBatchFileName.Location = new System.Drawing.Point(259, 10);
            this.txtBatchFileName.Name = "txtBatchFileName";
            this.txtBatchFileName.Size = new System.Drawing.Size(261, 20);
            this.txtBatchFileName.TabIndex = 29;
            this.txtBatchFileName.TextChanged += new System.EventHandler(this.txtBatchFileName_TextChanged);
            // 
            // grpBatchFileFolder
            // 
            this.grpBatchFileFolder.Controls.Add(this.lblBatchFileFolder);
            this.grpBatchFileFolder.Controls.Add(this.radBatchFileFolderCurrent);
            this.grpBatchFileFolder.Controls.Add(this.radBatchFileFolderNew);
            this.grpBatchFileFolder.Controls.Add(this.btnBatchFileExportGetPath);
            this.grpBatchFileFolder.Location = new System.Drawing.Point(12, 62);
            this.grpBatchFileFolder.Name = "grpBatchFileFolder";
            this.grpBatchFileFolder.Size = new System.Drawing.Size(526, 36);
            this.grpBatchFileFolder.TabIndex = 41;
            this.grpBatchFileFolder.TabStop = false;
            // 
            // radBatchFileFolderCurrent
            // 
            this.radBatchFileFolderCurrent.AutoSize = true;
            this.radBatchFileFolderCurrent.Checked = true;
            this.radBatchFileFolderCurrent.Location = new System.Drawing.Point(6, 12);
            this.radBatchFileFolderCurrent.Name = "radBatchFileFolderCurrent";
            this.radBatchFileFolderCurrent.Size = new System.Drawing.Size(184, 17);
            this.radBatchFileFolderCurrent.TabIndex = 38;
            this.radBatchFileFolderCurrent.TabStop = true;
            this.radBatchFileFolderCurrent.Text = "Export to image\'s current directory";
            this.radBatchFileFolderCurrent.UseVisualStyleBackColor = true;
            this.radBatchFileFolderCurrent.CheckedChanged += new System.EventHandler(this.radBatchFileFolderCurrent_CheckedChanged);
            // 
            // radBatchFileFolderNew
            // 
            this.radBatchFileFolderNew.AutoSize = true;
            this.radBatchFileFolderNew.Location = new System.Drawing.Point(201, 12);
            this.radBatchFileFolderNew.Name = "radBatchFileFolderNew";
            this.radBatchFileFolderNew.Size = new System.Drawing.Size(50, 17);
            this.radBatchFileFolderNew.TabIndex = 39;
            this.radBatchFileFolderNew.Text = "New:";
            this.radBatchFileFolderNew.UseVisualStyleBackColor = true;
            this.radBatchFileFolderNew.CheckedChanged += new System.EventHandler(this.radBatchFileFolderNew_CheckedChanged);
            // 
            // btnBatchFileExportGetPath
            // 
            this.btnBatchFileExportGetPath.Location = new System.Drawing.Point(259, 10);
            this.btnBatchFileExportGetPath.Name = "btnBatchFileExportGetPath";
            this.btnBatchFileExportGetPath.Size = new System.Drawing.Size(28, 20);
            this.btnBatchFileExportGetPath.TabIndex = 35;
            this.btnBatchFileExportGetPath.Text = "...";
            this.btnBatchFileExportGetPath.UseVisualStyleBackColor = true;
            this.btnBatchFileExportGetPath.Click += new System.EventHandler(this.btnBatchFileExportGetPath_Click);
            // 
            // chkBatchFileExport
            // 
            this.chkBatchFileExport.AutoSize = true;
            this.chkBatchFileExport.Checked = true;
            this.chkBatchFileExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBatchFileExport.Location = new System.Drawing.Point(8, 10);
            this.chkBatchFileExport.Name = "chkBatchFileExport";
            this.chkBatchFileExport.Size = new System.Drawing.Size(98, 17);
            this.chkBatchFileExport.TabIndex = 34;
            this.chkBatchFileExport.Text = "Export to Files?";
            this.chkBatchFileExport.UseVisualStyleBackColor = true;
            this.chkBatchFileExport.CheckedChanged += new System.EventHandler(this.chkBatchFileExport_CheckedChanged);
            // 
            // comboBatchFileExportType
            // 
            this.comboBatchFileExportType.FormattingEnabled = true;
            this.comboBatchFileExportType.Items.AddRange(new object[] {
            ".Jpg",
            ".Bmp",
            ".Png",
            ".Tiff"});
            this.comboBatchFileExportType.Location = new System.Drawing.Point(68, 34);
            this.comboBatchFileExportType.Name = "comboBatchFileExportType";
            this.comboBatchFileExportType.Size = new System.Drawing.Size(64, 21);
            this.comboBatchFileExportType.TabIndex = 31;
            // 
            // lblBatchFileExportType
            // 
            this.lblBatchFileExportType.AutoSize = true;
            this.lblBatchFileExportType.Location = new System.Drawing.Point(9, 37);
            this.lblBatchFileExportType.Name = "lblBatchFileExportType";
            this.lblBatchFileExportType.Size = new System.Drawing.Size(50, 13);
            this.lblBatchFileExportType.TabIndex = 32;
            this.lblBatchFileExportType.Text = "FileType:";
            // 
            // batchFileFlowSelection
            // 
            this.batchFileFlowSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.batchFileFlowSelection.Controls.Add(this.lblBatchFileImagesInstructions);
            this.batchFileFlowSelection.Controls.Add(this.chkBatchFileProcessAll);
            this.batchFileFlowSelection.Controls.Add(this.batchFileSelectionList);
            this.batchFileFlowSelection.Location = new System.Drawing.Point(3, 191);
            this.batchFileFlowSelection.Name = "batchFileFlowSelection";
            this.batchFileFlowSelection.Size = new System.Drawing.Size(550, 235);
            this.batchFileFlowSelection.TabIndex = 2;
            // 
            // lblBatchFileImagesInstructions
            // 
            this.lblBatchFileImagesInstructions.AutoSize = true;
            this.lblBatchFileImagesInstructions.Location = new System.Drawing.Point(15, 6);
            this.lblBatchFileImagesInstructions.Name = "lblBatchFileImagesInstructions";
            this.lblBatchFileImagesInstructions.Size = new System.Drawing.Size(128, 13);
            this.lblBatchFileImagesInstructions.TabIndex = 39;
            this.lblBatchFileImagesInstructions.Text = "Select images to process:";
            // 
            // chkBatchFileProcessAll
            // 
            this.chkBatchFileProcessAll.AutoSize = true;
            this.chkBatchFileProcessAll.Checked = true;
            this.chkBatchFileProcessAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBatchFileProcessAll.Location = new System.Drawing.Point(32, 26);
            this.chkBatchFileProcessAll.Name = "chkBatchFileProcessAll";
            this.chkBatchFileProcessAll.Size = new System.Drawing.Size(78, 17);
            this.chkBatchFileProcessAll.TabIndex = 38;
            this.chkBatchFileProcessAll.Text = "Process All";
            this.chkBatchFileProcessAll.UseVisualStyleBackColor = true;
            this.chkBatchFileProcessAll.CheckedChanged += new System.EventHandler(this.chkBatchFileProcessAll_CheckedChanged);
            // 
            // batchFileSelectionList
            // 
            this.batchFileSelectionList.BackColor = System.Drawing.SystemColors.Window;
            this.batchFileSelectionList.CheckOnClick = true;
            this.batchFileSelectionList.FormattingEnabled = true;
            this.batchFileSelectionList.Items.AddRange(new object[] {
            "This may ",
            "Or may not",
            "Be the optimum controller",
            "For this purpose"});
            this.batchFileSelectionList.Location = new System.Drawing.Point(15, 49);
            this.batchFileSelectionList.Name = "batchFileSelectionList";
            this.batchFileSelectionList.Size = new System.Drawing.Size(486, 169);
            this.batchFileSelectionList.TabIndex = 11;
            this.batchFileSelectionList.SelectedIndexChanged += new System.EventHandler(this.batchFileSelectionList_SelectedIndexChanged);
            // 
            // tabTransform
            // 
            this.tabTransform.Controls.Add(this.chkBatchTransform);
            this.tabTransform.Location = new System.Drawing.Point(4, 22);
            this.tabTransform.Name = "tabTransform";
            this.tabTransform.Size = new System.Drawing.Size(581, 445);
            this.tabTransform.TabIndex = 4;
            this.tabTransform.Text = "Transform";
            this.tabTransform.UseVisualStyleBackColor = true;
            // 
            // chkBatchTransform
            // 
            this.chkBatchTransform.AutoSize = true;
            this.chkBatchTransform.Location = new System.Drawing.Point(34, 24);
            this.chkBatchTransform.Name = "chkBatchTransform";
            this.chkBatchTransform.Size = new System.Drawing.Size(93, 17);
            this.chkBatchTransform.TabIndex = 0;
            this.chkBatchTransform.Text = "Transformers?";
            this.chkBatchTransform.UseVisualStyleBackColor = true;
            this.chkBatchTransform.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabAdjustments
            // 
            this.tabAdjustments.Controls.Add(this.batchAdjFlow);
            this.tabAdjustments.Location = new System.Drawing.Point(4, 22);
            this.tabAdjustments.Name = "tabAdjustments";
            this.tabAdjustments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdjustments.Size = new System.Drawing.Size(581, 445);
            this.tabAdjustments.TabIndex = 3;
            this.tabAdjustments.Text = "Adjustments";
            this.tabAdjustments.UseVisualStyleBackColor = true;
            // 
            // batchAdjFlow
            // 
            this.batchAdjFlow.AutoScroll = true;
            this.batchAdjFlow.Controls.Add(this.batchAdjFlowHead);
            this.batchAdjFlow.Controls.Add(this.batchAdjFlowTransparency);
            this.batchAdjFlow.Controls.Add(this.panel3);
            this.batchAdjFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.batchAdjFlow.Location = new System.Drawing.Point(3, 3);
            this.batchAdjFlow.Name = "batchAdjFlow";
            this.batchAdjFlow.Size = new System.Drawing.Size(575, 439);
            this.batchAdjFlow.TabIndex = 0;
            // 
            // batchAdjFlowHead
            // 
            this.batchAdjFlowHead.BackColor = System.Drawing.Color.WhiteSmoke;
            this.batchAdjFlowHead.Controls.Add(this.lblBatchAdjInstructions);
            this.batchAdjFlowHead.Location = new System.Drawing.Point(3, 3);
            this.batchAdjFlowHead.Name = "batchAdjFlowHead";
            this.batchAdjFlowHead.Size = new System.Drawing.Size(550, 25);
            this.batchAdjFlowHead.TabIndex = 0;
            // 
            // lblBatchAdjInstructions
            // 
            this.lblBatchAdjInstructions.AutoSize = true;
            this.lblBatchAdjInstructions.Location = new System.Drawing.Point(6, 6);
            this.lblBatchAdjInstructions.Name = "lblBatchAdjInstructions";
            this.lblBatchAdjInstructions.Size = new System.Drawing.Size(170, 13);
            this.lblBatchAdjInstructions.TabIndex = 1;
            this.lblBatchAdjInstructions.Text = "Select which adjustments to apply:";
            // 
            // batchAdjFlowTransparency
            // 
            this.batchAdjFlowTransparency.BackColor = System.Drawing.Color.WhiteSmoke;
            this.batchAdjFlowTransparency.Controls.Add(this.txtBatchAdjTransparencyInput);
            this.batchAdjFlowTransparency.Controls.Add(this.lblBatchAdjTransparencyInstructions);
            this.batchAdjFlowTransparency.Controls.Add(this.chkBatchAdjTransparency);
            this.batchAdjFlowTransparency.Location = new System.Drawing.Point(3, 34);
            this.batchAdjFlowTransparency.Name = "batchAdjFlowTransparency";
            this.batchAdjFlowTransparency.Size = new System.Drawing.Size(550, 100);
            this.batchAdjFlowTransparency.TabIndex = 1;
            // 
            // txtBatchAdjTransparencyInput
            // 
            this.txtBatchAdjTransparencyInput.Location = new System.Drawing.Point(197, 54);
            this.txtBatchAdjTransparencyInput.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.txtBatchAdjTransparencyInput.Name = "txtBatchAdjTransparencyInput";
            this.txtBatchAdjTransparencyInput.Size = new System.Drawing.Size(120, 20);
            this.txtBatchAdjTransparencyInput.TabIndex = 12;
            // 
            // lblBatchAdjTransparencyInstructions
            // 
            this.lblBatchAdjTransparencyInstructions.AutoSize = true;
            this.lblBatchAdjTransparencyInstructions.Location = new System.Drawing.Point(179, 29);
            this.lblBatchAdjTransparencyInstructions.Name = "lblBatchAdjTransparencyInstructions";
            this.lblBatchAdjTransparencyInstructions.Size = new System.Drawing.Size(166, 13);
            this.lblBatchAdjTransparencyInstructions.TabIndex = 10;
            this.lblBatchAdjTransparencyInstructions.Text = "Select transparency value (0-255)";
            // 
            // chkBatchAdjTransparency
            // 
            this.chkBatchAdjTransparency.AutoSize = true;
            this.chkBatchAdjTransparency.Location = new System.Drawing.Point(7, 6);
            this.chkBatchAdjTransparency.Name = "chkBatchAdjTransparency";
            this.chkBatchAdjTransparency.Size = new System.Drawing.Size(91, 17);
            this.chkBatchAdjTransparency.TabIndex = 9;
            this.chkBatchAdjTransparency.Text = "Transparency";
            this.chkBatchAdjTransparency.UseVisualStyleBackColor = true;
            this.chkBatchAdjTransparency.CheckedChanged += new System.EventHandler(this.chkBatchEffectBrightness_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Location = new System.Drawing.Point(3, 140);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(550, 100);
            this.panel3.TabIndex = 1;
            // 
            // tabFilters
            // 
            this.tabFilters.Controls.Add(this.batchFilterFlow);
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilters.Size = new System.Drawing.Size(581, 445);
            this.tabFilters.TabIndex = 1;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // batchFilterFlow
            // 
            this.batchFilterFlow.AutoScroll = true;
            this.batchFilterFlow.Controls.Add(this.panel1);
            this.batchFilterFlow.Controls.Add(this.panel2);
            this.batchFilterFlow.Controls.Add(this.panel4);
            this.batchFilterFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.batchFilterFlow.Location = new System.Drawing.Point(3, 3);
            this.batchFilterFlow.Name = "batchFilterFlow";
            this.batchFilterFlow.Size = new System.Drawing.Size(575, 439);
            this.batchFilterFlow.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.lblBatchEffectsIstructions);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 25);
            this.panel1.TabIndex = 0;
            // 
            // lblBatchEffectsIstructions
            // 
            this.lblBatchEffectsIstructions.AutoSize = true;
            this.lblBatchEffectsIstructions.Location = new System.Drawing.Point(6, 6);
            this.lblBatchEffectsIstructions.Name = "lblBatchEffectsIstructions";
            this.lblBatchEffectsIstructions.Size = new System.Drawing.Size(138, 13);
            this.lblBatchEffectsIstructions.TabIndex = 4;
            this.lblBatchEffectsIstructions.Text = "Select which filters to apply:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.radBatchFilterGrayscaleAvg);
            this.panel2.Controls.Add(this.chkBatchFilterGrayscale);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.radBatchFilterGrayscaleLum);
            this.panel2.Location = new System.Drawing.Point(3, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 100);
            this.panel2.TabIndex = 1;
            // 
            // radBatchFilterGrayscaleAvg
            // 
            this.radBatchFilterGrayscaleAvg.AutoSize = true;
            this.radBatchFilterGrayscaleAvg.Location = new System.Drawing.Point(255, 48);
            this.radBatchFilterGrayscaleAvg.Name = "radBatchFilterGrayscaleAvg";
            this.radBatchFilterGrayscaleAvg.Size = new System.Drawing.Size(65, 17);
            this.radBatchFilterGrayscaleAvg.TabIndex = 15;
            this.radBatchFilterGrayscaleAvg.Text = "Average";
            this.radBatchFilterGrayscaleAvg.UseVisualStyleBackColor = true;
            // 
            // chkBatchFilterGrayscale
            // 
            this.chkBatchFilterGrayscale.AutoSize = true;
            this.chkBatchFilterGrayscale.Location = new System.Drawing.Point(3, 3);
            this.chkBatchFilterGrayscale.Name = "chkBatchFilterGrayscale";
            this.chkBatchFilterGrayscale.Size = new System.Drawing.Size(73, 17);
            this.chkBatchFilterGrayscale.TabIndex = 10;
            this.chkBatchFilterGrayscale.Text = "Grayscale";
            this.chkBatchFilterGrayscale.UseVisualStyleBackColor = true;
            this.chkBatchFilterGrayscale.CheckedChanged += new System.EventHandler(this.chkBatchEffectGrayscale_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Please select a conversion algorithm:";
            // 
            // radBatchFilterGrayscaleLum
            // 
            this.radBatchFilterGrayscaleLum.AutoSize = true;
            this.radBatchFilterGrayscaleLum.Checked = true;
            this.radBatchFilterGrayscaleLum.Location = new System.Drawing.Point(175, 48);
            this.radBatchFilterGrayscaleLum.Name = "radBatchFilterGrayscaleLum";
            this.radBatchFilterGrayscaleLum.Size = new System.Drawing.Size(74, 17);
            this.radBatchFilterGrayscaleLum.TabIndex = 14;
            this.radBatchFilterGrayscaleLum.TabStop = true;
            this.radBatchFilterGrayscaleLum.Text = "Luminosity";
            this.radBatchFilterGrayscaleLum.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.chkBatchFilterSepia);
            this.panel4.Location = new System.Drawing.Point(3, 140);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(550, 100);
            this.panel4.TabIndex = 1;
            // 
            // chkBatchFilterSepia
            // 
            this.chkBatchFilterSepia.AutoSize = true;
            this.chkBatchFilterSepia.Location = new System.Drawing.Point(3, 3);
            this.chkBatchFilterSepia.Name = "chkBatchFilterSepia";
            this.chkBatchFilterSepia.Size = new System.Drawing.Size(53, 17);
            this.chkBatchFilterSepia.TabIndex = 9;
            this.chkBatchFilterSepia.Text = "Sepia";
            this.chkBatchFilterSepia.UseVisualStyleBackColor = true;
            this.chkBatchFilterSepia.CheckedChanged += new System.EventHandler(this.chkBatchEffectSepia_CheckedChanged);
            // 
            // tabChannels
            // 
            this.tabChannels.Controls.Add(this.lblBatchChannelsType);
            this.tabChannels.Controls.Add(this.comboBatchChannelsType);
            this.tabChannels.Controls.Add(this.chkBatchChannelsBypass);
            this.tabChannels.Controls.Add(this.chkBatchChannelsAlpha);
            this.tabChannels.Controls.Add(this.btnBatchChannelsAll);
            this.tabChannels.Controls.Add(this.lblBatchChannelsSelect);
            this.tabChannels.Controls.Add(this.btnBatchChannelsA);
            this.tabChannels.Controls.Add(this.btnBatchChannelsB);
            this.tabChannels.Controls.Add(this.btnBatchChannelsG);
            this.tabChannels.Controls.Add(this.btnBatchChannelsR);
            this.tabChannels.Location = new System.Drawing.Point(4, 22);
            this.tabChannels.Name = "tabChannels";
            this.tabChannels.Size = new System.Drawing.Size(581, 445);
            this.tabChannels.TabIndex = 2;
            this.tabChannels.Text = "Channels";
            this.tabChannels.UseVisualStyleBackColor = true;
            // 
            // lblBatchChannelsType
            // 
            this.lblBatchChannelsType.AutoSize = true;
            this.lblBatchChannelsType.Location = new System.Drawing.Point(15, 49);
            this.lblBatchChannelsType.Name = "lblBatchChannelsType";
            this.lblBatchChannelsType.Size = new System.Drawing.Size(50, 13);
            this.lblBatchChannelsType.TabIndex = 21;
            this.lblBatchChannelsType.Text = "FileType:";
            // 
            // comboBatchChannelsType
            // 
            this.comboBatchChannelsType.FormattingEnabled = true;
            this.comboBatchChannelsType.Items.AddRange(new object[] {
            ".jpg",
            ".bmp",
            ".png",
            ".tiff"});
            this.comboBatchChannelsType.Location = new System.Drawing.Point(98, 46);
            this.comboBatchChannelsType.Name = "comboBatchChannelsType";
            this.comboBatchChannelsType.Size = new System.Drawing.Size(75, 21);
            this.comboBatchChannelsType.TabIndex = 20;
            // 
            // chkBatchChannelsBypass
            // 
            this.chkBatchChannelsBypass.AutoSize = true;
            this.chkBatchChannelsBypass.Checked = true;
            this.chkBatchChannelsBypass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBatchChannelsBypass.Location = new System.Drawing.Point(17, 23);
            this.chkBatchChannelsBypass.Name = "chkBatchChannelsBypass";
            this.chkBatchChannelsBypass.Size = new System.Drawing.Size(233, 17);
            this.chkBatchChannelsBypass.TabIndex = 19;
            this.chkBatchChannelsBypass.Text = "Bypass Save Dialog? (Use current filename)";
            this.chkBatchChannelsBypass.UseVisualStyleBackColor = true;
            this.chkBatchChannelsBypass.CheckedChanged += new System.EventHandler(this.chkBatchChannelsBypass_CheckedChanged);
            // 
            // chkBatchChannelsAlpha
            // 
            this.chkBatchChannelsAlpha.AutoSize = true;
            this.chkBatchChannelsAlpha.Location = new System.Drawing.Point(17, 117);
            this.chkBatchChannelsAlpha.Name = "chkBatchChannelsAlpha";
            this.chkBatchChannelsAlpha.Size = new System.Drawing.Size(130, 17);
            this.chkBatchChannelsAlpha.TabIndex = 18;
            this.chkBatchChannelsAlpha.Text = "Alpha as Black/White";
            this.chkBatchChannelsAlpha.UseVisualStyleBackColor = true;
            // 
            // btnBatchChannelsAll
            // 
            this.btnBatchChannelsAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBatchChannelsAll.Location = new System.Drawing.Point(179, 117);
            this.btnBatchChannelsAll.Name = "btnBatchChannelsAll";
            this.btnBatchChannelsAll.Size = new System.Drawing.Size(156, 46);
            this.btnBatchChannelsAll.TabIndex = 17;
            this.btnBatchChannelsAll.Text = "All (R,G,B,A)";
            this.btnBatchChannelsAll.UseVisualStyleBackColor = true;
            // 
            // lblBatchChannelsSelect
            // 
            this.lblBatchChannelsSelect.AutoSize = true;
            this.lblBatchChannelsSelect.Location = new System.Drawing.Point(14, 72);
            this.lblBatchChannelsSelect.Name = "lblBatchChannelsSelect";
            this.lblBatchChannelsSelect.Size = new System.Drawing.Size(153, 13);
            this.lblBatchChannelsSelect.TabIndex = 16;
            this.lblBatchChannelsSelect.Text = "Select the channel/s to export:";
            // 
            // btnBatchChannelsA
            // 
            this.btnBatchChannelsA.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBatchChannelsA.Location = new System.Drawing.Point(17, 88);
            this.btnBatchChannelsA.Name = "btnBatchChannelsA";
            this.btnBatchChannelsA.Size = new System.Drawing.Size(75, 23);
            this.btnBatchChannelsA.TabIndex = 15;
            this.btnBatchChannelsA.Text = "Alpha";
            this.btnBatchChannelsA.UseVisualStyleBackColor = true;
            // 
            // btnBatchChannelsB
            // 
            this.btnBatchChannelsB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBatchChannelsB.Location = new System.Drawing.Point(260, 88);
            this.btnBatchChannelsB.Name = "btnBatchChannelsB";
            this.btnBatchChannelsB.Size = new System.Drawing.Size(75, 23);
            this.btnBatchChannelsB.TabIndex = 14;
            this.btnBatchChannelsB.Text = "B";
            this.btnBatchChannelsB.UseVisualStyleBackColor = true;
            // 
            // btnBatchChannelsG
            // 
            this.btnBatchChannelsG.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBatchChannelsG.Location = new System.Drawing.Point(179, 88);
            this.btnBatchChannelsG.Name = "btnBatchChannelsG";
            this.btnBatchChannelsG.Size = new System.Drawing.Size(75, 23);
            this.btnBatchChannelsG.TabIndex = 13;
            this.btnBatchChannelsG.Text = "G";
            this.btnBatchChannelsG.UseVisualStyleBackColor = true;
            // 
            // btnBatchChannelsR
            // 
            this.btnBatchChannelsR.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBatchChannelsR.Location = new System.Drawing.Point(98, 88);
            this.btnBatchChannelsR.Name = "btnBatchChannelsR";
            this.btnBatchChannelsR.Size = new System.Drawing.Size(75, 23);
            this.btnBatchChannelsR.TabIndex = 12;
            this.btnBatchChannelsR.Text = "R";
            this.btnBatchChannelsR.UseVisualStyleBackColor = true;
            // 
            // btnBatch
            // 
            this.btnBatch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBatch.Location = new System.Drawing.Point(491, 477);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(100, 33);
            this.btnBatch.TabIndex = 10;
            this.btnBatch.Text = "Batch That Shit";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(385, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // lblBatchFileFolder
            // 
            this.lblBatchFileFolder.BackColor = System.Drawing.SystemColors.Control;
            this.lblBatchFileFolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBatchFileFolder.Location = new System.Drawing.Point(291, 10);
            this.lblBatchFileFolder.Name = "lblBatchFileFolder";
            this.lblBatchFileFolder.Size = new System.Drawing.Size(226, 20);
            this.lblBatchFileFolder.TabIndex = 40;
            this.lblBatchFileFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BatchSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 519);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.tabBatch);
            this.Name = "BatchSettings";
            this.Text = "BatchSettings";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabBatch.ResumeLayout(false);
            this.tabFile.ResumeLayout(false);
            this.batchFileFlow.ResumeLayout(false);
            this.chkBatchFileType.ResumeLayout(false);
            this.chkBatchFileType.PerformLayout();
            this.grpBatchFileName.ResumeLayout(false);
            this.grpBatchFileName.PerformLayout();
            this.grpBatchFileFolder.ResumeLayout(false);
            this.grpBatchFileFolder.PerformLayout();
            this.batchFileFlowSelection.ResumeLayout(false);
            this.batchFileFlowSelection.PerformLayout();
            this.tabTransform.ResumeLayout(false);
            this.tabTransform.PerformLayout();
            this.tabAdjustments.ResumeLayout(false);
            this.batchAdjFlow.ResumeLayout(false);
            this.batchAdjFlowHead.ResumeLayout(false);
            this.batchAdjFlowHead.PerformLayout();
            this.batchAdjFlowTransparency.ResumeLayout(false);
            this.batchAdjFlowTransparency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchAdjTransparencyInput)).EndInit();
            this.tabFilters.ResumeLayout(false);
            this.batchFilterFlow.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabChannels.ResumeLayout(false);
            this.tabChannels.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TabControl tabBatch;
        private System.Windows.Forms.TabPage tabFile;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.TabPage tabChannels;
        private System.Windows.Forms.CheckBox chkBatchFilterGrayscale;
        private System.Windows.Forms.CheckBox chkBatchFilterSepia;
        private System.Windows.Forms.Label lblBatchEffectsIstructions;
        private System.Windows.Forms.TabPage tabAdjustments;
        private System.Windows.Forms.Label lblBatchAdjInstructions;
        public System.Windows.Forms.CheckBox chkBatchAdjTransparency;
        private System.Windows.Forms.NumericUpDown txtBatchAdjTransparencyInput;
        private System.Windows.Forms.Label lblBatchAdjTransparencyInstructions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radBatchFilterGrayscaleLum;
        private System.Windows.Forms.RadioButton radBatchFilterGrayscaleAvg;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Label lblBatchFileImagesInstructions;
        private System.Windows.Forms.CheckedListBox batchFileSelectionList;
        private System.Windows.Forms.CheckBox chkBatchFileProcessAll;
        private System.Windows.Forms.CheckBox chkBatchFileExport;
        private System.Windows.Forms.Button btnBatchFileExportGetPath;
        private System.Windows.Forms.ComboBox comboBatchFileExportType;
        private System.Windows.Forms.FlowLayoutPanel batchFileFlow;
        private System.Windows.Forms.Panel batchFileFlowHead;
        private System.Windows.Forms.Panel chkBatchFileType;
        private System.Windows.Forms.Panel batchFileFlowSelection;
        private System.Windows.Forms.FlowLayoutPanel batchAdjFlow;
        private System.Windows.Forms.Panel batchAdjFlowHead;
        private System.Windows.Forms.Panel batchAdjFlowTransparency;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel batchFilterFlow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabTransform;
        private System.Windows.Forms.CheckBox chkBatchTransform;
        private System.Windows.Forms.Label lblBatchChannelsType;
        private System.Windows.Forms.ComboBox comboBatchChannelsType;
        private System.Windows.Forms.CheckBox chkBatchChannelsBypass;
        private System.Windows.Forms.CheckBox chkBatchChannelsAlpha;
        private System.Windows.Forms.Button btnBatchChannelsAll;
        private System.Windows.Forms.Label lblBatchChannelsSelect;
        private System.Windows.Forms.Button btnBatchChannelsA;
        private System.Windows.Forms.Button btnBatchChannelsB;
        private System.Windows.Forms.Button btnBatchChannelsG;
        private System.Windows.Forms.Button btnBatchChannelsR;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radBatchFileFolderNew;
        private System.Windows.Forms.RadioButton radBatchFileFolderCurrent;
        private System.Windows.Forms.GroupBox grpBatchFileName;
        private System.Windows.Forms.RadioButton radBatchFileNameCurrent;
        private System.Windows.Forms.RadioButton radBatchFileNameNew;
        private System.Windows.Forms.TextBox txtBatchFileName;
        private System.Windows.Forms.GroupBox grpBatchFileFolder;
        private System.Windows.Forms.Label lblBatchFileExportType;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblBatchFileFolder;
    }
}