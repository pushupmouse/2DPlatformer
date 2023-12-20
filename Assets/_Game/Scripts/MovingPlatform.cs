using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private float speed = 1f;

    private const string PLAYER = "Player";

    private Vector3 target;
    
    private void Start()
    {
        transform.position = pointA.position;
        target = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, pointA.position) < 0.1f)
        {
            target = pointB.position;
        }
        else if(Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            target = pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == PLAYER)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER)
        {
            collision.transform.SetParent(null);
        }
    }
}
