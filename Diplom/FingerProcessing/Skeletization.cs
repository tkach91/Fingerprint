using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Diplom
{

    class Skeletization
    {
        private int w, h, f, t;
        private Bitmap bmp, resImg;
        private Color[] color;
        private Color setcol;
        private Point point;
        int[,] wArr;

        public Skeletization(Bitmap bm, bool fon, int[,] arr)
        {
            bmp = bm;
            w = bmp.Width;
            h = bmp.Height;
            f = 255;
            t = 0;

            wArr = arr;

            color = new Color[9];
            setcol = Color.White;
            point = new Point(0, 0);
            if (!fon)
            {
                f = 0;
                t = 255;
                setcol = Color.Black;
            }
        }

        public int[,] iSkeletized
        {
            get
            {
                return wArr;
            }
        }

        private void Skeletic(int[,] wArr)
        {
            int count = 1;
            int cc = 1;
            while (count != 0)
            {
                count = del(wArr);
                del_noise(wArr);
                cc++;
            }

            cc = 0;
        }

        public void Execute()
        {
            Skeletic(wArr);
        }

        // Удаляем пиксель по основному набору
        private int del(int[,] wArr)
        {
            int count = 0;
            for (int i = 1; i < h - 1; i++)
                for (int j = 1; j < w - 1; j++)
                {
                    if (wArr[j, i] == 0)
                        if (deletable(wArr, j, i))
                        {
                            wArr[j, i] = 1;
                            count++;
                        }
                }
            return count;
        }

        // удаляем пиксель по шумовому набору
        private void del_noise(int[,] wArr)
        {
            int count = 0;
            for (int i = 1; i < h - 1; i++)
                for (int j = 1; j < w - 1; j++)
                {
                    if (wArr[j, i] == 0)
                        if (deletable_noise(wArr, j, i))
                        {
                            wArr[j, i] = 1;
                        }
                }
        }

        // получаем участок размером 3 x 3 и передаём на проверку по основному шаблону
        private bool deletable(int[,] arr, int x, int y)
        {
            ArrayList a = new ArrayList();
            for (int i = y - 1; i < y + 2; i++)
            {
                for (int j = x - 1; j < x + 2; j++)
                {
                    a.Add(arr[j, i]);
                }
            }
            return check(a);
        }

        // получаем участок размером 3 x 3 и передаём на проверку на шумы
        private bool deletable_noise(int[,] arr, int x, int y)
        {
            ArrayList a = new ArrayList();
            for (int i = y - 1; i < y + 2; i++)
            {
                for (int j = x - 1; j < x + 2; j++)
                {
                    a.Add(arr[j, i]);
                }
            }
            return noise(a);
        }

        // принадлежность к основным шаблонам
        private bool check(ArrayList a)
        {
            if ((int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[7] == 0)
                return true;
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[7] == 0)
                return true;
            if ((int)a[1] == 0 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[6] == 1 && (int)a[7] == 1)
                return true;
            if ((int)a[1] == 0 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[7] == 0)
                return true;
            if ((int)a[0] == 1 && (int)a[1] == 0 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[6] == 1 && (int)a[7] == 0)
                return true;
            if ((int)a[1] == 0 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            if ((int)a[1] == 0 && (int)a[2] == 1 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[7] == 0 && (int)a[8] == 1)
                return true;

            return false;
        }

        // принадлежность к шумам
        private bool noise(ArrayList a)
        {
            // 1
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 2
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 1 && (int)a[7] == 0 && (int)a[8] == 0)
                return true;
            // 3
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 0 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 4
            if ((int)a[0] == 0 && (int)a[1] == 0 && (int)a[2] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 5
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 0 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 6
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 0 && (int)a[7] == 0 && (int)a[8] == 1)
                return true;
            // 7
            if ((int)a[0] == 0 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 8
            if ((int)a[0] == 1 && (int)a[1] == 0 && (int)a[2] == 0 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 9
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 0)
                return true;
            // 10
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 0 && (int)a[7] == 0 && (int)a[8] == 0)
                return true;
            // 11
            if ((int)a[0] == 0 && (int)a[1] == 1 && (int)a[2] == 1 && (int)a[3] == 0 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 0 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 12
            if ((int)a[0] == 0 && (int)a[1] == 0 && (int)a[2] == 0 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 1 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 1)
                return true;
            // 13
            if ((int)a[0] == 1 && (int)a[1] == 1 && (int)a[2] == 0 && (int)a[3] == 1 && (int)a[4] == 0 && (int)a[5] == 0 && (int)a[6] == 1 && (int)a[7] == 1 && (int)a[8] == 0)
                return true;

            return false;
        }

        public void invert()
        {
            for (int i = 1; i < h - 1; i++)
                for (int j = 1; j < w - 1; j++)
                {
                    if (wArr[j, i] == 1)
                        wArr[j, i] = 0;
                    else
                        wArr[j, i] = 1;
                }
        }

        /*private void createImg(Bitmap bmp)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (wArr[j, i] == 1)
                        bmp.SetPixel(j, i, Color.Black);
                    else
                        bmp.SetPixel(j, i, Color.White);
                }
            }
        }*/
    }
}

