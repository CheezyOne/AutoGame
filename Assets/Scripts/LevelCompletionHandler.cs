using System;
using TMPro;
using UnityEngine;

public class LevelCompletionHandler : MonoBehaviour
{
    public static Action onLevelCompleted;
    [SerializeField] private bool _needsCrucialParts, _needsMoney;
    [SerializeField] private int _necesseryCrucial, _necessaryMoney, _startingMoney;
    [SerializeField] private TMP_Text _necessaryMoneyText;
    [SerializeField] private MoneyManager _moneyManager;
    private int _currentCrucial;
    private bool _gotCrucialParts, _gotMoney;
    private void OnEnable()
    {
        _gotCrucialParts = !_needsCrucialParts;
        _gotMoney = !_needsMoney;
        _moneyManager.MoneyAmount = _startingMoney;
        if (_needsMoney)
        {
            _necessaryMoneyText.text = "/" + _necessaryMoney;
        }
        else
            _necessaryMoneyText.text = string.Empty;
        CrucialPartToHit.onCrucialPartHit += GetCrucialParts;
    }
    private void OnDisable()
    {
        CrucialPartToHit.onCrucialPartHit -= GetCrucialParts;
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
    private void Update()
    {
        if (_gotCrucialParts && _gotMoney)
            onLevelCompleted?.Invoke();
        GetScore();
    }
}
