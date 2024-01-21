using static Avi_s_task.Linq;

namespace Avi_s_task

    
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> mtList = new MyList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            foreach (var mt in mtList)
            {
                Console.WriteLine(mt);
            }

            var newList = mtList.MyWhere1(s => s < 5);
            foreach(var m in newList)
                Console.WriteLine(m);
        }
    }
}
