using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 Camera;


    void LateUpdate()
    {
        transform.position = new Vector3 (player.position.x,player.position.y,-10);

        //transform.LookAt(player);
    }
}
