using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaveGameObj
{
    void SaveData(ref SaveDataGame data);
    void LoadData(SaveDataGame data);
}
