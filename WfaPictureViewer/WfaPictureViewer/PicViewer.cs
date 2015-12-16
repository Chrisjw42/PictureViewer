using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging; // For 'PixelFormat' and others, redrawing Bitmaps
using System.Linq;
using System.Windows.Forms;
using System.Threading; // For 'Thread.Sleep'
using System.Runtime.InteropServices; // For 'Marshal.Copy'
using System.IO; // For 'MemoryStream'

namespace WfaPictureViewer
{
    // CLASS DECLARATION, "Form" is the base class and "PicViewer" is the derived class. The variables cannot be initialised here, only declared
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class PicViewer : Form
    {
        string[] inputFileText = System.IO.File.ReadAllLines(@"..\..\input.txt"); // Initialise an array of each line of the input file
        float picBoxRatio;
        Color defaultBG;
        public List<LoadedImage> listLoadedImg { get; private set; }
        int curImgIndex, curGalleryHeight; // The height of the flowGallery
        bool pnlGalleryHidden;
        ImgAdjust adjustImg = new ImgAdjust();

        //CONSTRUCTOR - Does not have a return type and shares a deaultName with the class
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public PicViewer()
        {
            InitializeComponent();

            // Allow the form to process key inputs
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            // Allow the picbox to process mousewhel inputs
            this.picBoxMain.MouseWheel += new MouseEventHandler(PicboxMain_MouseWheel);

            // "Size" is a struct, so you can't simply declare this.MinimumSize.Size = x,y
            this.MinimumSize = new Size(400, 200);
            // Bool initialisors
            chkAspectLock.Enabled = false;
            // BG Colour stuff
            defaultBG = BackColor;
            menuResetBGColour.Enabled = false;
            // Run the initial text and Option updates
            UpdateImgOptions();
            UpdateText();

            // Gallery initialisation
            pnlGalleryHidden = false;
            // Don't want the panel that holds the flowgallery to scroll, only want the gallery itself to scroll
            pnlGallery.AutoScrollMinSize = new Size(1, 1);
            listLoadedImg = new List<LoadedImage>();
            UpdateGallery();
        }

        // METHODS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Creating a LoadedImage object, most of which is handled by it's constructor.
        public void CreateLoadedImg(string path)
        {
            //First, create a Bitmap with the filepath, and convert it to Argb.
            Bitmap baseImage = adjustImg.GetArgbVer(new Bitmap(path));

            // Creating a LoadedImage object with, passing filepath and Bitmap to constructor
            LoadedImage img = new LoadedImage(path, baseImage, this, curImgIndex);

            // Add the new object to the list
            listLoadedImg.Add(img);
            curImgIndex++;
        }

        private void removeCurrentImg()
        {
            listLoadedImg[curImgIndex] = null;
            StepThroughImgList(1);
        }

        // Update the picbox with a passed image, ususally straight from the Load event ^
        private void UpdatePicbox(LoadedImage img)
        {
            // Assign current img to current and picbox
            picBoxMain.Image = img.GetBitmap("c");

            // Updating the display info
            UpdatePicboxInfoAndSizeMode();
            UpdateText();
            UpdateImgOptions();
            UpdateGallery();
            txtImgWindowControl.Value = 100;

            pnlPicBox.AutoScrollMinSize = new Size(img.GetBitmap("c").Width - 10, img.GetBitmap("c").Height - 10);
        }

        public void UpdateGallery()
        {// An array of the columnstyles, used below to define width of a specific column
            TableLayoutColumnStyleCollection styleCollection = tableLayoutPanel1.ColumnStyles;

            if (pnlGalleryHidden == false)
            {
                // Empty all thumbnails from gallery
                flowGallery.Controls.Clear();
                // First, update each thumbnail so they match the actual current versions of the image, if there's an image loaded
                if (listLoadedImg != null && listLoadedImg.Count() != 0)
                {
                    curGalleryHeight = 0;
                    for (int i = 0; i < listLoadedImg.Count(); i++)
                    {
                        // Increase the height of the gallery to fit each thumbnail.
                        curGalleryHeight += listLoadedImg[i].GetThumbnail().Height;

                        // Update label with semantic numbering
                        listLoadedImg[i].UpdateLblThumbText((i + 1).ToString());
                        // Update .Names to i, which matches their imgIndex, and will be used when clicked on to dictate the image that gets loaded
                        listLoadedImg[i].UpdateLblThumbName(i.ToString());
                        listLoadedImg[i].GetThumbnail().Name = i.ToString();
                        listLoadedImg[i].curIndex = i;

                        flowGallery.Controls.Add(listLoadedImg[i].GetThumbnail());

                        UpdateGallerySelection();
                    }
                }
                // Column, panel and flowgallery all have size set, curGalleryHeight is used to define when scrollbars appear
                // 137 is 100 image width + fixed3d padding + scrollbar width
                styleCollection[1].Width = flowGallery.Width = pnlGallery.Width = 137;

                flowGallery.AutoScrollMinSize = new Size(1, curGalleryHeight);
            }
            else
            {
                flowGallery.Controls.Clear();
                styleCollection[1].Width = flowGallery.Width = pnlGallery.Width = 0;
                flowGallery.AutoScrollMinSize = new Size(1, curGalleryHeight);
            }
        }

        private void UpdateGallerySelection()
        {
            for (int i = 0; i < listLoadedImg.Count(); i++)
            {
                // Aesthetic changing of borderstyle based on image selection.
                if (i == curImgIndex)
                    listLoadedImg[i].GetThumbnail().BorderStyle = BorderStyle.Fixed3D;
                else if (listLoadedImg[i].GetThumbnail().BorderStyle != BorderStyle.None)
                    listLoadedImg[i].GetThumbnail().BorderStyle = BorderStyle.None;
            }
        }

        private void StepThroughImgList(int numSteps)
        {
            // If the desired destination is not null
            if ((curImgIndex + numSteps) >= 0 && (curImgIndex + numSteps) <= (listLoadedImg.Count - 1))
            {
                curImgIndex += numSteps;
                UpdatePicbox(listLoadedImg[curImgIndex]);
                UpdateGallerySelection();
            }
        }


        // Combined method that updates the picbox label info display and the sizemode of the image
        private void UpdatePicboxInfoAndSizeMode()
        {
            // Writing the file info to the label
            if (picBoxMain.Image != null)
            {
                lblPicInfo.Text = ("File Name: " + listLoadedImg[curImgIndex].GetName() + Environment.NewLine + "H: " + picBoxMain.Image.Height + Environment.NewLine + "W: " + picBoxMain.Image.Width + Environment.NewLine + "Aspect Ratio: " + GetPicBoxRatio() + Environment.NewLine + "Stretching: " + GetRatioDistortion());
                // Note: "AutoSize" allows the use of the scroll bars
                if (chkStretch.Checked == true)
                    picBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
                else if (picBoxMain.Size.Width < picBoxMain.Image.Width || picBoxMain.Size.Height < picBoxMain.Image.Height)
                    picBoxMain.SizeMode = PictureBoxSizeMode.AutoSize;
                else // includes if picBoxMain > PicBoxMain.Image
                    picBoxMain.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else
                lblPicInfo.Text = null;
        }

        // Method is called whenever upon changing the state of the picturebox. 
        private void UpdateText()
        {
            if (picBoxMain.Image != null)
                lblPicNotifier.Text = "Img " + (curImgIndex + 1) + "/" + listLoadedImg.Count;
            else
                lblPicNotifier.Text = "Img 0/0";
        }

        private void CycleMaximised()
        {
            // If the form is not maximised
            if (this.FormBorderStyle == FormBorderStyle.Sizable && this.WindowState == FormWindowState.Normal)
            {
                // Maximise
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            // else if the form IS maximised
            else if (this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized)
            {
                // Un-maximise
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                MessageBox.Show("Fullscreening error");
                this.Close();
            }
        }

        // After resizing
        private void Form1_PostResize(object sender, EventArgs e)
        {
            UpdatePicboxInfoAndSizeMode();
        }

        // Returns the aspect ratio of the image currently loaded in the picBox
        private float GetPicBoxRatio()
        {
            // If there is actually an image loaded
            if (picBoxMain.Image != null)
            {
                // Find out if the stretching is turned on
                if (chkStretch.Checked == false)
                {
                    // The ratio is the width of the image divided by the height
                    picBoxRatio = picBoxMain.Image.PhysicalDimension.Width / picBoxMain.Image.PhysicalDimension.Height;
                    return picBoxRatio;
                }
                // Or if the stretch checkbox IS checked, instead get the values of the pictureBox itself, as the iamge will match it
                else
                {
                    picBoxRatio = picBoxMain.Width / picBoxMain.Height;
                    return picBoxRatio;
                }
            }
            else
            {
                return 0.0f;
            }
        }

        // Compare the current stretched image's aspect ratio against it's original aspect ratio
        private string GetRatioDistortion()
        {
            // Only run the comparison if image stretching is enabled
            if (chkStretch.Checked)
            {
                // To avoid calling the function multiple times
                double tempPicBoxRatio = GetPicBoxRatio();
                double distortion;
                double tmpCorrectRatio = listLoadedImg[curImgIndex].GetCorrectRatio();

                // if the images 'correct' ratio (e.g. 1.43322) is less than current 
                if (tmpCorrectRatio < tempPicBoxRatio)
                {
                    Console.WriteLine("Correct < Current" + Environment.NewLine + "Correct: " + tmpCorrectRatio + "Current: " + tempPicBoxRatio);
                    distortion = tmpCorrectRatio / tempPicBoxRatio - 1;
                    return (distortion.ToString("0.000"));
                }
                else if (tmpCorrectRatio > tempPicBoxRatio)
                {
                    Console.WriteLine("Correct > Current" + Environment.NewLine + "Correct: " + tmpCorrectRatio + "Current: " + tempPicBoxRatio);
                    distortion = tmpCorrectRatio / tempPicBoxRatio - 1;
                    return (distortion.ToString("0.000"));
                }
                else if (tmpCorrectRatio == tempPicBoxRatio)
                    return "Aspect ratio accurate!";

                else
                    return "Error";
            }
            else
                return "Aspect ratio accurate!";
        }

        // Update options that require an image to be loaded.
        public void UpdateImgOptions()
        {
            // Enable if image is currently loaded
            if (picBoxMain.Image != null)
            {
                menuClearImage.Enabled =
                menuCopyImage.Enabled =
                menuTransp.Enabled =
                menuGrayscale.Enabled =
                menuSepia.Enabled =
                menuSaveImage.Enabled =
                menuImageAdjustments.Enabled =
                menuImageFilters.Enabled =
                menuResetAdjustments.Enabled = true;

                if (listLoadedImg.Count >= 2)
                {
                    menuBatchMenu.Enabled = true;
                    menuLoadImage.Text = "Add Image/s";
                    //MenuHideGallery.Enabled = true;
                }
                else
                {
                    menuBatchMenu.Enabled = false;
                    menuLoadImage.Text = "Load Image/s";
                    //MenuHideGallery.Enabled = false;
                }

                // Activate or deactivate stretching-specific menu items depending on whether stretching is enabled
                if (chkStretch.Checked == true)
                {
                    menuResetStretching.Enabled = true;
                    menuFitWindow.Enabled = false;
                }
                else if (chkStretch.Checked == false)
                {
                    menuResetStretching.Enabled = false;
                    menuFitWindow.Enabled = true;
                }

                // If there is something in the list
                if (listLoadedImg.Count > 0)
                {
                    // If the list only has one item
                    if (listLoadedImg.Count == 1)
                    {
                        btnNavigateRight.Enabled = false;
                        btnNavigateLeft.Enabled = false;
                    }
                    // if it has more than one, and user is on the first
                    else if (listLoadedImg.Count > 1 && curImgIndex == 0)
                    {
                        btnNavigateRight.Enabled = true;
                        btnNavigateLeft.Enabled = false;
                    }
                    // if on the last item, and there is more than one
                    else if (curImgIndex == listLoadedImg.Count - 1 && listLoadedImg.Count > 1)
                    {
                        btnNavigateRight.Enabled = false;
                        btnNavigateLeft.Enabled = true;
                    }
                    // Otherwise, acrtivate them both
                    else
                    {
                        btnNavigateLeft.Enabled = true;
                        btnNavigateRight.Enabled = true;
                    }
                }
                // If nothing in list
                else
                {
                    btnNavigateRight.Enabled = false;
                    btnNavigateLeft.Enabled = false;
                }

                // Enable undo/redo based on images stacks.
                if (listLoadedImg[curImgIndex].IsUndoEmpty())
                    menuStepBackward.Enabled = false;
                else
                    menuStepBackward.Enabled = true;

                if (listLoadedImg[curImgIndex].IsRedoEmpty())
                    menuStepForward.Enabled = false;
                else
                    menuStepForward.Enabled = true;
            }
            // Disable if image is not currently loaded
            else
            {
                menuLoadImage.Enabled = true;

                menuClearImage.Enabled =
                menuCopyImage.Enabled =
                menuFitWindow.Enabled =
                menuResetStretching.Enabled =
                menuTransp.Enabled =
                menuGrayscale.Enabled =
                menuSepia.Enabled =
                menuImageFilters.Enabled =
                menuImageAdjustments.Enabled =
                menuSaveImage.Enabled =
                menuResetAdjustments.Enabled =
                btnNavigateLeft.Enabled =
                btnNavigateRight.Enabled =
                menuStepBackward.Enabled =
                menuStepForward.Enabled =
                menuBatchMenu.Enabled = false;
            }
        }
        private void SaveImageOld(SaveFileDialog dlg, Image img)
        {
            // Create a MemoryStream that will be minimally scoped
            using (MemoryStream memStream = new MemoryStream())
            {
                // Save the image to the memorystream in it's native format
                img.Save(memStream, listLoadedImg[curImgIndex].GetOriginalFormat());

                // Creating an Image that can actually be saved - Should probably make everything up to this point a method
                // Should also incorporate some kind of using statement to close off the MemoryStream
                Image imgToSave = Image.FromStream(memStream);

                // FilterIndex appears to record which filetype is arrIsProcessed
                switch (dlg.FilterIndex)
                {
                    case 1:
                        imgToSave.Save(dlg.FileName, ImageFormat.Jpeg);
                        break;
                    case 2:
                        imgToSave.Save(dlg.FileName, ImageFormat.Bmp);
                        break;
                    case 3:
                        imgToSave.Save(dlg.FileName, ImageFormat.Png);
                        break;
                    case 4:
                        imgToSave.Save(dlg.FileName, ImageFormat.Tiff);
                        break;
                }
            } dlg.Dispose();
        }

        private void SaveImage(string filePath, Image img, ImageFormat fmt)
        {
            // Create a MemoryStream that will be minimally scoped
            using (MemoryStream memStream = new MemoryStream())
            {
                // Save the image to the memorystream in it's native format
                // img.Save(memStream, listLoadedImg[curImgIndex].GetOriginalFormat());
                // Image imgToSave = Image.FromStream(memStream);                
                img.Save(filePath, fmt);
            }
        }

        // EVENT HANDLERS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void Form1_KeyPress(object sender, KeyPressEventArgs key)
        {
            if (key.KeyChar.ToString() == "f")
                CycleMaximised();
            if (key.KeyChar.ToString() == "x")
                StepThroughImgList(1);
            if (key.KeyChar.ToString() == "z")
                StepThroughImgList(-1);
        }

        private void MenuLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlgOpen = new OpenFileDialog())
            {
                dlgOpen.InitialDirectory = "C:/Desktop";
                dlgOpen.Filter = "All Image Files|*.jpg; *.bmp; *.png; *.tiff|Jpeg Image|*.jpg|Bmp Image|*.bmp|Png Image|*.png|Tiff Image|*.tiff";
                dlgOpen.Title = "Select an image to Load";
                dlgOpen.Multiselect = true;

                // only opens if the V user clicks OK
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {

                    // 'Filenames' is a property that holds an array of strings, iterating through the array each can be added to the LoadedImg list
                    foreach (string name in dlgOpen.FileNames)
                    {
                        CreateLoadedImg(name);
                    }
                    curImgIndex = 0; // change to equivalent of total length, or currently displayed??
                    UpdatePicbox(listLoadedImg[curImgIndex]);
                }
            }
        }

        // Run when a thumbnail picbox in the gallery is clicked, also applied to the label.
        public void picBox_Click(object sender, EventArgs e)
        {
            // Check what type the sender was (whether the user clicked on the label or the picturebox
            // The index is stored in the object as it's deaultName, and is used to the dictate the curimg index
            // Only UpdatePicbox if the clicked image != the one that's already arrIsProcessed
            if (sender is PictureBox && curImgIndex != Int32.Parse(((PictureBox)sender).Name))
            {
                curImgIndex = Int32.Parse(((PictureBox)sender).Name);
                UpdatePicbox(listLoadedImg[curImgIndex]);
            }

            else if (sender is Label && curImgIndex != Int32.Parse(((Label)sender).Name))
            {
                curImgIndex = Int32.Parse(((Label)sender).Name);
                UpdatePicbox(listLoadedImg[curImgIndex]);
            }
            UpdateGallerySelection();
        }

        private void btnNavigateLeft_Click(object sender, EventArgs e)
        {
            StepThroughImgList(-1);
        }

        private void btnNavigateRight_Click(object sender, EventArgs e)
        {
            StepThroughImgList(1);
        }

        private void MenuSaveImage_Click(object sender, EventArgs e)
        {
            // Creating an instance of the dialog to hold 
            using (SaveFileDialog dlgSaveImg = new SaveFileDialog())
            {
                dlgSaveImg.FileName = listLoadedImg[curImgIndex].GetName();
                dlgSaveImg.InitialDirectory = "C:/Desktop";
                dlgSaveImg.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";
                dlgSaveImg.Title = "Save Your Image";

                // Only initiate save if OK is received
                if (dlgSaveImg.ShowDialog() == DialogResult.OK)
                //SaveImage(dlgSaveImg, listLoadedImg[curImgIndex].GetBitmap("c"));
                {
                    switch (dlgSaveImg.FilterIndex)
                    {
                        case 1:
                            ImageFormat jpeg = ImageFormat.Jpeg;

                            SaveImage(dlgSaveImg.FileName, listLoadedImg[curImgIndex].GetBitmap("c"), ImageFormat.Jpeg);
                            break;
                        case 2:
                            SaveImage(dlgSaveImg.FileName, listLoadedImg[curImgIndex].GetBitmap("c"), ImageFormat.Bmp);
                            break;
                        case 3:
                            SaveImage(dlgSaveImg.FileName, listLoadedImg[curImgIndex].GetBitmap("c"), ImageFormat.Png);
                            break;
                        case 4:
                            SaveImage(dlgSaveImg.FileName, listLoadedImg[curImgIndex].GetBitmap("c"), ImageFormat.Tiff);
                            break;
                    }
                }
            }
        }

        private void MenuClearImage_Click(object sender, EventArgs e)
        {
            // Clears any image that might be in the pictureBox, and if there isn't any being displayed, opens a messagebox
            if (picBoxMain.Image != null)
            {
                // Resetting the scrollbars
                pnlPicBox.AutoScrollMinSize = new Size(0, 0);

                // Removes the currently displayed item from the list
                listLoadedImg.Remove(listLoadedImg[curImgIndex]);

                if (listLoadedImg.Count == 0)
                {
                    // Updating needs to be done here, because UpdatePicbox wont be called
                    curImgIndex = 0;
                    UpdateText();
                    UpdatePicboxInfoAndSizeMode();
                    UpdateImgOptions();
                }
                // If there is still an image loaded
                else
                {
                    // if on first index, list steps forward, load the new first object
                    if (curImgIndex == 0)
                    {
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                    }
                    // If not, adjust index int and refresh with new image
                    else
                    {
                        curImgIndex -= 1;
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                    }
                }
                UpdateGallery();
            }
            else
                MessageBox.Show("No image currently being displayed");
        }

        private void MenuCopyImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(picBoxMain.Image);
            MessageBox.Show("Image copied to clipboard");
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            // this (a component that represents the program being run from) runs the 'Close' function
            this.Close();
        }

