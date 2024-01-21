

using System.Drawing;

namespace ClassesAndObjects
{
    internal class Car
    {   
        // Member variables
        private string _name; // private field typically used for storing data.
        private int _hp;
        private string _color;


        // Default Constructor
        public Car()
        {
            _name = "Car";
            _hp = 0;
            _color = "red";
           
        }
        // Partial Specification Constructor
        public Car(string name, int hp = 0)
        {
            _name = name;
            Console.WriteLine(name + " was created");
            _hp = hp;
            _color="red";
            
        }
        // Full Specification Constructor
        public Car(string name, int hp, string color)
        {
            _name = name;
            Console.WriteLine(name + " was created");
            _hp = hp;
            _color = color;
        }

       

        // Member Method
        public void Drive() { 
            Console.WriteLine(_name + " is driving"); 
        }

        // Member Methods
        public void Stop()
        {
            Console.WriteLine(_name + " stopped!");
        }

        public void Details()
        {
            Console.WriteLine("The "+_color +" car "+_name
                + " has "+ _hp + " hp");
        }
    }
}
