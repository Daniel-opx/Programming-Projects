using static Puzzle_5.RegexPatterns;
using static Puzzle_5.FileReadMethods;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Net;
using static Puzzle_5.IsInRangeMethods;
using static Puzzle_5.Program;
using System.Runtime.InteropServices;
using static Puzzle_5.ListExtensionMethods;

namespace Puzzle_5
{
    internal class Program
    {


        public enum Maps
        {
            seed = 0,
            soil,
            fertilizer,
            water,
            light,
            temperature,
            humidity,
            location
        };
        internal delegate (long? index, bool isInRange) isElementInRange((long src, long dst, long skips)[] tupleArr, long currentElement);




        static void Main(string[] args)
        {
            string inputPath = "C:\\Programming Projects\\advent of code 2023\\Puzzle 5\\Puzzle 5\\input.txt";
            string smallInput = "C:\\Programming Projects\\advent of code 2023\\Puzzle 5\\Puzzle 5\\TextFile1.txt";

            string input = inputPath; // change here the input you want to work with

            var sortedSeedList = extractNumbers.Matches(ReadFirstLine(input)) //parsing of the seeds to list of seeds , each seed represented by a number
                .Select(match => long.Parse(match.Value)).OrderBy(num => num)
                .ToList();

            // Create a dictionary to map enum values to HashSet<long>
            Dictionary<Maps, HashSet<long>> hashSets = new Dictionary<Maps, HashSet<long>>();

            // Initialize hash sets
            foreach (Maps map in Enum.GetValues(typeof(Maps)))
            {
                hashSets[map] = new HashSet<long>();

            }

            var seedToLoactionMapList = new List<SeedToLoactionMap>();


            var textBlocks = ExtractBlock.Matches(File.ReadAllText(input));

            var seedToSoilTExtBlock = textBlocks[0].Value; //make a variable for the text block

            var seedLines = seedToSoilTExtBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).
                ToList(); //converts the text into list of seedLines, each line contains (int destanation,int src,int skips)
            long[]? numbers;


            int seedToLoactionMapListIndex = 0;

            for (int j = 0; j < seedLines.Count; j++)
            {
                var sourceHashSet = hashSets[(Maps)0];
                var dstHashSet = hashSets[(Maps)1];
                numbers = extractNumbers.Matches(seedLines[j])
                    .Select(match => long.Parse(match.Value))
                    .ToArray();

                long dst = numbers[0]; long src = numbers[1]; long skips = numbers[2];
                var increment = 0;

                for (int k = 0; k < skips; k += (int)skips - 1)
                {

                    var currIndex = (int)seedToLoactionMapListIndex + increment;

                    seedToLoactionMapList.Add(new SeedToLoactionMap());
                    var srcIncrement = increment == 0 ? src : src + skips - 1;
                    var dstIncrement = increment == 0 ? dst : dst + skips - 1;
                    seedToLoactionMapList[currIndex].SetValue((Maps)0, srcIncrement);
                    seedToLoactionMapList[currIndex].SetValue((Maps)1, dstIncrement);
                    increment++;

                    sourceHashSet.Add(srcIncrement);
                    dstHashSet.Add(dstIncrement);
                }

                seedToLoactionMapListIndex = seedToLoactionMapListIndex + 2;

                if (!isRangeContainSeed(sortedSeedList, seedToLoactionMapList, seedToLoactionMapListIndex)) // delete the range of the last 2 element, they are not relevent to the seed list
                {
                    hashSets[Maps.seed].Remove(seedToLoactionMapList[seedToLoactionMapListIndex - 2].Seed);
                    hashSets[Maps.soil].Remove(seedToLoactionMapList[seedToLoactionMapListIndex - 2].Soil);
                    hashSets[Maps.seed].Remove(seedToLoactionMapList[seedToLoactionMapListIndex - 1].Seed);
                    hashSets[Maps.soil].Remove(seedToLoactionMapList[seedToLoactionMapListIndex - 1].Soil);
                    seedToLoactionMapList.RemoveRange(seedToLoactionMapListIndex - 2, 2);
                    seedToLoactionMapListIndex -= 2;


                }
            }
            var indexOfFirstRealMap = seedToLoactionMapList.Count;
            Console.WriteLine("First phase is done");
            //=================================================soil process==============================================================//
            foreach (var seed in sortedSeedList)
            {
                var boolIndexTuple = isSeedInRangeOfSeedToLoactionMapList(seedToLoactionMapList, seed, indexOfFirstRealMap); // return (index,bool isinrange) of the current seed
                if (boolIndexTuple.isInRage)
                {
                    if (!hashSets[Maps.seed].Contains(seed))
                    {
                        seedToLoactionMapList.Add(new SeedToLoactionMap());
                        seedToLoactionMapList.Last().Seed = seed; //set the seed property of the seedToLoactionMapList object to the seed that is currently being iterated on  

                        var indexOfReleventSeedObject = (int)boolIndexTuple.index;
                        seedToLoactionMapList.Last().Soil = (seedToLoactionMapList[indexOfReleventSeedObject].Soil - seedToLoactionMapList[indexOfReleventSeedObject].Seed) //the diffrence between destanation and the source
                            + seedToLoactionMapList.Last().Seed; //the seed property of the seedToLoactionMapList object that being set
                        hashSets[Maps.seed].Add(seed); hashSets[Maps.soil].Add(seedToLoactionMapList.Last().Soil);
                    }
                    else
                    {
                        var tuple = FindReleventMapIndexInList(seedToLoactionMapList, seed);
                        var index = (int)tuple.index;

                        seedToLoactionMapList.Add(seedToLoactionMapList[index]);
                    }
                }
                else
                {
                    seedToLoactionMapList.Add(new SeedToLoactionMap());
                    seedToLoactionMapList.Last().Seed = seedToLoactionMapList.Last().Soil = seed;
                    hashSets[Maps.soil].Add(seedToLoactionMapList.Last().Soil); hashSets[Maps.seed].Add(seedToLoactionMapList.Last().Seed);
                }

            }
            //====================================================================fertilizer process========================================================//

