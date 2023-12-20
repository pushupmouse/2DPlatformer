using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyConst.PLAYER)
        {
            collision.GetComponent<Player>().Savepoint();
        }
    }
}
