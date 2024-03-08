using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private SaveDataGame saveDataGame;
    private List<SaveGameObj> saveGameObjs;
    private FileDataHandle fileDataHandle;


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
        this.fileDataHandle = new FileDataHandle(Application.persistentDataPath, fileName, useEncryption);
        this.saveGameObjs = FindAllSaveGames();
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
        this.saveDataGame = fileDataHandle.Load();

        if(this.saveDataGame == null)
        {
            Debug.Log("No datas was found");
            NewGame();
        }

        foreach(SaveGameObj saveGameObj in saveGameObjs)
        {
            saveGameObj.LoadData(saveDataGame);
        }

        //Debug.Log("Load point: " + saveDataGame.point_total);
    }

    public void SaveGame()
    {
        foreach (SaveGameObj saveGameObj in saveGameObjs)
        {
            saveGameObj.SaveData(ref saveDataGame);
        }
        //Debug.Log("Save point: " + saveDataGame.point_total);

        fileDataHandle.Save(saveDataGame);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
