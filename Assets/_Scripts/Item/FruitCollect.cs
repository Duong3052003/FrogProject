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

    private int point_total = 0;

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

    private void Start()
    {
        text.text= "Point: "+ point_total;
    }

    public void Collect(int point)
    {
        SoundManager.Instance.PlaySound(collectSoundEffect);
        point_total = point_total+ point;
        text.text= "Point: " + point_total;
    }

    public void SaveData(ref SaveDataGame data)
    {
        data.point_total = this.point_total;
    }

    public void LoadData(SaveDataGame data)
    {
        this.point_total = data.point_total;
    }
}
