using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Check : MonoBehaviour
{
    public static bool returnNow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            returnNow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            returnNow=true;
        }
    }
}
