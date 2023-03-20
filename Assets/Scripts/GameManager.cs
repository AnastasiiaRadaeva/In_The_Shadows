using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<ShadowManager> _shadowManagers;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject victoryImage;
    [SerializeField] private GameObject unlockText;
    [SerializeField] private GameObject mainCamera;
    private AudioSource _sceneMusic;

    public List<GameObject> items;
    public int countOfObjectsInProcess = -1;
    public bool victory;
    
    

    // Start is called before the first frame update
    void Start()
    {
        if(Settings.GameMusic)
            mainCamera.GetComponent<AudioSource>().Play();
        victory = false;
        countOfObjectsInProcess = items.Count;
        _shadowManagers = new List<ShadowManager>();

        for (int i = 0; i < items.Count; i++)
        {
            SetObjectToStartPosition(i);
            _shadowManagers.Add(items[i].GetComponent<ShadowManager>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectVictory())
        {
            victory = true;
            victoryImage.SetActive(true);
            for (int i = 0; i < items.Count; i++)
            {
                SetObjectToTargetPosition(_shadowManagers[i], items[i]);
            }
            if (SceneManager.GetActiveScene().buildIndex <= Settings.Data.tasksStatus.Length)
            {
                if (Settings.GameMode == GameMode.NormalMode)
                {
                    if (!Settings.Data.tasksStatus[SceneManager.GetActiveScene().buildIndex - 1])
                    {
                        Settings.Data.tasksStatus[SceneManager.GetActiveScene().buildIndex - 1] = true;
                        unlockText.SetActive(true);
                    }
                }
                nextButton.gameObject.SetActive(true);
            }
        }
    }

    private bool DetectVictory()
    {
        if (countOfObjectsInProcess == 0) return true;
        foreach (var obj in _shadowManagers)
        {
            countOfObjectsInProcess = obj.Victory
                ? countOfObjectsInProcess - 1
                : countOfObjectsInProcess;
        }
        return countOfObjectsInProcess == 0;
    }

    private void SetObjectToStartPosition(int index)
    {
        GameObject item = items[index];
        if (Settings.Data.saveForCurrentLevelExist && 
            Settings.Data.activeTask == SceneManager.GetActiveScene().buildIndex)
        {
            item.transform.rotation = Settings.Data.itemsRotation[index];
            if (item.GetComponent<ObjectMover>().move)
                item.transform.position = Settings.Data.itemsPosition[index];
        }
        else
        {
            item.transform.RotateAround(item.transform.position, Vector3.up, Random.Range(20f, 340f));
            if (item.GetComponent<ObjectMover>().upDownRotate)
                item.transform.RotateAround(item.transform.position, Vector3.right, Random.Range(20f, 340f));
            if (item.GetComponent<ObjectMover>().move)
            {
                Vector3 pos = item.transform.position;
                item.transform.position = new Vector3(pos.x + Random.Range(-10f, 10f), pos.y, pos.z);
            }
        }
    }

    private void SetObjectToTargetPosition(ShadowManager shadow, GameObject item)
    {
        item.transform.rotation = Quaternion.Lerp(item.transform.rotation, shadow.targetRotation, 0.01f);
        item.transform.position = 
            Vector3.Lerp(item.transform.position, shadow.targerPosition, Time.deltaTime * 1.0f);
    }
}
