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

            string input = realInput;

            var raceTimeList = new List<int>();
            var recordDistanceList = new List<int>();
            catchNumbers.Matches(File.ReadLines(input).First()) // gets first line of input
                .Aggregate(raceTimeList, (acc, next) =>
            {
                acc.Add(int.Parse(next.Value));
                return acc;
            });

            catchNumbers.Matches(File.ReadLines(input).Skip(1).First()) // gets second line of input
                .Aggregate(recordDistanceList, (acc, next) =>
            {
                acc.Add(int.Parse(next.Value));
                return acc;
            });

            if (raceTimeList.Count != recordDistanceList.Count) // parsing proccess checker- the count of both list must be equal, foreach race time there is record distance
            {
                throw new Exception("Parsing failed, check input file");
            }

            var raceList = new List<Race>();
            for (int i = 0; i < raceTimeList.Count; i++)
            {
                raceList.Add(new Race(raceTimeList[i], recordDistanceList[i]));
            }

            var winningOptions = new List<int>();
            foreach (var race in raceList)
            {
                var raceTime = race.RaceTime;
                var recordDistance = race.DistaneRecord;
                var raceParobola = race.parabola;


                var inequalitySolution = (Parabola.SolveQuadraticInequality(raceParobola.coefficientA, raceParobola.coefficientB, recordDistance));
                var raceWinningOptions = GeneralStaticMethods.CountNumOfIntInRange(inequalitySolution[0], inequalitySolution[1]);
                winningOptions.Add(raceWinningOptions);
            }
            Console.WriteLine("the answer is {0}", winningOptions.Aggregate((acc, next) => acc * next));

            //===============================================part 2===================================================================================//
            raceList.Clear();
            raceTimeList.Clear();
            recordDistanceList.Clear();
            winningOptions.Clear();

            var compoundRaceTime = catchEverySingleNum.Matches(File.ReadLines(input).First()) // gets first line of input
                 .Select(s => int.Parse(s.Value)).ToList();


            var compoundDistnaceRecord = catchEverySingleNum.Matches(File.ReadLines(input).Skip(1).First()) // gets second line of input
                .Select(s => int.Parse(s.Value)).ToList();

            raceList.Add(new Race(ConvertIntListToNum(compoundRaceTime), ConvertIntListToNum(compoundDistnaceRecord)));
            Console.WriteLine();

            foreach (var race in raceList)
            {
                var raceTime = race.RaceTime;
                var recordDistance = race.DistaneRecord;
                var raceParobola = race.parabola;


                var inequalitySolution = (Parabola.SolveQuadraticInequality(raceParobola.coefficientA, raceParobola.coefficientB, recordDistance));
                var raceWinningOptions = GeneralStaticMethods.CountNumOfIntInRange(inequalitySolution[0], inequalitySolution[1]);
                winningOptions.Add(raceWinningOptions);
            }
            Console.WriteLine("the answer is {0}", winningOptions.Aggregate((acc, next) => acc * next));






            static double ConvertIntListToNum(List<int> list)
            {
                int ListCount = list.Count;
                double num = 0;
                int baseOftenPower = list.Count - 1;

                var foo = list.Aggregate(num, (acc, next) => acc + (next * Math.Pow(10, baseOftenPower--)));
                return foo;
            }
            static Dictionary<long, long> keyValuePairs(long raceTimes)
            {
                var dict = new Dictionary<long, long>();

                for (int i = 0; i < raceTimes + 1; i++)
                {
                    long loadingTime = i;
                    long distanceReached = (raceTimes - loadingTime) * (loadingTime);
                    dict.Add(loadingTime, distanceReached);
                }
                foreach (var kvp in dict)
                {
                    Console.WriteLine($"{kvp.Key}:{kvp.Value}");
                }
                return dict;
            }


        }
    }
}
