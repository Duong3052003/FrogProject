
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MASKMAN : MonoBehaviour
{
    [SerializeField] private int hp_Max;
    private int hp_Current;

    [SerializeField] private Slider hpBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp_Current/ hp_Max;
    }
}
