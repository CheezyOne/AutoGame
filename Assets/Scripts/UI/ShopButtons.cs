using System;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    private bool _allGreenPartsUnlocked, _allMovingPartUnlocked;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private float[] _ballcosts;
    [SerializeField] private GameObject[] _spawnBalls;
    public static Action<GameObject> onBallSpawn;
    public static Action onGreenPurchase, onMovingPurchase;
    private void OnEnable()
    {
        GreenPartsPurchase.onAllGreenPartsUnlocked += AllGreenPartsUnlocked;
        MovingPartsPurchase.onAllMovingPartsUnlocked += AllMovingPartsUnlocked;
    }
    private void OnDisable()
    {
        GreenPartsPurchase.onAllGreenPartsUnlocked -= AllGreenPartsUnlocked;
        MovingPartsPurchase.onAllMovingPartsUnlocked -= AllMovingPartsUnlocked;
    }
    private void AllGreenPartsUnlocked() => _allGreenPartsUnlocked = true;
    private void AllMovingPartsUnlocked() => _allMovingPartUnlocked = true;
    public void SpawnNewBall(int BallIndex)
    {
        if (IsEnoughMoney(_ballcosts[BallIndex]))
            onBallSpawn?.Invoke(_spawnBalls[BallIndex]);
    }
    public void BuyNewGreen(float Cost) 
    {
        if (_allGreenPartsUnlocked)
            return;
        if (IsEnoughMoney(Cost))
            onGreenPurchase?.Invoke();
    }
    public void BuyNewMoving(float Cost)
    {
        if (_allMovingPartUnlocked)
            return;
        if (IsEnoughMoney(Cost))
            onMovingPurchase?.Invoke();
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
