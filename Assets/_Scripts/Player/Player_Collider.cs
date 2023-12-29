using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Collider : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private KnockBack knockBack;

    private Vector2 direction;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();

        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Revive");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = (transform.position- collision.collider.transform.position).normalized;
        if (collision.gameObject.tag.Equals("Weapon_Enemy") || collision.gameObject.tag.Equals("Enemy"))
        {
            Player_Hp.TakeDamage();
            
            if (Player_Hp.hp == 0)
            {
                rb.bodyType = RigidbodyType2D.Static;
                animator.SetTrigger("Die");
            }
            else
            {
                TakeDamage();
                StartCoroutine(GetHurt());
            }
            
        }
    }

    private void TakeDamage()
    {
        knockBack.CallKnockBack(direction, Vector2.up, Input.GetAxisRaw("Horizontal"));
    }

    private IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(3, 11);
        animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        animator.SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(3, 11,false);

    }

    private void Dead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Revive()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
