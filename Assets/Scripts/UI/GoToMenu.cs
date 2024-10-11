using UnityEngine;

public class GoToMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToClose;
    [SerializeField] private GameObject[] _objectsToOpen;
    public void GoToAnotherMenu()
    {
        foreach(GameObject Object in _objectsToOpen)
        {
            Object.SetActive(true);
        }
        foreach (GameObject Object in _objectsToClose)
        {
            Object.SetActive(false);
        }
    }
}
