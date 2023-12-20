using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Button levelButton;
    public static int buttonNum;

    public Text LevelText { get => levelText; set { levelText = value;} }

    public void SetData(int id)
    {
        levelText.text = MyConst.LEVEL + " " + id.ToString();
        levelButton.onClick.AddListener(() => OnClickLevelButton(id));
    }

    private void OnClickLevelButton(int id)
    {
        buttonNum = id;
        SceneManager.LoadScene(MyConst.LEVEL);
    }
}
