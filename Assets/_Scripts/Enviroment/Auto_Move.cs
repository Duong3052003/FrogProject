using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Auto_Move : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject PointA;
    [SerializeField] private GameObject PointB;

    [SerializeField] private int direction = 1;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    Vector2 currentTarget()
    {
        if (direction == 1)
        {
            return PointA.transform.position;
        }
        else
        {
            return PointB.transform.position;
        }
    }

    protected void Move()
    {
        Vector2 target = currentTarget();

        transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);
        float distance = (target - (Vector2)transform.position).magnitude;
        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        if ( PointA != null && PointB != null)
        {
            Gizmos.DrawLine(transform.position,PointA.transform.position);
            Gizmos.DrawLine(transform.position,PointB.transform.position);
        }
    }
}
