using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonGenerator : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private Transform levelItemParent;

    private void Start()
    {
        SpawnLevelButtons();
    }
    public void SpawnLevelButtons()
    {
        for (int i = 0; i < levelData.itemLevelDatas.Count; i++)
        {
            LevelButton levelButton = Instantiate(levelButtonPrefab, levelItemParent);
            levelButton.SetData(levelData.itemLevelDatas[i].itemLevelDataId);
        }
    }
}