            //var soilToFertilizerTextBlock = textBlocks[(int)Maps.soil].Value;

            //var soilToFertilizerTuppleArr = soilToFertilizerTextBlock.Split("\r\n").
            //    Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
            //    {
            //        var nums = extractNumbers.Matches(str);
            //        (long soil, long fertilizer, long skips) currentTuple;
            //        currentTuple.soil = long.Parse(nums[1].Value); currentTuple.fertilizer = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
            //        return currentTuple;
            //    }).ToArray();


            //for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            //{
            //    var currentMap = seedToLoactionMapList[i];

            //    var soilBollIndexTuple = isSoilInRange(soilToFertilizerTuppleArr, currentMap.Soil);
            //    if (soilBollIndexTuple.isInRange)
            //    {
            //        var indexOfReleventTuple = (int)soilBollIndexTuple.index;

            //        currentMap.Fertilizer = soilToFertilizerTuppleArr[indexOfReleventTuple].fertilizer +
            //       (currentMap.Soil - soilToFertilizerTuppleArr[indexOfReleventTuple].soil);

            //    }
            //    else
            //    {
            //        currentMap.Fertilizer = currentMap.Soil;
            //    }
            //}
            ////=====================================================water process==============================================================///

            //var fertilizerToWaterTextBlock = textBlocks[(int)Maps.fertilizer].Value;
            //var fertilizerToWaterTuppleArr = fertilizerToWaterTextBlock.Split("\r\n").
            //    Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
            //    {
            //        var nums = extractNumbers.Matches(str);
            //        (long fertilizer, long water, long skips) currentTuple;
            //        currentTuple.fertilizer = long.Parse(nums[1].Value); currentTuple.water = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
            //        return currentTuple;
            //    }).ToArray();

            //for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            //{
            //    var currentMap = seedToLoactionMapList[i];

            //    var fertilizerToWaterBoolIndexTuple = isFertilizerInRange(fertilizerToWaterTuppleArr, currentMap.Fertilizer);
            //    if (fertilizerToWaterBoolIndexTuple.isInRange)
            //    {
            //        var indexOfReleventTuple = (int)fertilizerToWaterBoolIndexTuple.index;

            //        currentMap.Water = fertilizerToWaterTuppleArr[indexOfReleventTuple].water +
            //       (currentMap.Fertilizer - fertilizerToWaterTuppleArr[indexOfReleventTuple].fertilizer);

            //    }
            //    else
            //    {
            //        currentMap.Water = currentMap.Fertilizer;
            //    }
            //}
            ////====================================================light proccess===============================================================//

