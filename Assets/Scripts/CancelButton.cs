using UnityEngine;
using UnityEngine.UI;

// Controlling of the "Cancel" button.
public class CancelButton : MonoBehaviour
{
    private UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find(Settings.UIManagerObjName).GetComponent<UIManager>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BackToMenu);
    }
    
    void BackToMenu()
    {
        _uiManager.RunScene();
    }
}