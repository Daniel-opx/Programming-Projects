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
        /// <summary>
        /// checks if the coordinates is in the range of the matrix
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool NonStaticIsCoordinateInRange(int i, int j)
        {
            if (j > array2D.GetLength(1) - 1 || i > array2D.GetLength(0) - 1 || i < 0 || j < 0)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        ///         checks if the coordinates is in the range of the matrix

        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        static public bool IsCorrdinatesWithinBounds(int i, int j, char[,] array)
        {
            if (j > array.GetLength(1) - 1 || i > array.GetLength(0) - 1 || i < 0 || j < 0)
            {
                return false;
            }
            return true;
        }



        /*                                                                                 helper functions- travel in the giver direction and extract a number if possible
         * ==============================================================================================================================================================================================================================
         */

        

        public List<int> CalculateGearRatio(int i , int j)
        {
            var intlist = new List<int>();
            for (int lengthOfsset = -1; lengthOfsset <= 1; lengthOfsset++)
            {
                for (int widthOffset = -1; widthOffset <= 1; widthOffset++)
                {
                    int iWithLengthOffset = i + lengthOfsset;
                    int jWithWidthOffset = j + widthOffset;

                    if (!NonStaticIsCoordinateInRange(iWithLengthOffset, jWithWidthOffset))
                        continue;
                    if(lengthOfsset == -1 && widthOffset == 0)
                    {
                        if (array2D[iWithLengthOffset,jWithWidthOffset].isDigit())
                        {
                            //compute number
                            int number = ParseNumberFromSeparateDigits(new Coordinate(iWithLengthOffset, jWithWidthOffset), new List<int>());
                            intlist.OverWriteLastElementOfList(number);
                            lengthOfsset = 0; widthOffset = -2;
                            continue;
                        }
                    }

                    if( lengthOfsset == 1 && widthOffset == 0)
                    {
                        if (array2D[iWithLengthOffset,jWithWidthOffset].isDigit())
                        {
                            int number = ParseNumberFromSeparateDigits(new Coordinate(iWithLengthOffset, jWithWidthOffset), new List<int>());
                            intlist.OverWriteLastElementOfList(number);
                            break;
                        }
                    }




                    if (array2D[iWithLengthOffset,jWithWidthOffset].isDigit())
                    {
                        int number = ParseNumberFromSeparateDigits(new Coordinate(iWithLengthOffset, jWithWidthOffset), new List<int>());
                        intlist.Add(number);
                    }

                }
            }
            return intlist;
        }





        




        /// <summary>
        /// part 1 solution- checks if there is a any sign around the coordinate - the char - in question
        /// </summary>
        /// <param name="array2D"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="numLength"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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
