using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private int levelNum;
    [SerializeField] private Button buttonLevel;

    public int LevelNum => levelNum;
    

    private void Start()
    {
        buttonLevel.onClick.AddListener(delegate { OnClickLevelButton(levelNum); });
    }

    private void OnClickLevelButton(int level)
    {
        SceneManager.LoadScene("Level");
    }
}
