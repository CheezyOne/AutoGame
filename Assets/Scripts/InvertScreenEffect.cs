using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class InvertScreenEffect : MonoBehaviour
{
    [SerializeField] private string _effectName;
    private Animation _animationComponent;
    private void Awake()
    {
        _animationComponent = GetComponent<Animation>();
    }
    private void OnEnable()
    {
        InLevelTimer.onTimerExpire += StartEffect;
    }
    private void OnDisable()
    {
        InLevelTimer.onTimerExpire -= StartEffect;
    }

    [Button]
    private void StartEffect()
    {
        _animationComponent.Stop();
        _animationComponent.Play(_effectName);
    }
}
