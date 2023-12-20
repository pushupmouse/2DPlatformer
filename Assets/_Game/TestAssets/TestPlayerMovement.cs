using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField] Transform pointA, pointB;
    [SerializeField] float speed = 2f;
    [SerializeField] float timeDelay = 3f;

    private bool isAtEndpoint = false;
    private bool canMove = false;

    private float progress = 0f;
    private float deltaTime = 0f;

    private void Update()
    {
        if (!canMove)
        {
            Move();
        }
        RunTimer();
    }
    
    private void RunTimer()
    {
        if(deltaTime != 0 && deltaTime >= timeDelay)
        {
            deltaTime = 0;
            canMove = !canMove;
        }
        deltaTime += Time.deltaTime;
    }

    private void Move()
    {
        Vector2 startPoint, endPoint;

        if(progress >= 1f)
        {
            progress = 0f;
            isAtEndpoint = !isAtEndpoint;
        }

        if (isAtEndpoint)
        {
            startPoint = pointB.position;
            endPoint = pointA.position;
        }
        else
        {
            startPoint = pointA.position;
            endPoint = pointB.position;
        }

        progress += Time.deltaTime * speed;
        transform.position = Vector2.Lerp(startPoint, endPoint, progress);
    }
}
