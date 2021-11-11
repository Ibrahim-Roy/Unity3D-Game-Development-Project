using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAmmo : MonoBehaviour
{
    public float damage;
    public string targetTag;
    public string sourceTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == targetTag)
        {
            if(other.gameObject.tag == "HostileNPC")
            {
                other.gameObject.GetComponent<HostileNPC>().takeDamage(damage);
            }
            else if(other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().takeDamage(damage);
            }
        }
        
        if(other.gameObject.tag != "Flat" && other.gameObject.tag != sourceTag)
        {
            Destroy(gameObject);
        }    
    }
}
