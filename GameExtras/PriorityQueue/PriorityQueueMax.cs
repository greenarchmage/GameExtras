﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExtras.PriorityQueue
{
  public class PriorityQueueMax<T> : IEnumerable<T> where T : IComparable<T> 
  {
    private T[] pq;
    public int Count { get; private set; }

    #region Constructors
    public PriorityQueueMax()
    {
      pq = new T[1];
      Count = 0;
    }
    public PriorityQueueMax(int size)
    {
      pq = new T[size + 1];
      Count = 0;
    }

    public PriorityQueueMax(T[] keys)
    {
      Count = keys.Length;
      pq = new T[keys.Length + 1];
      for (int i = 0; i < Count; i++)
      { pq[i + 1] = keys[i]; }
      for (int k = Count / 2; k >= 1; k--)
      { sink(k); }

      // isMinHeap(); check if min heap?
    }

    #endregion
    public bool IsEmpty() { return Count == 0; }

    public void insert(T x)
    {
      // double size of array if necessary
      if (Count == pq.Length - 1) resize(2 * pq.Length);

      // add x, and percolate it up to maintain heap invariant
      pq[++Count] = x;
      swim(Count);
      // isMinHeap(); check if min heap?
    }

    public T Max()
    {
      //if (IsEmpty()) { // Throw exception? }
      return pq[1];
    }

    private void resize(int capacity)
    {
      //capacity > n;
      T[] temp = new T[capacity];
      for (int i = 1; i <= Count; i++)
      {
        temp[i] = pq[i];
      }
      pq = temp;
    }

    public T DelMax()
    {
      //if (IsEmpty()) throw new NoSuchElementException("Priority queue underflow");
      T max = pq[1];
      exch(1, Count--);
      sink(1);
      pq[Count + 1] = default(T);     // to avoid loiterig and help with garbage collection
      if ((Count > 0) && (Count == (pq.Length - 1) / 4)) resize(pq.Length / 2);
      return max;
    }
    /***************************************************************************
     * Helper functions.
     ***************************************************************************/
    private bool less(int i, int j)
    {
      return pq[i].CompareTo(pq[j]) < 0;
    }

    private void exch(int i, int j)
    {
      T swap = pq[i];
      pq[i] = pq[j];
      pq[j] = swap;
    }

    /***************************************************************************
     * Heap functions
     ***************************************************************************/
    /// <summary>
    /// Restructures the heap if the child element has become higher than its parent
    /// </summary>
    /// <param name="k">index of priority queue object</param>
    private void swim(int k)
    {
      while (k > 1 && less(k / 2, k))
      {
        exch(k, k / 2);
        k = k / 2;
      }
    }

    private void sink(int k)
    {
      while (2 * k <= Count)
      {
        int j = 2 * k;
        if (j < Count && less(j, j + 1)) { j++; }
        if (!less(k, j)) { break; }
        exch(k, j);
        k = j;
      }
    }

    // is pq[1..N] a min heap?
    private bool isMinHeap()
    {
      return isMinHeap(1);
    }

    // is subtree of pq[1..n] rooted at k a min heap?
    private bool isMinHeap(int k)
    {
      if (k > Count) return true;
      int left = 2 * k;
      int right = 2 * k + 1;
      if (left <= Count && less(k, left)) return false;
      if (right <= Count && less(k, right)) return false;
      return isMinHeap(left) && isMinHeap(right);
    }
    

    public IEnumerator<T> GetEnumerator()
    {
      PriorityQueueMax<T> copy = new PriorityQueueMax<T>();
      for(int i = 0; i < Count; i++)
      {
        copy.insert(pq[i+1]);
      }
      while(!copy.IsEmpty())
      {
        yield return copy.DelMax();
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
      {
        return GetEnumerator();
      }
  }
}