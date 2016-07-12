using UnityEngine;
using System.Collections;

/*
- node class do not attatch to anything
*/

public class Node : IHeapItem<Node>
{
    
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX, gridY, gridZ;
    public int gCost, hCost;
    public Node parent;
    int heapIndex;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _gridZ)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        gridZ = _gridZ;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    //things below arent implemented yet because i messed up the heap datastructure. using a list atm
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        //we want lower
        return -compare;
    }
}