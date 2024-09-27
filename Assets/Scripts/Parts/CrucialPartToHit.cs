using System;
using UnityEngine;

public class CrucialPartToHit : MonoBehaviour
{
    public static Action onCrucialPartHit;
    [SerializeField] private string _ballTag = "Ball";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == _ballTag)
            onCrucialPartHit?.Invoke();
    }
}
