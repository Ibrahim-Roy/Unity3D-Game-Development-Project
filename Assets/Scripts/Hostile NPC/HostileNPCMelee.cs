using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileNPCMelee : HostileNPC
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            attackCoroutine = StartCoroutine(attack());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
        }
    }

    private IEnumerator attack()
    {
        if (isCoroutineExecuting) yield break;
        isCoroutineExecuting = true;
        target.GetComponent<Player>().takeDamage(2f);
        yield return new WaitForSeconds(attackDelayTime);
        isCoroutineExecuting = false;
    }
}
