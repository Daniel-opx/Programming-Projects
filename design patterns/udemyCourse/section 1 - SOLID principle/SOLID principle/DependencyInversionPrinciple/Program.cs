namespace DependencyInversionPrinciple
/*this principle dictates that high level parts of the system 
 should not depend on the low level parts of the system directly.
but instead the should depend on some kind of abstraction

 in this code the PRogram block is the high level module 
and the classes of person and relationship is the low level modules*/


{
    public enum Relationship
    {
        Parent, 
        Child , 
        Sibiling
    }
    public class Person
    {
        public string Name { get; set; }
        //Public DateTime DateOfBirth;
    }
    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOff(string name);
    }
    //Low level
    public class Relationships :IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations =
            new List<(Person, Relationship, Person)> ();

        public void AddParentAndChild(Person parent , Person child)
        {
            relations.Add((parent,Relationship.Parent,child));
            relations.Add((child, Relationship.Child, parent));

        }

        public IEnumerable<Person> FindAllChildrenOff(string name)
        {
            return relations.Where(
                per => per.Item1.Name == name &&
                per.Item2 == Relationship.Parent
            ).Select(per => per.Item3);
           
        }
        //this line of code is not good , it exposing a private field of the class
        //public List<(Person, Relationship, Person)> Relations => relations; // property
    }
    internal class Resarch
    {
        /*this ctor is not good because its hard coding the way that the filter works and
         create a situatioj in which the high level of the code - the research - depends directly on the 
        low level - the person class , and realtionship class*/
        //public Program(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach(var r in relations.Where(
        //        x => x.Item1.Name == "John" &&
        //        x.Item2 == Relationship.Parent 
        //        ))
        //    {
        //        Console.WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}



        //in this way i am not exposing the hight module - the resarch - to the low level module - the 
        //relationships class. also i can now change the way that the dat is stored and querid inside the relationship class
        // and the encapsulation preserved
        /// <summary>
        /// this constructor takes as an argument every class that implement the IRelationshipBrowser
        /// interface
        /// </summary>
        /// <param name="browser"></param>
        public Resarch(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOff("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Marry" };

            var realationships = new Relationships();
            realationships.AddParentAndChild (parent , child1);
            realationships.AddParentAndChild(parent , child2);

            new Resarch(realationships); // in the constructor i have the code that 
                                         //calls the FindAllChildrenOff method that is inside thew 
                                         //relationships class, where the private list of relationships 
                                         //is stored and thats how i can acces the dtat without exposing it 
                                         // and without creating direct depndency of the high level module on the 
                                         // low module

        }
    }
}
