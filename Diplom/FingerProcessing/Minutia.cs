using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Diplom
{
    class Minutia
    {
        private int x, y, angle;
        private bool type; // true - окончание, false - раздвоение

        // Координата по X
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        // Координата по Y
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        // Угол повторота относительно центра изображения
        public int ang
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        } 

        // Тип минюции
        public string Type
        {
            get
            {
                if (type)
                    return "окончание";
                else
                    return "раздвоение";
            }
            set
            {
                if (value == "окончание")
                    type = true;
                else if (value == "раздвоение")
                    type = false;
            }
        }

        public Minutia()
        {
            x = 0;
            y = 0;
            angle = 0;
        }

        public Minutia Clone()
        {
            return (Minutia)this.MemberwiseClone();
        }
    }

    class MinutiaList
    {
        private ArrayList bp, ep;

        // Возвращаем список раздвоений
        public ArrayList brP
        {
            get
            {
                return bp;
            }
            set
            {
                bp = value;
            }
        }

        // Возвращаем список окончаний
        public ArrayList enP
        {
            get
            {
                return ep;
            }
            set
            {
                ep = value;
            }
        }

        // Добавляем окончание
        public Minutia setBP
        {
            set
            {
                bp.Add(value.Clone());
            }
        }

        public Minutia setEP
        {
            set
            {
                ep.Add(value.Clone());
            }
        }

        // Добавляем раздвоение
        public MinutiaList()
        {
            bp = new ArrayList();
            ep = new ArrayList();
        }
    }
}
