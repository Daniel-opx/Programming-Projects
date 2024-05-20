using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPractice
{
    public class Car
    {
       internal int Kilometraz {  get; set; }
        internal int numberOfSeats { get; set; }
        internal string color, brand, Model; 
       internal double FeulEfficensy;

        public override string ToString()
        {
            return $"the car specs are:\n" +
                $"{nameof(Kilometraz)} : {Kilometraz}\n" +
                $"{nameof(brand)}: {brand} , {nameof(Model)} : {Model} ,  with the color of : {color}\n" +
                $"the car has {numberOfSeats} , with {nameof(FeulEfficensy)} of {FeulEfficensy} liter per kilometer" +
                $"the car did so far {Kilometraz} km";
        }

    }
}
