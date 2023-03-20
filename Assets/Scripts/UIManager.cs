using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.GetComponent<AudioSource>().enabled = Settings.MenuMusic;
        
        normalModeButton.onClick.AddListener(NormalModeMenu);
        testModeButton.onClick.AddListener(TestModeMenu);
        loadGameButton.onClick.AddListener(LoadGameMenu);
        settingsButton.onClick.AddListener(SettingsMenu);
        exitButton.onClick.AddListener(ExitGame);
        
        saveButton.onClick.AddListener(Save);
        dontSaveButton.onClick.AddListener(DontSave);
        okButton.onClick.AddListener(LoadGameMenu);

        RunScene();
    }

    public void RunScene()
    {
        if (Settings.GameMode == GameMode.NormalMode)
            NormalModeMenu();
        else if (Settings.GameMode == GameMode.TestMode)
            TestModeMenu();
        else if (Settings.GameMode == GameMode.LoadGame)
            LoadGameMenu();
        else
            MainMenu();
    }
    
    void MainMenu()
    {
        Settings.GameMode = GameMode.MainMenu;
        DisableCanvas();
        mainMenu.SetActive(true);
        loadGameLocker.gameObject.SetActive(Settings.RecordsNames.Count == 0);
    }

    void NormalModeMenu()
    {
        if (Settings.RecordsNames.Count < 10)
        {
            Settings.GameMode = GameMode.NormalModeMenu;
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
        Settings.GameMode = GameMode.TestModeMenu;
        DisableCanvas();
        levelsMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    void LoadGameMenu()
    {
        Settings.GameMode = GameMode.LoadGameMenu;

        if (Settings.Data.sessionInProgress)
            SaveMenu();
        else
        {
            DisableCanvas();
            loadGameButtons.SetActive(true);
            loadGameButtons.GetComponent<LoadGameButtons>().PlaceButtons();
            backButton.gameObject.SetActive(true);
        }
    }

    void SaveMenu()
    {
        saveMenu.SetActive(true);
        backButton.gameObject.SetActive(false);
    }

    void Save()
    {
        if (Settings.RecordsNames.Count < Settings.MaxRecordCount)
        {
            SaveSerial.SaveGame();
            Settings.RecordsNames = SaveSerial.WriteRecordsFromDirectory();
            SaveSerial.ResetData();
            if (Settings.GameMode == GameMode.LoadGameMenu)
                LoadGameMenu();
            else if (Settings.GameMode == GameMode.MainMenu)
                ExitGame();
        }
    }

    void DontSave()
    {
        SaveSerial.ResetData();
        if (Settings.GameMode == GameMode.LoadGameMenu)
            LoadGameMenu();
        else if (Settings.GameMode == GameMode.MainMenu)
            ExitGame();
    }

    void SettingsMenu()
    {
        Settings.GameMode = GameMode.SettingMenu;
        DisableCanvas();
        settingsMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
    
    void ExitGame()
    {
        if (Settings.Data.sessionInProgress)
            SaveMenu();
        else
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }

    void LockerStatus()
    {
        for (int i = 0; i < lockerIcons.Count; i++)
            lockerIcons[i].SetActive(!Settings.Data.tasksStatus[i]);
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
