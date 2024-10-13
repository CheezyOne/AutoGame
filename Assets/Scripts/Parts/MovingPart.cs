using DG.Tweening;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
    private Sequence TweeningSequence;
    [SerializeField] private bool _activatedOnPurchase = false, _shallReturnToStart = true;
    [SerializeField] private Transform[] _destinations;
    [SerializeField] private float[] _timesToMove;
    [SerializeField] private int _loopTimes = -1;
    [SerializeField] private Ease _easeType = Ease.InSine;
    public bool IsActive = false, IsBought = false, CanBeStopped = true;
    private void OnEnable()
    {
        if (!_activatedOnPurchase)
            StartMoving();
    }
    private void Update()
    {
        if (!IsActive || !IsBought)
            TweeningSequence.Pause();
        else TweeningSequence?.Play();
    }
    public void StartMoving()
    {
        TweeningSequence = DOTween.Sequence();
        for(int i=0;i<_destinations.Length;i++)
        {
            TweeningSequence.Append(transform.DOMove(_destinations[i].position, _timesToMove[i]).SetEase(_easeType));
        }
        if (_shallReturnToStart)
            TweeningSequence.Append(transform.DOMove(transform.position, _timesToMove[_timesToMove.Length - 1]).SetEase(_easeType));
        TweeningSequence.SetLoops(_loopTimes).Play();
    }
}
