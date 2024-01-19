using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject Box_Break;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
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
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Breaked();
        }
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            StartCoroutine(tagBox());
        }
    }

    private IEnumerator tagBox()
    {
        yield return new WaitForSeconds(1);
        gameObject.tag = "Box";
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
