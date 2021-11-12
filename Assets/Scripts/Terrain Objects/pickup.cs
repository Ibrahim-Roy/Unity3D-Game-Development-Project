using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>().PickupItem();
            Destroy(GameObject.FindGameObjectWithTag("Log"));
        }
    }
}