            //var WaterToLightTextBlock = textBlocks[(int)Maps.water].Value;
            ////Console.WriteLine(WaterToLightTextBlock);
            //var WaterToLightTuppleArr = WaterToLightTextBlock.Split("\r\n").
            //    Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
            //    {
            //        var nums = extractNumbers.Matches(str);
            //        (long water, long light, long skips) currentTuple;
            //        currentTuple.water = long.Parse(nums[1].Value); currentTuple.light = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
            //        return currentTuple;
            //    }).ToArray();

            //for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            //{
            //    var currentMap = seedToLoactionMapList[i];

            //    var WaterToLightBoolIndexTuple = isWaterInRange(WaterToLightTuppleArr, currentMap.Water);
            //    if (WaterToLightBoolIndexTuple.isInRange)
            //    {
            //        var indexOfRelevantMap = (int)WaterToLightBoolIndexTuple.index;

            //        currentMap.Light = WaterToLightTuppleArr[indexOfRelevantMap].light +
            //       (currentMap.Water - WaterToLightTuppleArr[indexOfRelevantMap].water);

            //    }
            //    else
            //    {
            //        currentMap.Light = currentMap.Water;
            //    }
            //}
            ////====================================================temperature proccess===============================================================//
            //var lightToTemperatureTextBlock = textBlocks[(int)Maps.light].Value;
            ////Console.WriteLine(lightToTemperatureTextBlock);


            //var lightToTemperatureTuppleArr = lightToTemperatureTextBlock.Split("\r\n").
            //    Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
            //    {
            //        var nums = extractNumbers.Matches(str);
            //        (long light, long temperature, long skips) currentTuple;
            //        currentTuple.light = long.Parse(nums[1].Value); currentTuple.temperature = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
            //        return currentTuple;
            //    }).ToArray();

            //for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            //{
            //    var currentMap = seedToLoactionMapList[i];

            //    var lightTotemperatureBoolIndexTuple = isLightInRange(lightToTemperatureTuppleArr, currentMap.Light);
            //    if (lightTotemperatureBoolIndexTuple.isInRange)
            //    {
            //        var indexOfRelevantTuple = (int)lightTotemperatureBoolIndexTuple.index;

            //        currentMap.Temperature = lightToTemperatureTuppleArr[indexOfRelevantTuple].temperature +
            //       (currentMap.Light - lightToTemperatureTuppleArr[indexOfRelevantTuple].light);

            //    }
            //    else
            //    {
            //        currentMap.Temperature = currentMap.Light;
            //    }
            //}
            ////=========================================================Humidity Process====================================================//
            //var TemperatureToHumidityTextBlock = textBlocks[(int)Maps.temperature].Value;
            ////Console.WriteLine(TemperatureToHumidityTextBlock);


            //var TemperatureToHumidityTuppleArr = TemperatureToHumidityTextBlock.Split("\r\n").
            //    Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
            //    {
            //        var nums = extractNumbers.Matches(str);
            //        (long temperature, long humidity, long skips) currentTuple;
            //        currentTuple.temperature = long.Parse(nums[1].Value); currentTuple.humidity = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
            //        return currentTuple;
            //    }).ToArray();

            //for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            //{
            //    var currentMap = seedToLoactionMapList[i];

            //    var temperatureToHumidityBoolIndexTuple = isTemperatureInRange(TemperatureToHumidityTuppleArr, currentMap.Temperature);
            //    if (temperatureToHumidityBoolIndexTuple.isInRange)
            //    {
            //        var indexOfRelevantTuple = (int)temperatureToHumidityBoolIndexTuple.index;
            //        currentMap.Humidity = TemperatureToHumidityTuppleArr[indexOfRelevantTuple].humidity +
            //       (currentMap.Temperature - TemperatureToHumidityTuppleArr[indexOfRelevantTuple].temperature);

            //    }
            //    else
            //    {
            //        currentMap.Humidity = currentMap.Temperature;
            //    }
            //}
            ////========================================location process===============================///

            //var humidityToLocationTextBlock = textBlocks[(int)Maps.humidity].Value;
            ////Console.WriteLine(humidityToLocationTextBlock);


            //var humidityToLocationTuppleArr = humidityToLocationTextBlock.Split("\r\n").
            //    Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
            //    {
            //        var nums = extractNumbers.Matches(str);
            //        (long humidity, long location, long skips) currentTuple;
            //        currentTuple.humidity = long.Parse(nums[1].Value); currentTuple.location = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
            //        return currentTuple;
            //    }).ToArray();

