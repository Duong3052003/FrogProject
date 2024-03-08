using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruifCollect : MonoBehaviour, SaveGameObj
{
    private static FruifCollect instance;
    public static FruifCollect Instance => instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioClip collectSoundEffect;

    [SerializeField] private int point_total = 0;
    private int point_collected = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        text.text= "Point: "+ point_collected + "/" + point_total;
    }

    public void Collect(int point)
    {
        SoundManager.Instance.PlaySound(collectSoundEffect);
        point_collected = point_collected + point;
    }

    public void SaveData(ref SaveDataGame data)
    {
        //data.point_total = this.point_collected;
    }

    public void LoadData(SaveDataGame data)
    {
        //this.point_collected = data.point_total;
        foreach (KeyValuePair<string, bool> pair in data.fruitCollected)
        {
            if (pair.Value)
            {
                point_collected++;
            }
        }
    }
}
