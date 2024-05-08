using System.Collections.Generic;
using System.Linq;
using NUnit;
using NUnit.Framework;



namespace Flyweight
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;
        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if(idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ",
            names.Select(i => strings[i]));
    }

    internal class RUNProgram
    {
        public static string RandomString()
        {
            Random rand = new Random();
            return new string(
              Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
        }

        public void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
        [Test]
        public void TestUser()
        {
            var users = new List<User>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            
        }

        [Test]
        public void TestUser2()
        {
            var users = new List<User2>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User2($"{firstName} {lastName}"));

            ForceGC();

           
        }



    }
}
