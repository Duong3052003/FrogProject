using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Collider : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 direction;
    private bool isWin;
    private bool isBeingDamaged=false;
    private Transform CheckPoint;

    [SerializeField] private LayerMask layerCollider;

    private Player_Ctrl player_ctrl;
    private UIManager uiManager;
    [SerializeField] private AudioClip dieSoundEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player_ctrl= GetComponent<Player_Ctrl>();
        uiManager=FindAnyObjectByType<UIManager>();
        isWin = false;
    }

    private void Start()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("Revive");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = (transform.position- collision.collider.transform.position).normalized;
        if (((1 << collision.gameObject.layer) & layerCollider) != 0 && isBeingDamaged == false)
        {
            isBeingDamaged = true;
            this.player_ctrl.player_TakeDamage.TakeDamage();
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                TakeKnockBack();
                StartCoroutine(GetHurt());
            }
        }
        if (collision.gameObject.layer == 17 && isBeingDamaged == false)
        {
            isBeingDamaged = true;
            if (CheckPoint.position != null)
            {
                transform.position = CheckPoint.position;
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            Win();
        }
        if (collision.CompareTag("CheckPoint"))
        {
            CheckPoint = collision.gameObject.transform;
            collision.GetComponent<Collider2D>().enabled = false;
        }

        if (collision.gameObject.layer == 22 && isBeingDamaged == false)
        {
            isBeingDamaged=true;
            direction = (transform.position - collision.GetComponent<Collider2D>().bounds.center).normalized;
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                StartCoroutine(GetHurt());
                TakeKnockBack();
            }
            this.player_ctrl.player_TakeDamage.TakeDamage();
        }

    }

    private void TakeKnockBack()
    {
        this.player_ctrl.knockBack.CallKnockBack(direction, Vector2.up, Input.GetAxisRaw("Horizontal"));
    }

    private IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(3, 11);
        Physics2D.IgnoreLayerCollision(3, 15);
        Physics2D.IgnoreLayerCollision(3, 16);
        Physics2D.IgnoreLayerCollision(3, 22);
        Physics2D.IgnoreLayerCollision(3, 24);

        Physics2D.IgnoreLayerCollision(23, 11);
        Physics2D.IgnoreLayerCollision(23, 15);
        Physics2D.IgnoreLayerCollision(23, 16);
        Physics2D.IgnoreLayerCollision(23, 21);
        Physics2D.IgnoreLayerCollision(23, 22);
        Physics2D.IgnoreLayerCollision(23, 24);
        animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        animator.SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(3, 11,false);
        Physics2D.IgnoreLayerCollision(3, 15,false);
        Physics2D.IgnoreLayerCollision(3, 16,false);
        Physics2D.IgnoreLayerCollision(3, 22, false);
        Physics2D.IgnoreLayerCollision(3, 24, false);

        Physics2D.IgnoreLayerCollision(23, 11, false);
        Physics2D.IgnoreLayerCollision(23, 15, false);
        Physics2D.IgnoreLayerCollision(23, 16, false);
        Physics2D.IgnoreLayerCollision(23, 21, false);
        Physics2D.IgnoreLayerCollision(23, 22, false);
        Physics2D.IgnoreLayerCollision(23, 24, false);
        isBeingDamaged= false;

    }

    public void BeingDead()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(0, 0);
        animator.SetTrigger("Die");
        SoundManager.Instance.PlaySound(dieSoundEffect);
    }

    private void Dead()
    {
        Destroy(gameObject);
        if (isWin == true)
        {
            uiManager.Win();
        }
        else
        {
            uiManager.GameOver();
        }
        
    }

    private void Revive()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(0, 1);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Win()
    {
        BeingDead();
        isWin = true;
    }
}
