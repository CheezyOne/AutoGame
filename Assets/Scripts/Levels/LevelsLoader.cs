using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLoader : MonoBehaviour
{
    public static Action onLevelStart;
    [SerializeField] private GameObject _gamePlayCanvas;
    private List<GameObject> _levels = new();
    public int CurrentLevel;
    private void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            _levels.Add(transform.GetChild(i).gameObject);
            _levels[i].SetActive(false);
        }
    }
    public void RestartLevel()
    {
        LoadLevel(CurrentLevel);
    }
    public bool IsLastLevel()
    {
        return CurrentLevel+1 == _levels.Count;
    }
    public void LoadLevel(int level)
    {
        _gamePlayCanvas.SetActive(true);
        _levels[CurrentLevel].SetActive(false);
        CurrentLevel = level;
        _levels[CurrentLevel].SetActive(true);
        onLevelStart?.Invoke();
    }
    public void LoatNextLevel()
    {
        LoadLevel(CurrentLevel+1);
    }
}
