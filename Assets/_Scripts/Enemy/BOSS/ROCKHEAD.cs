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

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(checkUp.position, checkRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(checkDown.position, checkRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(checkWall.position, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkUp.position, checkRadius);
        Gizmos.DrawWireSphere(checkDown.position, checkRadius);
        Gizmos.DrawWireSphere(checkWall.position, checkRadius);
    }
}
