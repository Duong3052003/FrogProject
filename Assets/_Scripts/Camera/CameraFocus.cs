using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private Transform playerTransform;

    [SerializeField] private float flipRotationTime=0.5f;

    private Coroutine turnCoutine;
    private Player _Player;

    private bool rightCheck;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _Player = playerTransform.gameObject.GetComponent<Player>();
        rightCheck = _Player.rightCheck;
    }

    private void Update()
    {
       if(_Player != null)
       transform.position= playerTransform.position;
        
    }

    public void CallTurn()
    {
        turnCoutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation,endRotation,(elapsedTime /flipRotationTime));
            transform.rotation = Quaternion.Euler(0f,yRotation,0f);
        }
        yield return null;
    }

    private float DetermineEndRotation()
    {
        rightCheck = !rightCheck;
        if (rightCheck == true)
        {
            return 0;
        }
        else 
        {
            return -180;
        }
    }
}
