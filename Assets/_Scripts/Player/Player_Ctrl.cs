using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour
{
   public Player_TakeDamage player_TakeDamage;
   public Player_Collider player_Collider;
   public Player Player;
   public KnockBack knockBack;
   public Player_OnWayPlatfom Player_OnWayPlatfom;
   public PickUp PickUp;


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
