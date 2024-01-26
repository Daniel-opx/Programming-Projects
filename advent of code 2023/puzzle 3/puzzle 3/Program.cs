using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Math;
using static puzzle_3.RegexStrings;
using static puzzle_3.ExtentionMethods;
using static puzzle_3.ArrayHandler2D;

namespace puzzle_3
{
    internal class Program
    {
       public static int ListToInt(List<int> list)
        {
            int powerOften = list.Count - 1;
            var num = list.Aggregate(0, (acc, current) =>
            {
                acc = current * (int)Math.Pow(10, powerOften--) + acc;
                return acc;
            });
            list.RemoveRange(0, list.Count);
            return num;
        }

        static void Main(string[] args)
        {
            string path = "C:\\Programming Projects\\advent of code 2023\\puzzle 3\\puzzle 3\\input.txt";

            int numOfLines = File.ReadAllLines(path).Length; //number of strings in the input file
            int numOfCharsInLine = File.ReadAllLines(path)[0].Length; // number of chars in each line /string- 140 cahrs in each string in input
            

            var charMatrix = new char[numOfLines, numOfCharsInLine+2]; // the +2 is for the \r and \n in the end of each line in the input, beacuse it reads all the byutes from text file it reads also uneccessey thing line new line -\n and cartridge - \r           
            var charMatrixArrayHanlder = new ArrayHandler2D(charMatrix);


            //second way- to use agrregate , the seed overload. the seed here is 2d char array so each 
            //iteration the aggregate use the array as the accumelator -the acc, and assign the current with the add function that i made
            File.ReadAllBytes(path).Aggregate(charMatrix, (acc, current) =>
            {
                
                    charMatrixArrayHanlder.Add((char)current);
                

                return acc;
            });

            charMatrix = charMatrixArrayHanlder.array2D;
            var ListOfIntForSumAgrregate = new List<int>(); 

            var currentNumberList = new List<int>(); //list that will go through the listtoint function and convert the list to one int.

            
            
            for (int i = 0; i < numOfLines; i++) // logic for first part of puzzle- sum of all numbers that adjacent to signs  
            {
                for (int j = 0; j < charMatrix.GetLength(1); j++)
                {
                    //https://stackoverflow.com/questions/239103/convert-char-to-int-in-c-sharp - char to int conversion stackoverflow
                    char currentChar = charMatrix[i, j];
                    if (currentChar.isDigit())
                    {
                        Console.Write(currentChar);
                        currentNumberList.Add(currentChar - '0');
                    }
                    else
                    {
                        Console.Write(currentChar);
                        if (currentNumberList.Count > 0)
                        {

                            int number = ListToInt(currentNumberList);
                            int lengthOfNumber = ExtentionMethods.IntLength(number);
                            if(isSignAround(charMatrix,i,j-lengthOfNumber,lengthOfNumber))
                            {
                                ListOfIntForSumAgrregate.Add(number);
                            }
                        }
                    }
                }
            }
            int sum = ListOfIntForSumAgrregate.Aggregate((acc, current) => acc + current);
           
            Console.WriteLine($"the sum without the seed is : {sum}");


            var listOfGearRatio = new List<int?>();

            for (int i = 0; i < numOfLines; i++)
            {
                for (int j = 0; j < charMatrix.GetLength(1); j++)
                {
                    char currentChar = charMatrix[i, j];
                    if (currentChar.Equals('*'))
                    {
                        var UpDownIntList = charMatrixArrayHanlder.CalculateGearRatioUpDown(i, j);
                        var RightLeftIntList = charMatrixArrayHanlder.CalculateGearRatioLeftRight(i, j);
                        if(!UpDownIntList.Contains(null))
                        {
                            //listOfGearRatio.Add( UpDownIntList.Aggregate( (acc, current) => acc + current));
                            listOfGearRatio.Add(UpDownIntList[0]+ UpDownIntList[1]);
                        }
                        else if(!RightLeftIntList.Contains(null))
                            {
                            listOfGearRatio.Add(RightLeftIntList.Aggregate((acc,curr)=>acc+curr));
                        }


                    }
                }
            }

            Console.WriteLine("");


        }



    }
}
