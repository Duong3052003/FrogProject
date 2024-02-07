using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform ObjTrigger;
    [SerializeField] private Vector3 PositionChanged;

    private void Awake()
    {
        transform.localPosition= Vector3.zero;
    }

    private void Update()
    {
        if(ObjTrigger != null)
        {
            if (transform.position.x > ObjTrigger.position.x)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, PositionChanged, 1 * Time.deltaTime); 
            }
        }
        
    }
}
