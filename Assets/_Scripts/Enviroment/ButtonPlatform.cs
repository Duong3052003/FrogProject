using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private GameObject Platform;
    [SerializeField] private LayerMask LayerCanTrigger;
    [SerializeField] private GameObject MapTrigger;

    private Animator animator;

    [SerializeField] private bool Button;
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
        if (Button == true)
        {
            if (((1 << collision.gameObject.layer) & LayerCanTrigger) != 0)
            {
                onCollider += 1;
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Button == true)
        {
            if (((1 << collision.gameObject.layer) & LayerCanTrigger) != 0 && Platform.GetComponent<Platform>().canMove == false)
            {
                trigger = true;
                animator.SetBool("Trigger", trigger);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Button == true)
        {
            if (((1 << collision.gameObject.layer) & LayerCanTrigger) != 0)
            {
                onCollider -= 1;
            }

            if (onCollider <= 0)
            {
                trigger = false;
                animator.SetBool("Trigger", trigger);
            }
        }
    }

    public void BeTrigger()
    {
        if (Button == false)
        {
            trigger = !trigger;

            if(trigger == true)
            {
                animator.SetFloat("Trigger", 1);
                Trigger();
            }
            else
            {
                animator.SetFloat("Trigger", 0);
                CancelTrigger();
            }
            
        }
    }

    private void Trigger()
    {
        Platform.GetComponent<Platform>().canMove = true;
        if(MapTrigger != null)
        {
            MapTrigger.SetActive(false);
        }
    }

    private void CancelTrigger()
    {
        Platform.GetComponent<Platform>().canMove = false;
        if (MapTrigger != null)
        {
            MapTrigger.SetActive(true);
        }
    }
}
