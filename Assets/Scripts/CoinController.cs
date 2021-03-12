using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public bool Active = false;
    CEventManager Event;

    // Start is called before the first frame update
    void Start()
    {
        Event = GameObject.Find("Background").GetComponent<CEventManager>();
        Event.PauseOn += OnPauseMode;
    }

    private void OnPauseMode()
    {
        Active = false;
    }

    private void OnMouseDown()
    {
        if (Active)
        {
            Event.OnDraw();
            Event.OnFirstMove();
            Active = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            Event.OnDestroyCoin();
            Destroy(gameObject);
        }
    }
}
