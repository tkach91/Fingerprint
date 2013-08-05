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
        private int[,] wArr;
        private Color[] color;
        private Color setcol;
        private Point point;

        public Skeletization(Bitmap bm, bool fon, int[,] arr)
        {
            bmp = bm;
            w = bmp.Width;
            h = bmp.Height;
            wArr = arr;

            //color = new Color[9];
            //setcol = Color.White;
            //point = new Point(0, 0);
            /*if (!fon)
            {
                f = 0;
                t = 255;
                setcol = Color.Black;
            }*/
        }

        public Bitmap Skeletized
        {
            get
            {
                return bmp;
            }
        }

        public int[,] iSkeletized
        {
            get
            {
                return wArr;
            }
        }

        public void Execute()
        {
            Skeletic(/*bmp*/);
            /*for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    if (wArr[i, j] == 1)
                        bmp.SetPixel(i, j, System.Drawing.Color.White);
                    else
                        bmp.SetPixel(i, j, System.Drawing.Color.Black);
                }*/
        }

        private void Skeletic(/*Bitmap bmp*/)
        {
            while (true)
            {
                int flag = 0;
                for (int j = 1; j < w - 1; j++)
                {
                    for (int i = 1; i < h - 1; i++)
                    {
                        // Проверка на первый шаблон
                        if (wArr[j - 1, i - 1] == 0 && wArr[j + 1, i - 1] == 1 && wArr[j - 1, i] == 0
                            && wArr[j, i] == 1 && wArr[j + 1, i] == 1 && wArr[j - 1, i + 1] == 0 && wArr[j + 1, i + 1] == 1)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Второй шаблон
                        if (wArr[j - 1, i - 1] == 1 && wArr[j, i - 1] == 1 && wArr[j + 1, i - 1] == 1
                            && wArr[j, i] == 1 && wArr[j - 1, i + 1] == 0 && wArr[j, i + 1] == 0 && wArr[j + 1, i + 1] == 0)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Третий шаблон
                        if (wArr[j - 1, i - 1] == 1 && wArr[j + 1, i - 1] == 0 && wArr[j - 1, i] == 1
                            && wArr[j, i] == 1 && wArr[j - 1, i + 1] == 0 && wArr[j, i + 1] == 0 && wArr[j + 1, i + 1] == 0)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Четвёртый шаблон
                        if (wArr[j - 1, i - 1] == 1 && wArr[j, i - 1] == 0 && wArr[j + 1, i - 1] == 0
                            && wArr[j, i] == 1 && wArr[j - 1, i + 1] == 1 && wArr[j, i + 1] == 1 && wArr[j + 1, i + 1] == 1)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Пятый шаблон
                        if (wArr[j, i - 1] == 1 && wArr[j - 1, i] == 0 && wArr[j, i] == 1
                            && wArr[j + 1, i] == 1 && wArr[j - 1, i + 1] == 0 && wArr[j, i + 1] == 0)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Шестой шаблон
                        if (wArr[j, i - 1] == 1 && wArr[j - 1, i] == 1 && wArr[j, i] == 1
                            && wArr[j + 1, i] == 0 && wArr[j, i + 1] == 0 && wArr[j + 1, i + 1] == 0)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Седьмой шаблон
                        if (wArr[j, i - 1] == 0 && wArr[j + 1, i - 1] == 0 && wArr[j - 1, i] == 1
                            && wArr[j, i] == 1 && wArr[j + 1, i] == 0 && wArr[j, i + 1] == 1)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }
                        // Восьмой шаблон
                        if (wArr[j - 1, i - 1] == 0 && wArr[j, i - 1] == 0 && wArr[j - 1, i] == 0
                            && wArr[j, i] == 1 && wArr[j + 1, i] == 1 && wArr[j, i + 1] == 1)
                        {
                            flag++;
                            wArr[j, i] = 0;
                        }	
                        
                        /*color[0] = bmp.GetPixel(j - 1, i - 1);  // 1 строка, 1 столбец
                        color[1] = bmp.GetPixel(j - 1, i);      // 2 строка, 1 столбец
                        color[2] = bmp.GetPixel(j - 1, i + 1);  // 3 строка, 1 столбец
                        color[3] = bmp.GetPixel(j, i - 1);      // 1 строка, 2 столбец
                        color[4] = bmp.GetPixel(j, i);          // 2 строка, 2 столбец
                        color[5] = bmp.GetPixel(j, i + 1);      // 3 строка, 2 столбец
                        color[6] = bmp.GetPixel(j + 1, i - 1);  // 1 строка, 3 столбец
                        color[7] = bmp.GetPixel(j + 1, i);      // 2 строка, 3 столбец
                        color[8] = bmp.GetPixel(j + 1, i + 1);  // 3 строка, 3 столбец*/

                        /*// Проверка на первый шаблон
                        if (color[0].R == 0 && color[6].R == 255 && color[1].R == 0
                            && color[4].R == 255 && color[7].R == 255 && color[2].R == 0 && color[8].R == 255)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Второй шаблон
                        if (color[0].R == 255 && color[3].R == 255 && color[6].R == 255
                            && color[4].R == 255 && color[2].R == 0 && color[5].R == 0 && color[8].R == 0)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Третий шаблон
                        if (color[0].R == 255 && color[6].R == 0 && color[1].R == 255
                            && color[4].R == 255 && color[2].R == 0 && color[5].R == 0 && color[8].R == 0)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Четвёртый шаблон
                        if (color[0].R == 255 && color[3].R == 0 && color[6].R == 0
                            && color[4].R == 255 && color[2].R == 255 && color[5].R == 255 && color[8].R == 255)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Пятый шаблон
                        if (color[3].R == 255 && color[1].R == 0 && color[4].R == 255
                            && color[7].R == 255 && color[2].R == 0 && color[5].R == 0)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Шестой шаблон
                        if (color[3].R == 255 && color[1].R == 255 && color[4].R == 255
                            && color[7].R == 0 && color[5].R == 0 && color[8].R == 0)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Седьмой шаблон
                        if (color[3].R == 0 && color[6].R == 0 && color[1].R == 255
                            && color[4].R == 255 && color[7].R == 0 && color[5].R == 255)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }
                        // Восьмой шаблон
                        if (color[0].R == 0 && color[3].R == 0 && color[1].R == 0
                            && color[4].R == 255 && color[7].R == 255 && color[5].R == 255)
                        {
                            flag++;
                            bmp.SetPixel(j, i, setcol);
                        }	*/
                    }
                }

                if (flag == 0)
                    break;
            }

            for (int j = 1; j < w - 1; j++)
            {
                for (int i = 1; i < h - 1; i++)
                {
                    /*color[0] = bmp.GetPixel(j - 1, i - 1);
                    color[1] = bmp.GetPixel(j - 1, i);
                    color[2] = bmp.GetPixel(j - 1, i + 1);
                    color[3] = bmp.GetPixel(j, i - 1);
                    color[4] = bmp.GetPixel(j, i);
                    color[5] = bmp.GetPixel(j, i + 1);
                    color[6] = bmp.GetPixel(j + 1, i - 1);
                    color[7] = bmp.GetPixel(j + 1, i);
                    color[8] = bmp.GetPixel(j + 1, i + 1);*/

                    if (wArr[j - 1, i - 1] == f && wArr[j - 1, i] == t && wArr[j - 1, i + 1] == t
                            && wArr[j, i] == f)
                    {
                        wArr[j-1, i+1] = 0; i += 2; continue;
                    }
                    if (wArr[j - 1, i - 1] == t && wArr[j - 1, i] == t && wArr[j, i - 1] == t
                        && wArr[j, i] == f)
                    {
					    wArr[j-1, i-1] = 0; i += 2; continue;
                    }
                    if (wArr[j, i - 1] == t && wArr[j + 1, i - 1] == t && wArr[j + 1, i] == t
                        && wArr[j, i] == f)
                    {
                        wArr[j-1, i+1] = 0; i += 2; continue;
                    }
                    if (wArr[j + 1, i + 1] == t && wArr[j, i + 1] == t && wArr[j + 1, i] == t
                        && wArr[j, i] == f)
                    {
						wArr[j-1, i+1] = 0; i += 2; continue;
                    }
                }
            }

            /*if (f == 0)
                setcol = Color.White;
            else
                setcol = Color.Black;*/

            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    /*color[0] = bmp.GetPixel(j, i - 1);
                    color[1] = bmp.GetPixel(j, i);
                    color[2] = bmp.GetPixel(j, i + 1);*/

                    if (wArr[j, i-1] == 1 && wArr[j, i] == 1 && wArr[j, i+1] == 1)
                    { 
                        wArr[j, i] = 0; i++;
                    }
                }
            }

            for (int j = 1; j < w - 1; j++)
            {
                for (int i = 0; i < h; i++)
                {
                    /*color[0] = bmp.GetPixel(j - 1, i);
                    color[1] = bmp.GetPixel(j, i);
                    color[2] = bmp.GetPixel(j + 1, i);*/

                    if (wArr[j-1, i] == 1 && wArr[j, i] == 1 && wArr[j+1, i] == 1)
                    {
                        wArr[j, i] = 0; j++;
                    }
                }
            }
        }
    }
}