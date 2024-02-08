﻿using static Puzzle_5.RegexPatterns;
using static Puzzle_5.FileReadMethods;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Net;

namespace Puzzle_5
{
    internal class Program
    {
       
        
       public enum maps
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

            var seedList =extractNumbers.Matches(ReadFirstLine(inputPath)) //parsing of the seeds to list of seeds , each seed represented by a number
                .Select(match=>long.Parse(match.Value))
                .ToList();

            

            // Create a dictionary to map enum values to HashSet<long>
            Dictionary<maps, HashSet<long>> hashSets = new Dictionary<maps, HashSet<long>>();

            // Initialize hash sets
            foreach (maps map in Enum.GetValues(typeof(maps)))
            {
                hashSets[map] = new HashSet<long>();
            }
            var mapsArr = Enum.GetValues(typeof(maps));


            var seedToLoactionMapList = new List<SeedToLoactionMap>();
            var matchCollection = ExtractBlock.Matches(File.ReadAllText(inputPath));
            long seedToLoactionMapListIndex = 0;
          

            for (int i = 0; i < matchCollection.Count; i++) //matchCollection.count is 7, there is 7 blocks of numbers map
            {
                var textBlock = matchCollection[i].Value; //make a variable for the text block
                var lines = textBlock.Split("\r\n").Where(str => !str.Equals("") && !str.EndsWith(":")).ToList(); //converts the text into list of lines, each line contains (int destanation,int src,int skips)
                long[]? numbers;
                var sourceHashSet = hashSets[(maps)i];
                var dstHashSet = hashSets[(maps)i+1];
                
               
                for(int j = 0; j < lines.Count;j++)
                {
                     numbers = extractNumbers.Matches(lines[j]).Select(match => long.Parse(match.Value)).ToArray();
                    long dst = numbers[0]; long src = numbers[1]; long skips = numbers[2];
                    
                    if (i == 0)
                    {
                        var increment = 0;
                        for (int k = 0; k < skips; k+=(int)skips-1)
                        {

                            var currIndex = (int)seedToLoactionMapListIndex + increment;
                            
                            seedToLoactionMapList.Add(new SeedToLoactionMap());
                            var srcIncrement = increment == 0? src : src+skips-1 ;
                            var dstIncrement = increment == 0? dst : dst+skips-1 ;
                            seedToLoactionMapList[currIndex].SetValue((maps)i, srcIncrement);
                            seedToLoactionMapList[currIndex].SetValue((maps)i + 1, dstIncrement);
                            increment++;

                            sourceHashSet.Add(srcIncrement);
                            dstHashSet.Add(dstIncrement);
                        }

                        seedToLoactionMapListIndex = seedToLoactionMapListIndex + 2;
                    }




                }
               
            }
            
                



            Console.WriteLine();

        }
    }
}
