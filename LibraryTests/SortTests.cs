using Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LibraryTests
{
    public abstract class SortTests<T> where T : Sort.ISortable
    {
        [Theory]
        [MemberData(nameof(CountingSortData))]
        public void CountingSortTest(T[] unsorted, int maxKey, T[] expected) 
        {
            Assert.Equal(expected, Sort.CountingSort(unsorted, maxKey));
        }

        public static IEnumerable<object[]> CountingSortData = new List<object[]>()
        {
            new object[] { new int[] { 5, 4, 3, 2, 1 }, 5, new int[] { 1, 2, 3, 4, 5 } }
        };
    }
}
