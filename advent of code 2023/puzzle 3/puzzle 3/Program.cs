using System.Text.RegularExpressions;

namespace puzzle_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Programming Projects\\advent of code 2023\\puzzle 3\\puzzle 3\\input.txt";
            var inputCollection = File.ReadLines(path).ToList();

            var regex = new Regex("(\\d+)|[$@/*]");
            var collection = inputCollection.Where(s => regex.IsMatch(s)).Select(strng => regex.Matches(strng)).
                Select(s => s.Select(match => new { 
                    index = match.Index,
                    value = match.Value,
                    isNumber = int.TryParse(match.Value,out int a)
                })
                ).ToList();




        }
    }
}
