using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_6
{
    internal class Race
    {
        public Parabola parabola;
        public double RaceTime { get; set; }
        public double DistaneRecord { get; set; }
        public Race()
        {
            parabola = new Parabola();


        }
        public Race(double raceTime, double distanceRecord) : this()
        {
            this.RaceTime = raceTime;
            this.DistaneRecord = distanceRecord;
            SetCoeficcient();
        }

        internal void ShowParobolaEquation()
        {

            if(this.parabola.coefficientB > 0)
            {
                Console.WriteLine($"{this.parabola.coefficientA}x^2+{this.parabola.coefficientB}x");
            }
            else
                Console.WriteLine($"{this.parabola.coefficientA}x^2{this.parabola.coefficientB}x");



        }
        void SetCoeficcient()
        {

            for (int i = 0; i < 3; i++) // vreate the first 2 coordinates in the parabola
            {
                double loadingTime = i;
                double distanceReached = (this.RaceTime - loadingTime) * (loadingTime);
                this.parabola.coordinateslist.Add(loadingTime, new Coordinate(loadingTime, distanceReached));
            }
            var coefficeint = Parabola.FindCoefficient(this.parabola.coordinateslist.GetValueAtIndex(1), this.parabola.coordinateslist.GetValueAtIndex(2));
            this.parabola.coefficientA = coefficeint.a;
            this.parabola.coefficientB = coefficeint.b;

        }

    }
}
