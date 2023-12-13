using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Collider : MonoBehaviour
{

    private static Player_Collider instance;
    public static Player_Collider Instance => instance;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Weapon_Enemy") || collision.gameObject.tag.Equals("Enemy"))
        {
            if (Player_Hp.hp <= 0)
            {
                animator.SetTrigger("Die");
                rb.bodyType = RigidbodyType2D.Static;
            }
            else if (Player_Hp.hp == 1)
            {
                Player_Hp.TakeDamage();
                animator.SetTrigger("Die");
                rb.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                Player_Hp.TakeDamage();
                StartCoroutine(GetHurt());
            }

        }
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
}
