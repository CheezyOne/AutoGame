using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Transform _ballHolder, _ballSpawn;
    [SerializeField] private float _spawnForceX = 1, _spawnForceY = 1;
    private void OnEnable()
    {
        ShopButtons.onBallSpawn += BallSpawn;
    }
    private void OnDisable()
    {
        ShopButtons.onBallSpawn -= BallSpawn;
        DisableBalls();
    }
    private void BallSpawn(GameObject SpawnBall)
    {
        GameObject NewBall = PoolManager.SpawnObject(SpawnBall, _ballSpawn.position, Quaternion.identity, _ballHolder);
        NewBall.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-_spawnForceX, _spawnForceX), Random.Range(-_spawnForceY, _spawnForceY)));
    }
    private void DisableBalls()
    {
        for (int i=0;i< _ballHolder.childCount;i++)
        {
            PoolManager.ReturnObjectToPool(_ballHolder.GetChild(i).gameObject);
        }
    }
}
