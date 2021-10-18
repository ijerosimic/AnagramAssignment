using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramAssignment
{
    public interface IAnagramGrouper
    {
        Task<Dictionary<string, HashSet<string>>> GetGroupedDataAsync(string filePath);
    }

    public class AnagramGrouper : IAnagramGrouper
    {
        public async Task<Dictionary<string, HashSet<string>>> GetGroupedDataAsync(string filePath)
        {
            var groupedStrings = new Dictionary<string, HashSet<string>>();

            using var sr = File.OpenText(filePath);

            string currentLine;
            while ((currentLine = await sr.ReadLineAsync()) != null)
                CreateOrUpdateGroup(currentLine, groupedStrings);

            return groupedStrings;
        }

        private static void CreateOrUpdateGroup(string item, IDictionary<string, HashSet<string>> anagramGroupsDictionary)
        {
            var key = string.Concat(item.OrderBy(c => c));

            if (!anagramGroupsDictionary.ContainsKey(key))
                anagramGroupsDictionary.Add(key, new HashSet<string> { item });
            else
                anagramGroupsDictionary[key].Add(item);
        }
    }
}