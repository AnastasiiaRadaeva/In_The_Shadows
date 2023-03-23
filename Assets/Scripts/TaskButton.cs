using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controlling of the task buttons. 
public class TaskButton : MonoBehaviour
{
    [SerializeField] private Canvas fadeScene;
    
    private Animator _transition;
    
    void Start()
    {
        _transition = fadeScene.GetComponent<Animator>();
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(PlayTask);
    }
    
    void PlayTask()
    {
        GameState.GameMode = GameState.GameMode == GameMode.TestModeMenu ? GameMode.TestMode : GameMode.NormalMode;
        if (GameState.GameMode == GameMode.NormalMode)
            SaveSerial.Data.sessionInProgress = true;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        _transition.SetTrigger(Settings.FadeTrigger);
        yield return new WaitForSeconds(Settings.FadeTime);
        SceneManager.LoadScene(gameObject.name[..Settings.SceneNameLength]);
    }
}