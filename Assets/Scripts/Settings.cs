using UnityEngine;

// Game settings and const
public static class Settings
{
    // item position and movement
    public const float RotateAngle = 0.8f;
    public const float MovementStep = 0.1f;
    public const float AngleTolerance = 10.0f;
    public const float DistanceTolerance = 1.0f;
    public const float MinRandomRotateAngle = 20f;
    public const float MaxRandomRotateAngle = 340f;
    public const float RandomXShift = 10f;
    public const float InterpolationRatio = 0.01f;
    
    // const for records
    public static readonly string Path = Application.persistentDataPath;
    public const string Extension = ".json";
    public const string DateFormat = "yyyy-MM-dd HH:mm:ss";
    public const int MaxRecordCount = 10;

    // fade const
    public const float FadeTime = 1.5f;
    public const string FadeTrigger = "Start";
    
    // names length
    public const int SceneNameLength = 6;
    public const int CheckboxNameLength = 4;
    
    // game objects names
    public const string UIManagerObjName = "UI Manager";
    public const string GameManagerObjName = "Game Manager";
    public const string MenuSceneName = "Menu";
}

// load buttons const
public struct LoadButton
{
    public const float Height = 100f;
    public const float Wide = 570f;
    public const float DistanceBetweenButtons = 40f;
    public const string TextFont = "BalooBhaijaanRegular";
    public const int FontSize = 50;
    public static readonly Color TextColor = new Color32(15, 39, 86, 255);
    public const TextAnchor Alignment =  TextAnchor.MiddleCenter;
}