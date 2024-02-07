using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Rotate_Around : MonoBehaviour
{
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    [SerializeField] protected bool canRotate;
    [SerializeField] private float speedRotate;

    [SerializeField] private GameObject rotate_Around;

    [SerializeField] private bool DrawGizomos;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Move()
    {
        if (canMove)
        this.transform.RotateAround(rotate_Around.transform.position, Vector3.forward, speed * Time.deltaTime);
    }

    protected void Rotate()
    {
        if(canRotate)
        {
            this.transform.Rotate(Vector3.forward, speedRotate * Time.deltaTime);
        }
        else
        {
            this.transform.Rotate(Vector3.forward, -35 * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (rotate_Around != null && DrawGizomos == true)
        {
            Gizmos.DrawSphere(rotate_Around.transform.position, radius);
        }
    }
}
