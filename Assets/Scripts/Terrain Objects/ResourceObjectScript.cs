using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObjectScript : MonoBehaviour
{
    /*
        This class represents all objects in the game that the player can harvest
        from to gather materials.

        It can be attached to objects with different sprites, for each object
        the strength of the object and the prefab for the material the object
        provides will need to be assigned via the inspector in the relevant public
        variables. The object also has a float variable called lifecycle which represents
        in seconds how long the object will remain destroyed before regrowing after being
        harvested.

        The script also has a public Animator variable in case the object has idle
        animations. The object can also have animations for when it is idle, ready too be
        harvested and when it has been harvested. Make a trigger called Hit to animate when
        the object is hit, and a boolean called Harvested to animate object before and after
        it is harvested for resources.

        takeDamage() method can be called from player to decrease health 
    */

    public int totalHealth;
    public GameObject resourceMaterialPrefab;
    public float lifeCycle;
    public Animator anim;

    public void takeDamage()
    {
        if(currentHealth>0)
        {
            if(anim != null)
            {
                Shake();
            }  
            currentHealth--;
        }
        else if(!destroyed)
        {
            destroyed = true;
            if(anim != null)
            {
                anim.SetBool("Harvested", true);
            }
            GameObject item = Instantiate(resourceMaterialPrefab, transform.position, transform.rotation);
            item.transform.position = Vector2.MoveTowards(item.transform.position, player.transform.position, 2.0f);
            Invoke("growBack", lifeCycle);
        }
    }

    //Boolean variable that represents if the object has been destroyed by the player
    private bool destroyed = false;
    private int currentHealth;
    private GameObject player;

    private void Start() {
        currentHealth = totalHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Shake()
    {
        anim.SetTrigger("Hit");
    }

    private void growBack(){
        destroyed = false;
        currentHealth = totalHealth;
        if(anim != null)
        {
            anim.SetBool("Harvested", false);
        }
    }
}