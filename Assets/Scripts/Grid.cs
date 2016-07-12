/*
-note: have task manager ready as if you create too large of a grid by accident, you freeze
-unwalkable mask is done on the layer of objects so set the layer of the objects you dont want to walk on to unwalkable or w/e you define it
-Percent Text, Node Text, Item Pos, Node Pos are for testing
-Necesarry items to set are Player, Unwalkable Mask, Grid World Size (maybe 30,30,20?), Node Radius
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    // debug code
    public Text percentText;
    public Text nodeText;
    public Text ItemPos;
    public Text NodePos;
    //debug code end
    //do you want to draw the black squares for pathfinding
    public bool doIDraw;
    //testing node is to highlight a single node pink 
    //player needs to change but as of now its just the fps char
    public Transform player, testing_node;
    public LayerMask unwalkableMask;
    //edit this in unity to get the size of your grid
    //X is the same as the X axis on unity but Y and Z are switched
    public Vector3 gridWorldSize;
    //might remove this later
    int gridSizeX, gridSizeY, gridSizeZ;
    public float nodeRadius;
    float nodeDiameter;

    Node[,,] grid;
    //so its grid[0,0,0]
    public Vector3 worldBottomLeft;

    //need better way to do recreate grid but this will do for now
    public bool toggle;

    
    

    public Vector3 targPos;
    
    /*press spacebar to reCreate the grid at the position of the player(fps char)
    */
    public void reCreate(Vector3 targPosition)
    {
        
        CreateGrid(targPosition);
        gameObject.transform.position = player.position;  
        toggle = true; 

    }
    //init
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);
        targPos = player.position;
        CreateGrid(targPos);
    }
    //did i even use this?
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY * gridSizeZ;
        }
    }

    //creates a grid given a position
    void CreateGrid(Vector3 targPos)
    {
        grid = new Node[gridSizeX, gridSizeY, gridSizeZ];
        
        worldBottomLeft = targPos - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.y / 2) - (Vector3.up * gridWorldSize.z / 2);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius) + Vector3.up * (z * nodeDiameter + nodeRadius);
                    //check if there is an object, if so the node cannot be walked on
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                    grid[x, y, z] = new Node(walkable, worldPoint, x, y, z);
                }
            }
        }
    }
    
    //gets the neighbours of the node
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == 0 && y == 0 && z == 0)
                    {
                        continue;
                    }

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;
                    int checkZ = node.gridZ + z;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && checkZ >= 0 && checkZ < gridSizeZ)
                    {
                        neighbours.Add(grid[checkX, checkY, checkZ]);
                    }
                }
            }
        }

        return neighbours;

    }

    //Gets the node given a world position. assuming the position is in the grid
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
        //fucked up somewhere here....
        

        float percentX = (worldPosition.x - worldBottomLeft.x) / nodeDiameter;
        // y and z are not incorrectly switched (z axis starts funny)
        float percentY = (worldPosition.z - worldBottomLeft.z) / nodeDiameter;
        float percentZ = (worldPosition.y - worldBottomLeft.y) / nodeDiameter;


        int x = Mathf.RoundToInt(percentX -1);
        int y = Mathf.RoundToInt(percentY -1);
        int z = Mathf.RoundToInt(percentZ -1);
        if (x < 1) { x++; }
        if (y < 1) { y++; };
        if (z < 1) { z++; };
        //cant be -1.....

        //debug code
        /*
        nodeText.text = x.ToString() + "___" + y.ToString() + "__" + z.ToString() + "  ";
        percentText.text = "Perc(" + percentX.ToString() + ", " + percentY.ToString() + ", " + percentZ.ToString()+")";
        ItemPos.text = "ItemPos" + testing_node.position.ToString("G4");
        
        //print(percX + ", " + percZ + ", " + percY);
        //print(x + ", " + z + ", " + y);
        //print(x.ToString() + "___" + y.ToString() + "__" + z.ToString() + "  "); 
        //print(worldBottomLeft.ToString());
        */
        //debug code end
        return grid[x, y, z];
	}


    public List<Node> path;

    void OnDrawGizmos()
    {
        //when pressing left shift to recreate grid
        if(toggle == true) {
            targPos = player.position;
            toggle = false;
        }
        
        Gizmos.DrawWireCube(targPos, new Vector3(gridWorldSize.x, gridWorldSize.z, gridWorldSize.y));


        if (grid != null)
        {
            /* testing a single node
            Node playerNode = NodeFromWorldPoint(testing_node.position);
            //Node playerNode = grid[0, 0, 0];
            
            NodePos.text = playerNode.worldPosition.ToString("G4");
            //debug code end
            */
            foreach (Node n in grid)
            {
            
                /* testing a single node 
                if (playerNode == n)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
                */
                //Gizmos.color = (n.walkable) ? Color.white : Color.red;
                
                //testing dont draw if actually using it
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                        if (doIDraw == true)
                        {
                            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                        }
                        
                    }
                }
                //draws every node, dont do it in thrid person, only if you wanted to visualize a one node height grid (Grid World Size Z = 1)
                //Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}

