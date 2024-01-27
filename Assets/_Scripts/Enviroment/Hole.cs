using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform response;
    [SerializeField] private bool onlyPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.position = response.position;
        }
        
        if (collision.gameObject.layer == 14 && onlyPlayer==false)
        {
            collision.gameObject.transform.position = response.position;
        }
    }
}
