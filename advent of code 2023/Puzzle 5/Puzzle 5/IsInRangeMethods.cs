using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_5
{
    internal class IsInRangeMethods
    {


        public static (long? index, bool isInRage) isSeedInRangeOfSeedToLoactionMapList(List<SeedToLoactionMap> seedToLoactionMapList, long currentSeed, int indexOfFirstRealMap)
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
        public static (long? index, bool isInRage) FindReleventMapIndexInList(List<SeedToLoactionMap> seedToLoactionMapList, long currentSeed)
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
        public static bool isRangeContainSeed(List<long> seedList, List<SeedToLoactionMap>? seedToLoactionMapList, int seedToLoactionMapListIndex)
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
        //===========================================================================seed process================================================================================//

        public static (long? index, bool isInRange) isSoilInRange((long soil, long fertilizer, long skips)[] tupleArr, long currentSoil)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentSoil >= currTuple.soil && currentSoil <= (currTuple.soil + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
       public static (long? index, bool isInRange) isFertilizerInRange((long fertilizer, long water, long skips)[] tupleArr, long currentFertilizer)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentFertilizer >= currTuple.fertilizer && currentFertilizer <= (currTuple.fertilizer + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
       public static (long? index, bool isInRange) isWaterInRange((long water, long light, long skips)[] tupleArr, long currentWater)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentWater >= currTuple.water && currentWater <= (currTuple.water + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }

      public  static (long? index, bool isInRange) isLightInRange((long light, long temperature, long skips)[] tupleArr, long currentLight)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentLight >= currTuple.light && currentLight <= (currTuple.light + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }

       public static (long? index, bool isInRange) isTemperatureInRange((long temperature, long humidity, long skips)[] tupleArr, long currentTemperature)
        {
            for (int i = 0; i < tupleArr.Length; i++)
            {
                var currTuple = tupleArr[i];

                if (currentTemperature >= currTuple.temperature && currentTemperature <= (currTuple.temperature + currTuple.skips - 1)) return (i, true);
            }
            return (null, false);
        }
       public static (long? index, bool isInRange) isHumidityInRange((long humidity, long location, long skips)[] tupleArr, long currentHumidity)
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

