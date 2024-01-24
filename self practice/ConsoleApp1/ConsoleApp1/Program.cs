using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int> { 1, 5, 4, 788, 9, 6, 966 };
            var foo = list.Where(x => x > 5);
            var bar = foo.GetEnumerator();
            var bar2 = bar.MoveNext();
          }
    }
}
