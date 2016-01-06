using System;
using System.IO; // Path
using System.Drawing; // Bitmap
using System.Drawing.Imaging; // Imageformat
using System.Windows.Forms; // PictureBox
using System.Collections.Generic; // Stack

namespace WfaPictureViewer
{
    public class LoadedImage
    {
        private PicViewer PicViewer { get; set; }
        private Bitmap originalVer { get; set; }
        private Bitmap currentVer { get; set; }
        private Bitmap previewVer { get; set; }
        private PictureBox thumbnail { get; set; }
        private Label lblThumb { get; set; }
        public string deaultName { get; private set; }
        public string defaultDir { get; private set; }
        private ImageFormat originalFormat { get; set; }
        private ImageFormat tmpExportFormat { get; set; }
        private double correctRatio { get; set; }
        private Stack<Bitmap> undo { get; set; }
        private Stack<Bitmap> redo { get; set; }

        // CONSTRUCTOR
        public LoadedImage(string path, Bitmap baseImage, PicViewer sender, int index)
        {
            //defaultLocation = Path.GetDirectoryName(path);
            PicViewer = sender; 
            originalVer = currentVer = baseImage; // baseImage already converted to ARGB

            deaultName = Path.GetFileNameWithoutExtension(path);
            defaultDir = Path.GetDirectoryName(path);
            originalFormat = baseImage.RawFormat;
            correctRatio = (float)currentVer.Width / (float)currentVer.Height;

            // Create label to dock over thumbnail
            lblThumb = new Label();
            lblThumb.Dock = DockStyle.Left;
            lblThumb.Size = new Size(23, 15);
            lblThumb.Font = new Font(lblThumb.Font, FontStyle.Bold);
            lblThumb.ForeColor = Color.White;
            lblThumb.TextAlign = ContentAlignment.TopCenter;
            lblThumb.BackColor = Color.FromArgb(50, 0, 0, 0);

            UpdateThumbnail();

            // Names are given the index as a string, they will eventually be converted back to ints and used as indexing definitions.
            //thumbnail.Name = lblThumb.Name = index.ToString();

            // Add eventhandler references to clicks
            thumbnail.Click += PicViewer.picBox_Click;
            lblThumb.Click += PicViewer.picBox_Click;

            undo = new Stack<Bitmap>();
            redo = new Stack<Bitmap>();

            // previewVer will match currentVer when created
            UpdatePreview(currentVer);
        }

        // Return one of the three bitmap properties.
        // c = currentVer, o = originalVer, p = previewVer
        public Bitmap GetBitmap(string ver)
        {
            switch (ver)
            {
                case "o":
                    {
                        return originalVer;
                    }
                case "c":
                    {
                        return currentVer;
                    }
                case "p":
                    {
                        return previewVer;
                    }
                default:
                    {
                        MessageBox.Show("An invalid Bitmap type was defined, please input o, c or p.");
                        Environment.Exit(20);
                        return null;
                    }
            }
        }

        public void UpdateBitmap(Bitmap img)
        {
            // Can be used for validating passed image
            if (true)
            {
                redo.Clear(); // Destroy redo stack
                undo.Push(currentVer); // Add curent state to undo stack
                currentVer = img;
                UpdateThumbnail(); // Update thumbnail
            }
        }
        public void UpdatePreview (Bitmap img)
        {
            // Can be used for validating passed image
            if (true)
            {
                previewVer = img;
            }
        }

        public PictureBox GetThumbnail()
        {
            return thumbnail;        
        }

        public void UpdateThumbnail() // Uses current Image to create a new thumbnail
        {
            thumbnail = new PictureBox();
            // New image height given that thumbnail width is 100: original height / original width x 100 = new height
            double newHeight = (double)originalVer.Height / (double)originalVer.Width * 100;
            // Rounding to the nearest int
            int newHeightRounded = (int)Math.Round(newHeight);
            // Assigning new height calculations ot thumbnail
            thumbnail.Image = new Bitmap(currentVer, new Size(100, newHeightRounded));
            thumbnail.Size = new Size(thumbnail.Image.Size.Width, thumbnail.Image.Size.Height);
            thumbnail.BorderStyle = BorderStyle.Fixed3D;
            thumbnail.Dock = DockStyle.Top;
            thumbnail.SizeMode = PictureBoxSizeMode.AutoSize;

            thumbnail.Controls.Add(lblThumb);
            thumbnail.Click += PicViewer.picBox_Click;
            PicViewer.UpdateGallery();
        }

        public void StepForward()
        {
            undo.Push(currentVer); // Add the currentVersion of the image to the undo.
            currentVer = redo.Pop(); // Consume the next redo object
            UpdateThumbnail();
            PicViewer.UpdateImgOptions();
        }

        public void StepBackward()
        {
            redo.Push(currentVer); // Add the current version to the redo stack
            currentVer = undo.Pop(); // consume the next object in the undo stack
            UpdateThumbnail();
            PicViewer.UpdateImgOptions();
        }

        public void ApplyPreview()
        {
            undo.Push(currentVer); // Add current version to undo queue
            currentVer = previewVer;
            previewVer = null; // consume previewVer after application to currentVer
            UpdateThumbnail();
            PicViewer.UpdateImgOptions();
        }

        public string GetName()
        {
            if (deaultName != null)
            return deaultName;
            else
            {
                return null;
            }                
        }

        public double GetCorrectRatio()
        {
            return correctRatio;
        }

        public ImageFormat GetOriginalFormat()
        {
            return originalFormat;
        }

        public ImageFormat GetExportFormat()
        {
            return tmpExportFormat;
        }

        public void UpdateExportFormat(ImageFormat frmt)
        {
            tmpExportFormat = frmt;
        }

        public string GetDefaultDir()
        {
            return defaultDir;
        }

        public string GetLblThumbText()
        {
            return lblThumb.Text;
        }

        public void UpdateLblThumbText(string txt)
        {
            lblThumb.Text = txt;
        }

        public void UpdateLblThumbName(string txt)
        {
            lblThumb.Name = txt;
        }

        public bool IsUndoEmpty()
        {
            if (undo.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsRedoEmpty()
        {
            if (redo.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
