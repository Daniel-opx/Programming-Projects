using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Puzzle_5
{
    static internal class RegexPatterns
    {
        public static Regex extractNumbers = new Regex("\\d+");
        public static Regex ExtractBlock = new Regex("\\w+-\\w+-\\w+ \\w+:\\r\\n(\\d+ \\d+ \\d+\\r\\n)+");
    }
}
