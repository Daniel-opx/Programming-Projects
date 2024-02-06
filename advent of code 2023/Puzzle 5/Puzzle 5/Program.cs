using static Puzzle_5.RegexPatterns;
using static Puzzle_5.FileReadMethods;
using System.Text.RegularExpressions;
namespace Puzzle_5
{
    internal class Program
    {
        enum maps
        {
            seed = 0,
            soil,
            fertilizer,
            water,
            light,
            temperature,
            humidity,
            location
        }
        static void Main(string[] args)
        {
            string inputPath = "C:\\Programming Projects\\advent of code 2023\\Puzzle 5\\Puzzle 5\\input.txt";

            var seedList =extractNumbers.Matches(ReadFirstLine(inputPath)) //parsing of the seeds to list of seeds , each seed represented by a number
                .Select(match=>long.Parse(match.Value))
                .ToList();

            List<(int seed, int soil, int fertilizer,int water,int light,int temperature,int humidity,int location)> seedToLoactionMapList =
                new List<(int seed, int soil, int fertilizer, int water, int light, int temperature, int humidity, int location)>(); // initialized list of tuple

            ( int , int, int, int, int, int, int, int) t = (1, 2, 3, 4, 5, 6, 7, 8);
            



            var matchCollection = ExtractBlock.Matches(File.ReadAllText(inputPath));

            for(int i = 0; i < matchCollection.Count - 1; i++)
            {
                var match = matchCollection[i].Value; //make a variable for the text block
                var lines = match.Split("\r\n").Where(str => !str.Equals("")).ToList(); //converts the 
                



            }

            //foreach (Match match in matchCollection)
            //{
            //    Console.WriteLine(match.Value+ "\n" +"====================================================");
                 
            //}


            Console.WriteLine();

        }
    }
}
