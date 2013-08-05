using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Diplom
{
    class MinutiaMapCreation
    {
        private int w, h;
        private Bitmap bmp, bmp1;
        /*private List<string> branchPoint;
        private List<string> endPoint;*/
        //private ArrayList iBP, iEP;
        private int[,] wArr; 
        MinutiaList min;
        Minutia minu;

        public Bitmap Minutia
        {
            get
            {
                return bmp;
            }
        }

        public Bitmap Minutia1
        {
            get
            {
                return bmp1;
            }
        }

        public MinutiaMapCreation(Bitmap bm, Bitmap bm1, int[,] arr)
        {
            bmp = bm;
            bmp1 = bm1;
            w = bmp.Width;
            h = bmp.Height;
            wArr = arr;
            /*branchPoint = new List<string>();
            endPoint = new List<string>();*/
            //iBP = new ArrayList();
            //iEP = new ArrayList();
        }

        /*// Подсчет количества черных точек в окрестности
        private int checkPoint(int[,] wArr, int x, int y)
        {
            int c = 0;
            // Color cl = new Color();
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    //cl = bmp.GetPixel(i, j);
                    if (wArr[i, j] == 0)
                        c++;   
                }
            }

            return --c;
        }

        // Формируем списки точек ветвления и конечных точек
        private void findCheckPoint(int[,] wArr)
        {
            int t = 0;
            //Color cl = new Color();
            int[] bp = new int[2];
            int[] ep = new int[2];

            int x = w, y = h;

            for (int i = 1; i < x - 1; i++)
            {
                for (int j = 1; j < y - 1; j++)
                {
                    //cl = bmp.GetPixel(i, j);
                    if (wArr[i, j] == 1)
                        t = checkPoint(wArr, i, j);

                    if (t == 1)
                    {
                        ep[0] = i; ep[1] = j;
                        endPoint.Add("x:" + i.ToString() + " " + "y:" + j.ToString());
                        iEP.Add(ep.Clone());
                        bmp.SetPixel(i, j, Color.Red);
                    }
                    else
                    {
                        if (wArr[i, j] == 1)
                            bmp.SetPixel(i, j, Color.Black);
                        else
                            bmp.SetPixel(i, j, Color.White);
                    }

                    if (t == 3)
                    {
                        bp[0] = i; bp[1] = j;
                        branchPoint.Add("x:" + i.ToString() + " " + "y:" + j.ToString());
                        iBP.Add(bp.Clone());
                        bmp.SetPixel(i, j, Color.Red);
                    }
                    else
                    {
                        if (wArr[i, j] == 1)
                            bmp.SetPixel(i, j, Color.Black);
                        else
                            bmp.SetPixel(i, j, Color.White);
                    }
                }
            }
        }*/

        private void setColor(int j, int i, bool type)
        {
            if (type)
            {
                bmp.SetPixel(j - 1, i - 1, Color.Red);
                bmp.SetPixel(j - 1, i, Color.Red);
                bmp.SetPixel(j - 1, i + 1, Color.Red);
                bmp.SetPixel(j, i - 1, Color.Red);
                bmp.SetPixel(j, i + 1, Color.Red);
                bmp.SetPixel(j + 1, i - 1, Color.Red);
                bmp.SetPixel(j + 1, i, Color.Red);
                bmp.SetPixel(j + 1, i + 1, Color.Red);
            }
            else
            {
                bmp.SetPixel(j - 1, i - 1, Color.Blue);
                bmp.SetPixel(j - 1, i, Color.Blue);
                bmp.SetPixel(j - 1, i + 1, Color.Blue);
                bmp.SetPixel(j, i - 1, Color.Blue);
                bmp.SetPixel(j, i + 1, Color.Blue);
                bmp.SetPixel(j + 1, i - 1, Color.Blue);
                bmp.SetPixel(j + 1, i, Color.Blue);
                bmp.SetPixel(j + 1, i + 1, Color.Blue);
            }

        }


        private void setColor1(int j, int i, bool type)
        {
            if (type)
            {
                bmp1.SetPixel(j - 1, i - 1, Color.Red);
                bmp1.SetPixel(j - 1, i, Color.Red);
                bmp1.SetPixel(j - 1, i + 1, Color.Red);
                bmp1.SetPixel(j, i - 1, Color.Red);
                bmp1.SetPixel(j, i + 1, Color.Red);
                bmp1.SetPixel(j + 1, i - 1, Color.Red);
                bmp1.SetPixel(j + 1, i, Color.Red);
                bmp1.SetPixel(j + 1, i + 1, Color.Red);
            }
            else
            {
                bmp1.SetPixel(j - 1, i - 1, Color.Blue);
                bmp1.SetPixel(j - 1, i, Color.Blue);
                bmp1.SetPixel(j - 1, i + 1, Color.Blue);
                bmp1.SetPixel(j, i - 1, Color.Blue);
                bmp1.SetPixel(j, i + 1, Color.Blue);
                bmp1.SetPixel(j + 1, i - 1, Color.Blue);
                bmp1.SetPixel(j + 1, i, Color.Blue);
                bmp1.SetPixel(j + 1, i + 1, Color.Blue);
            }

        }

        private void findCheckPoint(int[,] wArr)
        {
            min = new MinutiaList();
            minu = new Minutia();
            for (int j = 1; j < w - 1; j++)
            {
                for (int i = 1; i < h - 1; i++)
                {
                    /* Окончания */
                    // Проверка на первый шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 1 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i;  minu.ang = 270; minu.Type = "окончание";
                        min.setEP = minu;
                        //endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Второй шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 1 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 225; minu.Type = "окончание";
                        min.setEP = minu;
                        //endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Третий шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 1 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 180; minu.Type = "окончание";
                        min.setEP = minu;
                        // endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Четвёртый шаблон
                    if (wArr[j - 1, i - 1] == 1 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 135; minu.Type = "окончание";
                        min.setEP = minu;
                        // endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Пятый шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 1 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 01 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 90; minu.Type = "окончание";
                        min.setEP = minu;
                        // endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Шестой шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 1
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 45; minu.Type = "окончание";
                        min.setEP = minu;
                        // endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Седьмой шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 1
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 0; minu.Type = "окончание";
                        min.setEP = minu;
                        // endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Восьмой шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 1)
                    {
                        //setColor(j, i, true);
                        minu.X = j; minu.Y = i; minu.ang = 315; minu.Type = "окончание";
                        min.setEP = minu;
                        // endPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }

                    /* Раздвоения */
                    // Проверка на первый шаблон
                    if (wArr[j - 1, i - 1] == 1 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 1
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 90; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Второй шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 1 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 1
                        && wArr[j + 1, i - 1] == 1 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 45; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Третий шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 1
                        && wArr[j, i - 1] == 1 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 1)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 0; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Четвёртый шаблон
                    if (wArr[j - 1, i - 1] == 1 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 1
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 1 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 315; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Пятый шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 1 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 1 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 1)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 270; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Шестой шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 1
                        && wArr[j, i - 1] == 1 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 1 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 225; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Седьмой шаблон
                    if (wArr[j - 1, i - 1] == 1 && wArr[j - 1, i] == 0 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 0 && wArr[j, i] == 1 && wArr[j, i + 1] == 1
                        && wArr[j + 1, i - 1] == 1 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 0)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 180; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                    // Восьмой шаблон
                    if (wArr[j - 1, i - 1] == 0 && wArr[j - 1, i] == 1 && wArr[j - 1, i + 1] == 0
                        && wArr[j, i - 1] == 1 && wArr[j, i] == 1 && wArr[j, i + 1] == 0
                        && wArr[j + 1, i - 1] == 0 && wArr[j + 1, i] == 0 && wArr[j + 1, i + 1] == 1)
                    {
                        //setColor(j, i, false);
                        minu.X = j; minu.Y = i; minu.ang = 135; minu.Type = "раздвоение";
                        min.setBP = minu;
                        // branchPoint.Add("x:" + j.ToString() + " " + "y:" + i.ToString());
                    }
                }
            }
        }

        private void delNoisePoints()
        {
            ArrayList ep = min.enP;
            ArrayList bp = min.brP;
            showPoints1(ep, bp);
            Minutia em, bm;
            foreach (object oep in ep)
            {
                foreach (object obp in bp)
                {
                    em = (Minutia)oep;
                    bm = (Minutia)obp;
                    if ((Math.Abs(em.X - bm.X) < 10) && (Math.Abs(em.Y - bm.Y) < 10))
                    {
                        ep.Remove(ep);
                        // bp.RemoveAt(j);
                    }
                }
            }

            for (int i = 0; i < ep.Count; i++)
            {
                em = (Minutia)ep[i];
                for (int j = 0; j < ep.Count; j++)
                {
                    Minutia tmp = (Minutia)ep[j];
                    if ((Math.Abs(tmp.X - em.X) < 30) && (Math.Abs(tmp.Y - em.Y) < 30))
                    {
                        ep.RemoveAt(j);
                    }
                }
            }

            for (int i = 0; i < bp.Count; i++)
            {
                bm = (Minutia)bp[i];
                for (int j = 0; j < bp.Count; j++)
                {
                    Minutia tmp = (Minutia)bp[j];
                    if ((Math.Abs(tmp.X - bm.X) < 30) && (Math.Abs(tmp.Y - bm.Y) < 30))
                    {
                        bp.RemoveAt(j);
                    }
                }
            }

            min.enP = ep;
            min.brP = bp;

            showPoints(ep, bp);
        }

        private void showPoints(ArrayList ep, ArrayList bp)
        {
            Minutia tmp;
            for (int i = 0; i < ep.Count; i++)
            {
                tmp = (Minutia)ep[i];
                setColor(tmp.X, tmp.Y, true);
            }

            for (int i = 0; i < bp.Count; i++)
            {
                tmp = (Minutia)bp[i];
                setColor(tmp.X, tmp.Y, false);
            }
        }

        private void showPoints1(ArrayList ep, ArrayList bp)
        {
            Minutia tmp;
            for (int i = 0; i < ep.Count; i++)
            {
                tmp = (Minutia)ep[i];
                setColor1(tmp.X, tmp.Y, true);
            }

            for (int i = 0; i < bp.Count; i++)
            {
                tmp = (Minutia)bp[i];
                setColor1(tmp.X, tmp.Y, false);
            }
        }

        /*private void WriteFile()
        {
            StreamWriter fs = new StreamWriter("bp.txt");
            branchPoint.ForEach(delegate(string data) { fs.WriteLine(data + "\n"); });
            fs.Close();

            StreamWriter fs1 = new StreamWriter("ep.txt");
            endPoint.ForEach(delegate(string data) { fs1.WriteLine(data + "\n"); });
            fs1.Close();
        }*/

        public void Execute()
        {
            findCheckPoint(wArr);
            delNoisePoints();
            //WriteFile();
        }
    }
}
