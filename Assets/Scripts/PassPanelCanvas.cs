using System;
using UnityEngine;


public class PassPanelCanvas : MonoBehaviour
{
    public static PassPanelCanvas Instance;
    public RectTransform panelRectTransform;
    private string _targetSceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        SetPanelSize();
    }

    private void SetPanelSize()
    {
        var w = Screen.width * 0.5f;
        var h = Screen.height * 0.5f;
        panelRectTransform.sizeDelta = new Vector2(w, h);
    }

    public void OnClickNextLevelButton()
    {
        GlobalSceneManager.Instance.LoadSceneBySceneName(_targetSceneName);
        IsShowPanel(false);
    }

    public void SetTargetSceneName(string targetSceneName)
    {
        _targetSceneName = targetSceneName;
    }

    public void IsShowPanel(bool isShow)
    {
        panelRectTransform.gameObject.SetActive(isShow);
    }
}