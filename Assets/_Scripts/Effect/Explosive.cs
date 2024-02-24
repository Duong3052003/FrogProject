using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private bool lockY;

    [SerializeField] private ParticleSystem[] explosives;
    [SerializeField] private GameObject damage;
    [SerializeField] private AudioClip explosivesEffect;

    private void Update()
    {
        if (lockY == true)
        {
            transform.position = new Vector3(Target.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        }
    }

    public void Explosivee()
    {
        damage.gameObject.SetActive(true);
        SoundManager.Instance.PlaySound(explosivesEffect);

        for (int i = 0; i < explosives.Length; i++)
        {
            explosives[i].Play();
        }
        Invoke("CancelExplosive", 0.1f);
    }

    private void CancelExplosive()
    {
        damage.gameObject.SetActive(false);
        CancelInvoke();
    }
}
