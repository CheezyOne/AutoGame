using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GreenPart : MonoBehaviour
{
    public int CurrentLevel = 0, StartingLevel=0;
    public int LevelsToUpgrade = 1;
    [SerializeField] private Material[] _materials;
    [SerializeField] private float[] _gainedMoney = new float[] { 1, 2, 3 };
    private SpriteRenderer _spriteRenderer;
    public static Action<float> onGreenPartMoney;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CurrentLevel==0)
            return;
        onGreenPartMoney?.Invoke(_gainedMoney[CurrentLevel-1]);
    }
    private void Start()
    {
        CurrentLevel = StartingLevel;
        _spriteRenderer=GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        _spriteRenderer.material = _materials[StartingLevel];
        SetStart();
    }
    private void SetStart()
    {
        CurrentLevel = StartingLevel;
    }
    public void Upgrade()
    {
        CurrentLevel++;
        _spriteRenderer.material = _materials[CurrentLevel];
    }
}
