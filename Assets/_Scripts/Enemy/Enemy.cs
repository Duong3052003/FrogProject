using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 knockBack;

    public GameObject PointA;
    public GameObject PointB;
    public GameObject Enemy_top;
    private Transform current_Point;
    private bool canMove = true;

    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D Collider;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        current_Point = PointA.transform;
        
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            Enemy_top.SetActive(false);
            canMove = false;
            rb.bodyType = RigidbodyType2D.Static;
            Collider.enabled = false;
            animator.SetTrigger("Death");
        }
    }

    private void SetActiveEnemy()
    {
        Enemy_top.SetActive(true);
        canMove = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Collider.enabled = true;
    }


    private void Move()
    {
        if (current_Point == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Mathf.Abs(transform.position.x - current_Point.position.x) < 0.5f && current_Point == PointB.transform)
        {
            Flip();
            current_Point = PointA.transform;
        }
        if (Mathf.Abs(transform.position.x - current_Point.position.x) < 0.5f && current_Point == PointA.transform)
        {
            Flip();
            current_Point = PointB.transform;
        }

        animator.SetFloat("xVelocity",speed);
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
    private void Dead()
    {
        gameObject.SetActive(false);
        SetActiveEnemy();
    }
}
