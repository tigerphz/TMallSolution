using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace TMall.Infrastructure.Utility
{
    public class ImageHelper
    {
        /// <summary>
        ///  加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="WaterMarkPicPath">水印图片的地址</param>
        public static void addWatermarkImage(Image _Image, string WaterMarkPicPath)
        {
            try
            {
                Graphics picture = Graphics.FromImage(_Image);
                Image watermark = new Bitmap(WaterMarkPicPath);
                int _width = _Image.Width;
                int _height = _Image.Height;

                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.1f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };

                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //imageAttributes.SetColorKey(System.Drawing.Color.Black, System.Drawing.Color.Black, ColorAdjustType.Bitmap);

                int xpos = 0;
                int ypos = 0;
                int WatermarkWidth = 0;
                int WatermarkHeight = 0;
                //double bl = 1d;
                WatermarkWidth = _width;
                WatermarkHeight = _height;
                xpos = 0;
                ypos = 0;

                picture.DrawImage(watermark, new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                _Image = watermark;
                watermark.Dispose();
                imageAttributes.Dispose();
            }
            catch { }
        }
    }
}
