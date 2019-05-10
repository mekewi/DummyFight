using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SavingData {
    public List <LinkDetails> itmesInScene;
}
public class SavingGameStates : Singleton<SavingGameStates>
{
    private string fileName;
    SavingData savingData;
    void Start()
    {
        Load();
    }
    private void Awake()
    {
        InitializeData();
    }
    public void InitializeData() {
        savingData = new SavingData();
    }
    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(FilePath(), FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fileStream, savingData);
        fileStream.Close();
    }
    public void Load() {
        if (File.Exists(FilePath()))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(FilePath(),FileMode.Open);
            savingData = (SavingData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
    }
    public void SavePlayerData(List<LinkDetails> itmesInScene) {
        savingData.itmesInScene = itmesInScene;
        Save();
    }
    public List<LinkDetails> LoadPlayerData()
    {
        Load();
        return savingData.itmesInScene ?? new List<LinkDetails>();
    }

    public void ClearData() {
        File.Delete(FilePath());
    }
    public string FilePath() {
        string ApplicationInstanceID = "";
        if (PlayerPrefs.HasKey("ApplicationInstanceID"))
        {
            ApplicationInstanceID = PlayerPrefs.GetString("ApplicationInstanceID");
        }
        else
        {
            ApplicationInstanceID = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("ApplicationInstanceID", ApplicationInstanceID);
        }
        return Application.persistentDataPath + "/" +ApplicationInstanceID + ".dat";
    }
}
