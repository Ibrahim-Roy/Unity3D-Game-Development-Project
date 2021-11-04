using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    Grid grid;
    public Transform seeker, target;

    void Awake() //Before start
    {
        grid = GetComponent<Grid>(); //Get grid from grid script
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos) 
    {
        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>(); //Create a List of open Nodes
        HashSet<Node> closedSet = new HashSet<Node>(); //Hash of closed Nodes
        openSet.Add(startNode);

        while (openSet.Count > 0)//If openSet is not empty
        {
            Node currentNode = openSet[0];//Go to start of openSet
            for (int i = 1; i < openSet.Count; i++)//Go through openSet, omit the startNode
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)//A* pathfinding rules for switching lowest f cost node
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);//Remove lowest fCost node
            closedSet.Add(currentNode);//Add lowest fCost node

            if (currentNode == targetNode)
            {//Path found
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))//If can not be walked on or closedSet path skip
                {
                    continue;
                }

                int newMovementCost = currentNode.gCost + getDistance(currentNode, neighbour);
                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
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

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }

    int getDistance (Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14*distanceY + 10 * (distanceX - distanceY); //A* pathfinding calculation
        }
        else
        {
            return 14*distanceX + 10 * (distanceY - distanceX);
        }
    }
}
