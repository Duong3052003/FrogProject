using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_TakeDamage : Player_Hp
{
    protected Player_Ctrl player_Ctrl;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite heart;
    [SerializeField] private Sprite heart_empty;

    private void Awake()
    {
        player_Ctrl = GetComponent<Player_Ctrl>();
        hp = 3;
        
    }

    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = heart_empty;
        }
        for (int i = 0; i < hp; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].sprite = heart;
            }
        }
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        if (this.IsDead())
        {
            this.player_Ctrl.player_Collider.BeingDead();
        }
    }
}