            //for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            //{
            //    var currentMap = seedToLoactionMapList[i];
            //    var humidityToLocationBoolIndexTuple = isHumidityInRange(humidityToLocationTuppleArr, currentMap.Temperature);

            //    if (humidityToLocationBoolIndexTuple.isInRange)
            //    {
            //        var inexOfRelevantTuple = (int)humidityToLocationBoolIndexTuple.index;
            //        currentMap.Location = humidityToLocationTuppleArr[inexOfRelevantTuple].location +
            //       (currentMap.Humidity - humidityToLocationTuppleArr[inexOfRelevantTuple].humidity);

            //    }
            //    else
            //    {
            //        currentMap.Location = currentMap.Humidity;
            //    }
            //}


            ////===================================================final calaculations===========================================================//
            //seedToLoactionMapList.RemoveRange(0, indexOfFirstRealMap);
            //var list = seedToLoactionMapList.Select(map => map.Location).ToArray().Min();
            //var minimumLOcationMap = seedToLoactionMapList.OrderBy(map => map.Location).First();
            //Console.WriteLine($"the map with the min location starts with seed:{minimumLOcationMap.Seed} and its location is {minimumLOcationMap.Location}");


            //var number = 1;
            //foreach (var map in seedToLoactionMapList)
            //{
            //    Console.WriteLine("=============================================================\n");
            //    Console.WriteLine($"{number++}.seed:{map.Seed}=>soil:{map.Soil}=>fertilizer:{map.Fertilizer}=>water:{map.Water}=>light:{map.Light}=>temp:{map.Temperature}=>humidity:{map.Humidity}=>location:{map.Location}\n");
            //}




            for (var i = 1; i < textBlocks.Count; i++)
            {
                var srcToDstTextBlock = textBlocks[i].Value;
                var currentEnum = (Maps)i;

                var srcToDstTuppleArr = srcToDstTextBlock.Split("\r\n").
               Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
               {
                   var nums = extractNumbers.Matches(str);
                   (long src, long dst, long skips) currentTuple;
                   currentTuple.src = long.Parse(nums[1].Value); currentTuple.dst = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                   return currentTuple;
               }).ToArray();

                var elementRangeDelegate = RangeMethodFinder(currentEnum);

                for (int j = indexOfFirstRealMap; j < seedToLoactionMapList.Count; j++)
                {
                    var currentMap = seedToLoactionMapList[j];

                    var srcToDstIndexBoolTuple = elementRangeDelegate(srcToDstTuppleArr, currentMap.GetValue(currentEnum));

                    if (srcToDstIndexBoolTuple.isInRange)
                    {
                        var indexOfRelevantTuple = (int)srcToDstIndexBoolTuple.index;

                        currentMap.SetValue((Maps)((int)currentEnum + 1),
                        srcToDstTuppleArr[indexOfRelevantTuple].dst +
                        (currentMap.GetValue(currentEnum) - srcToDstTuppleArr[indexOfRelevantTuple].src));
                    }
                    else
                    {
                        currentMap.SetValue((Maps)((int)currentEnum + 1), currentMap.GetValue(currentEnum));
                    }

                }
                Console.WriteLine($"textblock number {i} is done");
            }

            seedToLoactionMapList.RemoveRange(0, indexOfFirstRealMap);
            var list = seedToLoactionMapList.Select(map => map.Location).ToArray().Min();
            var minimumLOcationMap = seedToLoactionMapList.OrderBy(map => map.Location).First();
            Console.WriteLine($"the map with the min location starts with seed:{minimumLOcationMap.Seed} and its location is {minimumLOcationMap.Location}");

            var number = 1;
            foreach (var map in seedToLoactionMapList)
            {
                Console.WriteLine("=============================================================\n");
                Console.WriteLine($"{number++}.seed:{map.Seed}=>soil:{map.Soil}=>fertilizer:{map.Fertilizer}=>water:{map.Water}=>light:{map.Light}=>temp:{map.Temperature}=>humidity:{map.Humidity}=>location:{map.Location}\n");
            }


            //=========================================part 2===========================

