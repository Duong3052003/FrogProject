using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject Box_Break;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Animator Animator;

    private int hp = 2;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Box_Break.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            TakeDamaged();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer))
        {
            gameObject.tag = "Box";
        }
        if(collision.gameObject.tag.Equals("Weapon_Enemy") || collision.gameObject.tag.Equals("Enemy"))
        {
            Breaked();
        }
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
            Animator.SetTrigger("Hited");
        }
    }
    void Breaked()
    {
        Box_Break.SetActive(true);
        Destroy(gameObject);
    }
}
