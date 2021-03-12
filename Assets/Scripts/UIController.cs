using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    bool Paused = false;
    CEventManager Event;

    private void Start()
    {
        Event = GameObject.Find("Background").GetComponent<CEventManager>();
        Event.Win += OnWin;
    }

    private void OnMouseDown()
    {
        
    }

    public void PlayPause()
    {
        if (!Paused)
        {
            Time.timeScale = 0F;
            Paused = true;
            Event.OnPauseOn();
        }
        else
        {
            Time.timeScale = 1F;
            Paused = false;
            Event.OnPauseOff();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1F;
        Paused = false;

        GameObject.Find("ResetButton").GetComponent<Image>().enabled = false;
        GameObject.Find("ResetButton").GetComponent<Button>().interactable = false;

        GameObject.Find("PlayPauseButton").GetComponent<Image>().enabled = true;
        GameObject.Find("PlayPauseButton").GetComponent<Button>().interactable = true;
    }
    private void OnWin()
    {
        Time.timeScale = 0F;
        Paused = true;

        GameObject.Find("ResetButton").GetComponent<Image>().enabled = true;
        GameObject.Find("ResetButton").GetComponent<Button>().interactable = true;

        GameObject.Find("PlayPauseButton").GetComponent<Image>().enabled = false;
        GameObject.Find("PlayPauseButton").GetComponent<Button>().interactable = false;
    }
}
