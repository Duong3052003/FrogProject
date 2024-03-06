using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [NonSerialized] public int IsGround;
    [SerializeField] private LayerMask groundLayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            IsGround = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGround = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy_top")|| collision.gameObject.tag.Equals("BOSS"))
        {
            IsGround = 2;
        }
    }
}
