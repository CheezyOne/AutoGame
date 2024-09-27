using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen, _congratsText, _nextButton;
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private TMP_Text _timerText;
    private float _gameEndTimer;
    private void OnEnable()
    {
        LevelsLoader.onLevelStart += NullTimer;
        LevelCompletionHandler.onLevelCompleted += ActivateVictoryScreen;
    }
    private void OnDisable()
    {
        LevelsLoader.onLevelStart -= NullTimer;
        LevelCompletionHandler.onLevelCompleted -= ActivateVictoryScreen;
    }
    private void NullTimer()
    {
        _gameEndTimer = 0;
    }
    public void ActivateVictoryScreen()
    {
        if(_levelsLoader.IsLastLevel())
        {
            _congratsText.SetActive(true);
            _nextButton.SetActive(false);
        }
        else
        {
            _congratsText.SetActive(false);
            _nextButton.SetActive(true);
        }
        _timerText.text = _gameEndTimer.ToString();
        _victoryScreen.SetActive(true);
    }
    private void Update()
    {
        if (!_victoryScreen.activeSelf)
            _gameEndTimer += Time.deltaTime;
    }
}
