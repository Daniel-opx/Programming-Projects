using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_5
{
    static internal class ListExtensionMethods
    {
        
        /// <summary>
        /// method that cmpares range a and range b and return a bool wether range a overlaps (intersect) with range b, 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"> the range that range a compared against
        /// </param>
        /// <returns></returns>
        public static bool isRangeOverlapWith(this Range a, Range b)
        {
            if (a.Start >= b.Start && a.End <= b.End) return true;
            /**/

            if (a.Start < b.Start && a.End > b.Start && a.End <= b.End) return true;
            /**/
            if (a.End == b.Start) return true;
            if (b.End > a.Start && b.End < a.End && b.Start <= a.Start) return true;
            /**/
            if (a.Start == b.End) return true;
            /**/
            if (a.Start < b.Start && a.End >= b.End) return true;
            if (a.Start <= b.Start && a.End > b.End) return true;

            return false;
        }
        public static void AddAtIndex<T>(this List<T> list, int index, T item)
        {
            if (list[index] != null)
            {
                for (int i = list.Count - 1; i >= index; i--)
                {
                    list[i + 1] = list[i];

                }
                list[index] = item;
            }
            else
            {
                list.Add(item);
            }
        }

    }
}
