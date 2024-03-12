using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Box_CheckGround : MonoBehaviour
{
    [SerializeField] private GameObject Box;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = Box.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Box != null)
        {
            transform.position = Box.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            //Box.GetComponent<Box>().isGround = true;
        }

    }
}
