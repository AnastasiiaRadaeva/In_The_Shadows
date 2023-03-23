using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controlling of the majority of the "Menu" scene buttons.
public class UIManager : MonoBehaviour
{
    // Canvas Components
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsMenu;
    [SerializeField] private GameObject lockerIconsObj;
    [SerializeField] private List<GameObject> lockerIcons;
    [SerializeField] private GameObject saveMenu;
    [SerializeField] private GameObject loadManagerMenu;
    [SerializeField] private GameObject storageFullMenu;
    [SerializeField] private GameObject settingsMenu;

    // Main Menu
    [SerializeField] private Button normalModeButton;
    [SerializeField] private Button testModeButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Image loadGameLocker;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    
    // Save and Load Menu
    [SerializeField] private Button saveButton;
    [SerializeField] private Button dontSaveButton;
    [SerializeField] private GameObject loadGameButtons;
    [SerializeField] private Button okButton;

    // UI
    [SerializeField] private Button backButton;

    // Main camera for Audio (AudioSource - menu music)
    [SerializeField] private GameObject mainCamera;

    void Start()
    {
        // Turn on/off music
        mainCamera.GetComponent<AudioSource>().enabled = GameState.MenuMusic;
        
        normalModeButton.onClick.AddListener(NormalModeMenu);
        testModeButton.onClick.AddListener(TestModeMenu);
        loadGameButton.onClick.AddListener(LoadGameMenu);
        settingsButton.onClick.AddListener(SettingsMenu);
        exitButton.onClick.AddListener(ExitGame);
        
        saveButton.onClick.AddListener(Save);
        dontSaveButton.onClick.AddListener(DontSave);
        okButton.onClick.AddListener(LoadGameMenu);

        // Select right menu
        RunScene();
    }

    public void RunScene()
    {
        if (GameState.GameMode == GameMode.NormalMode)
            NormalModeMenu();
        else if (GameState.GameMode == GameMode.TestMode)
            TestModeMenu();
        else if (GameState.GameMode == GameMode.LoadGame)
            LoadGameMenu();
        else
            MainMenu();
    }
    
    void MainMenu()
    {
        GameState.GameMode = GameMode.MainMenu;
        DisableCanvas();
        mainMenu.SetActive(true);
        // Turn off Load Game Button if there aren't records
        loadGameLocker.gameObject.SetActive(SaveSerial.RecordsNames.Count == 0);
    }
    
    void SaveMenu()
    {
        saveMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
    }
    
    
    
    //-------------------------
    // Buttons functionality --
    //-------------------------
    
    void NormalModeMenu()
    {
        if (SaveSerial.RecordsNames.Count < Settings.MaxRecordCount)
        {
            GameState.GameMode = GameMode.NormalModeMenu;
            DisableCanvas();
            levelsMenu.SetActive(true); 
            lockerIconsObj.SetActive(true);
            LockerStatus();
            backButton.gameObject.SetActive(true);
        }
        else
            storageFullMenu.SetActive(true);
    }
    
    void TestModeMenu()
    {
        GameState.GameMode = GameMode.TestModeMenu;
        DisableCanvas();
        levelsMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    void LoadGameMenu()
    {
        GameState.GameMode = GameMode.LoadGameMenu;
        if (SaveSerial.Data.sessionInProgress)
            SaveMenu();
        else
        {
            DisableCanvas();
            loadGameButtons.SetActive(true);
            backButton.gameObject.SetActive(true);
            loadGameButtons.GetComponent<LoadGameButtons>().PlaceButtons();
        }
    }
    
    void SettingsMenu()
    {
        GameState.GameMode = GameMode.SettingMenu;
        DisableCanvas();
        settingsMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
    
    void ExitGame()
    {
        if (SaveSerial.Data.sessionInProgress)
            SaveMenu();
        else
            Application.Quit();
    }
    
    void Save()
    {
        if (SaveSerial.RecordsNames.Count < Settings.MaxRecordCount)
        {
            SaveSerial.SaveGame();
            SaveSerial.RecordsNames = SaveSerial.WriteRecordsFromDirectory();
            SaveSerial.ResetData();
            if (GameState.GameMode == GameMode.LoadGameMenu)
                LoadGameMenu();
            else if (GameState.GameMode == GameMode.MainMenu)
                ExitGame();
        }
    }

    void DontSave()
    {
        SaveSerial.ResetData();
        if (GameState.GameMode == GameMode.LoadGameMenu)
            LoadGameMenu();
        else if (GameState.GameMode == GameMode.MainMenu)
            ExitGame();
    }
    
    
    
    //------------------
    // Helper methods --
    //------------------

    void LockerStatus()
    {
        for (int i = 0; i < lockerIcons.Count; i++)
            lockerIcons[i].SetActive(!SaveSerial.Data.tasksStatus[i]);
    }

    void DisableCanvas()
    {
        mainMenu.SetActive(false);
        lockerIconsObj.SetActive(false);
        loadGameLocker.gameObject.SetActive(false);
        levelsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        backButton.gameObject.SetActive(false);
        loadGameButtons.SetActive(false);
        loadManagerMenu.SetActive(false);
        storageFullMenu.SetActive(false);
        saveMenu.SetActive(false);
    }
}