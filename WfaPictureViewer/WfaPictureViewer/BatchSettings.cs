using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaPictureViewer
{
    public partial class BatchSettings : Form
    {
        // Attributes created for public accessing
        public bool exportToFile { get; private set; }
        public bool provideName { get; private set; }
        public bool provideDir { get; private set; }
        public string fileTypeString, fileName, newFileDir;
        private Color colourEnabled = Color.FromArgb(161, 212, 144), colourDisabled = Color.WhiteSmoke, colourUnavailable = Color.FromArgb(212, 161, 144);
        public CheckBox[][] arrChkOptions { get; private set; } // Jagged array of all checkbox options, retreivable but not editable.
        private int curFilterIndex;
        private PicViewer mainProgram;
        public bool[] arrIsProcessed; // is the image at this position selected for processing.
        public byte transpInput { get; private set; }

        // CONSTRUCTOR
        public BatchSettings(object sender)
        {
            InitializeComponent();
            // Stops manual editing of the combo box, meaning that somehting HAS to be arrIsProcessed
            comboBatchFileExportType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBatchFileExportType.SelectedIndex = 0;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            // Tracks the currenty in-use filter
            curFilterIndex = 0;
            UpdateCheckedStatus();
            
            mainProgram = (PicViewer)sender; // Reference to main form
            arrIsProcessed = new bool[mainProgram.listLoadedImg.Count()]; // array of bool matching length of list

            UpdateOptions();
            PopulateList();
        }

        private void UpdateCheckedStatus()
        {
            arrChkOptions = new CheckBox[5][];

            // Row 0, File
            arrChkOptions[0] = new CheckBox[] { chkBatchFileExport };
            // Row 1, Transform
            arrChkOptions[1] = new CheckBox[] { chkBatchTransform };
            // Row 2, Adjustments
            arrChkOptions[2] = new CheckBox[] { chkBatchAdjTransparency };
            // Row 3, Filters
            arrChkOptions[3] = new CheckBox[] { chkBatchFilterSepia, chkBatchFilterGrayscale };
            // Row 4, Channels
            arrChkOptions[4] = new CheckBox[] { chkBatchChannelsBypass };

        }

        // Returns a List of active options
        private List<CheckBox> GetActiveChkOptions()
        {
            List<CheckBox> activeOptions = new List<CheckBox>();

            // Create a list of active checkboxes based on check value
            for (int i = 0; i < arrChkOptions.Length; i++)
            {
                for (int j = 0; j < arrChkOptions[i].Length; j++)
                {
                    if (arrChkOptions[i][j].Checked == true)
                    {
                        activeOptions.Add(arrChkOptions[i][j]);
                    }
                }
            }
                return activeOptions;
        }

        // Populate checklist of images
        private void PopulateList()
        {
            batchFileSelectionList.Items.Clear();
            for (int i = 0; i < arrIsProcessed.Length; i++ )
            {
                // Add an item using the listLoadedImage collection's item names, prechecked
                batchFileSelectionList.Items.Add(mainProgram.listLoadedImg[i].GetName(), true); 
            }            
        }

        private void UpdateOptions()
        {
            // File Options
            if (chkBatchFileExport.Checked)
                exportToFile = true;
            else
                exportToFile = false;

            if (radBatchFileNameCurrent.Checked)
            {
                txtBatchFileName.Text = null;
                radBatchFileNameCurrent.Checked = true; // For some reason this is required to avoid having to click the radiobutton twice
            }

            // Enable label only when the "New" radiobutton is checked
            lblBatchFileFolder.Enabled = radBatchFileFolderNew.Checked;
           


            // Adjustment Options

            // Filter Options

            // Reset currently active filter tracker
            curFilterIndex = -1;
            
            // Loop through collection of chkboxes
            for (int i = 0; i < arrChkOptions.Length; i++)
            {
                // Each row of chkboxes corresponds to a tab's options
                for (int j = 0; j < arrChkOptions[i].Length; j++)
                {
                    // Colour code based on whether the checkbox is checked.
                    if (arrChkOptions[i][j].Checked)
                    {
                        ((Panel)arrChkOptions[i][j].Parent).BackColor = colourEnabled;
                        // Every control (including chkbox) inside the prent panel is enabled
                        foreach (Control ctrl in ((Panel)arrChkOptions[i][j].Parent).Controls)
                        {
                            if (!ctrl.Enabled)
                                ctrl.Enabled = true;
                        }
                        if(i == 3) // If currently checking filters
                        { 
                            curFilterIndex = j;
                        }
                    }
                    else if (arrChkOptions[i][j].CheckState == CheckState.Unchecked)
                    {
                        ((Panel)arrChkOptions[i][j].Parent).BackColor = colourDisabled;
                        // Every control (including chkbox) inside the prent panel is disabled
                        foreach (Control ctrl in ((Panel)arrChkOptions[i][j].Parent).Controls)
                        {
                            if (ctrl.Enabled)
                                ctrl.Enabled = false;
                        }
                        arrChkOptions[i][j].Enabled = true; // chkbox needs to be reenabled
                    }
                }
            }

            // If an active filter was found
            if (curFilterIndex != -1)
            {
                for (int j = 0; j < arrChkOptions[3].Length; j++)
                {
                    // If we aren't dealing with the currently active filter
                    if (j != curFilterIndex)
                    {
                        arrChkOptions[3][j].Enabled = false;
                        ((Panel)arrChkOptions[3][j].Parent).BackColor = colourUnavailable;
                    }
                }
            }
            // Simulate a click on the "Process All button"
            chkBatchFileProcessAll_CheckedChanged(this, null);

            // Channel Options
        }

        // Finalise, confirm, do the thing, get all the values from input elements.
        private void btnBatch_Click(object sender, EventArgs e)
        {
            UpdateCheckedStatus();

            // Grab all the important values for use by mainProgram's algorithms
            transpInput = (byte)txtBatchAdjTransparencyInput.Value;
            exportToFile = chkBatchFileExport.Checked;
            provideDir = radBatchFileFolderNew.Checked;
            provideName = radBatchFileNameNew.Checked;
            newFileDir = lblBatchFileFolder.Text;

            // For each selected image in the electionlist, edit bool in that array position
            // This array is used by menuBatch_Click
            for (int i = 0; i < arrIsProcessed.Length; i++)
            {
                if (batchFileSelectionList.CheckedIndices.Contains(i))
                {
                    arrIsProcessed[i] = true;
                }
            }

        }

        public ImageFormat GetImageFormat()
        {
            int fileType = comboBatchFileExportType.SelectedIndex;
            switch (fileType)
            {
                case 0: // JPG
                    {
                        return ImageFormat.Jpeg;
                    }
                case 1: // BMP
                    {
                        return ImageFormat.Bmp;
                    }
                case 2: // PNG
                    {
                        return ImageFormat.Png;
                    }
                case 3: // TIFF
                    {
                        return ImageFormat.Tiff;
                    }
                default: // SOME SHIT WENT WRONG
                    {
                        Environment.Exit(23);
                    }break;
            }
            return ImageFormat.Jpeg;
        }

        // Returns a BatchSettings form element VALUE on request
        public string GetValue (string formElement)
        {
            switch (formElement)
            {
                case "newFileDir":
                    {
                        return lblBatchFileFolder.Text;
                    }
                case "newFileName":
                    {
                        return txtBatchFileName.Text;
                    }
                default: // SOME SHIT WENT WRONG
                    {
                        Environment.Exit(24);
                        return null;
                    }
            }
        }
        


        // EVENT HANDLERS
        ////////////////////////////////////////////////////////////////////

        private void chkExport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBatchFileExport.Checked)
                exportToFile = true;
            else
                exportToFile = false;

            UpdateOptions();
        }

        private void chkBypass_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkFileName_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkBatchFileExportNewName.Checked == true)
            //{
            //    provideName = true;
            //}
            //else
            //    provideName = false;
            UpdateOptions();
        }

        private void btnGetPath_Click(object sender, EventArgs e)
        {
            /*using (SaveFileDialog dlgGetPath = new SaveFileDialog())
            {
                dlgGetPath.InitialDirectory = "C:/Desktop";
                dlgGetPath.Title = "Select File Name and Path";
                dlgGetPath.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";

                if (dlgGetPath.ShowDialog() == DialogResult.OK)
                {
                    fileName = dlgGetPath.FileName;
                    fileType = dlgGetPath.FilterIndex;
                    txtBatchFileFolder.Text = dlgGetPath.FileName;
                }
            }*/
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (bypass)
            //{
            //    if (!provideName)
            //    {
            //        fileType = comboBatchFileExportType.SelectedIndex;
            //        fileTypeString = comboBatchFileExportType.Text;
            //    }
            //    else
            //        // unchanged, leave it as the dlg assigned it 

            //        if (fileName != null)
            //        {
            //            this.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please enter a valid file deaultName & path using the [...] button.");
            //        }
            //}
            //else
            //{
            //}
        }

        private void chkBatchEffectBrightness_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchEffectGrayscale_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchEffectSepia_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchFileExport_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchChannelsBypass_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void radBatchFileFolderNew_CheckedChanged(object sender, EventArgs e)
        {
            // Simulate buttonclick on 'get path' button if this radio is checked, and the path was not already defined (i.e. if the user clicks this radiobutton instead of the 'get path' button.
            if (radBatchFileFolderNew.Checked == true && lblBatchFileFolder.Text == "")
            {
                btnBatchFileExportGetPath_Click(this, null);
            }
            UpdateOptions();
        }

        private void radBatchFileFolderCurrent_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void radBatchFileNameCurrent_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void radBatchFileNameNew_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void txtBatchFileName_TextChanged(object sender, EventArgs e)
        {
            radBatchFileNameNew.Checked = true;
            UpdateOptions();
        }

        private void btnBatchFileExportGetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                lblBatchFileFolder.Text = fb.SelectedPath;
                radBatchFileFolderNew.Checked = true;
            }
            else // if the user cancelled
            {
                radBatchFileFolderCurrent.Checked = true;
            }
        }

        // Whenever a part of the selection list is clicked
        private void batchFileSelectionList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // If the "Process all" box is clicked
        private void chkBatchFileProcessAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBatchFileProcessAll.Checked)
            {
                for (int i = 0; i < batchFileSelectionList.Items.Count; i++)
                {
                    batchFileSelectionList.SetItemChecked(i, true);
                }
                batchFileSelectionList.Enabled = false;
            }
            else
            {
                batchFileSelectionList.Enabled = true;
            }
        }
    }
}
