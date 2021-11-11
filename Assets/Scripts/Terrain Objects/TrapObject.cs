using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public float damage;
    public float damageDelayTime;

    private Coroutine attackCoroutine;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            attackCoroutine = StartCoroutine(attack(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            StopCoroutine(attackCoroutine);   
        }
    }

    private IEnumerator attack(GameObject player)
    {
        while(true)
        {
            yield return new WaitForSeconds(damageDelayTime);
            player.GetComponent<Player>().takeDamage(damage);
        }
    }
}
