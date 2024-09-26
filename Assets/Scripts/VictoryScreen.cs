using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
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
        _victoryScreen.SetActive(true);
        _timerText.text = _gameEndTimer.ToString();
    }
    private void Update()
    {
        if (!_victoryScreen.activeSelf)
            _gameEndTimer += Time.deltaTime;
    }
}
