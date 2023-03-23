using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controlling of the "Back" button on the Tasks Scenes. Collecting of the game process data.
public class BackButton : MonoBehaviour
{
    [SerializeField] private Canvas fadeScene;
    [SerializeField] private GameObject gameManagerObj;
    
    private List<GameObject> _items;
    private GameManager _gameManager;
    private Animator _transition;
    
    void Start()
    {
        _transition = fadeScene.GetComponent<Animator>();
        _gameManager = gameManagerObj.GetComponent<GameManager>();
        _items = GameState.GameMode == GameMode.NormalMode ? _gameManager.items : null;

        Button button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
    }

    void PressButton()
    {
        if (GameState.GameMode == GameMode.NormalMode &&
            // the last scene (Task 4)
            (SceneManager.GetActiveScene().buildIndex - 1 == SaveSerial.Data.tasksStatus.Length || 
            // the next task is locked
            !SaveSerial.Data.tasksStatus[SceneManager.GetActiveScene().buildIndex - 1]))
        {
            CollectData();
        }
        StartCoroutine(LoadNextScene());
    }

    void CollectData()
    {
        SaveSerial.Data.activeTask = SceneManager.GetActiveScene().buildIndex;
        if (_gameManager.victory)
            SaveSerial.Data.taskDone = true;
        else if (_items != null)
        {
            SaveSerial.Data.taskDone = false;
            for (int i = 0; i < _items.Count; i++)
            {
                SaveSerial.Data.itemsPosition[i] = _items[i].transform.position;
                SaveSerial.Data.itemsRotation[i] = _items[i].transform.rotation;
            }
        }
    }
    
    IEnumerator LoadNextScene()
    {
        _transition.SetTrigger(Settings.FadeTrigger);
        yield return new WaitForSeconds(Settings.FadeTime);
        SceneManager.LoadScene(Settings.MenuSceneName);
    }
}