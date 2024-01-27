using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private GameObject Platform;

    private Animator animator;

    private bool trigger;

    private void Start()
    {
        Platform.GetComponent<Platform>().canMove = !Platform.GetComponent<Platform>().canMove;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            trigger = true;
            animator.SetBool("Trigger", trigger);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        animator.SetBool("Trigger", trigger);
    }

    private void Trigger()
    {
        Platform.GetComponent<Platform>().canMove = !Platform.GetComponent<Platform>().canMove;
    }
}
