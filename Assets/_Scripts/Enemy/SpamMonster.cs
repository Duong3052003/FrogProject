using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamMonster : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private float time_revive=10f;
    [SerializeField] private bool canReponse;

    void Update()
    {
        CallSpam();
    }

    void CallSpam()
    {
        if (enemy.gameObject.activeSelf== false)
        {
            if (canReponse == true)
            {
                Invoke(nameof(Spam), time_revive);
            }
            else
            {
                Destroy(gameObject);
            }
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
