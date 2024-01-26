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
        static int ListToInt(List<int> list)
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


            //char[,] charMatrix = new char[File.ReadAllLines(path).Length, File.ReadAllLines(path)[0].Length];

            //first way to parse the input- for loop
            //using (var reader = new StreamReader(path))
            //{
            //    int boo, i = 0, j = 0;

            //    while ((boo = reader.Read()) != -1)
            //    {
            //        Console.WriteLine($"this is the {i} {j} iteration");
            //        if (j == numOfCharsInLine)
            //        {
            //            j = 0; i++;
            //        }
            //        if (i == numOfLines)
            //        { break; }

            //        charMatrix[i, j] = (char)boo;
            //        j++;
            //    }
            //}


            int numOfLines = File.ReadAllLines(path).Length; //number of strings in the input file
            int numOfCharsInLine = File.ReadAllLines(path)[0].Length; // number of chars in each line /string- 140 cahrs in each string in input
            

            var charMatrixLinq = new char[numOfLines, numOfCharsInLine+2]; // the +2 is for the \r and \n in the end of each line in the input, beacuse it reads all the byutes from text file it reads also uneccessey thing line new line -\n and cartridge - \r
            
            var arrayHandler = new ArrayHandler2D(charMatrixLinq);
            //second way- to use agrregate , the seed overload. the seed here is 2d char array so each 
            //iteration the aggregate use the array as the accumelator -the acc, and assign the current with the add function that i made
            File.ReadAllBytes(path).Aggregate(charMatrixLinq, (acc, current) =>
            {
                
                    arrayHandler.Add((char)current);
                

                return acc;
            });

            charMatrixLinq = arrayHandler.array2D;
            var ListOfIntForAggregate = new List<int>();

            var currentNumberList = new List<int>();
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < charMatrixLinq.GetLength(1); j++)
                {
                    //https://stackoverflow.com/questions/239103/convert-char-to-int-in-c-sharp - char to int conversion stackoverflow
                    char currentChar = charMatrixLinq[i, j];
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
                            if(isSignAround(charMatrixLinq,i,j-lengthOfNumber,lengthOfNumber))
                            {
                                ListOfIntForAggregate.Add(number);
                            }
                        }
                    }
                }
            }
            int sum = ListOfIntForAggregate.Aggregate((acc, current) => acc + current);
            int sumWithZero = ListOfIntForAggregate.Aggregate(0, (acc, current) => acc + current);
            Console.WriteLine($"the sum without the seed is : {sum}\nthe sum with the seed is {sumWithZero}");





        }



    }
}
