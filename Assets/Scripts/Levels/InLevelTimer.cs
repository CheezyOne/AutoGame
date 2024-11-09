using System;
using TMPro;
using UnityEngine;

public class InLevelTimer : MonoBehaviour
{
    [SerializeField] private GameObject _timer;
    [SerializeField] private float _timeToComplete;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private bool _isLevelOnTime;
    private bool _isGameOn;
    public float TimePassed;
    public static Action onTimerExpire;

    private void OnEnable()
    {
        TimePassed = 0f;
        LevelsLoader.onLevelStart += GameIsOn;
        LevelCompletionHandler.onLevelCompleted += GameIsOff;
        if (!_isLevelOnTime)
            return;
        if(_timeToComplete<=0)
            _timerText.gameObject.SetActive(false);
        else
            _timerText.text = _timeToComplete.ToString();
        _timer.SetActive(true);
    }
    private void OnDisable()
    {
        _timer.SetActive(false);
        LevelsLoader.onLevelStart -= GameIsOn;
        LevelCompletionHandler.onLevelCompleted -= GameIsOff;
    }
    private void GameIsOn() => _isGameOn = true;
    private void GameIsOff() => _isGameOn = false;

    private void Update()
    {
        if (!_isGameOn)
            return;
        TimePassed += Time.deltaTime;
        if (!_isLevelOnTime)
            return;
        if (_timeToComplete - TimePassed <= 0)
        {
            _isGameOn = false;
            onTimerExpire?.Invoke();
        }
        _timerText.text= Convert.ToInt32(_timeToComplete - TimePassed).ToString();
    }
}
