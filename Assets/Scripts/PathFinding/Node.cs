using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //grid info
    public bool walkable;
    public Vector3 position;
    public int gridX, gridY;
    public Node parentNode;

    //costs
    public int g_cost; //length from start node
    public int h_cost; //length until target node

    public Node(bool _walkable, Vector3 _position, int _gridX, int _gridY) 
    {
        walkable = _walkable;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get { return g_cost + h_cost; }
    }
}
