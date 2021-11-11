using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
<<<<<<< Updated upstream

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
=======
=======
>>>>>>> Stashed changes
using System;//Reverse list

public class Pathfinding : MonoBehaviour
{
<<<<<<< Updated upstream
    PathRequests requestManager;
=======
    PathRequestManager requestManager;
>>>>>>> Stashed changes
    Grid grid;

    void Awake() //Before start
    {
<<<<<<< Updated upstream
        requestManager = GetComponent<PathRequests>();
=======
        requestManager = GetComponent<PathRequestManager>();
>>>>>>> Stashed changes
        grid = GetComponent<Grid>(); //Get grid from grid script
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) 
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable) //Only pathfinds if both objects are on walkable tiles
        {
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
                    pathSuccess = true;
                    break;
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
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
                    }
                }
            }
        }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    }

    void RetracePath(Node startNode, Node endNode)
=======
=======
>>>>>>> Stashed changes
        yield return null; //Wait for 1 frame before returning
        if(pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess); //Runs with the complete path, and with information if it reached the target

    }

    Vector3[] RetracePath(Node startNode, Node endNode)
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        path.Reverse();

        grid.path = path;
=======
=======
>>>>>>> Stashed changes
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)//Store waypoints where direction changes
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }            
        return waypoints.ToArray();

<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
