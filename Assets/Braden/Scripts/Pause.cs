using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused = false;
    public KeyCode pauseKey = KeyCode.Escape;

    public GameObject mainCanvas;
    public GameObject bgCanvas;

    void Start()
    {
        Time.timeScale = 1;

        mainCanvas.SetActive(false);
        bgCanvas.SetActive(false);
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

        mainCanvas.SetActive(state);
        bgCanvas.SetActive(state);

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
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
