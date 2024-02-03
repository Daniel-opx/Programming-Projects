using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Puzzle_4
{
    internal class RegexPatterns
    {
       public static Regex cardNumberRegex = new Regex(@"(?<=Card\s{1,})\d+");
       public static Regex winningNumbersRegex = new Regex("(?<=: +)\\d+( *\\d{1,})*");
       public static Regex myNumbersRegex = new Regex("(?<=\\| +)(\\d+ *)+");
        public static Regex ExtractNumbers = new Regex(@"(\d{1,} *)");
    }
}
