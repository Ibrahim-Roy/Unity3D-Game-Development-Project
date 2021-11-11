using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileNPC : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public float maximumRoamingDistance;
    public float stoppingDistance;
    public bool territorialRoaming;
    public float detectionDistance;
    public float attackDelayTime;


    public void takeDamage(float damage)
    {
        if(health > 1)
        {
            health--;
            StartCoroutine(animateTakeDamage(0.2f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected GameObject target;
    protected float distanceToTarget;
    protected Vector2 randomRoamDestination;
    protected Vector2 originalPosition;
    protected bool isCoroutineExecuting = false;
    protected Coroutine attackCoroutine;

    protected virtual void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        setRandomRoamDestination();
    }

    protected virtual void Update()
    {
        checkDistanceToTarget();
        if(distanceToTarget > detectionDistance)
        {
            roamWorldRandomly();
        }
        else
        {
            chasePlayer();
        }
    }

    protected void OnCollisionStay2D(Collision2D other) {
        setRandomRoamDestination();
    }

    protected void checkDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
    }

    protected void roamWorldRandomly()
    {
        transform.position = Vector2.MoveTowards(transform.position, randomRoamDestination, (movementSpeed/2) * Time.deltaTime); 
        if(Vector2.Distance(transform.position, randomRoamDestination) < 0.5)
        {
            setRandomRoamDestination();
        }
    }

    protected void setRandomRoamDestination()
    {
        if(territorialRoaming)
        {
            randomRoamDestination = new Vector2(
                Random.Range(originalPosition.x - maximumRoamingDistance, originalPosition.x + maximumRoamingDistance),
                Random.Range(originalPosition.y - maximumRoamingDistance, originalPosition.y + maximumRoamingDistance)
            );
        }
        else
        {
            randomRoamDestination = (new Vector2(transform.position.x, transform.position.y))
                + (new Vector2(Random.Range(-maximumRoamingDistance, maximumRoamingDistance),
                Random.Range(-maximumRoamingDistance, maximumRoamingDistance))
            );
        }
    }

    protected void chasePlayer()
    {
        if(Vector2.Distance(transform.position, target.transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, target.transform.position) < stoppingDistance - 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -movementSpeed * Time.deltaTime);
        }
    }

    private IEnumerator animateTakeDamage(float time)
    {
        Color currentColour = gameObject.GetComponent<SpriteRenderer>().color;
        currentColour.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = currentColour;
        yield return new WaitForSeconds(time);
        currentColour.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = currentColour;
    }
}