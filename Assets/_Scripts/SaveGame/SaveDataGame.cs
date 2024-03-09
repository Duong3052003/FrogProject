using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SaveDataGame
{
    public int point_total;
    public int unlockedLevels;
    public SerializableDictionary<string, bool> fruitCollected;
    public SerializableDictionary<string, bool> finishsCollected;

    public SaveDataGame()
    {
        this.point_total = 0;
        this.unlockedLevels = 0;
        fruitCollected = new SerializableDictionary<string, bool>();
        finishsCollected = new SerializableDictionary<string, bool>();

    }
}
