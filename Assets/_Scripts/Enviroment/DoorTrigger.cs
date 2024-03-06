using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private static DoorTrigger Instance;
    public static DoorTrigger instance => Instance;

    [SerializeField] private GameObject[] Doors;
    private Collider2D col;
    [SerializeField] private bool TriggerFromLeft=true;
    [SerializeField] private AudioClip trigger;
    [SerializeField] private AudioClip cancelTrigger;
    [SerializeField] private AudioClip BGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - col.bounds.center).normalized;

            if (TriggerFromLeft == true && exitDirection.x > 0f)
            {  
                for (int i = 0; i < Doors.Length; i++)
                {
                    Doors[i].gameObject.SetActive(true);
                }
                SoundManager.Instance.PlaySound(trigger);
            }
            if (BGM != null)
            {
                SoundManager.Instance.ChangedBGM(BGM);
            }
        }
    }

    public void CancelTrigger()
    {
      for (int i = 0; i < Doors.Length; i++)
          {
            if (Doors[i] != null)
            {
                Doors[i].gameObject.SetActive(false);
            }
          }
        SoundManager.Instance.PlaySound(cancelTrigger);
        if (BGM != null)
        {
            SoundManager.Instance.ReturnBGM();
        }
    }
}
