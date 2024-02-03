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
            var cardsList = new List<Card>();
            foreach (var value in ParsedInput)
            {
                var pointerCounter = new PointerCounter();
                cardsList.Add(new Card(value.Key));
                foreach (var item in value.Value[1])
                {

                    if (value.Value[0].Contains(item))
                    {
                        pointerCounter.AddPoint();
                        cardsList[value.Key-1].Matches++;
                        
                    }
                }
                sumOfAllPoints += pointerCounter.points;
            }
            Console.WriteLine("the sum is "+ sumOfAllPoints );

            foreach (var card in cardsList)
            {
               int position = card.Id ;
               for(int i = 0;i < card.Matches;i++)
               {
                    if(position < cardsList.Count)
                    cardsList[position++].AddCopies(card.Copies);
               }
            }
           var sum= cardsList.Aggregate(0, (acc, curr) => acc + curr.Copies);
            Console.WriteLine("the sum of all copies is "+ sum);






        }
    }
}
