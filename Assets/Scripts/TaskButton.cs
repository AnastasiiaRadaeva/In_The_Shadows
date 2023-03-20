using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskButton : MonoBehaviour
{
    [SerializeField] private Canvas fadeScene;
    private Animator _transition;
    // private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _transition = fadeScene.GetComponent<Animator>();
        Button button = gameObject.GetComponent<Button>();
        // _audio = gameObject.GetComponent<AudioSource>();
        button.onClick.AddListener(PlayTask);
    }
    
    void PlayTask()
    {
        Settings.GameMode = Settings.GameMode == GameMode.TestModeMenu ? GameMode.TestMode : GameMode.NormalMode;
        if (Settings.GameMode == GameMode.NormalMode)
            Settings.Data.sessionInProgress = true;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(Settings.FadeTime);
        SceneManager.LoadScene(gameObject.name[..6]);
    }
}
