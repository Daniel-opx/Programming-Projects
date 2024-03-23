using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle7
{
    internal class HandsCmp2 : IComparer<Hand2>
    {
        public int Compare(Hand2? x, Hand2? y)
        {
            int xValue = (int)x.GetHandValue(); int yValue = (int)y.GetHandValue();
            if (xValue > yValue) return 1;
            if (xValue < yValue) return -1;

            for (int i = 0; i < 5; i++)
            {
                var currXCard = x.CardSet[i]; var currYCard = y.CardSet[i];
                if (currXCard > currYCard) return 1;
                else if (currXCard < currYCard) return -1;
            } 
            return 0;
        }
    }
}
