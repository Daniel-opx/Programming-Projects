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
        //we will now comment the enum here and try to build an abstract factory ,odel that doesnt rely on an set of enms
        //public enum AvailableDrink // violates open-closed
        //{
        //    Coffee, Tea
        //}

        //private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
        //  new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //private List<Tuple<string, IHotDrinkFactory>> namedFactories =
        //  new List<Tuple<string, IHotDrinkFactory>>();

        ///*
        // * this part of the HotDrinkMachine constructor is responsible 
        // * for initializing the factories dictionary with instances of different
        // * types of hot drink factories based on the AvailableDrink enum.
        // */
        //public HotDrinkMachine()
        //{
        //    //This loop iterates over each value in the AvailableDrink enum. This enum
        //    //represents the types of available drinks, which in this case are Coffee and Tea.
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        //Inside the loop, it dynamically creates instances of factories for each drink type using reflection.
        //        //It constructs the fully qualified type name of the factory using string concatenation,
        //        //for example, "DotNetDesignPatternDemos.Creational.AbstractFactory.CoffeeFactory" or
        //        //"DotNetDesignPatternDemos.Creational.AbstractFactory.TeaFactory".
        //        //Then it uses Activator.CreateInstance to create an instance of the factory type.
        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
        //          //important note-  the arg that get type gets need to fully match the name space of the code- it creates a full path of the object that 
        //          // is being created , case senstive so its: nameSpace_name.className
        //          Type.GetType("DotNetDesignPatternDemos.Creational.AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));


        //        factories.Add(drink, factory);
        //    }
        //}


        //// after the ctor was activaied in an ew insanciation of HotDrinkMachine object, we have dictionary with number of key
        ////pair values as the number of enum in the HotDrinkMachine class, so we have :
        //// 1. (enum AvailableDrink)Tea: TeaFactory 2. (enum AvailableDrink)Coffee : CoffeeFactory
        ////each one has a prepare function that has ahd to be implemented since this class implement IHotDrinkFactory interface
        ////that return IhotDrink object.
        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}

        /*till this separator we built a model of abtract factory that violates the open-closed-priciple :
         * in order to extend the class of hotDrinkMachine and add more drinks we need to modifty the class itself and 
         * add mor enums to the enums inside the class
        */
        //==========================================================================================================================//
        //now we will nuild abstract factory that dont violates the open closed principle
        private List<Tuple<string, IHotDrinkFactory>> factories = //declare new list of tuples 
     new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine)//typeof retur the type of the argument - in this case - class
                .Assembly // gets the system.reflection.assembly in which the type is declared
                .GetTypes()) // gets ann array of all the types that declared in the assembly
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) //checks id the type t implements the interface IHotDrinkFactory
                    && !t.IsInterface) //checks id the type is not an interface itself but a class that implemnts an interface
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", String.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            //the next block till line 140 -  till the while(true) block-  iterrates over all the list values and print the bevrages names
            Console.WriteLine("Available drinks");
            for (var index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");

            }

            //gets an input from user and validate its
            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i) // c# 7 //checks if the input is int
                    && i >= 0 // checks if the input is positive number
                    && i < factories.Count) // checks if the int is in the available range
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }
                Console.WriteLine("Incorrect input, try again.");
            }

        }


        internal class Program
        {
            static void Main(string[] args)
            {
                var machine = new HotDrinkMachine();
                IHotDrink drink = machine.MakeDrink();
                drink.Consume();



            }
        }
    }
}
