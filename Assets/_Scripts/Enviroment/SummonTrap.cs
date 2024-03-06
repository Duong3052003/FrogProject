using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTrap : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;

    private void OnEnable()
    {
        Fire();
    }

    private void Fire()
    {
        Instantiate(Bullet, transform.position, transform.rotation);
    }
}
