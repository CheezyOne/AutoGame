using UnityEngine;

public class SliderSoundsMusic : MonoBehaviour
{
    [SerializeField] private Material _changingMaterial;
    private const int FIRSTVALUE = 191, SECONDVALUE = 1;
    public void ChangeSliderValue(int currentValue)
    {
        Color currentColor = _changingMaterial.color;
        float currentIntensity = (currentColor.r + currentColor.g + currentColor.b) / 3;
        Color newColor = currentColor * (4 / currentIntensity);
        _changingMaterial.color = newColor;       
    }
}