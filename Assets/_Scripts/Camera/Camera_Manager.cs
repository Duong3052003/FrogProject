using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public static Camera_Manager instance;

    [SerializeField] CinemachineVirtualCamera[] _allCameras;

    private Coroutine _panCameraCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private Vector2 _startingTrackedObjOffSet;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for(int i = 0; i < _allCameras.Length; i++)
        {
            if (_allCameras[i].enabled)
            {
                _currentCamera = _allCameras[i];

                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        _startingTrackedObjOffSet = _framingTransposer.m_TrackedObjectOffset;
    }

    #region PanCamera
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera( panDistance,  panTime,  panDirection,  panToStartingPos));
    }

    public IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        if (!panToStartingPos)
        {
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up; ;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down; ;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.left; ;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.right; ;
                    break;
                default:
                    break;

            }

            endPos *= panDistance;
            startingPos = _startingTrackedObjOffSet;
            endPos += startingPos;

        }
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjOffSet;
        }

        float elapsedTime = 0f;
        while(elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            _framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }
    }
    #endregion

    #region SwapCamera
    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        if(_currentCamera==cameraFromLeft && triggerExitDirection.x > 0f)
        {
            cameraFromLeft.gameObject.SetActive(false);

            cameraFromRight.gameObject.SetActive(true);

            _currentCamera = cameraFromRight;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        }

        if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            cameraFromLeft.gameObject.SetActive(true);

            cameraFromRight.gameObject.SetActive(false);

            _currentCamera = cameraFromLeft;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        }
    }


    #endregion
}
