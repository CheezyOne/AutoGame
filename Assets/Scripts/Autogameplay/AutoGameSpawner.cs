using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGameSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private GameObject[] _ballsToSpawn;
    [SerializeField] private float _minSpawnDelay, _maxSpawnDelay;
    [SerializeField] private float _spawnForceX = 1, _spawnForceY = 1;
    [SerializeField] private int[] _ballThresholds;
    private float _spawnDelay;
    private int _allowedBallIndex;
    private const string LEVELS_PREFS = "BestLevel";
    private void OnEnable()
    {
        SetAllowedBall();
        StartCoroutine(SpawnNewBall());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator SpawnNewBall()
    {
        _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        int BallIndex = Random.Range(0, _allowedBallIndex + 1);
        yield return new WaitForSeconds(_spawnDelay);
        GameObject NewBall = PoolManager.SpawnObject(_ballsToSpawn[BallIndex],_spawn.position,Quaternion.identity, _spawn);
        Rigidbody2D BallRB = NewBall.GetComponent<Rigidbody2D>();
        BallRB.velocity = Vector3.zero;
        BallRB.AddForce(new Vector3(Random.Range(-_spawnForceX, _spawnForceX), Random.Range(-_spawnForceY, _spawnForceY)));
        yield return SpawnNewBall();
    }
    private void SetAllowedBall()
    {
        for (int i=0;i<_ballThresholds.Length;i++)
        {
            if (PlayerPrefs.GetInt(LEVELS_PREFS) > _ballThresholds[i])
                _allowedBallIndex = i+1;
        }
    }
}
