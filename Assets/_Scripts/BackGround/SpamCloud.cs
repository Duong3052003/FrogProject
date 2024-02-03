using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamCloud : MonoBehaviour
{
    private GameObject Cloud;
    [SerializeField] private float countDown;
    [SerializeField] private GameObject[] clouds;
    private Vector3 location;
    private float _elapsedTime = 0f;

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;
        if (_elapsedTime > countDown)
        {
            Cloud = clouds[Random.Range(0, clouds.Length)];

            location.x = transform.position.x;
            location.y = transform.position.y + Random.Range(-15, 15);
            location.z = transform.position.z;

            Fire();
            _elapsedTime = 0f;
        }
    }

    private void Fire()
    {
        Instantiate(Cloud, location, transform.rotation);
    }
}
