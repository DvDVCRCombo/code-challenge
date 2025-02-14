using CodeChallenge.Services;
using FluentAssertions;
using System;
using Xunit;

namespace CodeChallenge.Tests.Services
{
    public class CalculatorServiceTests
    {
        private CalculatorService? _sut;

        public static IEnumerable<object[]> CountAndExceptionTestData =>
            new List<object[]>
            {
                new object[] { "\n7", 2 },
                new object[] { "20\n2", 2 },
                new object[] { "20\n2\nyasdf\n3", 4 },
                new object[] { "20", 1 },
                new object[] { "1,2", 2 },
                new object[] { "1,2,3", 3 },
                new object[] { "zz,yy", 2 },
                new object[] { "zz,2,D~", 3 },
                new object[] { "zz,2,D~,asf2f2", 4 }
            };

        public CalculatorServiceTests()
        {
            _sut = new CalculatorService();
        }

        [Theory]
        [MemberData(nameof(CountAndExceptionTestData))]
        public void GetStringParts_ReturnsCorrectCount(string input, int expectedCount)
        {
            var result = _sut!.GetStringParts(input);

            result.ToList().Count.Should().Be(expectedCount);
        }

        [Theory]
        [MemberData(nameof(CountAndExceptionTestData))]
        public void CalculcateAddForString_ShouldNotThrowException(string input, int _)
        {
            Action action = () => _sut!.CalculcateAddForString(input);

            action.Should().NotThrow<ArgumentException>();
        }

        // have to use array:
        // An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type
        [Theory]
        [InlineData(new int[] { 1, 2, 3, -4 })]
        public void ValidateNoNegativeNumbers_ShouldThrowExceptionForNegativeNumbers(int[] numbers)
        {
            var enumerable = numbers.AsEnumerable();

            Action action = () => _sut!.ValidateNoNegativeNumbers(enumerable);
            action.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4)]
        [InlineData(new int[] { 1, 2, 3, 1000 }, 3)]
        public void FilterOutNumbersGreaterThan1000_ShouldFilterOutNumbersGreaterThan1000(int[] numbers, int expectedCount)
        {
            var enumerable = numbers.AsEnumerable();
            var results = _sut!.FilterOutNumbersGreaterThan1000(enumerable);
            results.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData("\n7", 7)]
        [InlineData("7\n", 7)]
        [InlineData("\n", 0)]
        [InlineData("\n\n2\nu\n3", 5)]
        [InlineData("20", 20)]     
        [InlineData(",2", 2)]
        [InlineData("2,", 2)]
        [InlineData("y", 0)]
        [InlineData("7,y", 7)]
        [InlineData("asdf,7", 7)]
        [InlineData("not even close to a valid input or what we'd expect, yet here we are", 0)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
        [InlineData("a,b,c", 0)]
        [InlineData("a,b,c,7", 7)]
        [InlineData("7,a,b,c", 7)]
        [InlineData("1,5000", 1)]
        [InlineData("1\n5000\nu", 1)]
        [InlineData("1\n5000\nu\n3", 4)]
        public void CalculcateAddForString_ShouldReturnCorrectTotal(string input, int expectedResult)
        {
            var result = _sut!.CalculcateAddForString(input);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("4,-3", 1)]
        [InlineData("4\n-3", 1)]
        [InlineData("-4\nu\n\n", 1)]
        [InlineData("-4000\nu\n\n", 1)]
        public void CalculcateAddForString_ShouldThrowException(string input, int expectedResult)
        {
            Action action = () => _sut!.CalculcateAddForString(input);
            action.Should().Throw<Exception>();
        }
    }
}
