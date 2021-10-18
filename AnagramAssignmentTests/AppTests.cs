using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AnagramAssignment;
using FluentAssertions;
using Moq;
using Xunit;

namespace AnagramAssignmentTests
{
    public class AppTests
    {
        private readonly Mock<IOutputPrinter> _mockPrinter;
        private readonly Mock<IAnagramGrouper> _mockAnagramGrouper;

        public AppTests()
        {
            _mockPrinter = new Mock<IOutputPrinter>();
            _mockAnagramGrouper = new Mock<IAnagramGrouper>();
        }

        [Fact]
        public async Task It_Returns_True_When_Passed_Valid_FilePath()
        {
            var validFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\TestData\\example1.txt"));
            var mockArgs = new Arguments(new[] { validFilePath });

            var grouperOutput = new Dictionary<string, HashSet<string>>
            {
                { "abc", new HashSet<string>() { "abc", "bac", "cba" } },
                { "fnu", new HashSet<string> { "unf", "fun" } },
                { "hello", new HashSet<string> { "hello" } }
            };
            _mockAnagramGrouper.Setup(x => x.GetGroupedDataAsync(validFilePath))
                .ReturnsAsync(grouperOutput);

            var sut = new App(mockArgs, _mockPrinter.Object, _mockAnagramGrouper.Object);
            var result = await sut.RunAsync();

            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(grouperOutput["abc"]), Times.Once);
            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(grouperOutput["fnu"]), Times.Once);
            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(grouperOutput["hello"]), Times.Once);
            
            result.Should().BeTrue();
        }

        [Fact]
        public async Task It_Returns_Invalid_Argument_Length_When_Passed_Invalid_Argument()
        {
            var mockArgs = new Arguments(Array.Empty<string>());

            var sut = new App(mockArgs, _mockPrinter.Object, _mockAnagramGrouper.Object);
            var result = await sut.RunAsync();

            _mockPrinter.Verify(x => x.PrintInvalidArgumentLength(), Times.Once);
            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(It.IsAny<IEnumerable<string>>()), Times.Never);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task It_Returns_Invalid_File_Path_When_Passed_Invalid_Path_Argument()
        {
            var invalidArgument = string.Empty;
            var mockArgs = new Arguments(new[] { invalidArgument });

            var sut = new App(mockArgs, _mockPrinter.Object, _mockAnagramGrouper.Object);
            var result = await sut.RunAsync();

            _mockPrinter.Verify(x => x.PrintInvalidPathToFile(), Times.Once);
            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(It.IsAny<IEnumerable<string>>()), Times.Never);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task It_Returns_Non_Existent_File_When_Passed_Invalid_FilePath()
        {
            const string invalidFilePath = "non-existent-file-path";
            var mockArgs = new Arguments(new[] { invalidFilePath });

            var sut = new App(mockArgs, _mockPrinter.Object, _mockAnagramGrouper.Object);
            var result = await sut.RunAsync();

            _mockPrinter.Verify(x => x.PrintNonExistentFile(), Times.Once);
            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(It.IsAny<IEnumerable<string>>()), Times.Never);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task It_Returns_False_When_AnagramGrouper_Throws_Exception()
        {
            var validFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\TestData\\example1.txt"));
            var mockArgs = new Arguments(new[] { validFilePath });

            _mockAnagramGrouper.Setup(x => x.GetGroupedDataAsync(validFilePath))
                .Throws(new Exception());

            var sut = new App(mockArgs, _mockPrinter.Object, _mockAnagramGrouper.Object);
            var result = await sut.RunAsync();

            _mockPrinter.Verify(x => x.PrintParsingError(), Times.Once);
            _mockPrinter.Verify(x => x.PrintListAsCommaSeparatedStrings(It.IsAny<IEnumerable<string>>()), Times.Never);
            result.Should().BeFalse();
        }
    }
}