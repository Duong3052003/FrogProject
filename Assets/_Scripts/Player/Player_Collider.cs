using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Collider : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 direction;

    [SerializeField] private LayerMask layerCollider;

    private Player_Ctrl player_ctrl;
    private UIManager uiManager;
    [SerializeField] private AudioSource dieSoundEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player_ctrl= GetComponent<Player_Ctrl>();
        uiManager=FindAnyObjectByType<UIManager>();
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
        if (((1 << collision.gameObject.layer) & layerCollider) != 0)
        {
            this.player_ctrl.player_TakeDamage.TakeDamage();
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                TakeKnockBack();
                StartCoroutine(GetHurt());
            }
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
        animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        animator.SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(3, 11,false);
        Physics2D.IgnoreLayerCollision(3, 15,false);
        Physics2D.IgnoreLayerCollision(3, 16,false);

    }

    public void BeingDead()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(0, 0);
        animator.SetTrigger("Die");
        dieSoundEffect.Play();

    }

    private void Dead()
    {
        Destroy(gameObject);
        uiManager.GameOver();
    }

    private void Revive()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(0, 1);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
