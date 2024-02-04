using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int point = 1;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            FruifCollect.Instance.Collect(point);
            animator.SetTrigger("Collect");
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
