using System.Linq;
using static puzzle_6.RegexPatterns;
namespace puzzle_6

{
    internal class Program
    {
        static void Main(string[] args)
        {
            string realInput = "C:\\Programming Projects\\advent of code 2023\\puzzle 6\\puzzle 6\\RealInpt.txt";
            string smallInput = "C:\\Programming Projects\\advent of code 2023\\puzzle 6\\puzzle 6\\smallInput.txt";

            string input = smallInput;

            var raceTimeList = new List<int>();
            var recordDistanceList = new List<int>();
            catchNumbers.Matches(File.ReadLines(input).First()).Aggregate(raceTimeList, (acc, next) =>
            {
                acc.Add(int.Parse(next.Value));
                return acc;
            });

            catchNumbers.Matches(File.ReadLines(input).Skip(1).First()).Aggregate(recordDistanceList, (acc, next) =>
            {
                acc.Add(int.Parse(next.Value));
                return acc;
            });

            if(raceTimeList.Count != recordDistanceList.Count) // parsing proccess checker- the count of both list must be equal, foreach race time there is record distance
            {
                throw new Exception("Parsing failed, check input file");
            }
            

            var raceList = new List<Race>();
            for(int i = 0;i < raceTimeList.Count; i++)
            {
                raceList.Add(new Race(raceTimeList[i], recordDistanceList[i]));
            }




        }











       
    }
}
