using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject Box_Break;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip hitedSoundEffect;
    private Rigidbody2D rb;

    private Animator Animator;

    private int hp = 2;

    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Box_Break.transform.position = transform.position;
        if (rb.bodyType == RigidbodyType2D.Static)
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack") && collision.gameObject.layer!=0)
        {
            TakeDamaged();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            StopAllCoroutines();
            gameObject.tag = "Box";
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Breaked();
        }
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            SoundManager.Instance.PlaySound(hitedSoundEffect);
            StartCoroutine(tagBox());
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 14 && collision.gameObject.layer == 3) && rb.bodyType == RigidbodyType2D.Static)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            StopAllCoroutines();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopAllCoroutines();

        if ((collision.gameObject.layer == 14 && collision.gameObject.layer == 3) && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            StartCoroutine(tagBox());
        }
    }

    private IEnumerator tagBox()
    {
        yield return new WaitForSeconds(1);
        gameObject.tag = "Box";
        rb.bodyType= RigidbodyType2D.Static;

    }

    void TakeDamaged()
    {
        hp = hp - 1;
        if(hp <= 0)
        {
            Breaked();
        }
        else
        {
            SoundManager.Instance.PlaySound(hitedSoundEffect);
            Animator.SetTrigger("Hited");
        }
    }
    void Breaked()
    {
        Box_Break.SetActive(true);
        Destroy(gameObject);
    }
}
