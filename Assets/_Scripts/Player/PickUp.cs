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
    private float forceThrow=22f;
    private float Direction;
    private bool grabbing;

    private Player_Ctrl player_Ctrl;

    private void Awake()
    {
        player_Ctrl=GetComponent<Player_Ctrl>();
    }

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
                    grabedOject.transform.parent = grabPoint; 
                }
            }
            else if (grabedOject != null)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    grabedOject.transform.parent = null;
                    grabbing = false;
                    if (Player.rightCheck == true)
                    {
                        Direction = 1;
                    }
                    else Direction = -1;
                    grabedOject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    grabedOject.GetComponent<Rigidbody2D>().velocity = new Vector2(Direction, 0.3f) * forceThrow;
                    grabedOject = null;
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    grabedOject.transform.parent = null;
                    grabedOject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    grabbing = false;
                    grabedOject = null;
                }
            }
        }
        if(grabPoint.childCount > 0)
        {
            if (grabbing == true)
            {
                grabedOject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabedOject.transform.position = grabPoint.position;
                grabedOject.tag = "Attack";
                player_Ctrl.Player.jumpPower = 10f;
                player_Ctrl.Player.wallJump = false;
                player_Ctrl.Player.wallSlide = false;
            }
        }

        if (grabedOject.IsDestroyed())
        {
           grabbing=false;
        }
        if (grabbing == false)
        {
            player_Ctrl.Player.jumpPower = 15f;
            player_Ctrl.Player.wallJump = true;
            player_Ctrl.Player.wallSlide = true;
        }
    
    }
}