        private void menuFitWindow_Click(object sender, EventArgs e)
        {
            // Getting Image's original size
            int picWidth = picBoxMain.Image.Width;
            int picHeight = picBoxMain.Image.Height;
            int galWidth;

            if (pnlGalleryHidden)
                galWidth = 25;
            else
                galWidth = 100;

            this.Size = new Size(picWidth + galWidth + 149, picHeight + 72);
        }

        private void MenuChangeBG_Click(object sender, EventArgs e)
        {
            // Runs the colour dialog in the if(), and if the user selects OK
            if (colourDialog1.ShowDialog() == DialogResult.OK)
                BackColor = colourDialog1.Color;

            menuResetBGColour.Enabled = true;
        }

        // Here lies applications of image effects and file handling
        private void menuBatch_Click(object sender, EventArgs e)
        {
            using (BatchSettings bs = new BatchSettings(this))
            {
                if (bs.ShowDialog() == DialogResult.OK)
                {
                    // Distinguish new collection, which images to apply Batching to?
                    List<LoadedImage> listBatch = new List<LoadedImage>();

                    for (int i = 0; i < listLoadedImg.Count; i++)
                    {
                        // Add each item that is marked for processing to the new batch list, arIsProcessed is populated in bs.GrabInputValues()
                        if (bs.arrIsProcessed[i] == true)
                        {
                            listBatch.Add(listLoadedImg[i]);
                        }
                    }

                    // Used for iterating filenames
                    int suffix = 0;

                    // Loop through each image
                    foreach (LoadedImage batchImg in listBatch)
                    {
                        suffix++; // Only iterate when moving to the next image
                        ApplyBatchSettingsEffects(bs, batchImg);
                        batchImg.ApplyPreview();

                        // If Exporting to file & Channels are not being exported
                        if (bs.arrChkOptions[0][0].Checked && !bs.arrChkOptions[4][0].Checked)
                        {
                            // Grab the curent ImageFormat from the bs form
                            ImageFormat expFormat = bs.GetImageFormat();
                            string filename, fileDir, filePath;

                            if (bs.provideDir)
                            {
                                fileDir = bs.GetValue("newFileDir");
                            }
                            else
                            {
                                fileDir = batchImg.defaultDir;
                            }

                            // if providing new file deaultName, and there's more than one image being exported
                            if (bs.provideName && listBatch.Count > 1)
                            {
                                filename = bs.GetValue("newFileName") + "_" + suffix.ToString();
                            }
                            // If (for some reason) there is only one image in the batch
                            else if (bs.provideName && listBatch.Count == 1)
                            {
                                filename = bs.GetValue("newFileName");
                            }
                            else
                            {
                                filename = batchImg.deaultName;
                            }

                            // Generate a full path based on name + directory
                            filePath = fileDir + "\\" + filename + "." + expFormat.ToString();
                            // batchImg.GetBitmap("c").Save(filePath, expFormat);
                            SaveImage(filePath, batchImg.GetBitmap("c"), expFormat);
                        }
                        // Exporting to file & exporting channels
                        else if (bs.arrChkOptions[0][0].Checked && bs.arrChkOptions[4][0].Checked)
                        {
                            // 4 channels, R, G, B, A
                            // Create a new array of Bitmaps, length based on maximum number to be exported
                            Bitmap[] imgExportChannels = new Bitmap[4];

                            // Loop through each channel position
                            for (int i = 0; i < 4; i++)
                            {
                                // if the checkbox is checked (e.g. "R") on the Batch dialog
                                if (bs.arrChannelOptions[i].Checked)
                                {
                                    string channelName = "";
                                    // Based on the channel position, Get a channel from the previewVer of this image. 
                                    // Put that result into this new array of channel images
                                    switch (i)
                                    {
                                        case 0: // R
                                            {
                                                imgExportChannels[i] = adjustImg.GetChannel("r", batchImg.GetBitmap("c"));
                                                channelName = "R";
                                            } break;
                                        case 1: // G
                                            {
                                                imgExportChannels[i] = adjustImg.GetChannel("g", batchImg.GetBitmap("c"));
                                                channelName = "G";
                                            } break;
                                        case 2: // B
                                            {
                                                imgExportChannels[i] = adjustImg.GetChannel("b", batchImg.GetBitmap("c"));
                                                channelName = "B";
                                            } break;
                                        case 3: // Alpha
                                            {
                                                if (bs.exportAlphaBW)
                                                {
                                                    imgExportChannels[i] = adjustImg.GetChannel("abw", batchImg.GetBitmap("c"));
                                                    channelName = "A"; // use the same suffix for both the alpha export options
                                                }
                                                else if (bs.exportAlphaTransp)
                                                {
                                                    imgExportChannels[i] = adjustImg.GetChannel("a", batchImg.GetBitmap("c"));
                                                    channelName = "A"; // use the same suffix for both the alpha export options
                                                }
                                            } break;
                                        default:
                                            {
                                                Environment.Exit(26);
                                            } break;

                                    }
                                    // Consider that there is some repeated code from the above save functionailty

                                    // Grab the curent ImageFormat from the bs form
                                    ImageFormat expFormat = bs.GetImageFormat();
                                    string filename, fileDir, filePath;

                                    if (bs.provideDir)
                                    {
                                        fileDir = bs.GetValue("newFileDir");
                                    }
                                    else
                                    {
                                        fileDir = batchImg.defaultDir;
                                    }

                                    // if providing new file deaultName, and there's more than one image being exported
                                    if (bs.provideName && listBatch.Count > 1)
                                    {
                                        filename = bs.GetValue("newFileName") + "_" + suffix.ToString() + "_" + channelName;
                                    }
                                    // If (for some reason) there is only one image in the batch
                                    else if (bs.provideName && listBatch.Count == 1)
                                    {
                                        filename = bs.GetValue("newFileName") + "_" + channelName;
                                    }
                                    else
                                    {
                                        filename = batchImg.deaultName + "_" + channelName;
                                    }

                                    // Generate a full path based on name + directory
                                    filePath = fileDir + "\\" + filename + "." + expFormat.ToString();
                                    // imgExportChannels[i].Save(filePath, expFormat);
                                    SaveImage(filePath, imgExportChannels[i], expFormat);
                                }
                                else
                                {
                                    imgExportChannels[i] = null; // This array position will be null
                                }
                            }
                        }
                    }
                }
                else // if Batch window OK ("Batch that Shit") is not selected
                {

                }
            }
            // Update the loaded picturebox window, in case there were any changes. 
            if (listLoadedImg.Count != 0)
                UpdatePicbox(listLoadedImg[curImgIndex]);
        }

