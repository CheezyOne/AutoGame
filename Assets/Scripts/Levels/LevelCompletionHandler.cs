using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(InLevelTimer))]
public class LevelCompletionHandler : MonoBehaviour
{
    public static Action onLevelCompleted;
    [SerializeField] private bool _needsCrucialParts, _needsMoney;
    [SerializeField] private int _currentLevel, _necesseryCrucial, _necessaryMoney, _startingMoney;
    [SerializeField] private TMP_Text _necessaryMoneyText;
    [SerializeField] private MoneyManager _moneyManager;
    private InLevelTimer _inLevelTimer => GetComponent<InLevelTimer>();
    private int _currentCrucial;
    private bool _gotCrucialParts, _gotMoney;
    private const string LEVEL_TIME = "LevelTime";
    private void OnEnable()
    {
        _gotCrucialParts = !_needsCrucialParts;
        _gotMoney = !_needsMoney;
        _moneyManager.MoneyAmount = _startingMoney;
        _moneyManager.UpdateText();
        if (_needsMoney)
        {
            _necessaryMoneyText.text = "/" + _necessaryMoney;
        }
        else
            _necessaryMoneyText.text = string.Empty;
        CrucialPartToHit.onCrucialPartHit += GetCrucialParts;
        CrucialPartToHit.onCrucialPartRegenerate += CrucialPartRegenerated;
    }
    private void OnDisable()
    {
        CrucialPartToHit.onCrucialPartHit -= GetCrucialParts;
        CrucialPartToHit.onCrucialPartRegenerate -= CrucialPartRegenerated;
    }

    private void CrucialPartRegenerated()
    {
        _currentCrucial--;
        _gotCrucialParts = false;
    }
    private void GetCrucialParts()
    {
        _currentCrucial++;
        if (_currentCrucial == _necesseryCrucial)
            _gotCrucialParts = true;
    }
    private void GetScore()
    {
        if (_moneyManager.MoneyAmount >= _necessaryMoney)
            _gotMoney = true;
    }
    private void SetVictoryTime()
    {
        if (PlayerPrefs.GetFloat(_currentLevel.ToString() + LEVEL_TIME) > _inLevelTimer.TimePassed || !PlayerPrefs.HasKey(_currentLevel.ToString() + LEVEL_TIME))
            PlayerPrefs.SetFloat(_currentLevel.ToString() + LEVEL_TIME, (float)Math.Round(_inLevelTimer.TimePassed, 2));
    }
    private void Update()
    {
        if (_gotCrucialParts && _gotMoney)
        {
            SetVictoryTime();
            onLevelCompleted?.Invoke();
        }
        GetScore();
    }
}
