using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    [SerializeField]private float speed;

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactor;
        newPos.y -= delta.y * parallaxFactor;

        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, speed * Time.deltaTime);

    }

}
