using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue ;
    public float speed ;

    void OnMove(InputValue value) {
        moveValue = value.Get<Vector2>() ;
    }

    void FixedUpdate () {
        GetComponent <Rigidbody>().velocity = new Vector3(moveValue.x*speed , 0.0f , moveValue.y*speed) ;
    }
}