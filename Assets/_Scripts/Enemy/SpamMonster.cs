using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamMonster : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private float time_revive=10f;

    void Update()
    {
        CallSpam();
    }

    void CallSpam()
    {
        if (enemy.gameObject.activeSelf== false)
        {
            Invoke(nameof(Spam), time_revive);
        }
        else
        {
            CancelInvoke(nameof(Spam));
        }
    }
    void Spam()
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }
}
