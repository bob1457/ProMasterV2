using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProMaster.Infrastructure.Utilities
{
    public static class ImageProcessor
    {
        //********************************************
        // Temperarily disabled for future development
        //**********************************************
        //
        //public static void SaveResizedImage(string filePath, string fileName, string resizedFileName, int percent)
        //{
        //    Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

        //    Image resizedImg = ScaleByPercent(origImg, percent);

        //    resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Jpeg);
        //    resizedImg.Dispose();
        //    origImg.Dispose();
        //}

        public static void SaveResizedImage(string filePath, string fileName, string resizedFileName, int width, int height)
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

            Image resizedImg = FixedSize(origImg, width, height);

            resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Jpeg);
            resizedImg.Dispose();
            origImg.Dispose();

        }

        public static void SaveResizedImage(string filePath, string fileName, string resizedFileName, int width)
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

            //Image resizedImg = FixedSize(origImg, widith, height);

            Image resizedImg = ScaleImageByWidth(origImg, width);

            resizedImg.Save(Path.Combine(filePath, resizedFileName), ImageFormat.Jpeg);
            resizedImg.Dispose();
            origImg.Dispose();

        }


        public static Image ScaleByPercent(Image imgPhoto, int percent)
        {
            float nPercent = ((float)percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static Image ScaleImage(Image imgPhoto, int maxWidth, int maxHeight)
        {
            double ratioX = maxWidth / imgPhoto.Width;
            double ratioY = maxHeight / imgPhoto.Height;
            var ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(imgPhoto.Width * ratio);
            int newHeith = (int)(imgPhoto.Height * ratio);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeith);
            Graphics.FromImage(bmPhoto).DrawImage(imgPhoto, 0, 0, newWidth, newHeith);

            return bmPhoto; 
        }

        public static Image ScaleImageByWidth(Image imgPhoto, int maxWidth)
        {
            double ratio =( (double)maxWidth / imgPhoto.Width);
            //double ratioY = maxHeight / imgPhoto.Height;
            //var ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(imgPhoto.Width * ratio);
            int newHeight = (int)(imgPhoto.Height * ratio);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(bmPhoto).DrawImage(imgPhoto, 0, 0, newWidth, newHeight);

            return bmPhoto;
        }

        public static Image FixedSize(Image imgPhoto, int width, int height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static int origImgWidth(string filePath, string fileName)
        {
            //int origImageWidth = 0;

            Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

            return origImg.Width;
        }

        public static int origImgHeight(string filePath, string fileName)
        {
            Image origImg = Image.FromFile(Path.Combine(filePath, fileName));

            return origImg.Height;
        }

        public static string orgImgWidth(Image photoImg, string fileName)
        {
            return photoImg.Width.ToString();
        }

        public static string orgImgheight(Image photoImg, string fileName)
        {
            return photoImg.Height.ToString();
        }
    }
}
