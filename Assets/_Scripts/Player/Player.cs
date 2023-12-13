using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    private float move;
    private bool rightCheck=true;
    private bool doubleJump;

    private bool isWallSliding;
    private bool isWallJumping;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 15f;
    private float downJumpingDuration = 0.6f;
    private bool canDown;
    private Vector2 Superjump = new Vector2(2f,18f);

    [SerializeField] private float wallSlidingSpeed = 1.5f;
    private float wallJumpingTime=0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration=0.4f;
    private Vector2 wallJumpingPower = new Vector2(10f,17f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask supergroundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private GameObject groundCheck;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            move = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("xVelocity", Mathf.Abs(move));
            animator.SetFloat("yVelocity", rb.velocity.y);
            Jump();
            WallJump();
            WallSlide();
            autoJump();
            
            if (!isWallJumping)
            {
                Flip();
            }

        }
       
    }
    private void FixedUpdate()
    {
        if (isWallJumping|| rb.bodyType != RigidbodyType2D.Static)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }
        if (Input.GetKeyDown("s") || Input.GetKey("s"))
        {
            if (canDown == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * (-1f));
                CancelInvoke(nameof(ChangedTagGroundCheck));
                groundCheck.tag="Attack";
            }
        }
    }


    private void Jump()
    {
        if (Input.GetButtonDown("Jump") )
        {
            if (IsGround()!=0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                doubleJump = true;
            }
            else if(doubleJump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.8f);
                doubleJump = false;
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;

        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter >0f && isWallSliding)
        {
            isWallJumping=true;
            rb.velocity = new Vector2(-move * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            rightCheck = !rightCheck;
            transform.Rotate(0f, 180f, 0f);
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void WallSlide()
    {
        if(IsWall() && IsGround() == 0 && move!=0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y,-wallSlidingSpeed,float.MaxValue));
            animator.SetBool("IsSliding", isWallSliding);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("IsSliding", isWallSliding);
        }
    }


    private void autoJump()
    {
        if (IsGround()==1)
        {
            OnGround();
        }
        if (IsGround()==2)
        {
            OnGround();
            if (!groundCheck.tag.Equals("Attack"))
            {
                rb.velocity = new Vector2(Superjump.x, Superjump.y);
            }
                
        }
        if (IsGround() == 0)
        {
            Invoke(nameof(CanDown), downJumpingDuration);
            animator.SetBool("IsJumping", true);
            
        }
    }

    private void OnGround()
    {
        animator.SetBool("IsJumping", false);
        canDown = false;
        Invoke(nameof(ChangedTagGroundCheck),0.2f);
        CancelInvoke(nameof(CanDown)); 
    }
    private void ChangedTagGroundCheck()
    {
        groundCheck.tag = "Untagged";
    }

    private void CanDown()
    {
        canDown = true;
    }

    private bool IsWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private int IsGround()
    {
        if(Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer))
        {
            return 1;
        }
        else if(Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, supergroundLayer))
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    private void Flip()
    {
        if (rightCheck == true && move < 0f || rightCheck == false && move > 0f)
        {
            rightCheck = !rightCheck;

            transform.Rotate(0f, 180f, 0f);
            
        }
    }

    
}
