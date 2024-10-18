using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuLevel : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private Material[] _materials;
    [SerializeField] private int _levelToLoad = 0;
    private const float FADING_TIME = 0.15f;
    private const string LEVELS_PREFS = "BestLevel";
    private Image _image;

    private void Awake()
    {
        PlayerPrefs.SetInt(LEVELS_PREFS, 4);
        _image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(LEVELS_PREFS) < _levelToLoad)
            _image.material = _materials[0];
        else if (PlayerPrefs.GetInt(LEVELS_PREFS) > _levelToLoad)
            _image.material = _materials[1];
        else
            _image.material = _materials[2];
    }
    public void ClickOnButton()
    {
        _background.DOColor(new Color(0,0,0,0.5f), FADING_TIME);
    }
    public void MouseOffButton()
    {
        _background.DOColor(new Color(0, 0, 0, 0f), FADING_TIME);
    }
}
