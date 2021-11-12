using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileNPCRanged : HostileNPC
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
         if(distanceToTarget < (stoppingDistance * 2))
         {
             StartCoroutine(attack());
         }
    }

    private IEnumerator attack()
    {
        if (isCoroutineExecuting) yield break;
        isCoroutineExecuting = true;
        ((transform.GetChild(0).gameObject).transform.GetChild(0).gameObject).GetComponent<RangedWeapon>().shoot(target.transform.position, "Player");
        yield return new WaitForSeconds(attackDelayTime);
        isCoroutineExecuting = false;
    }
}
