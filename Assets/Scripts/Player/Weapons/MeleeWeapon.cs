using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "HostileNPC")
        {
            other.gameObject.GetComponent<HostileNPC>().takeDamage(damage);
        }
    }
}
