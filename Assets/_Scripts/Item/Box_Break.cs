using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Break : MonoBehaviour
{
    private Vector2 forceDirecton;
    private float torque;
    private Rigidbody2D rb;

    void Start()
    {
        float randTorque = UnityEngine.Random.Range(-50, 50);
        float randForceX = UnityEngine.Random.Range(-50, 50);
        float randForceY = UnityEngine.Random.Range(-50, 50);
        
        forceDirecton.x = randForceX;
        forceDirecton.y = randForceY;
        torque = randTorque;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirecton);
        rb.AddTorque(torque);
        Invoke(nameof(DestroyPiece), 3f);
    }

    private void DestroyPiece()
    {
        Destroy(gameObject);
    }

}
