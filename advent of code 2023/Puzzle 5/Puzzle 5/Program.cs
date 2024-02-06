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

            List<(long? seed, long? soil, long? fertilizer, long? water, long? light, long? temperature, long? humidity, long? location)> seedToLoactionMapList =
                                new List<(long? seed, long? soil, long? fertilizer, long? water, long? light, long? temperature, long? humidity, long? location)>();
            seedToLoactionMapList.Add((5,null,null,null,null,null,null,null));
            Console.WriteLine(seedToLoactionMapList[0].seed);


            var  seed = new HashSet<long>();
            var soil = new HashSet<long>(); 
            var fertilizer = new HashSet<long>(); 
            var water = new HashSet<long>();
            var light = new HashSet<long>();
            var temperature = new HashSet<long>(); 
            var humidity = new HashSet<long>();
            var location = new HashSet<long>();

            var matchCollection = ExtractBlock.Matches(File.ReadAllText(inputPath));

            for(int i = 0; i < matchCollection.Count; i++)
            {
                var textBlock = matchCollection[i].Value; //make a variable for the text block
                var lines = textBlock.Split("\r\n").Where(str => !str.Equals("") && !str.EndsWith(":")).ToList(); //converts the text into list of lines, each line contains (int destanation,int source,int skips)
                long[]? numbers;
                foreach(var line in lines)
                {

                     numbers = extractNumbers.Matches(line).Select(match => long.Parse(match.Value)).ToArray();
                    long? dst = numbers[0]; long? source = numbers[1]; long? skips = numbers[2];


                    
                    

                }



            }

            //foreach (Match match in matchCollection)
            //{
            //    Console.WriteLine(match.Value+ "\n" +"====================================================");
                 
            //}


            Console.WriteLine();

        }
    }
}
