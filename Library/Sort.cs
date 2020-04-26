using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Library
{
    public static class Sort
    {
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

        public interface ISortable
        {
            int Key { get; }
        }
    }
}
