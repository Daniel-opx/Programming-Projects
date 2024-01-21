

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace puzzle_2
{
    internal class RegexStuff 
    {
        
        readonly static public Regex catchGameNum = new Regex(@"(?<=Game\s)\d{1,}");
        readonly static public Regex pullBallsFromSack = new Regex("(\\d{1,} (red|green|blue))");
        readonly public static Regex seperateBySemicolon = new Regex("(\\d{1,} (red|green|blue)(, )?){1,}");
        readonly public static Regex catchNumOfBalls = new Regex(@"\d{1,}");
        
    }
}
