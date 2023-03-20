using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] private Canvas fadeScene;
    private Animator _transition;
    
    // Start is called before the first frame update
    void Start()
    {
        _transition = fadeScene.GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(NextButtonClick);
    }
    
    private void NextButtonClick()
    {
        for (int i = 0; i < Settings.Data.itemsPosition.Length; i++)
        {
            Settings.Data.itemsPosition[i] = Vector3.zero;
            Settings.Data.itemsRotation[i] = new Quaternion(0, 0, 0, 0);
        }
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
        StartCoroutine(LoadNextScene());

    }
    
    IEnumerator LoadNextScene()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(Settings.FadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
