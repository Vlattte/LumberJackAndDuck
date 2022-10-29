using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T: IHeapItem<T>
{
    T[] items;
    int cur_item_count;

    // create array with the size "maxHeapSize" (usually it's grid size)
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.heapIndex = cur_item_count;
        items[cur_item_count] = item;
        SortUp(item);
        cur_item_count++;
    }

    public T RemoveFirstItem()
    {
        T firstItem = items[0];
        cur_item_count--;
        items[0] = items[cur_item_count];
        items[0].heapIndex = 0;
        SortDown(firstItem);
        return firstItem;
    }

    public void Update(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get { return cur_item_count; }
    }

    public bool Contains(T item)
    {
        return Equals(items[item.heapIndex], item);
    }

    void SortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = 2 * item.heapIndex + 1;
            int childIndexRight = 2 * item.heapIndex + 2;
            int swapIndex = 0;

            // find lowest child index
            if (childIndexLeft < cur_item_count)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < cur_item_count)
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        swapIndex = childIndexRight;

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else return; // if all children has higher priority
            }
            else // if no children
                return;
        }
    }

    void SortUp(T item)
    {
        int parentIdx = (item.heapIndex - 1) / 2;

        while(true)
        {
            T parent = items[parentIdx];
            if (item.CompareTo(parent) > 0)
            {
                Swap(item, parent);
            }
            else
            {
                break;
            }

            parentIdx = (item.heapIndex - 1) / 2;

        }
    }

    void Swap (T obj1, T obj2)
    {
        items[obj1.heapIndex] = obj2;
        items[obj2.heapIndex] = obj1;
        int temp_idx = obj1.heapIndex;
        obj1.heapIndex = obj2.heapIndex;
        obj2.heapIndex = temp_idx;
    }

}

public interface IHeapItem<T> : IComparable<T>
{ 
    int heapIndex { get; set; }
}
