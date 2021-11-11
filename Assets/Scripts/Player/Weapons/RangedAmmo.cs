using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAmmo : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "HostileNPC")
        {
            other.gameObject.GetComponent<HostileNPC>().takeDamage(damage);
        }
        if(other.gameObject.tag != "Flat")
        {
            Destroy(gameObject);
        }    
    }
}
