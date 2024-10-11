using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSoundsMusic : MonoBehaviour
{
    public float CurrentValue;
    [SerializeField] private Transform[] _squares;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _mixerVariable;
    [SerializeField] private Material _changingMaterial;
    [SerializeField] private float _scaleUpSize = 115f, _scaleDownSize = 30f, _scaleTime = 0.4f;
    [SerializeField] private float _firstMinIntensity, _firstMaxIntensity, _secondMinIntensity, _secondMaxIntensity, _thirdMinIntensity, _thirdMaxIntensity;
    private const float FIRST_THRESHOLD = 0.5f, SECOND_THRESHOLD = 0.85f;
    private Color _firstColor = new Color(191f / 255f, 0f, 0f), _secondColor = new Color(191f / 255f, 191f / 255f, 0f), _thirdColor = new Color(57f / 255f, 191f / 255f, 0f), _finalColor = new Color(1f / 255f, 191f / 255f, 0f);
    private Slider _slider;
    private List<bool> _squareOpened = new();
    private float _stepForSquares;
    private List<float> _thresholdsForSquares = new();
    public void ChangeSliderValue(float currentValue)
    {
        CurrentValue = currentValue;
        _audioMixer.SetFloat(_mixerVariable, Mathf.Log10(CurrentValue) * 20);
        ChangeColors();
        CheckForSquaresToOpen();
    }
    public void SetSlider()
    {
        _slider.value = CurrentValue;
    }
    private void Awake()
    {
        _audioMixer.GetFloat(_mixerVariable, out CurrentValue);
        CurrentValue = Mathf.Pow(10, CurrentValue / 20);
        _slider = GetComponent<Slider>();
        SetSlider();
        ChangeColors();
        _stepForSquares = 1f / _squares.Length;
        for (int i=0;i<_squares.Length;i++)
        {
            _thresholdsForSquares.Add(_stepForSquares * i);
            OpenSquare(i);
            _squareOpened.Add(true);
        }
        _slider.onValueChanged.AddListener(ChangeSliderValue);
    }
    private void CheckForSquaresToOpen()
    {
        for(int i=_squares.Length-1;i>=0;i--)
        {
            if (CurrentValue >= _thresholdsForSquares[i])
            {
                for(int j = 0;j<=i;j++)
                {
                    OpenSquare(j);
                    _squareOpened[i] = true;
                }
                break;
            }
            else
            {
                CloseSquare(i);
                _squareOpened[i] = false;
            }
        }
    }
    private void ChangeColors()
    {
        Color newColor;
        float intensityScale;
        if (CurrentValue <= FIRST_THRESHOLD)
        {
            // Interpolate from red to yellow
            float t = CurrentValue / FIRST_THRESHOLD;
            newColor = Color.Lerp(_firstColor, _secondColor, t);

            intensityScale = _firstMinIntensity + (CurrentValue / FIRST_THRESHOLD) * (_firstMaxIntensity - _firstMinIntensity);
        }
        else if (CurrentValue <= SECOND_THRESHOLD)
        {
            // Interpolate from yellow to light yellow
            float t = (CurrentValue - FIRST_THRESHOLD) / (SECOND_THRESHOLD - FIRST_THRESHOLD);
            newColor = Color.Lerp(_secondColor, _thirdColor, t);
            intensityScale = _secondMinIntensity + (CurrentValue - FIRST_THRESHOLD) / (SECOND_THRESHOLD - FIRST_THRESHOLD) * (_secondMaxIntensity - _secondMinIntensity);
        }
        else
        {
            // Interpolate from light yellow to green
            float t = (CurrentValue - SECOND_THRESHOLD) / (1f - SECOND_THRESHOLD);
            newColor = Color.Lerp(_thirdColor, _finalColor, t);
            intensityScale = _thirdMinIntensity + (CurrentValue - SECOND_THRESHOLD) / (1f - SECOND_THRESHOLD) * (_thirdMaxIntensity - _thirdMinIntensity);
        }
        newColor.r *= Mathf.Pow(2, intensityScale);
        newColor.g *= Mathf.Pow(2, intensityScale);
        newColor.b *= Mathf.Pow(2, intensityScale);

        _changingMaterial.color = newColor;
    }
    private void OpenSquare(int index)
    {
        _squares[index].DOKill();
        _squares[index].DOScale(_scaleUpSize, _scaleTime);
    }
    private void CloseSquare(int index)
    {
        _squares[index].DOKill();
        _squares[index].DOScale(_scaleDownSize, _scaleTime);
    }
}