using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyConst.PLAYER || collision.tag == MyConst.ENEMY)
        {
            collision.GetComponent<Character>().OnHit(40f);
        }
    }
}
