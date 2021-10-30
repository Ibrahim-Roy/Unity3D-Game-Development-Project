using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue ;
    public float speed ;
    private GameObject collisionObject ;

    void OnMove(InputValue value) {
        moveValue = value.Get<Vector2>() ;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (collisionObject != null){
                if (collisionObject.tag == "Tree") {
                    collisionObject.transform.parent.gameObject.GetComponent<Tree>().takeDamage();
                }
            }
        }
    }

    void FixedUpdate() {
        GetComponent <Rigidbody>().velocity = new Vector3(moveValue.x*speed , 0.0f , moveValue.y*speed) ;
    }

    void OnTriggerEnter(Collider other) {
        collisionObject = other.GetComponent<Collider>().gameObject ;
    }

    void OnTriggerExit(Collider other) {
        collisionObject = null ;
    }
}