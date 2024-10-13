using UnityEngine;

public class AutoGameplayBall : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
