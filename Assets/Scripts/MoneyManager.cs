using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public float MoneyAmount;
    [SerializeField] private TMP_Text _moneyText;
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
    }
}
