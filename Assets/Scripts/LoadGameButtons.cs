using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creation, placement and controlling of the load buttons.
// Controlling of "Load" and "Delete" buttons.
public class LoadGameButtons : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject loadManagerMenu;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private GameObject storageFullMenu;
    [SerializeField] private GameObject uiManager;
    
    private UIManager _uiManager;
    private string _curRecordName = "";
    private List<GameObject> _buttonsList = new List<GameObject>();
    private float _distanceBetweenButtonsAxes;
    private float _height;
    private float _width;
    private float _distanceBetweenButtons;

    public void Awake()
    {
        _uiManager = uiManager.GetComponent<UIManager>();
        _height = Screen.height / LoadButton.HeightCoefficient;
        _width = Screen.width / LoadButton.WidthCoefficient;
        _distanceBetweenButtons = Screen.height / LoadButton.DistanceBetweenButtonsCoefficient;
        _distanceBetweenButtonsAxes = _height + _distanceBetweenButtons;
        loadButton.onClick.AddListener(LoadRecord);
        deleteButton.onClick.AddListener(DeleteRecord);
    }
    
    
    
    //--------------------------------------------------
    // Creation and functionality of the load buttons --
    //--------------------------------------------------
    public void PlaceButtons()
    {
        float y = FindStartPosition();
        
        ClearButtonsList();
        foreach (var recordName in SaveSerial.RecordsNames)
        {
            CreateButton(recordName, y);
            y -= _distanceBetweenButtonsAxes;
        }
    }

    private float FindStartPosition()
    {
        float startPosition = 0f;
        float buttonsNumber = SaveSerial.RecordsNames.Count;
        
        if (buttonsNumber == 1f) return startPosition;
        if (buttonsNumber % 2 != 0)
            startPosition = (_distanceBetweenButtonsAxes) * (buttonsNumber / 2 - 0.5f);
        else
            startPosition = (_distanceBetweenButtonsAxes) / 2 +
                            (_distanceBetweenButtonsAxes) * (buttonsNumber / 2 - 1);
        return startPosition;
    }
    
    private void CreateButton(string recordName, float yPosition)
    {
        // Button
        GameObject button = new GameObject();
        button.transform.parent = transform;
        
        button.AddComponent<Button>().onClick.AddListener(delegate { LoadManagerMenu(recordName); });
        button.AddComponent<Image>().sprite = sprite;
        
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.localPosition = new Vector3(0, yPosition, 0);
        buttonRectTransform.sizeDelta = new Vector2(_width, _height);
        
        // Text
        GameObject text = new GameObject();
        text.transform.parent = button.transform;
        
        Text textComp = text.AddComponent<Text>();
        textComp.font = (Font)Resources.Load(LoadButton.TextFont);
        textComp.text = recordName;
        textComp.fontSize = LoadButton.FontSize;
        textComp.color = LoadButton.TextColor;
        textComp.alignment = LoadButton.Alignment;
        
        RectTransform textRectTransform = textComp.GetComponent<RectTransform>();
        textRectTransform.localPosition = Vector3.zero;
        textRectTransform.sizeDelta = new Vector2(_width, _height);
        
        // adding new button in the buttons list
        _buttonsList.Add(button);
        _buttonsList.Add(text);
    }
    
    void ClearButtonsList()
    {
        foreach (var obj in _buttonsList)
            Destroy(obj);
        _buttonsList.Clear();
    }

    private void LoadManagerMenu(string fileName)
    {
        GameState.GameMode = GameMode.LoadGame;
        loadManagerMenu.SetActive(true);
        _curRecordName = fileName;
    }
    
    
    
    //------------------------------------------------
    // Functionality of "Load" and "Delete" buttons --
    //------------------------------------------------
    private void LoadRecord()
    {
        if (SaveSerial.RecordsNames.Count < 10)
        {
            SaveSerial.LoadGame( _curRecordName + Settings.Extension);
            GameState.GameMode = GameMode.NormalMode;
            _uiManager.RunScene();
        }
        else
        {
            storageFullMenu.SetActive(true);
            loadManagerMenu.SetActive(false);
        }
    }
    
    private void DeleteRecord()
    {
        SaveSerial.DeleteRecord(_curRecordName + Settings.Extension);
        SaveSerial.RecordsNames = SaveSerial.WriteRecordsFromDirectory();
        _uiManager.RunScene();
    }
}