        // Apply the current set of effects from the BatchSettings window
        // In all instances, the previewVer of the image is being updated, so the effects will stack
        public void ApplyBatchSettingsEffects(BatchSettings bs, LoadedImage img)
        {
            // Initially set previewVer to match currentVer, start from plain image as opposed to previous preview effects
            img.UpdatePreview(img.GetBitmap("c"));

            // TRANSFORMS
            // Scale
            if (bs.arrChkOptions[1][0].Checked)
            {
                img.UpdatePreview(adjustImg.GetScaledVer(img.GetBitmap("p"), bs.transformScale, bs.transformScale, true));
            }

            // FILTERS
            int filterToApply = -1;
            for (int i = 0; i < bs.arrChkOptions[3].Length; i++)
            {
                // If a filter is found checked, stop and begin to work on it. 
                if (bs.arrChkOptions[3][i].Checked)
                {
                    filterToApply = i;
                    break;
                }
            }

            // If a filter was found checked
            if (filterToApply != -1)
            {
                switch (filterToApply)
                {
                    case 0: // Sepia
                        {
                            // Get current bitmap, apply sepia and place in the preview slot of the loadedimg
                            img.UpdatePreview(adjustImg.GetSepia(img.GetBitmap("p")));
                        }
                        break;
                    case 1: // Grayscale
                        {
                            // tmp for readability. Apply Grayscale (with algorithm string from bs) to current version
                            Bitmap tmp = adjustImg.GetGrayscale(img.GetBitmap("p"), bs.grayAlgorithm);
                            // Then apply to preview slot
                            img.UpdatePreview(tmp);
                        }
                        break;
                    default:
                        {
                            Environment.Exit(25);
                        }
                        break;
                }
            }

            // ADJUSTMENTS

            // Transparency
            if (bs.arrChkOptions[2][0].Checked == true)
            {
                // The byte value is necessary for the image adjustment
                byte amount = bs.transpInput;
                // tmp for readability, 
                Bitmap tmp = adjustImg.GetTransparent(img.GetBitmap("p"), amount);
                img.UpdatePreview(tmp);
            }
        }

