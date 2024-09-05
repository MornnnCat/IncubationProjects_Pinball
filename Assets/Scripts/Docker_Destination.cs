using UnityEngine;

public class Docker_Destination : MonoBehaviour
{
    public GlobalSceneManager.SceneNameEnum nextSceneName;

    private void Awake()
    {
        // 时间恢复
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Score();
    }

    private void Score()
    {
        // 设置下一关场景名称
        //PassPanelCanvas.Instance.SetTargetSceneName(nextSceneName.ToString());
        // 展示过关面板
        PassPanelCanvas.Instance.IsShowPanel(true);
        // 时间暂停
        Time.timeScale = 0;
    }
}