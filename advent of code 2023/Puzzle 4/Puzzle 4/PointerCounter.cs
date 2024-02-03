using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_4
{
    internal class PointerCounter
    {
        public PointerCounter()
        {
            isFirst = true;
            points = 0;
        }
        bool isFirst;
        public int points { get; private set; }

        public void AddPoint()
        {
            
            if (isFirst)
            {
                points++;
                isFirst = false;
            }
            else
            {
                points *= 2;
            }
        }


    }
}
