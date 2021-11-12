using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public float healthValue = 0f;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            GameObject.FindGameObjectWithTag("UI").GetComponent<UserInterface>().PickupItem(gameObject.tag);
            if(gameObject.tag == "Food Item")
            {
                other.gameObject.GetComponent<Player>().incrementHealth(healthValue);
            }
            Destroy(gameObject);
        }
    }
}
