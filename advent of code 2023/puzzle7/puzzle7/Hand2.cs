using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace puzzle7
{
    internal class Hand2
    {
        public Hand2()
        {
            CardSet = new Card2[5];

        }

        public Hand2(Card2[] cardSet, int bid) : this()
        {
            this.Bid = bid;
            this.CardSet = cardSet;
        }

        public int Bid { get; private set; }

        public Card2[] CardSet { get; private set; }

        internal HandValue GetHandValue()
        {
            var cardCountByType = CardSet
                .GroupBy(card => card)
                .Select(group => group.Count())
                .OrderByDescending(s => s)
                .ToArray();

            var cardCountByTypeTuple = CardSet
                .GroupBy(card => card)
                .Select(group => (group.Key, group.Count())).OrderByDescending(s => s.Item2).ToArray();

            var maxtuple = cardCountByTypeTuple[0];




            int numOfJ = 0;
            foreach (Card2 card2 in CardSet)
            {
                if (card2 == Card2.J) numOfJ++;
            }

            int highestMatch = cardCountByType[0];
            switch (highestMatch)
            {
                case 5:
                    return HandValue.FiveOfAKind;
                case 4:
                    return numOfJ == 4 ? HandValue.FiveOfAKind : (HandValue)((int)HandValue.fourOfAKind + numOfJ);
                case 3:
                    if (cardCountByType[1] == 2)
                    {
                        return numOfJ == 0 ? HandValue.fullHouse : HandValue.FiveOfAKind;
                    }
                    else
                    {
                        if (numOfJ >= 1) return HandValue.fourOfAKind;
                        else return HandValue.threeOfAKind;
                    }
                case 2:
                    if (cardCountByType[1] == 2)
                    {
                        if (numOfJ == 2) return HandValue.fourOfAKind;
                        else if (numOfJ == 1) return HandValue.fullHouse;
                        else return (HandValue)((int)HandValue.twoPair + numOfJ);
                    }
                    else
                    {
                        return numOfJ == 1 ? HandValue.threeOfAKind : (HandValue)((int)HandValue.onePair + (numOfJ));
                    }
                case 1:
                    return (HandValue)((int)HandValue.highcard + numOfJ);
                default:
                    throw new Exception("wtf");

            }
        }
        internal static Hand2 CreateHand(string inputLine)
        {
            var hand = new Hand2();

            int bid = int.Parse(RgxPatterns.CatchNums.Match(inputLine).Value);
            hand.Bid = bid;

            var cards = RgxPatterns.CatchEverything.Matches(RgxPatterns.CatchHand.Match(inputLine).Value);
            int position = 0;
            for (int i = 0; i < 5; i++)
            {
                var currMatch = cards[i];
                hand.CardSet[position++] = Utils.EnumParser.Parse<Card2>(currMatch.Value);
            }
            return hand;


        }
    }
}
