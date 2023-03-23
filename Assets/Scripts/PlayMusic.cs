using UnityEngine;
using UnityEngine.UI;

// Controlling of the checkmarks and menu/game music. 
public class PlayMusic : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    
    private Toggle _toggle;
    private AudioSource _audioMainCamera;
    
    void Awake()
    {
        _toggle = GetComponent<Toggle>();

        if (gameObject.name[.. Settings.CheckboxNameLength] == Settings.MenuSceneName)
        {
            _audioMainCamera = mainCamera.GetComponent<AudioSource>();
            _toggle.isOn = GameState.MenuMusic;
        }
        else
            _toggle.isOn = GameState.GameMusic;
    }

    public void ToggleMusic(bool toggleOn)
    {
        if (gameObject.name[.. Settings.CheckboxNameLength] == Settings.MenuSceneName)
        {
            GameState.MenuMusic = toggleOn;
            _audioMainCamera.enabled = toggleOn;
        }
        else
            GameState.GameMusic = toggleOn;
    }
}