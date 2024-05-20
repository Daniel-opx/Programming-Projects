using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;


namespace SOLID_principle
{
    /*single responsibilty priciple -  a class should have only single 
     responsibilty and should have one and only reason to change so in the example
    below we have the journal calss that is incharge of keeping the diary entries, and 
    another class that in charge of saving the entries on file and loading them when asked*/


    // just stores a couple of journal entries and ways of
    // working with them
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern!
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        // breaks single responsibility principle
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }

        public void Load(string filename)
        {

        }

        public void Load(Uri uri)
        {

        }
    }

    // handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I ate a bug.");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\Programming Projects\design patterns\section 1 - SOLID principle\journal.txt";
            p.SaveToFile(j, filename);
            

        }
    }
}
