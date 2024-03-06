using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncWall : MonoBehaviour
{
    [Header("Infor")]
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 moveDirection;

    [Header("Other")]
    [SerializeField] protected Transform checkUp;
    [SerializeField] protected Transform checkDown;
    [SerializeField] protected Transform checkWall;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundLayer;

    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    public bool isFacingLeft = true;

    private Rigidbody2D rb;

    void Start()
    {
        moveDirection.Normalize();
        rb = GetComponent<Rigidbody2D>();
        checkDirection();
    }

    private void Update()
    {
        if (checkUp != null)
        {
            isTouchingUp = Physics2D.OverlapCircle(checkUp.position, checkRadius, groundLayer);
        }
        if (checkDown != null)
        {
            isTouchingDown = Physics2D.OverlapCircle(checkDown.position, checkRadius, groundLayer);
        }
        if (checkWall != null)
        {
            isTouchingWall = Physics2D.OverlapCircle(checkWall.position, checkRadius, groundLayer);
        }
    }

    public void InteractWall()
    {
        if (isTouchingUp && goingUp && checkUp != null)
        {
            ChangedDirection();
        }
        else if (isTouchingDown && !goingUp && checkDown != null)
        {
            ChangedDirection();
        }
        else if (isTouchingWall && checkWall != null)
        {
            Flip();
        }
        rb.velocity = moveSpeed * moveDirection;

    }

    private void ChangedDirection()
    {
        goingUp = !goingUp;
        moveDirection.y *= -1;
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        moveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void checkDirection()
    {
        if(isFacingLeft==true && transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (isFacingLeft == false && transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (checkUp != null)
        {
            Gizmos.DrawWireSphere(checkUp.position, checkRadius);
        }
        if (checkDown != null)
        {
            Gizmos.DrawWireSphere(checkDown.position, checkRadius);
        }
        if (checkWall != null)
        {
            Gizmos.DrawWireSphere(checkWall.position, checkRadius);
        }
    }
}
