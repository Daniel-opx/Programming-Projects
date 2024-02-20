using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_5
{
    internal class Range
    {


        public long Start { get; set; }
        public long End { get; set; }
        public long dstSrcDifference { get; set; }
        public Range()
        {

        }
        public Range(long rangeStart, long rangeEnd) : this()
        {
            this.Start = rangeStart;
            this.End = rangeEnd;
            this.dstSrcDifference = 0;
        }
        public Range(long rangeStart, long rangeEnd, long difference)
        {
            this.Start = rangeStart;
            this.End = rangeEnd;
            this.dstSrcDifference = difference;
        }

        internal static List<Range> CompleteRanges(Range rangeInQuestion, List<Range> ranges)
        {
            List<Range> completedRanges = new List<Range>();
            if (ranges.Count == 0)
            {
                completedRanges.Add(rangeInQuestion);
                completedRanges.Last().dstSrcDifference = 0;
                return completedRanges;

            }
            // Add the range before the first range in the list
            if (rangeInQuestion.Start < ranges[0].Start)
                completedRanges.Add(new Range(rangeInQuestion.Start, ranges[0].Start - 1) { dstSrcDifference = 0 });

            // Add the ranges between existing ranges
            for (int i = 0; i < ranges.Count - 1; i++)
            {
                long start = ranges[i].End + 1;
                long end = ranges[i + 1].Start - 1;
                if (start <= end)
                    completedRanges.Add(new Range(start, end) { dstSrcDifference = 0 });
            }

            // Add the range after the last range in the list
            if (rangeInQuestion.End > ranges[ranges.Count - 1].End)
                completedRanges.Add(new Range(ranges[ranges.Count - 1].End + 1, rangeInQuestion.End) { dstSrcDifference = 0 });
            completedRanges = MergeAndOrderList(ranges, completedRanges);
            return completedRanges;
        }

        static List<Range> MergeAndOrderList(List<Range> src, List<Range> dest)
        {
            foreach (Range range in src)
            {
                dest.Add(range);
            }
            dest = dest.OrderBy(s => s.Start).ToList();
            return dest;
        }
        internal static Range tuppleToRangeConverter((long src, long dst, long skips) currentTuple) =>
             new Range(currentTuple.src, currentTuple.src + currentTuple.skips - 1) { dstSrcDifference = currentTuple.dst - currentTuple.src };
        /// <summary>
        /// static Range class method- compares range a to range b and return the intersection range betweeen a and b,
        /// </summary>
        /// <param name="a">
        /// range a is the range that we want to get the intersection in </param>
        /// <param name="b"> the range that range a is being compared against </param>
        /// <returns></returns>
        internal static Range FindOverlap(Range a, Range b)
        {
            if (a.Start >= b.Start && a.End <= b.End) return new Range(a.Start, a.End) { dstSrcDifference = b.dstSrcDifference };
            /**/

            if (a.Start < b.Start && a.End > b.Start && a.End <= b.End) return new Range(b.Start, a.End) { dstSrcDifference = b.dstSrcDifference };
            /**/
            if (a.End == b.Start) return new Range(b.Start, b.Start) { dstSrcDifference = b.dstSrcDifference };
            /**/
            if (b.End > a.Start && b.End < a.End && b.Start <= a.Start) return new Range(a.Start, b.End) { dstSrcDifference = b.dstSrcDifference };
            /**/
            if (a.Start == b.End) return new Range(a.Start, a.Start) { dstSrcDifference = b.dstSrcDifference };
            /**/
            if (a.Start < b.Start && a.End >= b.End) return new Range(b.Start, b.End) { dstSrcDifference = b.dstSrcDifference };

            if (a.Start <= b.Start && a.End > b.End) return new Range(b.Start, b.End) { dstSrcDifference = b.dstSrcDifference };

            return new Range(-5, -5);




        }
    }
}
