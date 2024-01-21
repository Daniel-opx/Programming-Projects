using System;
using System.Text.RegularExpressions;

namespace kitten_challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Test the code with 14 input strings
            TestNotHungryCats("~O~O~O~O F");
            TestNotHungryCats("O~~O~O~O F O~O~");
            TestNotHungryCats("~O~O~OF~O");
            TestNotHungryCats("F~O~O~O~O~O");
            TestNotHungryCats("~O~O~O~OF");
            TestNotHungryCats("F");
            TestNotHungryCats("O~~OF~O");
            TestNotHungryCats("~O~OF~O~O");
            TestNotHungryCats("~O~O~O~O~OF");
            TestNotHungryCats("O~F~O~O~O~O");
            TestNotHungryCats("O~~O~O5~O~O~OF~O~O~O~O");
            
            TestNotHungryCats("O~F");
            ;
            // Add more test cases as needed
            

            
            Console.ReadLine(); // To keep the console window open
        }

        static void TestNotHungryCats(string input)
        {
            string newInput = input.Replace(" ", "");
            int result = NotHungryCats(newInput); 
            if (result == -1)
            {
                Console.WriteLine("expression \"{0}\" is not valid", input);
            }
            else
            {
                Console.WriteLine($"Input: \"{input}\" => Result: {result}");
            }
           
        }

        static int NotHungryCats(string kitchen)
        {
            int counter = 0;
            string cat = "O~";
            if(!ValidateInput(kitchen))
            {
                return -1;
            }
            int index = 0;
            bool isBeforeF = true;
            while(index < kitchen.Length-1)
            {
                if (kitchen[index] == 'F')
                {
                    isBeforeF = false;
                    index++;
                    continue;
                }
                var subString = kitchen.Substring(index, 2);
                if(isBeforeF == subString.Equals(cat))
                {
                    counter++;
                }
                index += 2;
                
                
            }

            return counter;
            

            
            
           
        }
           

       

        static bool ValidateInput(string input)
        {
            var validateRegex = new Regex(@"\A(~O|O~)*F(~O|O~)*"); // the \A anchor in regex validate taht the match
            // has to start at the begining of the string
            var collection = validateRegex.Matches(input);
            bool isValid = validateRegex.IsMatch(input);
            return isValid;
        }



    }
}
