using System.Text.RegularExpressions;

namespace puzzle_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Programming Projects\\advent of code 2023\\puzzle 3\\puzzle 3\\input.txt";
            char[,] charMatrix = new char[File.ReadAllLines(path).Length, File.ReadAllLines(path)[0].Length];
            int numOfLines = File.ReadAllLines(path).Length;
            int numOfCharsInLine = File.ReadAllLines(path)[0].Length;
            using (var reader = new StreamReader(path))
            {
                int boo, i = 0, j = 0;

                while ((boo = reader.Read()) != -1)
                {
                    Console.WriteLine($"this is the {i} {j} iteration");
                    if (j == numOfCharsInLine)
                    {
                        j = 0; i++;
                    }
                    if (i == numOfLines)
                    { break; }

                    charMatrix[i, j] = (char)boo;
                    j++;
                }
            }
            var secondArray = new char[numOfLines, numOfCharsInLine];
            var arrayHandler = new ArrayHandler2D(secondArray);

            File.ReadAllBytes(path).Aggregate(secondArray, (acc, current) =>
            {
                arrayHandler.Add((char)current);
                return acc;
            });

            secondArray = arrayHandler.array2D;

            var lengthat0 = secondArray.GetLength(0);
            var lengthat1 = secondArray.GetLength(1);

            
            foreach (var item in secondArray)
            {
               
                Console.Write(item);
              
            }
            Console.WriteLine("now printing the char matrix with loop char[][]=============================================");
            for (int i = 0; i < charMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < charMatrix.GetLength(1); j++)
                {
                    Console.Write(charMatrix[i, j]);
                }

            }



            Console.WriteLine("sd");







        }



    }
}
