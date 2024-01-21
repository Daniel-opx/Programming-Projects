using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static puzzle_2.RegexStuff;

namespace puzzle_2
{
    internal class BallsContainer
    {

       private enum Color
        {
            green,
            red,
            blue
        }
     private  static Color[] colors = new Color[]
        {
            Color.red, Color.green, Color.blue
        };



        const int maxRed = 12, maxBlue = 14, maxGreen = 13;
        static int  redBalls, greenBalls, blueBalls;
        
        public static int IsPossible(MatchCollection matchCollection) // the match will be one turn of pullouts
        {
            int impossibleGames = 0;


            if (matchCollection.Count == 0) {
                Console.WriteLine("there is 0 matches!");
                return -1;
            }


            foreach (Match match in matchCollection)
            {

                redBalls = 0; greenBalls = 0; blueBalls = 0;
                IncreaseAmountOfBalls(match);
                Console.WriteLine(
                $"red balls:{redBalls}, blue balls:{blueBalls}, green balls: {greenBalls}");
                if (redBalls > maxRed || blueBalls > maxBlue || greenBalls > maxGreen)
                {
                    impossibleGames++;
                }
            }

            
            

            return impossibleGames;
        }

        
       

        private static void IncreaseAmountOfBalls(Match match)
        {
            var seperateBallCount = pullBallsFromSack.Matches(match.Value);
            foreach(Match element in  seperateBallCount)
            {
                int ballsIncrement = int.Parse(catchNumOfBalls.Match(element.Value).Value);
                if (element.Value.Contains("blue")) {
                    blueBalls = blueBalls + ballsIncrement;
                }
                else if(element.Value.Contains("red"))
                    {
                    redBalls = redBalls + ballsIncrement;
                }
                else if(element.Value.Contains("green"))
                {
                    greenBalls = greenBalls + ballsIncrement;
                }
                
            }
        }

        

    }
}