        private void MenuResetStretching_Click(object sender, EventArgs e)
        {
            // Getting Image's original size
            int picWidth = picBoxMain.Image.Width;
            int picHeight = picBoxMain.Image.Height;
            this.Size = new Size(picWidth + 149, picHeight + 72);
        }

        private void menuResetAdjustments_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                // Reverting the class' current image to match the original
                listLoadedImg[curImgIndex].UpdateBitmap(listLoadedImg[curImgIndex].GetBitmap("o"));
                UpdatePicbox(listLoadedImg[curImgIndex]);
            }
        }

        private void MenuResetBGColour_Click(object sender, EventArgs e)
        {
            Color tempBGColor = Color.FromArgb(defaultBG.A, defaultBG.R, defaultBG.G, defaultBG.B);
            BackColor = tempBGColor;
            menuResetBGColour.Enabled = false;
        }

        private void MenuDisplayBGInfo_Click(object sender, EventArgs e)
        {
            // Now, a single variable can be changed, as opposed to many. Should consider creating a function that takes a passed value
            Color colourToTest = BackColor;
            // Feeding argb values to an integer
            int argbInt = colourToTest.ToArgb();

            MessageBox.Show
                ("Name: " + colourToTest.Name +
                Environment.NewLine + "A: " + colourToTest.A + " R: " + colourToTest.R + " G: " + colourToTest.G + " B: " + colourToTest.B +
                Environment.NewLine + "Brightness: " + colourToTest.GetBrightness() +
                Environment.NewLine + "Hash Code: " + colourToTest.GetHashCode() +
                Environment.NewLine + "Hue: " + colourToTest.GetHue() +
                Environment.NewLine + "Saturation: " + colourToTest.GetSaturation() +
                Environment.NewLine + "Int value: " + argbInt.ToString()
                );

            // Because 'Color' is a struct, you cannot assign it 'null', instead the 'Empty' value is assigned.
            colourToTest = Color.Empty;
        }

        private void menuBatchResetAdjustments_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                foreach (LoadedImage img in listLoadedImg)
                {
                    img.UpdateBitmap(img.GetBitmap("o"));
                }
                UpdatePicbox(listLoadedImg[curImgIndex]);
            }
            else
            {
                MessageBox.Show("You must first load an image");
            }
        }

        // Known as an "Event Handler" becuase they are called when an event occurs in the program
        private void chkStretch_CheckedChanged(object sender, EventArgs e)
        {
            // Finds out if the box is/isn't checked after a click, runs the method that updates the sizemode based on chkStretched
            if (chkStretch.Checked)
            {
                // Make the font Bold, mimicing the current font's style, but making it bold
                chkStretch.Font = new Font(chkStretch.Font, FontStyle.Bold);
                UpdatePicboxInfoAndSizeMode();
            }
            else if (chkStretch.Checked == false)
            {
                // Make the font Bold, mimicing the current font's style, but making it not bold
                chkStretch.Font = new Font(chkStretch.Font, FontStyle.Regular);
                UpdatePicboxInfoAndSizeMode();
            }
            UpdateImgOptions();
        }

        private void forEachTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLoadedImg.Count > 0)
            {
                foreach (LoadedImage clunge in listLoadedImg)
                {
                    MessageBox.Show(clunge.GetName());
                }
            }
        }

        private void MenuHideGallery_Click(object sender, EventArgs e)
        {
            pnlGalleryHidden = !pnlGalleryHidden;
            UpdateGallery();
        }

        private void menuStepBackward_Click(object sender, EventArgs e)
        {
            listLoadedImg[curImgIndex].StepBackward();
        }

        private void menuTransp_Click(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                // The byte value is necessary for the image adjustments
                byte amount = 0;
                // Creating the Form that will be the dialog box
                using (Transparency dlgTransp = new Transparency())
                {
                    // Result is saved before check, so the result can be checked in more than one bool statement
                    DialogResult dlgResult = dlgTransp.ShowDialog();

                    if (dlgResult == DialogResult.OK)
                    {
                        amount = dlgTransp.getAmount();

                        // tmp created for readability, with transparency is applied separately. The main picBox image is also updated here.
                        Bitmap tmp = adjustImg.GetTransparent(listLoadedImg[curImgIndex].GetBitmap("c"), amount);
                        listLoadedImg[curImgIndex].UpdateBitmap(tmp);
                        UpdatePicbox(listLoadedImg[curImgIndex]);
                        //picBoxMain.Image = listLoadedImg[curImgIndex].GetBitmap("c");
                    }
                    else if (dlgResult == DialogResult.Cancel)
                    {
                        // Nothing
                    }
                    else
                        MessageBox.Show("Error");
                }
            }
        }

        private void menuSepia_Click_1(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                Bitmap tmp = adjustImg.GetSepia(listLoadedImg[curImgIndex].GetBitmap("c"));
                listLoadedImg[curImgIndex].UpdateBitmap(tmp);
            }
        }

        private void menuGrayscale_Click_1(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                Grayscale dlgGrayscale = new Grayscale();
                DialogResult dlgResult;
                dlgResult = dlgGrayscale.ShowDialog();

                // tmp created for readability, will eventually be applied to picBoxMain
                Bitmap tmp = null;

                // The 'Luminosity' button is set to "OK".
                if (dlgResult == DialogResult.OK)
                {
                    // tmp = ApplyGrayscale(listLoadedImg[curImgIndex].GetBitmap("c"), "luminosity");
                    tmp = adjustImg.GetGrayscale(listLoadedImg[curImgIndex].GetBitmap("c"), "luminosity");
                }
                // The 'Average' button is set to "Yes".
                else if (dlgResult == DialogResult.Yes)
                {
                    // tmp = ApplyGrayscale(listLoadedImg[curImgIndex].GetBitmap("c"), "average");
                    tmp = adjustImg.GetGrayscale(listLoadedImg[curImgIndex].GetBitmap("c"), "average");
                }
                else
                {
                    MessageBox.Show("An error has occured during the grayscale operation.");
                    Environment.Exit(22);
                }
                // If we reached here, a certain kind of grayscaling has been applied
                listLoadedImg[curImgIndex].UpdateBitmap(tmp);
                UpdatePicbox(listLoadedImg[curImgIndex]);
            }
        }

        private void menuStepForward_Click(object sender, EventArgs e)
        {
            listLoadedImg[curImgIndex].StepForward();
        }

        private void PicBoxMain_MouseOver(object sender, EventArgs e)
        {
            picBoxMain.Focus();
        }

        private void TxtImgWindowControl_ValueChanged(object sender, EventArgs e)
        {
            if (picBoxMain.Image != null)
            {
                // Save a version of the current image, in case an out of range exception (in GetScaledVer) returns a null
                Image oldImg = picBoxMain.Image;
                picBoxMain.Image = adjustImg.GetScaledVer(listLoadedImg[curImgIndex].GetBitmap("c"), (float)txtImgWindowControl.Value, (float)txtImgWindowControl.Value, false) ?? oldImg;
            }
        }

        private void PicboxMain_MouseWheel(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                // Grab mousewheel Delta (on testing, was either 120 or -120 depending on scroll direction).
                int mouseScrollValue = ((MouseEventArgs)e).Delta;
                ((HandledMouseEventArgs)e).Handled = true;

                // Create an offset which equals 10 + how ever many hundreds the current % value is displaying
                int offset = (int)((txtImgWindowControl.Value - (txtImgWindowControl.Value % 100)) / 10) + 10;

                // If it the value asignment wil be out of range, default to max
                if (mouseScrollValue > 0) // Going up
                {
                    if (txtImgWindowControl.Value + (0 + offset) > 999)
                    {
                        txtImgWindowControl.Value = 999;
                    }
                    else
                    {
                        txtImgWindowControl.Value += (0 + offset);
                    }                    
                }
                else // Going down
                {
                    if (txtImgWindowControl.Value + (0 - offset) < 1)
                    {
                        txtImgWindowControl.Value = 1;
                    }
                    else
                    {
                        txtImgWindowControl.Value += (0 - offset);
                    }                    
                }
            }            
        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            txtImgWindowControl.Value = 100;
        }
    }
}

