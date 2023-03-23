using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controlling of the Next button.
public class NextButton : MonoBehaviour
{
    [SerializeField] private Canvas fadeScene;
    
    private Animator _transition;
    
    void Start()
    {
        _transition = fadeScene.GetComponent<Animator>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(NextButtonClick);
    }
    
    private void NextButtonClick()
    {
        StartCoroutine(LoadNextScene());
    }
    
    IEnumerator LoadNextScene()
    {
        _transition.SetTrigger(Settings.FadeTrigger);
        yield return new WaitForSeconds(Settings.FadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}