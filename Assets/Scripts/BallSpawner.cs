using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Transform _ballHolder, _ballSpawn;
    [SerializeField] private GameObject[] _spawnableBalls;
    [SerializeField] private float _spawnForceX = 1, _spawnForceY = 1;
    private void OnEnable()
    {
        ShopButtons.onBallSpawn += BallSpawn;
    }
    private void OnDisable()
    {
        ShopButtons.onBallSpawn -= BallSpawn;
    }
    private void BallSpawn(int BallIndex)
    {
        GameObject NewBall = PoolManager.SpawnObject(_spawnableBalls[BallIndex], _ballSpawn.position, Quaternion.identity, _ballHolder);
        NewBall.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-_spawnForceX, _spawnForceX), Random.Range(-_spawnForceY, _spawnForceY)));
    }
}