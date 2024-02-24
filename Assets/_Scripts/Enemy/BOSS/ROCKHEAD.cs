using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROCKHEAD : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;

    [Header("AttackUp&Down")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;

    [Header("AttackPlayer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform playerPos;

    [Header("Other")]
    [SerializeField] Transform checkUp;
    [SerializeField] Transform checkDown;
    [SerializeField] Transform checkWall;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundLayer;

    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    private bool isFacingLeft=true;
    [SerializeField] private float timeMin;
    [SerializeField] private float timeMax;
    private float time=0f;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        rb = GetComponent<Rigidbody2D>();
        ChangedAttack();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(checkUp.position, checkRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(checkDown.position, checkRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(checkWall.position, checkRadius, groundLayer);
    }

    private void ChangedAttack()
    {
        int ranged = Random.Range(0, 2);

        if (ranged == 0)
        {
            AttackUpNDown();
        }
        else
        {
            AttackUpNDown();
        }
    }

    #region AttackUP&Down
    private void AttackUpNDown()
    {
        if (time <= 0)
        {
            time = Random.Range(timeMin, timeMax);
        }

        if(isTouchingUp && goingUp)
        {
            ChangedDirection();
        }
        else if(isTouchingDown && !goingUp)
        {
            ChangedDirection();
        }else if (isTouchingWall)
        {
            Flip();
        }

        if(time > 0)
        {
            rb.velocity = attackMoveSpeed * attackMoveDirection;
            time -= Time.deltaTime;
        }
        
    }

    private void ChangedDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *=-1;
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        idleMoveDirection.x *=-1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkUp.position, checkRadius);
        Gizmos.DrawWireSphere(checkDown.position, checkRadius);
        Gizmos.DrawWireSphere(checkWall.position, checkRadius);
    }
}
