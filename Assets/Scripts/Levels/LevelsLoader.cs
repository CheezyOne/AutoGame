using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLoader : MonoBehaviour
{
    public static Action onLevelStart;
    private List<GameObject> _levels = new();
    public int _currentLevel;
    private void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            _levels.Add(transform.GetChild(i).gameObject);
            _levels[i].SetActive(false);
        }
    }
    private void OnEnable()
    {
        InLevelTimer.onTimerExpire += RestartLevel;
    }
    private void OnDisable()
    {
        InLevelTimer.onTimerExpire -= RestartLevel;
    }
    public void RestartLevel()
    {
        LoadLevel(_currentLevel);
    }
    public bool IsLastLevel()
    {
        return _currentLevel+1 == _levels.Count;
    }
    public void LoadLevel(int level)
    {
        _levels[_currentLevel].SetActive(false);
        _currentLevel = level;
        _levels[_currentLevel].SetActive(true);
        onLevelStart?.Invoke();
    }
    public void LoatNextLevel()
    {
        LoadLevel(_currentLevel+1);
    }
}
