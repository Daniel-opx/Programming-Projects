using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionTree
{
    internal static class Extensions
    {
        public static string AddDoubleQuotes(this string value)
        {
            return "\"" + value + "\"";
        }
    }
}
