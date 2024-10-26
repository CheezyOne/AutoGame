using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    public float MoneyAmount;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Transform _moneyObject;
    [SerializeField] private float _moneyTextScaleSize, _moneyTextScaleSpeed;
    private void OnEnable()
    {
        GreenPart.onGreenPartMoney += GetMoney;
    }
    private void OnDisable()
    {
        GreenPart.onGreenPartMoney -= GetMoney;
    }
    private void GetMoney(float Money)
    {
        MoneyAmount += Money;
        UpdateText();
    }
    public void UpdateText()
    {
        _moneyText.text = MoneyAmount.ToString();
        AnimateText();
    }
    private void AnimateText()
    {
        Sequence MoneyAnimation = DOTween.Sequence();
        MoneyAnimation.Append(_moneyObject.DOScale(_moneyTextScaleSize, _moneyTextScaleSpeed));
        MoneyAnimation.Append(_moneyObject.DOScale(1, _moneyTextScaleSpeed));
        MoneyAnimation.Play();
    }
}
