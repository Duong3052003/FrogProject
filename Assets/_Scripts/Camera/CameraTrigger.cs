using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CameraTrigger : MonoBehaviour
{
    public CustomInspectorObj customInspectorObj;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (customInspectorObj.panCameraOnContact)
            {
                Camera_Manager.instance.PanCameraOnContact(customInspectorObj.panDistance, customInspectorObj.panTime, customInspectorObj.PanDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - col.bounds.center).normalized;

            if(customInspectorObj.swapCamera && customInspectorObj.cameraOnLeft !=null && customInspectorObj.cameraOnRight != null)
            {
                Camera_Manager.instance.SwapCamera(customInspectorObj.cameraOnLeft,customInspectorObj.cameraOnRight,exitDirection);
            }

            if (customInspectorObj.panCameraOnContact)
            {
                Camera_Manager.instance.PanCameraOnContact(customInspectorObj.panDistance, customInspectorObj.panTime, customInspectorObj.PanDirection, true);
            }
            
        }
    }
}

[System.Serializable]
public class CustomInspectorObj
{
    public bool swapCamera = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection PanDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraTrigger))]
public class MyScriptEditor : Editor
{
    CameraTrigger cameraTrigger;

    private void OnEnable()
    {
        cameraTrigger = (CameraTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (cameraTrigger.customInspectorObj.swapCamera)
        {
            cameraTrigger.customInspectorObj.cameraOnLeft = EditorGUILayout.ObjectField("Camera on left",cameraTrigger.customInspectorObj.cameraOnLeft,
                typeof(CinemachineVirtualCamera),true) as CinemachineVirtualCamera;

            cameraTrigger.customInspectorObj.cameraOnRight = EditorGUILayout.ObjectField("Camera on right", cameraTrigger.customInspectorObj.cameraOnRight,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if (cameraTrigger.customInspectorObj.panCameraOnContact)
        {
            cameraTrigger.customInspectorObj.PanDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
                cameraTrigger.customInspectorObj.PanDirection);

            cameraTrigger.customInspectorObj.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraTrigger.customInspectorObj.panDistance);
            cameraTrigger.customInspectorObj.panTime = EditorGUILayout.FloatField("Pan Time", cameraTrigger.customInspectorObj.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraTrigger);
        }
    }
}
#endif