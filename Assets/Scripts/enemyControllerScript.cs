using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControllerScript : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float maxRoamingDistance = 10f;
    
    private GameObject target;
    private float distanceToTarget;
    private Vector2 randomRoamDestination;
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        setRandomRoamDestination();
    }

    private void Update()
    {
        checkDistanceToTarget();
        if(distanceToTarget > 3)
        {
            roamWorldRandomly();
        }
        else
        {
            chasePlayer();
        }
    }

    private void checkDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
    }

    private void roamWorldRandomly()
    {
        transform.position = Vector2.MoveTowards(transform.position, randomRoamDestination, (movementSpeed/2) * Time.deltaTime); 
        if(Vector2.Distance(transform.position, randomRoamDestination) < 0.5)
        {
            setRandomRoamDestination();
        }
    }

    private void setRandomRoamDestination()
    {
        randomRoamDestination = (new Vector2(transform.position.x, transform.position.y)) + (new Vector2(Random.Range(-maxRoamingDistance, maxRoamingDistance), Random.Range(-maxRoamingDistance, maxRoamingDistance)));
        //Use the statement below to confine the enemies to a certain area instead of roaming the entire map;
        //randomRoamDestination = new Vector2(Random.Range(-maxRoamingDistance, maxRoamingDistance), Random.Range(-maxRoamingDistance, maxRoamingDistance));
    }

    private void chasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            target.GetComponent<playerControllerScript>().takeDamage(2);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            target.GetComponent<playerControllerScript>().takeDamage(2);
        }
        else
        {
            setRandomRoamDestination();
        }
    }
}
