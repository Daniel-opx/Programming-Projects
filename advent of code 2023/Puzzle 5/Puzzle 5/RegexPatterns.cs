using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Puzzle_5
{
    static public class RegexPatterns
    {
        public static Regex extractNumbers = new Regex("\\d+");
        public static Regex ExtractBlock = new Regex("\\w+-\\w+-\\w+ \\w+:\\r\\n(\\d+ \\d+ \\d+\\r\\n)+");


      public static bool ContainsInRange(this HashSet<long> hash, long start, long end)
        {
            for (long i = start; i < end; i++)
            {
                if (hash.Contains(i)) return true;
            }
            return false;
        }
    }

   
}
