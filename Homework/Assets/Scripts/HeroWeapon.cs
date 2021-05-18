using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapon : MonoBehaviour
{

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bullet;

    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Fire();
        }
    }

    private void Fire() {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
