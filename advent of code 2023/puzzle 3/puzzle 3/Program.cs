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

            Console.WriteLine("sd");







        }



    }
}
