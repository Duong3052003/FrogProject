using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpikeHead : Enemy
{
    [SerializeField] private GameObject Check;
    [SerializeField] private LayerMask layerCheck;
    [SerializeField] private LayerMask layerWall;
    [SerializeField] private ParticleSystem particleDead=null;

    public static Transform currentTarget;

    [SerializeField] private float speed;
    public static bool isAwakup;
    private bool isChasing=true;

    private Vector3 direction;

    void Update()
    {
        Chase();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") || ((1 << collision.gameObject.layer) & layerCanHurting) != 0)
        {
            BeingDead();
        }
        if (((1 << collision.gameObject.layer) & layerWall) != 0)
        {
            animator.SetTrigger("Hited");
        }
    }

    private void Chase()
    {
        if (isAwakup == true)
        {
            if (SpikeHead_Awakup.Player != null)
            {
                currentTarget = SpikeHead_Awakup.Player;
                animator.SetTrigger("Awakup");

            }
            if (currentTarget != null)
            {
                if (isChasing == false)
                {
                    direction = currentTarget.position - transform.position;
                    rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
                    isChasing = true;
                }
            }
        }
    }

    private void Stop()
    {
        isChasing = false;
    }

    private void BeingDead()
    {
        particleDead.transform.position=transform.position;
        particleDead.Play();
        GameObject.Destroy(gameObject);
    }

}
