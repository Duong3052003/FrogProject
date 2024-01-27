using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SpikeHead_Awakup : MonoBehaviour
{
    [SerializeField] private LayerMask layerCheck;
    [SerializeField] private GameObject spikeHead;
    public static Transform Player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(((1 << collision.gameObject.layer) & layerCheck) != 0)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb.velocity.x > 5.5f || rb.velocity.y > 2f)
                {
                    spikeHead.GetComponent<SpikeHead>().isAwakup = true;
                }
                if (collision.gameObject.CompareTag("Player") && spikeHead.GetComponent<SpikeHead>().isAwakup == true)
                {
                    Player = collision.transform;
                }
            }     
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && spikeHead.GetComponent<SpikeHead>().isAwakup == true)
        {
            spikeHead.GetComponent<SpikeHead>().isAwakup = false;
        }
    }
}
