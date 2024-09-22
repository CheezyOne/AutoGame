using System;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    private bool _allGreenPartsUnlocked;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private float[] _ballcosts;
    public static Action<int> onBallSpawn;
    public static Action onGreenPurchase;
    private void OnEnable()
    {
        GreenPartsPurchase.onAllGreenPartsUnlocked += AllGreenPartsUnlocked;
    }
    private void OnDisable()
    {
        GreenPartsPurchase.onAllGreenPartsUnlocked -= AllGreenPartsUnlocked;
    }
    private void AllGreenPartsUnlocked() => _allGreenPartsUnlocked = true;
    public void SpawnNewBall(int BallIndex)
    {
        if (IsEnoughMoney(_ballcosts[BallIndex]))
            onBallSpawn?.Invoke(BallIndex);
    }
    public void BuyNewGreen(float Cost) 
    {
        if (_allGreenPartsUnlocked)
            return;
        if (IsEnoughMoney(Cost))
            onGreenPurchase?.Invoke();
    }
    private bool IsEnoughMoney(float Cost)
    {
        if (Cost <= _moneyManager.MoneyAmount)
        {
            _moneyManager.MoneyAmount -= Cost;
            _moneyManager.UpdateText();
            return true;
        }
        return false;
    }
}
