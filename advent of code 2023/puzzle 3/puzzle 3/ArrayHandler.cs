using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_3
{
    internal class ArrayHandler2D
    {
        public ArrayHandler2D(char[,] array)
        {
            dimensions = array.Rank;
            array2D = array;
            I = 0;
            J = 0;
            FirstDimensionLength = array.GetLength(0);
            SecondDimensionLength = array.GetLength(1);


        }
        private int dimensions { get; set; }
        public char[,] array2D {  get; private set; }
        private int FirstDimensionLength { get; set; }
        private int SecondDimensionLength { get; set; }

        private int I {  get; set; }
        private int J { get; set; }


        public void Add(char value)
        {
            if (J == SecondDimensionLength)
            {
                I++;
                J = 0;
            }
            if(I == FirstDimensionLength)
            {
                return;
            }
            array2D[I, J] = value;
            J++;

        }

    }
}
