using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static puzzle_3.RegexStrings;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        /// <summary>
        /// add,func, it uses the i  and j properties of this class and save as a type of iterator in a linq line in the main function.
        /// saves location between calls and know when tou go down a line int the insertion process
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        /// the FindFirstDigit takes 2 arguments- i  and j ,i.e the coordinate in the matrix, then it uses while loop to travel back until it encounters the last digit in the digit sequence
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        Coordinate FindFirstDigit(int i , int j)
        {

            while (array2D[i, j].isDigit() )
            {
                j--;
                if (!NonStaticIsCoordinateInRange(i, j)) break;
            }
            return new Coordinate(i, j+1);
        }

        /// <summary>
        /// function in the process of part 2 solution- it takes coordinate object and int list, parse the seperate digits to one int with the help of the list and the ListToInt func and return the number
        /// </summary>
        /// <param name="locationOfNum"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        int ParseNumberFromSeparateDigits(Coordinate locationOfNum, List<int> list)
        {
            var locationOfFirstDigit = FindFirstDigit(locationOfNum.I, locationOfNum.J);
            while (array2D[locationOfFirstDigit.I, locationOfFirstDigit.J].isDigit())
            {
                list.Add(array2D[locationOfFirstDigit.I, locationOfFirstDigit.J] - '0');
                locationOfFirstDigit.J++;

            }
            return Program.ListToInt(list);

        }


        /*                                                                                 helper functions- travel in the giver direction and extract a number if possible
         * ==============================================================================================================================================================================================================================
         */

        int? FindUpperNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            var FirstOccurrenceOfNum = new Coordinate(-1,-1);
            if (!NonStaticIsCoordinateInRange(x-1,y))
                {
                Console.WriteLine("upper line is out of range");
                
                return null;

            }
            for(int upWidthOffset = -1; upWidthOffset <= 1 ; upWidthOffset++)
            {
                if(!NonStaticIsCoordinateInRange(x - 1, y+upWidthOffset))
                {
                    continue;
                }
                if (array2D[x + 1, y + upWidthOffset].isDigit())
                {
                    FirstOccurrenceOfNum.I = x - 1;
                    FirstOccurrenceOfNum.J = y + upWidthOffset;
                    break;
                }
            }
            if(FirstOccurrenceOfNum.I != -1)
            {
                int upperNumber = ParseNumberFromSeparateDigits(FirstOccurrenceOfNum, ListForNum);
                return upperNumber;
            }
           return null;


        }

        int? FindBottomNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            
            var FirstOccurrenceOfNum = new Coordinate(-1,-1);
            if (!NonStaticIsCoordinateInRange(x + 1, y))
            {
                Console.WriteLine("bottom line is out of range");

                return null;
            }
            for (int upWidthOffset = -1; upWidthOffset <= 1; upWidthOffset++)
            {
                if (!NonStaticIsCoordinateInRange(x + 1, y + upWidthOffset))
                {
                    continue;
                }
                if (array2D[x + 1, y + upWidthOffset].isDigit())
                {
                    FirstOccurrenceOfNum.I = x + 1;
                    FirstOccurrenceOfNum.J = y + upWidthOffset;
                    break;
                }
            }

            if(FirstOccurrenceOfNum.I != -1)
            {
                int bottomNumber = ParseNumberFromSeparateDigits(FirstOccurrenceOfNum, ListForNum);
                return bottomNumber;
            }
            return null;


        }


      


        int? FindLeftNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            if (!NonStaticIsCoordinateInRange(x, y - 1))
                return null;

            var locationOfNum = new Coordinate(-1,-1);

            if (array2D[x,y-1].isDigit())
            {
                locationOfNum.I = x;
                locationOfNum.J = y - 1;
            }

            if(locationOfNum.I != -1)
            {
                int leftNumber = ParseNumberFromSeparateDigits(locationOfNum, ListForNum);
                return leftNumber;
            }

            return null;


        }

         int? FindRightNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            if (!NonStaticIsCoordinateInRange(x, y + 1))
                return null;

            var locationOfNum = new Coordinate(-1, -1);

            if (array2D[x, y + 1].isDigit())
            {
                locationOfNum.I = x;
                locationOfNum.J = y + 1;
            }

            if (locationOfNum.I != -1)
            {
               int rightNumber =ParseNumberFromSeparateDigits(locationOfNum,ListForNum);
                return rightNumber;
            }

            return null;


        }

        public List<int?> CalculateGearRatioUpDown(int i ,int j)
        {
           var returnList = new List<int?>();
            returnList.Add(FindBottomNumber(i, j));
            returnList.Add(FindUpperNumber(i, j));
            return returnList;
        }
        public List<int?> CalculateGearRatioLeftRight(int i, int j)
        {
            var returnList = new List<int?>();
            returnList.Add(FindLeftNumber(i, j));
            returnList.Add(FindRightNumber(i, j));
            return returnList;
        }




        public bool NonStaticIsCoordinateInRange(int i, int j)
        {
            if (j > array2D.GetLength(1) - 1 || i > array2D.GetLength(0) - 1 || i < 0 || j < 0)
            {
                return false;
            }
            return true;
            
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
