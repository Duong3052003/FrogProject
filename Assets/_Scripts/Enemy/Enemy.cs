using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 5f;

    [SerializeField] private Vector2 knockBack;

    [SerializeField] protected GameObject PointA;
    [SerializeField] protected GameObject PointB;

    [SerializeField] protected float distancePointA;
    [SerializeField] protected float distancePointB;

    [SerializeField] private GameObject Enemy_top;
    [SerializeField] private Transform RayCastPoint;
    [SerializeField] private float rayDistance;

    [SerializeField] private LayerMask targetLayer;

    private bool chase;
    private bool returnn;

    private Coroutine Coroutine_StopTarget;

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
        PointA.transform.position=new Vector2(transform.position.x+distancePointA,transform.position.y);
        PointB.transform.position=new Vector2(transform.position.x+distancePointB,transform.position.y);

        Coroutine_StopTarget = StartCoroutine(StopTarget());
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayCastPoint.position, transform.right*-1, rayDistance);
        Debug.DrawRay(transform.position, transform.right*rayDistance*-1, Color.red);
        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & targetLayer) != 0)
            {
                chase = true;
                canMove = true;
                StopCoroutine(Coroutine_StopTarget);

                if (transform.rotation== Quaternion.Euler(0, 0, 0))
                {
                    PointA.transform.position= hit.transform.position;
                    current_Point = PointA.transform;
                }
                if (transform.rotation == Quaternion.Euler(0, -180, 0))
                {
                    PointB.transform.position = hit.transform.position;
                    current_Point = PointB.transform;
                }
            }
        }

        Chase();
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            Move();
        }
    }

    private IEnumerator StopTarget()
    {
        canMove = false;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            Flip();
            PointA.transform.position = new Vector2(transform.parent.position.x + distancePointA, transform.position.y);
            PointB.transform.position = new Vector2(transform.parent.position.x + distancePointB, transform.position.y);
            if (Mathf.Abs(PointA.transform.position.x - transform.position.x) > Mathf.Abs(PointB.transform.position.x - transform.position.x))
            {
                current_Point = PointA.transform;
                
            }
            else
            {
                current_Point = PointB.transform;
                
            }
            if (i == 2)
            {
                if (current_Point == PointA.transform && transform.rotation == Quaternion.Euler(0,180, 0) || current_Point == PointB.transform && transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    Flip();
                }
            }
        }
        yield return new WaitForSeconds(1);

        chase = false;
        canMove = true;
        returnn = true;
    }

    private void Chase()
    {
        if (chase == true)
        {
            returnn = false;
            speed = 7f;

        }
        if (chase == false && returnn == true)
        {
            StopCoroutine(Coroutine_StopTarget);
            speed = 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") && collision.gameObject.layer==3)
        {
            Enemy_top.SetActive(false);
            canMove = false;
            rb.bodyType = RigidbodyType2D.Static;
            Collider.enabled = false;
            animator.SetTrigger("Death");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
        if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            rb.velocity = new Vector2(speed, 0);
        }
        if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            rb.velocity = new Vector2(speed, 0);
        }
       
        if (Mathf.Abs(transform.position.x - current_Point.position.x) < 0.5f && current_Point == PointB.transform)
        {
            if (chase == true)
            {
                Coroutine_StopTarget = StartCoroutine(StopTarget());
            }
            else
            {
                returnn = false;
                Flip();
                current_Point = PointA.transform;
            }
        }
        if (Mathf.Abs(transform.position.x - current_Point.position.x) < 0.5f && current_Point == PointA.transform)
        {
            if (chase == true)
            {
                Coroutine_StopTarget = StartCoroutine(StopTarget());
            }
            else
            {
                returnn = false;
                Flip();
                current_Point = PointB.transform;
            }

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
