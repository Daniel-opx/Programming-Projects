using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_5
{
    internal class FileReadMethods
    {

        static internal string ReadFirstLine(string path)
        {
            string firstLine;
            using (StreamReader sr = new StreamReader(path))
            {
                firstLine = sr.ReadLine() ?? ""; // null-coalescing operator, for more explantion view https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
                                                 //read a single line from the stream (input) , if the first line is null it will return the left side of the ?? - empty string
            }
            return firstLine;
         
        }
    }
}
