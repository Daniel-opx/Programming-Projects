using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace puzzle7
{
    public static class RgxPatterns
    {
        internal static Regex CatchHand = new Regex(@".{5}");
        internal static Regex CatchNums = new Regex(@"(?<= )\d{1,}");
        internal static Regex CatchEverything = new Regex(@".");
    }
}
