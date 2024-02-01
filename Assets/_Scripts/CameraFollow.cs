using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera cnm;

    private void Awake()
    {
        cnm.GetComponent<CinemachineVirtualCamera>();
    }

    void LateUpdate()
    {
        cnm.m_Lens.OrthographicSize = 10;   
    }
}
