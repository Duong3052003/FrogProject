using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    private float move;
    [NonSerialized] public bool rightCheck=true;
    private int direction=1;
    private bool doubleJump;
    [NonSerialized] public bool wallJump=true;
    [NonSerialized] public bool wallSlide=true;

    private bool isWallSliding;
    private bool isWallJumping;
    private bool isAttackJumping;

    [NonSerialized] public float speed = 5f;
    [NonSerialized] public float jumpPower = 10f;
    private float downJumpingDuration = 0.6f;
    private bool canDown;
    [NonSerialized] public bool canJump=true;
    private Vector2 Superjump = new Vector2(2f,14f);

    [SerializeField] private float wallSlidingSpeed = 1.5f;
    private float wallJumpingTime=0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration=0.5f;
    private Vector2 wallJumpingPower = new Vector2(10f,19f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform behindCheck;
    [SerializeField] private GameObject _GroundCheck;
    private CheckGround groundCheck;

    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private AudioClip jumpSoundEffect;
    
    private GameObject _CameraFocus;
    private CameraFocus cameraFocus;

    private Player_Ctrl player_Ctrl;

    private void Awake()
    {
        player_Ctrl = GetComponent<Player_Ctrl>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = _GroundCheck.GetComponent<CheckGround>();
    }

    private void Start()
    {
        _CameraFocus = GameObject.FindGameObjectWithTag("CameraFocus");
        cameraFocus = _CameraFocus.GetComponent<CameraFocus>();

    }

    void Update()
    {
        if (!player_Ctrl.knockBack.IsBeingKnockBack || rb.bodyType != RigidbodyType2D.Static)
        {
            move = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("xVelocity", Mathf.Abs(move));
            animator.SetFloat("yVelocity", rb.velocity.y);
            if (!isWallJumping)
            {
                if ((rightCheck == true && move < 0f || rightCheck == false && move > 0f) && !CanReturn())
                {
                    Flip();
                }
            }
            Jump();
            WallJump();
            WallSlide();
            autoJump();
        }
    }
    private void FixedUpdate()
    {
        if (!player_Ctrl.knockBack.IsBeingKnockBack && rb.bodyType != RigidbodyType2D.Static)
        {
            Move();
        }
    }

    #region Move
    private void Move()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }
        if (Input.GetKeyDown("s") || Input.GetKey("s"))
        {
            if (canDown == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * (-1f));
            }
        }
        
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump==true)
        {
            if (groundCheck.IsGround != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                doubleJump = true;
                SoundManager.Instance.PlaySound(jumpSoundEffect);
            }
            else if(doubleJump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.8f);
                doubleJump = false;
                SoundManager.Instance.PlaySound(jumpSoundEffect);
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0 && canJump == true)
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

        if (Input.GetButtonDown("Jump") && wallJumpingCounter >0f && isWallSliding && wallJump == true)
        {
            SoundManager.Instance.PlaySound(jumpSoundEffect);
            isWallJumping = true;
            rb.velocity = new Vector2(-direction *wallJumpingPower.x, wallJumpingPower.y);
            Flip();
            wallJumpingCounter = 0f;
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void WallSlide()
    {
        if(IsWall() && groundCheck.IsGround == 0 && wallSlide==true && (move != 0|| isWallSliding == true|| isWallJumping == true))
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
        if (groundCheck.IsGround == 0)
        {
            Invoke(nameof(CanDown), downJumpingDuration);
            animator.SetBool("IsJumping", true);
            isAttackJumping=false;
        }

        if (groundCheck.IsGround == 1)
        {
            OnGround();
        }

        if (groundCheck.IsGround == 2 && isAttackJumping==false)
        {
            isAttackJumping = true;
            rb.velocity = new Vector2(move * Superjump.x, Superjump.y);
            canDown = false;
            CancelInvoke();
            SoundManager.Instance.PlaySound(jumpSoundEffect);
        }

    }
    private void CanDown()
    {
        canDown = true;
    }
    #endregion

    #region check ground/wall
    private void OnGround()
    {
        animator.SetBool("IsJumping", false);
        canDown = false;
        CancelInvoke();
    }

    private bool IsWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private bool CanReturn()
    {
        if(player_Ctrl.PickUp.grabbing == true)
        {
            return Physics2D.OverlapCircle(behindCheck.position, 0.5f, groundLayer);
        }
        return false;
    }

    #endregion

    #region Flip
    private void Flip()
    {
        rightCheck = !rightCheck;

        if (rightCheck == true)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            direction = 1;
            cameraFocus.CallTurn();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            direction = -1;
            cameraFocus.CallTurn();
        }
        
    }
    #endregion

}
