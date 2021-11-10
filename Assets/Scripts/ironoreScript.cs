using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironoreScript : MonoBehaviour
{
    public int health = 3;
    public GameObject ironPrefab;
    public Animator anim;

    public void takeDamage()
    {
        Shake();
        health--;
        if(health<=0)
        {
            //anim.SetBool("Chopped", true);
            Instantiate(ironPrefab, transform.position, transform.rotation);
            Invoke("sproutStump", 10.0f);
        }
    }

    private void Shake()
    {
        //anim.SetTrigger("Hit");
    }

    private void sproutStump(){
        health = 3;
        //anim.SetBool("Chopped", false);
    }
}