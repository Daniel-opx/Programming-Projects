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

          CoordinateObject FindFirstDigit(int i , int j)
        {

            while (array2D[i, j].isDigit() )
            {
                j--;
                if (!NonStaticIsCoordinateInRange(i, j)) break;
            }
            return new CoordinateObject(i, j+1);
        }


        int? FindUpperNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            var FirstOccurrenceOfNum = new CoordinateObject(-1,-1);
            if (!NonStaticIsCoordinateInRange(x-1,y))
                {
                Console.WriteLine("upper line is out of range");
                
                return null;

            }
            for(int upWidthOffset = -1; upWidthOffset < 1 ; upWidthOffset++)
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
                CoordinateObject loactionOfFirstDigit = FindFirstDigit(FirstOccurrenceOfNum.I, FirstOccurrenceOfNum.J);
                while (array2D[loactionOfFirstDigit.I, loactionOfFirstDigit.J].isDigit())
                {
                    ListForNum.Add(array2D[loactionOfFirstDigit.I, loactionOfFirstDigit.J] - '0');
                    loactionOfFirstDigit.J++;
                }

                return Program.ListToInt(ListForNum);
            }
           return null;


        }

        int? FindBottomNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            
            var FirstOccurrenceOfNum = new CoordinateObject(-1,-1);
            if (!NonStaticIsCoordinateInRange(x + 1, y))
            {
                Console.WriteLine("bottom line is out of range");

                return null;
            }
            for (int upWidthOffset = -1; upWidthOffset < 1; upWidthOffset++)
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
                CoordinateObject loactionOfFirstDigit = FindFirstDigit(FirstOccurrenceOfNum.I, FirstOccurrenceOfNum.J);
                while (array2D[loactionOfFirstDigit.I, loactionOfFirstDigit.J].isDigit())
                {
                    ListForNum.Add(array2D[loactionOfFirstDigit.I, loactionOfFirstDigit.J] - '0');
                    loactionOfFirstDigit.J++;
                }

                return Program.ListToInt(ListForNum);
            }
            return null;


        }

        int? FindLeftNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            if (!NonStaticIsCoordinateInRange(x, y - 1))
                return null;

            var locationOfNum = new CoordinateObject(-1,-1);

            if (array2D[x,y-1].isDigit())
            {
                locationOfNum.I = x;
                locationOfNum.J = y - 1;
            }

            if(locationOfNum.I != -1)
            {
                var locationOfFirstDigit = FindFirstDigit(locationOfNum.I, locationOfNum.J);

                while (array2D[locationOfFirstDigit.I, locationOfFirstDigit.J].isDigit())
                {
                    ListForNum.Add(array2D[locationOfFirstDigit.I, locationOfFirstDigit.J] - '0');
                    locationOfFirstDigit.J++;
                    
                }
                return Program.ListToInt(ListForNum);
            }

            return null;


        }

         int? FindRightNumber(int x, int y)
        {
            var ListForNum = new List<int>();
            if (!NonStaticIsCoordinateInRange(x, y + 1))
                return null;

            var locationOfNum = new CoordinateObject(-1, -1);

            if (array2D[x, y + 1].isDigit())
            {
                locationOfNum.I = x;
                locationOfNum.J = y + 1;
            }

            if (locationOfNum.I != -1)
            {
                var locationOfFirstDigit = FindFirstDigit(locationOfNum.I, locationOfNum.J);

                while (array2D[locationOfFirstDigit.I, locationOfFirstDigit.J].isDigit())
                {
                    ListForNum.Add(array2D[locationOfFirstDigit.I, locationOfFirstDigit.J] - '0');
                    locationOfFirstDigit.J++;

                }
                return Program.ListToInt(ListForNum);
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


        //static public bool IsNumberAdjacentUp(int i,int j, char[,] array ) //to uncomment and todo
        //{

        //    return false;
        //}
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
