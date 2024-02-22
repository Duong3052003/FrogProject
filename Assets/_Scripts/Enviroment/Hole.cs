using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform response;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14 && response != null)
        {
            collision.gameObject.transform.position = response.position;
        }
    }
}
