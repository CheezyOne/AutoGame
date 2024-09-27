using System;
using TMPro;
using UnityEngine;

public class InLevelTimer : MonoBehaviour
{
    [SerializeField] private GameObject _timer;
    [SerializeField] private float _timeToComplete;
    [SerializeField] private TMP_Text _timerText;
    private float _timePassed;
    public static Action onTimerExpire;

    private void OnEnable()
    {
        _timePassed = 0f;
        if(_timeToComplete<=0)
            _timerText.gameObject.SetActive(false);
        else
            _timerText.text = _timeToComplete.ToString();
        _timer.SetActive(true); 
    }
    private void OnDisable()
    {
        _timer.SetActive(false);
    }
    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timeToComplete-_timePassed <= 0)
            onTimerExpire?.Invoke();
        _timerText.text= Convert.ToInt32(_timeToComplete - _timePassed).ToString();
    }
}
