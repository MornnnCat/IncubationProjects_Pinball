using UnityEngine;
using UnityEngine.UI;


public class PassPanelCanvas : MonoBehaviour
{
    public Button nextLevelBtn;
    public RectTransform panelRectTransform;
    [SceneNameAttribute] public string targetSceneName;

    private void Start()
    {
        SetPanelSize();
        SetupNextLevelButton();
    }

    private void SetPanelSize()
    {
        var w = Screen.width * 0.5f;
        var h = Screen.height * 0.5f;
        panelRectTransform.sizeDelta = new Vector2(w, h);
    }

    private void SetupNextLevelButton()
    {
        nextLevelBtn.onClick.AddListener(() =>
        {
            GlobalSceneManager.Instance.LoadSceneBySceneName(targetSceneName);
            this.gameObject.SetActive(false);
        });
    }
}