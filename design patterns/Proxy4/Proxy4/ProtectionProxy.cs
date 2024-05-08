namespace Proxy
{
    public interface Icar
    {
        void Drive();
    }
    public class Car : Icar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven, big wow!");
        }
    }
    public class Driver
    {

        public Driver()
        {
            
        }
        public Driver(int age)
        {
            this.Age = age;
        }
        public int Age { get; set; }
    }

    public class CarProxy : Icar //car proxy
    {
        private Driver driver;
        public Car car = new Car();

        public CarProxy(Driver driver)  
        {
            this.driver = driver;
        }

        public void Drive()
        {
            if(driver.Age >= 16) car.Drive();
            else Console.WriteLine("too young bitch!");

        }
    } 
    internal class ProtectionProxy
    {
        static void Main(string[] args)
        {
            Icar car = new CarProxy(new Driver(16));
            car.Drive();
            
        }
    }
}
