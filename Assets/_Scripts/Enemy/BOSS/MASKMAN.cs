
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MASKMAN : MonoBehaviour
{
    [SerializeField] private int hp_Max;
    private int hp_Current;

    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject _explosive;
    private Explosive explosive;

    void Start()
    {
        explosive = _explosive.GetComponent<Explosive>();
    }

    private void Explosive()
    {
        explosive.Explosivee();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp_Current/ hp_Max;
    }

    
}
