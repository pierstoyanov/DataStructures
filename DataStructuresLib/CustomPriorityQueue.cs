﻿using System;
using System.Collections.Generic;


namespace DataStructuresLib 
{
    public class CustomPriorityQueue<T>
    {
        private List<(T element, int priority)> heap;

        public CustomPriorityQueue()
        {
            heap = new List<(T, int)>();
        }

        public CustomPriorityQueue(T element, int priority)
        {
            heap = new List<(T, int)>();
            Enqueue(element, priority);
        }

        public int Count
        {
            get { return heap.Count; }
        }

        private int Parent(int idx)
        {
            return (idx - 1) / 2;
        }

        private int Left(int idx)
        {
            return 2 * idx + 1;
        }

        private int Right(int idx)
        {
            return 2 * idx + 2;
        }

        public void Enqueue(T element, int priority)
        {
            var idx = Count;
            heap.Add((element, priority));

            while (idx != 0 &&
                heap[idx].priority < heap[Parent(idx)].priority)
            {
                Swap(idx, Parent(idx));
                idx = Parent(idx);
            }
        }

        public T Dequeue()
        {
            ValidateNotEmpty();

            if (Count == 1)
            {
                return heap[Count - 1].element;
            }

            var root = heap[0];

            heap[0] = heap[Count - 1];
            heap.RemoveAt(Count - 1);

            Heapify(0);

            return root.element;
        }

        private void Heapify(int idx)
        {
            int left = Left(idx);
            int right = Right(idx);
            int smallest = idx;

            if (left < Count &&
                heap[left].priority < heap[smallest].priority)
            {
                smallest = left;
            }

            if (right < Count &&
                heap[right].priority < heap[smallest].priority)
            {
                smallest = right;
            }

            if (smallest != idx)
            {
                Swap(idx, smallest);
                Heapify(smallest);
            }
        }

        public T Peek()
        {
            ValidateNotEmpty();
            return heap[0].element;
        }

        private void HeapifyDown(int index)
        {

        }


        private void Swap(int indexOne, int indexTwo)
        {
            ValidateIndex(indexOne, indexTwo);

            (T, int) temp = heap[indexOne];
            heap[indexOne] = heap[indexTwo];
            heap[indexTwo] = temp;
        }

        private void ValidateNotEmpty()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException();
        }

        private void ValidateIndex(params int[] indexes)
        {
            foreach (int index in indexes)
            {
                if (index < 0 || index > heap.Count - 1)
                    throw new InvalidOperationException();
            }
        }


        public bool checkMinHeap()
        {
            if (heap.Count <= 1)
            {
                return true;
            }

            for (int i = 0; i <= (heap.Count - 2) / 2; i++)
            {
                // check if node has lower priority than left child
                if (heap[i].priority > heap[Left(i)].priority)
                {
                    return false;
                }

                // check if node has lower priority than right child and right child exists
                if (Right(i) != heap.Count && 
                    heap[i].priority > heap[Right(i)].priority)
                {
                    return false;
                }
            }

            return true;
        }
    }
}