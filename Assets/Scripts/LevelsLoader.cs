using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLoader : MonoBehaviour
{
    public static Action onLevelStart;
    private List<GameObject> _levels = new();
    private int _currentLevel;
    private void Awake()
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
    public void LoadLevel(int level)
    {
        _levels[_currentLevel].SetActive(false);
        _currentLevel = level;
        _levels[_currentLevel].SetActive(true);
        onLevelStart?.Invoke();
    }
    public void LoatNextLevel()
    {
        _currentLevel++;
        LoadLevel(_currentLevel);
    }
}
