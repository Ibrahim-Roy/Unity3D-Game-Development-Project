using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequests : MonoBehaviour
{

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>(); //Store all paths to be processed
    PathRequest currentPathRequest;

    static PathRequests instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();//pathfinding = to A* pathfinding algorithm path
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback); //Request new path to be processed
        Debug.Log(newRequest);
        instance.pathRequestQueue.Enqueue(newRequest); //Add to queue
        instance.TryProcessNext();
    }

    void TryProcessNext()//If not processing path ask to process next one
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue(); //Remove first from queue
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
    
    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _pathStart, Vector3 _pathEnd, Action<Vector3[], bool> _callback)
        {
            pathStart = _pathStart;
            pathEnd = _pathEnd;
            callback = _callback;
        }            
    }

}