using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace puzzle_3
{
    static public class ExtentionMethods
    {
        static public int IntLength(this int a)
        {
            return (int)Log10(a) + 1;
        }
        static public bool isDigit(this char ch, char zero = '0', char nine = '9')
        {
            if (ch >= zero && ch <= nine) return true;
            return false;
        }
    }
}
