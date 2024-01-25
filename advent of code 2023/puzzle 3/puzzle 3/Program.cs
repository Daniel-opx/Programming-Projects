using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace puzzle_3
{
    internal class Program
    {

        
        static void Main(string[] args)
        {
            string path = "C:\\Programming Projects\\advent of code 2023\\puzzle 3\\puzzle 3\\input.txt";


            char[,] charMatrix = new char[File.ReadAllLines(path).Length, File.ReadAllLines(path)[0].Length];

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


            int numOfLines = File.ReadAllLines(path).Length;
            int numOfCharsInLine = File.ReadAllLines(path)[0].Length;
            

            var charMatrixLinq = new char[numOfLines, numOfCharsInLine];
            var arrayHandler = new ArrayHandler2D(charMatrixLinq);
            //second way- to use agrregate , the seed overload. the seed here is 2d char array so each 
            //iteration the aggregate use the array as the accumelator -the acc, and assign the current with the add function that i made
            File.ReadAllBytes(path).Aggregate(charMatrixLinq, (acc, current) =>
            {
                arrayHandler.Add((char)current);
                return acc;
            });

            charMatrixLinq = arrayHandler.array2D;

            var currentNumber = new List<int>();
            for(int i = 0;i < 40;i++)
            {
                for(int j = 0;j < charMatrixLinq.GetLength(1);j++)
                {
                    //https://stackoverflow.com/questions/239103/convert-char-to-int-in-c-sharp - char to int conversion stackoverflow
                    char currentChar = charMatrixLinq[i,j];
                    if(currentChar.isDigit())
                    {
                        currentNumber.Add(currentChar - '0');
                    }
                    else
                    {
                        if(currentNumber.Count>0)
                        {
                            int powerOften = currentNumber.Count-1;
                            var num =currentNumber.Aggregate(0,(acc, current) =>
                            {
                                acc = current * (int)Math.Pow(10, powerOften--) + acc;
                                return acc;
                            });
                        }
                    }
                }
            }
        }



    }
}
