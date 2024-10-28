using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public static Action onBallEnter, onBallExit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onBallEnter?.Invoke();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onBallExit?.Invoke();
    }
}
