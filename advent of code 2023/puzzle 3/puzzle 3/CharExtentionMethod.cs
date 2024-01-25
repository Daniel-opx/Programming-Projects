using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_3
{
    static public class CharExtensionMethod
    {
        static public bool isDigit(this char ch, char zero = '0', char nine = '9')
        {
            if (ch >= zero && ch <= nine) return true;
            return false;
        }
    }
}
