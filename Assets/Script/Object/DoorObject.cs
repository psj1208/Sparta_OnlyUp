using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorObject : IItem
{
    [SerializeField] Vector3 dir;
    [SerializeField] float distance;
    [SerializeField] float speed = 2f;
    Vector3 prePos;
    Vector3 targetPos;
    Vector3 curPos;
    [SerializeField] bool open = false;
    Coroutine curCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        prePos = transform.position;
        targetPos = transform.position + dir * distance;
    }

    public override void ItemAction()
    {
        if (curCoroutine == null)
        {
            curCoroutine = StartCoroutine(DoorAction());
        }
        else
        {
            StopCoroutine(curCoroutine);
            curCoroutine = StartCoroutine(DoorAction());
        }
    }

    IEnumerator DoorAction()
    {
        curPos = transform.position;
        Vector3 target = open ? prePos : targetPos;
        open = !open;

        float elapsedTime = 0f;
        float duration = 1f / speed;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(curPos, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
}
