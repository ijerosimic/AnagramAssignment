using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramAssignment
{
    public interface IOutputPrinter
    {
        public void PrintListAsCommaSeparatedStrings(IEnumerable<string> stringsToPrint);
        public void PrintInvalidArgumentLength();
        public void PrintInvalidPathToFile();
        public void PrintNonExistentFile();
        public void PrintParsingError();
    }
    
    public class OutputPrinter : IOutputPrinter
    {
        public void PrintListAsCommaSeparatedStrings(IEnumerable<string> stringsToPrint)
        {
            var output = new StringBuilder();
            output.AppendJoin(',', stringsToPrint);
            Console.Write(output.ToString());
            Console.WriteLine();
        }

        public void PrintInvalidArgumentLength()
        {
            Console.WriteLine("Invalid arguments length.");
        }

        public void PrintInvalidPathToFile()
        {
            Console.WriteLine("Invalid path to file.");
        }

        public void PrintNonExistentFile()
        {
            Console.WriteLine("File not found.");
        }

        public void PrintParsingError()
        {
            Console.WriteLine("Error parsing anagrams file.");
        }
    }
}