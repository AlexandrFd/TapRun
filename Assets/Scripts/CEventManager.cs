using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEventManager : MonoBehaviour
{
    public delegate void DCallback(Vector3 data);
    public delegate void DAction();
    public event DAction DrawTrajectory;
    public event DAction DestroyCoin;
    public event DAction StartMove;
    public event DAction Win;
    public event DAction FirstMove;
    public event DAction PauseOn;
    public event DAction PauseOff;


    Vector2 CoinCoordinate;
    Vector2 PlayerCoordinate;

    public void OnPauseOn()
    {
        PauseOn?.Invoke();
    }
    public void OnPauseOff()
    {
        PauseOff?.Invoke();
    }
    public void OnFirstMove()
    {
        FirstMove?.Invoke();
    }
    public void OnDraw()
    {
        DrawTrajectory?.Invoke();
    }
    public void OnStartMove()
    {
        StartMove?.Invoke();
    }

    public void OnDestroyCoin()
    {
        DestroyCoin?.Invoke();
    }

    public void OnWin()
    {
        Win?.Invoke();
    }
    public void SendCoinCoordinate(Vector3 data)
    {
        CoinCoordinate = data;
    }
    public void GetCoinCoordinate(DCallback callback)
    {
        callback(CoinCoordinate);
    }

    public void SendPlayerCoordinate(Vector3 data)
    {
        PlayerCoordinate = data;
    }
    public void GetPlayerCoordinate(DCallback callback)
    {
        callback(PlayerCoordinate);
    }
}
