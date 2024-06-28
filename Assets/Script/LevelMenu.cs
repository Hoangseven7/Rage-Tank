using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{
    public Button[] button;

    private void Awake()
    {
        int unlockedLever = PlayerPrefs.GetInt("UnlockedLever", 1);
        for(int i = 0;i< button.Length; i++)
        {
            button[i].interactable = false;
        }
        for(int i = 0; i< unlockedLever; i++)
        {
            button[i].interactable = true;
        }
    }
    public void OpenLevel(int levelId)
    {
        string sceneName = "Lever" + levelId;
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Make sure it is added to the Build Settings.");
        }
    }
}
