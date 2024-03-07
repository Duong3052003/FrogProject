using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandle
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandle(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public SaveDataGame Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveDataGame loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<SaveDataGame>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Loi khi load: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(SaveDataGame data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Loi khi save: " +fullPath+"\n" + e);
        }
    }
}
