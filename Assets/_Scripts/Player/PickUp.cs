using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private GameObject CloneBox;
    [SerializeField] private float rayDistance;
    [SerializeField] LayerMask layerMask;

    private GameObject grabedOject;
    private float forceThrow=22f;
    private float Direction;
    public bool grabbing { get; private set; }

    private Player_Ctrl player_Ctrl;

    private void Awake()
    {
        player_Ctrl=GetComponent<Player_Ctrl>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, transform.right , rayDistance, layerMask);
        Grab(hit);
        CheckGrabed();
    }


    private void Grab(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 13 || hit.collider.gameObject.layer == 14)
            {
                if (grabedOject == null)
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        grabbing = true;
                        grabedOject = hit.collider.gameObject;
                        grabedOject.transform.parent = grabPoint;
                    }
                }
                else if (grabedOject != null)
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        Physics2D.IgnoreCollision(CloneBox.GetComponent<BoxCollider2D>(), grabedOject.GetComponent<BoxCollider2D>(), false);
                        grabedOject.transform.parent = null;
                        grabbing = false;
                        if (player_Ctrl.Player.rightCheck == true)
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
                        Physics2D.IgnoreCollision(CloneBox.GetComponent<BoxCollider2D>(), grabedOject.GetComponent<BoxCollider2D>(), false);
                        grabedOject.transform.parent = null;
                        grabedOject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        grabbing = false;
                        grabedOject = null;
                    }
                }
            }
            else
            {
                if (grabedOject == null)
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        hit.collider.gameObject.GetComponent<ButtonPlatform>().BeTrigger();
                    }
                }
            }

        }
    }

    private void CheckGrabed()
    {
        if (grabPoint.childCount > 1)
        {
            if (grabbing == true)
            {
                Physics2D.IgnoreCollision(CloneBox.GetComponent<BoxCollider2D>(), grabedOject.GetComponent<BoxCollider2D>());
                CloneBox.GetComponent<BoxCollider2D>().isTrigger = false;
                grabedOject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabedOject.transform.position = grabPoint.position;
                grabedOject.tag = "Attack";
                player_Ctrl.Player.jumpPower = 12f;
                player_Ctrl.Player.wallJump = false;
                player_Ctrl.Player.wallSlide = false;
            }
        }

        if (grabedOject.IsDestroyed())
        {
            grabbing = false;
        }

        if (grabbing == false)
        {
            CloneBox.GetComponent<BoxCollider2D>().isTrigger = true;
            player_Ctrl.Player.jumpPower = 20f;
            player_Ctrl.Player.wallJump = true;
            player_Ctrl.Player.wallSlide = true;
        }
    }
}
