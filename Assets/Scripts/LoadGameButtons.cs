using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButtons : MonoBehaviour
{
    public Sprite sprite;
    public Button loadButton;
    public GameObject deleteButton;
    public GameObject loadManagerMenu;
    public GameObject storageFullMenu;

    private GameObject _text;
    private GameObject _button;
    private UIManager _uiManager;

    private float _heightButton = 100f;
    private float _wideButton = 570f;
    private float _distanceBetweenButtons = 40f;

    private string _recordName = "";
    // private AudioSource _deleteAudio;

    private List<GameObject> _gameObjList = new List<GameObject>();

    public void Start()
    {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        // _deleteAudio = deleteButton.GetComponent<AudioSource>();
        loadButton.onClick.AddListener(LoadRecord);
        deleteButton.GetComponent<Button>().onClick.AddListener(DeleteRecord);
    }

    public void PlaceButtons()
    {
        foreach (var obj in _gameObjList)
            Destroy(obj);
        _gameObjList.Clear();
        
        float y = FindStartPosition(Settings.RecordsNames.Count);
        for (int i = 0; i < Settings.RecordsNames.Count; i++)
        {
            CreateButton(Settings.RecordsNames[i], y);
            y -= _heightButton + _distanceBetweenButtons;
        }
    }

    private float FindStartPosition(float buttonsCount)
    {
        float startPosition = 0f;
        if (buttonsCount == 1) return startPosition;
        if (buttonsCount % 2 != 0)
        {
            startPosition = 
                (_heightButton + _distanceBetweenButtons) * (buttonsCount / 2 - 0.5f);
        }
        else
        {
            startPosition = 
                (_distanceBetweenButtons + _heightButton) / 2 +
                (_heightButton + _distanceBetweenButtons) * (buttonsCount / 2 - 1);
        }
        return startPosition;
    }
    
    private void CreateButton(string recordName, float yPosition)
    {
        // Button
        _button = new GameObject();
        _button.transform.parent = transform;
        Button buttonComponent = _button.AddComponent<Button>();
        buttonComponent.onClick.AddListener(delegate { LoadManagerMenu(recordName); });
        Image imageComponent = _button.AddComponent<Image>();
        imageComponent.sprite = sprite;
        RectTransform buttonRectTransform = _button.GetComponent<RectTransform>();
        buttonRectTransform.localPosition = new Vector3(0, yPosition, 0);
        buttonRectTransform.sizeDelta = new Vector2(_wideButton, _heightButton);
        
        // Text
        _text = new GameObject();
        _text.transform.parent = _button.transform;
        Text textComp = _text.AddComponent<Text>();
        textComp.font = (Font)Resources.Load("BalooBhaijaanRegular");
        textComp.text = recordName;
        textComp.fontSize = 50;
        textComp.color = new Color32(15, 39, 86, 255);
        textComp.alignment = TextAnchor.MiddleCenter;
        RectTransform textRectTransform = textComp.GetComponent<RectTransform>();
        textRectTransform.localPosition = Vector3.zero;
        textRectTransform.sizeDelta = new Vector2(_wideButton, _heightButton);
        
        _gameObjList.Add(_button);
        _gameObjList.Add(_text);
    }

    private void LoadManagerMenu(string fileName)
    {
        Settings.GameMode = GameMode.LoadGame;
        loadManagerMenu.SetActive(true);
        _recordName = fileName;
    }

    private void LoadRecord()
    {
        if (Settings.RecordsNames.Count < 10)
        {
            SaveSerial.LoadGame( _recordName + Settings.Extension);
            Settings.GameMode = GameMode.NormalMode;
            _uiManager.RunScene();
        }
        else
            storageFullMenu.SetActive(true);
    }

    private void DeleteRecord()
    {
        
        SaveSerial.DeleteRecord(_recordName + Settings.Extension);
        Settings.RecordsNames = SaveSerial.WriteRecordsFromDirectory();
        
        // _deleteAudio.Play();
        _uiManager.RunScene();
    }
}
