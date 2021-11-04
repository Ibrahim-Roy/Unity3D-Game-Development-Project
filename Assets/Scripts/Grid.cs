using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize; //Area the grid covers
    public float nodeRadius; //Space each node covers
    Node[,] grid; //2D node array

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //Rounded to integer as we need full nodes
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    //Create the grid of Nodes
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; //Array instance of type Node
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;//Forward in 3D space

        for (int x = 0; x < gridSizeX; x++)
        {
            Debug.Log("In x:" + x);
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);//Forward in 3D space
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); //If there is a collision (unwalkableMask) return false
                grid[x, y] = new Node(walkable, worldPoint, x, y);
                Debug.Log("Value:" + grid[x, y]);
            }
        }
    }
    
    public List<Node> GetNeighbours(Node nodeToCheck){
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {//Go through a 3 by 3 area around the node
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue; //Avoid checking the node itself
                }
                int neighbourX = nodeToCheck.gridX + x;//Find the GridX values around the node
                int neighbourY = nodeToCheck.gridY + y;//Find the GridY values arround the node

                if (neighbourX >= 0 && neighbourX < gridSizeX && neighbourY >= 0 && neighbourY < gridSizeY)
                {
                    neighbours.Add(grid[neighbourX, neighbourY]);
                }
            }

        }
        return neighbours;
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)//Find what node is at a specific point
    {
        float percentX = Mathf.Clamp01(worldPosition.x/gridWorldSize.x + 0.5f);//How far along the grid is it (between 0 and 1)
        float percentY = Mathf.Clamp01(worldPosition.z/gridWorldSize.y + 0.5f);//How far up the grid is it (between 0 and 1)

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
        return grid[x,y];

    }

    //Always draws these after running start

    public List<Node> path;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));//Y axis represents z in 3D space, draws cube around map

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red; //The cube will be white if its walkable and red otherwise
                if (path != null) //If a path exists and contains the current node, paint that node black
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f)); //Draws a cube around world position of the size 1/1/1 scaled by a little less than the diameter
            }
        }
        else
        {
            Debug.Log("No grid drawn");
        }
    }
    
}