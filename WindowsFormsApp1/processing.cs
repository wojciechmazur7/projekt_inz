using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class processing
    {
        public processing()
        {

        }

        public static bool ConvertToGray(Bitmap b)
        {
            form1._Form1.updateRichTextBox("...przetwarzanie... \n");
            for (int i = 0; i < b.Width; i++)
            {
                form1._Form1.updateProgressBarValue(1);
                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int a = c1.A;
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;
                    int avg = (r1 + g1 + b1) / 3;
                    b.SetPixel(i, j, Color.FromArgb(255, avg, avg, avg));
                }
            }
            return true;
        }

        public static bool ConvertToNegative(Bitmap b)
        {
            form1._Form1.updateRichTextBox("...przetwarzanie... \n");
            for (int i = 0; i < b.Width; i++)
            {
                form1._Form1.updateProgressBarValue(1);
                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int a = c1.A;
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;
                    b.SetPixel(i, j, Color.FromArgb(255, 255 - r1, 255 - g1, 255 - b1));
                }
            }
            return true;
        }

        public static bool ConvertToProg(Bitmap b)
        {
            form1._Form1.updateRichTextBox("...przetwarzanie... \n");
            for (int i = 0; i < b.Width; i++)
            {
                form1._Form1.updateProgressBarValue(1);
                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int a = c1.A;
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;
                    if (r1 > 125) { r1 = 255; } else r1 = 0;
                    if (g1 > 125) { g1 = 255; } else g1 = 0;
                    if (b1 > 125) { b1 = 255; } else b1 = 0;
                    b.SetPixel(i, j, Color.FromArgb(255, r1, g1, b1));
                }
            }
            return true;
        }

        public static bool ChangeColor(Bitmap b, object red, object green, object blue)
        {
            int r = (int)red;
            int g = (int)green;
            int bl = (int)blue;

            form1._Form1.updateRichTextBox("...przetwarzanie... \n");
            for (int i = 0; i < b.Width; i++)
            {
                form1._Form1.updateProgressBarValue(1);
                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;   
                    
                    if (c1.R+r >= 255)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, 255, g1, b1));
                    }
                    else if (c1.R+r<= 0)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, 0, g1, b1));                        
                    }
                    else

                    b.SetPixel(i, j, Color.FromArgb(255, r1 + r, g1, b1));

                    if (c1.G + g >= 255)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, r1, 255, b1));
                     
                    }
                    else if (c1.G + g <= 0)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, r1, 0, b1));
                      
                    }
                    else

                        b.SetPixel(i, j, Color.FromArgb(255, r1, g1 + g, b1));

                    if (c1.B + bl >= 255)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, r1, g1, 255));
                       
                    }
                    else if (c1.B + bl <= 0)
                    {
                        b.SetPixel(i, j, Color.FromArgb(255, r1, g1 , 0));
                        
                    }
                    else

                        b.SetPixel(i, j, Color.FromArgb(255, r1, g1, b1+bl));

                }
            }
            return true;
        }

        public static void Blur(Bitmap b)
        {            
            form1._Form1.updateRichTextBox("...przetwarzanie... \n");
            int maskSize = 1;                      
            for (int i = 0; i < b.Width; i++)
            {
                form1._Form1.updateProgressBarValue(1);
                for (int j=0; j<b.Height; j++)
                {
                    int count = 0;
                    double[] colors = new double[3];

                    for (int i1 = i - maskSize; i1 <= i + maskSize; i1++)
                    {                        
                        for (int j1 = j - maskSize; j1 <= j + maskSize; j1++)
                        {
                            if (i1 >= 0 && j1 >= 0 && i1 < b.Width && j1 < b.Height)
                            {
                                Color RGB = b.GetPixel(i1, j1);
                                colors[0] += RGB.R;
                                colors[1] += RGB.G;
                                colors[2] += RGB.B;
                                count++;
                            }
                        }
                    }
                        colors[0] = colors[0] / count;
                        colors[1] = colors[1] / count;
                        colors[2] = colors[2] / count;
                        b.SetPixel(i, j, Color.FromArgb(255, Convert.ToByte(colors[0]), Convert.ToByte(colors[1]), Convert.ToByte(colors[2])));
                }
            }
           
        }
        public static bool MedianBlur2(Bitmap b)
        {
            int maskSize = 1;
            double[] colors = new double[3];
           
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int count = 0;
                    colors[0] = 0;
                    colors[1] = 0;
                    colors[2] = 0;

                    for (int i1 = i - maskSize; i1 <= i + maskSize; i1++)
                    {
                        for (int j1 = j - maskSize; j1 <= j + maskSize; j1++)
                        {
                            if (i1 >= 0 && j1 >= 0 && i1 < b.Width && j1 < b.Height)
                            {
                                Color RGB = b.GetPixel(i1, j1);
                                colors[0] += RGB.R;
                                colors[1] += RGB.G;
                                colors[2] += RGB.B;
                                count++;
                            }
                        }
                    }                   
                    b.SetPixel(i, j, Color.FromArgb(255, Convert.ToByte(colors[0]), Convert.ToByte(colors[1]), Convert.ToByte(colors[2])));
                }
            }
            return true;
        }
    }
}

