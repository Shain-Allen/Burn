using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject moveToPoint;
    [SerializeField] float moveSpeed = 2f;

    void Update()
    {
        float moveDistance = moveSpeed * Vector3.Distance(transform.position, moveToPoint.transform.position) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveToPoint.transform.position, moveDistance);
    }
}
