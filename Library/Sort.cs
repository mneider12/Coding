using Library.Interfaces;

namespace Library
{
    /// <summary>
    /// sorting algorithms
    /// </summary>
    public static class Sort
    {
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
    }
}
