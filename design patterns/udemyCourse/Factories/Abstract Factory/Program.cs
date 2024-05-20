using System.Security.Cryptography.X509Certificates;
using static System.Console;

namespace DotNetDesignPatternDemos.Creational.AbstractFactory

{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("this tea is nice , yummy!");
        }
    }
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This coffee is yumyyyy!!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);

    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();

        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();

        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink // violates open-closed
        {
            Coffee, Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
          new Dictionary<AvailableDrink, IHotDrinkFactory>();

        private List<Tuple<string, IHotDrinkFactory>> namedFactories =
          new List<Tuple<string, IHotDrinkFactory>>();

        /*
         * this part of the HotDrinkMachine constructor is responsible 
         * for initializing the factories dictionary with instances of different
         * types of hot drink factories based on the AvailableDrink enum.
         */
        public HotDrinkMachine()
        {
            //This loop iterates over each value in the AvailableDrink enum. This enum
            //represents the types of available drinks, which in this case are Coffee and Tea.
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                //Inside the loop, it dynamically creates instances of factories for each drink type using reflection.
                //It constructs the fully qualified type name of the factory using string concatenation,
                //for example, "DotNetDesignPatternDemos.Creational.AbstractFactory.CoffeeFactory" or
                //"DotNetDesignPatternDemos.Creational.AbstractFactory.TeaFactory".
                //Then it uses Activator.CreateInstance to create an instance of the factory type.
                var factory = (IHotDrinkFactory)Activator.CreateInstance(
                    //important note-  the arg that get type gets need to fully match the name space of the code- it creates a full path of the object that 
                    // is being created , case senstive so its: nameSpace_name.className
                  Type.GetType("DotNetDesignPatternDemos.Creational.AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));


                factories.Add(drink, factory);
            }
        }


        // after the ctor was activaied in an ew insanciation of HotDrinkMachine object, we have dictionary with number of key
        //pair values as the number of enum in the HotDrinkMachine class, so we have :
        // 1. (enum AvailableDrink)Tea: TeaFactory 2. (enum AvailableDrink)Coffee : CoffeeFactory
        //each one has a prepare function that has ahd to be implemented since this class implement IHotDrinkFactory interface
        //that return IhotDrink object.
        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();


        }
    }
}
