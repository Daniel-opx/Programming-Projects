using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static puzzle_2.RegexStuff;

namespace puzzle_2
{
    internal class CubeSet
    {
        public int redCubes { get; private set; }
        public int greenCubes { get;  private set; }
         public int blueCubes { get;  private set; }

        public CubeSet() {
            redCubes = 0;
            greenCubes = 0;
            blueCubes = 0;
        }
        public CubeSet(int redCubes, int greenCubes, int blueCubes)
        {
            this.redCubes = redCubes;
            this.greenCubes = greenCubes;
            this.blueCubes = blueCubes;
        }

        public static CubeSet CreateCube(string s)
        {
            var cube = new CubeSet();
            var matches = pullBallsFromSack.Matches(s);

            foreach(Match match in matches)
            {
                int cubesIncrement = int.Parse(catchNumOfBalls.Match(match.Value).Value);
                if (match.Value.Contains("blue"))
                {
                    cube.blueCubes =+ cubesIncrement;
                }
                if(match.Value.Contains("green"))
                {
                    cube.greenCubes=+cubesIncrement;
                }
                if(match.Value.Contains("red"))
                {
                    cube.redCubes=+cubesIncrement;
                }
            }

            //if (cube.blueCubes > 0 && cube.redCubes > 0 && cube.greenCubes > 0)
            //    return true;
            //return false;
            return cube;
        }
        
    }
}
