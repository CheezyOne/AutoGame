using UnityEngine;

[RequireComponent(typeof(LevelsLoader))]
public class TutorialsManager : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialsCanvas;
    [SerializeField] private Transform _tutorialsHolder;
    private Transform _currentTutorial;
    private int _currentSlide = 0;

    private void OnEnable()
    {
        LevelsLoader.onLevelStart += StartTutorial;
    }
    private void OnDisable()
    {
        LevelsLoader.onLevelStart -= StartTutorial;
    }
    private void StartTutorial()
    {
        Time.timeScale = 0;
        _tutorialsCanvas.SetActive(true);
        _currentSlide = -1;
        _currentTutorial = _tutorialsHolder.GetChild(GetComponent<LevelsLoader>().CurrentLevel);
        _currentTutorial.gameObject.SetActive(true);
        NextSlide();
    }
    public void NextSlide()
    {
        if (_currentSlide >= 0)
            _currentTutorial.GetChild(_currentSlide).gameObject.SetActive(false);
        _currentSlide++;
        if (_currentTutorial.childCount <= _currentSlide)
        {
            Time.timeScale = 1;
            _currentTutorial.gameObject.SetActive(false);
            _tutorialsCanvas.SetActive(false);
            return;
        }
        _currentTutorial.GetChild(_currentSlide).gameObject.SetActive(true);
    }
}
