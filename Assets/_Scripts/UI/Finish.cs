using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, SaveGameObj
{
    [SerializeField] private string id;
    [ContextMenu("Generate guild for id")]
    private void GenerateGuild()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private bool collected = false;
    private int unlockedLevel;

    [SerializeField] private AudioClip winSoundEffect;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(winSoundEffect);
            animator.SetTrigger("Finish");
            collected = true;
        }
    }

    public void SaveData(ref SaveDataGame data)
    {
        if (data.finishsCollected.ContainsKey(id))
        {
            data.finishsCollected.Remove(id);
        }
        data.finishsCollected.Add(id, collected);
        data.unlockedLevels = this.unlockedLevel;
    }

    public void LoadData(SaveDataGame data)
    {
        data.finishsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            this.unlockedLevel = data.unlockedLevels;
        }
        else
        {
            this.unlockedLevel = data.unlockedLevels+1;
        }
    }
}
