using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawmRock : MonoBehaviour
{
    private Collider2D spawnRockCollider;
    [SerializeField] private GameObject rockPrefab;

    private void Awake()
    {
        spawnRockCollider = GetComponent<Collider2D>();  
    }

    private void OnEnable()
    {
        SpawmRocks();
        gameObject.SetActive(false);
    }


    private void SpawmRocks()
    {
        var randomX = Random.Range(spawnRockCollider.bounds.min.x, spawnRockCollider.bounds.max.x);
        var rock = Object.Instantiate(rockPrefab, new Vector3(randomX, spawnRockCollider.bounds.min.y), Quaternion.identity);
    }
}
