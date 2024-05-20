using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Functional_Builder
{
    public class Person
    {
        public string Name, Position;
    }
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new ()
    {

        private readonly List<Func<Person, Person>> actions =
            new List<Func<Person, Person>>();
       
        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
        public TSelf Do(Action<Person> action) => AddAction(action);

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; }); // converts the Action<Person> to Func<Person,Person>
            return (TSelf)this; // fluent method- return reference to that class
        }
    }

     public sealed class PersonBuilder : FunctionalBuilder <Person,PersonBuilder>
    {
        public PersonBuilder Called(string name) =>
            Do(p => p.Name = name);
    }
    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person, Person>> actions =
    //        new List<Func<Person, Person>>();
    //    public PersonBuilder Called(string name) => Do(p => p.Name = name);
    //    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
    //    public PersonBuilder Do(Action<Person> action) => AddAction(action);

    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p => { action(p); return p; }); // converts the Action<Person> to Func<Person,Person>
    //        return this; // fluent method- return reference to that class
    //    }
    //}

    public static class PersonBuildrExtensions
    {
        public static PersonBuilder WorkASA(this PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new PersonBuilder()
                .Called("Tony")
                .WorkASA("blabla")
                .Build();
            Console.WriteLine($"the name of {nameof(person)} is {person.Name}\n" +
                $"this person {nameof(person.Position)} is {person.Position}");
        }
    }
}
