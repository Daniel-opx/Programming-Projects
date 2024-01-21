using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_1
{
    public class EnumMethods
    {
        public enum Number
        {
            one = 1, two = 2, three = 3, four = 4, five = 5, six = 6, seven = 7, eight = 8, nine = 9
        }
       

        public static char GetLastChar(Enum myEnum) => myEnum.ToString()[^1];
        public static char GetFirstChar(Enum myEnum) 
            {
             int enumLength =myEnum.ToString().Length;
                return myEnum.ToString()[enumLength- enumLength];
        } 

    }
}
