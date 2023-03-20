using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Settings
{
    public static GameMode GameMode = GameMode.MainMenu;
    public static bool GameMusic = true;
    public static bool MenuMusic = true;

    public static SaveData Data = new SaveData
    {
        sessionInProgress = false,
        activeTask = -1,
        taskDone = false,
        tasksStatus = new [] { false, false, false },
        saveForCurrentLevelExist = false,
        itemsPosition = new [] { Vector3.zero, Vector3.zero },
        itemsRotation = new [] { new Quaternion(0f, 0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f)}
    };

    public const float RotateAngle = 0.8f;
    public const float MovementStep = 0.1f;
    public const float AngleTolerance = 10.0f;
    public const float DistanceTolerance = 0.2f;

    public static readonly string Path = Application.persistentDataPath;
    public const string Extension = ".json";
    public const int MaxRecordCount = 10;
    public static List<string> RecordsNames = SaveSerial.WriteRecordsFromDirectory();

    public static float FadeTime = 1.5f;
}

public struct SceneNames
{
    public const String Menu = "Menu";
    public const String Task1 = "Task 1";
    public const String Task2 = "Task 2";
    public const String Task3 = "Task 3";
    public const String Task4 = "Task 4";
}

// public struct Tasks
// {
//     public const int Task2 = 0;
//     public const int Task3 = 1;
//     public const int Task4 = 2;
// }

public enum GameMode
{
    MainMenu,
    NormalModeMenu,
    TestModeMenu,
    LoadGameMenu,
    SettingMenu,
    LoadGame,
    NormalMode,
    TestMode
}
