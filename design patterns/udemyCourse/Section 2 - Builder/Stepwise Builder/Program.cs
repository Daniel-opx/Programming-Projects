using System.Text;


namespace Stepwise_Builder
    // a builder in which we force the user to set the different properties of the class in a certain way
{
    public enum CarType
    {
        Sedan,
        Crossover
    }
    public interface ISpecifyCarType
    {
        ISpecifyWheelsize ofType(CarType type);
    }
    public interface ISpecifyWheelsize
    {
        IBuildCar WithWheels(int size);
    }
    public interface IBuildCar
    {
        public Car Build();
    }
    public class CarBuildr
    {
        private class impl : ISpecifyCarType, ISpecifyWheelsize, IBuildCar
        {
            private Car car = new Car();
            Car IBuildCar.Build()
            {
                return car;
                
            }

            ISpecifyWheelsize ISpecifyCarType.ofType(CarType type)
            {
                car.type = type;
                return this; // when we return this we return an impl class that converted to ISpecifyWheelsize
            }

            public override string ToString()
            {
                return base.ToString(); 
            }

            IBuildCar ISpecifyWheelsize.WithWheels(int size)
            {
                switch(car.type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"wrong size of wheel for {car.type}");
                }
                car.WheelSize = size;
                return this;
            }
        }
        public static ISpecifyCarType Create()
        {
            return new impl();
        }
    }
    public class Car
    {
        public CarType type { get; set; }
        public int WheelSize { get; set; }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var car = CarBuildr.Create() //this return ISpecifyCarType
                .ofType(CarType.Crossover). // this return ISpecifyWheelSize
                WithWheels(18) // this return IBuildCar
                .Build(); // this return car
            // in this way we force the user to set the parameter in a certain order

            var bla = CarBuildr.Create()

            
        }
    }
}
