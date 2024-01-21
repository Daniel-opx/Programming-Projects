
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Transactions;
using static puzzle_2.RegexStuff;


namespace puzzle_2
{ 

    internal class Program
    {
        static void Main(string[] args)
        {

            var intList = new List<int>();  
            
            var path = @"C:\Programming Projects\advent of code 2023\puzzle 2\puzzle 2\inputFile.txt";
            

            Console.WriteLine("=======================newnewnewnew=============================\nnew stuff");

            var stringsCollection = File.ReadLines(path);
            int counter = 0;
            foreach (var sentence in stringsCollection)
            {
                int gameNumber = int.Parse(catchGameNum.Match(sentence).Value);

                Console.WriteLine("this is the {0} loop\ngame number {1}", counter + 1, gameNumber);
                counter++;
                var matches = seperateBySemicolon.Matches(sentence);
                if (BallsContainer.IsPossible(matches) == 0)
                {
                    Console.WriteLine($"{gameNumber} added to list============================================\n\n");
                    intList.Add(gameNumber);
                }
            }
            int sum = intList.Aggregate((a, b) => a + b);
            Console.WriteLine("the sum of all  possible is {0}", sum);

            Console.WriteLine("now with linq===============================================================================================\n\n\n");

            var stringsCollectionWithLinq = File.ReadLines(path);
            //var dictioanry =stringsCollectionWithLinq.Select(sentence => new // dictionary make adictionary of gameid:array of strings
            //{
            //    gameID = int.Parse(catchGameNum.Match(sentence).Value),
            //    trunArr = seperateBySemicolon.Matches(sentence).
            //    Select(ArrCurrent => ArrCurrent.Value).ToArray(),
            //}).ToDictionary(newObject => newObject.gameID, newObject => newObject.trunArr);

            //var finalDictionary = dictioanry.Select(curr => new //turn the former dictianry to gameID:array of cubes object
            //{
            //    key = curr.Key,
            //    stringlLstLength = curr.Value.Length,
            //    cube = curr.Value.Select(sentence => CubeSet.CreateCube(sentence)).ToArray(),

            //}).ToDictionary(thing => thing.key, thing => thing.cube);

            var finalDictonary2 = stringsCollectionWithLinq.Select(sent => new //does all the proccess in one linq
            {
                GameId = int.Parse(catchGameNum.Match(sent).Value),
                cubeArr = seperateBySemicolon.Matches(sent).
                Select(ArrCurrent => ArrCurrent.Value).ToArray().
                Select(currString => CubeSet.CreateCube(currString)).ToArray(),
            }).ToDictionary(newObject => newObject.GameId, newObject => newObject.cubeArr);

            //each value of dictionary-arrayofcubes=>each cube -set of callors=>all of each collor=>minimum

            var redMinimums = finalDictonary2.Values.Select(cubeArr => cubeArr.Select(cube => cube.redCubes))
                .Select(s=>s.Max()).ToList();

            var blueMinimums = finalDictonary2.Values.Select(cubeArr => cubeArr.Select(cube => cube.blueCubes))
                .Select(s => s.Max()).ToList();

            var greenMinimums = finalDictonary2.Values.Select(cubeArr => cubeArr.Select(cube => cube.greenCubes))
                .Select(s => s.Max()).ToList();

            var powerList = new List<int>();

            for(int i = 0;i < redMinimums.Count;i++)
            {
                powerList.Add(redMinimums[i] * greenMinimums[i] * blueMinimums[i]);
            }
            var sumOfPowers = powerList.Aggregate((curr,next)=>curr+next);
            Console.WriteLine("the sum of aggreagte is {0}",sumOfPowers);

            Console.WriteLine();








        }
    }
}
