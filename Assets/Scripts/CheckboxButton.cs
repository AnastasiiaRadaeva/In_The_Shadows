using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckboxButton : MonoBehaviour
{
    [SerializeField] private GameObject _secondComponent;
    [SerializeField] private GameObject _menuMainCamera;
    private AudioSource _menuMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        _menuMusic = _menuMainCamera.GetComponent<AudioSource>();
        if(gameObject.name == "Box")
            GetComponent<Button>().onClick.AddListener(Box);
        else
            GetComponent<Button>().onClick.AddListener(Checkmark);
    }

    // Update is called once per frame

    void Box()
    {
        _secondComponent.SetActive(true);
        // _menuMusic.loop = true;
        _menuMusic.Play();
    }

    void Checkmark()
    {
        gameObject.SetActive(false);
        _menuMusic.Stop();
    }
}
