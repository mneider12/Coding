using Solutions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Xunit;

namespace SolutionTests
{
    public class MarkAndToysTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void MarkAndToysTest(int[] prices, int k, int expected)
        {
            Assert.Equal(expected, MarkAndToys.MaximumToys(prices, k));
        }

        public static IEnumerable<object[]> Data = new List<object[]>()
        {
            new object[] { new int[] { 1, 12, 5, 111, 200, 1000, 10 }, 50, 4 },
            new object[] { new int[] { 1, 2, 3, 4 }, 7, 3 },
            new object[] { new int[] { 3, 7, 2, 9, 4 }, 15, 3 },
        };
    }
}
