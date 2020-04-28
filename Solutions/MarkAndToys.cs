using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    /// <summary>
    /// solution for the Mark and Toys sorting problem on HackerRank
    /// https://www.hackerrank.com/challenges/mark-and-toys/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=sorting
    /// </summary>
    public class MarkAndToys
    {
        public static int MaximumToys(int[] prices, int k)
        {
            int[] sortedPrices = QuickSort(prices);

            int spent = 0;
            int toyCount = -1;

            while (spent < k && toyCount < sortedPrices.Length)
            {
                toyCount++;
                spent += sortedPrices[toyCount];
            }

            return toyCount;
        }

        /// <summary>
        /// sort input using a quicksort
        /// https://en.wikipedia.org/wiki/Quicksort
        /// </summary>
        /// <typeparam name="T">type of input array</typeparam>
        /// <param name="unsorted">unsorted array</param>
        /// <returns>sorted array</returns>
        public static int[] QuickSort(int[] unsorted)
        {
            Random random = new Random();

            int[] sorting = new int[unsorted.Length];
            Array.Copy(unsorted, sorting, unsorted.Length);

            QuickSort(sorting, 0, sorting.Length - 1, random);

            return sorting;
        }

        private static void QuickSort(int[] sorting, int low, int high, Random random)
        {
            if (low < high)
            {
                int partition = Partition(sorting, low, high, random);
                QuickSort(sorting, low, partition, random);
                QuickSort(sorting, partition + 1, high, random);
            }
        }

        private static int Partition(int[] sorting, int low, int high, Random random)
        {
            int pivotIndex = random.Next(low, high + 1);
            int pivot = sorting[pivotIndex];

            int i = low - 1;
            int j = high + 1;

            while (true)
            {
                do
                {
                    i++;
                }
                while (sorting[i] < pivot);
                do
                {
                    j--;
                } while (sorting[j] > pivot);

                if (i < j)
                {
                    int temp = sorting[i];
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
    }
}
