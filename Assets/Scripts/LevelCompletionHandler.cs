using System;
using UnityEngine;

public class LevelCompletionHandler : MonoBehaviour
{
    public static Action onLevelCompleted;
    [SerializeField] private bool _needsCrucialParts, _needsMoney;
    [SerializeField] private int _necesseryCrucial, _necesseryMoney;
    [SerializeField] private MoneyManager _moneyManager;
    private int _currentCrucial;
    private bool _gotCrucialParts, _gotMoney;
    private void OnEnable()
    {
        _gotCrucialParts = !_needsCrucialParts;
        _gotMoney = !_needsMoney;
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
        if (_moneyManager.MoneyAmount >= _necesseryMoney)
            _gotMoney = true;
    }
    private void Update()
    {
        if (_gotCrucialParts && _gotMoney)
            onLevelCompleted?.Invoke();
        GetScore();
    }
}
