using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue ;
    public float speed ;
    public Animator anim ;
    private GameObject collisionObject ;
    [SerializeField] private LayerMask groundMask ;

    void OnMove(InputValue value) {
        moveValue = value.Get<Vector2>() ;
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
            Vector3 mousePosition = Vector3.zero ;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, groundMask)) {
                mousePosition = raycastHit.point ;
            }
            Vector3 attackDirection = (mousePosition - transform.position).normalized ;
            attackDirection.y = 0f;
            anim.SetFloat("Horizontal", attackDirection.x) ;
            anim.SetFloat("Vertical", attackDirection.z) ;
        if (Input.GetKeyDown(KeyCode.E)) {
            if (collisionObject != null){
                if (collisionObject.tag == "Tree") {
                    collisionObject.transform.parent.gameObject.GetComponent<Tree>().takeDamage() ;
                }
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("Attack") ;
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