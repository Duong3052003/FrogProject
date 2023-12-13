using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Hp : MonoBehaviour
{
    public static int hp = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite heart;
    [SerializeField] private Sprite heart_empty;

    // Update is called once per frame
    private void Awake()
    {
        hp = 3;
    }

    void Update()
    {
        foreach(Image img in hearts)
        {
            img.sprite = heart_empty;
        }
        for(int i = 0; i < hp; i++)
        {
            hearts[i].sprite = heart;
        }        
    }
    public static void TakeDamage()
    {
        hp=hp-1;
    }
}
