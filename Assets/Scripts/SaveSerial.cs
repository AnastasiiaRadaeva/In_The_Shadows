using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Working with records.
public static class SaveSerial
{
    public static SaveData Data = new()
    {
        sessionInProgress = false,
        activeTask = -1,
        taskDone = false,
        tasksStatus = new [] { false, false, false },
        itemsPosition = new [] { Vector3.zero, Vector3.zero },
        itemsRotation = new [] { new Quaternion(0f, 0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f)}
    };
    public static List<string> RecordsNames = WriteRecordsFromDirectory();
    
    public static void ResetData()
    {
        Data.sessionInProgress = false;
        Data.activeTask = -1;
        Data.taskDone = false;
        for (int i = 0; i < Data.tasksStatus.Length; i++)
            Data.tasksStatus[i] = false;
        for (int i = 0; i < Data.itemsPosition.Length; i++)
        {
            Data.itemsPosition[i] = Vector3.zero;
            Data.itemsRotation[i] = new Quaternion(0, 0, 0, 0);
        }
    }

    public static void SaveGame()
    {
        String dataJson = JsonUtility.ToJson(Data, true);
        File.WriteAllText(Path.Combine(Settings.Path, 
            (DateTime.Now.ToString(Settings.DateFormat) + Settings.Extension)), dataJson);
    }
    
    public static void LoadGame(string fileName)
    {
        if (!File.Exists(Path.Combine(Settings.Path, fileName)))
            Debug.LogError("No save data to load.");
        JsonUtility.FromJsonOverwrite
            (File.ReadAllText(Path.Combine(Settings.Path, fileName)), Data);
    }
    
    public static void DeleteRecord(string fileName)
    {
        if (File.Exists(Path.Combine(Settings.Path, fileName)))
            File.Delete(Path.Combine(Settings.Path, fileName));
        else
            Debug.LogError("No save data to delete.");
    }
    
    // creating of a list of all records names
    public static List<string> WriteRecordsFromDirectory()
    {
        List<string> list = new List<string>();
        DirectoryInfo path = new DirectoryInfo(Settings.Path);
        FileInfo[] files = path.GetFiles();
        
        foreach(FileInfo i in files)
        {
            if (Path.GetExtension(i.Name) == Settings.Extension)
                list.Add(Path.GetFileNameWithoutExtension(i.Name));
        }
        return list;
    }
}

// Data storage structure
[Serializable]
public class SaveData
{
    // true - there are active session in normal mode
    // false - there aren't active session in normal mode (test mode or game wasn't started) 
    public bool sessionInProgress;
    // the number of the last task in session
    public int activeTask;
    // status of the last task in session
    public bool taskDone;
    // array of every task status
    public bool[] tasksStatus;
    // arrays of items position and rotation
    public Vector3[] itemsPosition;
    public Quaternion[] itemsRotation;
}