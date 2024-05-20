using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPractice
{
    public interface Builder
    {
        Builder withColor(string color);
        Builder ofBrand(string brand);
        Builder OfModel(string model);
        Builder SetFeulConsumption(double feulConsumption);
        Builder ThatDrivedThisKm(int drivedThisKm);

        Builder SetNumberOfSeats(int numberOfSeats);
        
    }
    public class CarBuilder : Builder
    {
        private Car car;

        public CarBuilder()
        {
            reset();
        }

        private void reset()
        {
            car = new Car();
        }

        public Builder ofBrand(string brand)
        {
            car.brand = brand;
            return this;
        }

        public Builder OfModel(string model)
        {
            car.Model = model;
            return this;
        }

        public Builder SetFeulConsumption(double feulConsumption)
        {
            car.FeulEfficensy = feulConsumption;
            return this;
        }

        public Builder SetNumberOfSeats(int numberOfSeats)
        {
            car.numberOfSeats = numberOfSeats;
            return this;
        }

        public Builder ThatDrivedThisKm(int drivedThisKm)
        {
            car.Kilometraz = drivedThisKm;
            return this;
        }

        public Builder withColor(string color)
        {
            car.color = color;
            return this;
        }
        internal Car Build()
        {
            var car = this.car;
            reset();
            return car;
        }

    }


}
