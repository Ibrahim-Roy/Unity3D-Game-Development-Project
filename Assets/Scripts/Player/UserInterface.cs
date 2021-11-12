using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Text HealthOutput;
    public Text LogOutput;
    public Text StoneOutput;

    public Text Death;

    float health;
    public int log = 0;
    int stone = 0;


    void Update()
    {
        deathDetect();
        HealthOutput.text = "Health: " + health;
        LogOutput.text = "Logs collected: " + log;
        StoneOutput.text = "Stones collected: " + stone;
    }

    public void deathDetect()
    {
        if (health <= 0){
             Death.text = "YOU DIED";
        }
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }

    public void SetLog(int _log)
    {
        log = _log;
    }

    public void SetStone(int _stone)
    {
        stone = _stone;
    }

    //incrementing item when picked up
    public void PickupItem(){
        log++;
    }

}
