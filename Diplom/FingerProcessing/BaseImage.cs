using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Diplom
{
    class BaseImage
    {
        private int width, height;
        double[,] intMatrix;

        // Конструктор из битмапа
        public BaseImage(Bitmap img)
        {
            width = img.Width;
            height = img.Height;
            intMatrix = new double[img.Width, img.Height];
            BuildIntensMatrix(img);
        }

        // Конструктор из файла
        public BaseImage(string filePath)
        {
            Bitmap img = new Bitmap(filePath);
            width = img.Width;
            height = img.Height;
            intMatrix = new double[img.Width, img.Height];
            BuildIntensMatrix(img);
        }

        // Возвращаем матрицу интенсивностей
        public double[,] Image
        {
            get
            {
                return intMatrix;
            }
        }

        // Ширина изображения
        public int Width
        {
            get
            {
                return this.width;
            }
        }

        // Высота изображения
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        // Строим матрицу интенсивностей
        private void BuildIntensMatrix(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color t = bmp.GetPixel(i, j);
                    double p = t.R * 0.3 + t.G * 0.59 + t.B * 0.11;
                    intMatrix[i, j] = p;                   
                }
            }
        }
    }

    class BaseImageOperations : BaseImage
    {
        public BaseImageOperations(Bitmap img)
            : base(img)
        {
        }

        public BaseImageOperations(string filePath) 
            : base(filePath)
        {
        }

        public void changeContrast(int amount)
        {
        }
    }
}
