using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diplom.FingerProcessing
{
    class MinutiaComapration
    {
        public static double sd(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public static double dd(int ang1, int ang2)
        {
            return Math.Min(Math.Abs(ang2 - ang1), 360 - Math.Abs((ang2 - ang1)));
        }

    }
}
