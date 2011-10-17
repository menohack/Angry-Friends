using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

//namespace Friend_Wars.Misc
//{
    public class Fractal
    {
        const int DIMENSION = 2;
        const double EPSILON = 200;

        public Fractal(Image fractalImage)
        {
            //BitmapSource src = (BitmapSource) fractalImage.Source;
            int width = 200;
            int height = 200;
            WriteableBitmap wb = new WriteableBitmap(width, height);
            //Color color = new Color();
            //color.B = new byte();

            double leftBound = -20.0;
            double rightBound = 20.0;
            double upBound = 10.0;
            double downBound = -10.0;
            

            for (int j = 0; j < wb.PixelHeight; j++)
            {
                for (int i = 0; i < wb.PixelWidth; i++)
                {
                    double x = leftBound + (i / (double)width) * (rightBound - leftBound);
                    double y = downBound + (j / (double)height) * (upBound - downBound);
                    if (mandelbox(x, y, width, height, 2.0, 0.5, 1.0, 0.0, 25))
                        wb.Pixels[j * wb.PixelWidth + i] = ConvertToARGB32(Color.FromArgb(255, 0, 0, 0));
                    else
                        wb.Pixels[j * wb.PixelWidth + i] = ConvertToARGB32(Color.FromArgb(255, 255, 255, 255));
                }
            }

            wb.Invalidate();

            fractalImage.Source = wb;
        }

        private bool mandelbox(double x, double y, int width, int height, double scale, double minRadius, double fixedRadius, double c, int iterations)
        {
            if (iterations < 0)
                return false;


            double[] v = new double[2];
            v[0] = x;
            v[1] = y;

            for (int iter = 0; iter < iterations; iter++)
            {
                for (int axis = 0; axis < DIMENSION; axis++)
                {
                    if (v[axis] > 1.0)
                        v[axis] = 2.0 - v[axis];
                    else if (v[axis] < -1.0)
                        v[axis] = -2.0 - v[axis];
                }

                if (magnitude(v) < minRadius)
                {
                    v[0] *= (fixedRadius * fixedRadius) / (minRadius * minRadius);
                    v[1] *= (fixedRadius * fixedRadius) / (minRadius * minRadius);
                }
                else if (magnitude(v) < fixedRadius)
                {
                    v[0] *= (fixedRadius * fixedRadius) / (magnitude(v) * magnitude(v));
                    v[1] *= (fixedRadius * fixedRadius) / (magnitude(v) * magnitude(v));
                }

                v[0] = scale * v[0] + c;
                v[1] = scale * v[1] + c;

                if (magnitude(v) > EPSILON)
                    return false;
            }

            return true;
        }

        private double magnitude(double[] v)
        {
            return Math.Sqrt(v[0] * v[0] + v[1] * v[1]);
        }

        private int ConvertToARGB32(Color color)
        {
            return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
        }
    }
//}
