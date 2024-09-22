using System;
using System.Collections.Generic;
using UnityEngine;

public class GreenPartsPurchase : MonoBehaviour
{
    public static Action onAllGreenPartsUnlocked;
    private int _partsIndex = 0, _maxPartsIndex = 1;
    [SerializeField] private Color[] _colors;
    [SerializeField] private List<int> _unlockedLevels;
    [SerializeField] private GreenPart[] _greenPartsComponents;
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    private void OnEnable()
    {
        for(int i=0; i< _greenPartsComponents.Length; i++)
        {
            if (_greenPartsComponents[i].LevelsToUpgrade > _maxPartsIndex)
                _maxPartsIndex = _greenPartsComponents[i].LevelsToUpgrade;
            _unlockedLevels.Add(0);
        }
        OpenNextPart();
        ShopButtons.onGreenPurchase += OpenNextPart;
    }
    private void OnDisable()
    {
        ShopButtons.onGreenPurchase -= OpenNextPart;
    }
    private void OpenNextPart() 
    {
        bool UpgradedLevel = false;
        for (int i = 0; i < _greenPartsComponents.Length; i++)
        {
            if (_greenPartsComponents[i].LevelsToUpgrade > _unlockedLevels[i] && _unlockedLevels[i] == _partsIndex)
            {
                _greenPartsComponents[i].IsActive = true;
                _unlockedLevels[i]++;
                _spriteRenderers[i].color = _colors[_unlockedLevels[i]];
                UpgradedLevel = true;
                break;
            }
        }

        if (!UpgradedLevel)
        {
            _partsIndex++;
            for (int i = 0; i < _greenPartsComponents.Length; i++)
            {
                if (_greenPartsComponents[i].LevelsToUpgrade > _unlockedLevels[i])
                {
                    UpgradedLevel = true;
                    break;
                }
            }
            if (!UpgradedLevel)
            {
                onAllGreenPartsUnlocked?.Invoke();
                return;
            }
            OpenNextPart();
        }
    }
}
