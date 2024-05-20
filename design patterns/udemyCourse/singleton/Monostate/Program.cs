using System.Collections.Generic;
using static System.Console;

namespace DotNetDesignPatternDemos.Creational.Singleton.Monostate
{

    // so in this pattern we are keeping the singleton pattern because evntually even though the user can instantiate more than one ceo
    // all the instances would refrence to the same static fields name and age and we parctically pointing to the same ceo but it has couple
    // of adress in the memory-  but all the objects refrence to same static fileds 
    public class ChiefExecutiveOfficer
    {
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var ceo = new ChiefExecutiveOfficer();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new ChiefExecutiveOfficer();
            WriteLine(ceo2);
        }
    }
}