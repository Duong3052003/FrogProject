using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour
{
    private BoxCollider2D col;
    private BoxCollider2D colChild;
    private PickUp PickUp;

    public Bounds Bounds { get { return m_collider.bounds; } }
    BoxCollider2D m_collider;

    private void Awake()
    {
        PickUp = GetComponentInParent<PickUp>();
        col = GetComponent<BoxCollider2D>();
        colChild = GetComponentInChildren<BoxCollider2D>();
        colChild.isTrigger = false;
    }

    private void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_collider.isTrigger = true;

        Bounds bounds = m_collider.bounds;

        Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D child in childColliders)
        {
            bounds.Encapsulate(child.bounds);
        }

        m_collider.size = bounds.size;
        m_collider.offset = bounds.center - m_collider.transform.position;
    }

    private void Update()
    {
        //if (PickUp.grabbing==true && colChild == null)
        //{
        //    colChild = GetComponentInChildren<BoxCollider2D>();
        //    Debug.Log(2);
        //}
        //if (PickUp.grabbing == false)
        //{
        //    colChild = null;
        //    Debug.Log(3);
        //}

        //if (colChild != null)
        //{
        //    Debug.Log(1);
            
        //}
    }
}
