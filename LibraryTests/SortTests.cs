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
        /// test the sorting algorithms
        /// </summary>
        /// <param name="expected">expected sorted array</param>
        /// <param name="unsorted">unsorted input</param>
        /// <param name="maxKey">maximum sort key in input</param>
        [Theory]
        [MemberData(nameof(CountingSortData))]
        public void SortTest(ISortable[] expected, ISortable[] unsorted, int maxKey)
        {
            Assert.Equal(expected, Sort.CountingSort(unsorted, maxKey));
            Assert.Equal(expected, Sort.QuickSort(unsorted));
        }

        /// <summary>
        /// data for sort test
        /// </summary>
        public static IEnumerable<object[]> CountingSortData = new List<object[]>()
        {
            new object[] { new SortableInt[] { 1, 2, 3, 4, 5 }, new SortableInt[] { 5, 4, 3, 2, 1 }, 5 }
        };
    }
}
