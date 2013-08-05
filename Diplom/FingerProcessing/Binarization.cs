using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Diplom
{
    // Класс бинаризованного изображения
    class BinarizedImage
    {
        private int width, height;
        private int[,] lst;

        // lst - матрица 0 и 1, соответствующая изображение
        // img - бинаризованное изображение
        public BinarizedImage(int[,] l, int w, int h)
        {
            width = w;
            height = h;
            lst = l;
        }

        // Ширина бинаризованного изображения
        public int Width
        {
            get
            {
                return width;
            }
        }

        // Высота бинаризованного изображения
        public int Height
        {
            get
            {
                return Height;
            }
        }

        // Возвращаем бинаризованный список
        public int[,] Binarized
        {
            get
            {
                return lst;
            }
        }

        // Возвращаем бинаризованый Bitmap
        public Bitmap FillBitmap()
        {
            Bitmap bmp = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (lst[i, j] == 1)
                        bmp.SetPixel(i, j, Color.Black);
                    else
                        bmp.SetPixel(i, j, Color.White);
                }
            }

            return bmp;
        }
    }

    // Класс бинаризации
    class Binarization
    {
        private BaseImage bImg;
        private BinarizedImage binImg;
        private double[,] difMatr;

        public Binarization(BaseImage img)
        {
            bImg = img;
            difMatr = new double[img.Width, img.Height];
        }W

        // Возвращаем бинаризованный список
        public int[,] Binarized
        {
            get
            {
                return binImg.Binarized;
            }
        }

        // Возвращаем бинаризованный Bitmap
        public Bitmap bitBinarized
        {
            get
            {
                return binImg.FillBitmap();
            }
        }

        // Первый способ - вычисление среднего значения интенсивности для всего изображения
        private int MiddleValue()
        {
            double intense = 0;

            for (int i = 0; i < bImg.Width; i++)
            {
                for (int j = 0; j < bImg.Height; j++)
                {
                    intense += bImg.Image[i, j];
                }
            }

            return (int)Math.Round(intense / (bImg.Width * bImg.Height));
        }

        // Бинаризация - первый способ
        private void Binarize_first()
        {
            int[,] lst = new int[bImg.Width, bImg.Height];
            double mid = 80;//MiddleValue();

            for (int i = 0; i < bImg.Width; i++)
            {
                for (int j = 0; j < bImg.Height; j++)
                {
                    if (bImg.Image[i, j] > mid)
                    {
                        lst[i, j] = 1;
                    }
                    else
                    {
                        lst[i, j] = 0;
                    }
                }
            }

            binImg = new BinarizedImage(lst, bImg.Width, bImg.Height);
        }

        // Просто выполняем бинаризацию
        public void Execute(int num)
        {
            //if (num == 1)
            Binarize_first();
            /*else if (num == 2)
                Binarize_second();
            else
                Binarize_third();*/
        }
    }
}
 