using System;
using UnityEngine;

public class MovingPartsPurchase : MonoBehaviour
{
    public static Action onAllMovingPartsUnlocked;
    [SerializeField] private MovingPart[] _movingPartsComponents;
    private int _partIndex = 0;
    private void OnEnable()
    {
        ShopButtons.onMovingPurchase += OpenNextPart;
    }
    private void OnDisable()
    {
        ShopButtons.onMovingPurchase -= OpenNextPart;
    }
    private void OpenNextPart()
    {
        _movingPartsComponents[_partIndex].IsActive = true;
        _movingPartsComponents[_partIndex].StartMoving();
        _partIndex++;
        if (_partIndex == _movingPartsComponents.Length)
            onAllMovingPartsUnlocked?.Invoke();
    }
}
