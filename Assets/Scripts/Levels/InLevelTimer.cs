using System;
using TMPro;
using UnityEngine;

public class InLevelTimer : MonoBehaviour
{
    [SerializeField] private GameObject _timer;
    [SerializeField] private float _timeToComplete;
    [SerializeField] private TMP_Text _timerText;
    private float _timePassed;
    private bool _isGameOn;
    public static Action onTimerExpire;

    private void OnEnable()
    {
        _timePassed = 0f;
        if(_timeToComplete<=0)
            _timerText.gameObject.SetActive(false);
        else
            _timerText.text = _timeToComplete.ToString();
        _timer.SetActive(true);
        LevelsLoader.onLevelStart += GameIsOn;
        LevelCompletionHandler.onLevelCompleted += GameIsOff;
    }
    private void OnDisable()
    {
        LevelsLoader.onLevelStart -= GameIsOn;
        LevelCompletionHandler.onLevelCompleted -= GameIsOff;
        _timer.SetActive(false);
    }
    private void GameIsOn() => _isGameOn = true;
    private void GameIsOff() => _isGameOn = false;
    private void Update()
    {
        if (!_isGameOn)
            return;
        _timePassed += Time.deltaTime;
        if (_timeToComplete-_timePassed <= 0)
            onTimerExpire?.Invoke();
        _timerText.text= Convert.ToInt32(_timeToComplete - _timePassed).ToString();
    }
}
