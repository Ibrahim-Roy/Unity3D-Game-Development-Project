// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     public Vector2 moveValue ;
//     public float speed ;
//     public Animator anim ;
//     private GameObject collisionObject ;
//     [SerializeField] private LayerMask groundMask ;
//     public GameObject aimCursor ;
//     private string weaponMode ;
//     private Vector3 movementDirection ;
//     private Vector3 mousePosition ;
//     public GameObject rangedWeaponAmmo ;

//     void OnMove(InputValue value) {
//         moveValue = value.Get<Vector2>() ;
//     }

//     void Update() {
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
//         if (Physics.Raycast(ray, out RaycastHit raycastHit, groundMask)) {
//             mousePosition = raycastHit.point ;
//         }
//         movementDirection = (mousePosition - transform.position).normalized ;
//         movementDirection.y = 0f;
//         anim.SetFloat("Horizontal", movementDirection.x) ;
//         anim.SetFloat("Vertical", movementDirection.z) ;
//         if (Input.GetKeyDown(KeyCode.E)) {
//             if (collisionObject != null){
//                 if (collisionObject.tag == "Tree") {
//                     collisionObject.transform.parent.gameObject.GetComponent<Tree>().takeDamage() ;
//                 }
//             }
//         }
//         else if (Input.GetMouseButtonDown(0)) {
//             anim.SetTrigger("Attack") ;
//         }
//         //Temporary until the inventroy and equipping slots are implemented
//         else if (Input.GetKeyDown(KeyCode.Tab)) {
//             anim.SetBool("Ranged Equipped", false) ;
//             anim.SetBool("Melee Equipped", false) ;
//             weaponMode = "None" ;
//             aimCursor.SetActive(false);
//         }
//         else if (Input.GetKeyDown(KeyCode.Alpha1)) {
//             anim.SetBool("Ranged Equipped", false) ;
//             anim.SetBool("Melee Equipped", true) ;
//             weaponMode = "Melee" ;
//             aimCursor.SetActive(false);
//         }
//         else if (Input.GetKeyDown(KeyCode.Alpha2)) {
//             anim.SetBool("Melee Equipped", false) ;
//             anim.SetBool("Ranged Equipped", true) ;
//             weaponMode = "Ranged";
//             aimCursor.SetActive(true);
//         }
//         if(weaponMode == "Ranged"){
//             AimRangedWeapon() ;
//             ShootRangedWeapon() ;
//         }
//     }

//     void FixedUpdate() {
//         GetComponent <Rigidbody>().velocity = new Vector3(moveValue.x*speed , 0.0f , moveValue.y*speed) ;
//     }

//     private void OnTriggerEnter(Collider collider){
//         ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
//         if(itemWorld != null){
//             //if touching item
//             Debug.Log("TRIGGER");
//             inventory.AddItem(itemWorld.GetItem());
//             itemWorld.DestroySelf();
//         }
//     }

//     void OnTriggerExit(Collider other) {
//         collisionObject = null ;
//     }

//     void AimRangedWeapon() {
//         Vector3 mousePositionToAimAt = mousePosition ;
//         mousePositionToAimAt.y = 0.085f ;
//         aimCursor.transform.position =  mousePositionToAimAt ;
//     }

//     void ShootRangedWeapon() {
//         if(Input.GetMouseButtonUp(0)) {
//             GameObject arrow = Instantiate(rangedWeaponAmmo, transform.position, Quaternion.identity) ;
//             arrow.GetComponent<Rigidbody>().velocity = movementDirection * 2.0f;
//             arrow.transform.Rotate(0, 0, Mathf.Atan2(-movementDirection.x, movementDirection.z) * Mathf.Rad2Deg) ;
//             Destroy(arrow, 0.5f) ;
//         }
//     }

//     //inventory work
//     private Inventory inventory;
//     [SerializeField] private UI_Inventory uiInventory;

//     private void Awake(){
//         inventory = new Inventory();
//         uiInventory.SetInventory(inventory);

//         ItemWorld.SpawnItemWorld(new Vector3(3f, 0.2f), new Item {itemType = Item.ItemType.Sword, amount = 1});
//         ItemWorld.SpawnItemWorld(new Vector3(-3f, 0.2f), new Item {itemType = Item.ItemType.Bow, amount = 1});
//         ItemWorld.SpawnItemWorld(new Vector3(0f, 0.2f), new Item {itemType = Item.ItemType.Coin, amount = 1});
//     }

//     // private void OnCollisionEnter(Collider Collider){
//     //     ItemWorld itemWorld = GetComponent<Collider>().GetComponent<ItemWorld>();
//     //     if(itemWorld != null){
//     //         //if touching item
//     //         inventory.AddItem(itemWorld.GetItem());
//     //         itemWorld.DestroySelf();
//     //     }
//     // }
// }