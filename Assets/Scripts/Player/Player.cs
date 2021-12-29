using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator anim;

    public void takeDamage(float damage){
        if(health > 1)
        {
            health--;
            GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>().SetHealth(health);
            StartCoroutine(animateTakeDamage(0.2f));

        }
        else
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>().SetHealth(0);
            StartCoroutine(reloadWithDelay(2));
        }
    }
    
    public void incrementHealth(float addHealth){
        health += addHealth;
        GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>().SetHealth(health);

    }

    private float movementSpeed = 7.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rbody;
    private GameObject collisionSourceObject;
    private int currentCombatMode = 0;
    private float health = 10;
    private Inventory inventory;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>().SetHealth(health);
        rbody = GetComponent<Rigidbody2D>();

        inventory = new Inventory();
    }


   // Update is called once per frame
    private void Update()
    {
        playerAnimationHandler();
        playerInputHandler();
    }

    private void FixedUpdate()
    {
        playerMovementHandler();
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        collisionSourceObject = other.gameObject;
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        collisionSourceObject = null;   
    }

    private void playerMovementHandler()
    {
        rbody.velocity = new Vector2(horizontalInput, verticalInput)  * movementSpeed;
        if(movementSpeed == 150.0f)
        {
            movementSpeed = 5.0f;
        }
    }

    private void playerAnimationHandler()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 mouseDirection = (mouseWorldPosition - transform.position).normalized;
        anim.SetFloat("Horizontal", mouseDirection.x);
        anim.SetFloat("Vertical", mouseDirection.y);
    }

    private void playerInputHandler()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.E))
        {
            playerInteractionEventHandler();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeCombatMode(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeCombatMode(2);
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            changeCombatMode(0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            combatHandler();
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 150.0f;
        }

    }

    private void playerInteractionEventHandler()
    {
        if(collisionSourceObject != null)
            {
                if(collisionSourceObject.tag == "ResourceObject"){
                    collisionSourceObject.GetComponent<ResourceObjectScript>().takeDamage();
                }
            }
    }

    private void changeCombatMode(int newCombatMode)
    {
        anim.SetInteger("Combat Mode", newCombatMode);
        currentCombatMode = newCombatMode;
    }

    private void combatHandler()
    {
        anim.SetTrigger("Attack");
        //Ranged
        if(currentCombatMode == 1)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ((transform.GetChild(0).gameObject).transform.GetChild(0).gameObject).GetComponent<RangedWeapon>().shoot(mouseWorldPosition, "HostileNPC");
        }
        
    }

    private IEnumerator animateTakeDamage(float time)
    {
        Color playerColour = gameObject.GetComponent<SpriteRenderer>().color;
        playerColour.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = playerColour;
        yield return new WaitForSeconds(time);
        playerColour.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = playerColour;
    }

    private IEnumerator reloadWithDelay(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainMenu");//CHANGE THIS TO TAKE TO MAIN MENU LATER
    }
}