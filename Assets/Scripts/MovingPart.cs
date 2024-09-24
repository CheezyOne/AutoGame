using DG.Tweening;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
    private Tween _tween;
    [SerializeField] private bool _activatedOnPurchase = false;
    [SerializeField] private Vector2 _destinationChange;
    [SerializeField] private float _timeToMove = 1f;
    public bool IsActive = false;
    private void OnEnable()
    {
        if (!_activatedOnPurchase)
            StartMoving();
    }
    private void Update()
    {
        if (!IsActive)
            _tween.Pause();
    }
    public void StartMoving()//Requires fix
    {
        Sequence TweeningSequence = DOTween.Sequence();
        Vector2 Destination = transform.position;
        TweeningSequence.Append(transform.DOMove(Destination + _destinationChange, _timeToMove)).Append(transform.DOMove(Destination, _timeToMove));
        TweeningSequence.SetLoops(-1).Play();
    }
}
