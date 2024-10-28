using System;
using UnityEngine;
public class BallSpawner : MonoBehaviour
{
    public static Action onBallRefund;
    [SerializeField] private GameObject _closedBin, _openedBin;
    [SerializeField] private Transform _upperBinBorder, _rightBinBorder;
    private GameObject _heldBall;
    private Rigidbody2D _ballRB;
    private int _amountOfDeadZones = 0;
    private const string BALL_LAYER = "Ball", DEAD_ZONE_LAYER = "Dead zone";
    private void OnEnable()
    {
        ShopButtons.onBallSpawn += BallSpawn;
        LevelsLoader.onLevelStart += DisableBalls;
        DeadZone.onBallExit += DecreaseDeadZones;
        DeadZone.onBallEnter += IncreaseDeadZones;
    }
    private void OnDisable()
    {
        ShopButtons.onBallSpawn -= BallSpawn;
        LevelsLoader.onLevelStart -= DisableBalls;
        DeadZone.onBallExit -= DecreaseDeadZones;
        DeadZone.onBallEnter += IncreaseDeadZones;
    }
    private void IncreaseDeadZones()
    {
        _amountOfDeadZones++;
    }
    private void DecreaseDeadZones()
    {
        _amountOfDeadZones--;
    }
    private void BallSpawn(GameObject SpawnBall)
    {
        Vector3 SpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        _heldBall = PoolManager.SpawnObject(SpawnBall, SpawnPosition, Quaternion.identity, transform);
        _ballRB = _heldBall.GetComponent<Rigidbody2D>();
        _ballRB.gameObject.layer = LayerMask.NameToLayer(DEAD_ZONE_LAYER);
        _ballRB.bodyType = RigidbodyType2D.Static;
        _amountOfDeadZones = 0;
    }
    private void DisableBalls()
    {
        _amountOfDeadZones = 0;
        for (int i=0;i< transform.childCount;i++)
        {
            PoolManager.ReturnObjectToPool(transform.GetChild(i).gameObject);
        }
    }
    public void ReleaseHeldBall()
    {
        if (_heldBall == null)
            return;
        if (ObjectInBin())
            DisableHeldBall();
        else
            EnableHeldBall();
        _heldBall = null;
        _amountOfDeadZones = 0;
    }
    private void EnableHeldBall()
    {
        _ballRB.bodyType = RigidbodyType2D.Dynamic;
        _ballRB.gameObject.layer = LayerMask.NameToLayer(BALL_LAYER);
    }
    private void DisableHeldBall()
    {
        onBallRefund?.Invoke();
        PoolManager.ReturnObjectToPool(_heldBall);
    }
    private bool ObjectInBin()
    {
        if(_amountOfDeadZones>0)
            return true;
        return false;
    }
    private void OpenBin()
    {
        _closedBin.SetActive(false);
        _openedBin.SetActive(true);
    }
    private void CloseBin()
    {
        _closedBin.SetActive(true);
        _openedBin.SetActive(false);
    }
    private void Update()
    {
        if (_heldBall == null)
        {
            CloseBin();
            return;
        }
        if (ObjectInBin())
            OpenBin();
        else
            CloseBin();
        _heldBall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }
}