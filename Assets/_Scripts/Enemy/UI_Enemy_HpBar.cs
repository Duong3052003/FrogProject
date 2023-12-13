using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpBar : MonoBehaviour
{
    private static UI_HpBar instance;
    public static UI_HpBar Instance { get => instance; }

    [SerializeField] private Slider HpBar;

    // Update is called once per frame
    private void Awake()
    {
        if (UI_HpBar.instance != null) Debug.Log("Only 1 UI_HpBar allow to exist");
        instance = this;
    }

    public void UpdateHpBar(float currenHp, float maxHp)
    {
        HpBar.value = currenHp / maxHp;
    }
}
