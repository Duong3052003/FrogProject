using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Break : MonoBehaviour
{
    private Vector2 forceDirecton;
    private float torque;
    private Rigidbody2D rb;
    [SerializeField] private float range;
    [SerializeField] private GameObject Container_Box;

    void Start()
    {
        float randTorque = UnityEngine.Random.Range(-range, range);
        float randForceX = UnityEngine.Random.Range(-range, range);
        float randForceY = UnityEngine.Random.Range(-range, range);
        
        forceDirecton.x = randForceX;
        forceDirecton.y = randForceY;
        torque = randTorque;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forceDirecton);
        rb.AddTorque(torque);
        Invoke(nameof(DestroyPiece), 5f);
    }

    private void DestroyPiece()
    {
        if(Container_Box != null)
        {
            Destroy(Container_Box);
        }
    }

}