            var foo = new List<Range> { new Range(50, 50), new Range(51, 60), new Range(52, 62), new Range(63, 70) };
            var bar = Range.CompleteRanges(new Range(50, 73), foo);

            


            var seedList = extractNumbers.Matches(ReadFirstLine(input)) //parsing of the seeds to list of seeds , each seed represented by a number
                .Select(match => long.Parse(match.Value)).ToList();


            List<Range> seedRangeList = CreateSeedRangeList(seedList); // makes a list of range objects from list of longs (ints)
            List<Range> locationRanges = new List<Range>();


            for (int i = 0; i < seedRangeList.Count; i++)
            {
                var currentRange = seedRangeList[i];
                var rangePipeline = new List<Range>();
                var rangePipelineInterim = new List<Range>();


                for (int j = 0; j < textBlocks.Count; j++)
                {
                    var srcToDstTextBlock = textBlocks[j].Value;
                    var currentEnum = (Maps)i;

                    var srcToDstTuppleArr = srcToDstTextBlock.Split("\r\n").
                   Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                   {
                       var nums = extractNumbers.Matches(str);
                       (long src, long dst, long skips) currentTuple;
                       currentTuple.src = long.Parse(nums[1].Value); currentTuple.dst = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                       return currentTuple;
                   }).ToArray();

                    if (j == 0) rangePipeline.Add(currentRange);
                    if (j >= 1)
                    {
                        EmptyAndCopyList(rangePipelineInterim, rangePipeline);
                        rangePipelineInterim.Clear();
                    }


                    foreach (Range range in rangePipeline)
                    {
                        List<Range> ranges = new List<Range>();
                        for (int k = 0; k < srcToDstTuppleArr.Length; k++)
                        {
                            var currentTupple = srcToDstTuppleArr[k];
                            if (range.Start >= currentTupple.src && range.Start < currentTupple.src + currentTupple.skips - 1
                                && range.End >= currentTupple.src && range.End < currentTupple.src + currentTupple.skips)
                            {
                                ranges.Add(new Range(range.Start, range.End));
                                ranges.Last().dstSrcDifference = currentTupple.dst - currentTupple.src;

                            }
                        }
                        ranges = ranges.OrderBy(s => s.Start).ToList();
                        //TODO:complete the whole range with proper method , work on the ranges list. iterates over list and complete all the ranges that arr missing with 
                        //dstSrcDiffrence of 0.
                        ranges = Range.CompleteRanges(range, ranges);

                        ranges.Select(rng =>
                        {
                            rng.Start = rng.Start + rng.dstSrcDifference;
                            rng.End = rng.End + rng.dstSrcDifference;
                            return rng;
                        }).ToList();
                        ranges.ForEach(rng => { rangePipelineInterim.Add(rng); });


                    }

                }
            }
        }











       




        /// <summary>
        /// empty the dst list and copies the src list items into the dst list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcList">
        /// the list that is being copied
        /// </param>
        /// <param name="dstList">
        /// the list that is being cleared and being copied to
        /// </param>
        static void EmptyAndCopyList<T>(List<T> srcList, List<T> dstList)
        {
            if (dstList.Count > 0) dstList.Clear();
            foreach (var srcRange in srcList)
            {
                dstList.Add(srcRange);
            }
        }
        /// <summary>
        /// create a list of ranges from a list of longs 
        /// </summary>
        /// <param name="seedList"> seedList is list of longs (ints) that composed of the original seeds in the input file
        /// </param>
        /// <returns></returns>
        static private List<Range> CreateSeedRangeList(List<long> seedList)
        {
            List<Range> seedRangeList = new List<Range>();
            for (var i = 0; i < seedList.Count; i += 2)
            {
                var rangeStart = seedList[i];
                var rangeSkips = seedList[i + 1];
                var currentRange = new Range(rangeStart, rangeStart + rangeSkips - 1);
                seedRangeList.Add(currentRange);
            }
            return seedRangeList;
        }
        static internal isElementInRange RangeMethodFinder(Maps element)
        {
            switch (element)
            {
                case Maps.soil:
                    return isSoilInRange;
                case Maps.fertilizer:
                    return isFertilizerInRange;
                case Maps.water:
                    return isWaterInRange;
                case Maps.light:
                    return isLightInRange;
                case Maps.temperature:
                    return isTemperatureInRange;
                case Maps.humidity:
                    return isHumidityInRange;
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, null);
            }
        }
    }

}
