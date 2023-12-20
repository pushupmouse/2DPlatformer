using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        exitButton.onClick.AddListener(OnExitButtonClick);

    }

    private void OnExitButtonClick()
    {
        panel.SetActive(false);
    }
}
