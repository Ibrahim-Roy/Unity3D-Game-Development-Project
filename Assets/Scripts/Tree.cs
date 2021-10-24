using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject logPrefab ;
    public int health ;
    public Animator anim ;

    public void Shake() {
        anim.SetTrigger("Shake") ;
    }

    public void takeDamage() {
        Shake() ;
        health-- ;
        if (health<=0) {
            Instantiate(logPrefab, transform.position, transform.rotation);
            Destroy(gameObject) ;
        }
    }
}
