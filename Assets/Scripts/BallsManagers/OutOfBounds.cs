using UnityEngine;
public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PoolManager.ReturnObjectToPool(collision.gameObject);
        collision.transform.position = Vector3.zero;
    }
}
