using System;
using UnityEngine;

public class GreenPart : MonoBehaviour
{
    public bool IsActive = false;
    public int LevelsToUpgrade = 1;
    [SerializeField] private float _gainedMoney = 1;
    public static Action<float> onGreenPartMoney;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive)
            return;
        onGreenPartMoney?.Invoke(_gainedMoney);
    }
}
