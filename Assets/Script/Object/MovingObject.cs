using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] float distance;
    [SerializeField] float speed = 2f;
    Vector3 prePos;
    Vector3 targetPos;

    private void Start()
    {
        prePos = transform.position;
        targetPos = transform.position + dir * distance;
    }
    // Update is called once per frame
    void Update()
    {
        //목표 지점까지 반복 운동.
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(prePos, targetPos, t);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
