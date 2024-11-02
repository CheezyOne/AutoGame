using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AchievementsPopUp : MonoBehaviour
{
    [SerializeField] private Achievement[] _achievements;
    [SerializeField] private Transform _achievementsSpawnPoint;
    [SerializeField] private GameObject _achievementPrefab;
    [SerializeField] private float _achievementsYMoveDistance, _achievementLowerTime, _achievementMovementSpeed;
    [SerializeField] private AchievementsColors[] _achievementsColorFix;
    private Dictionary<AchievementRarity,Material> _achievementsMaterial = new();
    private bool[] _occupiedSpaces = new bool[6];
    private float _startingAchievementY;
    private void Awake()
    {
        _startingAchievementY = _achievementsSpawnPoint.position.y;
        foreach(var achievement in _achievementsColorFix) 
        {
            _achievementsMaterial.Add(achievement.Rarity,achievement.Material); // Это фиксит проблему с словарями в инспекторе
        }
    }
    public void AchievementPopUp(int achiemeventIndex)
    {
        int OccupiedSpace = -1;
        for (int i= 0;i<_occupiedSpaces.Length;i++)
        {
            if (!_occupiedSpaces[i])
            {
                _occupiedSpaces[i] = true;
                OccupiedSpace = i;
                break;
            }
        }
        if (OccupiedSpace < 0)
            return;
        Debug.Log(OccupiedSpace);
        GameObject NewAchievement = SpawnAchievement(achiemeventIndex);
        NewAchievement.transform.DOLocalMoveY(_startingAchievementY - ((OccupiedSpace+1) * _achievementsYMoveDistance), _achievementMovementSpeed);
        MoveAchievementUp(NewAchievement.transform, (OccupiedSpace + 1), OccupiedSpace);
    }
    private GameObject SpawnAchievement(int achiemeventIndex)
    {
        GameObject NewAchievement = PoolManager.SpawnObject(_achievementPrefab, _achievementsSpawnPoint.position, Quaternion.identity, _achievementsSpawnPoint);
        Transform NewAchievementTransform = NewAchievement.transform;
        NewAchievementTransform.GetChild(1).GetComponent<TMP_Text>().text = _achievements[achiemeventIndex].Name;
        NewAchievementTransform.GetChild(2).GetComponent<TMP_Text>().text = _achievements[achiemeventIndex].Description;
        foreach (Transform FramePart in NewAchievementTransform.GetChild(3))
        {
            FramePart.GetComponent<SpriteRenderer>().material = _achievementsMaterial[_achievements[achiemeventIndex].Rarity];
        }
        return NewAchievement;
    }
    private async void MoveAchievementUp(Transform achievement, int achievementPlace, int occupiedSpace)
    {
        await Task.Delay((int)_achievementLowerTime * 1000);
        achievement.DOLocalMoveY(achievement.localPosition.y + achievementPlace * _achievementsYMoveDistance, _achievementMovementSpeed).OnComplete(()=> PoolManager.ReturnObjectToPool(achievement.gameObject));
        _occupiedSpaces[occupiedSpace] = false; 
    }
}
[Serializable]
public struct AchievementsColors
{
    public Material Material;
    public AchievementRarity Rarity;
}

[Serializable]
public class Achievement
{
    public string Name, Description;
    public AchievementRarity Rarity;
}
public enum AchievementRarity
{
    Common,
    Rare,
    Unique,
    Legendary
}