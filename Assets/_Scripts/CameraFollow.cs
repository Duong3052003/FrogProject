using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 Camera;


    void LateUpdate()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);

        }
    }
}
