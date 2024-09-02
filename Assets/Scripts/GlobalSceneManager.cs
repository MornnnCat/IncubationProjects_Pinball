using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public static GlobalSceneManager Instance;


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