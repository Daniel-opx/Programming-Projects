using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_6
{
    internal class Race
    {
        public int RaceTime { get; set; }
        public int DistaneRecord { get; set; }
        public Race() { }
        public Race(int raceTime,int distanceRecord)
        {
            this.RaceTime = raceTime;
            this.DistaneRecord = distanceRecord;
        }
    }
}
