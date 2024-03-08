using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, SaveGameObj
{
    [SerializeField] private string id;
    [ContextMenu("Generate guild for id")]
    private void GenerateGuild()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private int point = 1;
    private bool collected=false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !collected)
        {
            FruifCollect.Instance.Collect(point);
            animator.SetTrigger("Collect");
        }
    }

    private void Collected()
    {
        collected=true;
        gameObject.SetActive(false);
    }

    public void SaveData(ref SaveDataGame data)
    {
        if (data.fruitCollected.ContainsKey(id))
        {
            data.fruitCollected.Remove(id);
        }
        data.fruitCollected.Add(id, collected);
    }

    public void LoadData(SaveDataGame data)
    {
        data.fruitCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }
}
