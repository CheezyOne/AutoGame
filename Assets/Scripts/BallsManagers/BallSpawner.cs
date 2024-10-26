using System;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static Action onBallRefund;
    [SerializeField] private GameObject _closedBin, _openedBin;
    [SerializeField] private Transform _upperBinBorder, _rightBinBorder;
    private GameObject _heldBall;
    private Rigidbody2D _ballRB;
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
        Vector3 SpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        _heldBall = PoolManager.SpawnObject(SpawnBall, SpawnPosition, Quaternion.identity, transform);
        _ballRB = _heldBall.GetComponent<Rigidbody2D>();
        _ballRB.bodyType = RigidbodyType2D.Static;
    }
    private void DisableBalls()
    {
        for (int i=0;i< transform.childCount;i++)
        {
            PoolManager.ReturnObjectToPool(transform.GetChild(i).gameObject);
        }
    }
    public void ReleaseHeldBall()
    {
        if (_heldBall == null)
            return;
        if (ObjectInBin(_heldBall.transform))
            DisableHeldBall();
        else
            EnableHeldBall();
        _heldBall = null;
    }
    private void EnableHeldBall()
    {
        _ballRB.bodyType = RigidbodyType2D.Dynamic;
    }
    private void DisableHeldBall()
    {
        onBallRefund?.Invoke();
        PoolManager.ReturnObjectToPool(_heldBall);
    }
    private bool ObjectInBin(Transform objectToCheck)
    {
        if(objectToCheck.position.x < _rightBinBorder.position.x && objectToCheck.position.y < _upperBinBorder.position.y)
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
        if (ObjectInBin(_heldBall.transform))
            OpenBin();
        else
            CloseBin();
        _heldBall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }
}
