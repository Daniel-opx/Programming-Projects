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


        public static bool isRangeOverlapWith(this Range rangeA, Range rangeB)
        {
            if (rangeA.Start <= rangeB.Start && rangeA.End >= rangeB.End) return true;
            if (rangeA.Start > rangeB.Start && (rangeB.End > rangeA.Start && rangeB.End < rangeA.End)) return true;
            if ((rangeB.Start > rangeA.Start && rangeB.Start < rangeA.End) && rangeB.End > rangeA.End) return true;
            if (rangeA.Start > rangeB.Start && rangeA.End < rangeB.End) return true;
            if (rangeA.Start == rangeB.Start && rangeA.End < rangeB.End) return true;
            if(rangeA.Start > rangeB.Start && rangeA.End == rangeB.End) return true;
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
