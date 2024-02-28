using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_6
{
    public static class GeneralStaticMethods
    {
        public static int CountNumOfIntInRange(double rangeStart, double rangeEnd) //In C#, Enumerable.Range is inclusive for the starting value and exclusive for the ending value.
        {
            if (!isNatural(rangeEnd))
            {
                int count = (int)(Math.Floor(rangeEnd) - (rangeStart));
                return Enumerable.Range((int)rangeStart + 1, count).Count();
            }
            else
            {
                return Enumerable.Range((int)rangeStart + 1, (int)(rangeEnd - rangeStart-1)).Count();
            }


        }




        public static bool isNatural(double d)
        {
            double a = d;
            int b = (int)a;
            if (d != b) return false;
            return true;
        }
    }
}
