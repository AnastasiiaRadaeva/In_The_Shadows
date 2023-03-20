using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    private Toggle _toggle;
    private AudioSource _audioMainCamera;
    
    void Awake()
    {
        _toggle = GetComponent<Toggle>();

        if (gameObject.name[.. 4] == "Menu")
        {
            _audioMainCamera = mainCamera.GetComponent<AudioSource>();
            _toggle.isOn = Settings.MenuMusic;
        }
        else
            _toggle.isOn = Settings.GameMusic;
    }

    public void ToggleMusic(bool toggleOn)
    {
        if (gameObject.name[.. 4] == "Menu")
        {
            Settings.MenuMusic = toggleOn;
            _audioMainCamera.enabled = toggleOn;
        }
        else
            Settings.GameMusic = toggleOn;
    }
}
