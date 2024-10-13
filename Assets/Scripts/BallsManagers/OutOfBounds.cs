using UnityEngine;
public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.localPosition = Vector3.zero;
        collision.rigidbody.velocity = Vector3.zero;
        PoolManager.ReturnObjectToPool(collision.gameObject);
    }
}
