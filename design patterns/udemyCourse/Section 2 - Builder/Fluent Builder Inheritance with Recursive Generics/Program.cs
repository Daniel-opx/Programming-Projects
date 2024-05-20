namespace Fluent_Builder_Inheritance_with_Recursive_Generics
    // now we will see what happens when builders inherite from another builder
{
    public class Person
    {
        public string Name;
        public string Position;
        public class Builder: PersonJobBuilder<Builder>
        {

        }
        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
    //class Foo : Bar<Foo>
    public class PersonInfoBuilder<SELF> : PersonBuilder 
        where SELF : PersonInfoBuilder<SELF>
    {
        
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }

    }
    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build() => person;
        
    }
    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
           var me =  Person.New.Called("dimitri").WorkAsA("carpenter").Build();
            Console.WriteLine(me);
        }
    }
}
