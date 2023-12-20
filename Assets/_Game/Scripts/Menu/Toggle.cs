using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    [SerializeField] private Button toggleButton;
    [SerializeField] private Text onAndOffText;
    [SerializeField] private GameObject sliderMusic;

    private bool isOff = false;

    private void Start()
    {
        toggleButton.onClick.AddListener(OnToggleButtonClick);
    }

    private void OnToggleButtonClick()
    {
        if(!isOff)
        {
            onAndOffText.text = "Off";
            sliderMusic.SetActive(false);
            isOff = !isOff;
        }
        else
        {
            onAndOffText.text = "On";
            sliderMusic.SetActive(true);
            isOff = !isOff;
        }
    }
}
