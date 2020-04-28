using Library.Interfaces;
using System;
using System.Globalization;

namespace Library
{
    /// <summary>
    /// sorting algorithms
    /// </summary>
    public static class Sort
    {
        #region counting sort

        /// <summary>
        /// sort input using a counting sort algorithm
        /// https://en.wikipedia.org/wiki/Counting_sort
        /// </summary>
        /// <typeparam name="T">type of objects being sorted</typeparam>
        /// <param name="unsorted">unsorted input</param>
        /// <param name="maxKey">maximum key value sorting on</param>
        /// <returns>sorted array</returns>
        public static T[] CountingSort<T>(T[] unsorted, int maxKey) where T : ISortable
        {
            int[] counts = new int[maxKey + 1];

            foreach (T item in unsorted)
            {
                counts[item.Key]++;
            }

            int total = 0;

            for (int i = 0; i < counts.Length; i++)
            {
                int count = counts[i];
                counts[i] = total;
                total += count;
            }

            T[] sorted = new T[unsorted.Length];

            for (int i = 0; i < unsorted.Length; i++)
            {
                int position = counts[unsorted[i].Key];
                sorted[position] = unsorted[i];
                counts[unsorted[i].Key]++;
            }

            return sorted;
        }

        #endregion counting sort

        #region quicksort

        /// <summary>
        /// sort input using a quicksort
        /// https://en.wikipedia.org/wiki/Quicksort
        /// </summary>
        /// <typeparam name="T">type of input array</typeparam>
        /// <param name="unsorted">unsorted array</param>
        /// <returns>sorted array</returns>
        public static T[] QuickSort<T>(T[] unsorted) where T : ISortable
        {
            Random random = new Random();

            T[] sorting = new T[unsorted.Length];
            Array.Copy(unsorted, sorting, unsorted.Length);

            QuickSort(sorting, 0, sorting.Length - 1, random);

            return sorting;
        }

        private static void QuickSort<T>(T[] sorting, int low, int high, Random random) where T : ISortable
        {
            if (low < high)
            {
                int partition = Partition(sorting, low, high, random);
                QuickSort(sorting, low, partition, random);
                QuickSort(sorting, partition + 1, high, random);
            }
        }

        private static int Partition<T>(T[] sorting, int low, int high, Random random) where T : ISortable
        {
            int pivotIndex = random.Next(low, high + 1);
            T pivot = sorting[pivotIndex];

            int i = low - 1;
            int j = high + 1;

            while (true)
            {
                do
                {
                    i++;
                }
                while (sorting[i].Key < pivot.Key);
                do
                {
                    j--;
                } while (sorting[j].Key > pivot.Key);

                if (i < j)
                {
                    T temp = sorting[i];
                    sorting[i] = sorting[j];
                    sorting[j] = temp;
                }
                else
                {
                    break;
                }
            }

            return j;
        }

        #endregion quicksort
    }
}
