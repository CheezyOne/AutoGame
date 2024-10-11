using UnityEngine;

public class MusicIcon : MonoBehaviour
{
    [SerializeField] private protected SliderSoundsMusic _sliderComponent;
    [SerializeField] private GameObject _cross;
    private const float MINIMAL_SLIDER_VALUE = 0.001f;
    private void Update()
    {
        SetCross();
    }
    private protected void SetCross()
    {
        if (_sliderComponent.CurrentValue > MINIMAL_SLIDER_VALUE)
            _cross.SetActive(false);
        else
            _cross.SetActive(true);
    }
    public void ToggleSounds()
    {
        if (_sliderComponent.CurrentValue > MINIMAL_SLIDER_VALUE)
        {
            _sliderComponent.ChangeSliderValue(0f);
        }
        else
        {
            _sliderComponent.ChangeSliderValue(1f);
        }
        _sliderComponent.SetSlider();
    }
}
