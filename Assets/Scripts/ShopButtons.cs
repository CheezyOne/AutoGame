using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private GameObject[] _spawnableBalls;
    [SerializeField] private Transform[] _spawns, _ballsHolders;
    public void SpawnNewBall(int BallIndex)
    {
        PoolManager.SpawnObject(_spawnableBalls[BallIndex], _spawns[_levelIndex].position,Quaternion.identity, _ballsHolders[_levelIndex]);
    }
}
