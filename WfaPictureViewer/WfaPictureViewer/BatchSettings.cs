using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace WfaPictureViewer
{
    public partial class BatchSettings : Form
    {
        // get; private set allows public access, but not modification
        public bool provideName { get; private set; }
        public bool provideDir { get; private set; }
        public bool exportAlphaTransp { get; private set; }
        public bool exportAlphaBW { get; private set; }
        public string fileTypeString, fileName, newFileDir, grayAlgorithm;
        private Color colourEnabled = Color.FromArgb(161, 212, 144), colourDisabled = Color.WhiteSmoke, colourUnavailable = Color.FromArgb(212, 161, 144);
        public CheckBox[][] arrChkOptions { get; private set; } // Jagged array of all checkbox options, retreivable but not editable.
        public CheckBox[] arrChannelOptions { get; private set; } // array of all the channel choices
        private int curFilterIndex;
        private PicViewer mainProgram;
        public bool[] arrIsProcessed; // is the image at this position selected for processing.
        public byte transpInput { get; private set; }
        public float transformScale { get; private set; }
        private LoadedImage previewLoadedImage { get; set; }

        // CONSTRUCTOR
        public BatchSettings(object sender)
        {
            InitializeComponent();
            // Stops manual editing of the combo box, meaning that somehting HAS to be arrIsProcessed
            comboBatchFileExportType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            // Tracks the currenty in-use filter
            curFilterIndex = 0;
            UpdateCheckedStatus();
            
            mainProgram = (PicViewer)sender; // Reference to main form
            arrIsProcessed = new bool[mainProgram.listLoadedImg.Count()]; // array of bool matching length of list

            comboBatchFileExportType.SelectedIndex = 0; // Creates a call to  UpdateOptions();
            PopulateList();

            // Suite of image adjustment tools
            ImgAdjust adjustImg = new ImgAdjust();

            // Used to create a LoadedImage object
            Bitmap imgForPreview;
            // If check allows for development work without having to load an images every time
            if (mainProgram.listLoadedImg.Count() == 0)
            {
                imgForPreview = adjustImg.GetArgbVer(Bitmap.FromFile("..//..//testImage.jpg"));
            }
            else
            {
                imgForPreview = mainProgram.listLoadedImg[0].GetBitmap("c");
            }

            // LoadedImage class object is created, it's previewVer is displayed in the thumbnail
            // -999 & ../ are dummy values, they aren't used and are unimportant. 
            previewLoadedImage = new LoadedImage("../", imgForPreview, mainProgram, -9999);
            picBatchPreview.Image = previewLoadedImage.GetBitmap("p");            
        }

        private void UpdateCheckedStatus()
        {
            if (arrChkOptions == null)
            {
                arrChkOptions = new CheckBox[5][]; // Create the Jagged array if it doesn't already exist.
            }

            // Row 0, File
            arrChkOptions[0] = new CheckBox[] { chkBatchFileExport };
            // Row 1, Transforms
            arrChkOptions[1] = new CheckBox[] { chkBatchTransScale, chkBatchTransRotate, chkBatchTransFlip };
            // Row 2, Adjustments
            arrChkOptions[2] = new CheckBox[] { chkBatchAdjTransparency };
            // Row 3, Filters
            arrChkOptions[3] = new CheckBox[] { chkBatchFilterSepia, chkBatchFilterGrayscale };
            // Row 4, Channels
            arrChkOptions[4] = new CheckBox[] { chkBatchChannels};
            
            // Channel options
            arrChannelOptions = new CheckBox[] { chkBatchChannelsR, chkBatchChannelsG, chkBatchChannelsB, chkBatchChannelsA };

            provideDir = radBatchFileFolderNew.Checked;
            provideName = radBatchFileNameNew.Checked;
            exportAlphaBW = radBatchChannelsABW.Checked;
            exportAlphaTransp = radBatchChannelsATran.Checked;

            // Steal file naming code from ExportChannel()
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
            if (radBatchFileNameCurrent.Checked)
            {
                txtBatchFileName.Text = null;
                radBatchFileNameCurrent.Checked = true; // For some reason this is required to avoid having to click the radiobutton twice
            }

            // Enable label only when the "New" radiobutton is checked
            lblBatchFileFolder.Enabled = radBatchFileFolderNew.Checked;
           
            // Adjustment Options

            // Reset currently active filter tracker
            curFilterIndex = -1;

            // Channel options
            // Only allow channel export chkbox when 'export to files' is selected
            if (!arrChkOptions[0][0].Checked)
            {
                arrChkOptions[4][0].Enabled = arrChkOptions[4][0].Checked = false;
            }
            else
            {
                arrChkOptions[4][0].Enabled = true;
            }

            if (arrChkOptions[4][0].Enabled && arrChkOptions[4][0].Checked)
            {
                // Disable alpha channel radiobuttons if checkdbox isnt checked
                radBatchChannelsATran.Enabled = radBatchChannelsABW.Enabled = chkBatchChannelsA.Checked;

                // if not using PNG
                if (comboBatchFileExportType.SelectedIndex != 2)
                {
                    radBatchChannelsATran.Enabled = false;
                    lblAlphaInstructions.Visible = true;
                }
                else
                {
                    lblAlphaInstructions.Visible = false;
                }
            }
            
            // Loop through collection of chkboxes
            for (int i = 0; i < arrChkOptions.Length; i++)
            {
                // Each row of chkboxes corresponds to a tab's options
                for (int j = 0; j < arrChkOptions[i].Length; j++)
                {
                    bool wasChkBoxEnabled = arrChkOptions[i][j].Enabled;
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
                        // Every control (including chkbox) inside the parent panel is disabled
                        foreach (Control ctrl in ((Panel)arrChkOptions[i][j].Parent).Controls)
                        {
                            if (ctrl.Enabled)
                                ctrl.Enabled = false;
                        }
                        // chkbox.enabled needs to be returned to it's previous state. This implementation means that all chkbox states should be set before this set of nested loops. 
                        arrChkOptions[i][j].Enabled = wasChkBoxEnabled; 
                    }
                }

                UpdateCheckedStatus();
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
            else
            {
                // Enable all
                for (int j = 0; j < arrChkOptions[3].Length; j++)
                {
                    arrChkOptions[3][j].Enabled = true;
                }
            }
            // Simulate a click on the "Process All button"
            chkBatchFileProcessAll_CheckedChanged(this, null);

            // Channel Options
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

        // Finalise, confirm, get all the values from non-checkbox input elements.
        public void GrabInputValues()
        {
            UpdateCheckedStatus();

            // Grab all the important values for use by mainProgram's algorithms
            transpInput = (byte)txtBatchAdjTransparencyInput.Value;
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

            if (arrChkOptions[3][1].Checked)
            {
                if (radBatchFilterGrayscaleAvg.Checked)
                {
                    grayAlgorithm = "average";
                }
                else if (radBatchFilterGrayscaleLum.Checked)
                {
                    grayAlgorithm = "luminosity";
                }
            }

            transformScale = (float)txtBatchScale.Value;
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            GrabInputValues();
        }

        // EVENT HANDLERS
        ////////////////////////////////////////////////////////////////////

        private void chkExport_CheckedChanged(object sender, EventArgs e)
        {
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

        // If the "Process all" box is clicked
        private void chkBatchFileProcessAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBatchFileProcessAll.Checked)
            {
                for (int i = 0; i < batchFileSelectionList.Items.Count; i++)
                {
                    batchFileSelectionList.SetItemChecked(i, true);
                }
                btnBatchFileProcessDeselect.Enabled = batchFileSelectionList.Enabled = false;
            }
            else
            {
                btnBatchFileProcessDeselect.Enabled = batchFileSelectionList.Enabled = true;
            }
        }

        private void chkBatchChannels_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchTransScale_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchTransRotate_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchTransFlip_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchChannels_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void chkBatchChannelsA_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void radBatchChannelsABW_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void radBatchChannelsATran_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void comboBatchFileExportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GrabInputValues();
            mainProgram.ApplyBatchSettingsEffects(this, previewLoadedImage);
            picBatchPreview.Image = previewLoadedImage.GetBitmap("p");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ImgAdjust adj = new ImgAdjust();
            picBatchPreview.Image = adj.GetScaledVer((Bitmap)picBatchPreview.Image, (float)txtBatchScale.Value, (float)txtBatchScale.Value, false);
        }

        private void btnBatchFileProcessDeselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < batchFileSelectionList.Items.Count; i++)
            {
                batchFileSelectionList.SetItemChecked(i, false);
            }
        }
    }
}
