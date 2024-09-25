using DG.Tweening;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
    private Sequence TweeningSequence;
    [SerializeField] private bool _activatedOnPurchase = false;
    [SerializeField] private Vector2 _destinationChange;
    [SerializeField] private float _timeToMove = 1f;
    [SerializeField] private Ease _easeType = Ease.InSine;
    public bool IsActive = false;
    private void OnEnable()
    {
        if (!_activatedOnPurchase)
            StartMoving();
    }
    private void Update()
    {
        if (!IsActive)
            TweeningSequence.Pause();
        else TweeningSequence?.Play();
    }
    public void StartMoving()
    {
        Vector2 Destination = transform.position;
        TweeningSequence = DOTween.Sequence();
        TweeningSequence.Append(transform.DOMove(Destination + _destinationChange, _timeToMove).SetEase(_easeType)).Append(transform.DOMove(Destination, _timeToMove).SetEase(_easeType));
        TweeningSequence.SetLoops(-1).Play();
    }
}
