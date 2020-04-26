using Library;
using Library.Interfaces;
using Library.Model;
using System.Collections.Generic;
using Xunit;

namespace LibraryTests
{
    /// <summary>
    /// tests for Sort class
    /// </summary>
    public class SortTests
    {
        /// <summary>
        /// test the counting sort algorithm
        /// </summary>
        /// <param name="unsorted">unsorted input</param>
        /// <param name="maxKey">maximum sort key in input</param>
        /// <param name="expected">expected sorted array</param>
        [Theory]
        [MemberData(nameof(CountingSortData))]
        public void CountingSort(ISortable[] unsorted, int maxKey, ISortable[] expected)
        {
            Assert.Equal(expected, Sort.CountingSort(unsorted, maxKey));
        }

        public static IEnumerable<object[]> CountingSortData = new List<object[]>()
        {
            new object[] { new SortableInt[] { 5, 4, 3, 2, 1 }, 5, new SortableInt[] { 1, 2, 3, 4, 5 } }
        };
    }
}
