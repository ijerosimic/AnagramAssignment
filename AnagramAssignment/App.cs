using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AnagramAssignment
{
    public class App
    {
        private readonly string[] _args;
        private readonly IOutputPrinter _printer;
        private readonly IAnagramGrouper _anagramGrouper;

        public App(Arguments arguments, IOutputPrinter printer, IAnagramGrouper anagramGrouper)
        {
            _printer = printer;
            _anagramGrouper = anagramGrouper;
            _args = arguments.Args;
        }

        public async Task<bool> RunAsync()
        {
            if (_args.Length != 1)
            {
                _printer.PrintInvalidArgumentLength();
                return false;
            }

            var path = _args[0];
            if (string.IsNullOrWhiteSpace(path))
            {
                _printer.PrintInvalidPathToFile();
                return false;
            }

            if (!File.Exists(path))
            {
                _printer.PrintNonExistentFile();
                return false;
            }

            Dictionary<string, HashSet<string>> groupedAnagrams;
            try
            {
                groupedAnagrams = await _anagramGrouper.GetGroupedDataAsync(path);
            }
            catch (Exception)
            {
                _printer.PrintParsingError();
                return false;
            }

            foreach (var group in groupedAnagrams)
            {
                _printer.PrintListAsCommaSeparatedStrings(group.Value);
            }

            return true;
        }
    }
}