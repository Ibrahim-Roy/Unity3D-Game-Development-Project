using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Hostile") {
            //Destroy(other.transform.parent.gameObject) ;
        }
    }
}
