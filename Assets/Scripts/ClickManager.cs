using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
            RaycastHit hit ;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.tag == "Tree") {
                    hit.collider.gameObject.transform.parent.gameObject.GetComponent<Tree>().takeDamage();
                }
            }
        }
    }
}
