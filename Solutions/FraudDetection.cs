using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Solutions
{
    /// <summary>
    /// Solution for Fraudulent Activity Notification challenge on Hacker Rank
    /// https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=sorting
    /// </summary>
    public class FraudDetection
    {
        /// <summary>
        /// given a list of expenditures, and number of trailing days, 
        /// alert whenever a day's expenditures exceed 2x the trailing days median expenditures
        /// </summary>
        /// <param name="expenditure">list of daily expenditures</param>
        /// <param name="d">trailing days to calculate the median</param>
        /// <returns>number of alerts</returns>
        public static int ActivityNotifications(int[] expenditure, int d)
        {
            if (d < 1)
            {
                return 0;
            }

            Heap<ExpenditureHistory> left = new Heap<ExpenditureHistory>(expenditure.Length, new ExpenditureHistoryComparer());
            Heap<ExpenditureHistory> right = new Heap<ExpenditureHistory>(expenditure.Length, new ExpenditureHistoryComparer()
            {
                Min = true,
            });

            int leftCount = 0;
            int rightCount = 0;
            int alerts = 0;
            decimal median = 0m;

            Queue<ExpenditureHistory> current = new Queue<ExpenditureHistory>();

            foreach (int amount in expenditure)
            {
                ExpenditureHistory newExpenditure = new ExpenditureHistory()
                {
                    Expenditure = amount,
                };

                if (leftCount == 0)
                {
                    newExpenditure.Side = Side.Left;
                    left.Add(newExpenditure);
                    leftCount++;
                }
                else
                {
                    if (current.Count == d && amount >= 2 * median)
                    {
                        alerts++;
                    }

                    if (leftCount > rightCount)
                    {
                        newExpenditure.Side = Side.Right;
                        right.Add(newExpenditure);
                        rightCount++;
                    }
                    else
                    {
                        newExpenditure.Side = Side.Left;
                        left.Add(newExpenditure);
                        leftCount++;
                    }

                    ExpenditureHistory leftMax = left.Peek();
                    ExpenditureHistory rightMin = right.Peek();

                    if (leftMax != null && rightMin != null && leftMax.Expenditure > rightMin.Expenditure)
                    {
                        ExpenditureHistory toLeft = right.Pop();
                        ExpenditureHistory toRight = left.Pop();

                        toLeft.Side = Side.Left;
                        toRight.Side = Side.Right;

                        left.Add(toLeft);
                        right.Add(toRight);
                    }
                }

                current.Enqueue(newExpenditure);
                if (current.Count > d)
                {
                    ExpenditureHistory expired = current.Dequeue();
                    expired.Expired = true;
                    if (expired.Side == Side.Left)
                    {
                        leftCount--;
                    }
                    else
                    {
                        rightCount--;
                    }
                }

                while (left.Peek()?.Expired ?? false)
                {
                    left.Pop();
                }
                while (right.Peek()?.Expired ?? false)
                {
                    right.Pop();
                }

                if (leftCount < rightCount)
                {
                    ExpenditureHistory toLeft = right.Pop();
                    toLeft.Side = Side.Left;
                    left.Add(toLeft);
                    leftCount++;
                    rightCount--;
                }
                else if (rightCount < leftCount - 1)
                {
                    ExpenditureHistory toRight = left.Pop();
                    toRight.Side = Side.Right;
                    right.Add(toRight);
                    rightCount++;
                    leftCount--;
                }

                median = GetMedian(left, right, leftCount, rightCount);

            }
            return alerts;
        }

        private static decimal GetMedian(Heap<ExpenditureHistory> left, Heap<ExpenditureHistory> right, int leftCount, int rightCount)
        {
            if (leftCount == 0 && rightCount == 0)
            {
                return 0m;
            }
            else if (leftCount == rightCount)
            {
                return (left.Peek().Expenditure + right.Peek().Expenditure) / 2m;
            }
            else if (leftCount > rightCount)
            {
                return left.Peek().Expenditure;
            }
            else
            {
                return right.Peek().Expenditure;
            }
        }

        private class ExpenditureHistoryComparer : IComparer<ExpenditureHistory>
        {
            public bool Min { get; set; }
            public int Compare([AllowNull] ExpenditureHistory x, [AllowNull] ExpenditureHistory y)
            {
                return ((x?.Expenditure ?? 0) - (y?.Expenditure ?? 0)) * (Min ? -1 : 1);
            }
        }

        private class ExpenditureHistory
        {
            /// <summary>
            /// this expenditure is no longer current
            /// </summary>
            public bool Expired { get; set; }

            public Side Side { get; set; }

            /// <summary>
            /// amount of this expenditure
            /// </summary>
            public int Expenditure { get; set; }
        }

        private enum Side
        {
            Left, Right,
        }

        private class Heap<T>
        {
            private IComparer<T> Comparer { get; set; }
            private T[] Data { get; set; }

            private int Count { get; set; }

            public Heap(int size, IComparer<T> comparer)
            {
                Data = new T[size];
                Comparer = comparer;
                Count = 0;
            }

            public T Peek()
            {
                return Count > 0 ? Data[0] : default;
            }

            public T Pop()
            {
                T root = Data[0];
                
                Data[0] = Data[Count - 1];
                Count--;
                HeapifyDown(0);

                return root;
            }

            public void Add(T node)
            {
                Data[Count] = node;
                HeapifyUp(Count);
                Count++;
            }

            private void HeapifyDown(int index)
            {
                int leftChildIndex = GetLeftChildIndex(index);

                if (leftChildIndex < Count)
                {
                    int largestChildIndex = leftChildIndex;

                    T leftChild = Data[leftChildIndex];
                    T largestChild = leftChild;

                    int rightChildIndex = GetRightChildIndex(index);
                    if (rightChildIndex < Count)
                    {
                        T rightChild = Data[rightChildIndex];

                        if (Comparer.Compare(leftChild, rightChild) < 0)
                        {
                            largestChildIndex = rightChildIndex;
                            largestChild = rightChild;
                        }
                    }
                    
                    T parent = Data[index];
                    if (ShouldSwap(parent, largestChild))
                    {
                        Swap(largestChildIndex, index);
                        HeapifyDown(largestChildIndex);
                    }

                }
            }

            private void HeapifyUp(int index)
            {
                if (!IsRoot(index))
                {
                    int parent = GetParentIndex(index);
                    if (ShouldSwap(Data[parent], Data[index]))
                    {
                        Swap(index, parent);
                        HeapifyUp(parent);
                    }
                }
            }

            private int GetParentIndex(int index)
            {
                return (index - 1) / 2;
            }

            private int GetLeftChildIndex(int index)
            {
                return index * 2 + 1;
            }

            private int GetRightChildIndex(int index)
            {
                return index * 2 + 2;
            }

            private bool ShouldSwap(T parent, T child)
            {
                return Comparer.Compare(parent, child) < 0;
            }

            private void Swap(int index1, int index2)
            {
                T temp = Data[index1];
                Data[index1] = Data[index2];
                Data[index2] = temp;
            }
            
            private bool IsRoot(int index)
            {
                return index == 0;
            }
        }
    }
}
