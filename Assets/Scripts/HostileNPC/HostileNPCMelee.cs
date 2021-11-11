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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            target.GetComponent<playerControllerScript>().takeDamage(2);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            target.GetComponent<playerControllerScript>().takeDamage(2);
        }
        else
        {
            setRandomRoamDestination();
        }
    }
}
