using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D; // InterpolationMode etc.
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WfaPictureViewer
{
    class ImgAdjust
    {
        public Bitmap GetGrayscale(Bitmap passedImg, string algorithm)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Get the bit data from the image and draw it in to imgData
            BitmapData imgData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // A pointer is directed to the address of the first piece of data, this is now an int that holds the ARGB data of a given pixel (I think). A printf returns an int
            IntPtr dataPointer = imgData.Scan0;

            // An EMPTY array that will hold all of the data that makes up the image
            byte[] pixelByteBuffer = new byte[imgData.Stride * imgData.Height];

            // Copy data from the pointer to the buffer
            Marshal.Copy(dataPointer, pixelByteBuffer, 0, pixelByteBuffer.Length);

            // This will be used to hold the sum of the RGB values, and will be applied to each in turn
            float rgb;

            if (algorithm == "luminosity")
            {
                for (int i = 0; i < pixelByteBuffer.Length; i += 4)
                {
                    // 'Luminosity' method, places more priority on the green, as our eyes are more sensetive to that part of the spectrum
                    rgb = pixelByteBuffer[i] * 0.07f;
                    rgb += pixelByteBuffer[i + 1] * 0.72f;
                    rgb += pixelByteBuffer[i + 2] * 0.21f;

                    // The'amount of colour' value is given to each element. This syntax means the rgb value only needs to be cast once
                    pixelByteBuffer[i + 2] = pixelByteBuffer[i + 1] = pixelByteBuffer[i] = (byte)rgb;
                }
            }
            else if (algorithm == "average")
            {
                for (int i = 0; i < pixelByteBuffer.Length; i += 4)
                {
                    // "Average method", the sum of the partial B, G, and R values gives an average 'amount of colour' value
                    rgb = (pixelByteBuffer[i] + pixelByteBuffer[i + 1] + pixelByteBuffer[i + 2]) / 3;

                    // The'amount of colour' value is given to each element. This syntax means the rgb value only needs to be cast once
                    pixelByteBuffer[i + 2] = pixelByteBuffer[i + 1] = pixelByteBuffer[i] = (byte)rgb;
                }
            }

            // Copy the data back to the pointer
            Marshal.Copy(pixelByteBuffer, 0, dataPointer, pixelByteBuffer.Length);

            sourceImg.UnlockBits(imgData);
            pixelByteBuffer = null;
            imgData = null;
            return sourceImg;
        }

        public Bitmap GetSepia(Bitmap passedImg)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Lock the pixel data from the source image into imgData, a BitmapData Type container. 
            BitmapData imgData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = imgData.Scan0;

            byte[] imgBuffer = new byte[imgData.Stride * imgData.Height];

            // give data to buffer
            Marshal.Copy(dataPointer, imgBuffer, 0, imgBuffer.Length);
            float B, G, R;

            // Apply Sepia filtering
            for (int i = 0; i < imgBuffer.Length; i += 4)
            {

                // Original RGB values are saved because they are used to construct the new ones and need to stay unchanged.
                B = 0;
                G = 0;
                R = 0;

                B = (imgBuffer[i] * 0.13f + imgBuffer[i + 1] * 0.53f + imgBuffer[i + 2] * 0.27f);
                G = (imgBuffer[i] * 0.16f + imgBuffer[i + 1] * 0.68f + imgBuffer[i + 2] * 0.34f);
                R = (imgBuffer[i] * 0.18f + imgBuffer[i + 1] * 0.76f + imgBuffer[i + 2] * 0.39f);

                if (B > 255)
                    B = 255;

                if (G > 255)
                    G = 255;

                if (R > 255)
                    R = 255;

                imgBuffer[i] = (byte)B;
                imgBuffer[i + 1] = (byte)G;
                imgBuffer[i + 2] = (byte)R;
            }

            // give back to pointer
            Marshal.Copy(imgBuffer, 0, dataPointer, imgBuffer.Length);
            // give to bitmap type image for return
            sourceImg.UnlockBits(imgData);
            imgBuffer = null;
            imgData = null;
            return sourceImg;
        }

        public Bitmap GetChannel(string channel, Bitmap img)
        {
            if (img == null)
            {
                return null;
            }
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
                case "r":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 1;                    //bytesToChange[2] = 1; // Duped to avoid editing the alpha - Maybe look at creating the array inside here to make less wasteful? 
                    value = 0;
                    break;
                case "G":
                case "g":
                    bytesToChange[0] = 0;
                    bytesToChange[1] = 2;                    //bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "B":
                case "b":
                    bytesToChange[0] = 1;
                    bytesToChange[1] = 2;                    //bytesToChange[2] = 2; // Duped to avoid editing the alpha
                    value = 0;
                    break;
                case "A":
                case "a":
                case "ABW":
                case "abw":
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
                for (int j = 0; j < bytesToChange.Length; j++)
                {
                    imgBuffer[i + bytesToChange[j]] = value;
                }
            }
            Marshal.Copy(imgBuffer, 0, dataPointer, imgBuffer.Length);
            channelImg.UnlockBits(imgData);

            return channelImg;
        }

        // Converts an image into 32bit ARGB format for editing
        public Bitmap GetArgbVer(Image sourceImg)
        {
            // Here, the Bitmap is created, matching the size of the source image
            Bitmap newBmp = new Bitmap(sourceImg.Width, sourceImg.Height, PixelFormat.Format32bppArgb);

            // DISCLAIMER: I dont fully understand the following code
            // 'Graphics.FromImage' creates a Graphics object that is associated with a specified Image object.
            // The image still hasn't been drawn here yet, gfx is now created and associated with newBmp (which is still just a container)
            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                // Here, the DrawImage function is able to draw based on an existing image. 
                gfx.DrawImage(sourceImg, new Rectangle(0, 0, newBmp.Width, newBmp.Height), new Rectangle(0, 0, newBmp.Width, newBmp.Height), GraphicsUnit.Pixel);
                gfx.Flush();
            }
            return newBmp;
        }

        // Apply transparency to the supplied image, defaulting the value to 100
        public Bitmap GetTransparent(Bitmap passedImg, byte newAlphaAmount = 100)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Using BitmapData, the Lockbits method can be used to extract the image's pixel pixelData
            // Lockbits 'locks' a bitmap in to memory
            BitmapData pixelData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // From here on, whenever the pointer is changed, it is changing the data stored at the pointer address

            // A pointer directed at the location of the first pixel read by LockBits. I believe this accesses the B, G, R, A info, as opposed to the pixels themselves
            IntPtr pixelDataPointer = pixelData.Scan0;

            // Here, an array of all the bytes that make up the pixles is created, The stride is the width of the array when also accounting for the extra buffering area.
            byte[] pixelByteArray = new byte[pixelData.Stride * pixelData.Height];


            // Now the Marshal.Copy function copies pixel pixelData from pointer > byte array, preparing it for editing
            Marshal.Copy(pixelDataPointer, pixelByteArray, 0, pixelByteArray.Length);

            for (int i = 3; i < pixelByteArray.Length; i += 4)
            {
                pixelByteArray[i] = newAlphaAmount;
            }

            // Copy the byte pixelData back to the pointer, noting that the formatting of the 0 moves to follow the array
            Marshal.Copy(pixelByteArray, 0, pixelDataPointer, pixelByteArray.Length);

            // The data does not have to be passed to pixelData, because the pointer address was pointing to the data all along.

            // The new edited pixels are passed back to the image
            sourceImg.UnlockBits(pixelData);

            pixelByteArray = null;
            pixelData = null;

            return sourceImg;
        }

        // Experimenting with trying to compartmentalise parts of the ApplyEffect() process
        private byte[] GetByteArray(Bitmap passedImg)
        {
            // Creating a new memory assignment, so the pointer(I think) doesn't chance the originalImg
            Bitmap sourceImg = new Bitmap(passedImg);

            // Using BitmapData, the Lockbits method can be used to extract the image's pixel pixelData
            // Lockbits 'locks' a bitmap in to memory
            BitmapData pixelData = sourceImg.LockBits(new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // From here on, whenever the pointer is changed, it is changing the data stored at the pointer address

            // A pointer directed at the location of the first pixel read by LockBits. I believe this accesses the B, G, R, A info, as opposed to the pixels themselves
            IntPtr pixelDataPointer = pixelData.Scan0;

            // Here, an array of all the bytes that make up the pixles is created, The stride is the width of the array when also accounting for the extra buffering area.
            byte[] imgBuffer = new byte[pixelData.Stride * pixelData.Height];

            // Now the Marshal.Copy function copies pixel pixelData from pointer > byte array, preparing it for editing
            Marshal.Copy(pixelDataPointer, imgBuffer, 0, imgBuffer.Length);
            /* 
             // SWITCH FOR EFFECT

             // Copy the byte pixelData back to the pointer, noting that the formatting of the 0 moves to follow the array
             Marshal.Copy(imgBuffer, 0, pixelDataPointer, imgBuffer.Length);

             // The data does not have to be passed to pixelData, because the pointer address was pointing to the data all along.

             // The new edited pixels are passed back to the image
             sourceImg.UnlockBits(pixelData);

             imgBuffer = null;
             pixelData = null;
             */
            return imgBuffer;
        }

        // Return a scaled version of the image, using percentages for input
        public Bitmap GetScaledVer(Bitmap passedImg, float scaleX, float scaleY, bool highQ)
        {
            // Adjust for percentage-based input
            int newScaleX = (int)(passedImg.Width * (scaleX / 100));
            int newScaleY = (int)(passedImg.Height * (scaleY / 100));
            
            try 
            {
                Bitmap scaledVer = new Bitmap(passedImg, new Size(newScaleX, newScaleY));
                using (Graphics graphics = Graphics.FromImage(scaledVer))
                {
                    if (highQ)
                    {
                        // using System.Drawing.2d, these define the resizing algorithms 
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    }
                    else
                    {
                        // using System.Drawing.2d, these define the resizing algorithms 
                        graphics.InterpolationMode = InterpolationMode.Low;
                        graphics.SmoothingMode = SmoothingMode.HighSpeed;
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.PixelOffsetMode = PixelOffsetMode.None;
                    }

                    graphics.DrawImage(passedImg, new Rectangle(0, 0, newScaleX, newScaleY));
                }
                return scaledVer;
            }
            catch (ArgumentException Exception)
            {
                MessageBox.Show("Image size too large or too small!\n" + Exception.ToString());
            }
            // Return null if out of range exception is triggered
            return null;            
        }

        public Bitmap GetRotatedVer(Bitmap passedImg, int numDegrees)
        {
            // Create an empty Bitmap
            Bitmap newImg = new Bitmap(passedImg);
            
            switch(numDegrees)
            {
                case 90:
                    {
                        newImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    break;
                case 180:
                    {
                        newImg.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    }
                    break;
                case 270:
                    {
                        newImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    break;
                default:
                    {
                        MessageBox.Show("You dun fucked up!");
                    }
                    break;
            }
            return newImg;
        }

        public Bitmap GetFlippedVer(Bitmap passedImg, bool flipX, bool flipY)
        {
            Bitmap newImg = new Bitmap(passedImg);

            if (flipX && flipY)
            {
                newImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            }
            else if (flipX && !flipY)
            {
                newImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            else if (!flipY && flipX)
            {
                newImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            return newImg;            
        }
    }
}
