using System.Reflection.Metadata;
using static Puzzle_4.RegexPatterns;

namespace Puzzle_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Programming Projects\\advent of code 2023\\Puzzle 4\\Puzzle 4\\input.txt";
            string smallInput = @"C:\Programming Projects\advent of code 2023\Puzzle 4\Puzzle 4\smallInput.txt";

            
            var ParsedInput = File.ReadLines(inputPath).Select(str => new
            {
                cardNum = int.Parse(cardNumberRegex.Match(str).Value),
                winningNumbers = ExtractNumbers.Matches(winningNumbersRegex.Match(str).Value).Select(match => int.Parse(match.Value)).ToList(),
                myNums = ExtractNumbers.Matches(myNumbersRegex.Match(str).Value).Select(match => int.Parse(match.Value)).ToList()

            }).ToDictionary(a => a.cardNum, a => new List<List<int>>() { a.winningNumbers, a.myNums });

            int sumOfAllPoints = 0; //part 1 variable solution

            int totalNumOfScratchCards = ParsedInput.Count; //initial the variable with the original count of cards 
            
            foreach (var value in ParsedInput.Values)
            {
                var pointerCounter = new PointerCounter();
                
                foreach (var item in value[1])
                {
                    if (value[0].Contains(item))
                    {
                        pointerCounter.AddPoint();
                        
                    }
                }
                sumOfAllPoints += pointerCounter.points;


            }
            Console.WriteLine("the sum is "+ sumOfAllPoints );






        }
    }
}
