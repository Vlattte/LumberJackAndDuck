using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    //grid info
    public bool walkable;
    public Vector3 position;
    public int gridX, gridY;
    public Node parentNode;

    //costs
    public int g_cost; //length from start node
    public int h_cost; //length until target node

    //heap data
    public int heap_index;

    public Node(bool _walkable, Vector3 _position, int _gridX, int _gridY) 
    {
        walkable = _walkable;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int f_cost
    {
        get { return g_cost + h_cost; }
    }

    public int heapIndex
    {
        get { return heap_index; }
        set { heap_index = value; } 
    }

    public int CompareTo(Node compareNode)
    {
        int compare = f_cost.CompareTo(compareNode.f_cost);
        if (compare == 0)
        {
            compare = h_cost.CompareTo(compareNode.h_cost);
        }
        return -compare;
    }
}
