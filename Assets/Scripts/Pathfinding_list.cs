using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

//must be on same game object as script "Grid.cs

public class Pathfinding_list : MonoBehaviour
{

    public Transform seeker, target, tester;
    float oppX, oppY, oppZ;
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }


    void Update()
    {
        //debug leftshift, it recreates the gird
        if (Input.GetButtonDown("Fire3")){
            tester = gameObject.transform;
            grid.reCreate(tester.position);
            grid.targPos = tester.position;
        }

        //opp is the verticie of the grid opposite of worldBottomLeft
        oppX = grid.worldBottomLeft.x + grid.gridWorldSize.x;
        //relative to nodes not Vector3
        oppY = grid.worldBottomLeft.z + grid.gridWorldSize.y;
        oppZ = grid.worldBottomLeft.y + grid.gridWorldSize.z;

        /* debug text
        print("ox"+oppX);
        print("oy"+oppZ);
        print("oz"+oppY);
        print("x"+grid.worldBottomLeft.x);
        print("y"+grid.worldBottomLeft.y);
        print("z"+grid.worldBottomLeft.z);
        */

        //its not efficient.... i think. it checks if the seeker/target are within bounds of the grid
        if (grid.worldBottomLeft.x < seeker.position.x && grid.worldBottomLeft.y < seeker.position.y && grid.worldBottomLeft.z < seeker.position.z && grid.worldBottomLeft.x < target.position.x && grid.worldBottomLeft.y < target.position.y && grid.worldBottomLeft.z < target.position.z) {
            if (oppX > seeker.position.x && oppZ > seeker.position.y && oppY > seeker.position.z && oppX > target.position.x && oppZ > target.position.y && oppY > target.position.z) {
                //if they are, find path
                FindPath(seeker.position, target.position);

            }
        }

    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {

        /* diagnostics
        Stopwatch sw = new Stopwatch();
        sw.Start();

        */


        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        
        //use heap instead of list - not implemented yet
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        //A* pathfinding
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            //found path
            if (currentNode == targetNode)
            {
                /*diagnostic
                sw.Stop();
                //lags alot when printing
                print("Path found in: " + sw.ElapsedMilliseconds + "ms");
                */
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }

            }

        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }


    //gets the distance between two nodes
    // using distances X, Y, Z (node based), go diagonally based on the largest distance and straight on the mid-low distance? or was it the other way around....
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        int dstZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);
        int high, mid, low;

        if (dstX > dstY)
        {
            if (dstY > dstZ)//xyz
            {
                high = dstX;
                mid = dstY;
                low = dstZ;
            }
            else if (dstX > dstZ)//xzy
            {
                high = dstX;
                mid = dstZ;
                low = dstY;
            }
            else //zxy
            {
                high = dstZ;
                mid = dstX;
                low = dstY;
            }
        }
        else //Y>X
        {
            if (dstX > dstZ) //yxz 
            {
                high = dstY;
                mid = dstX;
                low = dstZ;
            }
            else if (dstY > dstZ) //yzx
            {
                high = dstY;
                mid = dstZ;
                low = dstX;
            }
            else //zyx
            {
                high = dstZ;
                mid = dstY;
                low = dstX;
            }
        }
        //got highest/lowest

        return 14 * high + 10 * (mid - low);

    }

}