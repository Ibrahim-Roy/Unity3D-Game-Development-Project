using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerScript : MonoBehaviour
{
    public Animator anim;
    public GameObject rangedWeaponAmmunitionPrefab;

    private float movementSpeed = 7.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rbody;
    private GameObject collisionSourceObject;
    private int currentCombatMode = 0;
    private int health = 10;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
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
                if(collisionSourceObject.tag == "Tree"){
                    collisionSourceObject.GetComponent<treeScript>().takeDamage();
                }
                if(collisionSourceObject.tag == "ironRock"){
                    collisionSourceObject.GetComponent<ironoreScript>().takeDamage();
                }
                if(collisionSourceObject.tag == "mineable"){
                    collisionSourceObject.GetComponent<ironoreScript>().takeDamage();
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
        if(currentCombatMode == 1){
            Vector3 weaponBarrelPosition = ((transform.GetChild(0).gameObject).transform.GetChild(0).gameObject).transform.position;
            GameObject arrow = Instantiate(rangedWeaponAmmunitionPrefab, weaponBarrelPosition, Quaternion.identity);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            Vector3 shootingDirection = (mouseWorldPosition - transform.position).normalized;
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 15.0f;
            arrow.transform.Rotate(0f, 0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
        }
    }

    public void takeDamage(int damage){
        health -= damage;
        if(health==0)
        {
            Debug.Log("Game Over");
            //Reload last save
        }
    }
}