using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] LayerMask layerMask;

    private GameObject grabedOject;
    private float forceThrow=15f;
    private float Direction;
    private bool grabbing;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, transform.right , rayDistance, layerMask);
        if (hit.collider != null)
        {
            if (grabedOject == null)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    grabbing = true;
                    grabedOject=hit.collider.gameObject;  
                }
            }
            else if (grabedOject != null)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    grabbing = false;
                    if (Player.rightCheck == true)
                    {
                        Direction = 1;
                    }
                    else Direction = -1;
                    grabedOject.GetComponent<Rigidbody2D>().velocity = new Vector2(Direction, 0.25f) * forceThrow;
                    grabedOject = null;
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    grabbing = false;
                    grabedOject = null;
                }
            }
        }

        if (grabedOject.IsDestroyed())
        {
            Player.jumpPower = 15f;
            Player.wallJump = true;
            Player.wallSlide = true;
        }

        if (grabbing == true)
        {
            grabedOject.transform.position = grabPoint.position;
            grabedOject.tag = "Attack";
            Player.jumpPower = 10f;
            Player.wallJump = false;
            Player.wallSlide = false;
        }
        if (grabbing == false)
        {
            Player.jumpPower = 15f;
            Player.wallJump = true;
            Player.wallSlide = true;
        }
    
    }
}
