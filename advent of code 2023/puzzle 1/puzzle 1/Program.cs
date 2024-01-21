using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using static puzzle_1.EnumMethods;
using static puzzle_1.ParseHanlder;


namespace puzzle_1
{
    internal class Program
    {
       
       static bool IsFirstNum = true;

        public static int ParseInput(Match match)
        {

            var parseHanler = ParseHanlderGenerator();   
             Number[] numbers = new Number[] {
                Number.one, Number.two, Number.three,Number.four, Number.five, Number.six, Number.seven, Number.eight, Number.nine
            };
            int num;
            if (int.TryParse(match.Value, out num))
            {
                return num;
            }
            else
            {
                foreach (var number in parseHanler.myDictionary)
                {
                    if (match.Value.Equals(number.Key))
                    {
                        num = IsFirstNum ? number.Value[0] : number.Value[1];
                        //num = number.Value[1];

                        return num;

                    }
                }
                return num;
            }
        }

       

        static void Main(string[] args)
        {
            /*
            //part 1 - solution
            int sum = 0, num1 = 0, num2 = 0, finalnNum = 0, counter = 0;
            var lisOfIntList = new List<List<int>>();
           using(StreamReader file = new StreamReader(@"C:\Programming Projects\advent of code 2023\puzzle 1\input.txt"))
           {
                var regex = new Regex(@"\d");
               
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    lisOfIntList.Add(new List<int>());
                    MatchCollection matches = regex.Matches(ln);
                    for (int i = 0; i < matches.Count; i++)
                    {
                        if( i  == 0 )
                        {
                           num1 =int.Parse(matches[i].Value);
                        }
                        if(i == matches.Count - 1 && matches.Count > 1)
                        {
                          num2 =  (Int32.Parse(matches[i].Value));
                        }
                       
                    }
                    if (matches.Count == 1)
                    {
                        finalnNum = Int32.Parse($"{num1}" + $"{num1}");
                    }
                    else if (matches.Count > 1)
                    {
                        finalnNum = Int32.Parse(String.Concat($"{num1}", $"{num2}"));
                    }
                    sum += finalnNum;
                    counter++;
                }

           }
            

            Console.WriteLine($"the sum is {sum}");
        */

            //                                            part 2                                               //
            //=================================================================================================//
            int sum = 0, counter = 0;

            var parseHandler2 = ParseHanlderGenerator();
           
            using (StreamReader file = new StreamReader(@"C:\Programming Projects\advent of code 2023\puzzle 1\input.txt"))
            {

                //var regex = new Regex(@"\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)");
                //var regex = new Regex(@"\d|oneight|twone|threeight|fiveight|sevenine|eightwo|eighthree|nineight|one|two|three|four|five|six|seven|eight|nine");
                var regex = parseHandler2.regex;




                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    IsFirstNum = true;
                    
                    MatchCollection matches = regex.Matches(ln);
                    //for (int i = 0; i < matches.Count; i++)   //previous code , too long
                    //{
                    //    if (i == 0)
                    //    {
                    //        num1 = ParseInput(matches[i]);
                    //    }
                    //    if (i == matches.Count - 1 && matches.Count > 1)
                    //    {
                    //        num2 = ParseInput(matches[i]);
                    //    }

                    //}
                    int num1 = ParseInput(matches[0]);
                    IsFirstNum = false;
                    int? num2 = matches.Count > 1 ? ParseInput(matches[matches.Count - 1]) : null; // trinary expression, short writing og if eles statement
                   
                    
                    int finalNum = num1 * 10 + (num2 ?? num1); // (int a ?? int b) - syntactic suger for if int a is no null choose it , eles choose int b

                    sum += finalNum;
                    counter++;
                    Console.WriteLine($"{ln} = {finalNum}");
                }
            }
            Console.WriteLine($"the sum is {sum}");
            Console.ReadKey();
            Number[] numbers = new Number[] {
                Number.one, Number.two, Number.three,Number.four, Number.five, Number.six, Number.seven, Number.eight, Number.nine
            };

            //Console.WriteLine(@"++++++++++++++++++++++++++++++++DEBUGGING AREA+++++++++++++++++++++++++++++++++");

            


            Console.ReadKey();
          
        }

        static ParseHanlder ParseHanlderGenerator()
        {
            Number[] MyEnumArray = new Number[] {
                Number.one, Number.two, Number.three,Number.four, Number.five, Number.six, Number.seven, Number.eight, Number.nine
            };


            //var daysList = new List<string>();
            ////Dictionary<string, int[]> intsDictionary = new Dictionary<string, int[]>();


            //for (int i = 0; i < MyEnumArray.Length; i++)
            //{
            //    {
            //        char lastChar = GetLastChar(MyEnumArray[i]);
            //        foreach (var element in MyEnumArray)
            //        {
            //            if (lastChar == GetFirstChar(element))
            //            {
            //                string joinedWord = String.Concat(MyEnumArray[i], element.ToString().Substring(1));
            //                daysList.Add(joinedWord); //element is the second number, myEnum[i] is the first.
            //                //intsDictionary.Add(joinedWord, new int[] { (int)MyEnumArray[i], (int)element });

            //            }
            //        }

            //    }
            //}


            var combined = MyEnumArray // create combined words of numbers
                .SelectMany(x =>
                {
                    char lastChar = GetLastChar(x);

                    return MyEnumArray
                        .Where(x2 => lastChar == GetFirstChar(x2))
                        .Select(x2 => (first: x, second: (Number?)x2));
                });
            var singles = MyEnumArray // create regular numbers 
                .Select(x =>
                {
                    Number? second = null;
                    return (first: x, second: second);
                });
            var intsDictionary = Enumerable.Concat(combined, singles) // joins both of the ienumerables to one and convert is to dictionary
                .ToDictionary(value => value.first + value.second?.ToString().Substring(1) ?? "", value => new int[] { (int)value.first, (int)(value.second ?? value.first) });
            var daysList = intsDictionary //
                .Where(pair => pair.Value[0] != pair.Value[1])
                .Select(pair => pair.Key)
                .ToList();
            //Console.WriteLine("your days list is");


            StringBuilder stringForREgex = new StringBuilder(@"\d|");



            //for (int i = 0; i < daysList.Count; i++)
            //{
            //    //Console.WriteLine(daysList[i]);
            //    stringForREgex.Append(stringForREgex, daysList[i], "|");

            //}
            //for (int i = 0; i < MyEnumArray.Length; i++)
            //{

            //    intsDictionary.Add(MyEnumArray[i].ToString(), new int[] { (int)MyEnumArray[i], (int)MyEnumArray[i] });
            //}
            //stringForREgex += String.Join('|', MyEnumArray);

            stringForREgex.Append(string.Join("|", daysList))
                .Append("|")
                .Append(String.Join('|', MyEnumArray));

            //foreach (var element in MyEnumArray
            //    .ToDictionary(value => value.ToString(), value => new int[] { (int)value, (int)value }))
            //{
            //    intsDictionary.Add(element.Key, element.Value);
            //}
                


            //Console.WriteLine(stringForREgex);
            //foreach (KeyValuePair<string, int[]> value in intsDictionary)
            //{
            //    Console.WriteLine($"{value.Key} : {value.Value[0]},{value.Value[1]}");
            //}

            var ParseHanlerClass = new ParseHanlder();
            ParseHanlerClass.regex = new Regex(stringForREgex.ToString());
            ParseHanlerClass.myDictionary = intsDictionary;
            return ParseHanlerClass;


        }
    }
}
