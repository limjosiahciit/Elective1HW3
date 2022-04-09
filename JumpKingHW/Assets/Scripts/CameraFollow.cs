using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing = 5f;

    private Vector3 offset;

    void Start()
    {
        offset = target.position - transform.position;
    }

    void Update()
    {
        Vector2 cameraCenter = Camera.main.ViewportToWorldPoint(new Vector2(0.7f, 0.7f));
        Vector2 outOfBound = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 0.1f));

        if (target.position.x > cameraCenter.x || target.position.x < outOfBound.x)
        {
            Vector3 targetPosition = target.position - offset;
            targetPosition.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (Time.deltaTime * smoothing));
        }

    }
}
