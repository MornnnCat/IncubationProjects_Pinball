using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TimeLimitedSwitch : MonoBehaviour
{
    public Transform door;
    public float speedTimeOfCloseDoor = 3f;
    private Vector2 _doorOriginScale;
    public bool isLimitTime = true;

    private void Start()
    {
        _doorOriginScale = door.localScale;
    }

    private void PlayCloseDoorAnimate()
    {
        door.transform.localScale = new Vector2(_doorOriginScale.x, 0f);
        door.transform.DOScaleY(_doorOriginScale.y, speedTimeOfCloseDoor).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLimitTime && door.gameObject.activeSelf) PlayCloseDoorAnimate();
        else door.gameObject.SetActive(false);
    }
}