using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Text coinText;
    [SerializeField] Text highscoreText;

    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SetHighscore(int highscore)
    {
        highscoreText.text = highscore.ToString();
    }
}
