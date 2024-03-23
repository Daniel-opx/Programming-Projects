using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace puzzle7
{
    internal class Hand 
    {
        public Hand() {
            CardSet = new Card[5];
        }

        public Hand(Card[] cardSet, int bid) :this()
        {
            this.Bid = bid;
            this.CardSet = cardSet;
        }

        public int Bid { get; set; }

        public Card[] CardSet { get; private set; }

        internal HandValue GetHandValue()
        {
            var cardCountByType = CardSet
                .GroupBy(card => card)
                .Select(group => group.Count())
                .OrderByDescending(s => s)
                .ToArray();

            int highestMatch = cardCountByType[0];
            switch (highestMatch)
            {
                case 5:
                    return HandValue.FiveOfAKind;
                case 4:
                    return HandValue.fourOfAKind;
                case 3:
                    return cardCountByType[1] == 2 ? HandValue.fullHouse : HandValue.threeOfAKind;
                case 2:
                    return cardCountByType[1] == 2 ? HandValue.twoPair :HandValue.onePair;
                case 1:
                    return HandValue.highcard;
                default:
                    throw new Exception("wtf");

            }
        }
        internal static Hand CreateHand(string inputLine)
        {
            var hand = new Hand();

            int bid = int.Parse(RgxPatterns.CatchNums.Match(inputLine).Value);
            hand.Bid = bid;

            var cards = RgxPatterns.CatchEverything.Matches(RgxPatterns.CatchHand.Match(inputLine).Value);
            int position = 0;
            for (int i = 0; i < 5; i++)
            {
                var currMatch = cards[i];
                hand.CardSet[position++] = Utils.EnumParser.Parse<Card>(currMatch.Value);
            }
            return hand;


        }

       
    }
}
