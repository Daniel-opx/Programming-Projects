namespace Faceted_Builder
{
    public class Person
    {
        //adress
        public string StreetAddress, PostCode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress},\n " +
                   $"{nameof(PostCode)}: {PostCode},\n " +
                   $"{nameof(City)}: {City},\n " +
                   $"{nameof(CompanyName)}: {CompanyName},\n " +
                   $"{nameof(Position)}: {Position},\n " +
                   $"{nameof(AnnualIncome)}: {AnnualIncome}";
        }

    }

    public class PersonBuilder // facade
    {
        //refernce
        protected Person person = new Person();
        
        /*
         * 
        Yes, the expressions public PersonJobBuilder works => new PersonJobBuilder(person); and public PersonAdressBuilder Lives => new PersonAdressBuilder(person); 
        are indeed lambda expressions
        , specifically using the lambda expression syntax for properties.

        These are lambda expressions used to define properties with a getter.
        They are syntactic sugar for defining properties with a getter method but allow a more concise syntax when the getter is just
        returning a value calculated from some expression.

        When you use the lambda expression syntax (=>) to define a property, it automatically creates a read-only property with only a getter.
         */
        public PersonJobBuilder works => new PersonJobBuilder(person);

        public PersonAdressBuilder Lives => new PersonAdressBuilder(person);

        public static implicit operator Person(PersonBuilder pb) => pb.person;
        //The syntax you're seeing is for defining an implicit conversion operator in C#.
        //Implicit conversion operators allow you to define custom rules for converting one type to another implicitly,
        //meaning the conversion happens automatically without needing to explicitly call a conversion method.

        /*
         * So, when you have a PersonBuilder object pb and you try to use it in a 
         * context where a Person object is expected (for example, assigning it to a Person variable), 
         * the C# compiler will automatically apply this conversion,
         * creating a Person object using the person field of the PersonBuilder object.
         * This allows for more flexible and convenient usage of your types.
         */
    }
    public class PersonAdressBuilder : PersonBuilder
    {
        public PersonAdressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAdressBuilder At(string streetAdress)
        {
            person.StreetAddress = streetAdress;
            return this;
        }
        public PersonAdressBuilder WithPostCode(string postCode)
        {
            person.PostCode = postCode;
            return this;
        }
        public PersonAdressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {

        public PersonJobBuilder(Person person)
        {
            this.person = person; // we pass to the constructor of the personJobBuilder a refernce to the Person that we working on
        }
        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }
        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }
        public PersonJobBuilder Earning(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            var pb = new PersonBuilder();

            /*the reason that we can jump from the adress builder - lives - to the job builder - works - is 
             that both of this builders inherit from person builder so both of them expose every other builder out ther
            
             we used to sub builders - adress builder and job builder - to build a fluent builder for person*/
            Person person = pb.works.At("mcdonalds")
                .Earning(1000)
                .AsA("griller").
                Lives.At("tapuah street").WithPostCode("025844").In("Paris");
            Console.WriteLine(person);


        }
    }
}
