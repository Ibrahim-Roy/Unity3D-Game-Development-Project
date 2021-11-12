using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public float damage;
    public float damageDelayTime;

    private Coroutine attackCoroutine;
    private bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            if(isColliding) return;
            isColliding = true;
            attackCoroutine = StartCoroutine(attack(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(!isColliding) return;
            isColliding = false;
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
