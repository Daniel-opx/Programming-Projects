using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_3
{
    internal class Coordinate
    {
        public int I { get; set; }
        public int J { get; set; }
        public Coordinate(int i ,int j) 
        {
            I = i;
            J = j;
            
        }
        public Coordinate()
        {
            
        }
    }
}
