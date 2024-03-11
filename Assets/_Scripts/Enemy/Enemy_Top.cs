using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Top : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    private Enemy_Walk enemyWalk;
    private MASKMAN maskMan;
    private bool isBeingDead=false;
    [SerializeField] private bool isBoss=false;

    private void OnEnable()
    {
        isBeingDead = false;
    }

    void Start()
    {
        if (isBoss == false)
        {
            enemyWalk = Enemy.GetComponent<Enemy_Walk>();
        }
        else
        {
            maskMan = Enemy.GetComponent<MASKMAN>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") && collision.gameObject.layer == 23 && isBeingDead==false)
        {
            if (!isBoss)
            {
                isBeingDead = true;
                enemyWalk.BeingDead();
            }
            else
            {
                maskMan.TakeDamage(); 
            }
            
        }
    }
}
