using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector2 oldPosition;

    void Start()
    {
        oldPosition = new Vector2(transform.position.x,transform.position.y);
    }

    void Update()
    {
        if (transform.position.x != oldPosition.x || transform.position.y != oldPosition.y)
        {
            if (onCameraTranslate != null)
            {
                Vector2 delta = oldPosition - new Vector2(transform.position.x, transform.position.y);
                onCameraTranslate(delta);
            }

            oldPosition = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
