using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AnagramAssignment;
using FluentAssertions;
using Xunit;

namespace AnagramAssignmentTests
{
    public class AnagramGrouperTests
    {
        [Fact]
        public async Task It_Returns_Expected_Output_When_Passed_A_Valid_File_Path()
        {
            var grouper = new AnagramGrouper();
            var filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\TestData\\example1.txt"));

            var expectedOutput = new Dictionary<string, HashSet<string>>
            {
                { "abc", new HashSet<string> { "abc", "bac", "cba" } },
                { "fnu", new HashSet<string> { "unf", "fun" } },
                { "ehllo", new HashSet<string> { "hello" } }
            };

            var actualOutput = await grouper.GetGroupedDataAsync(filePath);

            actualOutput.Should().BeEquivalentTo(expectedOutput);
        }
        
        [Fact]
        public async Task It_Returns_Empty_Dataset_When_Passed_A_Valid_File_Path_But_File_Is_Empty()
        {
            var grouper = new AnagramGrouper();
            var filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\TestData\\emptyExample.txt"));

            var expectedOutput = new Dictionary<string, HashSet<string>>();
         
            var actualOutput = await grouper.GetGroupedDataAsync(filePath);

            actualOutput.Should().BeEquivalentTo(expectedOutput);
        }
    }
}