using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    private UIManager _uiManager;
    // private AudioSource _audioClip;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        // _audioClip = GetComponent<AudioSource>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BackToMenu);
    }
    
    void BackToMenu()
    {
        // _audioClip.Play();
        _uiManager.RunScene();
    }
}
