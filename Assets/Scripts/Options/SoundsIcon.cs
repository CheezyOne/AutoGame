using UnityEngine;
public class SoundsIcon : MusicIcon
{
    [SerializeField] private GameObject[] _bars;
    [SerializeField] private float[] _soundLevels;
    private void Update()
    {
        for(int i = 0;i<_bars.Length;i++)
        {
            if (_sliderComponent.CurrentValue > _soundLevels[i])
                _bars[i].SetActive(true);
            else
                _bars[i].SetActive(false);
        }
        SetCross();
    }
}
