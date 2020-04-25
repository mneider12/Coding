using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Xunit;

namespace LibraryTests
{
    /// <summary>
    /// test the heap class
    /// </summary>
    public class HeapTests
    {
        /// <summary>
        /// test the IsEmpty method
        /// </summary>
        /// <typeparam name="T">type of heap being tested</typeparam>
        /// <param name="heap">heap to test</param>
        /// <param name="expectedIsEmpty">expected return value from IsEmpty</param>
        [Theory]
        [MemberData(nameof(IsEmptyTestData))]
        public void IsEmptyTest<T>(Heap<T> heap, bool expectedIsEmpty)
        {
            Assert.Equal(expectedIsEmpty, heap.IsEmpty());
        }

        /// <summary>
        /// test the Peek method
        /// </summary>
        /// <typeparam name="T">type of the heap being tested</typeparam>
        /// <param name="heap">heap to test</param>
        /// <param name="expected">expected return value from Peek</param>
        [Theory]
        [MemberData(nameof(PeekTestData))]
        public void PeekTest<T>(Heap<T> heap, T expected)
        {
            Assert.Equal(expected, heap.Peek());
        }

        /// <summary>
        /// test the exceptions thrown by Peek
        /// </summary>
        /// <typeparam name="T">type of the heap being tested</typeparam>
        /// <param name="heap">heap to test</param>
        [Theory]
        [MemberData(nameof(PeekExceptionTestData))]
        public void PeekExceptionTest<T>(Heap<T> heap)
        {
            Assert.Throws<IndexOutOfRangeException>(() => heap.Peek());
        }

        /// <summary>
        /// test the Pop method
        /// </summary>
        /// <typeparam name="T">type of the heap being tested </typeparam>
        /// <param name="heap">heap to test</param>
        /// <param name="expected">expected return value of pop</param>
        [Theory]
        [MemberData(nameof(PopTestData))]
        public void PopTest<T>(Heap<T> heap, T expected)
        {
            Assert.Equal(expected, heap.Pop());
        }

        /// <summary>
        /// test that a heap returns elements in the correct order
        /// </summary>
        /// <typeparam name="T">type of heap being tested</typeparam>
        /// <param name="heap">heap to test</param>
        /// <param name="expected">list of expected elements in order</param>
        [Theory]
        [MemberData(nameof(CombinedTestData))]
        public void CombinedTest<T>(Heap<T> heap, T[] expected)
        {
            foreach (T element in expected)
            {
                Assert.Equal(element, heap.Pop());
            }

            Assert.True(heap.IsEmpty());
        }

        #region test data

        /// <summary>
        /// data for the IsEmpty test
        /// </summary>
        public static IEnumerable<object[]> IsEmptyTestData = new List<object[]>()
        {
            new object[] { new Heap<int>(0, new IntComparer()), true },
            new object[] { AddRemoveEmptyHeap(), true },
            new object[] { SingleElementHeap(), false },
        };

        /// <summary>
        /// data for the Peek test
        /// </summary>
        public static IEnumerable<object[]> PeekTestData = new List<object[]>()
        {
            new object[] { SingleElementHeap(), 1 },
        };

        /// <summary>
        /// data for the Peek invalid test
        /// </summary>
        public static IEnumerable<object[]> PeekExceptionTestData = new List<object[]>()
        {
            new object[] { new Heap<int>(0, new IntComparer()) },
        };

        /// <summary>
        /// data for the Pop test
        /// </summary>
        public static IEnumerable<object[]> PopTestData = new List<object[]>()
        {
            new object[] { SingleElementHeap(), 1 },
        };

        /// <summary>
        /// data for the combined test
        /// </summary>
        public static IEnumerable<object[]> CombinedTestData = new List<object[]>()
        {
            new object[] { TestHeap1(), new int[] { 5, 4, 3, 2 , 1 } },
        };

        #endregion test data

        #region test data helpers

        /// <summary>
        /// make an empty heap by adding and removing an element
        /// </summary>
        /// <returns>empty heap</returns>
        private static Heap<int> AddRemoveEmptyHeap()
        {
            Heap<int> heap = new Heap<int>(1, new IntComparer());
            heap.Insert(1);
            heap.Pop();
            return heap;
        }

        /// <summary>
        /// heap with a single element (1) in it
        /// </summary>
        /// <returns>heap</returns>
        private static Heap<int> SingleElementHeap()
        {
            Heap<int> heap = new Heap<int>(1, new IntComparer());
            heap.Insert(1);
            return heap;
        }

        /// <summary>
        /// create a max heap with elements elements 1 - 5, entered in ascending order
        /// </summary>
        /// <returns>heap</returns>
        private static Heap<int> TestHeap1()
        {
            Heap<int> heap = new Heap<int>(5, new IntComparer());

            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);
            heap.Insert(4);
            heap.Insert(5);

            return heap;
        }

        #endregion empty test data helpers

        /// <summary>
        /// int comparer
        /// </summary>
        private class IntComparer : IComparer<int>
        {
            public int Compare([AllowNull] int x, [AllowNull] int y)
            {
                return x - y;
            }
        }
    }

}
