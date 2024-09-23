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
        bool NextLevelAvailable = false;
        for (int i = 0; i < _greenPartsComponents.Length; i++)
        {
            if (_greenPartsComponents[i].LevelsToUpgrade > _unlockedLevels[i] && _unlockedLevels[i] == _partsIndex)
            {
                _greenPartsComponents[i].CurrentLevel++;
                _unlockedLevels[i]++;
                _spriteRenderers[i].color = _colors[_unlockedLevels[i]];
                UpgradedLevel = true;
                for (int j = 0; j < _greenPartsComponents.Length; j++)
                {
                    if (_greenPartsComponents[j].LevelsToUpgrade > _unlockedLevels[j])
                    {
                        NextLevelAvailable = true;
                        break;
                    }
                }
                break;
            }
        }
        if (!NextLevelAvailable && _partsIndex + 1 == _maxPartsIndex)
        {
            onAllGreenPartsUnlocked?.Invoke();
        }
        if (!UpgradedLevel)
        {
            _partsIndex++;
            OpenNextPart();
        }
    }
}
