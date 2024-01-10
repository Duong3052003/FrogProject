using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Hp : MonoBehaviour
{
    protected int hp = 3;

    protected bool IsDead()
    {
        return hp <= 0;
    }

    public virtual void TakeDamage()
    {
        hp -=1;
    }
}
