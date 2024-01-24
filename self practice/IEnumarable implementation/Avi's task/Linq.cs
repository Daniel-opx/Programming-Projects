using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Avi_s_task
{
    public static  class Linq
    {


        /*
         * yield Keyword:

The yield keyword is used within an iterator method to indicate points where the iterator's state should be saved, allowing it to be resumed later.
It allows the method to produce a sequence of values on-the-fly, and the method can be paused and resumed between yield statements.
yield is often used in conjunction with yield return to specify the values to be returned in the iteration.
yield return Statement:

The yield return statement is used within an iterator method to return a value to the caller of the iterator and also to indicate that the iterator should be paused until the next value is requested.
It specifies the current value to be returned in the iteration.
         */
      public  static IEnumerable<T> MyWhere1<T>(this IEnumerable<T> collection, Predicate<T> predicate) // contains yield return - lazy evaluation and produce "genearl" IEmnumarable object
        {
             foreach(var item in collection)
            {
                if(predicate(item))
                    yield return item;
            }

             
           
        }

       public static IEnumerable<T> MyWhere2<T>(this IEnumerable<T> collection, Predicate<T> predicate) // this method dont use yield
        {
            var list = new List<T>();
            foreach(var item in collection)
            {
                if(predicate(item))
                    list.Add(item);
            }
            return list;
        }
    }
}
