using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    float speed = 2; //Default 5
    Vector3[] path;
    int targetIndex;

    void Start()
    {
<<<<<<< Updated upstream
        PathRequests.RequestPath(transform.position, target.position, OnPathFound);
=======
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
>>>>>>> Stashed changes
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            Debug.Log("Path successful");
            path = newPath;
            StopCoroutine("FollowPath");
            Debug.Log("Stopped Coroutine");
            StartCoroutine("FollowPath");
            Debug.Log("Started Coroutine");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex ++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }
}
