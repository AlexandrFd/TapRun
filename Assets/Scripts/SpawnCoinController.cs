using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnCoinController : MonoBehaviour
{
    float Height;
    float Width;

    public GameObject Coin;
    public int CoinCount;
    public Camera Camera;
    public LineRenderer Line;


    List<GameObject> CoinList;
    List<GameObject> TargetList;

    CEventManager Event;
    Vector3 PlayerCoordinate;
    Vector3[] CoordinateArray;



    // Start is called before the first frame update
    void Start()
    {
        Height = Camera.pixelHeight;
        Width = Camera.pixelWidth;

        Event = GameObject.Find("Background").GetComponent<CEventManager>();
        Event.DrawTrajectory += OnDrawTrajectory;
        Event.DestroyCoin += OnDestroyCoin;
        Event.FirstMove += OnSendFirstCoordinate;
        Event.PauseOff += OnPauseOff;

        TargetList = new List<GameObject>();
        CreateCoin();
    }


    void CreateCoin()
    {
        CoinList = new List<GameObject>();
        for (int i = 0; i < CoinCount; i++)
        {
            CoinList.Add(Instantiate(Coin, Camera.ScreenToWorldPoint(new Vector3(Random.Range(100F, Width  - 100F),
                  Random.Range(200F, Height - 200F), Camera.farClipPlane)), Quaternion.identity, transform));
        }


        SortCoinList();
        ActivateFirstCoin();
    }

    void OnDrawTrajectory()
    {  
        Event.GetPlayerCoordinate(GetPlayerCoordinate);
        DrawLine();
    }
    private void OnSendFirstCoordinate()
    {
        Event.SendCoinCoordinate(TargetList.First<GameObject>().transform.position);
        TargetList.RemoveAt(0);
        Event.OnStartMove();
    }

    private void OnDestroyCoin()
    {
        if (TargetList.Count > 0)
        {
            Event.SendCoinCoordinate(TargetList.First<GameObject>().transform.position);
            TargetList.RemoveAt(0);
            Event.OnStartMove();
        }
        else
            Event.OnWin();
    }
    private void OnPauseOff()
    {
        for (int i = CoinList.Count-1; i > -1; i--)
        {
            if (CoinList[i] == null)
            {
                CoinList.RemoveAt(i);
            }
        }
        for (int j = CoinList.Count-1; j > 0; j--)
        {
            if (CoinList[j].transform.position == TargetList.First<GameObject>().transform.position)
                CoinList[j].GetComponent<CoinController>().Active = true;
        }
    }

    void GetPlayerCoordinate(Vector3 data)
    {
        PlayerCoordinate = data;
    }

    void SortCoinList()
    {
        int indexarray = 1;
        CoordinateArray = new Vector3[CoinCount + 1];

        var sorteditem = from i in CoinList
                         orderby i.transform.position.y
                         select i;

        foreach (var item in sorteditem)
        {
            CoordinateArray[indexarray] = item.transform.position;
            TargetList.Add(item);
            indexarray++;
        }
    }

    void DrawLine()
    {
        CoordinateArray[0] = PlayerCoordinate;

        LineRenderer line;
        line = Instantiate(Line);
        line.positionCount = CoordinateArray.Length;

        for (int i = 0; i < CoordinateArray.Length; i++)
        {
            line.SetPosition(i, CoordinateArray[i]);
        }
    }

    void ActivateFirstCoin()
    {
        foreach (GameObject item in CoinList)
        {
            if (item.transform.position == TargetList[0].transform.position)
            {
                item.GetComponent<CoinController>().Active = true;
                item.transform.localScale += new Vector3(0.2f, 0.2f, 0);
            }
        }
    }
}
