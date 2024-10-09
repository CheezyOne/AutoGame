using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSoundsMusic : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _mixerVariable;
    [SerializeField] private Material _changingMaterial;
    [SerializeField] private float _currentValue, _firstMinIntensity, _firstMaxIntensity, _secondMinIntensity, _secondMaxIntensity, _thirdMinIntensity, _thirdMaxIntensity;
    private const float FIRST_THRESHOLD = 0.5f;
    private const float SECOND_THRESHOLD = 0.85f;
    private Color _firstColor = new Color(191f / 255f, 0f, 0f), _secondColor = new Color(191f / 255f, 191f / 255f, 0f), _thirdColor = new Color(57f/ 255f, 191f / 255f, 0f), _finalColor = new Color(1f / 255f, 191f / 255f, 0f);
    public void ChangeSliderValue(float currentValue)
    {
        _currentValue = currentValue;
        _audioMixer.SetFloat(_mixerVariable, Mathf.Log10(_currentValue) * 20);
        ChangeColors();
    }
    private void Start()
    {
        _audioMixer.GetFloat(_mixerVariable, out _currentValue);
        _currentValue = Mathf.Pow(10, _currentValue / 20);
        GetComponent<Slider>().value = _currentValue;
        ChangeColors();
    }
    private void ChangeColors()
    {
        Color newColor;
        float intensityScale;

        if (_currentValue <= FIRST_THRESHOLD)
        {
            // Interpolate from red to yellow
            float t = _currentValue / FIRST_THRESHOLD;
            newColor = Color.Lerp(_firstColor, _secondColor, t);

            intensityScale = _firstMinIntensity + (_currentValue / FIRST_THRESHOLD) * (_firstMaxIntensity - _firstMinIntensity);
        }
        else if (_currentValue <= SECOND_THRESHOLD)
        {
            // Interpolate from yellow to green
            float t = (_currentValue - FIRST_THRESHOLD) / (SECOND_THRESHOLD - FIRST_THRESHOLD);
            newColor = Color.Lerp(_secondColor, _thirdColor, t);
            intensityScale = _secondMinIntensity + (_currentValue - FIRST_THRESHOLD) / (SECOND_THRESHOLD - FIRST_THRESHOLD) * (_secondMaxIntensity - _secondMinIntensity);
        }
        else
        {
            // Interpolate from green to blue
            float t = (_currentValue - SECOND_THRESHOLD) / (1f - SECOND_THRESHOLD);
            newColor = Color.Lerp(_thirdColor, _finalColor, t);
            intensityScale = _thirdMinIntensity + (_currentValue - SECOND_THRESHOLD) / (1f - SECOND_THRESHOLD) * (_thirdMaxIntensity - _thirdMinIntensity);
        }

        newColor.r *= Mathf.Pow(2, intensityScale);
        newColor.g *= Mathf.Pow(2, intensityScale);
        newColor.b *= Mathf.Pow(2, intensityScale);

        // Set color and intensity
        _changingMaterial.SetVector("_Color", newColor);
    }
}