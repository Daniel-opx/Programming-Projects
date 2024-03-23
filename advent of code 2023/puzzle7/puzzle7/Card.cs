using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle7
{
    public enum Card
    {
        two = 2,
        three,
        four ,
        five ,
        six ,
        seven , 
        eight ,
        nine,
        T,
        J,
        Q,
        K,
        A


    }
    public enum Card2
    {
        J = 1,
        two = 2,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        T,
        Q,
        K,
        A


    }
    public enum HandValue
    {
        highcard ,
        onePair,
        twoPair,
        threeOfAKind,
        fullHouse,
        fourOfAKind,
        FiveOfAKind


    }
}
