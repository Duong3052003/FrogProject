using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Top : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    private Enemy_Walk enemyWalk;

    void Start()
    {
        enemyWalk = Enemy.GetComponent<Enemy_Walk>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") && collision.gameObject.layer == 23)
        {
            enemyWalk.BeingDead();
        }
    }
}
