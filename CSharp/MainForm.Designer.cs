namespace TwainHighPerformanceDemo
{
    partial class MainForm
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
            this.acquireImagesButton = new System.Windows.Forms.Button();
            this.saveImagesToMultipageFileCheckBox = new System.Windows.Forms.CheckBox();
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.canSkipPreviewImagesCheckBox = new System.Windows.Forms.CheckBox();
            this.imageSavingGroupBox = new System.Windows.Forms.GroupBox();
            this.savingThreadCountLabel = new System.Windows.Forms.Label();
            this.savingThreadCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.imageFileFormatComboBox = new System.Windows.Forms.ComboBox();
            this.imageFileFormatLabel = new System.Windows.Forms.Label();
            this.imageViewer1 = new Vintasoft.Imaging.UI.ImageViewer();
            this.imageScanningGroupBox = new System.Windows.Forms.GroupBox();
            this.showDeviceUiCheckBox = new System.Windows.Forms.CheckBox();
            this.showDeviceSelectionDialogCheckBox = new System.Windows.Forms.CheckBox();
            this.maximumImageCountInSavingQueueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.maximumImageCountInSavingQueueLabel = new System.Windows.Forms.Label();
            this.imageInfoLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.imageSavingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.savingThreadCountNumericUpDown)).BeginInit();
            this.imageScanningGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumImageCountInSavingQueueNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // acquireImagesButton
            // 
            this.acquireImagesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.acquireImagesButton.Location = new System.Drawing.Point(12, 152);
            this.acquireImagesButton.Name = "acquireImagesButton";
            this.acquireImagesButton.Size = new System.Drawing.Size(583, 41);
            this.acquireImagesButton.TabIndex = 0;
            this.acquireImagesButton.Text = "Acquire images";
            this.acquireImagesButton.UseVisualStyleBackColor = true;
            this.acquireImagesButton.Click += new System.EventHandler(this.acquireImagesButton_Click);
            // 
            // saveImagesToMultipageFileCheckBox
            // 
            this.saveImagesToMultipageFileCheckBox.AutoSize = true;
            this.saveImagesToMultipageFileCheckBox.Checked = true;
            this.saveImagesToMultipageFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveImagesToMultipageFileCheckBox.Location = new System.Drawing.Point(6, 52);
            this.saveImagesToMultipageFileCheckBox.Name = "saveImagesToMultipageFileCheckBox";
            this.saveImagesToMultipageFileCheckBox.Size = new System.Drawing.Size(163, 17);
            this.saveImagesToMultipageFileCheckBox.TabIndex = 2;
            this.saveImagesToMultipageFileCheckBox.Text = "Save images to multipage file";
            this.saveImagesToMultipageFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // useMultipleThreadsForSavingImagesToMultiPageFileCheckBox
            // 
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.AutoSize = true;
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.Checked = true;
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.Location = new System.Drawing.Point(6, 75);
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.Name = "useMultipleThreadsForSavingImagesToMultiPageFileCheckBox";
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.Size = new System.Drawing.Size(282, 17);
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.TabIndex = 3;
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.Text = "Use multiple threads for saving images to multipage file";
            this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.canSkipPreviewImagesCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 51);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image preview";
            // 
            // canSkipPreviewImagesCheckBox
            // 
            this.canSkipPreviewImagesCheckBox.AutoSize = true;
            this.canSkipPreviewImagesCheckBox.Checked = true;
            this.canSkipPreviewImagesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSkipPreviewImagesCheckBox.Location = new System.Drawing.Point(9, 21);
            this.canSkipPreviewImagesCheckBox.Name = "canSkipPreviewImagesCheckBox";
            this.canSkipPreviewImagesCheckBox.Size = new System.Drawing.Size(202, 17);
            this.canSkipPreviewImagesCheckBox.TabIndex = 0;
            this.canSkipPreviewImagesCheckBox.Text = "Can skip images if viewer is not ready";
            this.canSkipPreviewImagesCheckBox.UseVisualStyleBackColor = true;
            // 
            // imageSavingGroupBox
            // 
            this.imageSavingGroupBox.Controls.Add(this.savingThreadCountLabel);
            this.imageSavingGroupBox.Controls.Add(this.savingThreadCountNumericUpDown);
            this.imageSavingGroupBox.Controls.Add(this.imageFileFormatComboBox);
            this.imageSavingGroupBox.Controls.Add(this.imageFileFormatLabel);
            this.imageSavingGroupBox.Controls.Add(this.saveImagesToMultipageFileCheckBox);
            this.imageSavingGroupBox.Controls.Add(this.useMultipleThreadsForSavingImagesToMultiPageFileCheckBox);
            this.imageSavingGroupBox.Location = new System.Drawing.Point(307, 12);
            this.imageSavingGroupBox.Name = "imageSavingGroupBox";
            this.imageSavingGroupBox.Size = new System.Drawing.Size(289, 134);
            this.imageSavingGroupBox.TabIndex = 5;
            this.imageSavingGroupBox.TabStop = false;
            this.imageSavingGroupBox.Text = "Image saving";
            // 
            // savingThreadCountLabel
            // 
            this.savingThreadCountLabel.AutoSize = true;
            this.savingThreadCountLabel.Location = new System.Drawing.Point(6, 103);
            this.savingThreadCountLabel.Name = "savingThreadCountLabel";
            this.savingThreadCountLabel.Size = new System.Drawing.Size(106, 13);
            this.savingThreadCountLabel.TabIndex = 7;
            this.savingThreadCountLabel.Text = "Saving thread count:";
            // 
            // savingThreadCountNumericUpDown
            // 
            this.savingThreadCountNumericUpDown.Location = new System.Drawing.Point(118, 101);
            this.savingThreadCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.savingThreadCountNumericUpDown.Name = "savingThreadCountNumericUpDown";
            this.savingThreadCountNumericUpDown.Size = new System.Drawing.Size(159, 20);
            this.savingThreadCountNumericUpDown.TabIndex = 6;
            this.savingThreadCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // imageFileFormatComboBox
            // 
            this.imageFileFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageFileFormatComboBox.FormattingEnabled = true;
            this.imageFileFormatComboBox.Items.AddRange(new object[] {
            "TIFF",
            "PDF",
            "JPEG",
            "PNG",
            "BMP",
            "GIF"});
            this.imageFileFormatComboBox.Location = new System.Drawing.Point(118, 25);
            this.imageFileFormatComboBox.Name = "imageFileFormatComboBox";
            this.imageFileFormatComboBox.Size = new System.Drawing.Size(159, 21);
            this.imageFileFormatComboBox.TabIndex = 5;
            // 
            // imageFileFormatLabel
            // 
            this.imageFileFormatLabel.AutoSize = true;
            this.imageFileFormatLabel.Location = new System.Drawing.Point(6, 25);
            this.imageFileFormatLabel.Name = "imageFileFormatLabel";
            this.imageFileFormatLabel.Size = new System.Drawing.Size(87, 13);
            this.imageFileFormatLabel.TabIndex = 4;
            this.imageFileFormatLabel.Text = "Image file format:";
            // 
            // imageViewer1
            // 
            this.imageViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageViewer1.Location = new System.Drawing.Point(14, 222);
            this.imageViewer1.Name = "imageViewer1";
            this.imageViewer1.Size = new System.Drawing.Size(583, 529);
            this.imageViewer1.SizeMode = Vintasoft.Imaging.UI.ImageSizeMode.BestFit;
            this.imageViewer1.TabIndex = 1;
            this.imageViewer1.Text = "imageViewer1";
            // 
            // imageScanningGroupBox
            // 
            this.imageScanningGroupBox.Controls.Add(this.showDeviceUiCheckBox);
            this.imageScanningGroupBox.Controls.Add(this.showDeviceSelectionDialogCheckBox);
            this.imageScanningGroupBox.Controls.Add(this.maximumImageCountInSavingQueueNumericUpDown);
            this.imageScanningGroupBox.Controls.Add(this.maximumImageCountInSavingQueueLabel);
            this.imageScanningGroupBox.Location = new System.Drawing.Point(12, 12);
            this.imageScanningGroupBox.Name = "imageScanningGroupBox";
            this.imageScanningGroupBox.Size = new System.Drawing.Size(289, 77);
            this.imageScanningGroupBox.TabIndex = 6;
            this.imageScanningGroupBox.TabStop = false;
            this.imageScanningGroupBox.Text = "Image scanning";
            // 
            // showDeviceUiCheckBox
            // 
            this.showDeviceUiCheckBox.AutoSize = true;
            this.showDeviceUiCheckBox.Checked = true;
            this.showDeviceUiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDeviceUiCheckBox.Location = new System.Drawing.Point(179, 19);
            this.showDeviceUiCheckBox.Name = "showDeviceUiCheckBox";
            this.showDeviceUiCheckBox.Size = new System.Drawing.Size(102, 17);
            this.showDeviceUiCheckBox.TabIndex = 3;
            this.showDeviceUiCheckBox.Text = "Show device UI";
            this.showDeviceUiCheckBox.UseVisualStyleBackColor = true;
            // 
            // showDeviceSelectionDialogCheckBox
            // 
            this.showDeviceSelectionDialogCheckBox.AutoSize = true;
            this.showDeviceSelectionDialogCheckBox.Checked = true;
            this.showDeviceSelectionDialogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDeviceSelectionDialogCheckBox.Location = new System.Drawing.Point(9, 19);
            this.showDeviceSelectionDialogCheckBox.Name = "showDeviceSelectionDialogCheckBox";
            this.showDeviceSelectionDialogCheckBox.Size = new System.Drawing.Size(164, 17);
            this.showDeviceSelectionDialogCheckBox.TabIndex = 2;
            this.showDeviceSelectionDialogCheckBox.Text = "Show device selection dialog";
            this.showDeviceSelectionDialogCheckBox.UseVisualStyleBackColor = true;
            // 
            // maximumImageCountInSavingQueueNumericUpDown
            // 
            this.maximumImageCountInSavingQueueNumericUpDown.Location = new System.Drawing.Point(205, 44);
            this.maximumImageCountInSavingQueueNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.maximumImageCountInSavingQueueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maximumImageCountInSavingQueueNumericUpDown.Name = "maximumImageCountInSavingQueueNumericUpDown";
            this.maximumImageCountInSavingQueueNumericUpDown.Size = new System.Drawing.Size(72, 20);
            this.maximumImageCountInSavingQueueNumericUpDown.TabIndex = 1;
            this.maximumImageCountInSavingQueueNumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // maximumImageCountInSavingQueueLabel
            // 
            this.maximumImageCountInSavingQueueLabel.AutoSize = true;
            this.maximumImageCountInSavingQueueLabel.Location = new System.Drawing.Point(6, 46);
            this.maximumImageCountInSavingQueueLabel.Name = "maximumImageCountInSavingQueueLabel";
            this.maximumImageCountInSavingQueueLabel.Size = new System.Drawing.Size(193, 13);
            this.maximumImageCountInSavingQueueLabel.TabIndex = 0;
            this.maximumImageCountInSavingQueueLabel.Text = "Maximum image count in saving queue:";
            // 
            // imageInfoLabel
            // 
            this.imageInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageInfoLabel.Location = new System.Drawing.Point(12, 196);
            this.imageInfoLabel.Name = "imageInfoLabel";
            this.imageInfoLabel.Size = new System.Drawing.Size(583, 23);
            this.imageInfoLabel.TabIndex = 7;
            this.imageInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 763);
            this.Controls.Add(this.imageInfoLabel);
            this.Controls.Add(this.imageScanningGroupBox);
            this.Controls.Add(this.imageSavingGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imageViewer1);
            this.Controls.Add(this.acquireImagesButton);
            this.Name = "MainForm";
            this.Text = "Vintasoft TWAIN High Performance Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.imageSavingGroupBox.ResumeLayout(false);
            this.imageSavingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.savingThreadCountNumericUpDown)).EndInit();
            this.imageScanningGroupBox.ResumeLayout(false);
            this.imageScanningGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumImageCountInSavingQueueNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button acquireImagesButton;
        private Vintasoft.Imaging.UI.ImageViewer imageViewer1;
        private System.Windows.Forms.CheckBox saveImagesToMultipageFileCheckBox;
        private System.Windows.Forms.CheckBox useMultipleThreadsForSavingImagesToMultiPageFileCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox canSkipPreviewImagesCheckBox;
        private System.Windows.Forms.GroupBox imageSavingGroupBox;
        private System.Windows.Forms.ComboBox imageFileFormatComboBox;
        private System.Windows.Forms.Label imageFileFormatLabel;
        private System.Windows.Forms.Label savingThreadCountLabel;
        private System.Windows.Forms.NumericUpDown savingThreadCountNumericUpDown;
        private System.Windows.Forms.GroupBox imageScanningGroupBox;
        private System.Windows.Forms.NumericUpDown maximumImageCountInSavingQueueNumericUpDown;
        private System.Windows.Forms.Label maximumImageCountInSavingQueueLabel;
        private System.Windows.Forms.CheckBox showDeviceSelectionDialogCheckBox;
        private System.Windows.Forms.CheckBox showDeviceUiCheckBox;
        private System.Windows.Forms.Label imageInfoLabel;
    }
}

