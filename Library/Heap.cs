using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Library
{
    /// <summary>
    /// heap data structure implemented as a binary heap
    /// </summary>
    /// <typeparam name="T">type of element stored in the heap</typeparam>
    public class Heap<T>
    {
        #region private properties

        /// <summary>
        /// comparer to determine priority of elements stored in the heap
        /// </summary>
        /// <remarks>
        /// heap is a max heap with respect to the comparer
        /// </remarks>
        private IComparer<T> Comparer { get; set; }

        /// <summary>
        /// elements stored in the heap
        /// </summary>
        private T[] Data { get; set; }

        /// <summary>
        /// count of elements in the heap
        /// </summary>
        /// <remarks>
        /// data elements are only valid up to index Count - 1
        /// the root element is stored in Data[0].
        /// left child is at parent index * 2 + 1 right child is at parent index * 2 + 2
        /// </remarks>
        private int Count { get; set; }

        #endregion private properties

        #region constructors

        /// <summary>
        /// create a heap
        /// </summary>
        /// <param name="size">maximum size of the heap</param>
        /// <param name="comparer">comparer to determine priority of elements stored in the heap.
        ///                        heap is a max heap with respect to this comparer</param>
        public Heap(int size, IComparer<T> comparer)
        {
            Data = new T[size];
            Comparer = comparer;
            Count = 0;
        }

        #endregion constructors

        #region public methods

        /// <summary>
        /// check if the heap is empty
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// returns the root element, or a default value of element type if the heap is empty
        /// </summary>
        /// <returns>root or default value of element type</returns>
        public T Peek()
        {
            Contract.Requires(!IsEmpty());

            return Data[0];
        }

        /// <summary>
        /// remove the root element from the heap and return it
        /// </summary>
        /// <returns>root element</returns>
        public T Pop()
        {
            Contract.Requires(!IsEmpty());

            T root = Data[0];
            Data[0] = Data[Count - 1];
            Count--;
            HeapifyDown(0);

            return root;
        }

        /// <summary>
        /// insert an element into the heap
        /// </summary>
        /// <param name="node">element</param>
        public void Insert(T node)
        {
            Contract.Requires(Count < Data.Length);

            Data[Count] = node;
            HeapifyUp(Count);
            Count++;
        }

        #endregion public methods

        #region private helper methods

        /// <summary>
        /// take an element that is out of place in the heap and move it away from the root until it is in a valid position
        /// </summary>
        /// <param name="index">index in Data</param>
        private void HeapifyDown(int index)
        {
            int leftChildIndex = GetLeftChildIndex(index);

            if (IsValid(leftChildIndex))
            {
                int largestChildIndex = leftChildIndex;

                T leftChild = Data[leftChildIndex];
                T largestChild = leftChild;

                int rightChildIndex = GetRightChildIndex(index);
                if (IsValid(rightChildIndex))
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

        /// <summary>
        /// take an element out of place in the heap and move it toward the root until it is in a valid position
        /// </summary>
        /// <param name="index">index in Data</param>
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

        /// <summary>
        /// find the parent index of an element's index
        /// </summary>
        /// <param name="index">element's index in Data</param>
        /// <returns>parent's index</returns>
        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        /// <summary>
        /// find the left child index of a given index
        /// </summary>
        /// <remarks>
        /// child may not be populated. Need to check that index is valid
        /// </remarks>
        /// <param name="index">element's index</param>
        /// <returns>left child index</returns>
        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }

        /// <summary>
        /// find the right child index of a given index
        /// </summary>
        /// <remarks>
        /// child may not be populated. Need to check that index is valid
        /// </remarks>
        /// <param name="index">element's index</param>
        /// <returns>right child index</returns>
        private int GetRightChildIndex(int index)
        {
            return GetLeftChildIndex(index) + 1;
        }

        /// <summary>
        /// check if two elements should be swapped to improve the heap condition (parent and child are out of order)
        /// </summary>
        /// <param name="parent">parent element</param>
        /// <param name="child">child element</param>
        /// <returns>true or false</returns>
        private bool ShouldSwap(T parent, T child)
        {
            return Comparer.Compare(parent, child) < 0;
        }

        /// <summary>
        /// swap elements at two indexes
        /// </summary>
        /// <param name="index1">first index</param>
        /// <param name="index2">second index</param>
        private void Swap(int index1, int index2)
        {
            T temp = Data[index1];
            Data[index1] = Data[index2];
            Data[index2] = temp;
        }

        /// <summary>
        /// check if an index has valid data in it
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true or false</returns>
        private bool IsValid(int index)
        {
            return index < Count;
        }

        /// <summary>
        /// check if the index is at the root
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>true or false</returns>
        private bool IsRoot(int index)
        {
            return index == 0;
        }

        #endregion private helper methods
    }
}