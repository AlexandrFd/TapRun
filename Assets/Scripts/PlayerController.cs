using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    bool MoveOn = false;
    CEventManager Event;
    Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Event = GameObject.Find("Background").GetComponent<CEventManager>();
        Event.StartMove += OnMove;
        Event.SendPlayerCoordinate(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveOn)
            transform.Translate(-Direction * PlayerSpeed * Time.deltaTime);
    }


    void OnMove()
    {
        Event.GetCoinCoordinate(GetCoordinate);
    }

    void GetCoordinate(Vector3 data)
    {
        if (data != Vector3.zero)
        {
            Direction = transform.position - data;
            MoveOn = true;
        }
    }

    void ScaleUp()
    {
        transform.localScale += new Vector3(0.1F, 0.1F, 0.1F); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScaleUp();
    }
}
