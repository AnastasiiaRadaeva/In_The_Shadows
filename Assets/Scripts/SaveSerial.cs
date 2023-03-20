using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public static class SaveSerial
{ 
    public static void ResetData()
    {
        Settings.Data.sessionInProgress = false;
        Settings.Data.activeTask = -1;
        Settings.Data.taskDone = false;
        for (int i = 0; i < Settings.Data.tasksStatus.Length; i++)
            Settings.Data.tasksStatus[i] = false;
        for (int i = 0; i < Settings.Data.itemsPosition.Length; i++)
        {
            Settings.Data.itemsPosition[i] = Vector3.zero;
            Settings.Data.itemsRotation[i] = new Quaternion(0, 0, 0, 0);
        }
    }

    public static void SaveGame()
    {
        String dataJson = JsonUtility.ToJson(Settings.Data, true);
        File.WriteAllText(Path.Combine(Settings.Path, (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Settings.Extension)), dataJson);
    }
    
    public static void LoadGame(string fileName)
    {
        if (!File.Exists(Path.Combine(Settings.Path, fileName))) return;
        JsonUtility.FromJsonOverwrite
            (File.ReadAllText(Path.Combine(Settings.Path, fileName)), Settings.Data);
    }
    
    public static void DeleteRecord(string fileName)
    {
        if (File.Exists(Path.Combine(Settings.Path, fileName)))
        {
            File.Delete(Path.Combine(Settings.Path, fileName));
        }
        else
            Debug.LogError("No save data to delete.");
    }
    
    //Вызываем, когда удаляем или сохраняем игру или при первой загрузке
    public static List<string> WriteRecordsFromDirectory()
    {
        // Settings.RecordsNames.Clear();
        List<string> list = new List<string>();
        DirectoryInfo path = new DirectoryInfo(Settings.Path);
        FileInfo[] files = path.GetFiles();
        
        foreach(FileInfo i in files)
        {
            if (Path.GetExtension(i.Name) == ".json")
            {
                // Settings.RecordsNames.Add(Path.GetFileNameWithoutExtension(i.Name));
                list.Add(Path.GetFileNameWithoutExtension(i.Name));
            }
        }
        return list;
    }
}

[Serializable]
public class SaveData
{
    public bool sessionInProgress;
    public int activeTask;
    public bool taskDone;
    public bool[] tasksStatus;
    public bool saveForCurrentLevelExist;
    public Vector3[] itemsPosition;
    public Quaternion[] itemsRotation;
}
