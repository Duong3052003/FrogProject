using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private float knockBackTime = 0.2f;
    private float direction = 5f;
    private float constForce = 5f;
    private float inputForce = 7.5f;

    public bool IsBeingKnockBack { get; private set; }

    private Rigidbody2D rb;

    private Coroutine knockbackCoroutine;

    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();    
    }

    public IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        IsBeingKnockBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockBackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection *direction;
        _constantForce = constantForceDirection *constForce;

        float _elapsedTime = 0f;
        while(_elapsedTime < knockBackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;
            _knockBackForce= _hitForce + _constantForce;

            if(inputDirection != 0)
            {
                _combinedForce= _knockBackForce + new Vector2(inputDirection *inputForce,0f);
            }
            else
            {
                _combinedForce = _knockBackForce;
            }
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = _combinedForce;
            }

            yield return new WaitForFixedUpdate();
        }
        IsBeingKnockBack = false;
    }

    public void CallKnockBack(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(KnockBackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
