using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruifCollect : MonoBehaviour
{
    private static FruifCollect instance;
    public static FruifCollect Instance => instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource collectSoundEffect;

    private int point_total = 0;

    private void Awake()
    {
        if (instance != null) Debug.LogError("1 FruifCollect thoi");
        instance = this;
    }

    private void Start()
    {
        text.text= "Point: "+ point_total;
    }

    public void Collect(int point)
    {
        collectSoundEffect.Play();
        point_total = point_total+ point;
        text.text= "Point: " + point_total;
    }

}
