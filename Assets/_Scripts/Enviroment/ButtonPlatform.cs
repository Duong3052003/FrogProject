using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private GameObject Platform;
    [SerializeField] private LayerMask LayerCanTrigger;

    private Animator animator;

    private bool trigger;
    private int onCollider=0;

    private void Start()
    {
        Platform.GetComponent<Platform>().canMove = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1 << collision.gameObject.layer) & LayerCanTrigger) != 0)
        {
            onCollider += 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & LayerCanTrigger) != 0 && Platform.GetComponent<Platform>().canMove == false)
        {
            trigger = true;
            animator.SetBool("Trigger", trigger);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & LayerCanTrigger) != 0)
        {
            onCollider -= 1;
        }
        
        if (onCollider <=0)
        {
            trigger=false;
            animator.SetBool("Trigger", trigger);
        }
    }

    private void Trigger()
    {
        Platform.GetComponent<Platform>().canMove = true;
    }

    private void CancelTrigger()
    {
        Platform.GetComponent<Platform>().canMove = false;
    }
}
