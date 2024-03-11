using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Auto_Move
{
    [SerializeField] private LayerMask CanStand;

    private Player player;
    private Rigidbody2D rigidbodyPlayer;

    public bool canMove= true;

    protected override void FixedUpdate()
    {
        if (canMove == true)
        {
            base.FixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnTriggerExit2D(Collider2D collision)
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
