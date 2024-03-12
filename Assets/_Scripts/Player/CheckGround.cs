using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [NonSerialized] public int IsGround;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] ParticleSystem MovementParticle;
    [SerializeField] ParticleSystem FallParticle;
    [SerializeField] ParticleSystem TouchParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity = 4;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod = 0.08f;

    [SerializeField] Rigidbody2D playerRb;

    float counter;

    private void Start()
    {
        TouchParticle.transform.parent = null;
    }

    void Update()
    {
        counter += Time.deltaTime;

        if (IsGround == 1 && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                FallParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayTouchParticle(Vector2 pos)
    {
        TouchParticle.transform.position = pos;
        TouchParticle.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            IsGround = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGround = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy_top")|| collision.gameObject.tag.Equals("BOSS"))
        {
            IsGround = 2;
        }

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            FallParticle.Play();
        }
    }
}
