﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_6
{
    internal class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Coordinate()
        {

        }
        public Coordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
