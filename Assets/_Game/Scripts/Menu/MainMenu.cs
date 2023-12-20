using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject levelsPanel;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        optionsButton.onClick.AddListener(OnOptionsButtonClick);
        aboutButton.onClick.AddListener(OnAboutButtonClick);
    }

    private void OnAboutButtonClick()
    {
        aboutPanel.SetActive(true);
    }

    private void OnOptionsButtonClick()
    {
        optionsPanel.SetActive(true);
    }

    private void OnStartButtonClick()
    {
        levelsPanel.SetActive(true);    
    }
}
