using System;
using UnityEngine;

public class GreenPart : MonoBehaviour
{
    public int CurrentLevel = 0, StartingLevel=0;
    public int LevelsToUpgrade = 1;
    [SerializeField] private float[] _gainedMoney = new float[] { 1, 2, 3 };
    public static Action<float> onGreenPartMoney;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CurrentLevel==0)
            return;
        onGreenPartMoney?.Invoke(_gainedMoney[CurrentLevel-1]);
    }
    private void OnDisable()
    {
        CurrentLevel = StartingLevel;
    }
}
