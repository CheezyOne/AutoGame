using System;
using System.Collections;
using UnityEngine;

public class CrucialPartToHit : MonoBehaviour
{
    public static Action onCrucialPartHit, onCrucialPartRegenerate;
    [SerializeField] private string _ballTag = "Ball";
    [SerializeField] private GameObject _plus, _minus;
    [SerializeField] private float _timeToRegenerate;
    private float _rememberRegenerateTime;
    private void Start()
    {
        _rememberRegenerateTime = _timeToRegenerate;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _minus.SetActive(false);
        _plus.SetActive(true);
        if (collision.transform.tag == _ballTag)
            onCrucialPartHit?.Invoke();
        if (_timeToRegenerate > 0)
            StartCoroutine(Regenerate());
    }
    private IEnumerator Regenerate()
    {
        yield return new WaitForSeconds(_timeToRegenerate);
        _plus.SetActive(false);
        _minus.SetActive(true);
        _timeToRegenerate = _rememberRegenerateTime;
        onCrucialPartRegenerate?.Invoke();
    }
}
