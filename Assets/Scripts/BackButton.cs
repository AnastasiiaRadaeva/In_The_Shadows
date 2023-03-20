using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private List<GameObject> _items;
    private GameManager _gameManager;
    
    [SerializeField] private Canvas fadeScene;
    private Animator _transition;
    
    // Start is called before the first frame update
    void Start()
    {
        _transition = fadeScene.GetComponent<Animator>();
        if (Settings.GameMode == GameMode.NormalMode)
        {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            _items = _gameManager.items;
        }
        else
        {
            _gameManager = null;
            _items = null;
        }
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PressButton);
    }

    void PressButton()
    {
        if (Settings.GameMode == GameMode.NormalMode &&
            (SceneManager.GetActiveScene().buildIndex - 1 == Settings.Data.tasksStatus.Length ||
            !Settings.Data.tasksStatus[SceneManager.GetActiveScene().buildIndex - 1]))
        {
            CollectData();
        }
        // SceneManager.LoadScene(SceneNames.Menu);
        StartCoroutine(LoadNextScene());
    }
    
    IEnumerator LoadNextScene()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(Settings.FadeTime);
        SceneManager.LoadScene(SceneNames.Menu);
    }

    void CollectData()
    {
        Settings.Data.activeTask = SceneManager.GetActiveScene().buildIndex;
        if (_gameManager && _gameManager.victory)
        {
            Settings.Data.taskDone = true;
            Settings.Data.saveForCurrentLevelExist = false;
        }
        else if (_items != null)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                Settings.Data.itemsPosition[i] = _items[i].transform.position;
                Settings.Data.itemsRotation[i] = _items[i].transform.rotation;
            }
            Settings.Data.taskDone = false;
            Settings.Data.saveForCurrentLevelExist = true;
        }
    }
}
