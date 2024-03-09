using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_OnWayPlatfom : MonoBehaviour
{
    private GameObject OneWayPlatform;

    private BoxCollider2D PlayerCollider2D;

    void Awake()
    {
        PlayerCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (OneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 20)
        {
            OneWayPlatform = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 20)
        {
            OneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D PlatformCollider2D= OneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(PlayerCollider2D, PlatformCollider2D);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(PlayerCollider2D, PlatformCollider2D, false);
    }
}
