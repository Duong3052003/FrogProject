using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Move : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject PointA;
    [SerializeField] private GameObject PointB;

    [SerializeField] private int direction = 1;

    [SerializeField] private LayerMask CanStand;

    private Player player;
    private Rigidbody2D rigidbodyPlayer;
    private Vector2 forcePush;

    void Update()
    {
        Vector2 target = currentTarget();

        transform.position = Vector2.Lerp(transform.position, target, speed*Time.deltaTime);
        float distance = (target-(Vector2)transform.position).magnitude;

        if(distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentTarget()
    {
        if (direction == 1)
        {
            return PointA.transform.position;
        }
        else
        {
            return PointB.transform.position;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            forcePush = new Vector2(rigidbodyPlayer.velocity.x+10f, rigidbodyPlayer.velocity.y+10f);
            rigidbodyPlayer.AddForce(forcePush);
            Debug.Log(forcePush);
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
            rigidbodyPlayer.AddForce(forcePush);
        }
    }

    private void OnDrawGizmos()
    {
        if ( PointA != null && PointB != null)
        {
            Gizmos.DrawLine(transform.position,PointA.transform.position);
            Gizmos.DrawLine(transform.position,PointB.transform.position);
        }
    }
}
