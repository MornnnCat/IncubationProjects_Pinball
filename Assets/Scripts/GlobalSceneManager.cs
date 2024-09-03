using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public static GlobalSceneManager Instance;

    public enum SceneNameEnum
    {
       Level_1,
       Level_2,
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void LoadSceneBySceneName(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}