using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_4
{
     class Card
    {
        public Card()
        {
            Copies = 1;
            Matches = 0;
        }
        public Card(int id) :this() 
        {
            Id = id;
        }

        internal int Id { get; private set; }
        internal int Matches { get; set; }
        internal int Copies { get; private set; }

        public void AddCopies(int num)
        {
            Copies += num;
        }


    }
}
