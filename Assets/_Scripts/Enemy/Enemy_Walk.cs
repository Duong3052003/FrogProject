using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Walk : Enemy
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private GameObject PointA;
    [SerializeField] private GameObject PointB;

    [SerializeField] private float distancePointA;
    [SerializeField] private float distancePointB;

    [SerializeField] private GameObject Enemy_top;
    [SerializeField] private Transform RayCastPoint;
    [SerializeField] private float rayDistance;

    [SerializeField] private LayerMask targetLayer;

    private bool chase;
    private bool returnn;

    private Coroutine Coroutine_StopTarget;

    private Transform current_Point;
    private bool canMove = true;
    
    private Enemy_Check enemy_check;

    protected override void Awake()
    {
        base.Awake();
        enemy_check = GetComponentInChildren<Enemy_Check>();
    }

    private void Start()
    {
        Coroutine_StopTarget = StartCoroutine(StopTarget());
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayCastPoint.position, transform.right * -1, rayDistance,targetLayer);
        Debug.DrawRay(transform.position, transform.right * rayDistance * -1, Color.red);
        returnNow();

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer==3)
            {
                chase = true;
                canMove = true;
                StopCoroutine(Coroutine_StopTarget);

                if (transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    PointA.transform.position = hit.transform.position;
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
        animator.SetBool("CanMove", canMove);
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
            ResetPoint();

            if (i == 2)
            {
                if (current_Point == PointA.transform && transform.rotation == Quaternion.Euler(0, 180, 0) || current_Point == PointB.transform && transform.rotation == Quaternion.Euler(0, 0, 0))
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

    private void ResetPoint()
    {
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
        if (enemy_check.returnNow == true)
        {
            enemy_check.returnNow = false;
        }
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

    private void returnNow()
    {
        if (enemy_check.returnNow == true)
        {
            ResetPoint();
        }
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
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") && collision.gameObject.layer == 0)
        {
            BeingDead();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") || ((1 << collision.gameObject.layer) & layerCanHurting) != 0)
        {
            BeingDead();
        }
    }

    private void SetActiveEnemy()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Enemy_top.SetActive(true);
        canMove = true;
        Collider.enabled = true;
    }
    private void BeingDead()
    {
        canMove = false;
        Enemy_top.SetActive(false);
        Collider.enabled = false;
        animator.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
    }
    private void Dead()
    {
        gameObject.SetActive(false);
        SetActiveEnemy();
    }

}
