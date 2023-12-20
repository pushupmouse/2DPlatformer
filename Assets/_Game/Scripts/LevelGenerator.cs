using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private void Start()
    {
        GenerateLevel();
    }
    private void GenerateLevel()
    {
        if(LevelButton.buttonNum >= 1)
        {
            Instantiate(Resources.Load<GameObject>(MyConst.LEVEL + LevelButton.buttonNum));
        }
        else
        {
            Instantiate(Resources.Load<GameObject>(MyConst.LEVEL + "1"));
        }
    }
}
