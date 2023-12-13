using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform GunTip;
    [SerializeField] private int ammo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo >= 1)
            {
                Fire();
                ammo = ammo - 1;
            }
            
        }
        
    }

    private void Fire()
    {
        Instantiate(Bullet, GunTip.position,GunTip.rotation);
    }
    
}
