using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject Box_Break;
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

    void TakeDamaged()
    {
        hp = hp - 1;
        if(hp <= 0)
        {
            Box_Break.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            Animator.SetTrigger("Hited");
        }
    }
}
