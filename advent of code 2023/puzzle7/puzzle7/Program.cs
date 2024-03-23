using System.Text.RegularExpressions;
using static puzzle7.RgxPatterns;

namespace puzzle7

{
    internal class Program
    {

        static void Main(string[] args)
        {
            string Realinput = "C:\\Programming Projects\\advent of code 2023\\puzzle7\\puzzle7\\input.txt";
            string smallInput = "C:\\Programming Projects\\advent of code 2023\\puzzle7\\puzzle7\\smaiiInput.txt";
            string input = Realinput;

            Hand[] hands = File.ReadAllLines(input).Select(str => Hand.CreateHand(str)).
                Order(new HandsCmp())
                .ToArray();

            //foreach(Hand bla in hands)
            //{
            //    foreach (var item in bla.CardSet)
            //    {
            //        Console.Write(item + " " );

            //    }
            //    Console.Write(" The value of this hand is :{0}",bla.GetHandValue());
            //    Console.WriteLine("\n");
            //}
            var sum = hands.Select((hand, index) => (hand, index)).Aggregate(0, (acc, curr) => acc + (curr.hand.Bid * (curr.index+1)));

            Console.WriteLine("the solution for part 1 is:{0}\nnow solution for part 2======================================================",sum);

            
            Hand2[] hands2 = File.ReadAllLines(input).Select(str => Hand2.CreateHand(str)).
                Order(new HandsCmp2())
                .ToArray();


            foreach (Hand2 bla in hands2.Where(v => !v.CardSet.Contains(Card2.J)).ToList())
            {
                foreach (var item in bla.CardSet)
                {
                    Console.Write(item + " ");

                }
                Console.Write(" The value of this hand is :{0}", bla.GetHandValue());
                Console.WriteLine("\n");
            }

            //foreach (Hand2 bla in hands2)
            //{
            //    foreach (var item in bla.CardSet)
            //    {
            //        Console.Write(item + " ");

            //    }
            //    Console.Write(" The value of this hand is :{0}", bla.GetHandValue());
            //    Console.WriteLine("\n");
            //}
            var sum2 = hands2.Select((hand, index) => (hand, index)).Aggregate(0, (acc, curr) => acc + (curr.hand.Bid * (curr.index + 1)));


            Console.WriteLine($"the sum of part 2 is : {sum2}");
        }



    }

}

