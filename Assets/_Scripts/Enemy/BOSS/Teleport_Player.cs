using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Player : MonoBehaviour
{
    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;
    [SerializeField] private Transform Player;
    private float midpoint;

    private void Start()
    {
        midpoint = (PointA.position.x + PointB.position.x)/2;
    }

    public void Teleport()
    {
        if (Player.position.x - midpoint < 0)
        {
            transform.position = new Vector2( PointA.position.x, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(PointB.position.x, transform.position.y);
        }
    }
}
