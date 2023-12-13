using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float knocback;

    public Rigidbody2D rb;

    [SerializeField] private bool gravity_Changed;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity=transform.right*speed;
    }

    private void Update()
    {
        Invoke("_Destroy", 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gravity_Changed==true) 
        {
            Invoke("GravityChange", 0.2f);
        }
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.rigidbody.AddForce(Vector2.left * knocback);
        }
    }


    private void _Destroy()
    {
        Destroy(gameObject);
    }

    private void GravityChange()
    {
        rb.gravityScale = 2;
    }
}
