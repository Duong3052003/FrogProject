using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSaw : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Rigidbody2D rb;
    private Collider2D collider2d;

    private Animator animator;

    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 3 || collision.gameObject.tag.Equals("Attack"))
        {
            collider2d.enabled = false;
            animator.SetTrigger("Break");
        }
    }


    private void _Destroy()
    {
        Destroy(gameObject);
    }

}
