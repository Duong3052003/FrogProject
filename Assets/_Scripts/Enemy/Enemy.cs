using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected BoxCollider2D Collider;
    protected Rigidbody2D rb;

    [SerializeField] protected LayerMask layerCanHurting;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        Collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

}
