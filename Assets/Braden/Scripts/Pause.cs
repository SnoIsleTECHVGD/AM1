using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused = false;
    public KeyCode pauseKey = KeyCode.Escape;

    public Canvas mainCanvas;
    public Canvas bgCanvas;

    void Start()
    {
        mainCanvas.enabled = false;
        bgCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            SetPaused(!isPaused);
        }
    }

    // Main

    public void SetPaused(bool state)
    {
        if (state == isPaused)
            return;

        isPaused = state;

        mainCanvas.enabled = state;
        bgCanvas.enabled = state;

        if (state == true)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }

    public void SwitchToMenu()
    {
        SetPaused(false);
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
