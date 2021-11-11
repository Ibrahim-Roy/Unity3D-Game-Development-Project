using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public GameObject ammunitionPrefab;
    public float shootingSpeed;

    public void shoot()
    {
        Vector3 weaponBarrelPosition = transform.position;
        GameObject arrow = Instantiate(ammunitionPrefab, weaponBarrelPosition, ammunitionPrefab.transform.rotation);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 shootingDirection = (mouseWorldPosition - transform.position).normalized;
        arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * shootingSpeed;
        arrow.transform.Rotate(0f, 0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(arrow, 2.0f);
    }
}
