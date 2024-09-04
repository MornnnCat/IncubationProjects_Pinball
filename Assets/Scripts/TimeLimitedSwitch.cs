using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TimeLimitedSwitch : MonoBehaviour
{
    public Transform door;
    public float speedTimeOfCloseDoor = 3f;
    private Vector2 _doorOriginScale;

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
        PlayCloseDoorAnimate();
    }
}