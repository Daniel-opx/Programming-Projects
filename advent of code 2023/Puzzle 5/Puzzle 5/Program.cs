using static Puzzle_5.RegexPatterns;
using static Puzzle_5.FileReadMethods;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Net;

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

        static void Main(string[] args)
        {
            string inputPath = "C:\\Programming Projects\\advent of code 2023\\Puzzle 5\\Puzzle 5\\input.txt";
            string smallInput = "C:\\Programming Projects\\advent of code 2023\\Puzzle 5\\Puzzle 5\\TextFile1.txt";

            var seedList = extractNumbers.Matches(ReadFirstLine(inputPath)) //parsing of the seeds to list of seeds , each seed represented by a number
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


            var textBlocks = ExtractBlock.Matches(File.ReadAllText(inputPath));

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
                numbers = extractNumbers.Matches(seedLines[j]).Select(match => long.Parse(match.Value)).ToArray();
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

                if (!isRangeContainSeed(seedList, seedToLoactionMapList, seedToLoactionMapListIndex)) // delete the range of the last 2 element, they are not relevent to the seed list
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
            //=================================================soil process==============================================================//
            foreach (var seed in seedList)
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

            var soilToFertilizerTextBlock = textBlocks[(int)Maps.soil].Value;

            var soilToFertilizerTuppleArr = soilToFertilizerTextBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                {
                    var nums = extractNumbers.Matches(str);
                    (long soil, long fertilizer, long skips) currentTuple;
                    currentTuple.soil = long.Parse(nums[1].Value); currentTuple.fertilizer = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                    return currentTuple;
                }).ToArray();


            for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            {
                var currentMap = seedToLoactionMapList[i];

                var soilBollIndexTuple = isSoilInRange(soilToFertilizerTuppleArr, currentMap.Soil);
                if (soilBollIndexTuple.isInRange)
                {
                    var indexOfReleventTuple = (int)soilBollIndexTuple.index;

                    currentMap.Fertilizer = soilToFertilizerTuppleArr[indexOfReleventTuple].fertilizer +
                   (currentMap.Soil - soilToFertilizerTuppleArr[indexOfReleventTuple].soil);

                }
                else
                {
                    currentMap.Fertilizer = currentMap.Soil;
                }
            }
            //=====================================================water process==============================================================///

            var fertilizerToWaterTextBlock = textBlocks[(int)Maps.fertilizer].Value;
            var fertilizerToWaterTuppleArr = fertilizerToWaterTextBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                {
                    var nums = extractNumbers.Matches(str);
                    (long fertilizer, long water, long skips) currentTuple;
                    currentTuple.fertilizer = long.Parse(nums[1].Value); currentTuple.water = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                    return currentTuple;
                }).ToArray();

            for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            {
                var currentMap = seedToLoactionMapList[i];

                var fertilizerToWaterBoolIndexTuple = isFertilizerInRange(fertilizerToWaterTuppleArr, currentMap.Fertilizer);
                if (fertilizerToWaterBoolIndexTuple.isInRange)
                {
                    var indexOfReleventTuple = (int)fertilizerToWaterBoolIndexTuple.index;

                    currentMap.Water = fertilizerToWaterTuppleArr[indexOfReleventTuple].water +
                   (currentMap.Fertilizer - fertilizerToWaterTuppleArr[indexOfReleventTuple].fertilizer);

                }
                else
                {
                    currentMap.Water = currentMap.Fertilizer;
                }
            }
            //====================================================light proccess===============================================================//

            var WaterToLightTextBlock = textBlocks[(int)Maps.water].Value;
            //Console.WriteLine(WaterToLightTextBlock);
            var WaterToLightTuppleArr = WaterToLightTextBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                {
                    var nums = extractNumbers.Matches(str);
                    (long water, long light, long skips) currentTuple;
                    currentTuple.water = long.Parse(nums[1].Value); currentTuple.light = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                    return currentTuple;
                }).ToArray();

            for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            {
                var currentMap = seedToLoactionMapList[i];

                var WaterToLightBoolIndexTuple = isWaterInRange(WaterToLightTuppleArr, currentMap.Water);
                if (WaterToLightBoolIndexTuple.isInRange)
                {
                    var indexOfRelevantMap = (int)WaterToLightBoolIndexTuple.index;

                    currentMap.Light = WaterToLightTuppleArr[indexOfRelevantMap].light +
                   (currentMap.Water - WaterToLightTuppleArr[indexOfRelevantMap].water);

                }
                else
                {
                    currentMap.Light = currentMap.Water;
                }
            }
            //====================================================temperature proccess===============================================================//
            var lightToTemperatureTextBlock = textBlocks[(int)Maps.light].Value;
            //Console.WriteLine(lightToTemperatureTextBlock);


            var lightToTemperatureTuppleArr = lightToTemperatureTextBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                {
                    var nums = extractNumbers.Matches(str);
                    (long light, long temperature, long skips) currentTuple;
                    currentTuple.light = long.Parse(nums[1].Value); currentTuple.temperature = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                    return currentTuple;
                }).ToArray();

            for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            {
                var currentMap = seedToLoactionMapList[i];

                var lightTotemperatureBoolIndexTuple = isLightInRange(lightToTemperatureTuppleArr, currentMap.Light);
                if (lightTotemperatureBoolIndexTuple.isInRange)
                {
                    var indexOfRelevantTuple = (int)lightTotemperatureBoolIndexTuple.index;

                    currentMap.Temperature = lightToTemperatureTuppleArr[indexOfRelevantTuple].temperature +
                   (currentMap.Light - lightToTemperatureTuppleArr[indexOfRelevantTuple].light);

                }
                else
                {
                    currentMap.Temperature = currentMap.Light;
                }
            }
            //=========================================================Humidity Process====================================================//
            var TemperatureToHumidityTextBlock = textBlocks[(int)Maps.temperature].Value;
            //Console.WriteLine(TemperatureToHumidityTextBlock);


            var TemperatureToHumidityTuppleArr = TemperatureToHumidityTextBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                {
                    var nums = extractNumbers.Matches(str);
                    (long temperature, long humidity, long skips) currentTuple;
                    currentTuple.temperature = long.Parse(nums[1].Value); currentTuple.humidity = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                    return currentTuple;
                }).ToArray();

            for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            {
                var currentMap = seedToLoactionMapList[i];

                var temperatureToHumidityBoolIndexTuple = isTemperatureInRange(TemperatureToHumidityTuppleArr, currentMap.Temperature);
                if (temperatureToHumidityBoolIndexTuple.isInRange)
                {
                    var indexOfRelevantTuple = (int)temperatureToHumidityBoolIndexTuple.index;
                    currentMap.Humidity = TemperatureToHumidityTuppleArr[indexOfRelevantTuple].humidity +
                   (currentMap.Temperature - TemperatureToHumidityTuppleArr[indexOfRelevantTuple].temperature);

                }
                else
                {
                    currentMap.Humidity = currentMap.Temperature;
                }
            }
            //========================================location process===============================///

            var humidityToLocationTextBlock = textBlocks[(int)Maps.humidity].Value;
            //Console.WriteLine(humidityToLocationTextBlock);


            var humidityToLocationTuppleArr = humidityToLocationTextBlock.Split("\r\n").
                Where(str => !str.Equals("") && !str.EndsWith(":")).Select(str =>
                {
                    var nums = extractNumbers.Matches(str);
                    (long humidity, long location, long skips) currentTuple;
                    currentTuple.humidity = long.Parse(nums[1].Value); currentTuple.location = long.Parse(nums[0].Value); currentTuple.skips = long.Parse(nums[2].Value);
                    return currentTuple;
                }).ToArray();

            for (int i = indexOfFirstRealMap; i < seedToLoactionMapList.Count; i++)
            {
                var currentMap = seedToLoactionMapList[i];
                var humidityToLocationBoolIndexTuple = isHumidityInRange(humidityToLocationTuppleArr, currentMap.Temperature);

                if (humidityToLocationBoolIndexTuple.isInRange)
                {
                    var inexOfRelevantTuple = (int)humidityToLocationBoolIndexTuple.index;
                    currentMap.Location = humidityToLocationTuppleArr[inexOfRelevantTuple].location +
                   (currentMap.Humidity - humidityToLocationTuppleArr[inexOfRelevantTuple].humidity);

                }
                else
                {
                    currentMap.Location = currentMap.Humidity;
                }
            }


            //===================================================final calaculations===========================================================//
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




        }






        static (long? index, bool isInRage) isSeedInRangeOfSeedToLoactionMapList(List<SeedToLoactionMap> seedToLoactionMapList, long currentSeed, int indexOfFirstRealMap)
        {
            for (int i = 0; i < indexOfFirstRealMap; i += 2)
            {
                if (currentSeed >= seedToLoactionMapList[i].Seed && currentSeed <= seedToLoactionMapList[i + 1].Seed)
                {
                    return (i, true);
                }

            }
            return (null, false);
        }
        static (long? index, bool isInRage) FindReleventMapIndexInList(List<SeedToLoactionMap> seedToLoactionMapList, long currentSeed)
        {
            for (int i = 0; i < seedToLoactionMapList.Count; i += 2)
            {
                if (currentSeed >= seedToLoactionMapList[i].Seed && currentSeed <= seedToLoactionMapList[i + 1].Seed)
                {
                    return (i, true);
                }

            }
            return (null, false);
        }
        static bool isRangeContainSeed(List<long> seedList, List<SeedToLoactionMap>? seedToLoactionMapList, int seedToLoactionMapListIndex)
        {
            foreach (var item in seedList)
            {
                if ((item >= (long)seedToLoactionMapList[seedToLoactionMapListIndex - 2].Seed && item <= (long)seedToLoactionMapList[seedToLoactionMapListIndex - 1].Seed))
                {
                    return true;

                }

            }
            return false;
        }
        static (long? index, bool isInRange) isSoilInRange((long soil, long fertilizer, long skips)[] tupleArr, long currentSoil)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentSoil >= currTuple.soil && currentSoil <= (currTuple.soil + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
        static (long? index, bool isInRange) isFertilizerInRange((long fertilizer, long water, long skips)[] tupleArr, long currentFertilizer)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentFertilizer >= currTuple.fertilizer && currentFertilizer <= (currTuple.fertilizer + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
        static (long? index, bool isInRange) isWaterInRange((long water, long light, long skips)[] tupleArr, long currentWater)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentWater >= currTuple.water && currentWater <= (currTuple.water + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }

        static (long? index, bool isInRange) isLightInRange((long light, long temperature, long skips)[] tupleArr, long currentLight)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentLight >= currTuple.light && currentLight <= (currTuple.light + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }

        static (long? index, bool isInRange) isTemperatureInRange((long temperature, long humidity, long skips)[] tupleArr, long currentTemperature)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentTemperature >= currTuple.temperature && currentTemperature <= (currTuple.temperature + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
        static (long? index, bool isInRange) isHumidityInRange((long humidity, long location, long skips)[] tupleArr, long currentHumidity)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentHumidity >= currTuple.humidity && currentHumidity <= (currTuple.humidity + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
    }
}
