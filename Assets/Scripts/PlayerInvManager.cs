using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvManager : MonoBehaviour
{
   //inventory work
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    // private void Awake(){
    //     inventory = new Inventory();
    //     uiInventory.SetInventory(inventory);

    //     ItemWorld.SpawnItemWorld(new Vector3(3f, 0.2f), new Item {itemType = Item.ItemType.Sword, amount = 1});
    //     ItemWorld.SpawnItemWorld(new Vector3(-3f, 0.2f), new Item {itemType = Item.ItemType.Bow, amount = 1});
    //     ItemWorld.SpawnItemWorld(new Vector3(0f, 0.2f), new Item {itemType = Item.ItemType.Coin, amount = 1});
    // }

    private void OnTriggerEnter(Collider Collider){
        ItemWorld itemWorld = GetComponent<Collider>().GetComponent<ItemWorld>();
        if(itemWorld != null){
            //if touching item
            Debug.Log("TRIGGER");
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
