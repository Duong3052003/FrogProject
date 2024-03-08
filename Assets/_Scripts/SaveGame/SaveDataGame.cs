using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SaveDataGame
{
    public int point_total;
    public SerializableDictionary<string, bool> fruitCollected;

    public SaveDataGame()
    {
        this.point_total = 0;
        fruitCollected = new SerializableDictionary<string, bool>();
    }
}