/*
        private void ExportChannel(string channel, Bitmap img, bool bypass)
        {
            // Importantly, a completely new Bitmap has to be created from the passed image, to avoid the pointer (I think) editing things like curImg
            Bitmap channelImg = new Bitmap(img);
            BitmapData imgData = channelImg.LockBits(new Rectangle(0, 0, channelImg.Width, channelImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = imgData.Scan0;
            byte[] imgBuffer = new byte[imgData.Stride * imgData.Height];
            Marshal.Copy(dataPointer, imgBuffer, 0, imgBuffer.Length);

            // An array to hold the offset ints that represent the channels to be changed. 
            // B = 0 G = 1 R = 2 A = 3
            int[] bytesToChange = new int[2]; // initialised with 3 indicies, as R,G & B cases only require two channel edits, a third is added later only if necessary.
            byte value = 0;

            switch (channel)
            {
                case "R":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;                    //bytesToChange[2] = 1; // Duped to avoid editing the alpha - Maybe look at creating the array inside here to make less wasteful? 
                    value = 0;
                    break;
                case "G":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 2;                    //bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "B":
                    bytesToChange[0] = 1;
                    bytesToChange[1] = 2;                    //bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "A": case "ABW":
                    bytesToChange = new int[3];
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;
                    bytesToChange[2] = 2;
                    value = 255;
                    break;
            }

            for (int i = 0; i < imgBuffer.Length; i += 4)
            {
                // If we're exporting the B/W alpha image, the new value for R, G & B is equal to the alpha channel
                if (channel == "ABW")
                    value = imgBuffer[i + 3];
                // For each byte that is to be changed (as offset of current buffer pixel position), change to value
                for (int j = 0; j < bytesToChange.Length;  j++)
                {
                    imgBuffer[i + bytesToChange[j]] = value;
                }
            }
            Marshal.Copy(imgBuffer, 0, dataPointer, imgBuffer.Length);
            channelImg.UnlockBits(imgData);

            // Creating a minimally scoped instance of the dialog 
            using (SaveFileDialog dlgSaveChannel = new SaveFileDialog())
            {
                // Converging the alphas for the puspose of savedialog creation
                if (channel == "ABW")
                    channel = "A";

                // Preparing default state for dlg
                dlgSaveChannel.FileName = Path.GetFileNameWithoutExtension(listLoadedImg[curImgIndex].GetName()) + "_" + channel;
                dlgSaveChannel.InitialDirectory = "C://Desktop";
                dlgSaveChannel.Title = "Save (" + channel + ") Image Channel";
                dlgSaveChannel.Filter = "JPEG Image|*.jpg|BMP Image|*.bmp|PNG Image|*.png|TIFF Image|*.tiff";

                if (bypass == false)
                {
                    if (dlgSaveChannel.ShowDialog() == DialogResult.OK)
                        SaveImage(dlgSaveChannel, channelImg);
                }
                else
                {
                    // Need to get type from batch dialog instead
                    string path = listLoadedImg[curImgIndex].GetDefaultDir() + "\\" + Path.GetFileNameWithoutExtension(listLoadedImg[curImgIndex].GetName()) + "_" + channel + "." + listLoadedImg[curImgIndex].GetExportFormat().ToString();
                    // this has been changed channelImg.Save(path, listLoadedImg[curImgIndex].GetExportFormat());
                }
            }
        }

        private void ExportChannelMediator(string colourChannel, bool isBypassing)
        {
            if (colourChannel == "R" || colourChannel == "G" || colourChannel == "B" || colourChannel == "A")
                ExportChannel(colourChannel, listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);

            else if (colourChannel == "All")
            {
                // Runs the method once for each channel
                ExportChannel("R", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("G", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("B", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("A", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
            }
            else if (colourChannel == "AllBW")
            {
                ExportChannel("R", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("G", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("B", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
                ExportChannel("ABW", listLoadedImg[curImgIndex].GetBitmap("c"), isBypassing);
            }
            else
                MessageBox.Show("An error occurred when registering choice of colour channel.");
        }
 * 
 * private void menuExportChannels_Click(object sender, EventArgs e)
        {
            using (Channels dlgChannels = new Channels())
            {
                if (dlgChannels.ShowDialog() == DialogResult.OK)
                {
                    // Don't bypass dlg, since there's only 1 or 4 images being saved
                    ExportChannelMediator(dlgChannels.colourChannel, false);
                }
            }
        }
 * 
 * private void SaveImage(SaveFileDialog dlg, Image img)
        {
            // Create a MemoryStream that will be minimally scoped
            using (MemoryStream memStream = new MemoryStream())
            {
                // Save the image to the memorystream in it's native format
                // this has been changed img.Save(memStream, listLoadedImg[curImgIndex].GetOriginalFormat());

                // Creating an Image that can actually be saved - Should probably make everything up to this point a method
                // Should also incorporate some kind of using statement to close off the MemoryStream
                Image imgToSave = Image.FromStream(memStream);

                // FilterIndex appears to record which filetype is arrIsProcessed
                switch (dlg.FilterIndex)
                {
                    case 1:
                        // this has been changed imgToSave.Save(dlg.FileName, ImageFormat.Jpeg);
                        break;
                    case 2:
                        // this has been changed imgToSave.Save(dlg.FileName, ImageFormat.Bmp);
                        break;
                    case 3:
                        // this has been changed imgToSave.Save(dlg.FileName, ImageFormat.Png);
                        break;
                    case 4:
                        // this has been changed imgToSave.Save(dlg.FileName, ImageFormat.Tiff);
                        break;
                }
            } dlg.Dispose();
        }
        */