using static Puzzle_5.RegexPatterns;
using static Puzzle_5.FileReadMethods;
namespace Puzzle_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Programming Projects\\advent of code 2023\\Puzzle 5\\Puzzle 5\\input.txt";

            var seedList =extractNumbers.Matches(ReadFirstLine(inputPath)) //parsing of the seeds to list of seeds , each seed represented by a number
                .Select(match=>long.Parse(match.Value))
                .ToList();


            Console.WriteLine();

        }
    }
}
