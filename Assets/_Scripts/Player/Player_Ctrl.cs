using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour
{
   [NonSerialized] public Player_TakeDamage player_TakeDamage;
   [NonSerialized] public Player_Collider player_Collider;
   [NonSerialized] public Player Player;
   [NonSerialized] public KnockBack knockBack;
   [NonSerialized] public Player_OnWayPlatfom Player_OnWayPlatfom;
   [NonSerialized] public PickUp PickUp;


    private void Awake()
    {
        player_TakeDamage = GetComponent<Player_TakeDamage>();
        player_Collider = GetComponent<Player_Collider>();
        Player=GetComponent<Player>();
        knockBack = GetComponent<KnockBack>();
        Player_OnWayPlatfom = GetComponent<Player_OnWayPlatfom>();
        PickUp = GetComponent<PickUp>();

    }

}
