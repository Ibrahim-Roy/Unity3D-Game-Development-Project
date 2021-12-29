using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType{
        Sword,
        Apple,
        Wood,
        Stone,
    }

    public ItemType itemType;
    public int amount;
    
}
