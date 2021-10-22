using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 
    //setting player's speed, this can be adjusted to suit gameplay
    public float moveSpeed;
    //makes reference to the rigidbody of the player we are moving
    public Rigidbody rb;

    //variables that take input in Update()
    private float inputX;
    private float inputY;

    // Update is called once per frame, (may change to FixedUpdate later to have update based on time rather than FPS)
    void Update()
    {
        //changes the directional movement of the player
        rb.velocity = new Vector3(inputX * moveSpeed, 0.0f, inputY * moveSpeed);
    }   
    //Input functionality
    public void Move(InputAction.CallbackContext context){
        inputX  = context.ReadValue<Vector2>().x;
        inputY  = context.ReadValue<Vector2>().y;
    }
}
