using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1 : Auto_Rotate_Around
{
    [SerializeField] private LayerMask CanStand;

    private Player player;
    private Rigidbody2D rigidbodyPlayer;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canRotate==false)
        {
            if (((1 << collision.gameObject.layer) & CanStand) != 0)
            {
                collision.transform.SetParent(this.transform);
            }
            if (collision.CompareTag("Player"))
            {
                collision.transform.SetParent(this.transform);
                rigidbodyPlayer = collision.GetComponent<Rigidbody2D>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canRotate == false)
        {
            if (((1 << collision.gameObject.layer) & CanStand) != 0)
            {
                collision.transform.SetParent(null);
            }
            if (collision.CompareTag("Player"))
            {
                collision.transform.SetParent(null);
                rigidbodyPlayer = collision.GetComponent<Rigidbody2D>();
            }
        }
    }
}
