using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static puzzle_3.RegexStrings;

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

         static public bool IsCorrdinatesWithinBounds(int i,int j, char[,] array )
        {
            if(j > array.GetLength(1)-1 || i > array.GetLength(0)-1 || i < 0 || j < 0)
            {
                return false;
            }
            return true;
        }
        static public bool isSignAround(char[,] array2D,int i,int j,int numLength)
        {
            if(numLength < 1)
            {
                throw new ArgumentNullException(nameof(numLength),"the number in parmeter is below 1");
            }
            for (int lengthOffset = -1; lengthOffset < 2;lengthOffset++)
            {
                for(int widthOffset = -1; widthOffset < numLength+1 ;widthOffset++)
                {
                   int iWithLengthOffset = i + lengthOffset;
                    int jWithWidthOffset = j + widthOffset;

                    if (!IsCorrdinatesWithinBounds(iWithLengthOffset, jWithWidthOffset, array2D))
                        {
                        continue;

                    }
                    
                    if (RegexStrings.isSignRegex.IsMatch(Convert.ToString (array2D[iWithLengthOffset, jWithWidthOffset])))
                        {
                        return true;
                    }
                    

                }
                
            }
            return false;
        }

        

    }
}
