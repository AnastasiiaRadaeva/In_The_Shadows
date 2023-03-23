using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controlling of the start items position.
// Detecting of the victory and setting of the items to the right positions.
public class GameManager : MonoBehaviour
{
    public List<GameObject> items;
    public bool victory;
    
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject victoryImage;
    [SerializeField] private GameObject unlockText;
    [SerializeField] private GameObject mainCamera;

    private List<ShadowManager> _shadowManagers;
    
    void Start()
    {
        if(GameState.GameMusic) 
            mainCamera.GetComponent<AudioSource>().Play();
        victory = false;
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
                SetObjectToTargetPosition(_shadowManagers[i], items[i]);
            // detect the current scene is not the last one
            if (SceneManager.GetActiveScene().buildIndex <= SaveSerial.Data.tasksStatus.Length)
            {
                if (GameState.GameMode == GameMode.NormalMode &&
                    !SaveSerial.Data.tasksStatus[SceneManager.GetActiveScene().buildIndex - 1])
                {
                    SaveSerial.Data.tasksStatus[SceneManager.GetActiveScene().buildIndex - 1] = true;
                    unlockText.SetActive(true);
                }
                nextButton.gameObject.SetActive(true);
            }
        }
    }

    private bool DetectVictory()
    {
        int numberOfCompletedObj = 0;
        foreach (var obj in _shadowManagers)
            numberOfCompletedObj += obj.victory ? 1 : 0;
        return numberOfCompletedObj == _shadowManagers.Count;
    }

    private void SetObjectToStartPosition(int index)
    {
        GameObject item = items[index];
        if (GameState.GameMode == GameMode.NormalMode &&
            (!SaveSerial.Data.taskDone && SaveSerial.Data.activeTask == SceneManager.GetActiveScene().buildIndex))
        {
            item.transform.rotation = SaveSerial.Data.itemsRotation[index];
            if (item.GetComponent<ObjectMover>().move)
                item.transform.position = SaveSerial.Data.itemsPosition[index];
        }
        else
        {
            item.transform.RotateAround(item.transform.position, Vector3.up, 
                Random.Range(Settings.MinRandomRotateAngle, Settings.MaxRandomRotateAngle));
            if (item.GetComponent<ObjectMover>().upDownRotate)
                item.transform.RotateAround(item.transform.position, Vector3.right, 
                    Random.Range(Settings.MinRandomRotateAngle, Settings.MaxRandomRotateAngle));
            if (item.GetComponent<ObjectMover>().move)
            {
                float xShift = Random.Range(-Settings.RandomXShift, Settings.RandomXShift);
                Vector3 pos = item.transform.position;
                item.transform.position = new Vector3(pos.x + xShift, pos.y, pos.z);
            }
        }
    }

    private void SetObjectToTargetPosition(ShadowManager shadow, GameObject item)
    {
        item.transform.rotation = 
            Quaternion.Lerp(item.transform.rotation, shadow.targetRotation, Settings.InterpolationRatio);
        item.transform.position = 
            Vector3.Lerp(item.transform.position, shadow.targetPosition, Settings.InterpolationRatio);
    }
}