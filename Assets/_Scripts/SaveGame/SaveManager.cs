using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    private SaveDataGame saveDataGame;
    private List<SaveGameObj> SaveGameObjs;


    public static SaveManager instance { get; private set; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("1 Save Manager thoi");
        }
    }

    private void Start()
    {
        this.SaveGameObjs = FindAllSaveGames();
        LoadGame();
    }

    private List<SaveGameObj> FindAllSaveGames()
    {
        IEnumerable<SaveGameObj> SaveGameObjs = FindObjectsOfType<MonoBehaviour>().OfType<SaveGameObj>();

        return new List<SaveGameObj>(SaveGameObjs);
    }

    public void NewGame()
    {
        this.saveDataGame = new SaveDataGame();
    }

    public void LoadGame()
    {
        if(this.saveDataGame == null)
        {
            Debug.Log("NO DATA");
            NewGame();
        }

        foreach(SaveGameObj saveGameObj in SaveGameObjs)
        {
            saveGameObj.LoadData(saveDataGame);
        }

        Debug.Log("Load:" + saveDataGame.point_total);
    }

    public void SaveGame()
    {
        foreach (SaveGameObj saveGameObj in SaveGameObjs)
        {
            saveGameObj.SaveData(ref saveDataGame);
        }
        Debug.Log("SAve:" + saveDataGame.point_total);

    }
}
